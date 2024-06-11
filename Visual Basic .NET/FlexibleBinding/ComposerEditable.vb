'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [ComposerEditable.vb]
'
' This class derives from the Composer class and
' implements the IEditableObject interface needed for the
' ComposerBindingListComplete to work correctly.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports Microsoft.VisualBasic

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  ' The ComposerBindingListComplete uses elements of this type.
  Public Class ComposerEditable
    Inherits Composer
    Implements IEditableObject
    Public Sub New()
      MyBase.New()
    End Sub

    ' Create a new instance of the ComposerEditable class.
    Public Sub New(ByVal lastName As String, ByVal firstName As String, ByVal period As Period, ByVal birthYear As Integer, ByVal deathYear As Integer)
      MyBase.New(lastName, firstName, period, birthYear, deathYear)
    End Sub

#Region "Implementation of IEditableObject"

    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
      If m_isBeingEdited Then
        Return
      End If

      m_isBeingEdited = True

      ' Keep a copy of the original values in case CancelEdit is called.
      m_oldValues(0) = m_firstName
      m_oldValues(1) = m_lastName
      m_oldValues(2) = m_period
      m_oldValues(3) = m_birthYear
      m_oldValues(4) = m_deathYear
    End Sub

    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
      ' Make sure that the object is actually being edited.
      If (Not m_isBeingEdited) Then
        Return
      End If

      ' Return the values to their original values
      m_firstName = CStr(m_oldValues(0))
      m_lastName = CStr(m_oldValues(1))
      m_period = CType(m_oldValues(2), Period)
      m_birthYear = CInt(Fix(m_oldValues(3)))
      m_deathYear = CInt(Fix(m_oldValues(4)))
      m_isBeingEdited = False

      If Not m_containingList Is Nothing Then
        If (CType(m_containingList, ComposersBindingListComplete)).LastAddedComposer Is Me Then 'AddNew
          ' If the element is coming from an AddNew and EndEdit has not been called yet,
          ' we have to remove the row from our containing list.
          m_containingList.Remove(Me)
        Else
          ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End If
    End Sub

    Public Sub EndEdit() Implements IEditableObject.EndEdit
      ' Make sure that the object is actually being edited.
      If (Not m_isBeingEdited) Then
        Return
      End If

      ' There is nothing to do with the temporary values that we kept
      ' since the values are already committed. Therefore, we clear
      ' the temporary object array.
      Array.Clear(m_oldValues, 0, m_oldValues.Length)
      m_isBeingEdited = False

      If (CType(m_containingList, ComposersBindingListComplete)).LastAddedComposer Is Me Then ' AddNew
        ' If the element is coming from an AddNew and EndEdit was never called before,
        ' we have to advise the grid by raising a ListChanged with ListChangedType.ItemAdded a second time.
        Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, m_containingList.IndexOf(Me)))
        CType(m_containingList, ComposersBindingListComplete).LastAddedComposer = Nothing
      End If
    End Sub

#End Region ' Implementation of IEditableObject

    ' Raise the ListChanged event of our containing list.
    Protected Overrides Sub InvokeListChanged(ByVal e As ListChangedEventArgs)
      If Not m_isBeingEdited Then
        If Not m_containingList Is Nothing Then
          CType(m_containingList, ComposersBindingListComplete).InvokeOnListChanged(e)
        End If
      End If
    End Sub

    Private m_oldValues As Object() = New Object(4) {}
    Private m_isBeingEdited As Boolean = False
  End Class
End Namespace
