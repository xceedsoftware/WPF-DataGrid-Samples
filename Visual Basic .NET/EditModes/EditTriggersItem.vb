'
' Xceed DataGrid for WPF - SAMPLE CODE - Edit Modes Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [EditTriggersItem.vb]
'  
' This class represents a ComboBoxItem holding a EditTriggers object.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.EditModes
  Public Class EditTriggersItem

    Public Sub New()
    End Sub

    Public Sub New(ByVal editTriggers As EditTriggers, ByVal description As String, ByVal isChecked As Boolean)
      m_editTriggers = editTriggers
      m_description = description
      m_isChecked = isChecked
    End Sub

    Private m_editTriggers As EditTriggers

    Public Property EditTriggers() As EditTriggers
      Get

        Return m_editTriggers
      End Get
      Set(ByVal value As EditTriggers)
        m_editTriggers = value
      End Set
    End Property

    Private m_description As String

    Public Property Description() As String
      Get
        Return m_description
      End Get
      Set(ByVal value As String)
        m_description = value
      End Set
    End Property

    Private m_toolTip As String

    Public Property ToolTip() As String
      Get
        Return m_toolTip
      End Get
      Set(ByVal value As String)
        m_toolTip = value
      End Set
    End Property

    Private m_isChecked As Boolean

    Public Property IsChecked() As Boolean
      Get
        Return m_isChecked
      End Get
      Set(ByVal value As Boolean)
        m_isChecked = value
      End Set
    End Property
  End Class
End Namespace
