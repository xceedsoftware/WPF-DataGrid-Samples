/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the Flexible Binding sample.
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

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_categories = dataSet.Tables[ "Categories" ];
      m_products = dataSet.Tables[ "Products" ];
      m_suppliers = dataSet.Tables[ "Suppliers" ];
      m_orders = dataSet.Tables[ "Orders" ];
      m_customers = dataSet.Tables[ "Customers" ];
      m_employees = dataSet.Tables[ "Employees" ];

      base.OnStartup( e );
    }

    private DataTable m_categories;

    public DataTable Categories
    {
      get
      {
        return m_categories;
      }
    }

    private DataTable m_products;

    public DataTable Products
    {
      get
      {
        return m_products;
      }
    }

    private DataTable m_suppliers;

    public DataTable Suppliers
    {
      get
      {
        return m_suppliers;
      }
    }

    private DataTable m_orders;

    public DataTable Orders
    {
      get
      {
        return m_orders;
      }
    }

    private DataTable m_customers;

    public DataTable Customers
    {
      get
      {
        return m_customers;
      }
    }

    private DataTable m_employees;

    public DataTable Employees
    {
      get
      {
        return m_employees;
      }
    }
  }
}