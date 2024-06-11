'
' Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [MaskedTextEditors.xaml.cs]
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
Imports System.Net
Imports System.Text
Imports System.Windows

Imports Xceed.Wpf.Controls

Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Partial Public Class MaskedTextBoxEditors
    Inherits ResourceDictionary
    Private Sub IPAddressEditor_QueryTextFromValue(ByVal sender As Object, ByVal e As QueryTextFromValueEventArgs)
      Dim ipAddress As IPAddress = TryCast(e.Value, IPAddress)

      If ipAddress Is Nothing Then
        Return
      End If

      Dim bytes() As Byte = ipAddress.GetAddressBytes()

      Dim ipBuilder As New StringBuilder()

      For i As Integer = 0 To bytes.Length - 1
        ipBuilder.Append(bytes(i).ToString("00#") & ".")
      Next i

      ipBuilder.Remove(ipBuilder.Length - 1, 1)

      e.Text = ipBuilder.ToString()
    End Sub

    Private Sub IPAddressEditor_QueryValueFromText(ByVal sender As Object, ByVal e As QueryValueFromTextEventArgs)
      If String.IsNullOrEmpty(e.Text) Then
        e.Value = IPAddress.None
        e.HasParsingError = False
        Return
      End If

      Dim ipParts() As String = e.Text.Split(New Char() {"."c})
      Dim bytes(ipParts.Length - 1) As Byte

      For i As Integer = 0 To ipParts.Length - 1
        If (Not Byte.TryParse(ipParts(i), bytes(i))) Then
          e.Value = IPAddress.None
          e.HasParsingError = True
          Return
        End If
      Next i

      e.Value = New IPAddress(bytes)
      e.HasParsingError = False
    End Sub

    Private Sub OnAutoCompletingMaskHandler(ByVal sender As Object, ByVal e As AutoCompletingMaskEventArgs)
      ' This handler shifts the edited positions up to the next literal and inserts zeroes in the remaining positions.
      Dim autoCompletionBuilder As New StringBuilder()

      e.AutoCompleteStartPosition = e.MaskedTextProvider.FindNonEditPositionFrom(e.StartPosition, False) + 1
      Dim nextSeparatorIndex As Integer = e.MaskedTextProvider.FindNonEditPositionFrom(e.StartPosition, True)

      For i As Integer = e.AutoCompleteStartPosition To nextSeparatorIndex - 1
        If (Not e.MaskedTextProvider.IsAvailablePosition(i)) Then
          autoCompletionBuilder.Append(e.MaskedTextProvider(i))
        Else
          autoCompletionBuilder.Insert(0, "0"c)
        End If
      Next i

      e.AutoCompleteText = autoCompletionBuilder.ToString()
    End Sub
  End Class
End Namespace
