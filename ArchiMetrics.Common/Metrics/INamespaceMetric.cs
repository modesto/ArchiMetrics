// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INamespaceMetric.cs" company="Reimers.dk">
//   Copyright � Reimers.dk 2013
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the INamespaceMetric type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMetrics.Common.Metrics
{
	using System.Collections.Generic;

	public interface INamespaceMetric : ICodeMetric
	{
		IEnumerable<TypeCoupling> ClassCouplings { get; }
		
		int DepthOfInheritance { get; }
		
		IEnumerable<ITypeMetric> TypeMetrics { get; }
	}
}