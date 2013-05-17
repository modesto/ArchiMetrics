﻿namespace ArchiMeter.Raven.Indexes
{
	using System;
	using System.Linq;

	using ArchiMeter.Common.Documents;

	using global::Raven.Client.Indexes;

	public class TfsTypeSizeSigmaIndex : AbstractIndexCreationTask<TfsMetricsDocument, TypeSizeDeviation>
	{
		public TfsTypeSizeSigmaIndex()
		{
			Map = docs => from doc in docs
						  from namespaceMetric in doc.Metrics
						  from typeMetric in namespaceMetric.TypeMetrics
						  select new
								 {
									 doc.ProjectName,
									 NamespaceName = namespaceMetric.Name,
									 TypeName = typeMetric.Name,
									 LoC = typeMetric.LinesOfCode,
									 Sigma = 0.0
								 };

			Reduce = docs =>
					 from doc in docs
					 group doc by true
						 into grouping
						 let a = new
									 {
										 AverageLoC = grouping.Average(x => x.LoC),
										 Items = grouping
									 }
						 let sd = new
									  {
										  a.AverageLoC,
										  StandardDev = Math.Sqrt(a.Items.Sum(t => (t.LoC - a.AverageLoC) * (t.LoC - a.AverageLoC)) / a.Items.Count()),
										  a.Items,
									  }
						 from t in sd.Items
						 select new
									{
										t.ProjectName,
										t.NamespaceName,
										t.TypeName,
										t.LoC,
										Sigma = (t.LoC - a.AverageLoC) / sd.StandardDev
									};
		}
	}
}