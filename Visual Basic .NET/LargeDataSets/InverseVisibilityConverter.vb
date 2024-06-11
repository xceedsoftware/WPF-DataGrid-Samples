'
' Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [InverseVisibilityConverter.vb]
'  
' Converter that converts a Hidden or Collapsed Visibility value to Visible
' and vice-versa.
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

Namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
  Public Class InverseVisibilityConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If ((Not targetType.IsAssignableFrom(GetType(Visibility))) And _
          (Not value.GetType().IsAssignableFrom(GetType(Visibility)))) Then

        Return DependencyProperty.UnsetValue
      End If

      Dim newValue As Visibility = CType(value, Visibility)

      Select Case newValue
        Case Visibility.Collapsed
        Case Visibility.Hidden
          Return Visibility.Visible

        Case Visibility.Visible
          Return Visibility.Hidden

      End Select

      Return newValue
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function
  End Class
End Namespace
