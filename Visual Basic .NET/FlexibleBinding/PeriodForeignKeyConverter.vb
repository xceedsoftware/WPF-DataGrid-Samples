'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [PeriodForeignKeyConverter.vb]
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

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  Public Class PeriodForeignKeyConverter
    Inherits ForeignKeyConverter
    Public Overrides Function GetValueFromKey(ByVal key As Object, ByVal description As DataGridForeignKeyDescription) As Object
      Return System.Enum.GetName(GetType(Period), key)
    End Function
  End Class
End Namespace