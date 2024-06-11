/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  public partial class MainPage : Page, IWeakEventListener
  {
    public MainPage()
    {
      InitializeComponent();

      ConfigurationData.Singleton.PropertyChanged += new PropertyChangedEventHandler( ConfigurationData_PropertyChanged );
      this.AdjustHeadersFooters();
      PropertyChangedEventManager.AddListener( grid, this, "HasSearchControl" );
    }

    private void ConfigurationData_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      switch( e.PropertyName )
      {
        case "FilteringMethod":
        case "ShowGroupByControl":
        case "ShowColumnManagerRow":
        case "ShowInsertionRow":
          this.AdjustHeadersFooters();
          break;

        case "ShowSearchControl":
          this.ShowSearchControl();
          break;
      }
    }

    private void grid_DeletingSelectedItems( object sender, CancelRoutedEventArgs e )
    {
      e.Cancel = ( MessageBoxResult.No == MessageBox.Show(
        "Are you sure you want to delete the selected items?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question ) );
    }

    private void grid_DeletingSelectedItemError( object sender, DeletingSelectedItemErrorRoutedEventArgs e )
    {
      // When an exception occur while deleting a row, you have 
      // the chance to rectify the situation and retry the operation.
      // You can also skip the item in error or abort the operation.
      // For the purpose of this sample, we will skip on error.
      e.Action = DeletingSelectedItemErrorAction.Skip;
    }

    private void AdjustHeadersFooters()
    {
      // Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear();
      grid.View.Headers.Clear();

      // Add the group by control in the FixedHeaders if desired
      if( ConfigurationData.Singleton.ShowGroupByControl )
      {
        if( ( ConfigurationData.Singleton.ShowInsertionRow ) || ( ConfigurationData.Singleton.ShowColumnManagerRow ) )
        {
          grid.View.FixedHeaders.Add( this.FindResource( "hierarchicalGroupByControlTemplate" ) as DataTemplate );
        }
        else
        {
          grid.View.FixedHeaders.Add( this.FindResource( "aloneHierarchicalGroupByControlTemplate" ) as DataTemplate );
        }
      }

      // Add the column manager row in the FixedHeaders if desired
      if( ConfigurationData.Singleton.ShowColumnManagerRow )
      {
        grid.View.FixedHeaders.Add( this.FindResource( "columnManagerRowTemplate" ) as DataTemplate );
      }

      DataGridCollectionView collectionView = this.grid.ItemsSource as DataGridCollectionView;

      if( collectionView != null )
      {
        // Add the filter row in the FixedHeaders if desired
        if( ConfigurationData.Singleton.FilteringMethod == FilteringMethod.FilterRow )
        {
          grid.View.FixedHeaders.Add( this.FindResource( "filterRowTemplate" ) as DataTemplate );

          collectionView.FilterCriteriaMode = FilterCriteriaMode.And;
          collectionView.AutoFilterMode = AutoFilterMode.None;
        }
        else if( ConfigurationData.Singleton.FilteringMethod == FilteringMethod.AutoFilter )
        {
          collectionView.FilterCriteriaMode = FilterCriteriaMode.And;
          collectionView.AutoFilterMode = AutoFilterMode.And;
        }
        else
        {
          collectionView.FilterCriteriaMode = FilterCriteriaMode.None;
          collectionView.AutoFilterMode = AutoFilterMode.None;
        }
      }

      // Add the insertion row in the FixedHeaders if desired
      if( ConfigurationData.Singleton.ShowInsertionRow )
      {
        grid.View.FixedHeaders.Add( this.FindResource( "insertionRowTemplate" ) as DataTemplate );
      }
    }

    private void ShowSearchControl()
    {

      if( ConfigurationData.Singleton.ShowSearchControl )
      {
        if( DataGridCommands.OpenSearch.CanExecute( null, grid ) )
        {
          DataGridCommands.OpenSearch.Execute( null, grid );
        }
      }
      else
      {
        if( DataGridCommands.CloseSearch.CanExecute( null, grid ) )
        {
          DataGridCommands.CloseSearch.Execute( null, grid );
        }
      }
    }

    private void UpdateShowSearchControlCheckBox()
    {
      if( grid.HasSearchControl )
      {
        ConfigurationData.Singleton.ShowSearchControl = true;
      }
      else
      {
        ConfigurationData.Singleton.ShowSearchControl = false;
      }
    }

    bool IWeakEventListener.ReceiveWeakEvent( Type managerType, object sender, EventArgs e )
    {
      if( managerType == typeof( PropertyChangedEventManager ) )
      {
        this.UpdateShowSearchControlCheckBox();
        return true;
      }

      return false;
    }
  }
}
