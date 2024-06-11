'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [Composer.vb]
'
' This class contains information about a composer and advises its containing list
' when the value of one of the properties is changing.
' It is used by the ComposerBindingList and the ComposerCollection.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  ' The ComposerBindingList and the ComposerCollection use elements of this type.
  Public Class Composer
    Public Sub New()
    End Sub

    ' Create a new instance of the Composer class.
    Public Sub New(ByVal lastName As String, ByVal firstName As String, ByVal period As Period, ByVal birthYear As Integer, ByVal deathYear As Integer)
      m_lastName = lastName
      m_firstName = firstName
      m_period = period
      m_birthYear = birthYear
      m_deathYear = deathYear
    End Sub

    Public Property LastName() As String
      Get
        Return m_lastName
      End Get

      Set(ByVal value As String)
        m_lastName = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If Not m_containingList Is Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property

    Public Property FirstName() As String
      Get
        Return m_firstName
      End Get

      Set(ByVal value As String)
        m_firstName = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If Not m_containingList Is Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property

    Public Property Period() As Period
      Get
        Return m_period
      End Get

      Set(ByVal value As Period)
        m_period = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If Not m_containingList Is Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property

    Public Property BirthYear() As Integer
      Get
        Return m_birthYear
      End Get

      Set(ByVal value As Integer)
        ' Check the year to make sure that it is something reasonable
        If (value > DateTime.Now.Year) OrElse (value < 0) Then
          Throw New ArgumentOutOfRangeException("BirthYear", value, "The date must be less than " & DateTime.Now.Year.ToString() & " and greater than 0")
        End If

        m_birthYear = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If Not m_containingList Is Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property

    Public Property DeathYear() As Integer
      Get
        Return m_deathYear
      End Get

      Set(ByVal value As Integer)
        ' Check the year to make sure that it is something reasonable
        If (value > DateTime.Now.Year) OrElse (value < 0) Then
          Throw New ArgumentOutOfRangeException("DeathYear", value, "The date must be less than " & DateTime.Now.Year.ToString() & " and greater than 0")
        End If

        m_deathYear = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If Not m_containingList Is Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property

    ' Raise the ListChanged event of our containing list.
    Protected Overridable Sub InvokeListChanged(ByVal e As ListChangedEventArgs)
      CType(m_containingList, ComposersBindingList).InvokeOnListChanged(e)
    End Sub

    Protected m_lastName As String = String.Empty
    Protected m_firstName As String = String.Empty
    Protected m_period As Period = Period.Undefined
    Protected m_birthYear As Integer = DateTime.MinValue.Year
    Protected m_deathYear As Integer = DateTime.MaxValue.Year

    Friend m_containingList As IList = Nothing
  End Class
End Namespace
