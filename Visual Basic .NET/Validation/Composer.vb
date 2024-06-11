'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [Composer.vb]
'  
' This class contains information about a composer and advises its containing list
' when the value of one of the properties is changing.
' It is used by the ComposerBindingList.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
' 

Imports System
Imports System.Collections
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  ' The ComposerBindingList and the ComposerCollection use elements of this type.
  Public Class Composer
    Implements IDataErrorInfo

    Public Sub New()
    End Sub

    ' Create a new instance of the Composer class.
    Public Sub New(ByVal lastName As String, ByVal firstName As String, ByVal period As Period, ByVal birthYear As Integer, ByVal deathYear As Integer, ByVal compositionCount As Integer, _
     ByVal lastUpdate As DateTime)
      m_lastName = lastName
      m_firstName = firstName
      m_period = period
      m_birthYear = birthYear
      m_deathYear = deathYear
      m_compositionCount = compositionCount
      m_lastUpdate = lastUpdate
    End Sub

    Public Property LastName() As String
      Get
        Return m_lastName
      End Get

      Set(ByVal value As String)
        If String.IsNullOrEmpty(value) Then
          Throw New ArgumentException("The composer last name is required.", LastName)
        End If

        m_lastName = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If m_containingList IsNot Nothing Then
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
        If m_containingList IsNot Nothing Then
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
        If m_containingList IsNot Nothing Then
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
        If value < 0 Then
          Throw New ArgumentOutOfRangeException("BirthYear", value, "The date must be greater than 0")
        End If

        m_birthYear = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If m_containingList IsNot Nothing Then
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
        If value < 0 Then
          Throw New ArgumentOutOfRangeException("DeathYear", value, "The date must be greater than 0")
        End If

        m_deathYear = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If m_containingList IsNot Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property


    Public Property CompositionCount() As Integer
      Get
        Return m_compositionCount
      End Get
      Set(ByVal value As Integer)
        m_compositionCount = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If m_containingList IsNot Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property


    Public Property LastUpdate() As DateTime
      Get
        Return m_lastUpdate
      End Get
      Set(ByVal value As DateTime)
        ' Check the year to make sure that it is something reasonable
        If value > DateTime.Now Then
          Throw New ArgumentOutOfRangeException("LastUpdate", value, "The date must be less than " + DateTime.Now.ToString())
        End If

        m_lastUpdate = value

        ' Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        If m_containingList IsNot Nothing Then
          Me.InvokeListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, m_containingList.IndexOf(Me)))
        End If
      End Set
    End Property


    ' Raise the ListChanged event of our containing list.
    Protected Overridable Sub InvokeListChanged(ByVal e As ListChangedEventArgs)
      DirectCast(m_containingList, ComposersBindingListComplete).InvokeOnListChanged(e)
    End Sub


    Friend m_containingList As IList = Nothing

    Protected m_lastName As String = String.Empty
    Protected m_firstName As String = String.Empty
    Protected m_period As Period = Period.Undefined
    Protected m_birthYear As Integer = DateTime.MinValue.Year
    Protected m_deathYear As Integer = DateTime.MaxValue.Year
    Protected m_compositionCount As Integer = 1
    Protected m_lastUpdate As DateTime = DateTime.Now

    Public ReadOnly Property [Error]() As String Implements System.ComponentModel.IDataErrorInfo.Error
      Get
        Return String.Empty
      End Get
    End Property

    Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements System.ComponentModel.IDataErrorInfo.Item
      Get
        Dim [error] As String = String.Empty

        Select Case columnName
          Case "BirthYear", "DeathYear"
            If (m_deathYear - BirthYear) > 99 Then
              [error] = "The composer would have lived a hundred years!"
            End If

            Exit Select
          Case "Period"

            If m_period = Period.Undefined Then
              [error] = "The composer's period should be specified if possible."
            End If

            Exit Select
          Case "FirstName"

            If [String].IsNullOrEmpty(m_firstName) Then
              [error] = "The composer's first name should be specified if possible."
            End If

            Exit Select

        End Select

        Return [error]
      End Get
    End Property

  End Class
End Namespace
