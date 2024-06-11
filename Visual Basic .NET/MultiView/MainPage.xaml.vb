'
' Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
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
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.MultiView
  Partial Public Class MainPage
    Inherits Page

    Private Sub TableflowViewRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles tableflowViewRadio.Checked
      If (grid Is Nothing) Then
        Return
      End If

      Dim tableflowView As New Xceed.Wpf.DataGrid.Views.TableflowView()
      tableflowView.ContainerHeight = 40
      
      grid.View = tableflowView

      'When the sample is hosted in the Live Explorer, a default might be exposed.
      Dim defaultTheme As Theme = TryCast(grid.TryFindResource("defaultTheme"), Theme)

      If (Not defaultTheme Is Nothing) Then
        grid.View.Theme = defaultTheme
      End If
    End Sub

    Private Sub TableViewRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles tableViewRadio.Checked
      If (grid Is Nothing) Then
        Return
      End If

      grid.View = New Xceed.Wpf.DataGrid.Views.TableView()

      'When the sample is hosted in the Live Explorer, a default might be exposed.
      Dim defaultTheme As Theme = TryCast(grid.TryFindResource("defaultTheme"), Theme)

      If (Not defaultTheme Is Nothing) Then
        grid.View.Theme = defaultTheme
      End If
    End Sub

    Private Sub CardViewRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles cardViewRadio.Checked
      If (grid Is Nothing) Then
        Return
      End If

      grid.View = New Xceed.Wpf.DataGrid.Views.CardView()

      'When the sample is hosted in the Live Explorer, a default might be exposed.
      Dim defaultTheme As Theme = TryCast(grid.TryFindResource("defaultTheme"), Theme)

      If (Not defaultTheme Is Nothing) Then
        grid.View.Theme = defaultTheme
      End If
    End Sub

    Private Sub CompactCardViewRadio_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles compactCardViewRadio.Checked
      If (grid Is Nothing) Then
        Return
      End If

      grid.View = New Xceed.Wpf.DataGrid.Views.CompactCardView()

      'When the sample is hosted in the Live Explorer, a default might be exposed.
      Dim defaultTheme As Theme = TryCast(grid.TryFindResource("defaultTheme"), Theme)

      If (Not defaultTheme Is Nothing) Then
        grid.View.Theme = defaultTheme
      End If
    End Sub

    Private Sub Cardflow3DView_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles cardflow3DView.Checked
      If (grid Is Nothing) Then
        Return
      End If

      grid.View = New Xceed.Wpf.DataGrid.Views.CardflowView3D()
    End Sub
  End Class
End Namespace
