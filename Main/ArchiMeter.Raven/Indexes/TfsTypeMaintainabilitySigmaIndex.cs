﻿namespace ArchiMeter.Raven.Indexes
{
	using System;
	using System.Linq;

	using ArchiMeter.Common.Documents;

	using global::Raven.Client.Indexes;

	public class TfsTypeMaintainabilitySigmaIndex : AbstractIndexCreationTask<TfsMetricsDocument, TypeMaintainabilityDeviation>
	{
		public TfsTypeMaintainabilitySigmaIndex()
		{
			Map = docs => from doc in docs
			              from namespaceMetric in doc.Metrics
			              from typeMetric in namespaceMetric.TypeMetrics
			              select new
				                     {
					                     doc.ProjectName,
					                     NamespaceName = namespaceMetric.Name,
					                     TypeName = typeMetric.Name,
					                     typeMetric.MaintainabilityIndex,
					                     Sigma = 0.0
				                     };

			Reduce = docs =>
			         from doc in docs
			         group doc by true
			         into grouping
			         let a = new
				                 {
					                 AverageMI = grouping.Average(x => x.MaintainabilityIndex),
					                 Items = grouping
				                 }
			         let sd = new
				                  {
					                  AverageLoC = a.AverageMI,
					                  StandardDev = Math.Sqrt(a.Items.Sum(t => (t.MaintainabilityIndex - a.AverageMI) * (t.MaintainabilityIndex - a.AverageMI)) / a.Items.Count()),
					                  a.Items,
				                  }
			         from t in sd.Items
			         select new
				                {
					                t.ProjectName,
					                t.NamespaceName,
					                t.TypeName,
					                t.MaintainabilityIndex,
					                Sigma = (t.MaintainabilityIndex - a.AverageMI) / sd.StandardDev
				                };
		}
	}
}