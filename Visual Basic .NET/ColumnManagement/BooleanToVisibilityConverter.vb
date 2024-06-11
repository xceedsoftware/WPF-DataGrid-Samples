'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [BooleanToVisibilityConverter.vb]
'  
' This file demonstrates how to create a converter that converts
' a boolean value to a Visibility value.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Windows
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class BooleanToVisibilityConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert

      If Not targetType.IsAssignableFrom(GetType(Visibility)) Then
        Throw New ArgumentException("The values can only be converted to a Visibility value.", "targetType")
      End If

      Dim visibilityWhenTrue As Visibility = Visibility.Hidden

      If TypeOf parameter Is String Then
        visibilityWhenTrue = CType(System.Enum.Parse(GetType(Visibility), CType(parameter, String), True), Visibility)
      End If

      If (Not value Is Nothing) And (CType(value, Boolean) = True) Then
        Return visibilityWhenTrue
      End If

      If visibilityWhenTrue = Visibility.Visible Then
        Return Visibility.Collapsed
      Else
        Return Visibility.Visible
      End If
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function
  End Class
End Namespace
