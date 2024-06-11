'
' * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView View Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ConfigurationData.cs]
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
  Public Class ConfigurationData
	  Implements INotifyPropertyChanged
	#Region "Singleton Property"

	Public Shared ReadOnly Singleton As New ConfigurationData()

	#End Region

	#Region "CONSTRUCTORS"

	Private Sub New()
	End Sub

	#End Region

	#Region "FlowDirection Property"

	Public Property FlowDirection() As FlowDirection
	  Get
		Return m_flowDirection
	  End Get

	  Set(ByVal value As FlowDirection)
		If value <> m_flowDirection Then
		  m_flowDirection = value
		  Me.RaisePropertyChanged("FlowDirection")
		  Me.RaisePropertyChanged("FlowDirectionLeftToRight")
		  Me.RaisePropertyChanged("FlowDirectionRightToLeft")
		End If
	  End Set
	End Property

	Public Property FlowDirectionLeftToRight() As Boolean
	  Get
		Return (Me.FlowDirection = FlowDirection.LeftToRight)
	  End Get

	  Set(ByVal value As Boolean)
		If value Then
		  Me.FlowDirection = FlowDirection.LeftToRight
		End If
	  End Set
	End Property

	Public Property FlowDirectionRightToLeft() As Boolean
	  Get
		Return (Me.FlowDirection = FlowDirection.RightToLeft)
	  End Get

	  Set(ByVal value As Boolean)
		If value Then
		  Me.FlowDirection = FlowDirection.RightToLeft
		End If
	  End Set
	End Property

	Private m_flowDirection As FlowDirection = FlowDirection.LeftToRight

	#End Region

	#Region "ShowColumnManagerRow Property"

	Public Property ShowColumnManagerRow() As Boolean
	  Get
		Return m_showColumnManagerRow
	  End Get

	  Set(ByVal value As Boolean)
		If value <> m_showColumnManagerRow Then
		  m_showColumnManagerRow = value
		  Me.RaisePropertyChanged("ShowColumnManagerRow")
		End If
	  End Set
	End Property

	Private m_showColumnManagerRow As Boolean = True

	#End Region

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

	#End Region

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

	#End Region

	#Region "INotifyPropertyChanged Implementation"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Protected Sub RaisePropertyChanged(ByVal name As String)
	  If Me.PropertyChangedEvent IsNot Nothing Then
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
	  End If
	End Sub

	#End Region
  End Class
End Namespace
