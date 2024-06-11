'
' * Xceed DataGrid for WPF - SAMPLE CODE - Selection Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' * 
' * This is the main application of the Selection sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows
Imports System.Windows.Navigation
Imports Xceed.Wpf.DataGrid
Imports System.Collections.ObjectModel

Namespace Xceed.Wpf.DataGrid.Samples.Selection
  Partial Public Class App
    Inherits System.Windows.Application
    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_orders = dataSet.Tables("Orders")
      m_employees = dataSet.Tables("Employees")
      m_customers = dataSet.Tables("Customers")
      m_suppliers = dataSet.Tables("Suppliers")

      MyBase.OnStartup(e)
    End Sub

    Private m_orders As DataTable

    Public ReadOnly Property Orders() As DataTable
      Get
        Return m_orders
      End Get
    End Property

    Private m_employees As DataTable

    Public ReadOnly Property Employees() As DataTable
      Get
        Return m_employees
      End Get
    End Property

    Private m_customers As DataTable

    Public ReadOnly Property Customers() As DataTable
      Get
        Return m_customers
      End Get
    End Property

    Private m_suppliers As DataTable

    Public ReadOnly Property Suppliers() As DataTable
      Get
        Return m_suppliers
      End Get
    End Property

  End Class
End Namespace