'
' * Xceed DataGrid for WPF - SAMPLE CODE - Solid Foundation Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [InvariantCurrencyConverter.vb]
' *  
' * This file demonstrates how to create a converter to display a double
' * in a currency formatting
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Data
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
  <ValueConversion(GetType(Double), GetType(String))> _
  Public Class USCurrencyConverter
    Implements IValueConverter
    Private USCultureInfo As CultureInfo = CultureInfo.GetCultureInfo("EN-US")

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If (value IsNot Nothing) AndAlso (Not Object.Equals(String.Empty, value)) Then
        Try
          Return String.Format(USCultureInfo, "{0:C}", value)
        Catch generatedExceptionName As FormatException
        Catch generatedExceptionName As OverflowException
        End Try
      End If

      Return String.Format(CultureInfo.CurrentCulture, "{0}", value)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Return Double.Parse(TryCast(value, String), NumberStyles.Currency, USCultureInfo)
    End Function
  End Class
End Namespace
