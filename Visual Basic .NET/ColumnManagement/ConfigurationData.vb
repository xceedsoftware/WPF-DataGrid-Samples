'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ConfigurationData.vb]
'  
' This class implements the business object containing various dynamic configuration 
' options offered by the configuration panel.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.ComponentModel

Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Public Class ConfigurationData
    Implements INotifyPropertyChanged
    Public Shared ReadOnly Singleton As New ConfigurationData()

    Private Sub New()
    End Sub

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

    Private m_allowColumnChooser As Boolean

#End Region ' AllowColumnChooser Property

#Region "AllowColumnResizing Property"

    Public Property AllowColumnResizing() As Boolean
      Get
        Return m_allowColumnResizing
      End Get

      Set(ByVal value As Boolean)
        If value <> m_allowColumnResizing Then
          m_allowColumnResizing = value
          Me.RaisePropertyChanged("AllowColumnResizing")
        End If
      End Set
    End Property

    Private m_allowColumnResizing As Boolean = True

#End Region ' AllowColumnResizing Property

#Region "ColumnChooserSortOrder Property"

    Public Property ColumnChooserSortOrder() As ColumnChooserSortOrder
      Get
        Return m_columnChooserSortOrder
      End Get

      Set(ByVal value As ColumnChooserSortOrder)
        If value <> m_columnChooserSortOrder Then
          m_columnChooserSortOrder = value
          Me.RaisePropertyChanged("ColumnChooserSortOrder")
        End If
      End Set
    End Property

    Private m_columnChooserSortOrder As ColumnChooserSortOrder = ColumnChooserSortOrder.VisiblePosition

#End Region ' ColumnChooserSortOrder Property

#Region "ColumnStretchMinWidth Property"

    Public Property ColumnStretchMinWidth() As Double
      Get
        Return m_columnStretchMinWidth
      End Get

      Set(ByVal value As Double)
        If value <> m_columnStretchMinWidth Then
          m_columnStretchMinWidth = value
          Me.RaisePropertyChanged("ColumnStretchMinWidth")
        End If
      End Set
    End Property

    Private m_columnStretchMinWidth As Double = 50.0

#End Region ' ColumnStretchMinWidth Property

#Region "ColumnStretchMode Property"

    Public Property ColumnStretchMode() As ColumnStretchMode
      Get
        Return m_columnStretchMode
      End Get

      Set(ByVal value As ColumnStretchMode)
        If value <> m_columnStretchMode Then
          m_columnStretchMode = value
          Me.RaisePropertyChanged("ColumnStretchMode")
        End If
      End Set
    End Property

    Private m_columnStretchMode As ColumnStretchMode = ColumnStretchMode.None

#End Region ' ColumnStretchMode Property

#Region "DataGrid Property"

    Public Property DataGrid() As DataGridControl
      Get
        Return m_dataGrid
      End Get

      Set(ByVal value As DataGridControl)
        If value IsNot m_dataGrid Then
          m_dataGrid = value
          Me.RaisePropertyChanged("DataGrid")
        End If
      End Set
    End Property

    Private m_dataGrid As DataGridControl

#End Region ' DataGrid Property

#Region "IsAllowColumnResizingEnabled Property"

    Public Property IsAllowColumnResizingEnabled() As Boolean
      Get
        Return m_isAllowColumnResizingEnabled
      End Get

      Set(ByVal value As Boolean)
        If value <> m_isAllowColumnResizingEnabled Then
          m_isAllowColumnResizingEnabled = value
          Me.RaisePropertyChanged("IsAllowColumnResizingEnabled")
        End If
      End Set
    End Property

    Private m_isAllowColumnResizingEnabled As Boolean = True

#End Region ' IsAllowColumnResizingEnabled Property

#Region "IsColumnStretchMinWidthEnabled Property"

    Public Property IsColumnStretchMinWidthEnabled() As Boolean
      Get
        Return m_isColumnStretchMinWidthEnabled
      End Get

      Set(ByVal value As Boolean)
        If value <> m_isColumnStretchMinWidthEnabled Then
          m_isColumnStretchMinWidthEnabled = value
          Me.RaisePropertyChanged("IsColumnStretchMinWidthEnabled")
        End If
      End Set
    End Property

    Private m_isColumnStretchMinWidthEnabled As Boolean = True

#End Region ' IsColumnStretchMinWidthEnabled Property

#Region "UseAdvancedColumnManagement Property"

    Public Property UseAdvancedColumnManagement() As Boolean
      Get
        Return m_useAdvancedColumnManagement
      End Get

      Set(ByVal value As Boolean)
        If value <> m_useAdvancedColumnManagement Then
          m_useAdvancedColumnManagement = value
          Me.RaisePropertyChanged("UseAdvancedColumnManagement")
        End If
      End Set
    End Property

    Private m_useAdvancedColumnManagement As Boolean

#End Region ' UseAdvancedColumnManagement Property

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
