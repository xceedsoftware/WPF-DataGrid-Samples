'
' Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ConfigurationPanel.xaml.cs]
'  
' This class implements the various dynamic configuration options offered
' in this sample.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Collections
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.Views3D
  Partial Public Class ConfigurationPanel
    Inherits UserControl
    Public Sub New()
      InitializeComponent()

      AddHandler Loaded, AddressOf ConfigurationPanel_Loaded
    End Sub

#Region "DataGridControl Property"

    Public Shared ReadOnly DataGridControlProperty As DependencyProperty = DependencyProperty.Register("DataGridControl", GetType(DataGridControl), GetType(ConfigurationPanel), New FrameworkPropertyMetadata(Nothing))

    Public Property DataGridControl() As DataGridControl
      Get
        Return CType(Me.GetValue(ConfigurationPanel.DataGridControlProperty), DataGridControl)
      End Get
      Set(ByVal value As DataGridControl)
        Me.SetValue(ConfigurationPanel.DataGridControlProperty, value)
      End Set
    End Property

#End Region

    Private Sub ConfigurationPanel_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Initially choose the default view settings (with its associated background
      ' via the Presets_SelectionChanged event handler).
      ' This is done in the Loaded event to allow a consumer of ConfigurationPanel
      ' the time to subscribe to its events (ApplyNewDataGridBackground).
      Me.presetComboBox.SelectedIndex = 0
    End Sub

    Private Sub Presets_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Dim item As PresetItem = TryCast(presetComboBox.SelectedItem, PresetItem)

      If item IsNot Nothing Then
        Dim viewStyle As Style = Nothing

        If item.ResourceDictionary IsNot Nothing Then
          viewStyle = CType(item.ResourceDictionary(GetType(Xceed.Wpf.DataGrid.Views.CardflowView3D)), Style)
        End If

        Me.presetComboBox.SelectedItem = Nothing

        If item.PreferredDataGridBackgroundBrush Is Nothing Then
          Me.GridBackgroundBrush.SelectedIndex = 0
        Else
          Me.GridBackgroundBrush.SelectedValue = item.PreferredDataGridBackgroundBrush
        End If

        ViewImportExportManager.ImportViewFromStyle(Me.DataGridControl.View, viewStyle)
      End If
    End Sub

    Private Sub Export_Clicked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ViewImportExportManager.ExportView(Me.DataGridControl.View)
    End Sub

    Private Sub Import_Clicked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ViewImportExportManager.ImportView(Me.DataGridControl.View)
    End Sub

    Private Sub CardBackgroundBrush_Changed(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Me.DataGridControl IsNot Nothing Then
        Me.DataGridControl.Resources.Clear()

        If e.AddedItems.Count > 0 Then
          Me.DataGridControl.Resources.Add(GetType(DataRow), TryCast((CType(e.AddedItems(0), DictionaryEntry)).Value, Style))
        End If
      End If
    End Sub

    Private Sub GridBackgroundBrush_Changed(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Me.DataGridControl IsNot Nothing Then
        If e.AddedItems.Count > 0 Then
          Me.DataGridControl.Background = TryCast((CType(e.AddedItems(0), DictionaryEntry)).Value, Brush)
        Else
          Me.DataGridControl.ClearValue(DataGridControl.BackgroundProperty)
        End If
      End If
    End Sub
  End Class
End Namespace
