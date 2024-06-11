'
' * Xceed DataGrid for WPF - SAMPLE CODE - Column Chooser Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' * 
' * This class implements the Customized Column Chooser Control.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input

Namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
  ''' <summary>
  ''' Interaction logic for CustomizedColumnChooserControl.xaml
  ''' </summary>
  Partial Public Class CustomizedColumnChooserControl
	  Inherits UserControl
	Public Sub New()
	  InitializeComponent()
	End Sub

	Private Sub ShowHideButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
	  Dim button As Button = TryCast(sender, Button)
	  If button IsNot Nothing Then
		Dim col As ColumnBase = TryCast(button.DataContext, ColumnBase)
		If col IsNot Nothing Then
		  col.Visible = Not col.Visible
		End If
	  End If
	End Sub
  End Class
End Namespace