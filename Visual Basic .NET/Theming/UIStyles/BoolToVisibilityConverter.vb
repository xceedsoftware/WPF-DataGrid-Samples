'
' * Xceed DataGrid for WPF - SAMPLE CODE - Selection Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [BoolToVisibilityConverter.vb]
' * 
' * This is the converter from bool to Visibility.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports System.Windows
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
  Public Class BoolToVisibilityConverter
	  Implements IValueConverter
	#Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If (value Is Nothing) OrElse (value.GetType() IsNot GetType(Boolean)) Then
                Return DependencyProperty.UnsetValue
            End If

            Dim boolValue As Boolean = CBool(value)

            Dim isReversed As Boolean = False
            Dim stringParameter As String = CStr(parameter)
            If (Not String.IsNullOrEmpty(stringParameter)) Then
                isReversed = True
            End If

            If (boolValue AndAlso (Not isReversed)) OrElse ((Not boolValue) AndAlso isReversed) Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            If (value Is Nothing) OrElse (value.GetType() IsNot GetType(Visibility)) Then
                Return DependencyProperty.UnsetValue
            End If

            Dim visibility As Visibility = CType(value, Visibility)

            Select Case visibility
                Case visibility.Collapsed, visibility.Hidden
                    Return False

                Case Else
                    Return True
            End Select
        End Function

	#End Region
  End Class
End Namespace