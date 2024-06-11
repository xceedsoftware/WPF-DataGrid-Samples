'
' Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [RandomValueGenerator.vb]
'  
' Utility class that provides random values, which will be used to populate 
' the generated Person objects.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
  Friend Class RandomValueGenerator
    Shared Sub New()
      DepartmentsList.Add("Development")
      DepartmentsList.Add("Direction")
      DepartmentsList.Add("Human Resources")
      DepartmentsList.Add("Networking")
      DepartmentsList.Add("Sales")
      DepartmentsList.Add("Support")
    End Sub

    Public Shared Function GetRandomValue(ByVal type As Type) As Object
      If type Is GetType(Boolean) Then
        Return GetRandomBool()
      ElseIf type Is GetType(Integer) Then
        Return GetRandomInteger(0, 1000)
      ElseIf type Is GetType(String) Then
        Return GetRandomName()
      ElseIf type Is GetType(DateTime) Then
        Return GetRandomDateTime(1990, DateTime.Now.Year)
      Else
        Return GetRandomPhoneNumber()
      End If

      Return Nothing

    End Function

    Public Shared Function GetRandomBool() As Boolean
      Return Randomizer.Next(0, 2) = 1
    End Function

    Public Shared Function GetRandomName() As String

      Dim firstNameSyllableLength As Integer = Randomizer.Next(2, 5)
      Dim lastNameSyllableLength As Integer = Randomizer.Next(3, 7)

      Dim totalSyllableLength As Integer = firstNameSyllableLength + lastNameSyllableLength

      Dim nameBuilder As New StringBuilder()

      Dim i As Integer
      For i = 0 To totalSyllableLength - 1

        Dim consonnant As Char = Consonnants(Randomizer.Next(0, Consonnants.Length))
        Dim vowel As Char = Vowels(Randomizer.Next(0, Vowels.Length))

        If i = firstNameSyllableLength Then
          nameBuilder.Append(" ")
        End If

        If (i = 0) Or (i = firstNameSyllableLength) Then
          nameBuilder.Append(consonnant.ToString().ToUpper())
        Else
          nameBuilder.Append(consonnant)
        End If

        nameBuilder.Append(vowel)
      Next i

      Return nameBuilder.ToString()
    End Function

    Public Shared Function GetRandomInteger(ByVal min As Integer, ByVal max As Integer) As Integer
      Return Randomizer.Next(min, max)
    End Function

    Public Shared Function GetRandomPhoneNumber() As String
      Return "(555) 555-" + (Randomizer.Next(0, 10) + Randomizer.Next(0, 10) + Randomizer.Next(0, 10) + Randomizer.Next(0, 10)).ToString()
    End Function

    Public Shared Function GetRandomDepartment() As String
      Return DepartmentsList(Randomizer.Next(0, DepartmentsList.Count))
    End Function

    Public Shared Function GetRandomDateTime(ByVal minYear As Integer, ByVal maxYear As Integer) As DateTime
      Debug.Assert(minYear <= maxYear)

      Return New DateTime(Randomizer.Next(minYear, maxYear), Randomizer.Next(1, 13), Randomizer.Next(1, 28), Randomizer.Next(0, 23), Randomizer.Next(0, 60), Randomizer.Next(0, 60))
    End Function

    Public Shared ReadOnly Property Departments() As List(Of String)
      Get
        Return DepartmentsList
      End Get
    End Property


    Private Shared Randomizer As New Random()
    Private Shared DepartmentsList As New List(Of String)

    Private Const Consonnants As String = "bcdfgjklmnprstv"
    Private Const Vowels As String = "aeiou"
  End Class
End Namespace
