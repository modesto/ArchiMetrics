﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Roche">
//   Copyright © Roche 2012
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993] for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMeter.UI
{
	using System;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Markup;
	using Analysis;
	using ArchiCop.UI.ViewModel;
	using Autofac;
	using CodeReview;
	using CodeReview.Metrics;
	using Common;
	using Common.Metrics;
	using Controller;
	using Data.DataAccess;
	using Roslyn.Services;

	public partial class App : Application
	{
		static App()
		{
			// Ensure the current culture passed into bindings is the OS culture.
			// By default, WPF uses en-US as the culture, regardless of the system settings.
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement), 
				new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var builder = new ContainerBuilder();

			// container.RegisterType<IBuildItemRepository, FakeBuildItemRepository>(
			// 	new ContainerControlledLifetimeManager());
			builder.RegisterType<DefaultCollectionCopier>()
				   .As<ICollectionCopier>()
				   .SingleInstance();
			var config = new SolutionEdgeItemsRepositoryConfig();
			builder.RegisterInstance<ISolutionEdgeItemsRepositoryConfig>(config);

			builder.RegisterInstance(DefinedRules.Default);
			builder.RegisterType<ProjectMetricsCalculator>()
				   .As<IProjectMetricsCalculator>();
			builder.RegisterType<SolutionInspector>()
				   .As<INodeInspector>();
			var vertexRuleRepository = new FakeVertexRuleRepository();
			builder.RegisterInstance(new PathFilter(x => true))
				   .As<PathFilter>();
			builder.RegisterType<SolutionProvider>()
				   .As<IProvider<ISolution>>();
			builder.RegisterType<ProjectProvider>()
				   .As<IProvider<IProject>>();
			builder.RegisterType<ArchiMeterController>();
			builder.RegisterType<CodeErrorRepository>()
				   .As<ICodeErrorRepository>();
			builder.RegisterType<AggregateEdgeItemsRepository>()
				   .As<IEdgeItemsRepository>();
			builder.RegisterInstance<IVertexRuleRepository>(vertexRuleRepository);
			builder.RegisterInstance<IVertexRuleDefinition>(vertexRuleRepository);
			builder.RegisterType<EdgeTransformer>()
				   .As<IEdgeTransformer>();
			builder.RegisterType<RequirementTestAnalyzer>()
				   .As<IRequirementTestAnalyzer>();

			// Create the ViewModel to which 
			// the main window binds.            
			var viewModel = new MainWindowViewModel(config);

			builder.RegisterInstance<IShell>(viewModel);
			var container = builder.Build();
			container.Resolve<ArchiMeterController>();

			var window = new MainWindow();

			// When the ViewModel asks to be closed, 
			// close the window.
			EventHandler handler = null;
			handler = (sender, args) =>
						  {
							  viewModel.RequestClose -= handler;
							  window.Close();
						  };
			viewModel.RequestClose += handler;

			// Allow all controls in the window to 
			// bind to the ViewModel by setting the 
			// DataContext, which propagates down 
			// the element tree.
			window.DataContext = viewModel;

			window.Show();
		}
	}
}