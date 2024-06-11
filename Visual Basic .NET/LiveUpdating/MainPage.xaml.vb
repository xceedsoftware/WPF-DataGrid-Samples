'
' * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
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
Imports System.Data
Imports System.Threading

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
  ''' <summary>
  ''' Interaction logic for MainPage.xaml
  ''' </summary>
  Partial Public Class MainPage
    Inherits Page
    Private Shared ReadOnly InitialSymbols As String() = New String() {"XCEED", "YHOO", "MSFT", "AAPL", "GOOG", "IBM", _
     "CSCO", "DELL", "INTC", "NTGR"}

    Public Sub New()
      m_quoteWorkers = New List(Of QuoteWorker)()
      m_quote = Me.CreateDataTable()
      AddHandler Me.Unloaded, AddressOf MainPage_Unloaded


      InitializeComponent()

      For Each symbol As String In MainPage.InitialSymbols
        Me.AddNewQuote(symbol)
      Next

      sliderRecordsCount.Value = MainPage.InitialSymbols.Length
    End Sub

#Region "QUOTE PROPERTY"

    Public ReadOnly Property Quote() As DataView
      Get
        Return m_quote.DefaultView
      End Get
    End Property

#End Region

#Region "EVENT HANDLERS"

    Private Sub MainPage_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Stop fetching data.
      For Each quoteWorker As QuoteWorker In m_quoteWorkers
        quoteWorker.[Stop]()
      Next

      m_quoteWorkers.Clear()
    End Sub

    Private Sub quoteWorker_QuoteInfoUpdated(ByVal sender As Object, ByVal e As EventArgs)
      ' A Quote Worker is done gathering data.  Update the DataTable.
      ' Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      SyncLock m_quote.DefaultView
        Dim quoteWorker As QuoteWorker = TryCast(sender, QuoteWorker)

        If Not quoteWorker.Started Then
          Return
        End If

        If quoteWorker.DataRowIndex >= m_quote.Rows.Count Then
          Throw New InvalidOperationException("An error occured trying to update data of a non-existing quote.")
        End If

        ' Find the datarow to update using the quoteWorker's DataRowIndex property.
        ' This is an optimization so we don't have to find the DataRow corresponding to the quote worker each time.
        Dim dataRow As System.Data.DataRow = m_quote.Rows(quoteWorker.DataRowIndex)
        Dim quoteInfo As QuoteInfo = quoteWorker.QuoteInfo

        If Not dataRow("Symbol").Equals(quoteInfo.Symbol) Then
          Throw New InvalidOperationException("An error occured trying to update data of a non-existing quote.")
        End If

        dataRow("PreviousClose") = quoteInfo.PreviousClose
        dataRow("Open") = quoteInfo.OpeningValue

        Dim oldChange As Double = System.Convert.ToDouble(dataRow("Change"))
        Dim newChange As Double = quoteInfo.LastTrade - quoteInfo.PreviousClose
        dataRow("ChangeDiff") = newChange - oldChange
        dataRow("Change") = newChange

        Dim oldLastTrade As Double = System.Convert.ToDouble(dataRow("LastTrade"))
        Dim newLastTrade As Double = quoteInfo.LastTrade
        dataRow("LastTradeDiff") = newLastTrade - oldLastTrade
        dataRow("LastTrade") = newLastTrade
        dataRow("TradeTime") = quoteInfo.TradeTime
      End SyncLock
    End Sub

    Private Sub sliderSimulatedMinLatency_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
      If (Me.IsInitialized) AndAlso (sliderSimulatedMaxLatency.Value < e.NewValue) Then
        sliderSimulatedMaxLatency.Value = e.NewValue
      End If
    End Sub

    Private Sub sliderSimulatedMaxLatency_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
      If (Me.IsInitialized) AndAlso (sliderSimulatedMinLatency.Value > e.NewValue) Then
        sliderSimulatedMinLatency.Value = e.NewValue
      End If
    End Sub

    Private Sub sliderRecordsCount_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
      If Not Me.IsInitialized Then
        Return
      End If

      If m_addRemoveRecordsDelegate IsNot Nothing Then
        Return
      End If

      m_addRemoveRecordsDelegate = New AddRemoveRecordsDelegate(AddressOf Me.SyncDataTableWithSliderRecordsCount)

      Me.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, m_addRemoveRecordsDelegate)
    End Sub

    Private Sub SyncDataTableWithSliderRecordsCount()
      System.Diagnostics.Debug.Assert(m_addRemoveRecordsDelegate IsNot Nothing, "This method should only be invoked through the dispatcher by the Records count slider's ValueChanged event.")

      Dim rowCount As Integer

      ' Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      SyncLock m_quote.DefaultView
        rowCount = m_quote.Rows.Count
      End SyncLock

      Dim desiredRowCount As Integer = System.Convert.ToInt32(sliderRecordsCount.Value)

      If rowCount < desiredRowCount Then
        For i As Integer = rowCount To desiredRowCount - 1
          Me.AddNewQuote(MarketSimulator.GenerateRandomSymbol())
        Next
      ElseIf rowCount > desiredRowCount Then
        For i As Integer = rowCount To desiredRowCount + 1 Step -1
          Me.RemoveQuote(i - 1)
        Next
      End If

      m_addRemoveRecordsDelegate = Nothing
    End Sub

    Private Sub checkBoxAutoScroll_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      grid.AutoScrollCurrentItem = AutoScrollCurrentItemTriggers.All
    End Sub

    Private Sub checkBoxAutoScroll_Unchecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      grid.AutoScrollCurrentItem = AutoScrollCurrentItemTriggers.None
    End Sub

    Private Delegate Sub AddRemoveRecordsDelegate()

    Private m_addRemoveRecordsDelegate As AddRemoveRecordsDelegate

#End Region

#Region "PRIVATE METHODS"

    Private Function CreateDataTable() As DataTable
      Dim dataTable As New DataTable("StockData")

      dataTable.Columns.Add("Symbol", GetType(String))

      dataTable.Columns.Add("PreviousClose", GetType(Double))
      dataTable.Columns.Add("Open", GetType(Double))
      dataTable.Columns.Add("LastTrade", GetType(Double))
      dataTable.Columns.Add("TradeTime", GetType(String))

      dataTable.Columns.Add("Change", GetType(Double))

      ' Calculation columns meant to be hidden.
      dataTable.Columns.Add("LastTradeDiff", GetType(Double))
      dataTable.Columns.Add("ChangeDiff", GetType(Double))

      Return dataTable
    End Function

    Private Sub AddNewQuote(ByVal symbol As String)
      Dim dataRow As System.Data.DataRow

      Dim position As Integer

      ' Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      SyncLock m_quote.DefaultView
        dataRow = m_quote.NewRow()

        dataRow("Symbol") = symbol.ToUpper()

        dataRow("PreviousClose") = 0
        dataRow("Open") = 0
        dataRow("LastTrade") = 0
        dataRow("Change") = 0
        dataRow("TradeTime") = "Please wait..."

        dataRow("LastTradeDiff") = 0
        dataRow("ChangeDiff") = 0

        position = m_quote.Rows.Count
        m_quote.Rows.InsertAt(dataRow, position)
      End SyncLock

      ' Create and associate a quote worker with the DataRow.
      Dim quoteWorker As New QuoteWorker(position, symbol)

      m_quoteWorkers.Add(quoteWorker)

      Dim minLatencyBinding As New Binding()
      minLatencyBinding.Source = sliderSimulatedMinLatency
      minLatencyBinding.Path = New PropertyPath("Value")

      Dim maxLatencyBinding As New Binding()
      maxLatencyBinding.Source = sliderSimulatedMaxLatency
      maxLatencyBinding.Path = New PropertyPath("Value")

      BindingOperations.SetBinding(quoteWorker, QuoteWorker.MinLatencyProperty, minLatencyBinding)
      BindingOperations.SetBinding(quoteWorker, QuoteWorker.MaxLatencyProperty, maxLatencyBinding)
      AddHandler quoteWorker.QuoteInfoUpdated, AddressOf quoteWorker_QuoteInfoUpdated

      ' Subscribe to the quote worker's QuoteInfoUpdated so that we can apply the changes to the datarow.

      ' Start fetching quotes for this stock.
      quoteWorker.Start()
    End Sub

    Private Sub RemoveQuote(ByVal position As Integer)
      ' Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      SyncLock m_quote.DefaultView
        Dim quoteWorker As QuoteWorker = m_quoteWorkers(position)

        ' Stop the worker.
        quoteWorker.[Stop]()

        m_quoteWorkers.Remove(quoteWorker)

        m_quote.Rows.RemoveAt(position)
      End SyncLock
    End Sub

#End Region

#Region "PRIVATE FIELDS"

    Private m_quote As DataTable

    Private m_quoteWorkers As List(Of QuoteWorker)

#End Region
  End Class
End Namespace
