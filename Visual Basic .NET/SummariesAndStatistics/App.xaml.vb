'
' Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [App.xaml.vb]
'
' This is the main application of the Summaries & Statistics sample.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Data
Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
  Partial Public Class App
    Inherits System.Windows.Application
    Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Me.FillAvailableViews()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
      m_products = dataSet.Tables("Products")
      m_categories = dataSet.Tables("Categories")
      m_suppliers = dataSet.Tables("Suppliers")

      MyBase.OnStartup(e)
    End Sub

#Region "Products property"

    Private m_products As DataTable

    Public ReadOnly Property Products() As DataTable
      Get
        Return m_products
      End Get
    End Property

#End Region ' Products property

#Region "Categories property"

    Private m_categories As DataTable

    Public ReadOnly Property Categories() As DataTable
      Get
        Return m_categories
      End Get
    End Property

#End Region ' Categories property

#Region "Suppliers property"

    Private m_suppliers As DataTable

    Public ReadOnly Property Suppliers() As DataTable
      Get
        Return m_suppliers
      End Get
    End Property

#End Region ' Suppliers property

#Region "AvailableViews Property"

    Public ReadOnly Property AvailableViews() As ObservableCollection(Of ViewItem)
      Get
        Return m_views
      End Get
    End Property

    Private m_views As ObservableCollection(Of ViewItem) = New ObservableCollection(Of ViewItem)()

    Private Sub FillAvailableViews()
      m_views.Clear()

      For Each expType As Type In GetType(DataGridControl).Assembly.GetExportedTypes()
        If (GetType(UIViewBase).IsAssignableFrom(expType)) AndAlso ((Not expType.IsAbstract)) Then
          m_views.Add(New ViewItem(expType.Name, expType))
        End If
      Next expType
    End Sub

#End Region ' AvailableViews Property

    Public Event ViewChanged As EventHandler

    Public Sub OnViewChanged(ByVal e As EventArgs)
      If Not Me.ViewChangedEvent Is Nothing Then
        RaiseEvent ViewChanged(Me, e)
      End If
    End Sub
  End Class
End Namespace
