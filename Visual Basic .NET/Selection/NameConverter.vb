'
' Xceed DataGrid for WPF - SAMPLE CODE - Selection Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [NameConverter.vb]
'  
' This file demonstrates how to create a converter to display a combined
' name from 2 values
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Data
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Selection

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

  Public Class NameMutliConverter
    Implements IMultiValueConverter

    Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
      If (values(0) Is DependencyProperty.UnsetValue) Then
        values(0) = String.Empty
      End If

      If (values(1) Is DependencyProperty.UnsetValue) Then
        values(1) = String.Empty
      End If

      Dim name As String = String.Empty

      If (String.IsNullOrEmpty(values(0))) Then
        Return CType(values(1), String)
      End If

      If (String.IsNullOrEmpty(values(1))) Then
        Return CType(values(0), String)
      End If

      Select Case CType(parameter, String)
        Case "FormatLastFirst"
          name = CType(values(1), String) + ", " + CType(values(0), String)
          Exit Select

        Case "FormatNormal"
        Case Else
          name = CType(values(0), String) + " " + CType(values(1), String)
          Exit Select

      End Select

      Return name
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function
  End Class
End Namespace
