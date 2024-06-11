'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MonthNameConverter.vb]
'
' This class is used to get the Month name from an integer ranging from 1 to 12
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows
Imports System.Windows.Data
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
  Public Class MonthNameConverter
    Implements IValueConverter

    Public Sub New()
      Dim i As Integer = 0
      Do While i < DateTimeFormatInfo.InvariantInfo.MonthNames.Length
        Dim monthName As String = DateTimeFormatInfo.InvariantInfo.MonthNames(i)
        m_indexToMonthName.Add(i + 1, monthName)
        m_monthNameToIndex.Add(monthName, i + 1)
        i += 1
      Loop

    End Sub

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If value Is Nothing Then
        Return Binding.DoNothing
      End If

      Try
        Return m_indexToMonthName(CInt(Fix(value)))
      Catch
        Return DependencyProperty.UnsetValue
      End Try
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      If value Is Nothing Then
        Return Binding.DoNothing
      End If

      Try
        Return m_monthNameToIndex(CStr(value))
      Catch
        Return DependencyProperty.UnsetValue
      End Try
    End Function

    Private m_monthNameToIndex As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
    Private m_indexToMonthName As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
  End Class

End Namespace
