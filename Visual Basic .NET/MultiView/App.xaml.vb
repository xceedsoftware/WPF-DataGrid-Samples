'
' Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [App.xaml.vb]
' 
' This is the main application of the Multi-View sample.
' 
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports Xceed.Wpf.DataGrid.Samples
Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid

Namespace Xceed.Wpf.DataGrid.Samples.MultiView
  Partial Public Class App
    Inherits System.Windows.Application

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_products = dataSet.Tables("Products")

      MyBase.OnStartup(e)
    End Sub

#Region "Products Property"

    Private m_products As DataTable

    Public ReadOnly Property Products() As DataTable
      Get
        Return m_products
      End Get
    End Property

#End Region
  End Class
End Namespace