'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [BooleanInverserConverter.vb]
'  
' This class is a Converter (typically used in a Binding) that returns the inverse 
' value of a boolean.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.CustomViews
  <ValueConversion(GetType(Boolean), GetType(Boolean))> _
  Public Class BooleanInverserConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If TypeOf value Is Boolean Then
        Return Not CBool(value)
      End If

      Return value
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      If TypeOf value Is Boolean Then
        Return Not CBool(value)
      End If

      Return value
    End Function
  End Class
End Namespace
