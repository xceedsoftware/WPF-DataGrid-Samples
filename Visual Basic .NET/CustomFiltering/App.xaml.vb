'
'* Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [App.xaml.vb]
'*
'* This is the main application of the Custom Filtering sample.
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
Imports System
Imports System.Windows
Imports System.Data
Imports System.Xml
Imports System.Configuration
Imports Xceed.Wpf.DataGrid.Views
Imports System.Collections.ObjectModel

Namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
  Partial Public Class App
    Inherits System.Windows.Application
    Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_orders = dataSet.Tables("Orders")
      m_employees = dataSet.Tables("Employees")

      MyBase.OnStartup(e)
    End Sub

#Region "Employees Property"

    Private m_employees As DataTable

    Public ReadOnly Property Employees() As DataTable
      Get
        Return m_employees
      End Get
    End Property

#End Region

#Region "Orders Property"

    Private m_orders As DataTable

    Public ReadOnly Property Orders() As DataTable
      Get
        Return m_orders
      End Get
    End Property

#End Region
  End Class
End Namespace
