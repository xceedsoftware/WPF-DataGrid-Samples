'
' Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [Person.vb]
'  
' Class that exposes information about a person and to which "extra"
' properties can be added through a custom PropertyDescriptor.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
  Public Class Person
    Implements ICustomTypeDescriptor
    Public Sub New()

      MyBase.New()
      Dim chars(0) As Char
      chars(0) = " "

      Dim names As String() = RandomValueGenerator.GetRandomName().Split(chars)
      m_firstName = names(0).Trim()
      m_lastName = names(1).Trim()

      Dim currentYear As Integer = DateTime.UtcNow.Year

      m_age = RandomValueGenerator.GetRandomInteger(18, 65)
      m_hireDate = RandomValueGenerator.GetRandomDateTime(1990, currentYear)
      m_children = RandomValueGenerator.GetRandomInteger(0, 4)
      m_employed = RandomValueGenerator.GetRandomBool()
      m_phone = RandomValueGenerator.GetRandomPhoneNumber()
      m_department = RandomValueGenerator.GetRandomDepartment()
      m_boss = RandomValueGenerator.GetRandomName()
      m_reviewDate = RandomValueGenerator.GetRandomDateTime(m_hireDate.Year, currentYear)

      m_availableColumnTypes.Add(GetType(String))
      m_availableColumnTypes.Add(GetType(Integer))
      m_availableColumnTypes.Add(GetType(DateTime))
      m_availableColumnTypes.Add(GetType(Boolean))
    End Sub

    Public Sub New(ByVal additionalFieldCount As Integer)
      Me.New()
      m_additionalFieldCount = additionalFieldCount
    End Sub

#Region "Default Person Properties"

    Public ReadOnly Property FirstName() As String
      Get
        Return m_firstName
      End Get
    End Property

    Public ReadOnly Property LastName() As String
      Get
        Return m_lastName
      End Get
    End Property

    Public ReadOnly Property Age() As Integer
      Get
        Return m_age
      End Get
    End Property

    Public ReadOnly Property HireDate() As DateTime
      Get
        Return m_hireDate
      End Get
    End Property

    Public ReadOnly Property Children() As Integer
      Get
        Return m_children
      End Get
    End Property

    Public ReadOnly Property Employed() As Boolean
      Get
        Return m_employed
      End Get
    End Property

    Public ReadOnly Property PhoneNumber() As String
      Get
        Return m_phone
      End Get
    End Property

    Public ReadOnly Property Department() As String
      Get
        Return m_department
      End Get
    End Property

    Public ReadOnly Property Boss() As String
      Get
        Return m_boss
      End Get
    End Property

    Public ReadOnly Property ReviewDate() As DateTime
      Get
        Return m_reviewDate
      End Get
    End Property

#End Region

    ' Get the default property count, which is used to determine the actaul
    ' number of custom PropertyDescriptors to "attach" to the Person class.
    Public Shared ReadOnly Property DefaultPropertyCount() As Integer
      Get

        If m_defaultPropertyCount = 0 Then
          m_defaultPropertyCount = TypeDescriptor.GetProperties(GetType(Person)).Count
        End If

        Return m_defaultPropertyCount
      End Get
    End Property

    ' Returns a new PropertyDescriptor that is created based on the specified type.
    Private Function GetNewPropertyDescriptor(ByVal type As Type) As PropertyDescriptor
      Dim newProperty As PropertyDescriptor = New CustomPropertyDescriptor(type.ToString().Remove(0, 7) + " Column " + m_index.ToString(), GetType(Person), type, True)

      m_index += 1
      Return newProperty
    End Function

#Region "ICustomTypeDescriptor"

    Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
      Return AttributeCollection.Empty
    End Function

    Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
      Return Nothing
    End Function

    Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
      Return Nothing
    End Function

    Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
      Return Nothing
    End Function

    Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
      Return Nothing
    End Function

    Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
      Return Nothing
    End Function

    Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
      Return Nothing
    End Function

    Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
      Return EventDescriptorCollection.Empty
    End Function

    Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
      Return EventDescriptorCollection.Empty
    End Function

    Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
      ' If there are no additional fields to add, return the default properties.
      If m_additionalFieldCount <= 0 Then
        Return TypeDescriptor.GetProperties(Me, True)
      End If

      If m_combinedProperties Is Nothing Then
        ' Get a list of the default properties exposed by the Person class and add them to
        ' a collection that will also contain the new PropertyDescriptors
        Dim list As New List(Of PropertyDescriptor)
        Dim baseProperties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(Me, True)

        Dim prop As PropertyDescriptor
        For Each prop In baseProperties
          list.Add(prop)
        Next prop

        ' Create a new PropertyDescriptor according to the type at the specified index. 
        Dim index As Integer = 0
        Dim i As Integer

        For i = 0 To (m_additionalFieldCount - 1)

          list.Add(Me.GetNewPropertyDescriptor(Person.m_availableColumnTypes(index)))

          If index < Person.m_availableColumnTypes.Count - 1 Then
            index += 1
          Else
            index = 0
          End If
        Next i

        ' Add the new PropertyDescriptors to the collection.
        m_combinedProperties = New PropertyDescriptorCollection(list.ToArray())
      End If

      Return m_combinedProperties
    End Function

    Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
      Return Me.GetProperties(Nothing)
    End Function

    Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
      Return Me
    End Function

#End Region

    Private Shared m_defaultPropertyCount As Integer = 0
    Private Shared m_availableColumnTypes As New List(Of Type)

    ' Default Person properties
    Private m_firstName As String = String.Empty
    Private m_lastName As String = String.Empty
    Private m_phone As String = String.Empty
    Private m_department As String = String.Empty
    Private m_boss As String = String.Empty
    Private m_reviewDate As DateTime = DateTime.MinValue
    Private m_hireDate As DateTime = DateTime.MinValue
    Private m_age As Integer = 18
    Private m_children As Integer = 0
    Private m_employed As Boolean = True

    ' Used in GetProperties and GetNewPropertyDescriptor methods to create
    ' custom PropertyDescriptors.
    Private m_combinedProperties As PropertyDescriptorCollection
    Private m_additionalFieldCount As Integer = 0
    Private m_index As Integer = 0
  End Class
End Namespace
