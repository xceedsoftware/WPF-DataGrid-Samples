'
' * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [QuoteInfo.vb]
' * 
' * This class represents a quote fetched information.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports System
Imports System.Collections.Generic

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
  Public Class QuoteInfo
    Public Sub New(ByVal symbol As String)
      Me.New(symbol, 0, 0, 0, MarketSimulator.Popularities.Passive, MarketSimulator.Tendencies.Stable)
    End Sub

    Friend Sub New(ByVal symbol As String, ByVal previousClose As Double, ByVal openingValue As Double, ByVal lastTrade As Double, ByVal popularity As MarketSimulator.Popularities, ByVal tendency As MarketSimulator.Tendencies)
      m_symbol = symbol
      m_previousClose = previousClose
      m_openingValue = openingValue
      m_lastTrade = lastTrade
      m_tradeTime = "N/A"

      m_popularity = popularity
      m_tendency = tendency
    End Sub

#Region "Symbol PROPERTY"

    Public ReadOnly Property Symbol() As String
      Get
        Return m_symbol
      End Get
    End Property

    Private m_symbol As String

#End Region

#Region "PreviousClose PROPERTY"

    Public ReadOnly Property PreviousClose() As Double
      Get
        Return m_previousClose
      End Get
    End Property

    Private m_previousClose As Double

#End Region

#Region "OpeningValue PROPERTY"

    Public ReadOnly Property OpeningValue() As Double
      Get
        Return m_openingValue
      End Get
    End Property

    Private m_openingValue As Double

#End Region

#Region "LastTrade PROPERTY"

    Private m_lastTrade As Double

    Public ReadOnly Property LastTrade() As Double
      Get
        Return m_lastTrade
      End Get
    End Property

#End Region

#Region "TradeTime PROPERTY"

    Private m_tradeTime As String

    Public ReadOnly Property TradeTime() As String
      Get
        Return m_tradeTime
      End Get
    End Property

#End Region

#Region "INTERNAL METHODS"

    Friend Sub SetLastTrade(ByVal lastTrade As Double, ByVal tradeTime As DateTime)
      m_lastTrade = lastTrade
      m_tradeTime = tradeTime.ToString("HH:mm:ss:FF")
    End Sub

#End Region

#Region "INTERNAL PROPERTIES"

    Friend ReadOnly Property Popularity() As MarketSimulator.Popularities
      Get
        Return m_popularity
      End Get
    End Property

    Private m_popularity As MarketSimulator.Popularities

    Friend ReadOnly Property Tendency() As MarketSimulator.Tendencies
      Get
        Return m_tendency
      End Get
    End Property

    Private m_tendency As MarketSimulator.Tendencies

#End Region
  End Class
End Namespace
