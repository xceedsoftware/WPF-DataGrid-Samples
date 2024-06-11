'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [CustomComboBoxItem.vb]
'
' This file contains the classes used to populate various ComboBox of the
' application. They are used in ItemsSources.xaml and in the MainPage source code.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.CustomViews
  '
  ' This is the base class of all our ComboBoxItem classes.
  '
  Public Class CustomComboBoxItem
    Public Sub New()
    End Sub

    Public Sub New(ByVal description As String)
      m_description = description
    End Sub

    Private m_description As String

    Public Property Description() As String
      Get
        Return m_description
      End Get
      Set(ByVal value As String)
        m_description = value
      End Set
    End Property
  End Class

  '
  ' This class represents a ComboBoxItem holding a Pen object.
  '
  Public Class PenItem
    Inherits CustomComboBoxItem
    Private m_pen As Pen

    Public Property Pen() As Pen
      Get
        Return m_pen
      End Get
      Set(ByVal value As Pen)
        m_pen = value
      End Set
    End Property
  End Class

  '
  ' This class represents a ComboBoxItem holding a Glyph definition via a DataTemplate.
  '
  Public Class GlyphItem
    Inherits CustomComboBoxItem
    Private m_glyph As DataTemplate

    Public Property Glyph() As DataTemplate
      Get
        Return m_glyph
      End Get
      Set(ByVal value As DataTemplate)
        m_glyph = value
      End Set
    End Property
  End Class

  '
  ' This class represents a ComboBoxItem holding values used to define a vertical and
  ' an horizontal GridLine.
  '
  Public Class GridLinesItem
    Inherits CustomComboBoxItem
    Private m_verticalBrush As Brush

    Public Property VerticalBrush() As Brush
      Get
        Return m_verticalBrush
      End Get
      Set(ByVal value As Brush)
        m_verticalBrush = value
      End Set
    End Property

    Private m_horizontalBrush As Brush

    Public Property HorizontalBrush() As Brush
      Get
        Return m_horizontalBrush
      End Get
      Set(ByVal value As Brush)
        m_horizontalBrush = value
      End Set
    End Property

    Private m_verticalThickness As Double

    Public Property VerticalThickness() As Double
      Get
        Return m_verticalThickness
      End Get
      Set(ByVal value As Double)
        m_verticalThickness = value
      End Set
    End Property

    Private m_horizontalThickness As Double

    Public Property HorizontalThickness() As Double
      Get
        Return m_horizontalThickness
      End Get
      Set(ByVal value As Double)
        m_horizontalThickness = value
      End Set
    End Property
  End Class
End Namespace
