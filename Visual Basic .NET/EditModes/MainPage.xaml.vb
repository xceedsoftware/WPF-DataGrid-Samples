'
' Xceed DataGrid for WPF - SAMPLE CODE - Edit Modes Sample Application
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

Imports System
Imports System.Collections.Generic
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.EditModes
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page

    Public Sub New()
      InitializeComponent()
      Me.UpdateGridCellEditorDisplayConditions()
      Me.UpdateGridEditTriggers()
    End Sub

    '
    ' Set the Xceed DataGridControl's CellEditorDisplayConditions property value
    ' according to the currently checked options in the configuration panel.
    '
    Private Sub UpdateGridCellEditorDisplayConditions()
      Dim conditions As CellEditorDisplayConditions = CellEditorDisplayConditions.None

      Dim conditionsItem As CellEditorDisplayConditionsItem
      For Each conditionsItem In Me.cellEditorDisplayConditionsItemsControl.Items
        If conditionsItem.IsChecked Then
          conditions = conditions Or conditionsItem.CellEditorDisplayConditions
        End If
      Next conditionsItem

      Me.grid.CellEditorDisplayConditions = conditions
    End Sub

    '
    ' Set the Xceed DataGridControl's EditTriggers property value
    ' according to the currently checked options in the configuration panel.
    '
    Private Sub UpdateGridEditTriggers()
      Dim triggers As EditTriggers = EditTriggers.None

      Dim triggersItem As EditTriggersItem
      For Each triggersItem In Me.editTriggersItemsControl.Items
        If triggersItem.IsChecked Then
          triggers = triggers Or triggersItem.EditTriggers
        End If
      Next triggersItem

      Me.grid.EditTriggers = triggers
    End Sub

    ' Event Handlers

    Private Sub CellEditorDisplayConditionsChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.UpdateGridCellEditorDisplayConditions()
    End Sub

    Private Sub EditTriggersChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.UpdateGridEditTriggers()
    End Sub

  End Class
End Namespace
