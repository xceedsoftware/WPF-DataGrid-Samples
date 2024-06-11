' 
' * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [EqualityConverter.cs] 
' * 
' * This class implements a IValueConverter which converts 
' * a value and a parameter into a boolean value corresponding to their 
' * equality. 
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
  Public Class EqualityConverter
    Implements IValueConverter

#Region "IValueConverter Members"

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert

      If (value Is Nothing) OrElse (Not (TypeOf value Is Integer)) Then
        Return Binding.DoNothing
      End If



      If parameter Is Nothing Then
        Return Binding.DoNothing
      End If



      Try
        Dim num1 As Integer = System.Convert.ToInt32(value)
        Dim num2 As Integer = System.Convert.ToInt32(parameter)

        Return Int32.Equals(num1, num2)
      Catch
        Return Binding.DoNothing
      End Try
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack

      If Not (TypeOf value Is Boolean) Then
        Return Binding.DoNothing
      End If

      If parameter Is Nothing Then
        Return Binding.DoNothing
      End If



      Dim isChecked As Boolean = CBool(value)

      If Not isChecked Then
        Return Binding.DoNothing
      End If

      Dim returnedNum As Integer
      Try
        returnedNum = System.Convert.ToInt32(parameter)

        Return returnedNum
      Catch
        Return Binding.DoNothing
      End Try
    End Function

#End Region

    End Class 

End Namespace
