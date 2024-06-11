'
' Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
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
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls

Imports Xceed.Wpf.DataGrid.Export

Namespace Xceed.Wpf.DataGrid.Samples.Exporting
  Partial Public Class MainPage
    Inherits Page
    Public Sub New()
      InitializeComponent()

      AddHandler configurationPanel.ExportRequested, AddressOf configurationPanel_ExportRequested
    End Sub

    Private Sub configurationPanel_ExportRequested(ByVal exportFormat As ExportFormat)
      If exportFormat = Exporting.ExportFormat.Excel Then
        Me.ExportToExcel()
      Else
        Me.ExportToCsv()
      End If
    End Sub

    Private Sub ExportToCsv()
      If System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted Then
        MessageBox.Show("Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable")
        Return
      End If

      ' The simplest way to export in CSV format is to call the 
      ' DataGridControl.ExportToCsv() method. However, if you want to specify export
      ' settings, you have to take the longer, more descriptive and flexible route: 
      ' the CsvExporter class.
      Dim csvExporter As New CsvExporter(Me.grid)

      ' By setting the Culture to the CurrentCulture (system culture by default), the
      ' date and number formats set in the regional settings will be used.
      csvExporter.FormatSettings.Culture = CultureInfo.CurrentCulture

      csvExporter.FormatSettings.DateTimeFormat = ConfigurationData.Singleton.DateTimeFormat
      csvExporter.FormatSettings.NumericFormat = ConfigurationData.Singleton.NumberFormat
      csvExporter.FormatSettings.Separator = ConfigurationData.Singleton.Separator
      csvExporter.FormatSettings.TextQualifier = ConfigurationData.Singleton.TextQualifier

      csvExporter.IncludeColumnHeaders = ConfigurationData.Singleton.IncludeColumnHeaders
      csvExporter.RepeatParentData = ConfigurationData.Singleton.RepeatParentData
      csvExporter.DetailDepth = ConfigurationData.Singleton.DetailDepth
      csvExporter.UseFieldNamesInHeader = ConfigurationData.Singleton.UseFieldNamesInHeader

      Dim saveFileDialog As New Microsoft.Win32.SaveFileDialog()

      saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*"

      If saveFileDialog.ShowDialog().GetValueOrDefault() Then
        Try
          Using stream As Stream = saveFileDialog.OpenFile()
            csvExporter.Export(stream)
          End Using
        Catch IOe As IOException
          MessageBox.Show(IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
        Catch e As Exception
          MessageBox.Show("There seems to be a problem exporting the data. Please report this error to Xceed support." &
                          Constants.vbLf + Constants.vbCr + e.Message +
                          Constants.vbLf + Constants.vbCr + e.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
      End If
    End Sub

    Private Sub ExportToExcel()
      If System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted Then
        MessageBox.Show("Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable")
        Return
      End If

      ' The simplest way to export in Excel format is to call the 
      ' DataGridControl.ExportToExcel() method. However, if you want to specify export
      ' settings, you have to take the longer, more descriptive and flexible route: 
      ' the ExcelExporter class.

      ' excelExporter.FixedColumnCount will automatically be set to the specified
      ' grid's FixedColumnCount value.
      Dim excelExporter As New ExcelExporter(Me.grid)

      excelExporter.ExportStatFunctionsAsFormulas = ConfigurationData.Singleton.ExportStatFunctionsAsFormulas
      excelExporter.IncludeColumnHeaders = ConfigurationData.Singleton.IncludeColumnHeaders
      excelExporter.IsHeaderFixed = ConfigurationData.Singleton.IsHeaderFixed
      excelExporter.RepeatParentData = ConfigurationData.Singleton.RepeatParentData
      excelExporter.DetailDepth = ConfigurationData.Singleton.DetailDepth
      excelExporter.StatFunctionDepth = ConfigurationData.Singleton.StatFunctionDepth
      excelExporter.UseFieldNamesInHeader = ConfigurationData.Singleton.UseFieldNamesInHeader

      Dim saveFileDialog As New Microsoft.Win32.SaveFileDialog()

      saveFileDialog.Filter = "XML Spreadsheet (*.xml)|*.xml|All files (*.*)|*.*"

      If saveFileDialog.ShowDialog().GetValueOrDefault() Then
        Try
          Using stream As Stream = saveFileDialog.OpenFile()
            excelExporter.Export(stream)
          End Using
        Catch IOe As IOException
          MessageBox.Show(IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
        Catch e As Exception
          MessageBox.Show("There seems to be a problem exporting the data. Please report this error to Xceed support." &
                          Constants.vbLf + Constants.vbCr + e.Message +
                          Constants.vbLf + Constants.vbCr + e.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
      End If
    End Sub
  End Class
End Namespace
