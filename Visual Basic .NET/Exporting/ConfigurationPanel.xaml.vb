'
' Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ConfigurationPanel.xaml.cs]
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
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Exporting
  Partial Public Class ConfigurationPanel
    Inherits UserControl
    Public Sub New()
      InitializeComponent()

      ' Typically, an application should use the ListSeparator defined in the regional
      ' settings of the system.
      Dim listSeparator As String = CultureInfo.CurrentCulture.TextInfo.ListSeparator

      If listSeparator = "," Then
        Me.separatorComboBox.SelectedIndex = 0
      ElseIf listSeparator = ";" Then
        Me.separatorComboBox.SelectedIndex = 1
      Else
        Me.separatorComboBox.SelectedIndex = 2
      End If

      Me.textQualifierComboBox.SelectedIndex = 0
      Me.dateTimeFormatComboBox.SelectedIndex = 0
      Me.numberFormatComboBox.SelectedIndex = 0
    End Sub

    Public Event ExportRequested As Action(Of ExportFormat)

    Private Sub export_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Me.ExportRequestedEvent IsNot Nothing Then
        RaiseEvent ExportRequested(CType(Me.exportFormatComboBox.SelectedValue, ExportFormat))
      End If
    End Sub
  End Class
End Namespace
