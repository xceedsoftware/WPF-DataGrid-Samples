' 
' * Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [MainPageParams.cs] 
' * 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' * 
' 


Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Public Class MainPageParams
    Implements INotifyPropertyChanged
    Public Shared ReadOnly Singleton As New MainPageParams()

#Region "CONSTRUCTORS"

    Private Sub New()
    End Sub

#End Region

#Region "FreeTextColumnsVisible"

    Private m_freeTextColumnsVisible As Boolean = True

    Public Property FreeTextColumnsVisible() As Boolean
      Get
        Return m_freeTextColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_freeTextColumnsVisible = value Then
          Return
        End If

        m_freeTextColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("FreeTextColumnsVisible"))
      End Set
    End Property

#End Region

#Region "MaskedTextColumnsVisible"

    Private m_maskedTextColumnsVisible As Boolean = True

    Public Property MaskedTextColumnsVisible() As Boolean
      Get
        Return m_maskedTextColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_maskedTextColumnsVisible = value Then
          Return
        End If

        m_maskedTextColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("MaskedTextColumnsVisible"))
      End Set
    End Property

#End Region

#Region "DateTimeColumnsVisible"

    Private m_dateTimeColumnsVisible As Boolean = True

    Public Property DateTimeColumnsVisible() As Boolean
      Get
        Return m_dateTimeColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_dateTimeColumnsVisible = value Then
          Return
        End If

        m_dateTimeColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("DateTimeColumnsVisible"))
      End Set
    End Property

#End Region

#Region "NumericColumnsVisible"

    Private m_numericColumnsVisible As Boolean = True

    Public Property NumericColumnsVisible() As Boolean
      Get
        Return m_numericColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_numericColumnsVisible = value Then
          Return
        End If

        m_numericColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("NumericColumnsVisible"))
      End Set
    End Property

#End Region

#Region "ComboBoxColumnsVisible"

    Private m_comboBoxColumnsVisible As Boolean = True

    Public Property ComboBoxColumnsVisible() As Boolean
      Get
        Return m_comboBoxColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_comboBoxColumnsVisible = value Then
          Return
        End If

        m_comboBoxColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("ComboBoxColumnsVisible"))
      End Set
    End Property

#End Region

#Region "CheckBoxColumnsVisible"

    Private m_checkBoxColumnsVisible As Boolean = True

    Public Property CheckBoxColumnsVisible() As Boolean
      Get
        Return m_checkBoxColumnsVisible
      End Get

      Set(ByVal value As Boolean)
        If m_checkBoxColumnsVisible = value Then
          Return
        End If

        m_checkBoxColumnsVisible = value

        Me.OnPropertyChanged(New PropertyChangedEventArgs("CheckBoxColumnsVisible"))
      End Set
    End Property

#End Region


#Region "INotifyPropertyChanged Members"

    Protected Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
      RaiseEvent PropertyChanged(Me, e)
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

  End Class

End Namespace
