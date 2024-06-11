'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
 '*
 '* [UniqueIDCellValidationRule.cs]
 '*
 '* This file demonstrates how to create a validation rule for a Cell that
 '* ensures that the cell content is unique for the column.
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
Imports System.Windows.Controls
Imports System.Globalization

Imports Xceed.Wpf.DataGrid.ValidationRules
Imports System.Windows
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Class UniqueIDCellValidationRule
	  Inherits CellValidationRule
	Public Sub New()
	End Sub

	Public Overrides Function Validate(ByVal value As Object, ByVal culture As CultureInfo, ByVal context As CellValidationContext) As ValidationResult
	  ' Get the DataItem from the context and cast it to a DataRow
	  Dim dataRowView As DataRowView = TryCast(context.DataItem, DataRowView)

	  ' Convert the value to a long to make sure it is numerical.
	  ' When the value is not numerical, then an InvalidFormatException will be thrown.
	  ' We let it pass unhandled to demonstrate that an exception can be thrown when validating
	  ' and the grid will handle it nicely.
	  Dim id As Long = Convert.ToInt64(value, CultureInfo.CurrentCulture)

	  ' Try to find another row with the same ID
	  Dim existingRows As System.Data.DataRow() = dataRowView.Row.Table.Select(context.Cell.FieldName & "=" & id.ToString(CultureInfo.InvariantCulture))

	  ' If a row is found, we return an error
	  If (existingRows.Length <> 0) AndAlso (Not existingRows(0) Is dataRowView.Row) Then
		Return New ValidationResult(False, "The value must be unique")
	  End If

	  ' If no row was found, we return a ValidResult
	  Return ValidationResult.ValidResult
	End Function
  End Class
End Namespace
