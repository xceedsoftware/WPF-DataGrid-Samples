/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Spanned Cells sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();

      System.Data.DataRow emptyRow;

      m_employeesWithEmptyEntry = dataSet.Tables[ "Employees" ].Copy();
      emptyRow = m_employeesWithEmptyEntry.NewRow();
      emptyRow[ "EmployeeID" ] = DBNull.Value;
      m_employeesWithEmptyEntry.Rows.InsertAt( emptyRow, 0 );

      m_customersWithEmptyEntry = dataSet.Tables[ "Customers" ].Copy();
      emptyRow = m_customersWithEmptyEntry.NewRow();
      emptyRow[ "CustomerID" ] = DBNull.Value;
      m_customersWithEmptyEntry.Rows.InsertAt( emptyRow, 0 );

      m_shippersWithEmptyEntry = dataSet.Tables[ "Shippers" ].Copy();
      emptyRow = m_shippersWithEmptyEntry.NewRow();
      emptyRow[ "ShipperID" ] = DBNull.Value;
      m_shippersWithEmptyEntry.Rows.InsertAt( emptyRow, 0 );

      m_orders = dataSet.Tables[ "Orders" ];

      base.OnStartup( e );
    }

    #region EmployeesWithEmptyEntry property

    private DataTable m_employeesWithEmptyEntry;

    public DataTable EmployeesWithEmptyEntry
    {
      get
      {
        return m_employeesWithEmptyEntry;
      }
    }

    #endregion EmployeesWithEmptyEntry property

    #region CustomersWithEmptyEntry property

    private DataTable m_customersWithEmptyEntry;

    public DataTable CustomersWithEmptyEntry
    {
      get
      {
        return m_customersWithEmptyEntry;
      }
    }

    #endregion CustomersWithEmptyEntry property

    #region ShippersWithEmptyEntry property

    private DataTable m_shippersWithEmptyEntry;

    public DataTable ShippersWithEmptyEntry
    {
      get
      {
        return m_shippersWithEmptyEntry;
      }
    }

    #endregion ShippersWithEmptyEntry property

    #region Orders property

    private DataTable m_orders;

    public DataTable Orders
    {
      get
      {
        return m_orders;
      }
    }

    #endregion Orders property
  }
}