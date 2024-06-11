'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [ConfigurationData.vb]
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
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Class ConfigurationData
    Implements INotifyPropertyChanged
#Region "Singleton Property"

    Public Shared ReadOnly Singleton As ConfigurationData = New ConfigurationData()

#End Region ' Singleton Property

#Region "CONSTRUCTORS"

    Private Sub New()
    End Sub

#End Region ' CONSTRUCTORS

#Region "FilteringMethod Property"

    Public Property FilteringMethod() As FilteringMethod
      Get
        Return m_filteringMethod
      End Get

      Set(ByVal value As FilteringMethod)
        If value <> m_filteringMethod Then
          m_filteringMethod = value
          Me.RaisePropertyChanged("FilteringMethod")
          Me.RaisePropertyChanged("FilteringMethodAuto")
          Me.RaisePropertyChanged("FilteringMethodFilterRow")
          Me.RaisePropertyChanged("FilteringMethodNone")
        End If
      End Set
    End Property

    Public Property FilteringMethodAuto() As Boolean
      Get
        Return (Me.FilteringMethod = FilteringMethod.AutoFilter)
      End Get

      Set(ByVal value As Boolean)
        If value Then
          Me.FilteringMethod = FilteringMethod.AutoFilter
        End If
      End Set
    End Property

    Public Property FilteringMethodFilterRow() As Boolean
      Get
        Return (Me.FilteringMethod = FilteringMethod.FilterRow)
      End Get

      Set(ByVal value As Boolean)
        If value Then
          Me.FilteringMethod = FilteringMethod.FilterRow
          Me.ShowInsertionRow = False
        End If
      End Set
    End Property

    Public Property FilteringMethodNone() As Boolean
      Get
        Return (Me.FilteringMethod = FilteringMethod.None)
      End Get

      Set(ByVal value As Boolean)
        If value Then
          Me.FilteringMethod = FilteringMethod.None
        End If
      End Set
    End Property

    Private m_filteringMethod As FilteringMethod = FilteringMethod.AutoFilter

#End Region ' FilteringMethod Property

#Region "ShowMergedHeaders Property"

    Public Property ShowMergedHeaders() As Boolean
      Get
        Return m_showMergedHeaders
      End Get
      Set(ByVal value As Boolean)
        If value <> m_showMergedHeaders Then
          m_showMergedHeaders = value
          Me.RaisePropertyChanged("ShowMergedHeaders")
        End If
      End Set
    End Property

    Private m_showMergedHeaders As Boolean = True

#End Region ' ShowMergedHeaders Property

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

#End Region ' AllowColumnChooser Property

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

#End Region ' AllowFixedColumnSplitterDrag Property

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

#End Region ' FlowDirection Property

#Region "IsAlternatingRowStyleEnabled Property"

    Public Property IsAlternatingRowStyleEnabled() As Boolean
      Get
        Return m_isAlternatingRowStyleEnabled
      End Get

      Set(ByVal value As Boolean)
        If value <> m_isAlternatingRowStyleEnabled Then
          m_isAlternatingRowStyleEnabled = value
          Me.RaisePropertyChanged("IsAlternatingRowStyleEnabled")
        End If
      End Set
    End Property

    Private m_isAlternatingRowStyleEnabled As Boolean = True

#End Region ' IsAlternatingRowStyleEnabled Property

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

#End Region ' ShowColumnManagerRow Property

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

#Region "ShowSearchControl Property"

    Public Property ShowSearchControl() As Boolean
      Get
        Return m_showSearchControl
      End Get

      Set(ByVal value As Boolean)
        If value <> m_showSearchControl Then
          m_showSearchControl = value
          Me.RaisePropertyChanged("ShowSearchControl")
        End If
      End Set
    End Property

    Private m_showSearchControl As Boolean = False

#End Region

#Region "ShowGroupByControl Property"

    Public Property ShowGroupByControl() As Boolean
      Get
        Return m_showGroupByControl
      End Get

      Set(ByVal value As Boolean)
        If value <> m_showGroupByControl Then
          m_showGroupByControl = value
          Me.RaisePropertyChanged("ShowGroupByControl")
        End If
      End Set
    End Property

    Private m_showGroupByControl As Boolean = True

#End Region ' ShowGroupByControl Property

#Region "ShowInsertionRow Property"

    Public Property ShowInsertionRow() As Boolean
      Get
        Return m_showInsertionRow
      End Get

      Set(ByVal value As Boolean)
        If value <> m_showInsertionRow Then
          m_showInsertionRow = value
          Me.RaisePropertyChanged("ShowInsertionRow")
        End If
      End Set
    End Property

    Private m_showInsertionRow As Boolean = False

#End Region ' ShowInsertionRow Property

#Region "ShowSortIndex Property"

    Public Property ShowSortIndex() As Boolean
      Get
        Return m_showSortIndex
      End Get

      Set(ByVal value As Boolean)
        If value <> m_showSortIndex Then
          m_showSortIndex = value
          Me.RaisePropertyChanged("ShowSortIndex")
        End If
      End Set
    End Property

    Private m_showSortIndex As Boolean

#End Region ' ShowSortIndex Property

#Region "InsertionMode Property"

    Public Property InsertionMode() As InsertionMode
      Get
        Return m_insertionMode
      End Get

      Set(ByVal value As InsertionMode)
        If value <> m_insertionMode Then
          m_insertionMode = value
          Me.RaisePropertyChanged("InsertionMode")
        End If
      End Set
    End Property

    Private m_insertionMode As InsertionMode = InsertionMode.Default

