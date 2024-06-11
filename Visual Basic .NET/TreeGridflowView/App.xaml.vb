'
' * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.cs]
' * 
' * This is the main application of the TreeGridflowView sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 


Imports Microsoft.VisualBasic
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
  Partial Public Class App
	  Inherits System.Windows.Application
	Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet()
	  m_employees = dataSet.Tables("Employees")

	  MyBase.OnStartup(e)
	End Sub

	#Region "Employees property"

	Private m_employees As DataTable

	Public ReadOnly Property Employees() As DataTable
	  Get
		Return m_employees
	  End Get
	End Property

	#End Region ' Employees property
  End Class
End Namespace