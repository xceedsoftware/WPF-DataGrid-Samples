'
' * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [SolidColorBrushToColorConverter.vb]
' *  
' * This class is used to get the Color of a SolidColorBrush.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Media
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
    Public Class SolidColorBrushToColorConverter
        Implements IValueConverter
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim brush As SolidColorBrush = TryCast(value, SolidColorBrush)
            If brush IsNot Nothing Then
                Return brush.Color
            End If

            Return Nothing
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If value IsNot Nothing Then
                Dim color As Color = CType(value, Color)
                Return New SolidColorBrush(color)
            End If

            Return Nothing
        End Function
    End Class
End Namespace