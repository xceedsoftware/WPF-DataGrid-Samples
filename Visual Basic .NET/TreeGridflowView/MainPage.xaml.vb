'
' * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView View Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.cs]
' * 
' * This class implements the various dynamic configuration options offered 
' * by the configuration panel declared in MainPage.xaml.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
  Partial Public Class MainPage
    Inherits Page
    Public Sub New()
      Dim dataView As New DataView((CType(Application.Current, App)).Employees, "ReportsTo is Null", Nothing, DataViewRowState.CurrentRows)
      Me.DataContext = dataView

      InitializeComponent()

      AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf ConfigurationData_PropertyChanged

      Me.AdjustHeadersFooters()
    End Sub

    Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      Select Case e.PropertyName
        Case "ShowColumnManagerRow"
          Me.AdjustHeadersFooters()
      End Select
    End Sub

    Private Sub DataGrid_ItemsSourceChangeCompleted(ByVal sender As Object, ByVal e As EventArgs)
      Me.ExpandAllItems(DataGridControl.GetDataGridContext(sender))
    End Sub

    Private Sub AdjustHeadersFooters()
      ' Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear()
      grid.View.Headers.Clear()

      ' Add the column manager row in the FixedHeaders if desired
      If ConfigurationData.Singleton.ShowColumnManagerRow Then
        grid.View.FixedHeaders.Add(TryCast(Me.FindResource("columnManagerRowTemplate"), DataTemplate))
      End If
    End Sub

    Private Sub ExpandAllItems(ByVal dataGridContext As DataGridContext)
      If dataGridContext Is Nothing Then Exit Sub
      If Not dataGridContext.HasDetails Then Exit Sub

      For Each item As Object In dataGridContext.Items
        If dataGridContext.AreDetailsExpanded(item) Then Continue For

        dataGridContext.ExpandDetails(item)
      Next

      For Each childDataGridContext As DataGridContext In dataGridContext.GetChildContexts()
        Me.ExpandAllItems(childDataGridContext)
      Next
    End Sub
  End Class
End Namespace
