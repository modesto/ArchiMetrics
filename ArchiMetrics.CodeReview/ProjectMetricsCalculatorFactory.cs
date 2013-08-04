// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectMetricsCalculatorFactory.cs" company="Reimers.dk">
//   Copyright � Reimers.dk 2012
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the ProjectMetricsCalculatorFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMetrics.CodeReview
{
	using System;
	using ArchiMetrics.Analysis.Metrics;
	using ArchiMetrics.Common;
	using ArchiMetrics.Common.Metrics;

	public class ProjectMetricsCalculatorFactory : IFactory<ProjectSettings, ICodeMetricsCalculator>
	{
		~ProjectMetricsCalculatorFactory()
		{
			Dispose(false);
		}

		public ICodeMetricsCalculator Create(ProjectSettings parameter)
		{
			return new CodeMetricsCalculator();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				// Dispose of any managed resources here. If this class contains unmanaged resources, dispose of them outside of this block. If this class derives from an IDisposable class, wrap everything you do in this method in a try-finally and call base.Dispose in the finally.
			}
		}
	}
}
