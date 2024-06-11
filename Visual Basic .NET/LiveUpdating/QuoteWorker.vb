'
' * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [QuoteWorker.vb]
' * 
' * This class is used to refresh a quote information asynchronously.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports System
Imports System.Windows.Threading
Imports System.Globalization
Imports System.Net
Imports System.IO
Imports System.Threading
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
  Public Class QuoteWorker
    Inherits DependencyObject
    Private Shared LatencyRandomizer As New Random()

    Public Sub New(ByVal dataRowIndex As Integer, ByVal symbol As String)
      m_dataRowIndex = dataRowIndex
      m_quoteInfo = MarketSimulator.GenerateQuoteInfo(symbol)
    End Sub

#Region "MinLatency Dependency Property"

    Public Property MinLatency() As Integer
      Get
        Return CInt(GetValue(MinLatencyProperty))
      End Get
      Set(ByVal value As Integer)
        SetValue(MinLatencyProperty, value)
      End Set
    End Property

    Public Shared ReadOnly MinLatencyProperty As DependencyProperty = DependencyProperty.Register("MinLatency", GetType(Integer), GetType(QuoteWorker), New UIPropertyMetadata(0))

#End Region

#Region "MaxLatency Dependency Property"

    Public Property MaxLatency() As Integer
      Get
        Return CInt(GetValue(MaxLatencyProperty))
      End Get
      Set(ByVal value As Integer)
        SetValue(MaxLatencyProperty, value)
      End Set
    End Property

    Public Shared ReadOnly MaxLatencyProperty As DependencyProperty = DependencyProperty.Register("MaxLatency", GetType(Integer), GetType(QuoteWorker), New UIPropertyMetadata(Integer.MaxValue))

#End Region

#Region "DataRowIndex PROPERTY"
    Private m_dataRowIndex As Integer

    Public ReadOnly Property DataRowIndex() As Integer
      Get
        Return m_dataRowIndex
      End Get
    End Property
#End Region

#Region "QuoteInfo PROPERTY"
    Private m_quoteInfo As QuoteInfo

    Public Property QuoteInfo() As QuoteInfo
      Get
        Return m_quoteInfo
      End Get
      Set(ByVal value As QuoteInfo)
        m_quoteInfo = value
      End Set
    End Property
#End Region

#Region "Started PROPERTY"

    Public ReadOnly Property Started() As Boolean
      Get
        Return m_started
      End Get
    End Property

    Private m_started As Boolean

#End Region


#Region "PUBLIC METHODS"

    Public Sub Start()
      If m_started Then
        Return
      End If

      m_started = True

      Me.[Continue]()
    End Sub

    Public Sub [Stop]()
      m_started = False
    End Sub

    Private Sub [Continue]()
      If Not m_started Then
        Return
      End If

      Dim d As DispatcherOperationDelegate = AddressOf ConvertedAnonymousMethod1

      ' Invoke the QueueUserWorkItem method on the Dispatcher thread for the MinLatency and MaxLatency DPs to be accessible.
      Me.Dispatcher.BeginInvoke(DispatcherPriority.Background, d)
    End Sub

#End Region

    Private Sub UpdateQuoteInfoAsync(ByVal state As Object)
      If Not m_started Then
        Return
      End If

      ' Simulate a lag in collecting data.
      Dim latencyMinMax As Integer() = DirectCast(state, Integer())
      Thread.Sleep(QuoteWorker.LatencyRandomizer.[Next](latencyMinMax(0), latencyMinMax(1)))

      ' Simulate a variation in the market.
      Dim lastTrade As Double = MarketSimulator.SimulateChange(m_quoteInfo)

      m_quoteInfo.SetLastTrade(lastTrade, DateTime.Now)

      Me.OnQuoteInfoUpdated()

      Me.[Continue]()
    End Sub

    Protected Overridable Sub OnQuoteInfoUpdated()

      '      If Me.QuoteInfoUpdated IsNot Nothing Then
      RaiseEvent QuoteInfoUpdated(Me, EventArgs.Empty)
      'End If

    End Sub

    Public Event QuoteInfoUpdated As EventHandler

#Region "PRIVATE FIELDS"

    Private Delegate Sub DispatcherOperationDelegate()
    Private Sub ConvertedAnonymousMethod1()
      ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.UpdateQuoteInfoAsync), New Integer() {Me.MinLatency, Me.MaxLatency})
    End Sub

#End Region

  End Class
End Namespace
