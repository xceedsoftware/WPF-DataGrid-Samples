' 
' * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [BooleanInverterConverter.vb] 
' * 
' * This class implements a IValueConverter which inverts a boolean value. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' * 
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  Public Class BooleanInverterConverter
    Implements IValueConverter
#Region "IValueConverter Members"

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If (value Is Nothing) OrElse (Not (TypeOf value Is Boolean)) Then
        Return False
      End If

      Dim inverseValue As Boolean = Not CBool(value)

      Return inverseValue
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function
#End Region

  End Class

End Namespace
