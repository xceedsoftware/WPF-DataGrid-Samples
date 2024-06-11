'
' * Xceed DataGrid for WPF - SAMPLE CODE - Merged Headers Sample Application
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

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid

Namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
  Partial Public Class MainPage
    Inherits Page
#Region "CONSTRUCTORS"

    Public Sub New()
      m_dataItems = Me.CreateDataItems()

      InitializeComponent()
    End Sub

#End Region ' CONSTRUCTORS

#Region "PROPERTIES"

#Region "DataItems Property"

    Public ReadOnly Property DataItems() As IEnumerable(Of BusinessObject)
      Get
        Return m_dataItems
      End Get
    End Property

    Private ReadOnly m_dataItems As IEnumerable(Of BusinessObject)

#End Region

#End Region

#Region "PRIVATE METHODS"

    Private Function CreateDataItems() As IEnumerable(Of BusinessObject)
      Dim collection = New ObservableCollection(Of BusinessObject)()
      Dim rnd = New Random(123456)

      For Each name As String In Names
        collection.Add(Me.CreateDataItem(rnd, name))
      Next

      Return collection
    End Function

    Private Function CreateDataItem(ByVal rnd As Random, ByVal name As String) As BusinessObject
      Dim item = New BusinessObject()

      item.Symbol = name
      item.PreviousClose = Me.GenerateValue(rnd)
      item.Open = Me.GenerateValue(rnd)
      item.LastTrade = Me.GenerateValue(rnd)
      item.Change = item.LastTrade - item.Open
      item.DailyHigh = Math.Max(item.LastTrade, item.Open) + Math.Round(Convert.ToDecimal(rnd.NextDouble() * 15), 2)
      item.DailyLow = Math.Max(0, Math.Min(item.LastTrade, item.Open) * 80 / 100)
      item.DailyVolume = Math.Round(Me.GenerateValue(rnd) * 1000 / 3, 2)
      Dim dateTime = New DateTime(2015, 7, 1, rnd.Next(8, 17), rnd.Next(0, 60), rnd.Next(0, 60))
      item.LastTradeTime = dateTime.ToString("HH:mm:ss")
      item.TrailingPriceOverSales = Me.GenerateValue(rnd)
      item.TrailingPE = Me.GenerateValue(rnd)
      item.TrailingEPS = Me.GenerateValue(rnd)
      item.TrailingHigh = Math.Round(item.DailyHigh + (Me.GenerateValue(rnd) / 10), 2)
      item.TrailingLow = Math.Round(Math.Max(0, item.DailyLow - (Me.GenerateValue(rnd) / 10)), 2)
      item.TrailingAvgVolume = Math.Round(item.DailyVolume * Me.GenerateValue(rnd) * 100, 2)
      item.TrailingRevenu = Me.GenerateValue(rnd) * 10000
      item.TrailingGrossProfit = Math.Round(Me.GenerateValue(rnd) * 1000 / 3, 2)
      item.TrailingRevenuPerShare = Me.GenerateValue(rnd) * 10
      item.YearPriceOverSales = Me.GenerateValue(rnd)
      item.YearPE = Me.GenerateValue(rnd)
      item.YearEPS = Me.GenerateValue(rnd)
      item.YearHigh = Math.Round(item.DailyHigh + (Me.GenerateValue(rnd) / 10), 2)
      item.YearLow = Math.Round(Math.Max(0, item.DailyLow - (Me.GenerateValue(rnd) / 10)), 2)
      item.YearAvgVolume = Math.Round(item.DailyVolume * Me.GenerateValue(rnd) * 100, 2)
      item.YearRevenu = Me.GenerateValue(rnd) * 10000
      item.YearGrossProfit = Math.Round(Me.GenerateValue(rnd) * 100, 2)
      item.YearRevenuPerShare = Me.GenerateValue(rnd) * 10

      Return item
    End Function

    Private Function GenerateValue(ByVal rnd As Random) As Decimal
      Return Math.Round(Convert.ToDecimal(rnd.NextDouble() * 10000.0R + 500.0R), 2)
    End Function

#End Region

#Region "STATIC MEMBERS"

    Private Shared Names() As String = {
      "XCEED", "YHOO", "MSFT", "AAPL", "GOOG", "IBM", "CSCO", "DELL", "INTC", "NTGR",
      "KONE", "VISN", "IPCI", "SYRG", "BDEV", "NXTL", "BIOA", "PNXN", "NOAH", "SHIN",
      "DACN", "STVN", "TRV", "JPM", "MMM", "MCD", "JNJ", "PFE", "GE", "CAT"
    }

#End Region
  End Class
End Namespace
