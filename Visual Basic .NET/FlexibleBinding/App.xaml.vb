'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [App.xaml.vb]
' 
' This is the Application class of the Flexible Binding sample.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Data
Imports System.Xml
Imports System.Configuration

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  Partial Public Class App
    Inherits System.Windows.Application
    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_categories = dataSet.Tables("Categories")
      m_products = dataSet.Tables("Products")
      m_suppliers = dataSet.Tables("Suppliers")
      m_orders = dataSet.Tables("Orders")
      m_customers = dataSet.Tables("Customers")
      m_employees = dataSet.Tables("Employees")

      MyBase.OnStartup(e)
    End Sub

    Private m_categories As DataTable

    Public ReadOnly Property Categories() As DataTable
      Get
        Return m_categories
      End Get
    End Property

    Private m_products As DataTable

    Public ReadOnly Property Products() As DataTable
      Get
        Return m_products
      End Get
    End Property

    Private m_suppliers As DataTable

    Public ReadOnly Property Suppliers() As DataTable
      Get
        Return m_suppliers
      End Get
    End Property

    Private m_orders As DataTable

    Public ReadOnly Property Orders() As DataTable
      Get
        Return m_orders
      End Get
    End Property

    Private m_customers As DataTable

    Public ReadOnly Property Customers() As DataTable
      Get
        Return m_customers
      End Get
    End Property

    Private m_employees As DataTable

    Public ReadOnly Property Employees() As DataTable
      Get
        Return m_employees
      End Get
    End Property
  End Class
End Namespace
