'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MonthNamesDistinctValuesSortComparer.vb]
'
' This class is used to compare month names in order to sort them
' in logical order instead of alphabetical order
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
  Public Class MonthNamesDistinctValuesSortComparer
    Implements IComparer
    Public Sub New()
      Dim i As Integer = 0
      Do While i < DateTimeFormatInfo.CurrentInfo.MonthNames.Length
        Dim monthName As String = DateTimeFormatInfo.InvariantInfo.MonthNames(i)
        m_monthNameToIndex.Add(monthName, i)
        i += 1
      Loop
    End Sub

#Region "IComparer Members"

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
      Dim xMonth As String = TryCast(x, String)
      Dim yMonth As String = TryCast(y, String)

      If (Not xMonth Is Nothing) AndAlso (Not yMonth Is Nothing) Then
        Dim xIndex As Integer = m_monthNameToIndex(xMonth)
        Dim yIndex As Integer = m_monthNameToIndex(yMonth)

        If xIndex < yIndex Then
          Return -1
        ElseIf xIndex = yIndex Then
          Return 0
        Else
          Return 1
        End If
      End If

      If (xMonth Is Nothing) AndAlso (yMonth Is Nothing) Then
        Return 0
      ElseIf (xMonth Is Nothing) Then
        Return -1
      ElseIf (yMonth Is Nothing) Then
        Return 1
      End If

      ' Unable to compare, return 0 (equals)
      Return 0
    End Function

#End Region

    Private m_monthNameToIndex As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
  End Class
End Namespace

