'
' * Xceed DataGrid for WPF - SAMPLE CODE - Solid Foundation Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [GreaterThanZeroConverter.vb]
' *  
' * This file demonstrates how to create a converter to evaluate if a 
' * double is greater than zero.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Text
Imports System.Windows.Data
Imports System.Globalization
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating

  <ValueConversion(GetType(Double), GetType(Boolean))> _
  Public Class GreaterThanZeroConverter
    Implements IValueConverter
#Region "IValueConverter Members"

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert

      If Not targetType.IsAssignableFrom(GetType(Boolean)) Then
        Return DependencyProperty.UnsetValue
      End If

      If value Is Nothing Then
        Return DependencyProperty.UnsetValue
      End If

      Dim number As Double = 0

      Try
        number = System.Convert.ToDouble(value)
      Catch
        Return DependencyProperty.UnsetValue
      End Try

      Return number > 0
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack

      If Not targetType.IsAssignableFrom(GetType(Double)) Then
        Return DependencyProperty.UnsetValue
      End If

      If value Is Nothing Then
        Return 0
      End If

      Dim isGreaterThanZero As Boolean = CBool(value)

      If isGreaterThanZero Then
        Return 1
      End If

      Return 0
    End Function

#End Region
  End Class
End Namespace
