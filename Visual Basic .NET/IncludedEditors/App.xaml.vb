'
' Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [App.xaml.vb]
'
' This is the Application class of the Included Editors sample.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Data
Imports System.Xml
Imports System.Configuration

Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Partial Public Class App
    Inherits System.Windows.Application

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)

      XceedDeploymentLicense.SetLicense()

      MyBase.OnStartup(e)

    End Sub

  End Class
End Namespace
