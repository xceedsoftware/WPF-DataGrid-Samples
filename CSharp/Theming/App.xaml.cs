/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Theming sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Navigation;
using Xceed.Wpf.DataGrid;
using System.Collections.ObjectModel;

namespace Xceed.Wpf.DataGrid.Samples.Theming
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_orders = dataSet.Tables[ "Orders" ];
      m_employees = dataSet.Tables[ "Employees" ];
      m_customers = dataSet.Tables[ "Customers" ];
      m_shippers = dataSet.Tables[ "Shippers" ];

      base.OnStartup( e );
    }

    #region Orders property

    private DataTable m_orders;

    public DataTable Orders
    {
      get
      {
        return m_orders;
      }
    }

    #endregion

    #region Employees property

    private DataTable m_employees;

    public DataTable Employees
    {
      get
      {
        return m_employees;
      }
    }

    #endregion

    #region Customers property

    private DataTable m_customers;

    public DataTable Customers
    {
      get
      {
        return m_customers;
      }
    }

    #endregion

    #region Shippers property

    private DataTable m_shippers;

    public DataTable Shippers
    {
      get
      {
        return m_shippers;
      }
    }

    #endregion

    #region ViewChanged Event

    public event EventHandler ViewChanged;

    public void OnViewChanged( EventArgs e )
    {
      if( this.ViewChanged != null )
        this.ViewChanged( this, e );
    }

    #endregion ViewChanged Event
  }
}