'
' * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
  Partial Public Class MainPage
    Inherits Page
    Public Sub New()
      InitializeComponent()

      Me.grid.CurrentItem = Nothing
      Me.grid.CurrentColumn = Nothing
      Me.grid.SelectedItems.Clear()

      m_spannedCellContainerStyle = New Style(GetType(SpannedCell))
      m_spannedCellContainerStyle.Setters.Add(New Setter(SpannedCell.CellContainerStyleProperty, Me.FindResource("spannedCellContainerStyle")))

      AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf OnConfigurationDataPropertyChanged
    End Sub

    Private Sub OnConfigurationDataPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      If e.PropertyName = "AddBorders" Then
        Me.SetSpannedCellContainerStyle()
      End If
    End Sub

    Private Sub SetSpannedCellContainerStyle()
      If ConfigurationData.Singleton.AddBorders Then
        Me.Resources.Add(GetType(SpannedCell), m_spannedCellContainerStyle)
      Else
        Me.Resources.Remove(GetType(SpannedCell))
      End If
    End Sub

    Private m_spannedCellContainerStyle As Style

  End Class
End Namespace