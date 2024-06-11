'
' * Xceed DataGrid for WPF - SAMPLE CODE - Flexibles Rows and Cells Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' *  
' * This class implements the various dynamic configuration options offered
' * by the configuration panel declared in MainPage.xaml.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
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

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleRowsCells
  Partial Public Class MainPage
	  Inherits System.Windows.Controls.Page
	#Region "PUBLIC CONSTRUCTOR"

	Public Sub New()
	  InitializeComponent()

	  Dim context As DataGridContext = DataGridControl.GetDataGridContext(Me.grid)

	  If context Is Nothing Then
		Throw New NullReferenceException("context")
	  End If

            Me.ContactCollection = New ContactInfoCollection() From { _
           New ContactInfo() With { _
            .Name = "Brian Jones", _
               .Country = "Canada", _
            .Address = "150 Main Street", _
            .Phone = "450-598-6225", _
            .Title = "President", _
            .YearsOfexperience = 12 _
           }, _
           New ContactInfo() With { _
            .Name = "Tom Callahan", _
            .Country = "USA", _
            .Address = "18 Thomson drive", _
            .Phone = "714-856-9669", _
            .Title = "VP Marketing", _
            .YearsOfexperience = 3 _
           }, _
           New ContactInfo() With { _
            .Name = "Amanda Costa", _
            .Country = "Brazil", _
            .Address = "11 Milsa", _
            .Phone = "969-987-2552", _
            .Title = "Consultant", _
            .YearsOfexperience = 17 _
           }, _
           New ContactInfo() With { _
            .Name = "Jin Yang", _
            .Country = "China", _
            .Address = "3225 Fuy", _
            .Phone = "90-10-99-52-20", _
            .Title = "President", _
            .YearsOfexperience = 5 _
           } _
          }
	End Sub

	#End Region

	#Region "PROPERTIES"

	Private privateContactCollection As ContactInfoCollection
	Public Property ContactCollection() As ContactInfoCollection
		Get
			Return privateContactCollection
		End Get
		Private Set(ByVal value As ContactInfoCollection)
			privateContactCollection = value
		End Set
	End Property

	#End Region
  End Class
End Namespace