'
' Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'  
' [ConfigurationData.cs]
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

Namespace Xceed.Wpf.DataGrid.Samples.Exporting
  Public Class ConfigurationData
    Implements INotifyPropertyChanged
    Public Shared ReadOnly Singleton As New ConfigurationData()

    Private Sub New()
    End Sub

    ' Common export settings

#Region "DetailDepth Property"

    Public Property DetailDepth() As Integer
      Get
        Return m_detailDepth
      End Get

      Set(ByVal value As Integer)
        If value <> m_detailDepth Then
          m_detailDepth = value
          Me.RaisePropertyChanged("DetailDepth")
        End If
      End Set
    End Property

    Private m_detailDepth As Integer

#End Region ' DetailDepth Property

#Region "IncludeColumnHeaders Property"

    Public Property IncludeColumnHeaders() As Boolean
      Get
        Return m_includeColumnHeaders
      End Get

      Set(ByVal value As Boolean)
        If value <> m_includeColumnHeaders Then
          m_includeColumnHeaders = value
          Me.RaisePropertyChanged("IncludeColumnHeaders")
        End If
      End Set
    End Property

    Private m_includeColumnHeaders As Boolean = True

#End Region ' IncludeColumnHeaders Property

#Region "RepeatParentData Property"

    Public Property RepeatParentData() As Boolean
      Get
        Return m_repeatParentData
      End Get

      Set(ByVal value As Boolean)
        If value <> m_repeatParentData Then
          m_repeatParentData = value
          Me.RaisePropertyChanged("RepeatParentData")
        End If
      End Set
    End Property

    Private m_repeatParentData As Boolean

#End Region ' RepeatParentData Property

#Region "UseFieldNamesInHeader Property"

    Public Property UseFieldNamesInHeader() As Boolean
      Get
        Return m_useFieldNamesInHeader
      End Get

      Set(ByVal value As Boolean)
        If value <> m_useFieldNamesInHeader Then
          m_useFieldNamesInHeader = value
          Me.RaisePropertyChanged("UseFieldNamesInHeader")
        End If
      End Set
    End Property

    Private m_useFieldNamesInHeader As Boolean

#End Region ' UseFieldNamesInHeader Property

    ' Excel export settings

#Region "ExportStatFunctionsAsFormulas Property"

    Public Property ExportStatFunctionsAsFormulas() As Boolean
      Get
        Return m_exportStatFunctionsAsFormulas
      End Get

      Set(ByVal value As Boolean)
        If value <> m_exportStatFunctionsAsFormulas Then
          m_exportStatFunctionsAsFormulas = value
          Me.RaisePropertyChanged("ExportStatFunctionsAsFormulas")
        End If
      End Set
    End Property

    Private m_exportStatFunctionsAsFormulas As Boolean

#End Region ' ExportStatFunctionsAsFormulas Property

#Region "IsHeaderFixed Property"

    Public Property IsHeaderFixed() As Boolean
      Get
        Return m_isHeaderFixed
      End Get

      Set(ByVal value As Boolean)
        If value <> m_isHeaderFixed Then
          m_isHeaderFixed = value
          Me.RaisePropertyChanged("IsHeaderFixed")
        End If
      End Set
    End Property

    Private m_isHeaderFixed As Boolean = True

#End Region ' IsHeaderFixed Property

#Region "StatFunctionDepth Property"

    Public Property StatFunctionDepth() As Integer
      Get
        Return m_statFunctionDepth
      End Get

      Set(ByVal value As Integer)
        If value <> m_statFunctionDepth Then
          m_statFunctionDepth = value
          Me.RaisePropertyChanged("StatFunctionDepth")
        End If
      End Set
    End Property

    Private m_statFunctionDepth As Integer

#End Region ' StatFunctionDepth Property

    ' CSV export settings

#Region "DateTimeFormat Property"

    Public Property DateTimeFormat() As String
      Get
        Return m_dateTimeFormat
      End Get

      Set(ByVal value As String)
        If value <> m_dateTimeFormat Then
          m_dateTimeFormat = value
          Me.RaisePropertyChanged("DateTimeFormat")
        End If
      End Set
    End Property

    Private m_dateTimeFormat As String

#End Region ' DateTimeFormat Property

#Region "NumberFormat Property"

    Public Property NumberFormat() As String
      Get
        Return m_numberFormat
      End Get

      Set(ByVal value As String)
        If value <> m_numberFormat Then
          m_numberFormat = value
          Me.RaisePropertyChanged("NumberFormat")
        End If
      End Set
    End Property

    Private m_numberFormat As String

#End Region ' NumberFormat Property

#Region "Separator Property"

    Public Property Separator() As Char
      Get
        Return m_separator
      End Get

      Set(ByVal value As Char)
        If value <> m_separator Then
          m_separator = value
          Me.RaisePropertyChanged("Separator")
        End If
      End Set
    End Property

    Private m_separator As Char

#End Region ' Separator Property

#Region "TextQualifier Property"

    Public Property TextQualifier() As Char
      Get
        Return m_textQualifier
      End Get

      Set(ByVal value As Char)
        If value <> m_textQualifier Then
          m_textQualifier = value
          Me.RaisePropertyChanged("TextQualifier")
        End If
      End Set
    End Property

    Private m_textQualifier As Char

#End Region ' TextQualifier Property

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
