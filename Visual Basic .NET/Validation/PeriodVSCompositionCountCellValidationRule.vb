'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [PeriodVSCompositionCountCellValidationRule.vb]
'  
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Xceed.Wpf.DataGrid.ValidationRules
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  Public Class PeriodVSCompositionCountCellValidationRule
    Inherits CellValidationRule
    Public Overloads Overrides Function Validate(ByVal value As Object, ByVal culture As System.Globalization.CultureInfo, ByVal context As CellValidationContext) As ValidationResult
      Dim parentRow As Row = context.Cell.ParentRow

      Dim period As Period
      Dim compositionCount As Integer

      If context.Cell.FieldName = "Period" Then
        period = DirectCast(value, Period)

        Dim compositionCountCellContent As Object = parentRow.Cells("CompositionCount").Content

        If compositionCountCellContent Is Nothing Then
          compositionCount = 0
        Else
          compositionCount = CInt(compositionCountCellContent)
        End If
      Else
        period = DirectCast(parentRow.Cells("Period").Content, Period)

        If value Is Nothing Then
          compositionCount = 0
        Else
          compositionCount = CInt(value)
        End If
      End If

      If (period = Period.Modern) AndAlso compositionCount > 49 Then
        Return New ValidationResult(False, "Composition count must be less than 50 when the period is set to Modern.")
      End If

      Return ValidationResult.ValidResult
    End Function
  End Class
End Namespace
