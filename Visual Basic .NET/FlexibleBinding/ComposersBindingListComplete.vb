'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [ComposersBindingListComplete.vb]
'
' This class contains a collection of composer object which implement
' the IBindingList interface with all the functionnality needed to support
' deletion, insertion and modification.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Diagnostics

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  Public Class ComposersBindingListComplete
    Inherits CollectionBase
    Implements IBindingList, ITypedList, ISupportInitialize
    Public Sub New()
    End Sub

#Region "Implementation for the typed collection"

    ' Add a composer to the collection.  Used to fill the collection.
    Public Sub Add(ByVal composer As ComposerEditable)
      Me.List.Add(composer)
    End Sub

    Default Public Property Item(ByVal index As Integer) As ComposerEditable
      Get
        Return CType(Me.List(index), ComposerEditable)
      End Get
      Set(ByVal value As ComposerEditable)
        Me.List(index) = value
      End Set
    End Property

    Protected Overrides Sub OnValidate(ByVal value As Object)
      ' Check to make sure that the element is not null
      MyBase.OnValidate(value)

      ' Check to make sure that the element is a ComposerEditable object
      If Not (TypeOf value Is ComposerEditable) Then
        Throw New ArgumentException("Only ComposerEditable objects can be added to the collection")
      End If
    End Sub

#End Region ' Implementation for the typed collection

#Region "Implementation of IBindingList"

    ' Return a new ComposerEditable object already added to the collection.
    ' The grid will use it to insert a new row.
    Public Function AddNew() As Object Implements IBindingList.AddNew
      ' Create a new ComposerEditable object
      Dim composer As ComposerEditable = New ComposerEditable()

      ' Put the composer into a state where calling CancelEdit will remove the composer from the collection.
      composer.BeginEdit()

      ' Add the composer to the collection.
      Me.Add(composer)

      ' Keep track of the last composer added via AddNew.
      m_lastAddedComposer = composer

      Return composer
    End Function

    ' To tell that we support the AddNew method.
    Public ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
      Get
        Return True
      End Get
    End Property

    ' To tell that we support the removal of elements.
    Public ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
      Get
        Return True
      End Get
    End Property

    ' To tell that we support the modification of elements.
    Public ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
      Get
        Return True
      End Get
    End Property

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
      Get
        Return False
      End Get
    End Property

    ' Since the grid does not need to search, we do not need
    ' to enable searching in the collection.
    Public ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
      Get
        Return False
      End Get
    End Property

    ' To tell that we will raise the ListChanged event.
    Public ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
      Get
        Return True
      End Get
    End Property

    ' Since the grid does not need to search, we do not need
    ' to enable searching in the collection.
    Public Sub AddIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.AddIndex
      Throw New NotSupportedException()
    End Sub

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
      Throw New NotSupportedException()
    End Sub

    ' Since the grid does not need to search, we do not need
    ' to enable searching in the collection.
    Public Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
      Throw New NotSupportedException()
    End Function

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public Sub RemoveSort() Implements IBindingList.RemoveSort
      Throw New NotSupportedException()
    End Sub

    ' Since the grid does not need to search, we do not need
    ' to enable searching in the collection.
    Public Sub RemoveIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
      Throw New NotSupportedException()
    End Sub

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
      Get
        Throw New NotSupportedException()
      End Get
    End Property

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
      Get
        Throw New NotSupportedException()
      End Get
    End Property

    ' Since the grid will take care of sorting the data rows
    ' we do not need to enable sorting in the collection.
    Public ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
      Get
        Throw New NotSupportedException()
      End Get
    End Property

    ' Raise the ListChanged event to advise the grid of any changes.
    Protected Overridable Sub OnListChanged(ByVal e As ListChangedEventArgs)
      If Not Me.ListChangedEvent Is Nothing Then
        RaiseEvent ListChanged(Me, e)
      End If
    End Sub

    Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
      CType(value, ComposerEditable).m_containingList = Nothing

      ' Advise the grid that a row has been removed.
      Me.InvokeOnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
    End Sub

    Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
      ' Commit the previously pending AddNew.
      If Not m_lastAddedComposer Is Nothing Then
        m_lastAddedComposer.EndEdit()
        m_lastAddedComposer = Nothing
      End If
    End Sub

    Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
      CType(value, ComposerEditable).m_containingList = Me

      ' Advise the grid that a new row has been inserted.
      Me.InvokeOnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
    End Sub

    Protected Overrides Sub OnClearComplete()
      ' Advise the grid that the collection has been reset.
      Me.InvokeOnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1, -1))
    End Sub

    Protected Overrides Sub OnClear()
      For Each composer As ComposerEditable In Me
        composer.m_containingList = Nothing
      Next composer
    End Sub

    Protected Overrides Sub OnSetComplete(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
      If oldValue Is newValue Then
        Return
      End If

      ' We do not check if the old and new values are null since it is impossible.      
      CType(oldValue, ComposerEditable).m_containingList = Nothing
      CType(newValue, ComposerEditable).m_containingList = Me

      Me.InvokeOnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
    End Sub

    Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

#End Region ' Implementation of IBindingList

#Region "Implementation of ITypedList"

    Public Function GetItemProperties(ByVal properties As PropertyDescriptor()) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
      ' We sort the property descriptor to have the columns in our grid in that order by default.
      Dim orignal As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(ComposerEditable))
      Dim sorted As PropertyDescriptorCollection = New PropertyDescriptorCollection(Nothing)

      sorted.Add(orignal("LastName"))
      sorted.Add(orignal("FirstName"))
      sorted.Add(orignal("Period"))
      sorted.Add(orignal("BirthYear"))
      sorted.Add(orignal("DeathYear"))

      Debug.Assert(orignal.Count = sorted.Count)

      Return sorted
    End Function

    Public Function GetListName(ByVal listAccessors As PropertyDescriptor()) As String Implements ITypedList.GetListName
      Return Me.GetType().Name
    End Function

#End Region ' Implementation of ITypedList

#Region "Implementation of ISupportInitialize"

    ' Indicates if we are in batch initialization.
    Public ReadOnly Property InBatchInitialization() As Boolean
      Get
        Return m_initCounter > 0
      End Get
    End Property

    ' Begins the batch initialization.
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
      m_initCounter += 1
    End Sub

    ' Ends the batch initialization.
    Public Sub EndInit() Implements ISupportInitialize.EndInit
      If m_initCounter > 0 Then
        m_initCounter -= 1

        If m_initCounter = 0 Then
          Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1, -1))
        End If
      End If
    End Sub

#End Region ' Implementation of ISupportInitialize

    ' To keep track of the last added row via AddNew.
    Friend Property LastAddedComposer() As ComposerEditable
      Get
        Return m_lastAddedComposer
      End Get
      Set(ByVal value As ComposerEditable)
        m_lastAddedComposer = value
      End Set
    End Property

    ' Raises the ListChanged event.
    Friend Sub InvokeOnListChanged(ByVal e As ListChangedEventArgs)
      ' Prevents the raise of the ListChanged event if we are in batch initialisation.
      If (Not Me.InBatchInitialization) Then
        Me.OnListChanged(e)
      End If
    End Sub

    Private m_initCounter As Integer = 0
    Private m_lastAddedComposer As ComposerEditable = Nothing
  End Class
End Namespace
