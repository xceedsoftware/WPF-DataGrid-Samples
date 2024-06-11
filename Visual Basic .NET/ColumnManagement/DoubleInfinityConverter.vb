'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [DoubleInfinityConverter.vb]
'  
' This class implements the DoubleInfinityConverter class which allows a string to be
' converted to a double. This converter accepts the string "infinity" and other variants
' as a value (converted to Double.PositiveInfinity).
'
' This class will be used in the ColumnPropertyCell class.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Globalization
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  <ValueConversion(GetType(Double), GetType(String))> _
  Public Class DoubleInfinityConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      ' We don't want to use the CurrentUICulture (the language) received in the culture 
      ' parameter, but rather the CurrentCulture (the culture/ControlPanel settings).
      If TypeOf value Is Double Then
        Return System.Convert.ToString(CType(value, Double), CultureInfo.CurrentCulture.NumberFormat)
      End If

      Return value
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      If Not targetType Is GetType(Double) Then
        Throw New ArgumentException("The target type must be double.", "targetType")
      End If

      Dim strValue As String = TryCast(value, String)

      If Not strValue Is Nothing Then
        Dim dblValue As Double

        strValue.ToLower(culture)
        strValue.Trim()

        If ((strValue = "infinity") Or _
            (strValue = "+infinity") Or _
            (strValue = "+inf") Or _
            (strValue = "inf")) Then
          Return Double.PositiveInfinity
        End If

        If Double.TryParse(strValue, dblValue) Then
          Return dblValue
        End If
      End If

      Return value
    End Function
  End Class
End Namespace
