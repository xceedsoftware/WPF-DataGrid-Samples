'
' * Xceed DataGrid for WPF - SAMPLE CODE - Batch Updating Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [RandomValueGenerator.vb]
' *  
' * Utility class that provides random values, which will be used to populate 
' * the generated Person objects.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.BatchUpdating
  Friend NotInheritable Class RandomValueGenerator
	Private Sub New()
	End Sub
	Shared Sub New()
	  RandomValueGenerator.DepartmentsList.Add("Development")
	  RandomValueGenerator.DepartmentsList.Add("Direction")
	  RandomValueGenerator.DepartmentsList.Add("Human Resources")
	  RandomValueGenerator.DepartmentsList.Add("Networking")
	  RandomValueGenerator.DepartmentsList.Add("Sales")
	  RandomValueGenerator.DepartmentsList.Add("Support")
	End Sub

	Public Shared Function GetRandomValue(ByVal type As Type) As Object
	  If type Is GetType(Boolean) Then
		Return RandomValueGenerator.GetRandomBool()
	  ElseIf type Is GetType(Integer) Then
		Return RandomValueGenerator.GetRandomInteger(0, 1000)
	  ElseIf type Is GetType(String) Then
		Return RandomValueGenerator.GetRandomName()
	  ElseIf type Is GetType(DateTime) Then
		Return RandomValueGenerator.GetRandomDateTime(1990, DateTime.Now.Year)
	  Else
		Return RandomValueGenerator.GetRandomPhoneNumber()
	  End If
	End Function

	Public Shared Function GetRandomBool() As Boolean
	  Return (RandomValueGenerator.Randomizer.Next(0, 2) = 1)
	End Function

	Public Shared Function GetRandomName() As String
	  Dim firstNameSyllableLength As Integer = RandomValueGenerator.Randomizer.Next(2, 5)
	  Dim lastNameSyllableLength As Integer = RandomValueGenerator.Randomizer.Next(3, 7)

	  Dim totalSyllableLength As Integer = firstNameSyllableLength + lastNameSyllableLength

	  Dim nameBuilder As New StringBuilder()

	  For i As Integer = 0 To totalSyllableLength - 1
		Dim consonnant As Char = RandomValueGenerator.Consonnants(RandomValueGenerator.Randomizer.Next(0, RandomValueGenerator.Consonnants.Length))
		Dim vowel As Char = RandomValueGenerator.Vowels(RandomValueGenerator.Randomizer.Next(0, RandomValueGenerator.Vowels.Length))

		If i = firstNameSyllableLength Then
		  nameBuilder.Append(" "c)
		End If

		If (i = 0) OrElse (i = firstNameSyllableLength) Then
		  nameBuilder.Append(consonnant.ToString().ToUpper())
		Else
		  nameBuilder.Append(consonnant)
		End If

		nameBuilder.Append(vowel)
	  Next i

	  Return nameBuilder.ToString()
	End Function

	Public Shared Function GetRandomInteger(ByVal min As Integer, ByVal max As Integer) As Integer
	  Return RandomValueGenerator.Randomizer.Next(min, max)
	End Function

	Public Shared Function GetRandomPhoneNumber() As String
	  Return "(555) 555-" & RandomValueGenerator.Randomizer.Next(0, 10) + RandomValueGenerator.Randomizer.Next(0, 10) + RandomValueGenerator.Randomizer.Next(0, 10) + RandomValueGenerator.Randomizer.Next(0, 10)
	End Function

	Public Shared Function GetRandomDepartment() As String
	  Return RandomValueGenerator.DepartmentsList(Randomizer.Next(0, RandomValueGenerator.DepartmentsList.Count))
	End Function

	Public Shared Function GetRandomDateTime(ByVal minYear As Integer, ByVal maxYear As Integer) As DateTime
	  Debug.Assert(minYear <= maxYear)

	  Return New DateTime(RandomValueGenerator.Randomizer.Next(minYear, maxYear), RandomValueGenerator.Randomizer.Next(1, 13), RandomValueGenerator.Randomizer.Next(1, 28), RandomValueGenerator.Randomizer.Next(0, 23), RandomValueGenerator.Randomizer.Next(0, 60), RandomValueGenerator.Randomizer.Next(0, 60))
	End Function

	Public Shared ReadOnly Property Departments() As List(Of String)
	  Get
		Return RandomValueGenerator.DepartmentsList
	  End Get
	End Property


	Private Shared Randomizer As New Random()

	Private Shared DepartmentsList As List(Of String) = New List(Of String)()

	Private Const Consonnants As String = "bcdfgjklmnprstv"
	Private Const Vowels As String = "aeiou"
  End Class
End Namespace