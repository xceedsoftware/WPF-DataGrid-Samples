'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [App.xaml.vb]
' *  
' * This is the Application class of the MVVM sample.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.Windows
Imports Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel

Namespace Xceed.Wpf.DataGrid.Samples.MVVM
  Partial Public Class App
    Inherits Application
#Region "MainPageViewModelInstance Property"

    Private privateMainPageViewModelInstance As MainPageViewModel
    Public Property MainPageViewModelInstance() As MainPageViewModel
      Get
        Return privateMainPageViewModelInstance
      End Get
      Private Set(ByVal value As MainPageViewModel)
        privateMainPageViewModelInstance = value
      End Set
    End Property

#End Region

    Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      ' The ViewModel is created here, so the same instance can be set as the DataContext of the View,
      ' AND be referenced in the ProductsCollectionViewResources.xaml ResourceDictionary, so bindings can be used in it.
      Me.MainPageViewModelInstance = New MainPageViewModel()

      MyBase.OnStartup(e)
    End Sub
  End Class
End Namespace
