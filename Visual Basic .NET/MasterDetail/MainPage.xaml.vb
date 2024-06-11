'
' Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MainPage.xaml.vb]
'
' This class implements the various dynamic configuration options offered
' by the configuration panel declared in MainPage.xaml.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page

    Public Sub New()
      InitializeComponent()
      Me.SetDetailConfigurations()
    End Sub

    Private Sub ShowSubEmployeesDetailChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetDetailConfigurations()
    End Sub

    Private Sub ShowOrdersDetailChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetDetailConfigurations()
    End Sub

    Private Sub SetDetailConfigurations()
      If Me.grid Is Nothing Then
        Return
      End If

      Me.grid.DetailConfigurations.Clear()

      If Me.showSubEmployeesDetail.IsChecked.Value Then
        Me.grid.DetailConfigurations.Add(CType(Me.Resources.Item("subEmployeesDetailConfiguration"), DetailConfiguration))
      End If

      If Me.showOrdersDetail.IsChecked.Value Then
        Me.grid.DetailConfigurations.Add(CType(Me.Resources.Item("ordersDetailConfiguration"), DetailConfiguration))
      End If

      ' Collaps all detail and reexpand it.
      Dim dataGridContexts As List(Of DataGridContext) =
                          New List(Of DataGridContext)(Me.grid.GetChildContexts())

      Dim dataGridContext As DataGridContext

      For Each dataGridContext In dataGridContexts
        dataGridContext.ParentDataGridContext.CollapseDetails(dataGridContext.ParentItem)
        dataGridContext.ParentDataGridContext.ExpandDetails(dataGridContext.ParentItem)
      Next
    End Sub

    Private Sub grid_DeletingSelectedItems(ByVal sender As Object, ByVal e As CancelRoutedEventArgs)
      e.Cancel = (MessageBoxResult.No = MessageBox.Show("Are you sure you want to delete the selected items?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question))
    End Sub

    Private Sub grid_DeletingSelectedItemError(ByVal sender As Object, ByVal e As DeletingSelectedItemErrorRoutedEventArgs)
      ' When an exception occur while deleting a row, you have 
      ' the chance to rectify the situation and retry the operation.
      ' You can also skip the item in error or abort the operation.
      ' For the purpose of this sample, we will skip on error.
      e.Action = DeletingSelectedItemErrorAction.Skip
    End Sub

    ' Handled in the DataGridContext template to navigate to the corresponding
    ' item in the "clicked" context.
    Private Sub titleTextBlock_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
      If TypeOf sender Is TextBlock Then
        Dim mouseDownContext As DataGridContext = TryCast(CType(sender, TextBlock).DataContext, DataGridContext)
        Dim currentContext As DataGridContext = DataGridControl.GetDataGridContext(TryCast(sender, DependencyObject))
        Dim parentContext As DataGridContext = currentContext.ParentDataGridContext

        While Not parentContext Is Nothing
          If mouseDownContext.Equals(parentContext) Then
            mouseDownContext.CurrentItem = currentContext.ParentItem
            mouseDownContext.SelectedItems.Add(mouseDownContext.CurrentItem)
            Return
          End If

          currentContext = parentContext
          parentContext = IIf(Not parentContext Is Nothing, parentContext.ParentDataGridContext, parentContext)
        End While

        If parentContext Is Nothing Then

          If mouseDownContext.Items.Count > 0 Then
            mouseDownContext.CurrentItem = mouseDownContext.Items.GetItemAt(0)
            mouseDownContext.SelectedItems.Add(mouseDownContext.CurrentItem)
          End If
        End If
      End If
    End Sub
  End Class
End Namespace
