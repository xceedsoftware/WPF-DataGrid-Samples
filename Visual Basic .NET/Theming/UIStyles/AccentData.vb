'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [AccentData.vb]
' * 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports System.Windows
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
	Public Class AccentData
		Inherits DependencyObject

	#Region "CONSTRUCTORS"

	Public Sub New()
	End Sub

	#End Region ' CONSTRUCTORS

	#Region "ImageDataTemplate Property"

	Public Shared ReadOnly ImageDataTemplateProperty As DependencyProperty = DependencyProperty.Register("ImageDataTemplate", GetType(DataTemplate), GetType(AccentData))

	Public Property ImageDataTemplate() As DataTemplate
		Get
			Return CType(Me.GetValue(AccentData.ImageDataTemplateProperty), DataTemplate)
		End Get
		Set(ByVal value As DataTemplate)
			Me.SetValue(AccentData.ImageDataTemplateProperty, value)
		End Set
	End Property

	#End Region ' ImageDataTemplate Property

	#Region "Description Property"

	Public Shared ReadOnly DescriptionProperty As DependencyProperty = DependencyProperty.Register("Description", GetType(String), GetType(AccentData))

	Public Property Description() As String
		Get
			Return CStr(Me.GetValue(AccentData.DescriptionProperty))
		End Get
		Set(ByVal value As String)
			Me.SetValue(AccentData.DescriptionProperty, value)
		End Set
	End Property

	#End Region ' Description Property

	#Region "Accent Property"

	Public Shared ReadOnly ThemeProperty As DependencyProperty = DependencyProperty.Register("Accent", GetType(String), GetType(AccentData))

	Public Property Accent() As String
		Get
			Return CStr(Me.GetValue(AccentData.ThemeProperty))
		End Get
		Set(ByVal value As String)
			Me.SetValue(AccentData.ThemeProperty, value)
		End Set
	End Property

	#End Region ' Accent Property

	#Region "IsNewTheme Property"

	Public Shared ReadOnly IsNewThemeProperty As DependencyProperty = DependencyProperty.Register("IsNewTheme", GetType(Boolean), GetType(AccentData), New FrameworkPropertyMetadata(CBool(False)))

	Public Property IsNewTheme() As Boolean
		Get
			Return CBool(Me.GetValue(AccentData.IsNewThemeProperty))
		End Get
		Set(ByVal value As Boolean)
			Me.SetValue(AccentData.IsNewThemeProperty, value)
		End Set
	End Property

	#End Region

	End Class
End Namespace