'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [App.xaml.vb]
' 
' This is the Application class of the Column Management sample.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
Imports System.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Partial Public Class App
    Inherits System.Windows.Application

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      Xceed.Wpf.DataGrid.Samples.XceedDeploymentLicense.SetLicense()

      Dim musicDataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetMusicLibraryDataSet()
      m_songs = musicDataSet.Tables("Songs")

      MyBase.OnStartup(e)
    End Sub

    Private m_songs As DataTable

    Public ReadOnly Property Songs() As DataTable
      Get
        Return m_songs
      End Get
    End Property
  End Class
End Namespace
