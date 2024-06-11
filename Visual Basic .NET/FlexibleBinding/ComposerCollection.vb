'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [ComposersCollection.vb]
'
' This class contains a basic collection of composer objects
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
  Public Class ComposersCollection
    Inherits CollectionBase
    Implements ITypedList
    Public Sub New()
    End Sub

#Region "Implementation for the typed collection"

    ' Add a composer to the collection.  Used to fill the collection.
    Public Sub Add(ByVal composer As Composer)
      Me.InnerList.Add(composer)
    End Sub

    Default Public Property Item(ByVal index As Integer) As Composer
      Get
        Return CType(Me.InnerList(index), Composer)
      End Get
      Set(ByVal value As Composer)
        Me.InnerList(index) = value
      End Set
    End Property

    Protected Overrides Sub OnValidate(ByVal value As Object)
      ' Check to make sure that the element is not null
      MyBase.OnValidate(value)

      ' Check to make sure that the element is a ComposerEditable object
      If Not (TypeOf value Is Composer) Then
        Throw New ArgumentException("Only Composer objects can be added to the collection!")
      End If
    End Sub

#End Region ' Implementation for the typed collection

#Region "Implementation of ITypedList"

    Public Function GetItemProperties(ByVal properties As PropertyDescriptor()) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
      ' We sort the property descriptor to have the column in our grid in that order by default.
      Dim orignal As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Composer))
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
  End Class
End Namespace
