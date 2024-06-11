'
' * Xceed DataGrid for WPF - SAMPLE CODE - Selection™ View Sample Application
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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Selection
    Public Class ConfigurationData
        Implements INotifyPropertyChanged
#Region "Singleton Property"

        Public Shared ReadOnly Singleton As New ConfigurationData()

#End Region ' Singleton Property

#Region "CONSTRUCTORS"

        Private Sub New()
        End Sub

#End Region ' CONSTRUCTORS

#Region "NavigationBehavior Property"

        Public Property NavigationBehavior() As NavigationBehavior
            Get
                Return m_navigationBehavior
            End Get

            Set(ByVal value As NavigationBehavior)
                If value <> m_navigationBehavior Then
                    m_navigationBehavior = value
                    Me.RaisePropertyChanged("NavigationBehavior")
                    Me.RaisePropertyChanged("NavigationBehaviorCellOnly")
                    Me.RaisePropertyChanged("NavigationBehaviorRowOnly")
                    Me.RaisePropertyChanged("NavigationBehaviorRowOrCell")
                    Me.RaisePropertyChanged("NavigationBehaviorNone")
                End If
            End Set
        End Property

        Public Property NavigationBehaviorCellOnly() As Boolean
            Get
                Return (Me.NavigationBehavior = NavigationBehavior.CellOnly)
            End Get

            Set(ByVal value As Boolean)
                If value Then
                    Me.NavigationBehavior = NavigationBehavior.CellOnly
                End If
            End Set
        End Property

        Public Property NavigationBehaviorRowOnly() As Boolean
            Get
                Return (Me.NavigationBehavior = NavigationBehavior.RowOnly)
            End Get

            Set(ByVal value As Boolean)
                If value Then
                    Me.NavigationBehavior = NavigationBehavior.RowOnly
                End If
            End Set
        End Property

        Public Property NavigationBehaviorRowOrCell() As Boolean
            Get
                Return (Me.NavigationBehavior = NavigationBehavior.RowOrCell)
            End Get

            Set(ByVal value As Boolean)
                If value Then
                    Me.NavigationBehavior = NavigationBehavior.RowOrCell
                End If
            End Set
        End Property

        Public Property NavigationBehaviorNone() As Boolean
            Get
                Return (Me.NavigationBehavior = NavigationBehavior.None)
            End Get

            Set(ByVal value As Boolean)
                If value Then
                    Me.NavigationBehavior = NavigationBehavior.None
                End If
            End Set
        End Property

        Private m_navigationBehavior As NavigationBehavior = NavigationBehavior.CellOnly

#End Region ' NavigationBehavior Property

#Region "SelectionMode Property"

        Private Shared s_selectionModeList As Dictionary(Of System.Windows.Controls.SelectionMode, String) = Nothing

        Public ReadOnly Property SelectionModeList() As Dictionary(Of System.Windows.Controls.SelectionMode, String)
            Get
                If s_selectionModeList Is Nothing Then
                    s_selectionModeList = New Dictionary(Of System.Windows.Controls.SelectionMode, String)()
                    s_selectionModeList.Add(SelectionMode.Single, "Single")
                    s_selectionModeList.Add(SelectionMode.Multiple, "Multiple")
                    s_selectionModeList.Add(SelectionMode.Extended, "Extended")
                End If
                Return s_selectionModeList
            End Get
        End Property

        Private m_selectionMode As SelectionMode = SelectionMode.Extended

        Public Property SelectionMode() As SelectionMode
            Get
                Return m_selectionMode
            End Get

            Set(ByVal value As SelectionMode)
                If value <> m_selectionMode Then
                    m_selectionMode = value
                    Me.RaisePropertyChanged("SelectionMode")
                End If
            End Set
        End Property

#End Region ' SelectionMode Property

#Region "Selection Unit Property"

        Private Shared s_selectionUnitList As Dictionary(Of SelectionUnit, String) = Nothing

        Public ReadOnly Property SelectionUnitList() As Dictionary(Of SelectionUnit, String)
            Get
                If s_selectionUnitList Is Nothing Then
                    s_selectionUnitList = New Dictionary(Of SelectionUnit, String)()
                    s_selectionUnitList.Add(SelectionUnit.Cell, "Cell")
                    s_selectionUnitList.Add(SelectionUnit.Row, "Row")
                End If
                Return s_selectionUnitList
            End Get
        End Property

        Private m_selectionUnit As SelectionUnit = SelectionUnit.Row

        Public Property SelectionUnit() As SelectionUnit
            Get
                Return m_selectionUnit
            End Get

            Set(ByVal value As SelectionUnit)
                If value <> m_selectionUnit Then
                    m_selectionUnit = value
                    Me.RaisePropertyChanged("SelectionUnit")
                End If
            End Set
        End Property

#End Region

#Region "IsGroupSelectionEnabled Property"

        Private m_isGroupSelectionEnabled As Boolean = False

        Public Property IsGroupSelectionEnabled() As Boolean
            Get
                Return m_isGroupSelectionEnabled
            End Get

            Set(ByVal value As Boolean)
                If value <> m_isGroupSelectionEnabled Then
                    m_isGroupSelectionEnabled = value
                    Me.RaisePropertyChanged("IsGroupSelectionEnabled")
                End If
            End Set
        End Property

#End Region

#Region "EnableIsGroupSelectionEnabled"

        Private m_enableIsGroupSelectionEnabled As Boolean = False

        Public Property EnableIsGroupSelectionEnabled() As Boolean
            Get
                Return m_enableIsGroupSelectionEnabled
            End Get

            Set(ByVal value As Boolean)
                If value <> m_enableIsGroupSelectionEnabled Then
                    m_enableIsGroupSelectionEnabled = value
                    Me.RaisePropertyChanged("EnableIsGroupSelectionEnabled")
                End If
            End Set
        End Property

#End Region

#Region "EnableDragSelection Property"

        Private m_enableDragSelection As Boolean = False

        Public Property EnableDragSelection() As Boolean
            Get
                Return m_enableDragSelection
            End Get

            Set(ByVal value As Boolean)
                If value <> m_enableDragSelection Then
                    m_enableDragSelection = value
                    Me.RaisePropertyChanged("EnableDragSelection")
                End If
            End Set
        End Property

#End Region

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