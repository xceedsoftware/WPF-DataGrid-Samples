/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the MVVM sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel;

namespace Xceed.Wpf.DataGrid.Samples.MVVM
{
  public partial class App : Application
  {
    #region MainPageViewModelInstance Property

    public MainPageViewModel MainPageViewModelInstance
    {
      get;
      private set;
    }

    #endregion

    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      // The ViewModel is created here, so the same instance can be set as the DataContext of the View,
      // AND be referenced in the ProductsCollectionViewResources.xaml ResourceDictionary, so bindings can be used in it.
      this.MainPageViewModelInstance = new MainPageViewModel();

      base.OnStartup( e );
    }
  }
}
