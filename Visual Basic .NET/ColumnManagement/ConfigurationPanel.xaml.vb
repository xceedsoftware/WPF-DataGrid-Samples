'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ConfigurationPanel.xaml.vb]
'  
' This class implements the various dynamic configuration options offered
' in this sample. The underlying business object is ConfigurationData.Singleton.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Partial Public Class ConfigurationPanel
    Inherits UserControl
    Public Sub New()
      InitializeComponent()

      ' If the actual LicenseKey prevents the usage of the ColumnChooser, disable the 
      ' appropriate CheckBox.
      Try
        Dim tableView As Xceed.Wpf.DataGrid.Views.TableView = New Xceed.Wpf.DataGrid.Views.TableView()
        tableView.AllowColumnChooser = True
      Catch e As System.ComponentModel.LicenseException
        Me.chkAllowColumnChooser.IsEnabled = False
      End Try
    End Sub

    Public Sub CancelColumnWidthGridEdit()
      Me.columnWidthGrid.CancelEdit()
    End Sub

    Private Sub ColumnStretchMode_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If Me.IsInitialized Then
        If ConfigurationData.Singleton.ColumnStretchMode = Xceed.Wpf.DataGrid.Views.ColumnStretchMode.All Then
          ' Disable the column width grid because the Column Width property have
          ' no effect when all columns have "auto-calculated" widths.
          Me.disablingBorder.HorizontalAlignment = HorizontalAlignment.Stretch
          Me.columnWidthGrid.IsEnabled = False
        Else
          Me.disablingBorder.HorizontalAlignment = HorizontalAlignment.Left
          Me.columnWidthGrid.IsEnabled = True
        End If
      End If
    End Sub
  End Class
End Namespace
