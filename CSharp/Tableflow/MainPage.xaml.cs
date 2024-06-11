/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.Tableflow
{
  public partial class MainPage : Page, IWeakEventListener
  {
    public MainPage()
    {
      InitializeComponent();
      SetDetailConfigurations();
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

        case "ShowMergedHeaders":
          this.AdjustHeadersFooters( true );
          break;

        case "ShowMasterDetail":
          this.ShowOrdersDetail();
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

    private void AdjustHeadersFooters( bool isShowMergedHeaderChanged = false )
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

      // Add the merged column manager row in the FixedHeaders if desired
      if( ConfigurationData.Singleton.ShowMergedHeaders )
      {
        grid.View.FixedHeaders.Add( this.FindResource( "mergedColumnManagerRowTemplate" ) as DataTemplate );
      }

      if( isShowMergedHeaderChanged )
      {
        this.SetupUpMergedColumns( ConfigurationData.Singleton.ShowMergedHeaders );
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

    private void ShowOrdersDetail()
    {
      this.SetDetailConfigurations();
    }

    private void SetDetailConfigurations()
    {
      if( this.grid == null )
        return;

      this.grid.DetailConfigurations.Clear();

      if( ConfigurationData.Singleton.ShowMasterDetail )
        this.grid.DetailConfigurations.Add( ( DetailConfiguration )this.Resources[ "ordersDetailConfiguration" ] );

      // Collapse all details and re-expand them.
      List<DataGridContext> dataGridContexts = new List<DataGridContext>( this.grid.GetChildContexts() );

      foreach( DataGridContext dataGridContext in dataGridContexts )
      {
        dataGridContext.ParentDataGridContext.CollapseDetails( dataGridContext.ParentItem );
        dataGridContext.ParentDataGridContext.ExpandDetails( dataGridContext.ParentItem );
      }

    }

    private void SetupUpMergedColumns( bool attach )
    {
      var tableflowView = grid.View as TableflowView;
      var fixedColumnCount = tableflowView.FixedColumnCount;
      tableflowView.FixedColumnCount = 0;
      MergedColumnCollection mergedColumns;

      grid.BeginInit();

      if( attach )
      {
        grid.MergedHeaders.Add( m_mergedHeader );
        foreach( MergedColumn column in m_mergedColumnArray )
        {
          m_mergedHeader.MergedColumns.Add( column );
        }

        mergedColumns = m_mergedHeader.MergedColumns;

        var mergedColumn = mergedColumns[ "IDs" ] as MergedColumn;
        mergedColumn.ChildColumnNames = "OrderID EmployeeID";

        mergedColumn = mergedColumns[ "OrderDetails" ] as MergedColumn;
        mergedColumn.ChildColumnNames = "CustomerID OrderDate ShipVia Freight";

        mergedColumn = mergedColumns[ "AddressDetails" ] as MergedColumn;
        mergedColumn.ChildColumnNames = "ShipCity ShipCountry ShipName ShipAddress ShipPostalCode ShipRegion";
      }
      else
      {
        m_mergedHeader = grid.MergedHeaders[ 0 ];
        mergedColumns = m_mergedHeader.MergedColumns;
        m_mergedColumnArray = new MergedColumn[ mergedColumns.Count ];
        mergedColumns.CopyTo( m_mergedColumnArray, 0 );

        grid.MergedHeaders.Clear();

        grid.Columns[ "RequiredDate" ].VisiblePosition = int.MaxValue;
        grid.Columns[ "ShippedDate" ].VisiblePosition = int.MaxValue;
      }

      grid.EndInit();

      tableflowView.FixedColumnCount = fixedColumnCount;
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

    private MergedColumn[] m_mergedColumnArray;
    private MergedHeader m_mergedHeader;
  }
}
