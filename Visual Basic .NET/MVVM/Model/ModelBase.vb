'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ModelBase.vb]
' *  
' * This is the base class of models requiring PropertyChanged notifications.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
  Public Class ModelBase
    Implements INotifyPropertyChanged
#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal propertyName As String)
      Dim handler = Me.PropertyChangedEvent
      If handler Is Nothing Then
        Return
      End If

      handler.Invoke(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#End Region
  End Class
End Namespace
