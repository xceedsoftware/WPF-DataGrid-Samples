'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [UpdateTriggerItem.vb]
'  
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
' 

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  Public Class UpdateTriggerItem
    Public Sub New()
    End Sub

    Public Property Description() As String
      Get
        Return m_description
      End Get

      Set(ByVal value As String)
        m_description = value
      End Set
    End Property

    Public Property ToolTip() As String
      Get
        Return m_toolTip
      End Get

      Set(ByVal value As String)
        m_toolTip = value
      End Set
    End Property

    Public Property DataGridUpdateSourceTrigger() As DataGridUpdateSourceTrigger
      Get
        Return m_dataGridUpdateSourceTrigger
      End Get

      Set(ByVal value As DataGridUpdateSourceTrigger)
        m_dataGridUpdateSourceTrigger = value
      End Set
    End Property

    Private m_description As String
    Private m_toolTip As String
    Private m_dataGridUpdateSourceTrigger As DataGridUpdateSourceTrigger
  End Class
End Namespace
