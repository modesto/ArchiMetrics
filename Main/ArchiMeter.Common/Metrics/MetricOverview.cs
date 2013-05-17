// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricOverview.cs" company="Roche">
//   Copyright � Roche 2012
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the MetricOverview type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ArchiMeter.Common.Metrics
{
	using System.Collections.Generic;

	public class MetricOverview
	{
		public MetricOverview(int sourceLinesOfCode, IEnumerable<TypeMetric> metrics)
		{
			SourceLinesOfCode = sourceLinesOfCode;
			Metrics = metrics;
		}

		public int SourceLinesOfCode { get; set; }

		public IEnumerable<TypeMetric> Metrics { get; set; }
	}
}