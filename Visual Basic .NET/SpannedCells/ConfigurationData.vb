'
' Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
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

Imports System.ComponentModel
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
  Public Class ConfigurationData
    Implements INotifyPropertyChanged
    Public Shared ReadOnly Singleton As New ConfigurationData()

    Private Sub New()
      m_spannedCellSelector = New CustomSpannedCellSelector()
      m_spannedCellConfigurationSelector = New CustomSpannedCellConfigurationSelector(Me)
    End Sub

#Region "AllowCellSpanning Property"

    Public Property AllowCellSpanning() As Boolean
      Get
        Return m_allowCellSpanning
      End Get

      Set(ByVal value As Boolean)
        If value <> m_allowCellSpanning Then
          m_allowCellSpanning = value
          Me.RaisePropertyChanged("AllowCellSpanning")
        End If
      End Set
    End Property

    Private m_allowCellSpanning As Boolean = True

#End Region

#Region "ConflictResolutionMode Property"

    Public Property ConflictResolutionMode() As SpannedCellConflictResolutionMode
      Get
        Return m_conflictResolutionMode
      End Get

      Set(ByVal value As SpannedCellConflictResolutionMode)
        If value <> m_conflictResolutionMode Then
          m_conflictResolutionMode = value
          Me.RaisePropertyChanged("ConflictResolutionMode")
        End If
      End Set
    End Property

    Private m_conflictResolutionMode As SpannedCellConflictResolutionMode = SpannedCellConflictResolutionMode.ColumnFirst

#End Region

#Region "UseCustomConfiguration Property"

    Public Property UseCustomConfiguration() As Boolean
      Get
        Return m_useCustomConfiguration
      End Get

      Set(ByVal value As Boolean)
        If value <> m_useCustomConfiguration Then
          m_useCustomConfiguration = value
          Me.RaisePropertyChanged("UseCustomConfiguration")
          Me.RaisePropertyChanged("SpannedCellSelector")
          Me.RaisePropertyChanged("SpannedCellConfigurationSelector")
        End If
      End Set
    End Property

    Private m_useCustomConfiguration As Boolean = True

#End Region

#Region "HorizontalContentAlignment Property"

    Public Property HorizontalContentAlignment() As HorizontalAlignment
      Get
        Return m_horizontalContentAlignment
      End Get

      Set(ByVal value As HorizontalAlignment)
        If value <> m_horizontalContentAlignment Then
          m_horizontalContentAlignment = value
          Me.RaisePropertyChanged("HorizontalContentAlignment")
        End If
      End Set
    End Property

    Private m_horizontalContentAlignment As HorizontalAlignment = HorizontalAlignment.Stretch

#End Region

#Region "VerticalContentAlignment Property"

    Public Property VerticalContentAlignment() As VerticalAlignment
      Get
        Return m_verticalContentAlignment
      End Get

      Set(ByVal value As VerticalAlignment)
        If value <> m_verticalContentAlignment Then
          m_verticalContentAlignment = value
          Me.RaisePropertyChanged("VerticalContentAlignment")
        End If
      End Set
    End Property

    Private m_verticalContentAlignment As VerticalAlignment = VerticalAlignment.Stretch

#End Region

#Region "AddBorders Property"

    Public Property AddBorders() As Boolean
      Get
        Return m_addBorders
      End Get

      Set(ByVal value As Boolean)
        If value <> m_addBorders Then
          m_addBorders = value
          Me.RaisePropertyChanged("AddBorders")
        End If
      End Set
    End Property

    Private m_addBorders As Boolean

#End Region

#Region "EmployeeSpanningDirection Property"

    Public Property EmployeeSpanningDirection() As CellSpanningDirection
      Get
        Return m_employeeSpanningDirection
      End Get

      Set(ByVal value As CellSpanningDirection)
        If value <> m_employeeSpanningDirection Then
          m_employeeSpanningDirection = value
          Me.RaisePropertyChanged("EmployeeSpanningDirection")
        End If
      End Set
    End Property

    Private m_employeeSpanningDirection As CellSpanningDirection = CellSpanningDirection.Row

#End Region

#Region "CustomerSpanningDirection Property"

    Public Property CustomerSpanningDirection() As CellSpanningDirection
      Get
        Return m_customerSpanningDirection
      End Get

      Set(ByVal value As CellSpanningDirection)
        If value <> m_customerSpanningDirection Then
          m_customerSpanningDirection = value
          Me.RaisePropertyChanged("CustomerSpanningDirection")
        End If
      End Set
    End Property

    Private m_customerSpanningDirection As CellSpanningDirection = CellSpanningDirection.Row

#End Region

#Region "ShipViaSpanningDirection Property"

    Public Property ShipViaSpanningDirection() As CellSpanningDirection
      Get
        Return m_shipViaSpanningDirection
      End Get

      Set(ByVal value As CellSpanningDirection)
        If value <> m_shipViaSpanningDirection Then
          m_shipViaSpanningDirection = value
          Me.RaisePropertyChanged("ShipViaSpanningDirection")
        End If
      End Set
    End Property

    Private m_shipViaSpanningDirection As CellSpanningDirection = CellSpanningDirection.Row

#End Region

#Region "CitySpanningDirection Property"

    Public Property CitySpanningDirection() As CellSpanningDirection
      Get
        Return m_citySpanningDirection
      End Get

      Set(ByVal value As CellSpanningDirection)
        If value <> m_citySpanningDirection Then
          m_citySpanningDirection = value
          Me.RaisePropertyChanged("CitySpanningDirection")
        End If
      End Set
    End Property

    Private m_citySpanningDirection As CellSpanningDirection = CellSpanningDirection.Both

#End Region

#Region "CountrySpanningDirection Property"

    Public Property CountrySpanningDirection() As CellSpanningDirection
      Get
        Return m_countrySpanningDirection
      End Get

      Set(ByVal value As CellSpanningDirection)
        If value <> m_countrySpanningDirection Then
          m_countrySpanningDirection = value
          Me.RaisePropertyChanged("CountrySpanningDirection")
        End If
      End Set
    End Property

    Private m_countrySpanningDirection As CellSpanningDirection = CellSpanningDirection.Both

#End Region

#Region "SpannedCellSelector Property"

    Public ReadOnly Property SpannedCellSelector() As SpannedCellSelector
      Get
        If Me.UseCustomConfiguration Then
          Return m_spannedCellSelector
        Else
          Return Nothing
        End If
      End Get
    End Property

    Private ReadOnly m_spannedCellSelector As SpannedCellSelector

#End Region

#Region "SpannedCellConfigurationSelector Property"

    Public ReadOnly Property SpannedCellConfigurationSelector() As SpannedCellConfigurationSelector
      Get
        If Me.UseCustomConfiguration Then
          Return m_spannedCellConfigurationSelector
        Else
          Return Nothing
        End If
      End Get
    End Property

    Private ReadOnly m_spannedCellConfigurationSelector As SpannedCellConfigurationSelector

#End Region

#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub RaisePropertyChanged(ByVal name As String)
      If Me.PropertyChangedEvent IsNot Nothing Then
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
      End If
    End Sub

#End Region
  End Class
End Namespace
