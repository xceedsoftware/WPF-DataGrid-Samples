/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the Included Editors sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
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
