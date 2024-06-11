'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ColumnChooserSortOrderConverter.vb]
'  
' This class implements the ColumnChooserSortOrderConverter
' class which translate the ColumnChooserSortOrder enum values to a displayable
' description.
' 
' This class will be used in the ColumnPropertyCell class.
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
Imports System.Windows.Data
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement

  <ValueConversion(GetType(ColumnChooserSortOrder), GetType(String))> _
  Public Class ColumnChooserSortOrderConverter
    Implements IValueConverter
#Region "Singleton Property"

    Public Shared ReadOnly Property Singleton() As ColumnChooserSortOrderConverter
      Get
        If _singleton Is Nothing Then
          _singleton = New ColumnChooserSortOrderConverter()
        End If

        Return _singleton
      End Get
    End Property

    Private Shared _singleton As ColumnChooserSortOrderConverter

#End Region

#Region "IValueConverter Members"

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If value Is Nothing Then
        Return String.Empty
      End If

      Dim sortOrder As ColumnChooserSortOrder = CType(value, ColumnChooserSortOrder)

      Select Case sortOrder
        Case ColumnChooserSortOrder.None
          Return "None"

        Case ColumnChooserSortOrder.TitleAscending
          Return "Title (ascending)"

        Case ColumnChooserSortOrder.TitleDescending
          Return "Title (descending)"

        Case ColumnChooserSortOrder.VisiblePosition
          Return "Visible position"

        Case Else
          Return String.Empty
      End Select
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function

#End Region
  End Class
End Namespace
