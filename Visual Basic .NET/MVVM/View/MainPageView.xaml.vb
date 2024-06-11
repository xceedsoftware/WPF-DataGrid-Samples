'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPageView.xaml.vb]
' *  
' * This class implements the required logic for the View.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports Xceed.Wpf.DataGrid.Export

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.View
  Partial Public Class MainPageView
    Inherits Page
    Public Sub New()
      'Initialize the ViewModel.
      Me.DataContext = (CType(Application.Current, App)).MainPageViewModelInstance
      InitializeComponent()

      'This binding is used to obtain the next ProductID value from the ViewModel, and set it for the end user when initializing the InsertionRow.
      BindingOperations.SetBinding(Me, MainPageView.NextProductIDProperty, New Binding("InitialProductIDValue") With {.Source = Me.DataContext, .Mode = BindingMode.OneWay})
    End Sub

#Region "NextProductID Property"

    Public Shared ReadOnly NextProductIDProperty As DependencyProperty = DependencyProperty.Register("NextProductID", GetType(Integer), GetType(MainPageView), New PropertyMetadata(0))

    Public Property NextProductID() As Integer
      Get
        Return CInt(Fix(Me.GetValue(MainPageView.NextProductIDProperty)))
      End Get
      Set(ByVal value As Integer)
        Me.SetValue(MainPageView.NextProductIDProperty, value)
      End Set
    End Property

#End Region

    Private Sub OnInitializingInsertionRow(ByVal sender As Object, ByVal e As InitializingInsertionRowEventArgs)
      'Set the default values when the end user clicks on the InsertionRow.
      Dim insertionRowCells = e.InsertionRow.Cells
      insertionRowCells("ProductID").Content = Me.NextProductID
      insertionRowCells("SupplierID").Content = 1
      insertionRowCells("CategoryID").Content = 1
      insertionRowCells("ReorderDate").Content = DateTime.Today
    End Sub

    Private Sub OnExportButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Even though it is the data that is exported, it is closely linked to the UI as it is presented to the end user -
      ' for instance, ForeignKey columns can be exported using the displayed value, not the key value; only visible columns are exported, and in the order they appear -
      ' which is the reason why the Export feature is available through the DataGrid, thus the following code is implemented on the View side.
      Dim saveFileDialog As New Microsoft.Win32.SaveFileDialog()
      saveFileDialog.Filter = "XML Spreadsheet (*.xml)|*.xml|All files (*.*)|*.*"

      If saveFileDialog.ShowDialog().GetValueOrDefault() Then
        Try
          Using stream As Stream = saveFileDialog.OpenFile()
            Dim excelExporter = New ExcelExporter(grid) With {.DetailDepth = 1, .ShowStatsInDetails = True, .StatFunctionDepth = 1}
            excelExporter.Export(stream)
          End Using
        Catch IOe As IOException
          MessageBox.Show(IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
        Catch ex As Exception
          MessageBox.Show("There seems to be a problem exporting the data. Please report this error to Xceed support." & Constants.vbLf + Constants.vbCr + ex.Message + Constants.vbLf + Constants.vbCr + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
      End If
    End Sub
  End Class
End Namespace
