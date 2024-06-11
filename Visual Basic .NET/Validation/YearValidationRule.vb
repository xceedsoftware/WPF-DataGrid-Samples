'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [YearValidationRule.vb]
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
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  Public Class YearValidationRule
    Inherits ValidationRule
    Public Overloads Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult

      If value Is Nothing Then
        Return New ValidationResult(False, "Chosen year cannot be null.")
      End If

      Dim year As Integer = CInt(value)

      If year > DateTime.Now.Year Then
        Return New ValidationResult(False, "Chosen year cannot be greater than this year.")
      End If

      Return ValidationResult.ValidResult
    End Function
  End Class
End Namespace
