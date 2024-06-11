'
' Xceed DataGrid for WPF - SAMPLE CODE - Printing Sample Application
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
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Data
Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Windows.Controls
Imports Xceed.Wpf.DataGrid.Print
Imports Xceed.Wpf.DataGrid.Views
Imports System.IO.Packaging

Namespace Xceed.Wpf.DataGrid.Samples.Printing
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page
    Public Sub New()
      m_doingInitializeComponent = True
      InitializeComponent()
      m_doingInitializeComponent = False
    End Sub

    Private Sub ButtonPrint_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.textProgressInformation.Text = ""

      If (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted) Then
        Me.textProgressInformation.Text += "The UI cannot be updated in XBAP even though the ProgressionCallBack is called.  Please wait..." + vbCrLf
      End If

      Me.textProgressInformation.Text += "Document preparation started..."

      Me.progressScrollViewer.ScrollToBottom()

      grid.Print("Xceed Printing Sample", True, New EventHandler(Of ProgressEventArgs)(AddressOf Me.ProgressionCallBack), True)
    End Sub

    Private Sub ButtonExport_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted) Then
        MessageBox.Show("Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable")
        Return
      End If

      Me.textProgressInformation.Text = "Document exportation started..."
      Me.progressScrollViewer.ScrollToBottom()

      Dim saveFileDialog As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog()

      saveFileDialog.Filter = "XPS files (*.xps)|*.xps|All files (*.*)|*.*"

      If saveFileDialog.ShowDialog().GetValueOrDefault() Then
        Dim size As Size = New Size(8.5R * 96.0R, 11.0R * 96.0R)

        Try
          grid.ExportToXps(saveFileDialog.FileName, size, New Rect(size), New PageRange(1, 0), CompressionOption.Normal, New EventHandler(Of ProgressEventArgs)(AddressOf Me.ProgressionCallBack), True)
        Catch e1 As System.IO.IOException
        End Try
      End If
    End Sub

    Private Sub ProgressionCallBack(ByVal sender As Object, ByVal e As ProgressEventArgs)
      If e.ProgressInfo.PercentCompleted < 100 Then
        Me.textProgressInformation.Text += vbCrLf + "Preparing page " + e.ProgressInfo.CurrentPageNumber.ToString()
      Else
        Me.textProgressInformation.Text += vbCrLf + "...Completed."
      End If

      Me.progressScrollViewer.ScrollToBottom()
    End Sub


    Private Sub SelectedPrintViewChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If m_doingInitializeComponent Then
        Return
      End If

      If chkConfiguredPrintView.IsChecked.Value Then
        grid.PrintView = TryCast(Me.Resources("configuredPrintView"), PrintViewBase)
      ElseIf chkCustomPrintView.IsChecked.Value Then
        grid.PrintView = TryCast(Me.Resources("customPrintView"), PrintViewBase)
      Else
        grid.PrintView = Nothing
      End If
    End Sub

    Public m_doingInitializeComponent As Boolean ' = false;

    Private Sub ButtonPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

      Me.textProgressInformation.Text = String.Empty

      Dim size As Size = New Size(8.5D * 96D, 11D * 96D)

      Me.progressScrollViewer.ScrollToBottom()

      If System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted Then

        MessageBox.Show("Due to restricted user-access permissions caused by running the Live Explorer as an XBAP browser application, a feature-limited popup must be used to display the print-preview dialog. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to access the full feature set.", "Feature limited")

        Me.textProgressInformation.Text = "The UI cannot be updated in XBAP even though the ProgressionCallBack is called.  Please wait..." + vbCrLf

        Me.textProgressInformation.Text += "Generating Preview..."

        grid.ShowPrintPreviewPopup("Xceed Printing Sample", True, New EventHandler(Of ProgressEventArgs)(AddressOf Me.ProgressionCallBack), True, _
New Size(((grid.ActualHeight * 8.5D) / 11), grid.ActualHeight), size)

      Else

        Dim jobCompleted As Boolean = False

        Me.textProgressInformation.Text = "Generating Preview..."

        jobCompleted = grid.ShowPrintPreviewWindow("Xceed Printing Sample", True, New EventHandler(Of ProgressEventArgs)(AddressOf Me.ProgressionCallBack), True, _
New Size(600, 800), size)

        If jobCompleted Then
          Me.textProgressInformation.Text += "\n...Completed."
        Else
          Me.textProgressInformation.Text += "\n...Canceled."
        End If

      End If

      Me.progressScrollViewer.ScrollToBottom()
    End Sub
  End Class
End Namespace
