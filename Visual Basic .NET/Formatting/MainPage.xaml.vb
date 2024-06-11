'
' * Xceed DataGrid for WPF - SAMPLE CODE - Formatting Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' *  
' * This class implements the various dynamic configuration options offered
' * by the configuration panel declared in MainPage.xaml.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports Xceed.Wpf.DataGrid.Export
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Formatting
    Partial Public Class MainPage
        Inherits System.Windows.Controls.Page
        Public Sub New()
            Me.Cultures = New List(Of CultureInfo)() From { _
             New CultureInfo("en-US"), _
             New CultureInfo("fr-FR"), _
             New CultureInfo("zh-CN"), _
             New CultureInfo("en-GB") _
            }

            InitializeComponent()
        End Sub

        Public Property Cultures() As List(Of CultureInfo)
            Get
                Return m_Cultures
            End Get
            Private Set(ByVal value As List(Of CultureInfo))
                m_Cultures = Value
            End Set
        End Property
        Private m_Cultures As List(Of CultureInfo)

        Private Sub ExportButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
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

            Dim saveFileDialog As New Microsoft.Win32.SaveFileDialog()
            saveFileDialog.Filter = "XML Spreadsheet (*.xml)|*.xml|All files (*.*)|*.*"
            If saveFileDialog.ShowDialog().GetValueOrDefault() Then
                Try
                    Using stream As Stream = saveFileDialog.OpenFile()
                        excelExporter.Export(stream)
                    End Using
                Catch IOe As IOException
                    MessageBox.Show(IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
                Catch ex As Exception
                    MessageBox.Show("There seems to be a problem exporting the data. Please report this error to Xceed support." & vbLf & vbCr & ex.Message & vbLf & vbCr & ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
                End Try
            End If
        End Sub
    End Class
End Namespace