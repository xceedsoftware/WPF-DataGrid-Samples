'
' Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MainPage.xaml.vb]
'
' This class implements the various dynamic configuration options offered
' by the configuration panel declared in MainPage.xaml.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Data
Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Windows.Controls
Imports System.Data
Imports System.Net
Imports System.Text

Imports Xceed.Wpf.Controls
Imports Xceed.Wpf.DataGrid



Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page

    Private Shared RandomDataTable As DataTable
    Private Shared Randomizer As New Random()
    Private Shared DepartmentsList As New List(Of String)

    Private Const Consonnants As String = "bcdfgjklmnprstv"
    Private Const Vowels As String = "aeiou"

    Private Shared Function GetRandomIPAddress() As IPAddress

      Dim bytes(3) As Byte

      MainPage.Randomizer.NextBytes(bytes)

      Return New IPAddress(bytes)

    End Function


    Private Shared Function GetRandomBool() As Boolean

      Return (MainPage.Randomizer.Next(0, 2) = 1)

    End Function

    Private Shared Function GetRandomName() As String

      Dim firstNameSyllableLength As Integer = MainPage.Randomizer.Next(2, 5)
      Dim lastNameSyllableLength As Integer = MainPage.Randomizer.Next(3, 7)

      Dim totalSyllableLength As Integer = firstNameSyllableLength + lastNameSyllableLength

      Dim nameBuilder As New StringBuilder()

      For i As Integer = 0 To totalSyllableLength - 1

        Dim consonnant As Char = MainPage.Consonnants(MainPage.Randomizer.Next(0, MainPage.Consonnants.Length))
        Dim vowel As Char = MainPage.Vowels(MainPage.Randomizer.Next(0, MainPage.Vowels.Length))

        If (i = firstNameSyllableLength) Then
          nameBuilder.Append(" ")
        End If

        If ((i = 0) Or (i = firstNameSyllableLength)) Then
          nameBuilder.Append(consonnant.ToString().ToUpper())
        Else
          nameBuilder.Append(consonnant)
        End If

        nameBuilder.Append(vowel)
      Next

      Return nameBuilder.ToString()

    End Function

    Private Shared Function GetRandomInteger() As Integer

      Return MainPage.Randomizer.Next(0, 13)

    End Function

    Private Shared Function GetRandomDouble(ByVal multiplier As Double) As Double

      Return Math.Round((MainPage.Randomizer.NextDouble() * multiplier), 2)

    End Function

    Private Shared Function GetRandomPhoneNumber() As String

      Return "(555) 555-" & MainPage.Randomizer.Next(0, 10) & MainPage.Randomizer.Next(0, 10) & MainPage.Randomizer.Next(0, 10) & MainPage.Randomizer.Next(0, 10)

    End Function

    Private Shared Function GetRandomDepartment() As String

      Return MainPage.DepartmentsList(Randomizer.Next(0, MainPage.DepartmentsList.Count))

    End Function

    Private Shared Function GetRandomDateTime() As DateTime

      Return New DateTime(MainPage.Randomizer.Next(1990, 2010), MainPage.Randomizer.Next(1, 13), MainPage.Randomizer.Next(1, 28), MainPage.Randomizer.Next(0, 23), MainPage.Randomizer.Next(0, 60), MainPage.Randomizer.Next(0, 60))

    End Function

    Shared Sub New()

      MainPage.DepartmentsList = New List(Of String)()
      MainPage.DepartmentsList.Add("Development")
      MainPage.DepartmentsList.Add("Direction")
      MainPage.DepartmentsList.Add("Human Resources")
      MainPage.DepartmentsList.Add("Networking")
      MainPage.DepartmentsList.Add("Sales")
      MainPage.DepartmentsList.Add("Support")

      MainPage.RandomDataTable = New DataTable()

      MainPage.RandomDataTable.Columns.Add(New DataColumn("Included", GetType(Boolean)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("Name", GetType(String)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("IPAddress", GetType(IPAddress)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("PhoneNumber", GetType(String)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("Category", GetType(String)))

      MainPage.RandomDataTable.Columns.Add(New DataColumn("Price", GetType(Double)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("Scientific", GetType(Double)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("Quantity", GetType(Integer)))

      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime1", GetType(DateTime)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime2", GetType(DateTime)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime3", GetType(DateTime)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime4", GetType(DateTime)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime5", GetType(DateTime)))
      MainPage.RandomDataTable.Columns.Add(New DataColumn("DateTime6", GetType(DateTime)))

      For i As Integer = 0 To 29

        Dim values() As Object = {MainPage.GetRandomBool(), _
        MainPage.GetRandomName(), _
        MainPage.GetRandomIPAddress(), _
        MainPage.GetRandomPhoneNumber(), _
        MainPage.GetRandomDepartment(), _
        MainPage.GetRandomDouble(100), _
        MainPage.GetRandomDouble(Double.MaxValue), _
        MainPage.GetRandomInteger(), _
        MainPage.GetRandomDateTime(), _
        MainPage.GetRandomDateTime(), _
        MainPage.GetRandomDateTime(), _
        MainPage.GetRandomDateTime(), _
        MainPage.GetRandomDateTime(), _
        MainPage.GetRandomDateTime()}

        MainPage.RandomDataTable.Rows.Add(values)

      Next

    End Sub


    Public Sub New()

      InitializeComponent()

    End Sub

    Public ReadOnly Property RandomData() As DataTable
      Get
        Return MainPage.RandomDataTable
      End Get
    End Property

    Public ReadOnly Property Departments() As List(Of String)
      Get
        Return MainPage.DepartmentsList
      End Get
    End Property
  End Class
End Namespace
