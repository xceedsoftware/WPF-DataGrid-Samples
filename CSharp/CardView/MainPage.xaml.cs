/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 *  
 * This class implements the various dynamic configuration options offered
 * by the configuration panel declared in MainPage.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.CardView
{
  public partial class MainPage : Page, IWeakEventListener
  {
    public MainPage()
    {
      PropertyChangedEventManager.AddListener( ConfigurationData.Singleton, this, string.Empty );

      this.InitializeComponent();
      this.AdjustHeadersFooters();

      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
      this.Unloaded += new RoutedEventHandler( this.MainPage_Unloaded );
    }

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      PropertyChangedEventManager.RemoveListener( ConfigurationData.Singleton, this, string.Empty );
      PropertyChangedEventManager.AddListener( ConfigurationData.Singleton, this, string.Empty );

      ( ( App )Application.Current ).ViewChanged += new EventHandler( this.OnViewChanged );
    }

    private void MainPage_Unloaded( object sender, RoutedEventArgs e )
    {
      PropertyChangedEventManager.RemoveListener( ConfigurationData.Singleton, this, string.Empty );
      ( ( App )Application.Current ).ViewChanged -= new EventHandler( this.OnViewChanged );
    }

    private void ConfigurationData_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      switch( e.PropertyName )
      {
        case "FilteringMethod":
        case "ShowGridHeaders":
        case "ShowInsertionRow":
          this.AdjustHeadersFooters();
          break;
      }
    }

    private void AdjustHeadersFooters()
    {
      // Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear();
      grid.View.Headers.Clear();

      DataTemplate template;

      if( ConfigurationData.Singleton.ShowGridHeaders )
      {
        template = this.FindResource( "cardViewColumnManagerRowAndGroupByControl" ) as DataTemplate;
        grid.View.FixedHeaders.Add( template );
      }

      var collectionView = this.grid.ItemsSource as DataGridCollectionView;
      if( collectionView != null )
      {
        // Add the filter row in the FixedHeaders if desired
        if( ConfigurationData.Singleton.FilteringMethod == FilteringMethod.FilterRow )
        {
          grid.View.Headers.Add( this.FindResource( "filterRowTemplate" ) as DataTemplate );
          collectionView.FilterCriteriaMode = FilterCriteriaMode.And;
        }
        else
        {
          collectionView.FilterCriteriaMode = FilterCriteriaMode.None;
        }

        // Simply activate or deactive auto filtering. The actual drop-down presence in 
        // ColumnManagerCell is handled in XAML.
        if( ConfigurationData.Singleton.FilteringMethod == FilteringMethod.AutoFilter )
        {
          collectionView.AutoFilterMode = AutoFilterMode.And;
        }
        else
        {
          collectionView.AutoFilterMode = AutoFilterMode.None;
        }
      }

      // Add the insertion row in the Headers if desired
      if( ConfigurationData.Singleton.ShowInsertionRow )
      {
        template = this.FindResource( "cardViewInsertionRow" ) as DataTemplate;
        grid.View.Headers.Add( template );
      }
    }

    private void OnViewChanged( object sender, EventArgs e )
    {
      this.AdjustHeadersFooters();
    }

    #region IWeakEventListener Members

    bool IWeakEventListener.ReceiveWeakEvent( Type managerType, object sender, EventArgs e )
    {
      if( managerType == typeof( PropertyChangedEventManager ) )
      {
        if( sender == ConfigurationData.Singleton )
        {
          this.ConfigurationData_PropertyChanged( sender, ( PropertyChangedEventArgs )e );
        }

        return true;
      }

      return false;
    }

    #endregion
  }
}
