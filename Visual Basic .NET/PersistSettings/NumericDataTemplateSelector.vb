'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [NumericDataTemplateSelector.vb]
'  
' This class defines a DataTemplateSelector for numeric values.
' It returns the appropriate DataTemplate depending of the grid's
' current view.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Controls
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings

  Public Class NumericDataTemplateSelector
    Inherits DataTemplateSelector
#Region "Singleton Property"

    Public Shared ReadOnly Property Singleton() As NumericDataTemplateSelector
      Get
        If _singleton Is Nothing Then
          _singleton = New NumericDataTemplateSelector()
        End If

        Return _singleton
      End Get
    End Property

    Private Shared _singleton As NumericDataTemplateSelector

#End Region

#Region "PUBLIC METHODS"

    Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As System.Windows.DataTemplate
      Dim parentDataGrid As DataGridControl = DataGridControl.GetParentDataGridControl(container)

      If Not parentDataGrid Is Nothing Then
        ' When the View is null, it is internally set to TableView.
        If (parentDataGrid.View Is Nothing) Or (TypeOf parentDataGrid.View Is Xceed.Wpf.DataGrid.Views.TableView) Then
          ' When the grid's view is TableView, we want to right-align the value.
          Return TryCast(parentDataGrid.FindResource("numericCellDataTemplate"), DataTemplate)
        End If
      End If

      Return MyBase.SelectTemplate(item, container)
    End Function

#End Region
  End Class
End Namespace
