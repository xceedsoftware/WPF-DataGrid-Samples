'
' * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' *  
' * This sample demonstrates asynchronous binding, which provides a simple way
'   of displaying data until synchronization is done. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports Xceed.Wpf.DataGrid
Imports System.Threading
Imports System.Diagnostics
Imports System.Collections.Specialized
Imports System.Windows.Threading
Imports System.Windows.Media.Animation

Namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
  Partial Public Class MainPage
	  Inherits Page
	#Region "Constructors"

	Public Sub New()
	  ' Set the window's DataContext to itself to allow the grid to bind directly to the
	  ' DataContext rather than using an RelativeSource binding to find the window.
	  Me.DataContext = Me

	  InitializeComponent()
	  AddHandler Loaded, AddressOf MainPage_Loaded
	End Sub

	#End Region

	#Region "Persons Property"

	' Property that provides the Person data.
	Public Property Persons() As DataGridCollectionView
	  Get
		Return CType(GetValue(PersonsProperty), DataGridCollectionView)
	  End Get
	  Set(ByVal value As DataGridCollectionView)
		SetValue(PersonsProperty, value)
	  End Set
	End Property

	' Using a DependencyProperty as the backing store for Persons.  This enables animation, styling, binding, etc...
	Public Shared ReadOnly PersonsProperty As DependencyProperty = DependencyProperty.Register("Persons", GetType(DataGridCollectionView), GetType(MainPage), New UIPropertyMetadata(Nothing))

	#End Region

	#Region "Private Methods"

	' Create the new collection of data items
	Private Sub CreateDataItems()
	  Dim newSource As New PersonObservableCollection()

	  For i As Double = 0 To 999
		newSource.Add(New Person())
	  Next i

	  Me.Persons = New DataGridCollectionView(newSource)
	End Sub

	#End Region

	#Region "Events Handler"

	Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
	  ' Create the initial data items.
	  Me.CreateDataItems()
	End Sub

	Private Sub OnReloadDataParametersClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
	  ' Re-create the data items.
	  Me.CreateDataItems()
	End Sub

	#End Region
  End Class
End Namespace