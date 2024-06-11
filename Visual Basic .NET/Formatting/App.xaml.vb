'
' * Xceed DataGrid for WPF - SAMPLE CODE - Formatting Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' * 
' * This is the main application of the Formatting sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.Formatting
  Partial Public Class App
	  Inherits System.Windows.Application
	Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
	  m_products = dataSet.Tables("Products")

	  MyBase.OnStartup(e)
	End Sub

	#Region "Products property"

	Private m_products As DataTable

	Public ReadOnly Property Products() As DataTable
	  Get
		Return m_products
	  End Get
	End Property

	#End Region ' Products property
  End Class
End Namespace