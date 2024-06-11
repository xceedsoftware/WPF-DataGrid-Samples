'
' Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [GreaterThanConverter.vb]
'  
' This class demonstrates how to create a converter that compares
' a numeric value to an comparison value (0 by default) and returns 
' true if it is greater.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'

Imports System.Windows
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Exporting
  <ValueConversion(GetType(Object), GetType(Boolean))> _
  Public Class GreaterThanConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert

      If Not targetType.IsAssignableFrom(GetType(Boolean)) Then
        Return DependencyProperty.UnsetValue
      End If

      If value Is Nothing Then
        Return False
      End If

      Dim number As Double = 0D
      Dim comparedToValue As Double = 0D

      Try
        number = System.Convert.ToDouble(value)
      Catch
        Return False
      End Try

      If Not parameter Is Nothing Then
        Try
          comparedToValue = System.Convert.ToDouble(parameter)
        Catch
          Return DependencyProperty.UnsetValue
        End Try
      End If

      Return number > comparedToValue
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
      Return Binding.DoNothing
    End Function

  End Class
End Namespace
