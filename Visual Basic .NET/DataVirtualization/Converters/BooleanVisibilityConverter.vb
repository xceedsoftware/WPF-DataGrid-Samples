' 
' * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [BooleanVisibilityConverter.vb] 
' * 
' * This class implements a IValueConverter which converts 
' * a boolean value into a Visibility. 
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
  Public Class BooleanVisibilityConverter
    Implements IValueConverter
#Region "IValueConverter Members"

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If (value Is Nothing) OrElse (Not (TypeOf value Is Boolean)) Then
        Return Binding.DoNothing
      End If

      Dim elementVisibility As Visibility
      Dim shouldCollapse As Boolean = CBool(value)

      If parameter IsNot Nothing Then
        Try
          Dim inverse As Boolean = System.Convert.ToBoolean(parameter)

          If inverse Then
            shouldCollapse = Not shouldCollapse
          End If
        Catch
        End Try
      End If

      elementVisibility = If(shouldCollapse, Visibility.Collapsed, Visibility.Visible)
      Return elementVisibility
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
      Throw New NotImplementedException()
    End Function

#End Region

    End Class 

End Namespace
