'
' * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MarketSimulator.vb]
' * 
' * This class provides simulated data to the Live Updating sample.
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

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
  Friend NotInheritable Class MarketSimulator
    Private Sub New()
    End Sub
    Private Const MaxValue As Double = 999.99
    Private Const MinValue As Double = 1

    Private Const MaxChangePercentageErratic As Double = 0.8
    Private Const MaxChangePercentageStable As Double = 0.01
    Private Const MaxChangePercentageSteadyGainer As Double = 0.03
    Private Const MaxChangePercentageSteadyLooser As Double = 0.03

    Private Const Letters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    Private Shared Randomizer As New Random()

    Private Shared Function NextInteger() As Integer

      SyncLock MarketSimulator.Randomizer
        Return MarketSimulator.Randomizer.Next()
      End SyncLock

    End Function

    Private Shared Function NextInteger(ByVal minValue As Integer, ByVal maxValue As Integer) As Integer

      SyncLock MarketSimulator.Randomizer
        Return MarketSimulator.Randomizer.Next(minValue, maxValue)
      End SyncLock

    End Function

    Private Shared Function NextDouble() As Double

      SyncLock MarketSimulator.Randomizer
        Return MarketSimulator.Randomizer.NextDouble()
      End SyncLock

    End Function

    Friend Shared Function GenerateRandomSymbol() As String
      Dim stringBuilder As New StringBuilder()
      For i As Integer = 0 To MarketSimulator.NextInteger(3, 5) - 1

        stringBuilder.Append(MarketSimulator.Letters(MarketSimulator.NextInteger(0, Letters.Length)))
      Next

      Return stringBuilder.ToString()
    End Function

    Friend Shared Function GenerateQuoteInfo(ByVal symbol As String) As QuoteInfo
      Dim popularity As Popularities
      Dim tendency As Tendencies

      If [String].Equals(symbol, "XCEED") Then
        popularity = Popularities.Active
        tendency = Tendencies.SteadyGainer
      Else
        popularity = DirectCast(MarketSimulator.NextInteger(0, [Enum].GetValues(GetType(Popularities)).Length), Popularities)
        tendency = DirectCast(MarketSimulator.NextInteger(0, [Enum].GetValues(GetType(Tendencies)).Length), Tendencies)
      End If

      Dim previousClose As Double = MarketSimulator.NextInteger(5, 150)
      Dim openingValue As Double = MarketSimulator.SimulateChange(previousClose, popularity, tendency)
      Dim lastTrade As Double = MarketSimulator.SimulateChange(openingValue, popularity, tendency)

      Return New QuoteInfo(symbol, previousClose, openingValue, lastTrade, popularity, tendency)
    End Function

    Public Shared Function SimulateChange(ByVal quoteInfo As QuoteInfo) As Double
      Return MarketSimulator.SimulateChange(quoteInfo.LastTrade, quoteInfo.Popularity, quoteInfo.Tendency)
    End Function

    Private Shared Function SimulateChange(ByVal currentValue As Double, ByVal popularity As Popularities, ByVal tendency As Tendencies) As Double
      Dim newValue As Double = currentValue

      Dim activityFactor As Double = (MarketSimulator.NextDouble() * 100)

      If popularity = Popularities.Passive Then
        If activityFactor < 66 Then
          Return newValue
        End If
      ElseIf activityFactor < 33 Then
        Return newValue
      End If

      If (tendency = Tendencies.Stable) AndAlso (activityFactor < 33) Then
        Return newValue
      End If


      Dim changeFactor As Double = (MarketSimulator.NextDouble() * 100)

      Select Case tendency
        Case Tendencies.Erratic
          Dim change As Double = currentValue * (MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageErratic)

          If MarketSimulator.NextDouble() < 0.5 Then
            newValue = (currentValue - change)
          Else
            newValue = (currentValue + change)
          End If

          Exit Select
        Case Tendencies.SteadyGainer

          Dim change As Double = currentValue * (MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageSteadyGainer)

          If MarketSimulator.NextDouble() < 0.25 Then
            newValue = (currentValue - change)
          Else
            newValue = (currentValue + change)
          End If

          Exit Select
        Case Tendencies.SteadyLoser

          Dim change As Double = currentValue * (MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageSteadyLooser)

          If MarketSimulator.NextDouble() < 0.75 Then
            newValue = (currentValue - change)
          Else
            newValue = (currentValue + change)
          End If

          Exit Select
        Case Else

          Dim change As Double = currentValue * (MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageStable)

          If MarketSimulator.NextDouble() < 0.5 Then
            newValue = (currentValue - change)
          Else
            newValue = (currentValue + change)
          End If

          Exit Select
      End Select

      Return Math.Min(Math.Max(MarketSimulator.MinValue, newValue), MarketSimulator.MaxValue)
    End Function

#Region "NESTED ENUMS"

    Public Enum Tendencies
      Stable = 0
      Erratic = 1
      SteadyGainer = 2
      SteadyLoser = 3
    End Enum

    Public Enum Popularities
      Passive = 0
      Active = 1
    End Enum

#End Region
  End Class
End Namespace