#End Region ' InsertionMode Property

#Region "ShowScrollTip Property"

    Public Property ShowScrollTip() As Boolean
      Get
        Return m_showScrollTip
      End Get

      Set(ByVal value As Boolean)
        If value <> m_showScrollTip Then
          m_showScrollTip = value
          Me.RaisePropertyChanged("ShowScrollTip")
        End If
      End Set
    End Property

    Private m_showScrollTip As Boolean

#End Region ' ShowScrollTip Property

#Region "ShowMasterDetail Property"

    Public Property ShowMasterDetail() As Boolean
      Get
        Return m_showMasterDetail
      End Get
      Set(ByVal value As Boolean)
        If m_showMasterDetail <> value Then
          m_showMasterDetail = value
          Me.RaisePropertyChanged("ShowMasterDetail")
        End If
      End Set
    End Property

    Private m_showMasterDetail As Boolean = False

#End Region ' ShowMasterDetail Property

#Region "AreHeadersSticky Property"

    Public Property AreHeadersSticky() As Boolean
      Get
        Return m_areHeadersSticky
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areHeadersSticky Then
          m_areHeadersSticky = value
          Me.RaisePropertyChanged("AreHeadersSticky")
        End If
      End Set
    End Property

    Private m_areHeadersSticky As Boolean = True

#End Region ' AreHeadersSticky Property

#Region "AreFootersSticky Property"

    Public Property AreFootersSticky() As Boolean
      Get
        Return m_areFootersSticky
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areFootersSticky Then
          m_areFootersSticky = value
          Me.RaisePropertyChanged("AreFootersSticky")
        End If
      End Set
    End Property

    Private m_areFootersSticky As Boolean = True

#End Region ' AreFootersSticky Property

#Region "AreGroupHeadersSticky Property"

    Public Property AreGroupHeadersSticky() As Boolean
      Get
        Return m_areGroupHeadersSticky
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areGroupHeadersSticky Then
          m_areGroupHeadersSticky = value
          Me.RaisePropertyChanged("AreGroupHeadersSticky")
        End If
      End Set
    End Property

    Private m_areGroupHeadersSticky As Boolean = True

#End Region ' AreGroupHeadersSticky Property

#Region "AreGroupFootersSticky Property"

    Public Property AreGroupFootersSticky() As Boolean
      Get
        Return m_areGroupFootersSticky
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areGroupFootersSticky Then
          m_areGroupFootersSticky = value
          Me.RaisePropertyChanged("AreGroupFootersSticky")
        End If
      End Set
    End Property

    Private m_areGroupFootersSticky As Boolean = True

#End Region ' AreGroupFootersSticky Property

#Region "AreParentRowsSticky Property"

    Public Property AreParentRowsSticky() As Boolean
      Get
        Return m_areParentRowsSticky
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areParentRowsSticky Then
          m_areParentRowsSticky = value
          Me.RaisePropertyChanged("AreParentRowsSticky")
        End If
      End Set
    End Property

    Private m_areParentRowsSticky As Boolean = True

#End Region ' AreParentRowsSticky Property

#Region "AreGroupsFlattened Property"

    Public Property AreGroupsFlattened() As Boolean
      Get
        Return m_areGroupsFlattened
      End Get
      Set(ByVal value As Boolean)
        If value <> m_areGroupsFlattened Then
          m_areGroupsFlattened = value
          Me.RaisePropertyChanged("AreGroupsFlattened")
        End If
      End Set
    End Property

    Private m_areGroupsFlattened As Boolean = True

#End Region ' AreGroupsFlattened Property

#Region "ScrollingAnimationDuration Property"

    Public Property ScrollingAnimationDuration() As Double
      Get
        Return m_scrollingAnimationDuration
      End Get
      Set(ByVal value As Double)
        If m_scrollingAnimationDuration <> value Then
          m_scrollingAnimationDuration = value
          Me.RaisePropertyChanged("ScrollingAnimationDuration")
        End If
      End Set
    End Property

    Private m_scrollingAnimationDuration As Double = 750.0R

#End Region ' ScrollingAnimationDuration Property

#Region "RowFadeInAnimationDuration Property"

    Public Property RowFadeInAnimationDuration() As Double
      Get
        Return m_rowFadeInAnimationDuration
      End Get
      Set(ByVal value As Double)
        If m_rowFadeInAnimationDuration <> value Then
          m_rowFadeInAnimationDuration = value
          Me.RaisePropertyChanged("RowFadeInAnimationDuration")
        End If
      End Set
    End Property

    Private m_rowFadeInAnimationDuration As Double = 300.0R

#End Region ' RowFadeInAnimationDuration Property

#Region "IsAnimatedColumnReorderingEnabled Property"

    Public Property IsAnimatedColumnReorderingEnabled() As Boolean
      Get
        Return m_isAnimatedColumnReorderingEnabled
      End Get
      Set(ByVal value As Boolean)
        If m_isAnimatedColumnReorderingEnabled <> value Then
          m_isAnimatedColumnReorderingEnabled = value
          Me.RaisePropertyChanged("IsAnimatedColumnReorderingEnabled")
        End If
      End Set
    End Property

    Private m_isAnimatedColumnReorderingEnabled As Boolean = True

#End Region ' IsAnimatedColumnReorderingEnabled Property

#Region "INotifyPropertyChanged Implementation"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub RaisePropertyChanged(ByVal name As String)
      If Not Me.PropertyChangedEvent Is Nothing Then
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
      End If
    End Sub

#End Region ' INotifyPropertyChanged Implementation
  End Class
End Namespace
