/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
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
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
{
  public partial class MainPage : Page
  {
    #region CONSTRUCTOR

    public MainPage()
    {
      this.InitializeComponent();
    }

    #endregion

    #region ContextMenu creation and event handlers

    private void DataRow_ContextMenuOpening( object sender, RoutedEventArgs e )
    {
      DependencyObject obj = e.OriginalSource as DependencyObject;

      while( obj != null )
      {
        if( obj is DataRow )
        {
          ContextMenu contextMenu = grid.ContextMenu;

          contextMenu.DataContext = ( ( DataRow )obj ).DataContext;

          contextMenu.Items.Clear();

          // Bind the MenuItem.Header to the DataRow.DataContext for ShipCountry field
          MenuItem shipCountryFilterMenuItem = new MenuItem();
          shipCountryFilterMenuItem.Tag = "ShipCountry";

          Binding shipCountryBinding = new Binding( "[ShipCountry]" );
          shipCountryFilterMenuItem.SetBinding( MenuItem.HeaderProperty, shipCountryBinding );

          shipCountryFilterMenuItem.Click += new RoutedEventHandler( this.OnShipContryMenuItemSelected );

          contextMenu.Items.Add( shipCountryFilterMenuItem );


          // Bind the MenuItem.Header to the DataRow.DataContext for ShipCity field
          MenuItem shipCityFilterMenuItem = new MenuItem();
          shipCityFilterMenuItem.Tag = "ShipCity";

          Binding shipCityBinding = new Binding( "[ShipCity]" );
          shipCityFilterMenuItem.SetBinding( MenuItem.HeaderProperty, shipCityBinding );

          shipCityFilterMenuItem.Click += new RoutedEventHandler( this.OnShipCityMenuItemSelected );

          contextMenu.Items.Add( shipCityFilterMenuItem );

          // Bind the MenuItem.Header to the DataRow.DataContext for OrderDate field
          MenuItem orderDateFilterMenuItem = new MenuItem();
          orderDateFilterMenuItem.Tag = "OrderDate";

          Binding orderDateBinding = new Binding( "[OrderDate].Month" );
          orderDateBinding.Converter = this.FindResource( "monthNameConverter" ) as IValueConverter;
          orderDateFilterMenuItem.SetBinding( MenuItem.HeaderProperty, orderDateBinding );

          orderDateFilterMenuItem.Click += new RoutedEventHandler( this.OnOrderMonthMenuItemSelected );

          contextMenu.Items.Add( orderDateFilterMenuItem );

          contextMenu.Items.Add( new Separator() );

          MenuItem clearFiltersMenuItem = new MenuItem();
          clearFiltersMenuItem.Header = "Clear All Filters";
          clearFiltersMenuItem.Click += new RoutedEventHandler( this.OnClearAllFiltersMenuItemSelected );

          contextMenu.Items.Add( clearFiltersMenuItem );

          return;
        }

        obj = VisualTreeHelper.GetParent( obj );
      }

      // The context menu was not triggered over a DataRow. Do nothing.
      e.Handled = true;
    }

    private void OnShipContryMenuItemSelected( object sender, RoutedEventArgs e )
    {
      MenuItem item = sender as MenuItem;

      if( item.Header != null )
      {
        m_countryFilterCombo.SelectedItem = item.Header.ToString();
      }
    }

    private void OnShipCityMenuItemSelected( object sender, RoutedEventArgs e )
    {
      MenuItem item = sender as MenuItem;

      if( item.Header != null )
      {
        m_cityFilterCombo.SelectedItem = item.Header.ToString();
      }
    }

    private void OnOrderMonthMenuItemSelected( object sender, RoutedEventArgs e )
    {
      MenuItem item = sender as MenuItem;

      if( item.Header != null )
      {
        this.UpdateOrderMonthFilterComboBox( item.Header.ToString() );
      }
    }

    private void OnClearAllFiltersMenuItemSelected( object sender, RoutedEventArgs e )
    {
      this.UpdateOrderMonthFilterComboBox( string.Empty );

      DataGridContext dataGridContext = DataGridControl.GetDataGridContext( this.grid );

      if( dataGridContext == null )
        throw new InvalidOperationException( "The grid does not have a DataGridContext." );

      if( dataGridContext.Items == null )
        return;

      if( dataGridContext.Items.SourceCollection == null )
        return;

      //In the master level, the Items is an ItemCollection that wraps a DataGridCollectionView
      DataGridCollectionView dataGridCollectionView = dataGridContext.Items.SourceCollection as DataGridCollectionView;

      if( dataGridCollectionView == null )
        throw new InvalidOperationException( "Automatic filtering is only supported if the data source is a DataGridCollectionView." );

      using( dataGridCollectionView.DeferRefresh() )
      {
        int autoFilterValuesCount = dataGridCollectionView.AutoFilterValues.Keys.Count;

        // Reset all AutoFilterValues lists
        foreach( string fieldName in dataGridCollectionView.AutoFilterValues.Keys )
        {
          IList autoFilterValues = dataGridCollectionView.AutoFilterValues[ fieldName ] as IList;

          if( autoFilterValues != null )
            autoFilterValues.Clear();
        }

        m_cityFilterCombo.SelectedIndex = -1;
        m_countryFilterCombo.SelectedIndex = -1;
        m_countryFilterCombo.IsEnabled = true;
      }
    }

    #endregion

    #region ShipCountry ComboBox event handlers

    private void OnShipCountryComboBoxInitialized( object sender, EventArgs e )
    {
      m_countryFilterCombo = sender as ComboBox;
    }

    private void OnClearShipCountryButtonInitialized( object sender, EventArgs e )
    {
      m_clearCountryButton = sender as Button;
    }

    #endregion

    #region Ship City Combobox and Clear Button event handlers

    private void OnShipCityComboBoxInitialized( object sender, EventArgs e )
    {
      m_cityFilterCombo = sender as ComboBox;
    }

    private void OnShipCitySelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      // We don't want to allow the Country to be modified
      // if a City is set
      bool isCountryEnabled = true;

      if( m_cityFilterCombo.SelectedItem != null )
      {
        isCountryEnabled = false;
      }

      m_countryFilterCombo.IsEnabled = isCountryEnabled;
      m_clearCountryButton.IsEnabled = isCountryEnabled;
    }

    #endregion

    #region Order Month ComboBox and Clear Button event handlers

    private void OnOrderMonthComboBoxInitialized( object sender, EventArgs e )
    {
      m_orderMonthFilterCombo = sender as ComboBox;
    }

    private void OnOrderMonthComboBoxSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      string newValue = string.Empty;

      if( ( e.AddedItems.Count > 0 ) && ( e.AddedItems[ 0 ] != null ) )
        newValue = e.AddedItems[ 0 ].ToString();

      this.UpdateOrderMonthFilterComboBox( newValue );
    }

    private void OnClearMonthButtonInitialized( object sender, EventArgs e )
    {
      m_clearOrderMonthButton = sender as Button;
    }

    private void OnClearOrderMonthButtonClick( object sender, RoutedEventArgs e )
    {
      this.UpdateOrderMonthFilterComboBox( string.Empty );
    }

    private void UpdateOrderMonthFilterComboBox( string month )
    {
      m_orderMonth = month;
      DataGridCollectionView collectionView = this.grid.ItemsSource as DataGridCollectionView;

      if( collectionView != null )
        collectionView.Refresh();

      bool isClearMonthButtonEnabled = true;

      if( string.IsNullOrEmpty( month ) )
        isClearMonthButtonEnabled = false;

      m_clearOrderMonthButton.IsEnabled = isClearMonthButtonEnabled;
      m_orderMonthFilterCombo.SelectedValue = m_orderMonth;
    }

    #endregion

    #region OrderDate Month filtering using DataGridCollectionView filter predicate delegate

    private void OnDataGridCollectionViewSourceFilter( object sender, FilterEventArgs e )
    {
      if( m_orderMonth != string.Empty )
      {
        var dataRowView = e.Item as DataRowView;

        if( dataRowView != null )
        {
          try
          {
            var orderDate = ( DateTime )dataRowView[ "OrderDate" ];

            if( DateTimeFormatInfo.InvariantInfo.GetMonthName( orderDate.Month ) == m_orderMonth )
            {
              e.Accepted = true;
              return;
            }
          }
          catch( Exception )
          {
            // Exception
          }
        }

        e.Accepted = false;
      }
    }

    #endregion.

    #region ShipName CustomDistinctValues Calculation

    // Called when the DistinctValues for the field ShipName are calculated
    private void ShipNameQueryDistinctValue( object sender, QueryDistinctValueEventArgs e )
    {
      if( string.IsNullOrEmpty( e.DataSourceValue as string ) )
      {
        e.DistinctValue = "(Other)";
      }
      else
      {
        string name = e.DataSourceValue as string;

        char firstChar = name.ToLower()[ 0 ];

        if( ( firstChar >= '0' ) && ( firstChar <= '9' ) )
        {
          e.DistinctValue = "0 - 9";
        }
        else if( ( firstChar >= 'a' ) && ( firstChar <= 'c' ) )
        {
          e.DistinctValue = "A - C";
        }
        else if( ( firstChar >= 'd' ) && ( firstChar <= 'f' ) )
        {
          e.DistinctValue = "D - F";
        }
        else if( ( firstChar >= 'g' ) && ( firstChar <= 'i' ) )
        {
          e.DistinctValue = "G - I";
        }
        else if( ( firstChar >= 'j' ) && ( firstChar <= 'l' ) )
        {
          e.DistinctValue = "J - L";
        }
        else if( ( firstChar >= 'm' ) && ( firstChar <= 'o' ) )
        {
          e.DistinctValue = "M - O";
        }
        else if( ( firstChar >= 'p' ) && ( firstChar <= 'r' ) )
        {
          e.DistinctValue = "P - R";
        }
        else if( ( firstChar >= 's' ) && ( firstChar <= 'u' ) )
        {
          e.DistinctValue = "S - U";
        }
        else if( ( firstChar >= 'v' ) && ( firstChar <= 'z' ) )
        {
          e.DistinctValue = "V - Z";
        }
        else
        {
          e.DistinctValue = "(Other)";
        }
      }
    }

    #endregion

    #region ShippedDate CustomDistinctValues Calculation

    // Called when the DistinctValues for the field ShippedDate are calculated
    private void OnShippedDateQueryDistinctValue( object sender, QueryDistinctValueEventArgs e )
    {
      if( e.DataSourceValue is DateTime )
      {
        var dateTime = ( DateTime )e.DataSourceValue;

        e.DistinctValue = DateTimeFormatInfo.InvariantInfo.GetMonthName( dateTime.Month );
      }
    }

    #endregion

    #region PRIVATE FIELDS

    private string m_orderMonth = string.Empty;
    private ComboBox m_cityFilterCombo; // = null;
    private ComboBox m_countryFilterCombo; // = null;
    private Button m_clearCountryButton; // = null;
    private ComboBox m_orderMonthFilterCombo; // = null;
    private Button m_clearOrderMonthButton; // = null; 

    #endregion
  }
}
