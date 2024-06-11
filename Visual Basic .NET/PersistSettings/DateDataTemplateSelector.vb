'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [DateDataTemplateSelector.vb]
'  
' This class defines a DataTemplateSelector for date values.
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

  Public Class DateDataTemplateSelector
    Inherits DataTemplateSelector

#Region "Singleton Property"

    Public Shared ReadOnly Property Singleton() As DateDataTemplateSelector
      Get
        If _singleton Is Nothing Then
          _singleton = New DateDataTemplateSelector()
        End If

        Return _singleton
      End Get
    End Property

    Private Shared _singleton As DateDataTemplateSelector

#End Region

#Region "PUBLIC METHODS"

    Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate

      Dim parentDataGrid As DataGridControl = DataGridControl.GetParentDataGridControl(container)

      If parentDataGrid Is Nothing Then
        Return MyBase.SelectTemplate(item, container)
      End If

      ' When the View is null, it is internally set to TableView.
      If (parentDataGrid.View Is Nothing) Or (TypeOf parentDataGrid.View Is Xceed.Wpf.DataGrid.Views.TableView) Then
        ' When the grid's view is TableView, we want to right-align the value.
        Return TryCast(parentDataGrid.FindResource("dateCellDataTemplateRightAligned"), DataTemplate)
      End If

      Return TryCast(parentDataGrid.FindResource("dateCellDataTemplateDefaultAligned"), DataTemplate)
    End Function

#End Region
  End Class
End Namespace
