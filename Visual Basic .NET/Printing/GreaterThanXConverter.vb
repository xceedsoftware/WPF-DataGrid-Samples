'
' Xceed DataGrid for WPF - SAMPLE CODE - Printing Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [GreaterThanXConverter.vb]
'  
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Text
Imports System.Windows.Data
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.Printing
  <ValueConversion(GetType(Object), GetType(Boolean))> _
  Public Class GreaterThanXConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If Not targetType.IsAssignableFrom(GetType(Boolean)) Then
        Throw New ArgumentException("The value can only be converted to a boolean value.", "targetType")
      End If

      If (Not value Is Nothing) AndAlso (value.Equals(String.Empty)) Then
        Return False
      End If

      Dim number As Double = 0
      Dim numberParameter As Double = 0

      Try
        number = System.Convert.ToDouble(value)
        numberParameter = System.Convert.ToDouble(parameter)
      Catch
        Return False
      End Try

      Return number > numberParameter
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Throw New NotSupportedException()
    End Function

  End Class
End Namespace
