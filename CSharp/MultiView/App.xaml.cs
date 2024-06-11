/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Multi-View sample.
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
using Xceed.Wpf.DataGrid.Samples;
using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid;

namespace Xceed.Wpf.DataGrid.Samples.MultiView
{
  public partial class App : Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_products = dataSet.Tables[ "Products" ];

      base.OnStartup( e );
    }

    #region Products Property

    private DataTable m_products;

    public DataTable Products
    {
      get
      {
        return m_products;
      }
    }

    #endregion Products Property
  }
}
