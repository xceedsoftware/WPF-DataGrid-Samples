/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Formatting Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Formatting sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;

namespace Xceed.Wpf.DataGrid.Samples.Formatting
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( System.Windows.StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_products = dataSet.Tables[ "Products" ];

      base.OnStartup( e );
    }

    #region Products property

    private DataTable m_products;

    public DataTable Products
    {
      get
      {
        return m_products;
      }
    }

    #endregion Products property
  }
}