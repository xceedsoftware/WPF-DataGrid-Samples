' 
' * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [Person.vb] 
' * 
' * This class represents a typical business object used by 
' * the DataVirtualization Sample. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' 


Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  Friend Class Person
    Private Shared Bosses As String() = New String(9) {"Mr. " & RandomValueGenerator.GetRandomName(), "Mr. " & RandomValueGenerator.GetRandomName(), "Ms. " & RandomValueGenerator.GetRandomName(), "Mr. " & RandomValueGenerator.GetRandomName(), "Mr. " & RandomValueGenerator.GetRandomName(), "Dr. " & RandomValueGenerator.GetRandomName(), _
    "Mlle. " & RandomValueGenerator.GetRandomName(), "Mr. " & RandomValueGenerator.GetRandomName(), "Ms. " & RandomValueGenerator.GetRandomName(), "Mr. " & RandomValueGenerator.GetRandomName()}

    Private Shared CurrentYear As Integer = DateTime.Now.Year

    Public Sub New()
      MyBase.New()
      Dim names As String() = RandomValueGenerator.GetRandomName().Split(New Char(0) {" "c})
      m_firstName = names(0).Trim()
      m_lastName = names(1).Trim()

      m_age = RandomValueGenerator.GetRandomInteger(18, 65)
      m_hireDate = RandomValueGenerator.GetRandomDateTime(1990, DateTime.Now.Year)

      If RandomValueGenerator.GetRandomBool() Then
        m_children = RandomValueGenerator.GetRandomInteger(0, 3)
      ElseIf RandomValueGenerator.GetRandomBool() Then
        m_children = RandomValueGenerator.GetRandomInteger(1, 5)
      Else
        m_children = RandomValueGenerator.GetRandomInteger(3, 6)
      End If


      m_employed = RandomValueGenerator.GetRandomBool()
      m_phone = RandomValueGenerator.GetRandomPhoneNumber()
      m_department = RandomValueGenerator.GetRandomDepartment()

      m_boss = Person.Bosses(RandomValueGenerator.GetRandomInteger(0, 10))


      m_reviewDate = RandomValueGenerator.GetRandomDateTime(m_hireDate.Year, DateTime.Now.Year)
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
  End Class
End Namespace
