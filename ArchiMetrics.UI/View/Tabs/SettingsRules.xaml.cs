﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsRules.xaml.cs" company="Reimers.dk">
//   Copyright © Reimers.dk 2013
//   This source is subject to the Microsoft Public License (Ms-PL).
//   Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
//   All other rights reserved.
// </copyright>
// <summary>
//   Interaction logic for SettingsRules.xaml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ArchiMetrics.UI.View.Tabs
{
	using System.Windows.Controls;
	using ArchiMetrics.UI.Support;
	using ArchiMetrics.UI.ViewModel;

	/// <summary>
	/// Interaction logic for SettingsRules.xaml.
	/// </summary>
	[DataContext(typeof(SettingsViewModel))]
	public partial class SettingsRules : UserControl
	{
		public SettingsRules()
		{
			InitializeComponent();
		}
	}
}