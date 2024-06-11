'
' * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [Person.vb]
' *  
' * Class that exposes information about a person.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq.Expressions

Namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
  Public Class Person
	  Implements INotifyPropertyChanged
	#Region "Constructors"

	Public Sub New()
		MyBase.New()
	  Dim names() As String = RandomValueGenerator.GetRandomName().Split(New Char(0) { " "c })
	  m_firstName = names(0).Trim()
	  m_lastName = names(1).Trim()

	  Dim currentYear = DateTime.UtcNow.Year

	  m_id = RandomValueGenerator.GetRandomInteger(100, 1000)
	  m_age = RandomValueGenerator.GetRandomInteger(18, 65)
	  m_hireDate = RandomValueGenerator.GetRandomDateTime(1990, currentYear)
	End Sub

	#End Region

	#Region "Person Properties"

	Public Property ID() As Integer
	  Get
		Return m_id
	  End Get
	  Set(ByVal value As Integer)
		m_id = value
		Me.OnPropertyChanged("ID")
	  End Set
	End Property

	Public Property FirstName() As String
	  Get
		Return m_firstName
	  End Get
	  Set(ByVal value As String)
		m_firstName = value
		Me.OnPropertyChanged("FirstName")
	  End Set
	End Property

	Public Property LastName() As String
	  Get
		Return m_lastName
	  End Get
	  Set(ByVal value As String)
		m_lastName = value
		Me.OnPropertyChanged("LastName")
	  End Set
	End Property

	Public Property Age() As Integer
	  Get
		Return m_age
	  End Get
	  Set(ByVal value As Integer)
		m_age = value
		Me.OnPropertyChanged("Age")
	  End Set
	End Property

	Public Property Department() As String
	  Get
		If String.IsNullOrEmpty(m_department) Then
		  System.Threading.Thread.Sleep(1000)
		  m_department = RandomValueGenerator.GetRandomDepartment()
		End If
		Return m_department
	  End Get
	  Set(ByVal value As String)
		If m_department = value Then
		  Return
		End If
		m_department = value
		Me.OnPropertyChanged("Department")
	  End Set
	End Property

	Public Property Boss() As String
	  Get
		If String.IsNullOrEmpty(m_boss) Then
		  System.Threading.Thread.Sleep(2500)
		  m_boss = RandomValueGenerator.GetRandomName()
		End If
		Return m_boss
	  End Get
	  Set(ByVal value As String)
		If m_boss = value Then
		  Return
		End If
		m_boss = value
		Me.OnPropertyChanged("Boss")
	  End Set
	End Property

	Public Property HireDate() As DateTime
	  Get
		Return m_hireDate
	  End Get
	  Set(ByVal value As DateTime)
		m_hireDate = value
		Me.OnPropertyChanged("HireDate")
	  End Set
	End Property

	#End Region ' Person Properties

	#Region "INotifyPropertyChanged handling"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

	Private Sub OnPropertyChanged(ByVal propertyName As String)
	RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
	End Sub

	#End Region

	#Region "Private Fields"

	' Person properties
	Private m_firstName As String = String.Empty
	Private m_lastName As String = String.Empty
	Private m_department As String = String.Empty
	Private m_boss As String = String.Empty
	Private m_hireDate As DateTime = DateTime.MinValue
	Private m_age As Integer = 18
	Private m_id As Integer = 100

	#End Region
  End Class
End Namespace