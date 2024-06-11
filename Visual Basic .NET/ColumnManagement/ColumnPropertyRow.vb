'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ColumnPropertyRow.vb]
'  
' This class implements the ColumnPropertyRow class, which allows its cells to
' edit any numeric value of their sibling cells contained in the same column.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class ColumnPropertyRow
    Inherits Row

#Region "ColumnProperty Property"

    ' This property gets or sets a Column's DependencyProperty that will be 
    ' displayed and edited in the row's cells.
    Public Shared ReadOnly ColumnPropertyProperty As DependencyProperty = DependencyProperty.Register( _
      "ColumnProperty", _
      GetType(DependencyProperty), _
      GetType(ColumnPropertyRow), _
      New UIPropertyMetadata(Nothing), _
      New ValidateValueCallback(AddressOf ColumnPropertyRow.ValidateColumnPropertyCallback))

    Public Property ColumnProperty() As DependencyProperty
      Get
        Return CType(Me.GetValue(ColumnPropertyRow.ColumnPropertyProperty), DependencyProperty)
      End Get

      Set(ByVal value As DependencyProperty)
        Me.SetValue(ColumnPropertyRow.ColumnPropertyProperty, value)
      End Set
    End Property

    ' Only a dependency property of type double or ColumnWidth is accepted.
    Private Shared Function ValidateColumnPropertyCallback(ByVal value As Object) As Boolean
      If value Is Nothing Then
        Return True
      End If

      Dim dp As DependencyProperty = TryCast(value, DependencyProperty)

      If dp Is Nothing Then
        Return False
      End If

      Return (dp.PropertyType Is GetType(Double)) Or (dp.PropertyType Is GetType(ColumnWidth))
    End Function

#End Region

    Protected Overrides Function CreateCell(ByVal column As ColumnBase) As Cell
      Return New ColumnPropertyCell()
    End Function

    Protected Overrides Function IsValidCellType(ByVal cell As Cell) As Boolean
      Return TypeOf cell Is ColumnPropertyCell
    End Function
  End Class
End Namespace
