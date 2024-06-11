'
' Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [BooleanToVisibilityConverter.vb]
'  
' This file demonstrates how to create a converter a boolean
' value to a Visibility value.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  Public Class BooleanToVisibilityConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert

      If Not targetType.IsAssignableFrom(GetType(Visibility)) Then
        Throw New ArgumentException("The values can only be converted to a Visibility value.", "targetType")
      End If

      If (value Is Nothing) Or (CType(value, Boolean) = False) Then
        Return Visibility.Collapsed
      End If

      Return Visibility.Visible
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function

  End Class
End Namespace
