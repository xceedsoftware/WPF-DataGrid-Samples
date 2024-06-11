'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
'  Copyright (c) 2007-2024 Xceed Software Inc.
'  
'  [MainWindow.xaml.vb]
'  
'  This class implements the various dynamic configuration options offered 
'  by the configuration panel of the MainWindow.xaml.
'  
'  This file is part of the Xceed DataGrid for WPF product. The use 
'  and distribution of this Sample Code is subject to the terms 
'  and conditions referring to "Sample Code" that are specified in 
'  the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings

  Partial Public Class MainWindow
    Inherits System.Windows.Window

#Region "CONSTRUCTORS"

    Public Sub New()
      Me.InitializeComponent()
    End Sub

#End Region

#Region "EVENT HANDLERS"

    Private Sub viewComboBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)

      Dim viewItem As ViewItem = CType(Me.viewComboBox.SelectedItem, ViewItem)
      Dim grid As DataGridControl = Me.FindDataGridControl(Me.rootFrame)

      If Not grid Is Nothing Then
        Dim currentViewType As Type = IIf(grid.View Is Nothing, Nothing, grid.View.GetType())

        If Not viewItem.Type Is currentViewType Then
          CType(App.Current, App).OnViewChanging(EventArgs.Empty)
          grid.View = CType(Activator.CreateInstance(viewItem.Type), Xceed.Wpf.DataGrid.Views.UIViewBase)
          CType(App.Current, App).OnViewChanged(EventArgs.Empty)
        End If
      End If
    End Sub

#End Region

#Region "PRIVATE METHODS"

    ' Helper fonction to find a child DataGridControl inside a parent element
    Private Function FindDataGridControl(ByVal root As DependencyObject) As DataGridControl
      If root Is Nothing Then
        Return Nothing
      End If

      Dim childrenCount As Integer = VisualTreeHelper.GetChildrenCount(root)
      Dim child As DependencyObject
      Dim foundGrid As DataGridControl

      Dim i As Integer
      For i = 0 To childrenCount - 1
        child = VisualTreeHelper.GetChild(root, i)

        If TypeOf child Is DataGridControl Then
          Return CType(child, DataGridControl)
        End If

        foundGrid = Me.FindDataGridControl(child)

        If Not foundGrid Is Nothing Then
          Return foundGrid
        End If
      Next i

      Return Nothing
    End Function

#End Region
  End Class
End Namespace
