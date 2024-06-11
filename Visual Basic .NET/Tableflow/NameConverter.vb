'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [NameConverter.vb]
'*
'* This file demonstrates how to create a converter to display a combined
'* name from 2 values
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'*
'

Imports System
Imports System.Data
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Class NameConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If value Is Nothing Then
        Return Nothing
      End If

      'By default, the parameter will contain the ItemsSource define on the ForeignKeyConfiguration when this converter is set as a ForeignKeyValueConverter.
      Dim employees As DataView = TryCast(parameter, DataView)
      If employees Is Nothing Then
        Return Nothing
      End If

      employees.Sort = "EmployeeID"

      Dim index As Integer = employees.Find(value)
      Dim dataRow As DataRowView = employees(index)

      Return dataRow("LastName") & ", " & dataRow("FirstName")
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Return Nothing
    End Function
  End Class

  Public Class NameMultiConverter
    Implements IMultiValueConverter
    Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
      If values(0) Is DependencyProperty.UnsetValue Then
        values(0) = String.Empty
      End If

      If values(1) Is DependencyProperty.UnsetValue Then
        values(1) = String.Empty
      End If

      Dim name As String

      If String.IsNullOrEmpty(TryCast(values(0), String)) Then
        Return TryCast(values(1), String)
      End If

      If String.IsNullOrEmpty(TryCast(values(1), String)) Then
        Return TryCast(values(0), String)
      End If

      Select Case CStr(parameter)
        Case "FormatLastFirst"
          name = CStr(values(1)) & ", " & CStr(values(0))

        Case Else
          name = CStr(values(0)) & " " & CStr(values(1))
      End Select

      Return name
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
      Return Nothing
    End Function
  End Class
End Namespace
