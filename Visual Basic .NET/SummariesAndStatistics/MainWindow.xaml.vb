'
' Xceed DataGrid for WPF - SAMPLE CODE -  Summaries & Statistics Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MainWindow.xaml.vb]
'
' This class implements the various dynamic configuration options offered
' by the configuration panel of the MainWindow.xaml.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports Xceed.Wpf.DataGrid.Views
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
  Partial Public Class MainWindow
    Inherits System.Windows.Window
    Public Sub New()
      InitializeComponent()

      ' When the Window has finished loading, select the appropriate view (the grid's
      ' current view) in the ComboBox.
      AddHandler Loaded, AddressOf MainWindow_Loaded
    End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim grid As DataGridControl = Me.FindDataGridControl(Me.rootFrame)

      If Not grid Is Nothing Then
        Dim view As UIViewBase = grid.View

        For Each item As ViewItem In Me.viewComboBox.Items
          If item.Type Is view.GetType() Then
            Me.viewComboBox.SelectedItem = item
            Exit For
          End If
        Next item
      End If
    End Sub

    ' Set a new View on the grid based on the one selected in the combobox by the user
    Private Sub viewComboBoxChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Dim viewItem As ViewItem = CType(Me.viewComboBox.SelectedItem, ViewItem)
      Dim grid As DataGridControl = Me.FindDataGridControl(Me.rootFrame)

      If Not grid Is Nothing Then
        Dim currentViewType As Type
        If grid.View Is Nothing Then
          currentViewType = (Nothing)
        Else
          currentViewType = (grid.View.GetType())
        End If

        If Not viewItem.Type Is currentViewType Then
          grid.View = CType(Activator.CreateInstance(viewItem.Type), UIViewBase)
          ' Assign a theme that blends well with our custom styles and templates.
          grid.View.Theme = CType(Me.FindResource("defaultTheme"), Theme)
          CType(App.Current, App).OnViewChanged(EventArgs.Empty)
        End If
      End If
    End Sub

    ' Helper fonction to find a child DataGridControl inside a parent element
    Private Function FindDataGridControl(ByVal root As DependencyObject) As DataGridControl
      Dim childrenCount As Integer = VisualTreeHelper.GetChildrenCount(root)
      Dim child As DependencyObject
      Dim foundGrid As DataGridControl

      Dim i As Integer = 0
      Do While i < childrenCount
        child = VisualTreeHelper.GetChild(root, i)

        If TypeOf child Is DataGridControl Then
          Return CType(child, DataGridControl)
        End If

        foundGrid = Me.FindDataGridControl(child)

        If Not foundGrid Is Nothing Then
          Return foundGrid
        End If
        i += 1
      Loop

      Return Nothing
    End Function
  End Class
End Namespace
