'
' * Xceed DataGrid for WPF - SAMPLE CODE - Column Chooser Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' * 
' * This is the main application of the Column Chooser sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
  Partial Public Class App
        Inherits System.Windows.Application
	#Region "PROTECTED METHODS"

	Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  m_suppliers = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet().Tables("Suppliers")

	  MyBase.OnStartup(e)
	End Sub

	#End Region ' PROTECTED METHODS

	#Region "Suppliers property"

	Public ReadOnly Property Suppliers() As DataTable
	  Get
		Return m_suppliers
	  End Get
	End Property

	Private m_suppliers As DataTable

	#End Region ' Suppliers property
  End Class
End Namespace