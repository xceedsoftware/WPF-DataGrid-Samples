'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
 '*
 '* [MainPageParams.cs]
 '*
 '*
 '* This file is part of the Xceed DataGrid for WPF product. The use
 '* and distribution of this Sample Code is subject to the terms
 '* and conditions referring to "Sample Code" that are specified in
 '* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 '*
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Class MainPageParams
	  Implements INotifyPropertyChanged
	Public Shared ReadOnly Singleton As MainPageParams = New MainPageParams()

	#Region "CONSTRUCTORS"

	Private Sub New()
	End Sub

	#End Region ' CONSTRUCTORS

	#Region "AllowDetailToggle Property"

	Public Property AllowDetailToggle() As Boolean
	  Get
		Return _allowDetailToggle
	  End Get
	  Set
		If _allowDetailToggle = Value Then
		  Return
		End If

		_allowDetailToggle = Value
		Me.OnPropertyChanged(New PropertyChangedEventArgs("AllowDetailToggle"))
	  End Set
	End Property

	Private _allowDetailToggle As Boolean = True

	#End Region ' AllowDetailToggle Property

	#Region "UseCustomDetailHeader Property"

	Private _useCustomDetailHeader As Boolean

	Public Property UseCustomDetailHeader() As Boolean
	  Get
		Return _useCustomDetailHeader
	  End Get
	  Set
		If _useCustomDetailHeader = Value Then
		  Return
		End If

		_useCustomDetailHeader = Value
		Me.OnPropertyChanged(New PropertyChangedEventArgs("UseCustomDetailHeader"))
	  End Set
	End Property

	#End Region ' UseCustomDetailHeader Property

	#Region "INotifyPropertyChanged Members"

	Protected Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
	  If Not Me.PropertyChangedEvent Is Nothing Then
		RaiseEvent PropertyChanged(Me, e)
	  End If
	End Sub

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	#End Region
  End Class
End Namespace
