'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [App.xaml.cs]
'*
'* This is the main application of the Tableflow™ View sample.
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows
Imports System.Windows.Navigation
Imports Xceed.Wpf.DataGrid
Imports System.Collections.ObjectModel

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Partial Class App
	  Inherits System.Windows.Application
	Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()

	  Dim emptyRow As System.Data.DataRow

	  m_employeesWithEmptyEntry = dataSet.Tables("Employees").Copy()
	  emptyRow = m_employeesWithEmptyEntry.NewRow()
	  emptyRow("EmployeeID") = DBNull.Value
	  m_employeesWithEmptyEntry.Rows.InsertAt(emptyRow, 0)

	  m_customersWithEmptyEntry = dataSet.Tables("Customers").Copy()
	  emptyRow = m_customersWithEmptyEntry.NewRow()
	  emptyRow("CustomerID") = DBNull.Value
	  m_customersWithEmptyEntry.Rows.InsertAt(emptyRow, 0)

	  m_shippersWithEmptyEntry = dataSet.Tables("Shippers").Copy()
	  emptyRow = m_shippersWithEmptyEntry.NewRow()
	  emptyRow("ShipperID") = DBNull.Value
	  m_shippersWithEmptyEntry.Rows.InsertAt(emptyRow, 0)

	  m_orders = dataSet.Tables("Orders")

	  MyBase.OnStartup(e)
	End Sub

	#Region "EmployeesWithEmptyEntry property"

	Private m_employeesWithEmptyEntry As DataTable

	Public ReadOnly Property EmployeesWithEmptyEntry() As DataTable
	  Get
		Return m_employeesWithEmptyEntry
	  End Get
	End Property

	#End Region ' EmployeesWithEmptyEntry property

	#Region "CustomersWithEmptyEntry property"

	Private m_customersWithEmptyEntry As DataTable

	Public ReadOnly Property CustomersWithEmptyEntry() As DataTable
	  Get
		Return m_customersWithEmptyEntry
	  End Get
	End Property

	#End Region ' CustomersWithEmptyEntry property

	#Region "ShippersWithEmptyEntry property"

	Private m_shippersWithEmptyEntry As DataTable

	Public ReadOnly Property ShippersWithEmptyEntry() As DataTable
	  Get
		Return m_shippersWithEmptyEntry
	  End Get
	End Property

	#End Region ' ShippersWithEmptyEntry property

	#Region "Orders property"

	Private m_orders As DataTable

	Public ReadOnly Property Orders() As DataTable
	  Get
		Return m_orders
	  End Get
	End Property

	#End Region ' Orders property
  End Class
End Namespace
