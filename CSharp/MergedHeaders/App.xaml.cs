/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Merged Column Headers Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Merged Column Headers sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      base.OnStartup( e );
    }
  }
}