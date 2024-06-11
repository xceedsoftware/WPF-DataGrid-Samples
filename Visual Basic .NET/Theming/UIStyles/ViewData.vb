'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ViewData.vb]
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
Imports System.Windows
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
  Public Class ViewData
	  Inherits DependencyObject
	#Region "CONSTRUCTORS"

	Public Sub New()
	End Sub

	#End Region ' CONSTRUCTORS

	#Region "ImageDataTemplate Property"

	Public Shared ReadOnly ImageDataTemplateProperty As DependencyProperty = DependencyProperty.Register("ImageDataTemplate", GetType(DataTemplate), GetType(ViewData))

	Public Property ImageDataTemplate() As DataTemplate
	  Get
		Return CType(Me.GetValue(ViewData.ImageDataTemplateProperty), DataTemplate)
	  End Get
	  Set(ByVal value As DataTemplate)
		Me.SetValue(ViewData.ImageDataTemplateProperty, value)
	  End Set
	End Property

	#End Region ' ImageDataTemplate Property

	#Region "Description Property"

	Public Shared ReadOnly DescriptionProperty As DependencyProperty = DependencyProperty.Register("Description", GetType(String), GetType(ViewData))

	Public Property Description() As String
	  Get
		Return CStr(Me.GetValue(ViewData.DescriptionProperty))
	  End Get
	  Set(ByVal value As String)
		Me.SetValue(ViewData.DescriptionProperty, value)
	  End Set
	End Property

	#End Region ' Description Property

	#Region "IsView3D Property"

	Public Shared ReadOnly IsView3DProperty As DependencyProperty = DependencyProperty.Register("IsView3D", GetType(Boolean), GetType(ViewData))

	Public Property IsView3D() As Boolean
	  Get
		Return CBool(Me.GetValue(ViewData.IsView3DProperty))
	  End Get
	  Set(ByVal value As Boolean)
		Me.SetValue(ViewData.IsView3DProperty, value)
	  End Set
	End Property

	#End Region ' IsView3D Property

	#Region "ViewType Property"

	Public Shared ReadOnly ViewTypeProperty As DependencyProperty = DependencyProperty.Register("ViewType", GetType(Type), GetType(ViewData))

	Public Property ViewType() As Type
	  Get
		Return CType(Me.GetValue(ViewData.ViewTypeProperty), Type)
	  End Get
	  Set(ByVal value As Type)
		Me.SetValue(ViewData.ViewTypeProperty, value)
	  End Set
	End Property

	#End Region ' View Property
  End Class
End Namespace