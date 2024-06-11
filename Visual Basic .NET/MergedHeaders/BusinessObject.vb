'
' * Xceed DataGrid for WPF - SAMPLE CODE - Merged Headers Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [BusinessObject.vb]
' *  
' * Class that exposes information about incomes and expenses in a year.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
  Public NotInheritable Class BusinessObject
    Implements INotifyPropertyChanged

#Region "Properties"

#Region "Change Property"

    Public Property Change() As Decimal
      Get
        Return m_change
      End Get
      Set(ByVal value As Decimal)
        m_change = value
        Me.RaisePropertyChanged("Change")
      End Set
    End Property

    Private m_change As Decimal

#End Region

#Region "DailyHigh Property"

    Public Property DailyHigh() As Decimal
      Get
        Return m_dailyHigh
      End Get
      Set(ByVal value As Decimal)
        m_dailyHigh = value
        Me.RaisePropertyChanged("DailyHigh")
      End Set
    End Property

    Private m_dailyHigh As Decimal

#End Region

#Region "DailyLow Property"

    Public Property DailyLow() As Decimal
      Get
        Return m_dailyLow
      End Get
      Set(ByVal value As Decimal)
        m_dailyLow = value
        Me.RaisePropertyChanged("DailyLow")
      End Set
    End Property

    Private m_dailyLow As Decimal

#End Region

#Region "DailyVolume Property"

    Public Property DailyVolume() As Decimal
      Get
        Return m_dailyVolume
      End Get
      Set(ByVal value As Decimal)
        m_dailyVolume = value
        Me.RaisePropertyChanged("DailyVolume")
      End Set
    End Property

    Private m_dailyVolume As Decimal

#End Region

#Region "LastTrade Property"

    Public Property LastTrade() As Decimal
      Get
        Return m_lastTrade
      End Get
      Set(ByVal value As Decimal)
        m_lastTrade = value
        Me.RaisePropertyChanged("LastTrade")
      End Set
    End Property

    Private m_lastTrade As Decimal

#End Region

#Region "LastTradeTime Property"

    Public Property LastTradeTime() As String
      Get
        Return m_lastTradeTime
      End Get
      Set(ByVal value As String)
        m_lastTradeTime = value
        Me.RaisePropertyChanged("LastTradeTime")
      End Set
    End Property

    Private m_lastTradeTime As String

#End Region

#Region "Open Property"

    Public Property Open() As Decimal
      Get
        Return m_open
      End Get
      Set(ByVal value As Decimal)
        m_open = value
        Me.RaisePropertyChanged("Open")
      End Set
    End Property

    Private m_open As Decimal

#End Region

#Region "PreviousClose Property"

    Public Property PreviousClose() As Decimal
      Get
        Return m_previousClose
      End Get
      Set(ByVal value As Decimal)
        m_previousClose = value
        Me.RaisePropertyChanged("PreviousClose")
      End Set
    End Property

    Private m_previousClose As Decimal

#End Region

#Region "Symbol Property"

    Public Property Symbol() As String
      Get
        Return m_symbol
      End Get
      Set(ByVal value As String)
        m_symbol = value
        Me.RaisePropertyChanged("Symbol")
      End Set
    End Property

    Private m_symbol As String

#End Region

#Region "TrailingAvgVolume Property"

    Public Property TrailingAvgVolume() As Decimal
      Get
        Return m_trailingAvgVolume
      End Get
      Set(ByVal value As Decimal)
        m_trailingAvgVolume = value
        Me.RaisePropertyChanged("TrailingAvgVolume")
      End Set
    End Property

    Private m_trailingAvgVolume As Decimal

#End Region

#Region "TrailingEPS Property"

    Public Property TrailingEPS() As Decimal
      Get
        Return m_trailingEPS
      End Get
      Set(ByVal value As Decimal)
        m_trailingEPS = value
        Me.RaisePropertyChanged("TrailingEPS")
      End Set
    End Property

    Private m_trailingEPS As Decimal

#End Region

#Region "TrailingGrossProfit Property"

    Public Property TrailingGrossProfit() As Decimal
      Get
        Return m_trailingGrossProfit
      End Get
      Set(ByVal value As Decimal)
        m_trailingGrossProfit = value
        Me.RaisePropertyChanged("TrailingGrossProfit")
      End Set
    End Property

    Private m_trailingGrossProfit As Decimal

#End Region

#Region "TrailingHigh Property"

    Public Property TrailingHigh() As Decimal
      Get
        Return m_trailingHigh
      End Get
      Set(ByVal value As Decimal)
        m_trailingHigh = value
        Me.RaisePropertyChanged("TrailingHigh")
      End Set
    End Property

    Private m_trailingHigh As Decimal

#End Region

#Region "TrailingLow Property"

    Public Property TrailingLow() As Decimal
      Get
        Return m_trailingLow
      End Get
      Set(ByVal value As Decimal)
        m_trailingLow = value
        Me.RaisePropertyChanged("TrailingLow")
      End Set
    End Property

    Private m_trailingLow As Decimal

