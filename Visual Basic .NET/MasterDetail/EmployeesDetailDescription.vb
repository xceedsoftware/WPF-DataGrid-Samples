'
' Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [EmployeesDetailDescription.vb]
'  
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Xceed.Wpf.DataGrid
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  Public Class EmployeesDetailDescription
    Inherits DataGridDetailDescription

    Public Sub New()
      Me.RelationName = "SubEmployees"
    End Sub

    Protected Overrides Function GetDetailsForParentItem(ByVal parentCollectionView As DataGridCollectionViewBase, ByVal parentItem As Object) As System.Collections.IEnumerable
      Dim parentRowView As System.Data.DataRowView = CType(parentItem, System.Data.DataRowView)

      If parentRowView Is Nothing Then
        Return Nothing
      End If

      Dim employeeID As Integer = CType(parentRowView.Item("EmployeeID"), Integer)

      Dim dataView As DataView = New DataView( _
        parentRowView.Row.Table, _
        "ReportsTo = " + employeeID.ToString(), _
        Nothing, _
        DataViewRowState.CurrentRows)

      Return dataView
    End Function
  End Class
End Namespace
