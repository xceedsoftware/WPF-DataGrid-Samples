'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [IsView3DTypeConverter.vb]
' * 
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
Imports System.ComponentModel
Imports System.Windows.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
  Public Class IsView3DTypeConverter
	  Implements IValueConverter
	#Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If (value Is Nothing) OrElse (value.GetType() IsNot GetType(Boolean)) Then
                Return DependencyProperty.UnsetValue
            End If

            Dim isView3D As Boolean = CBool(value)

            If isView3D Then
                Return "3D Views"
            Else
                Return "2D Views"
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Binding.DoNothing
        End Function

	#End Region
  End Class
End Namespace