#End Region

#Region "TrailingPE Property"

    Public Property TrailingPE() As Decimal
      Get
        Return m_trailingPE
      End Get
      Set(ByVal value As Decimal)
        m_trailingPE = value
        Me.RaisePropertyChanged("TrailingPE")
      End Set
    End Property

    Private m_trailingPE As Decimal

#End Region

#Region "TrailingPriceOverSales Property"

    Public Property TrailingPriceOverSales() As Decimal
      Get
        Return m_trailingPriceOverSales
      End Get
      Set(ByVal value As Decimal)
        m_trailingPriceOverSales = value
        Me.RaisePropertyChanged("TrailingPriceOverSales")
      End Set
    End Property

    Private m_trailingPriceOverSales As Decimal

#End Region

#Region "TrailingRevenu Property"

    Public Property TrailingRevenu() As Decimal
      Get
        Return m_trailingRevenu
      End Get
      Set(ByVal value As Decimal)
        m_trailingRevenu = value
        Me.RaisePropertyChanged("TrailingRevenu")
      End Set
    End Property

    Private m_trailingRevenu As Decimal

#End Region

#Region "TrailingRevenuPerShare Property"

    Public Property TrailingRevenuPerShare() As Decimal
      Get
        Return m_trailingRevenuPerShare
      End Get
      Set(ByVal value As Decimal)
        m_trailingRevenuPerShare = value
        Me.RaisePropertyChanged("TrailingRevenuPerShare")
      End Set
    End Property

    Private m_trailingRevenuPerShare As Decimal

#End Region

#Region "YearAvgVolume Property"

    Public Property YearAvgVolume() As Decimal
      Get
        Return m_yearAvgVolume
      End Get
      Set(ByVal value As Decimal)
        m_yearAvgVolume = value
        Me.RaisePropertyChanged("YearAvgVolume")
      End Set
    End Property

    Private m_yearAvgVolume As Decimal

#End Region

#Region "YearEPS Property"

    Public Property YearEPS() As Decimal
      Get
        Return m_yearEPS
      End Get
      Set(ByVal value As Decimal)
        m_yearEPS = value
        Me.RaisePropertyChanged("YearEPS")
      End Set
    End Property

    Private m_yearEPS As Decimal

#End Region

#Region "YearGrossProfit Property"

    Public Property YearGrossProfit() As Decimal
      Get
        Return m_yearGrossProfit
      End Get
      Set(ByVal value As Decimal)
        m_yearGrossProfit = value
        Me.RaisePropertyChanged("YearGrossProfit")
      End Set
    End Property

    Private m_yearGrossProfit As Decimal

#End Region

#Region "YearHigh Property"

    Public Property YearHigh() As Decimal
      Get
        Return m_yearHigh
      End Get
      Set(ByVal value As Decimal)
        m_yearHigh = value
        Me.RaisePropertyChanged("YearHigh")
      End Set
    End Property

    Private m_yearHigh As Decimal

#End Region

#Region "YearLow Property"

    Public Property YearLow() As Decimal
      Get
        Return m_yearLow
      End Get
      Set(ByVal value As Decimal)
        m_yearLow = value
        Me.RaisePropertyChanged("YearLow")
      End Set
    End Property

    Private m_yearLow As Decimal

#End Region

#Region "YearPE Property"

    Public Property YearPE() As Decimal
      Get
        Return m_yearPE
      End Get
      Set(ByVal value As Decimal)
        m_yearPE = value
        Me.RaisePropertyChanged("YearPE")
      End Set
    End Property

    Private m_yearPE As Decimal

#End Region

#Region "YearPriceOverSales Property"

    Public Property YearPriceOverSales() As Decimal
      Get
        Return m_yearPriceOverSales
      End Get
      Set(ByVal value As Decimal)
        m_yearPriceOverSales = value
        Me.RaisePropertyChanged("YearPriceOverSales")
      End Set
    End Property

    Private m_yearPriceOverSales As Decimal

#End Region

#Region "YearRevenu Property"

    Public Property YearRevenu() As Decimal
      Get
        Return m_yearRevenu
      End Get
      Set(ByVal value As Decimal)
        m_yearRevenu = value
        Me.RaisePropertyChanged("YearRevenu")
      End Set
    End Property

    Private m_yearRevenu As Decimal

#End Region

#Region "YearRevenuPerShare Property"

    Public Property YearRevenuPerShare() As Decimal
      Get
        Return m_yearRevenuPerShare
      End Get
      Set(ByVal value As Decimal)
        m_yearRevenuPerShare = value
        Me.RaisePropertyChanged("YearRevenuPerShare")
      End Set
    End Property

    Private m_yearRevenuPerShare As Decimal

#End Region

#End Region

#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub RaisePropertyChanged(ByVal propertyName As String)
      If Me.PropertyChangedEvent IsNot Nothing Then
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
      End If
    End Sub

#End Region

  End Class
End Namespace
