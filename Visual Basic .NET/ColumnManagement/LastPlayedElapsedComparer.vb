'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [LastPlayedElapsedComparer.vb]
'  
' This file demonstrates how to create a comparer for two strings representing
' a formated time span.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class LastPlayedElapsedComparer
    Implements IComparer

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
      Dim xStrValue As String = TryCast(x, String)
      Dim yStrValue As String = TryCast(y, String)

      If String.IsNullOrEmpty(xStrValue) Then
        If String.IsNullOrEmpty(yStrValue) Then
          Return 0
        Else
          Return -1
        End If
      Else
        If String.IsNullOrEmpty(yStrValue) Then
          Return 1
        End If
      End If

      Dim xValueParts() As String = xStrValue.Split(" "c)
      Dim yValueParts() As String = yStrValue.Split(" "c)
      Dim xValue As Long = Long.Parse(xValueParts(0))
      Dim yValue As Long = Long.Parse(yValueParts(0))
      Dim xUnit As String = xValueParts(1)
      Dim yUnit As String = yValueParts(1)

      If xUnit = yUnit Then
        If xValue < yValue Then
          Return -1
        End If

        If xValue > yValue Then
          Return 1
        End If

        Return 0
      Else
        If xUnit = "years" Then
          Return 1
        End If

        If yUnit = "years" Then
          Return -1
        End If

        If xUnit = "days" Then
          Return 1
        End If

        If yUnit = "days" Then
          Return -1
        End If

        If xUnit = "hours" Then
          Return 1
        End If

        If yUnit = "hours" Then
          Return -1
        End If

        Return 0
      End If
    End Function
  End Class
End Namespace
