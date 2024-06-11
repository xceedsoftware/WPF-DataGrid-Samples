/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Views 3D sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.Views3D
{
  public partial class App : Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet northwindDataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_products = northwindDataSet.Tables[ "Products" ];

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
