'
' Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CustomSpannedCellSelector.vb]
'
' This class implements the business object that defines which cells could be merged.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Namespace Xceed.Wpf.DataGrid.Samples.SpannedCells

  Public Class CustomSpannedCellSelector
    Inherits SpannedCellSelector

    Public Sub New()
      m_comparer = EqualityComparer(Of Object).Default
    End Sub

    Public Overrides Function CanMerge(x As SpannedCellFragment, y As SpannedCellFragment) As Boolean
      If (CustomSpannedCellSelector.IsCountryColumn(x.Column) AndAlso CustomSpannedCellSelector.IsCityColumn(y.Column)) _
        OrElse (CustomSpannedCellSelector.IsCityColumn(x.Column) AndAlso CustomSpannedCellSelector.IsCountryColumn(y.Column)) Then
        Return True
      Else
        Return m_comparer.Equals(x.Content, y.Content)
      End If

    End Function

    Public Overrides Function SelectContent(ByVal fragments As IEnumerable(Of SpannedCellFragment)) As Object
      Dim items = fragments.ToList()

      If (items.Count >= 2) Then
        Dim countryFragment As SpannedCellFragment = Nothing
        Dim cityFragment As SpannedCellFragment = Nothing

        For Each fragment In fragments
          If ((countryFragment IsNot Nothing) AndAlso (cityFragment IsNot Nothing)) Then
            Exit For
          End If

          If ((countryFragment Is Nothing) AndAlso (CustomSpannedCellSelector.IsCountryColumn(fragment.Column))) Then
            countryFragment = fragment
          End If

          If ((cityFragment Is Nothing) AndAlso (CustomSpannedCellSelector.IsCityColumn(fragment.Column))) Then
            cityFragment = fragment
          End If
        Next

        If ((countryFragment IsNot Nothing) AndAlso (cityFragment IsNot Nothing)) Then
          Return New CountryCityData(countryFragment.Content, cityFragment.Content)
        End If
      End If

      If (items.Count > 0) Then
        Return items(0).Content
      End If

      Return MyBase.SelectContent(fragments)
    End Function

    Private Shared Function IsCountryColumn(ByVal column As ColumnBase)
      Return (column IsNot Nothing) AndAlso (column.FieldName = "ShipCountry")
    End Function

    Private Shared Function IsCityColumn(ByVal column As ColumnBase)
      Return (column IsNot Nothing) AndAlso (column.FieldName = "ShipCity")
    End Function

    Private ReadOnly m_comparer As EqualityComparer(Of Object)
  End Class
End Namespace