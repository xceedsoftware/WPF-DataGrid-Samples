'
' Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [FormatItem.vb]
'  
' This class represents an item that will be used in the number formats
' ComboBox and the Date/time format ComboBox of the CSV exporter settings.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'

Namespace Xceed.Wpf.DataGrid.Samples.Exporting
  Public Class FormatItem
    Private m_description As String

    Public Property Description() As String
      Get
        Return m_description
      End Get
      Set(ByVal value As String)
        m_description = value
      End Set
    End Property

    Private m_format As String

    Public Property Format() As String
      Get
        Return m_format
      End Get
      Set(ByVal value As String)
        m_format = value
      End Set
    End Property
  End Class
End Namespace
