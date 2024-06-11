'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ColumnWidthValidationRule.vb]
'  
' This class implements the ColumnWidthValidationRule class which validates values
' assigned to a ColumnWidth property. It will be used by the ColumnPropertyCell
' class CellEditor.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class ColumnWidthValidationRule
    Inherits ValidationRule

    Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As System.Windows.Controls.ValidationResult
      If value Is Nothing Then
        Return New ValidationResult(False, "Null is not a valid value.")
      End If

      If Not s_columnWidthConverter.CanConvertFrom(value.GetType()) Then
        Return New ValidationResult(False, "Cannot convert from type " + value.GetType().Name + ".")
      End If

      Try
        s_columnWidthConverter.ConvertFrom(Nothing, cultureInfo, value)
      Catch fex As FormatException
        Return New ValidationResult(False, "Invalid value.")
      Catch oex As OverflowException
        Return New ValidationResult(False, "The provided value was outside of the accepted range.")
      Catch ex As Exception
        Return New ValidationResult(False, ex.Message)
      End Try

      Return New ValidationResult(True, Nothing)
    End Function

    Private Shared s_columnWidthConverter As Converters.ColumnWidthConverter = New Converters.ColumnWidthConverter()
  End Class
End Namespace
