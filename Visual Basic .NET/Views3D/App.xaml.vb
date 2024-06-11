'
 '* Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
 '*
'* [App.xaml.vb]
 '*
 '* This is the main application of the Views 3D sample.
 '*
 '* This file is part of the Xceed DataGrid for WPF product. The use
 '* and distribution of this Sample Code is subject to the terms
 '* and conditions referring to "Sample Code" that are specified in
 '* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 '

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.Views3D
  Public Partial Class App
	  Inherits Application
	Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  Dim northwindDataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
	  m_products = northwindDataSet.Tables("Products")

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
