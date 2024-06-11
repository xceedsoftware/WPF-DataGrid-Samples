'
' * Xceed DataGrid for WPF - SAMPLE CODE - Merged Headers Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ConfigurationData.vb]
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports Microsoft.VisualBasic
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
  Public Class ConfigurationData
	  Implements INotifyPropertyChanged
	#Region "Singleton Property"

	Public Shared ReadOnly Singleton As New ConfigurationData()

	#End Region ' Singleton Property

	#Region "CONSTRUCTORS"

	Private Sub New()
	End Sub

	#End Region ' CONSTRUCTORS

	#Region "AllowColumnChooser Property"

	Public Property AllowColumnChooser() As Boolean
	  Get
		Return m_allowColumnChooser
	  End Get

	  Set(ByVal value As Boolean)
		If value <> m_allowColumnChooser Then
		  m_allowColumnChooser = value
		  Me.RaisePropertyChanged("AllowColumnChooser")
		End If
	  End Set
	End Property

	Private m_allowColumnChooser As Boolean = True

	#End Region	' AllowColumnChooser Property

	#Region "AllowFixedColumnSplitterDrag Property"

	Public Property AllowFixedColumnSplitterDrag() As Boolean
		Get
		Return m_allowFixedColumnSplitterDrag
		End Get

		Set(ByVal value As Boolean)
		If value <> m_allowFixedColumnSplitterDrag Then
			m_allowFixedColumnSplitterDrag = value
			Me.RaisePropertyChanged("AllowFixedColumnSplitterDrag")
		End If
		End Set
	End Property

	Private m_allowFixedColumnSplitterDrag As Boolean = True

	#End Region	' AllowFixedColumnSplitterDrag Property

	#Region "ShowFixedColumnSplitter Property"

	Public Property ShowFixedColumnSplitter() As Boolean
	  Get
		Return m_showFixedColumnSplitter
	  End Get
	  Set(ByVal value As Boolean)
		If value <> m_showFixedColumnSplitter Then
		  m_showFixedColumnSplitter = value
		  Me.RaisePropertyChanged("ShowFixedColumnSplitter")
		End If
	  End Set
	End Property

	Private m_showFixedColumnSplitter As Boolean = True

	#End Region ' ShowFixedColumnSplitter Property

	#Region "AllowColumnReorder Property"

	Public Property AllowColumnReorder() As Boolean
	  Get
		Return m_allowColumnReorder
	  End Get
	  Set(ByVal value As Boolean)
		If m_allowColumnReorder <> value Then
		  m_allowColumnReorder = value
		  Me.RaisePropertyChanged("AllowColumnReorder")
		End If
	  End Set
	End Property

	Private m_allowColumnReorder As Boolean 'false

	#End Region ' AllowColumnsReordering Property

	#Region "AllowColumnResize Property"

	Public Property AllowColumnResize() As Boolean
	  Get
		Return m_allowColumnResize
	  End Get
	  Set(ByVal value As Boolean)
		If m_allowColumnResize <> value Then
		  m_allowColumnResize = value
		  Me.RaisePropertyChanged("AllowColumnResize")
		End If
	  End Set
	End Property

	Private m_allowColumnResize As Boolean = True

	#End Region ' AllowColumnResize Property

	#Region "AllowMergedHeadersReorder Property"

	Public Property AllowMergedHeadersReorder() As Boolean
	  Get
		Return m_allowMergedHeadersReorder
	  End Get
	  Set(ByVal value As Boolean)
		If m_allowMergedHeadersReorder <> value Then
		  m_allowMergedHeadersReorder = value
		  Me.RaisePropertyChanged("AllowMergedHeadersReorder")
		End If
	  End Set
	End Property

	Private m_allowMergedHeadersReorder As Boolean = True

	#End Region ' AllowMergedHeadersReorder Property

	#Region "AllowMergedHeadersResize Property"

	Public Property AllowMergedHeadersResize() As Boolean
	  Get
		Return m_allowMergedHeadersResize
	  End Get
	  Set(ByVal value As Boolean)
		If m_allowMergedHeadersResize <> value Then
		  m_allowMergedHeadersResize = value
		  Me.RaisePropertyChanged("AllowMergedHeadersResize")
		End If
	  End Set
	End Property

	Private m_allowMergedHeadersResize As Boolean = True

	#End Region ' AllowMergedHeadersResize Property

	#Region "INotifyPropertyChanged Implementation"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Protected Sub RaisePropertyChanged(ByVal name As String)
	  If Me.PropertyChangedEvent IsNot Nothing Then
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
	  End If
	End Sub

	#End Region ' INotifyPropertyChanged Implementation
  End Class
End Namespace
