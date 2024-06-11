'
' * Xceed DataGrid for WPF - SAMPLE CODE - Selection™ View Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' * 
' * This class implements the various dynamic configuration options offered 
' * by the configuration panel declared in MainPage.xaml.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Selection
  Partial Public Class MainPage
	  Inherits Page
	Public Sub New()
	  InitializeComponent()
	  AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf ConfigurationData_PropertyChanged
	  Me.EnableDragSelectionChanged()
	  Me.UpdateGroupSelectionEnabled()
	End Sub

	Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
	  Select Case e.PropertyName
		Case "NavigationBehavior"
		  Me.NavigationBehaviorChanged()

		Case "EnableDragSelection"
		  Me.EnableDragSelectionChanged()

		Case "SelectionMode", "SelectionUnit"
		  Me.UpdateGroupSelectionEnabled()
	  End Select
	End Sub

	Private Sub NavigationBehaviorChanged()
	  If ConfigurationData.Singleton.NavigationBehavior = NavigationBehavior.None Then
		Me.grid.CurrentItem = Nothing
		Me.grid.CurrentColumn = Nothing
		Me.grid.SelectedItems.Clear()
	  End If
	End Sub

	Private Sub EnableDragSelectionChanged()
	  If ConfigurationData.Singleton.EnableDragSelection Then
		grid.AllowDrag = True
		grid.DragBehavior = DataGridDragBehavior.Select
	  Else
		grid.AllowDrag = False
	  End If
	End Sub

	Private Sub UpdateGroupSelectionEnabled()
	  If ConfigurationData.Singleton.SelectionMode = SelectionMode.Single OrElse ConfigurationData.Singleton.SelectionUnit = SelectionUnit.Cell Then
		ConfigurationData.Singleton.EnableIsGroupSelectionEnabled = False
		ConfigurationData.Singleton.IsGroupSelectionEnabled = False
	  Else
		ConfigurationData.Singleton.EnableIsGroupSelectionEnabled = True
	  End If
	End Sub
  End Class
End Namespace