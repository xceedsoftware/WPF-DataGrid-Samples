'
'* Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [App.xaml.vb]
'*
'* This is the Application class of the Card Viewsample.
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'*
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.CardView
  Partial Public Class App
    Inherits Application
    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      Xceed.Wpf.DataGrid.Samples.XceedDeploymentLicense.SetLicense()

      Dim dataSet As DataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetContactsDataSet()
      m_contacts = dataSet.Tables("Contacts")

      MyBase.OnStartup(e)
    End Sub

#Region "Contacts property"

    Private m_contacts As DataTable

    Public ReadOnly Property Contacts() As DataTable
      Get
        Return m_contacts
      End Get
    End Property

#End Region

    Public Event ViewChanged As EventHandler

    Public Sub OnViewChanged(ByVal e As EventArgs)
      If Not Me.ViewChangedEvent Is Nothing Then
        RaiseEvent ViewChanged(Me, e)
      End If
    End Sub
  End Class
End Namespace
