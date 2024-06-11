'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [App.xaml.vb]
' 
' This is the main application of the Persist User Settings sample.
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
Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings

  Partial Public Class App
    Inherits System.Windows.Application

#Region "PROTECTED METHODS"

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      Me.FillAvailableViews()

      m_suppliers = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet().Tables("Suppliers")

      MyBase.OnStartup(e)
    End Sub


#End Region

#Region "Suppliers property"

    Public ReadOnly Property Suppliers() As DataTable
      Get
        Return m_suppliers
      End Get
    End Property

    Private m_suppliers As DataTable

#End Region

#Region "ViewChanging Event"

    Public Event ViewChanging As EventHandler

    Public Sub OnViewChanging(ByVal e As EventArgs)
      RaiseEvent ViewChanging(Me, e)
    End Sub

#End Region

#Region "ViewChanged Event"

    Public Event ViewChanged As EventHandler

    Public Sub OnViewChanged(ByVal e As EventArgs)
      RaiseEvent ViewChanged(Me, e)
    End Sub

#End Region

#Region "AvailableViews Property"

    Public ReadOnly Property AvailableViews() As ObservableCollection(Of ViewItem)
      Get
        Return m_views
      End Get
    End Property

    Private m_views As New ObservableCollection(Of ViewItem)

    Private Sub FillAvailableViews()
      m_views.Clear()

      m_views.Add(New ViewItem("TableView", GetType(TableView)))
      m_views.Add(New ViewItem("CardView", GetType(CardView)))
      m_views.Add(New ViewItem("CompactCardView", GetType(CompactCardView)))
    End Sub

#End Region
  End Class
End Namespace
