'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [RelayCommand.vb]
' *  
' * This class implements the ICommand interface, which is then used in the ViewModel to create commands that are consumed by the View.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Windows.Input

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
  Public Class RelayCommand
    Implements ICommand
    Private ReadOnly m_canExecute As Predicate(Of Object)
    Private ReadOnly m_execute As Action(Of Object)

    Public Sub New(ByVal execute As Action(Of Object))
      Me.New(execute, Nothing)
    End Sub

    Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Predicate(Of Object))
      If execute Is Nothing Then
        Throw New ArgumentNullException("execute")
      End If

      m_execute = execute
      m_canExecute = canExecute
    End Sub

#Region "ICommand Members"

    Private Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
      AddHandler(ByVal value As EventHandler)
        AddHandler CommandManager.RequerySuggested, value
      End AddHandler

      RemoveHandler(ByVal value As EventHandler)
        RemoveHandler CommandManager.RequerySuggested, value
      End RemoveHandler
      RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
      End RaiseEvent
    End Event

    Private Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
      Return If(m_canExecute IsNot Nothing, m_canExecute(parameter), True)
    End Function

    Private Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
      m_execute(parameter)
    End Sub

#End Region
  End Class
End Namespace