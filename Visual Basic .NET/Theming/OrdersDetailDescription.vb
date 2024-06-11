'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [OrdersDetailDescription.vb]
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

Imports System.Collections
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.Theming
  Public Class OrdersDetailDescription
	  Inherits DataGridDetailDescription
	Public Sub New()
	  Me.RelationName = "RelatedCustomers"
	End Sub

	Protected Overrides Function GetDetailsForParentItem(ByVal parentCollectionViewBase As DataGridCollectionViewBase, ByVal parentItem As Object) As IEnumerable
	  Dim parentRowView As DataRowView = CType(parentItem, DataRowView)

	  If parentRowView Is Nothing Then
		Return Nothing
	  End If

	  Dim CustomerID As String = CStr(parentRowView("CustomerID"))

	  Dim dataView As New DataView(parentRowView.Row.Table, "CustomerID = " & "'" & CustomerID & "'", Nothing, DataViewRowState.CurrentRows)

	  Return dataView
	End Function
  End Class
End Namespace