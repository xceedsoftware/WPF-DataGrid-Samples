'
' Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
'  Copyright (c) 2007-2024 Xceed Software Inc.
'  
'  [DataRowStyleSelector.vb]
'   
'  This class defines a StyleSelector for data rows.
'  It returns the appropriate Style depending of the grid's
'  current view.
'  
'  This file is part of the Xceed DataGrid for WPF product. The use 
'  and distribution of this Sample Code is subject to the terms 
'  and conditions referring to "Sample Code" that are specified in 
'  the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Controls
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.MultiView
  Public Class DataRowStyleSelector
    Inherits StyleSelector
#Region "Singleton Property"

    Public Shared ReadOnly Property Singleton() As DataRowStyleSelector
      Get
        If (_singleton Is Nothing) Then
          _singleton = New DataRowStyleSelector()
        End If

        Return _singleton
      End Get
    End Property

    Private Shared _singleton As DataRowStyleSelector

#End Region

#Region "PUBLIC METHODS"

    Public Overrides Function SelectStyle(ByVal item As Object, ByVal container As DependencyObject) As System.Windows.Style
      Dim dataGridContext As DataGridContext = DataGridControl.GetDataGridContext(container)

      Dim parentDataGrid As DataGridControl
      If (Not dataGridContext Is Nothing) Then
        parentDataGrid = dataGridContext.DataGridControl
      Else
        parentDataGrid = Nothing
      End If

      If (Not parentDataGrid Is Nothing) Then
        If ((parentDataGrid.View Is Nothing) Or (TypeOf (parentDataGrid.View) Is Xceed.Wpf.DataGrid.Views.TableView)) Then
          Return TryCast(parentDataGrid.FindResource("tableViewDataRowStyle"), Style)
        ElseIf ((TypeOf (parentDataGrid.View) Is Xceed.Wpf.DataGrid.Views.CardView) Or (TypeOf (parentDataGrid.View) Is Xceed.Wpf.DataGrid.Views.CompactCardView)) Then

          Return TryCast(parentDataGrid.FindResource("cardViewDataRowStyle"), Style)
        End If
      End If

      Return MyBase.SelectStyle(item, container)
    End Function

#End Region
  End Class
End Namespace
