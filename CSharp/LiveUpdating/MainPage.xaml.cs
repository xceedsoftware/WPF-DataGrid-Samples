/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
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
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  /// <summary>
  /// Interaction logic for MainPage.xaml
  /// </summary>
  public partial class MainPage : Page
  {
    private static readonly string[] InitialSymbols =
      new string[]
      {
        "XCEED",
        "YHOO",
        "MSFT",
        "AAPL",
        "GOOG",
        "IBM",
        "CSCO",
        "DELL",
        "INTC",
        "NTGR",
      };

    public MainPage()
    {
      m_quoteWorkers = new List<QuoteWorker>();
      m_quote = this.CreateDataTable();

      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
      this.Unloaded += new RoutedEventHandler( this.MainPage_Unloaded );

      InitializeComponent();

      foreach( string symbol in MainPage.InitialSymbols )
      {
        this.AddNewQuote( symbol );
      }

      sliderRecordsCount.Value = MainPage.InitialSymbols.Length;
      checkBoxAutoScroll.IsChecked = ( grid.AutoScrollCurrentItem != AutoScrollCurrentItemTriggers.None );
    }

    #region QUOTE PROPERTY

    public DataView Quote
    {
      get
      {
        return m_quote.DefaultView;
      }
    }

    #endregion QUOTE PROPERTY

    #region EVENT HANDLERS

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      // Start fetching data.
      foreach( QuoteWorker quoteWorker in m_quoteWorkers )
      {
        quoteWorker.Start();
      }
    }

    private void MainPage_Unloaded( object sender, RoutedEventArgs e )
    {
      // Stop fetching data.
      foreach( QuoteWorker quoteWorker in m_quoteWorkers )
      {
        quoteWorker.Stop();
      }
    }

    private void quoteWorker_QuoteInfoUpdated( object sender, EventArgs e )
    {
      // A Quote Worker is done gathering data.  Update the DataTable.
      // Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      lock( m_quote.DefaultView )
      {
        QuoteWorker quoteWorker = sender as QuoteWorker;

        if( !quoteWorker.Started )
          return;

        if( quoteWorker.DataRowIndex >= m_quote.Rows.Count )
          throw new InvalidOperationException( "An error occured trying to update data of a non-existing quote." );

        // Find the datarow to update using the quoteWorker's DataRowIndex property.
        // This is an optimization so we don't have to find the DataRow corresponding to the quote worker each time.
        System.Data.DataRow dataRow = m_quote.Rows[ quoteWorker.DataRowIndex ];
        QuoteInfo quoteInfo = quoteWorker.QuoteInfo;

        if( !dataRow[ "Symbol" ].Equals( quoteInfo.Symbol ) )
          throw new InvalidOperationException( "An error occured trying to update data of a non-existing quote." );

        dataRow[ "PreviousClose" ] = quoteInfo.PreviousClose;
        dataRow[ "Open" ] = quoteInfo.OpeningValue;

        double oldChange = System.Convert.ToDouble( dataRow[ "Change" ] );
        double newChange = quoteInfo.LastTrade - quoteInfo.PreviousClose;
        dataRow[ "ChangeDiff" ] = newChange - oldChange;
        dataRow[ "Change" ] = newChange;

        double oldLastTrade = System.Convert.ToDouble( dataRow[ "LastTrade" ] );
        double newLastTrade = quoteInfo.LastTrade;
        dataRow[ "LastTradeDiff" ] = newLastTrade - oldLastTrade;
        dataRow[ "LastTrade" ] = newLastTrade;
        dataRow[ "TradeTime" ] = quoteInfo.TradeTime;
      }
    }

    private void sliderSimulatedMinLatency_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
    {
      if( ( this.IsInitialized ) && ( sliderSimulatedMaxLatency.Value < e.NewValue ) )
        sliderSimulatedMaxLatency.Value = e.NewValue;
    }

    private void sliderSimulatedMaxLatency_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
    {
      if( ( this.IsInitialized ) && ( sliderSimulatedMinLatency.Value > e.NewValue ) )
        sliderSimulatedMinLatency.Value = e.NewValue;
    }

    private void sliderRecordsCount_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
    {
      if( !this.IsInitialized )
        return;

      if( m_addRemoveRecordsDelegate != null )
        return;

      m_addRemoveRecordsDelegate = new AddRemoveRecordsDelegate( this.SyncDataTableWithSliderRecordsCount );

      this.Dispatcher.BeginInvoke( System.Windows.Threading.DispatcherPriority.Background, m_addRemoveRecordsDelegate );
    }

    private void SyncDataTableWithSliderRecordsCount()
    {
      System.Diagnostics.Debug.Assert( m_addRemoveRecordsDelegate != null,
        "This method should only be invoked through the dispatcher by the Records count slider's ValueChanged event." );

      int rowCount;

      // Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      lock( m_quote.DefaultView )
      {
        rowCount = m_quote.Rows.Count;
      }

      int desiredRowCount = System.Convert.ToInt32( sliderRecordsCount.Value );

      if( rowCount < desiredRowCount )
      {
        for( int i = rowCount; i < desiredRowCount; i++ )
        {
          this.AddNewQuote( MarketSimulator.GenerateRandomSymbol() );
        }
      }
      else if( rowCount > desiredRowCount )
      {
        for( int i = rowCount; i > desiredRowCount; i-- )
        {
          this.RemoveQuote( i - 1 );
        }
      }

      m_addRemoveRecordsDelegate = null;
    }

    private void checkBoxAutoScroll_Checked( object sender, RoutedEventArgs e )
    {
      grid.AutoScrollCurrentItem = AutoScrollCurrentItemTriggers.All;
    }

    private void checkBoxAutoScroll_Unchecked( object sender, RoutedEventArgs e )
    {
      grid.AutoScrollCurrentItem = AutoScrollCurrentItemTriggers.None;
    }

    private delegate void AddRemoveRecordsDelegate();

    private AddRemoveRecordsDelegate m_addRemoveRecordsDelegate;

    #endregion EVENT HANDLERS

    #region PRIVATE METHODS

    private DataTable CreateDataTable()
    {
      DataTable dataTable = new DataTable( "StockData" );

      dataTable.Columns.Add( "Symbol", typeof( string ) );

      dataTable.Columns.Add( "PreviousClose", typeof( double ) );
      dataTable.Columns.Add( "Open", typeof( double ) );
      dataTable.Columns.Add( "LastTrade", typeof( double ) );
      dataTable.Columns.Add( "TradeTime", typeof( string ) );

      dataTable.Columns.Add( "Change", typeof( double ) );

      // Calculation columns meant to be hidden.
      dataTable.Columns.Add( "LastTradeDiff", typeof( double ) );
      dataTable.Columns.Add( "ChangeDiff", typeof( double ) );

      return dataTable;
    }

    private void AddNewQuote( string symbol )
    {
      System.Data.DataRow dataRow;

      int position;

      // Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      lock( m_quote.DefaultView )
      {
        dataRow = m_quote.NewRow();

        dataRow[ "Symbol" ] = symbol.ToUpper();

        dataRow[ "PreviousClose" ] = 0.0d;
        dataRow[ "Open" ] = 0.0d;
        dataRow[ "LastTrade" ] = 0.0d;
        dataRow[ "Change" ] = 0.0d;
        dataRow[ "TradeTime" ] = "Please wait...";

        dataRow[ "LastTradeDiff" ] = 0.0d;
        dataRow[ "ChangeDiff" ] = 0.0d;

        position = m_quote.Rows.Count;
        m_quote.Rows.InsertAt( dataRow, position );
      }

      // Create and associate a quote worker with the DataRow.
      QuoteWorker quoteWorker = new QuoteWorker( position, symbol );

      m_quoteWorkers.Add( quoteWorker );

      Binding minLatencyBinding = new Binding();
      minLatencyBinding.Source = sliderSimulatedMinLatency;
      minLatencyBinding.Path = new PropertyPath( "Value" );

      Binding maxLatencyBinding = new Binding();
      maxLatencyBinding.Source = sliderSimulatedMaxLatency;
      maxLatencyBinding.Path = new PropertyPath( "Value" );

      BindingOperations.SetBinding( quoteWorker, QuoteWorker.MinLatencyProperty, minLatencyBinding );
      BindingOperations.SetBinding( quoteWorker, QuoteWorker.MaxLatencyProperty, maxLatencyBinding );

      // Subscribe to the quote worker's QuoteInfoUpdated so that we can apply the changes to the datarow.
      quoteWorker.QuoteInfoUpdated += new EventHandler( quoteWorker_QuoteInfoUpdated );

      // Start fetching quotes for this stock.
      quoteWorker.Start();
    }

    private void RemoveQuote( int position )
    {
      // Lock the DataTable's DefaultView so that other threads wait for their turn to modify it.
      lock( m_quote.DefaultView )
      {
        QuoteWorker quoteWorker = m_quoteWorkers[ position ];

        // Stop the worker.
        quoteWorker.Stop();

        m_quoteWorkers.Remove( quoteWorker );

        m_quote.Rows.RemoveAt( position );
      }
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private DataTable m_quote;

    private List<QuoteWorker> m_quoteWorkers;

    #endregion PRIVATE FIELDS
  }
}
