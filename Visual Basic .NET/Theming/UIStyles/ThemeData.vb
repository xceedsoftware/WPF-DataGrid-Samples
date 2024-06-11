'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ThemeData.vb]
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
  Public Class ThemeData
	  Inherits DependencyObject
	#Region "CONSTRUCTORS"

	Public Sub New()
	End Sub

	#End Region ' CONSTRUCTORS

	#Region "ImageDataTemplate Property"

	Public Shared ReadOnly ImageDataTemplateProperty As DependencyProperty = DependencyProperty.Register("ImageDataTemplate", GetType(DataTemplate), GetType(ThemeData))

	Public Property ImageDataTemplate() As DataTemplate
	  Get
		Return CType(Me.GetValue(ThemeData.ImageDataTemplateProperty), DataTemplate)
	  End Get
	  Set(ByVal value As DataTemplate)
		Me.SetValue(ThemeData.ImageDataTemplateProperty, value)
	  End Set
	End Property

	#End Region ' ImageDataTemplate Property

	#Region "Description Property"

	Public Shared ReadOnly DescriptionProperty As DependencyProperty = DependencyProperty.Register("Description", GetType(String), GetType(ThemeData))

	Public Property Description() As String
	  Get
		Return CStr(Me.GetValue(ThemeData.DescriptionProperty))
	  End Get
	  Set(ByVal value As String)
		Me.SetValue(ThemeData.DescriptionProperty, value)
	  End Set
	End Property

	#End Region ' Description Property

	#Region "Theme Property"

	Public Shared ReadOnly ThemeProperty As DependencyProperty = DependencyProperty.Register("Theme", GetType(Theme), GetType(ThemeData))

	Public Property Theme() As Theme
	  Get
		Return CType(Me.GetValue(ThemeData.ThemeProperty), Theme)
	  End Get
	  Set(ByVal value As Theme)
		Me.SetValue(ThemeData.ThemeProperty, value)
	  End Set
	End Property

	#End Region ' Theme Property

	#Region "ThemeGroup Property"

	Public Shared ReadOnly ThemeGroupProperty As DependencyProperty = DependencyProperty.Register("ThemeGroup", GetType(String), GetType(ThemeData))

	Public Property ThemeGroup() As String
	  Get
		Return CStr(Me.GetValue(ThemeData.ThemeGroupProperty))
	  End Get
	  Set(ByVal value As String)
		Me.SetValue(ThemeData.ThemeGroupProperty, value)
	  End Set
	End Property

	#End Region ' ThemeGroup Property

	#Region "IsTheme3D Property"

	Public Shared ReadOnly IsTheme3DProperty As DependencyProperty = DependencyProperty.Register("IsTheme3D", GetType(Boolean), GetType(ThemeData))

	Public Property IsTheme3D() As Boolean
	  Get
		Return CBool(Me.GetValue(ThemeData.IsTheme3DProperty))
	  End Get
	  Set(ByVal value As Boolean)
		Me.SetValue(ThemeData.IsTheme3DProperty, value)
	  End Set
	End Property

	#End Region ' IsTheme3D Property

	#Region "UseSystemTheme Property"

	Public Shared ReadOnly UseSystemThemeProperty As DependencyProperty = DependencyProperty.Register("UseSystemTheme", GetType(Boolean), GetType(ThemeData))

	Public Property UseSystemTheme() As Boolean
	  Get
		Return CBool(Me.GetValue(ThemeData.UseSystemThemeProperty))
	  End Get
	  Set(ByVal value As Boolean)
		Me.SetValue(ThemeData.UseSystemThemeProperty, value)
	  End Set
	End Property

	#End Region ' UseSystemTheme Property

	#Region "IsNewTheme Property"

	Public Shared ReadOnly IsNewThemeProperty As DependencyProperty = DependencyProperty.Register("IsNewTheme", GetType(Boolean), GetType(ThemeData), New FrameworkPropertyMetadata(CBool(False)))

	Public Property IsNewTheme() As Boolean
	  Get
		Return CBool(Me.GetValue(ThemeData.IsNewThemeProperty))
	  End Get
	  Set(ByVal value As Boolean)
		Me.SetValue(ThemeData.IsNewThemeProperty, value)
	  End Set
	End Property

	#End Region

  End Class
End Namespace