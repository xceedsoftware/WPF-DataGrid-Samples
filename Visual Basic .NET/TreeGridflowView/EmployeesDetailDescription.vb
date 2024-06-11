'
' * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [EmployeesDetailDescription.cs]
' * 
' * Custom detail description that returns the appropriate details for the
' * parent item received in the GetDetailsForParentItem override.
' *  
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
  Public Class EmployeesDetailDescription
	  Inherits DataGridDetailDescription
	Public Sub New()
	  Me.RelationName = "SubEmployees"
	End Sub

	Protected Overrides Function GetDetailsForParentItem(ByVal parentCollectionViewBase As DataGridCollectionViewBase, ByVal parentItem As Object) As IEnumerable
	  Dim parentRowView As DataRowView = CType(parentItem, DataRowView)

	  If parentRowView Is Nothing Then
		Return Nothing
	  End If

	  Dim employeeID As Integer = CInt(Fix(parentRowView("EmployeeID")))

	  Dim dataView As New DataView(parentRowView.Row.Table, "ReportsTo = " & employeeID.ToString(), Nothing, DataViewRowState.CurrentRows)

	  Return dataView
	End Function
  End Class
End Namespace
