'
' Xceed DataGrid for WPF - SAMPLE CODE - Selection Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CustomerConverter.vb]
'
' This file demonstrates how to create a converter to display a company name
' followed by an optional contact name between parentheses.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Data
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Selection
  Public Class CustomerConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If value Is Nothing Then
        Return Nothing
      End If

      'By default, the parameter will contain the ItemsSource define on the ForeignKeyConfiguration when this converter is set as a ForeignKeyValueConverter.
      Dim customers As DataView = TryCast(parameter, DataView)
      If customers Is Nothing Then
        Return Nothing
      End If

      customers.Sort = "CustomerID"

      Dim index As Integer = customers.Find(value)
      Dim dataRow As DataRowView = customers(index)

      Return dataRow("CompanyName") & " - " & dataRow("ContactName")
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      Return Nothing
    End Function
  End Class

  Public Class CustomerMultiConverter
    Implements IMultiValueConverter

    Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
      If (values(0) Is DependencyProperty.UnsetValue) Then
        values(0) = String.Empty
      End If

      If (values(1) Is DependencyProperty.UnsetValue) Then
        values(1) = String.Empty
      End If

      Dim name As String = CType(values(0), String)

      If Not String.IsNullOrEmpty(values(1)) Then
        name = name + " - " + CType(values(1), String)
      End If

      Return name
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function

  End Class

  Public Class CustomerForeignKeyConverter
    Inherits DataTableForeignKeyConverter
    'Only the GetValueFormKey method overload with the DataGridForeignKeyDescription parameter needs to be overridden,
    'for the base class DataTableForeignKeyConverter already provides an implementation of the other two overridable methods.
    Public Overrides Function GetValueFromKey(ByVal key As Object, ByVal description As DataGridForeignKeyDescription) As Object
      If key Is Nothing Then
        Return Nothing
      End If

      Dim dataView As DataView = TryCast(description.ItemsSource, DataView)
      If dataView IsNot Nothing Then
        dataView.Sort = description.ValuePath

        Dim index As Integer = dataView.Find(key)
        Dim dataRow As DataRowView = dataView(index)

        'Return a value built in this order, so sorting is done on company name, then contact name.
        Return dataRow("CompanyName") & " - " & dataRow("ContactName")
      End If

      Return key
    End Function
  End Class
End Namespace
