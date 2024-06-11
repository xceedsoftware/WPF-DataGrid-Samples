'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [PredefinedSettingsItem.vb]
'  
' Structure used for the predefined combo box binding.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings

  Public Class PredefinedSettingsItem
    Implements INotifyPropertyChanged
#Region "DisplayName Property"

    Public Property DisplayName() As String
      Get
        Return m_displayName
      End Get
      Set(ByVal value As String)
        If value <> m_displayName Then
          m_displayName = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("DisplayName"))
        End If
      End Set
    End Property

#End Region

#Region "XmlUri Property"

    Public Property XmlUri() As String
      Get
        Return m_xmlUri
      End Get
      Set(ByVal value As String)
        If value <> m_xmlUri Then
          m_xmlUri = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("XmlUri"))
        End If
      End Set
    End Property

#End Region

#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
      RaiseEvent PropertyChanged(Me, e)
    End Sub

#End Region

#Region "PRIVATE FIELDS"

    Private m_displayName As String
    Private m_xmlUri As String

#End Region
  End Class
End Namespace
