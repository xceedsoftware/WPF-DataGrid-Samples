'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
 '*
 '* [BooleanToVisibilityConverter.cs]
 '*
 '* This file demonstrates how to create a converter from a boolean
 '* value to a Visibility value.
 '*
 '* This file is part of the Xceed DataGrid for WPF product. The use
 '* and distribution of this Sample Code is subject to the terms
 '* and conditions referring to "Sample Code" that are specified in
 '* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 '*
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Class BooleanToVisibilityConverter
	  Implements IValueConverter
	Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
	  If (Not targetType.IsAssignableFrom(GetType(Visibility))) Then
		Return DependencyProperty.UnsetValue
	  End If

	  If value Is Nothing OrElse CBool(value) = False Then
		Return Visibility.Collapsed
	  End If

	  Return Visibility.Visible
	End Function

	Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
	  Return Binding.DoNothing
	End Function
  End Class
End Namespace
