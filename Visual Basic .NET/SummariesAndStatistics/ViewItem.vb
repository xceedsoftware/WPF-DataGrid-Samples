'
' Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [ViewItem.vb]
'
' This class represent a View that can be selected to change the look of the grid
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
  Public Class ViewItem
    Inherits DependencyObject
    Public Sub New()
    End Sub

    Public Sub New(ByVal description As String, ByVal viewType As Type)
      Me.New()
      m_description = description
      m_type = viewType
    End Sub

    Private m_type As Type

    ' Represents the Type to instantiate to have this view
    Public Property Type() As Type
      Get
        Return m_type
      End Get
      Set(ByVal value As Type)
        m_type = value
      End Set
    End Property

    Private m_description As String

    ' Represents the description for the view
    Public Property Description() As String
      Get
        Return m_description
      End Get
      Set(ByVal value As String)
        m_description = value
      End Set
    End Property
  End Class
End Namespace
