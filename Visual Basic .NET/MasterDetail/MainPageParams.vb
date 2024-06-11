' 
' Xceed DataGrid for WPF - SAMPLE CODE - Master/detail Sample Application 
' Copyright (c) 2007-2024 Xceed Software Inc. 
' 
' [MainPageParams.vb] 
' 
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' 
' 

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
  Public Class MainPageParams
    Implements INotifyPropertyChanged
    Public Shared ReadOnly Singleton As New MainPageParams()

#Region "CONSTRUCTORS"

    Private Sub New()
    End Sub

#End Region

#Region "AllowDetailToggle Property"

    Private _allowDetailToggle As Boolean = True

    Public Property AllowDetailToggle() As Boolean
      Get
        Return _allowDetailToggle
      End Get

      Set(ByVal value As Boolean)
        If _allowDetailToggle = Value Then
          Return
        End If

        _allowDetailToggle = Value
        Me.OnPropertyChanged(New PropertyChangedEventArgs("AllowDetailToggle"))
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
