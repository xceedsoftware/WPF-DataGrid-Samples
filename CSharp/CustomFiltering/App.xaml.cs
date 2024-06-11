/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Custom Filtering sample.
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
using Xceed.Wpf.DataGrid.Views;
using System.Collections.ObjectModel;

namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( System.Windows.StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_orders = dataSet.Tables[ "Orders" ];
      m_employees = dataSet.Tables[ "Employees" ];

      base.OnStartup( e );
    }

    #region Employees Property

    private DataTable m_employees;

    public DataTable Employees
    {
      get
      {
        return m_employees;
      }
    }

    #endregion

    #region Orders Property

    private DataTable m_orders;

    public DataTable Orders
    {
      get
      {
        return m_orders;
      }
    }

    #endregion
  }
}