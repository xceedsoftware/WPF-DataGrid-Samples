/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Master/Detail sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;
using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( System.Windows.StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_employees = dataSet.Tables[ "Employees" ];
      m_customers = dataSet.Tables[ "Customers" ];
      m_shippers = dataSet.Tables[ "Shippers" ];
      m_products = dataSet.Tables[ "Products" ];

      base.OnStartup( e );
    }

    #region Employees property

    private DataTable m_employees;

    public DataTable Employees
    {
      get
      {
        return m_employees;
      }
    }

    #endregion Employees property

    #region Customers property

    private DataTable m_customers;

    public DataTable Customers
    {
      get
      {
        return m_customers;
      }
    }

    #endregion Customers property

    #region Shippers property

    private DataTable m_shippers;

    public DataTable Shippers
    {
      get
      {
        return m_shippers;
      }
    }

    #endregion Shippers property

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