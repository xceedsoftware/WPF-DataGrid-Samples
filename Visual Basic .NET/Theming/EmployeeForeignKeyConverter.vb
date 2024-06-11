'
' Xceed DataGrid for WPF - SAMPLE CODE - Selection Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [EmployeeForeignKeyConverter.vb]
'
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.Theming
  Public Class EmployeeForeignKeyConverter
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

        'Return a value built in this order, so sorting is done on last name, then first name.
        Return dataRow("LastName") & ", " & dataRow("FirstName")
      End If

      Return key
    End Function
  End Class
End Namespace