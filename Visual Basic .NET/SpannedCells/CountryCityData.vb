'
' Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CountryCityData.vb]
'
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Namespace Xceed.Wpf.DataGrid.Samples.SpannedCells

  Public Class CountryCityData
    Public Sub New(ByVal country As Object, ByVal city As Object)
      m_country = country
      m_city = city
    End Sub

    Public ReadOnly Property Country() As Object
      Get
        Return m_country
      End Get
    End Property

    Public ReadOnly Property City() As Object
      Get
        Return m_city
      End Get
    End Property

    Private ReadOnly m_country As Object
    Private ReadOnly m_city As Object
  End Class
End Namespace
