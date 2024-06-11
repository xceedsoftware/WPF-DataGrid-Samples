/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Live Updating sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
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