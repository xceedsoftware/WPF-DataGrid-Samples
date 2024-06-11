'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
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
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media
Imports Xceed.Wpf.DataGrid.Views
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.CustomViews
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page

    Public Sub New()
      InitializeComponent()

      ' There is some important code run when we select a view mode.
      ' We have to "select" the default view after InitializeComponent is completed.
      Me.customTableViewRadioButton.IsChecked = True
    End Sub

    '
    ' Assign the currently selected "CurrentItemGlyph" to the DataGrid's View.
    '
    Private Sub SetCurrentItemGlyph(ByVal view As Xceed.Wpf.DataGrid.Views.UIViewBase)
      If Not view Is Nothing Then
        Dim item As GlyphItem = CType(Me.currentItemGlyphComboBox.SelectedItem, GlyphItem)

        If item.Glyph Is Nothing Then
          view.ClearValue(Xceed.Wpf.DataGrid.Views.UIViewBase.CurrentItemGlyphProperty)
        Else
          view.CurrentItemGlyph = item.Glyph
        End If
      End If
    End Sub

    '
    ' To enable (or disable) our CardView custom DataRow layout we simply add an 
    ' implicit style for DataRow (or remove it) in the Page's resources.
    ' Usually, this would be done by declaring directly the implicit Style in XAML, 
    ' but here, we want it dynamically on or off.
    '
    Private Sub SetCustomDataRowTemplate()
      Me.Resources.Remove(GetType(DataRow))

      If (Me.useCustomDataRowTemplate.IsChecked.GetValueOrDefault()) Then
        Me.Resources.Add(GetType(DataRow), Me.FindResource("customCardViewDataRow"))
      End If
    End Sub

    '
    ' Assign (or reset) the "Card Title Template" to the DataGrid's View.
    '
    Private Sub SetAlternativeCardTitle(ByVal cardView As CardView)
      If Not cardView Is Nothing Then
        If (Me.useAlternativeCardTitle.IsChecked.GetValueOrDefault()) Then
          cardView.CardTitleTemplate = Me.FindResource("alternativeCardTitleTemplate")
        Else
          cardView.ClearValue(cardView.CardTitleTemplateProperty)
        End If
      End If
    End Sub

    '
    ' The HideEmptyCells property will completely hide the Cell if its value is Null.
    '
    Private Sub SetHideEmptyCells(ByVal cardView As CardView)
      If Not cardView Is Nothing Then
        cardView.HideEmptyCells = Me.hideEmptyCellsCheckBox.IsChecked.GetValueOrDefault()
      End If
    End Sub

    '
    ' The column separator is a CardView property not to be confused with the 
    ' DataGridControl's Columns. A CardView Column can contain many items (DataRow, 
    ' Headers, Footers).
    '
    Private Sub SetColumnSeparator(ByVal cardView As CardView)
      If Not cardView Is Nothing Then
        Dim item As PenItem = CType(Me.columnSeparatorComboBox.SelectedItem, PenItem)

        If item.Pen Is Nothing Then
          cardView.ClearValue(cardView.SeparatorLinePenProperty)
        Else
          cardView.SeparatorLinePen = item.Pen
        End If
      End If
    End Sub

    '
    ' To enable (or disable) our TableView custom DataRow layout we simply add an 
    ' implicit style for DataRow (or remove it) in the Page's resources.
    ' Usually, this would be done by declaring directly the implicit Style in XAML, 
    ' but here, we want it dynamically on or off.
    '
    Private Sub SetCustomDataRowStyle()
      Me.Resources.Remove(GetType(DataRow))

      If Me.useCustomDataRowStyle.IsChecked.GetValueOrDefault() Then
        Me.Resources.Add(GetType(DataRow), Me.FindResource("customTableViewDataRowStyle"))
      End If
    End Sub

    '
    ' The RowSelectorPane is accessible via the TableView.
    '
    Private Sub SetShowRowSelectorPane(ByVal tableView As TableView)
      If Not tableView Is Nothing Then
        tableView.ShowRowSelectorPane = Me.showRowSelectorPaneCheckBox.IsChecked.GetValueOrDefault()
      End If
    End Sub

    '
    ' The Horizontal and Vertical GridLines are handled separately.
    '
    Private Sub SetGridLine(ByVal tableView As TableView)
      If Not tableView Is Nothing Then
        Dim gridLines As GridLinesItem = CType(Me.gridLineComboBox.SelectedItem, GridLinesItem)

        If gridLines.HorizontalBrush Is Nothing Then
          tableView.ClearValue(tableView.HorizontalGridLineBrushProperty)
        Else
          tableView.HorizontalGridLineBrush = gridLines.HorizontalBrush
        End If

        If gridLines.HorizontalThickness < 0 Then
          tableView.ClearValue(tableView.HorizontalGridLineThicknessProperty)
        Else
          tableView.HorizontalGridLineThickness = gridLines.HorizontalThickness
        End If

        If gridLines.VerticalBrush Is Nothing Then
          tableView.ClearValue(tableView.VerticalGridLineBrushProperty)
        Else
          tableView.VerticalGridLineBrush = gridLines.VerticalBrush
        End If
        If gridLines.VerticalThickness < 0 Then
          tableView.ClearValue(tableView.VerticalGridLineThicknessProperty)
        Else
          tableView.VerticalGridLineThickness = gridLines.VerticalThickness
        End If
      End If
    End Sub

    '
    ' The CellContentTemplateSelector allows you to specify a different Template for
    ' cells according to their value or other criterias.
    '
    Private Sub SetUseCustomContentTemplateSelector()
      If Not Me.grid Is Nothing Then
        If Me.useCustomContentTemplateSelector.IsChecked.GetValueOrDefault() Then
          Me.grid.Columns("BirthDate").CellContentTemplateSelector = New ZodiacCellContentTemplateSelector()
        Else
          Me.grid.Columns("BirthDate").ClearValue(Column.CellContentTemplateSelectorProperty)
        End If
      End If
    End Sub

    '****************************************
    ' Event Handlers
    '****************************************
    Private Sub CardViewSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Extract the custom CardView declaration from the resources.
      Dim cardView As CompactCardView = Me.FindResource("cardViewInstance")

      ' Show the configuration panel associated with our custom Card View
      Me.customCardViewParametersPanel.Visibility = Windows.Visibility.Visible
      Me.customTableViewParametersPanel.Visibility = Windows.Visibility.Collapsed

      Me.SetCustomDataRowTemplate()
      Me.SetCurrentItemGlyph(cardView)
      Me.SetHideEmptyCells(cardView)
      Me.SetColumnSeparator(cardView)
      Me.SetAlternativeCardTitle(cardView)

      Me.grid.View = cardView

      ' We don't want to show the zodiac sign watermark in the custom CardView layout.
      Me.grid.Columns("BirthDate").ClearValue(Column.CellContentTemplateSelectorProperty)
    End Sub

    Private Sub TableViewSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Extract the custom TableView declaration from the resources.
      Dim tableView As TableView = Me.FindResource("tableViewInstance")

      ' Show the configuration panel associated with our custom Table View
      Me.customCardViewParametersPanel.Visibility = Visibility.Collapsed
      Me.customTableViewParametersPanel.Visibility = Visibility.Visible

      Me.SetCustomDataRowStyle()
      Me.SetCurrentItemGlyph(tableView)
      Me.SetShowRowSelectorPane(tableView)
      Me.SetUseCustomContentTemplateSelector()
      Me.SetGridLine(tableView)

      Me.grid.View = tableView
    End Sub

    Private Sub CurrentItemGlyphSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetCurrentItemGlyph(Me.grid.View)
      End If
    End Sub

    '****************************************
    ' CardView Event Handlers
    '****************************************
    Private Sub UseCustomDataRowTemplateChange(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetCustomDataRowTemplate()
    End Sub

    Private Sub UseAlternativeCardTitleChange(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetAlternativeCardTitle(Me.grid.View)
      End If
    End Sub

    Private Sub HideEmptyCellsCheckedChange(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetHideEmptyCells(Me.grid.View)
      End If
    End Sub

    Private Sub ColumnSeparatorSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetColumnSeparator(Me.grid.View)
      End If
    End Sub

    '****************************************
    ' TableView Event Handlers
    '****************************************
    Private Sub UseCustomDataRowStyleChange(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetCustomDataRowStyle()
    End Sub

    Private Sub ShowRowSelectorPaneCheckedChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetShowRowSelectorPane(Me.grid.View)
      End If
    End Sub

    Private Sub UseCustomContentTemplateSelectorChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetUseCustomContentTemplateSelector()
    End Sub

    Private Sub GridLineSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.SetGridLine(Me.grid.View)
      End If
    End Sub
  End Class
End Namespace
