'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ColumnPropertyCell.vb]
'  
' This class implements a custom Cell used by the ColumnPropertyRow class.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class ColumnPropertyCell
    Inherits Cell

    Shared Sub New()
      Dim res As ColumnPropertyCellResources = New ColumnPropertyCellResources()
      s_textBoxTemplate = CType(res("textBoxEditorTemplate"), DataTemplate)

      ColumnPropertyCell.IsColumnPropertySetProperty = ColumnPropertyCell.IsColumnPropertySetPropertyKey.DependencyProperty
    End Sub

    Public Sub New()
      ' This cell type is not used to display and edit the content of a cell but the
      ' column's size. Force the ReadOnly property to False.
      Me.ReadOnly = False

      Me.ContextMenu = New ContextMenu()

      Dim menuItem As MenuItem = New MenuItem()

      menuItem.Header = "_Clear value"
      menuItem.CommandBindings.Add(New CommandBinding(ColumnPropertyCell.ClearValueCommand, AddressOf Me.ClearValueCommandExecuted, AddressOf Me.ClearValueCommandCanExecute))
      menuItem.Command = ColumnPropertyCell.ClearValueCommand
      Me.ContextMenu.Items.Add(menuItem)

      menuItem = New MenuItem()

      menuItem.Header = "Clear this _row's values"
      menuItem.CommandBindings.Add(New CommandBinding(ColumnPropertyCell.ClearRowValuesCommand, AddressOf Me.ClearRowValuesCommandExecuted, AddressOf Me.ClearRowValuesCommandCanExecute))
      menuItem.Command = ColumnPropertyCell.ClearRowValuesCommand
      Me.ContextMenu.Items.Add(menuItem)
    End Sub

#Region "ClearValueCommand Command"

    ' This command will clear the parent column's dependency property specified by the
    ' parent row (ColumnPropertyRow.ColumnProperty).

    Public Shared ClearValueCommand As RoutedCommand = New RoutedCommand()

    Private Sub ClearValueCommandExecuted(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
      Me.ClearColumnPropertyValue()
    End Sub

    Private Sub ClearValueCommandCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
      e.CanExecute = Me.IsColumnPropertySet
    End Sub

    Private Sub ClearColumnPropertyValue()
      Dim parentColumn As ColumnBase = Me.ParentColumn
      Dim parentRow As ColumnPropertyRow = TryCast(Me.ParentRow, ColumnPropertyRow)

      If (Not parentColumn Is Nothing) And (Not parentRow Is Nothing) Then
        parentColumn.ClearValue(parentRow.ColumnProperty)
        ' In the case where the value before the ClearValue is equal to the Column
        ' property's default value, the PropertyChanged won't be triggered.
        ' Force the IsColumnPropertySet update.
        UpdateIsColumnPropertySet(parentRow, parentColumn)
      End If
    End Sub
#End Region

#Region "ClearRowValuesCommand Command"

    ' This command will clear all the column dependency property specified by the
    ' parent row (ColumnPropertyRow.ColumnProperty).

    Public Shared ClearRowValuesCommand As RoutedCommand = New RoutedCommand()

    Private Sub ClearRowValuesCommandExecuted(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
      Dim parentRow As ColumnPropertyRow = TryCast(Me.ParentRow, ColumnPropertyRow)

      If Not parentRow Is Nothing Then
        Dim cell As ColumnPropertyCell

        For Each cell In parentRow.Cells
          cell.ClearColumnPropertyValue()
        Next
      End If
    End Sub

    Private Sub ClearRowValuesCommandCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
      e.CanExecute = True
    End Sub

#End Region

#Region "IsColumnPropertySet Read-Only Property"

    ' This read-only dependency property returns True if the parent column has a local 
    ' value associated with the parent row's ColumnProperty property. It allows the cell
    ' to display its content in highlight and activate the "Clear value" command.

    Protected Shared ReadOnly IsColumnPropertySetPropertyKey As DependencyPropertyKey = _
        DependencyProperty.RegisterReadOnly("IsColumnPropertySet", GetType(Boolean), GetType(ColumnPropertyCell), New PropertyMetadata(False))

    Public Shared ReadOnly IsColumnPropertySetProperty As DependencyProperty

    Public ReadOnly Property IsColumnPropertySet() As Boolean
      Get
        Return CType(Me.GetValue(ColumnPropertyCell.IsColumnPropertySetProperty), Boolean)
      End Get
    End Property

    Private Sub SetIsColumnPropertySet(ByVal value As Boolean)
      Me.SetValue(ColumnPropertyCell.IsColumnPropertySetPropertyKey, value)
    End Sub

#End Region

    ' Initialize this cell content binding according to the parent row's 
    ' (ColumnPropertyRow) ColumnProperty value.
    Protected Overrides Sub InitializeCore(ByVal dataGridContext As DataGridContext, ByVal parentRow As Row, ByVal parentColumn As ColumnBase)
      Dim path As String = Nothing
      Dim columnPropertyRow As ColumnPropertyRow = TryCast(parentRow, ColumnPropertyRow)

      MyBase.InitializeCore(dataGridContext, parentRow, parentColumn)

      If (Not columnPropertyRow Is Nothing) And (Not columnPropertyRow.ColumnProperty Is Nothing) Then
        path = columnPropertyRow.ColumnProperty.Name
      End If

      If (Not parentColumn Is Nothing) And (Not path Is Nothing) Then
        Dim binding As Binding = New Binding(path)
        binding.Mode = BindingMode.TwoWay
        binding.Source = parentColumn
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

        If columnPropertyRow.ColumnProperty.PropertyType Is GetType(ColumnWidth) Then
          binding.ValidationRules.Add(s_columnWidthValidationRule)
        Else
          binding.Converter = New DoubleInfinityConverter()
        End If

        BindingOperations.SetBinding(Me, Cell.ContentProperty, binding)
      End If
    End Sub

    ' This cell provides its own CellEditor. It's always a NumericTextBox, which means 
    ' that, most of the time, it will be different than the default parent column's
    ' CellEditor.
    Protected Overrides Function GetCellEditor() As CellEditor
      Dim cellEditor As CellEditor = New CellEditor()

      cellEditor.EditTemplate = s_textBoxTemplate
      cellEditor.ActivationGestures.Add(New TextInputActivationGesture())

      Return cellEditor
    End Function

    Protected Overrides Sub OnContentChanged(ByVal oldContent As Object, ByVal newContent As Object)
      MyBase.OnContentChanged(oldContent, newContent)

      ' The parent column's property used in the cell's content binding has changed. 
      ' Refresh the IsColumnPropertySet property.
      Me.UpdateIsColumnPropertySet(TryCast(Me.ParentRow, ColumnPropertyRow), Me.ParentColumn)
    End Sub

    Private Sub UpdateIsColumnPropertySet(ByVal parentRow As ColumnPropertyRow, ByVal parentColumn As ColumnBase)
      If (parentColumn Is Nothing) Or (parentRow Is Nothing) Then
        Return
      End If

      Me.SetIsColumnPropertySet(Not parentColumn.ReadLocalValue(parentRow.ColumnProperty) Is DependencyProperty.UnsetValue)
    End Sub

    Private Shared ReadOnly s_textBoxTemplate As DataTemplate
    Private Shared ReadOnly s_columnWidthValidationRule As ColumnWidthValidationRule = New ColumnWidthValidationRule()
  End Class
End Namespace
