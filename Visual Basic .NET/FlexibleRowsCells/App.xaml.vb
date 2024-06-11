'
' * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Rows and Cells Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' * 
' * This is the main application of the Flexible Rows and Cells sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Data

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleRowsCells
  Partial Public Class App
	  Inherits System.Windows.Application
	Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  Dim musicDataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetMusicLibraryDataSet()
	  m_songs = musicDataSet.Tables("Songs")

	  MyBase.OnStartup(e)
	End Sub

	#Region "Songs Property"

	Private m_songs As DataTable

	Public ReadOnly Property Songs() As DataTable
	  Get
		Return m_songs
	  End Get
	End Property

	#End Region ' Songs Property
  End Class
End Namespace