'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [RandomValueGenerator.vb]
' *  
' * Utility class that provides random values, and which is used to populate properties of model objects.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
  Friend NotInheritable Class RandomValueGenerator
    Private Sub New()
    End Sub
    Public Shared Function GetRandomInteger(ByVal min As Integer, ByVal max As Integer) As Integer
      Return RandomValueGenerator.Randomizer.Next(min, max)
    End Function

    Public Shared Function GetRandomBool() As Boolean
      Return (RandomValueGenerator.Randomizer.Next(0, 2) = 1)
    End Function

    Public Shared Function GetRandomProductName() As String
      Dim wordCount = RandomValueGenerator.Randomizer.[Next](1, 5)
      Dim nameBuilder = New StringBuilder()

      For i As Integer = 0 To wordCount - 1
        Dim syllableLength = RandomValueGenerator.Randomizer.[Next](2, 6)

        For j As Integer = 0 To syllableLength - 1
          Dim consonnant = RandomValueGenerator.Consonnants(RandomValueGenerator.Randomizer.[Next](0, RandomValueGenerator.Consonnants.Length))
          Dim vowel = RandomValueGenerator.Vowels(RandomValueGenerator.Randomizer.[Next](0, RandomValueGenerator.Vowels.Length))

          If j = 0 Then
            nameBuilder.Append(consonnant.ToString().ToUpper())
          Else
            nameBuilder.Append(consonnant)
          End If

          nameBuilder.Append(vowel)

          If (j = syllableLength - 1) AndAlso (i < wordCount - 1) Then
            nameBuilder.Append(" "c)
          End If
        Next
      Next

      Return nameBuilder.ToString()
    End Function

    'Public Shared Function GetRandomProductName() As String
    '  Dim wordCount = RandomValueGenerator.Randomizer.Next(1, 5)
    '  Dim nameBuilder = New StringBuilder()

    '  For i As Integer = 0 To wordCount - 1
    '    Dim syllableLength = RandomValueGenerator.Randomizer.Next(2, 6)

    '    For j As Integer = 0 To syllableLength - 1
    '      Dim consonnant = RandomValueGenerator.Consonnants(RandomValueGenerator.Randomizer.Next(0, RandomValueGenerator.Consonnants.Length))
    '      Dim vowel = RandomValueGenerator.Vowels(RandomValueGenerator.Randomizer.Next(0, RandomValueGenerator.Vowels.Length))

    '      If j = 0 Then
    '        nameBuilder.Append(consonnant.ToString().ToUpper())
    '      Else
    '        nameBuilder.Append(consonnant)
    '      End If

    '      nameBuilder.Append(vowel)

    '      If (j Is syllableLength - 1) AndAlso (i < wordCount - 1) Then
    '        nameBuilder.Append(" "c)
    '      End If
    '    Next j
    '  Next i

    '  Return nameBuilder.ToString()
    'End Function

    Public Shared Function GetRandomQuantityPerUnit() As String
      Return RandomValueGenerator.QuantityPerUnit(Randomizer.Next(0, RandomValueGenerator.QuantityPerUnit.Length))
    End Function

    Public Shared Function GetRandomDecimal() As Decimal
      Return Convert.ToDecimal(RandomValueGenerator.Randomizer.NextDouble() * 100)
    End Function

    Public Shared Function GetRandomShort(ByVal min As Integer, ByVal max As Integer) As Short
      Return Convert.ToInt16(RandomValueGenerator.Randomizer.Next(min, max))
    End Function

    Public Shared Function GetRandomDateTime(ByVal minYear As Integer, ByVal maxYear As Integer) As DateTime
      Return DateTime.Parse(RandomValueGenerator.Randomizer.Next(minYear, maxYear) & "-" & RandomValueGenerator.Randomizer.Next(1, 13) & "-" & RandomValueGenerator.Randomizer.Next(1, 28) & "T00:00:00-05:00")
    End Function

    Private Shared Randomizer As New Random()
    Private Const Consonnants As String = "bcdfgjklmnprstv"
    Private Const Vowels As String = "aeiou"
    Private Shared QuantityPerUnit() As String = {"10 boxes x 20 bags", "24 - 12 oz bottles", "12 - 550 ml bottles", "48 - 6 oz jars", "36 boxes", "12 - 8 oz jars", "12 - 1 lb pkgs.", "12 - 12 oz jars", "18 - 500 g pkgs.", "12 - 200 ml jars", "1 kg pkg.", "2 kg box", "40 - 100 g pkgs.", "24 - 250 ml bottles", "32 - 500 g boxes"}
  End Class
End Namespace