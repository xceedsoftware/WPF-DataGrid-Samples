'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [PercentConverter.vb]
' *  
' * This class converts a value to a percentage, or vice versa.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
  <ValueConversion(GetType(Single), GetType(String))>
  Public Class PercentConverter
    Implements IValueConverter
    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      'If no value, return 0%.
      If (value Is Nothing) OrElse Object.Equals(String.Empty, value) Then
        Return String.Format(CultureInfo.CurrentCulture, "{0}", value)
      End If

      Dim valueType As Type = value.GetType()

      ' Only Float or Double values can be converted
      If (Not valueType.IsAssignableFrom(GetType(Single))) AndAlso (Not valueType.IsAssignableFrom(GetType(Double))) Then
        Return Binding.DoNothing
      End If

      Return String.Format(CultureInfo.CurrentCulture, "{0:P0}", value)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Dim result = Nothing

      If Single.TryParse(TryCast(value, String), NumberStyles.Float, CultureInfo.CurrentCulture, result) Then
        Return result
      End If

      Return Binding.DoNothing
    End Function
  End Class
End Namespace