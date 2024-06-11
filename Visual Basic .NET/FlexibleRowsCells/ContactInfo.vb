'
' * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Rows & Cells Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ContactInfo.vb]
' *  
' * This class is used to save a Contact Info data.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleRowsCells
  Public Class ContactInfo
	Private privateName As String
	Public Property Name() As String
		Get
			Return privateName
		End Get
		Set(ByVal value As String)
			privateName = value
		End Set
	End Property

	Private privatePhone As String
	Public Property Phone() As String
		Get
			Return privatePhone
		End Get
		Set(ByVal value As String)
			privatePhone = value
		End Set
	End Property

	Private privateAddress As String
	Public Property Address() As String
		Get
			Return privateAddress
		End Get
		Set(ByVal value As String)
			privateAddress = value
		End Set
	End Property

	Private privateTitle As String
	Public Property Title() As String
		Get
			Return privateTitle
		End Get
		Set(ByVal value As String)
			privateTitle = value
		End Set
	End Property

	Private privateCountry As String
	Public Property Country() As String
		Get
			Return privateCountry
		End Get
		Set(ByVal value As String)
			privateCountry = value
		End Set
	End Property

	Private privateYearsOfexperience As Integer
	Public Property YearsOfexperience() As Integer
		Get
			Return privateYearsOfexperience
		End Get
		Set(ByVal value As Integer)
			privateYearsOfexperience = value
		End Set
	End Property
  End Class
End Namespace