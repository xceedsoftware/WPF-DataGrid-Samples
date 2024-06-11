'
' Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [DateTimeEditors.xaml.cs]
'  
' This class implements the event handlers referenced by some editors of this
' ResourceDictionary.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Text
Imports System.Windows

Imports Xceed.Wpf.Controls

Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Partial Public Class DateTimeEditors
    Inherits ResourceDictionary
    Private Sub InvariantAbbreviatedDateTimeEditor_QueryTextFromValue(ByVal sender As Object, ByVal e As QueryTextFromValueEventArgs)
      If (e.Value Is Nothing) OrElse (e.Value Is DBNull.Value) Then
        e.Text = String.Empty
      Else
        e.Text = DirectCast(e.Value, DateTime).ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
      End If
    End Sub

    Private Sub InvariantAbbreviatedDateTimeEditor_QueryValueFromText(ByVal sender As Object, ByVal e As QueryValueFromTextEventArgs)
      e.Value = Nothing
      e.HasParsingError = True

      Dim parsedDateTime As DateTime

      If DateTime.TryParse(e.Text, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, parsedDateTime) Then
        e.HasParsingError = False
        e.Value = parsedDateTime
      End If
    End Sub

    Private Sub InvariantAbbreviatedDateTimeEditor_AutoCompletingMask(ByVal sender As Object, ByVal e As AutoCompletingMaskEventArgs)
      Dim autoCompletionBuilder As New StringBuilder()

      Dim provider As System.ComponentModel.MaskedTextProvider = e.MaskedTextProvider
      Dim startPosition As Integer = e.StartPosition

      e.AutoCompleteStartPosition = provider.FindNonEditPositionFrom(startPosition, False) + 1
      Dim nextSeparatorIndex As Integer = provider.FindNonEditPositionFrom(startPosition, True)

      Dim validAutoCompletion As Boolean = False

      If e.AutoCompleteStartPosition = 0 Then
        ' auto completing the day.
        For i As Integer = e.AutoCompleteStartPosition To nextSeparatorIndex - 1
          If provider.IsAvailablePosition(i) Then
            autoCompletionBuilder.Insert(0, "0"c)
          Else
            Dim alreadyEnteredCharacter As Char = provider(i)

            If alreadyEnteredCharacter <> "0"c Then
              validAutoCompletion = True
            End If

            autoCompletionBuilder.Append(alreadyEnteredCharacter)
          End If
        Next i
      ElseIf e.AutoCompleteStartPosition = 3 Then
        ' auto completing the abbreviated month name.
        For i As Integer = e.AutoCompleteStartPosition To nextSeparatorIndex - 1
          If Not provider.IsAvailablePosition(i) Then
            autoCompletionBuilder.Append(provider(i))
          End If
        Next i

        Dim abbreviatedMonthNames() As String = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames

        Dim partialInput As String = autoCompletionBuilder.ToString()
        Dim uniqueCorrespondingMonthName As String = Nothing

        For Each monthName As String In abbreviatedMonthNames
          If monthName.ToUpper().StartsWith(partialInput.ToUpper()) Then
            If uniqueCorrespondingMonthName IsNot Nothing Then
              uniqueCorrespondingMonthName = Nothing
              Exit For
            End If

            uniqueCorrespondingMonthName = monthName
          End If
        Next monthName

        If uniqueCorrespondingMonthName IsNot Nothing Then
          autoCompletionBuilder = New StringBuilder(uniqueCorrespondingMonthName)
          validAutoCompletion = True
        End If
      Else
        ' auto completing the year.
        For i As Integer = e.AutoCompleteStartPosition To nextSeparatorIndex - 1
          If (Not provider.IsAvailablePosition(i)) Then
            autoCompletionBuilder.Append(provider(i))
          End If
        Next i

        ' Because different calendars behave differently, autocompletion is only possible
        ' if exactly one or two digits are already entered.
        If autoCompletionBuilder.Length = 1 Then
          autoCompletionBuilder.Insert(0, "0"c)
        End If

        If autoCompletionBuilder.Length = 2 Then
          Dim partialYear As Integer = Int32.Parse(autoCompletionBuilder.ToString())

          Dim dateTimeFormatInfo As System.Globalization.DateTimeFormatInfo = System.Globalization.DateTimeFormatInfo.GetInstance(System.Globalization.CultureInfo.InvariantCulture)

          Dim fullYear As Integer = dateTimeFormatInfo.Calendar.ToFourDigitYear(partialYear)

          autoCompletionBuilder = New StringBuilder(fullYear.ToString())
          validAutoCompletion = True
        End If
      End If

      e.AutoCompleteText = autoCompletionBuilder.ToString()
      e.Cancel = Not validAutoCompletion
    End Sub
  End Class
End Namespace
