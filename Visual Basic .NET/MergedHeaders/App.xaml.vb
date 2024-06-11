'
' * Xceed DataGrid for WPF - SAMPLE CODE - Merged Column Headers Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' * 
' * This is the main application of the Merged Column Headers sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use
' * and distribution of this Sample Code is subject to the terms
' * and conditions referring to "Sample Code" that are specified in
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 


Imports Microsoft.VisualBasic
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
  Partial Public Class App
	  Inherits System.Windows.Application
	Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
	  XceedDeploymentLicense.SetLicense()

	  MyBase.OnStartup(e)
	End Sub
  End Class
End Namespace