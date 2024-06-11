'
' Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [App.xaml.vb]
'
' This is the Application class of the Master/Detail sample.
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

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  Partial Public Class App
    Inherits System.Windows.Application

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)

      XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_employees = dataSet.Tables.Item("Employees")
      m_customers = dataSet.Tables.Item("Customers")
      m_shippers = dataSet.Tables.Item("Shippers")
      m_products = dataSet.Tables.Item("Products")

      MyBase.OnStartup(e)

    End Sub

#Region "Employees property"

    Private m_employees As DataTable

    Public ReadOnly Property Employees() As DataTable
      Get
        Return m_employees
      End Get
    End Property

#End Region

#Region "Customers property"

    Private m_customers As DataTable

    Public ReadOnly Property Customers() As DataTable
      Get
        Return m_customers
      End Get
    End Property

#End Region

#Region "Shippers property"

    Private m_shippers As DataTable

    Public ReadOnly Property Shippers() As DataTable
      Get
        Return m_shippers
      End Get
    End Property

#End Region

#Region "Products property"

    Private m_products As DataTable

    Public ReadOnly Property Products() As DataTable
      Get
        Return m_products
      End Get
    End Property

#End Region

  End Class
End Namespace
