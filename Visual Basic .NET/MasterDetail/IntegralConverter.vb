'
'* Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [IntegralConverter.vb]
'
' This file demonstrates how to create a converter to display a long
' in a numerical formatting ( adding a thousand separator )
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
Imports System.Globalization

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  <ValueConversion(GetType(Long), GetType(String))> _
  Friend Class IntegralConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If (Not value Is Nothing) AndAlso ((Not Object.Equals(String.Empty, value))) Then
        Try
          ' Convert the string value provided by an editor to a long before formatting. 
          Dim tempLong As Long = System.Convert.ToInt64(value, CultureInfo.CurrentCulture)
          Return String.Format(CultureInfo.CurrentCulture, "{0:#,##0}", tempLong)
        Catch e1 As FormatException
        Catch e2 As OverflowException
        End Try
      End If

      Return String.Format(CultureInfo.CurrentCulture, "{0}", value)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Return Long.Parse(TryCast(value, String), NumberStyles.Integer, CultureInfo.CurrentCulture)
    End Function
  End Class
End Namespace
