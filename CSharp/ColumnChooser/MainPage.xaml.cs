﻿/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Chooser Sample Application
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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
{
  public partial class MainPage : Page
  {
    #region CONSTRUCTORS

    public MainPage()
    {
      m_dataItems = this.CreateDataItems();

      InitializeComponent();

      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
    }

    #endregion CONSTRUCTORS

    #region PROPERTIES

    #region DataItems Property

    public IEnumerable<BusinessObject> DataItems
    {
      get
      {
        return m_dataItems;
      }
    }

    private readonly IEnumerable<BusinessObject> m_dataItems;

    #endregion

    #endregion

    #region EVENT HANDLERS

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      RadioButton selectedRadioButton = null;

      if( chkDefaultContextMenu.IsChecked.Value )
      {
        selectedRadioButton = chkDefaultContextMenu;
      }
      else if( chkDefaultPopup.IsChecked.Value )
      {
        selectedRadioButton = chkDefaultPopup;
      }
      else if( chkCustomized.IsChecked.Value )
      {
        selectedRadioButton = chkCustomized;
      }

      this.SetFixedHeaderStyles( selectedRadioButton );
    }

    private void chkRadioButton_Checked( object sender, RoutedEventArgs e )
    {
      this.SetFixedHeaderStyles( sender as RadioButton );
    }

    #endregion

    #region PRIVATE METHODS

    private IEnumerable<BusinessObject> CreateDataItems()
    {
      var collection = new ObservableCollection<BusinessObject>();
      var rnd = new Random( 123456 );

      for( int i = 0; i < 30; i++ )
      {
        collection.Add( this.CreateDataItem( rnd, names[ i ] ) );
      }

      return collection;
    }

    private BusinessObject CreateDataItem( Random rnd, string name )
    {
      var item = new BusinessObject();

      item.Symbol = name;
      item.PreviousClose = this.GenerateValue( rnd );
      item.Open = this.GenerateValue( rnd );
      item.LastTrade = this.GenerateValue( rnd );
      item.Change = item.LastTrade - item.Open;
      item.DailyHigh = Math.Max( item.LastTrade, item.Open ) + Math.Round( Convert.ToDecimal( rnd.NextDouble() * 15 ), 2 );
      item.DailyLow = Math.Max( 0, Math.Min( item.LastTrade, item.Open ) * 80 / 100 );
      item.DailyVolume = Math.Round( this.GenerateValue( rnd ) * 1000 / 3, 2 );
      var date = new DateTime( 2015, 07, 01, rnd.Next( 8, 17 ), rnd.Next( 0, 60 ), rnd.Next( 0, 60 ) );
      item.LastTradeTime = date.ToString( "HH:mm:ss" );
      item.TrailingPriceOverSales = this.GenerateValue( rnd );
      item.TrailingPE = this.GenerateValue( rnd );
      item.TrailingEPS = this.GenerateValue( rnd );
      item.TrailingHigh = Math.Round( item.DailyHigh + ( this.GenerateValue( rnd ) / 10 ), 2 );
      item.TrailingLow = Math.Round( Math.Max( 0, item.DailyLow - ( this.GenerateValue( rnd ) / 10 ) ), 2 );
      item.TrailingAvgVolume = Math.Round( item.DailyVolume * this.GenerateValue( rnd ) * 100, 2 );
      item.TrailingRevenu = this.GenerateValue( rnd ) * 10000;
      item.TrailingGrossProfit = Math.Round( this.GenerateValue( rnd ) * 1000 / 3, 2 );
      item.TrailingRevenuPerShare = this.GenerateValue( rnd ) * 10;
      item.YearPriceOverSales = this.GenerateValue( rnd );
      item.YearPE = this.GenerateValue( rnd );
      item.YearEPS = this.GenerateValue( rnd );
      item.YearHigh = Math.Round( item.DailyHigh + ( this.GenerateValue( rnd ) / 10 ), 2 );
      item.YearLow = Math.Round( Math.Max( 0, item.DailyLow - ( this.GenerateValue( rnd ) / 10 ) ), 2 );
      item.YearAvgVolume = Math.Round( item.DailyVolume * this.GenerateValue( rnd ) * 100, 2 );
      item.YearRevenu = this.GenerateValue( rnd ) * 10000;
      item.YearGrossProfit = Math.Round( this.GenerateValue( rnd ) * 100, 2 );
      item.YearRevenuPerShare = this.GenerateValue( rnd ) * 10;

      return item;
    }

    private decimal GenerateValue( Random rnd )
    {
      return Math.Round( Convert.ToDecimal( rnd.NextDouble() * rnd.NextDouble() * 100d ), 2 );  // 0 to 100.00
    }

    private void SetFixedHeaderStyles( RadioButton selectedRadioButton )
    {
      if( ( grid != null ) && ( selectedRadioButton != null ) )
      {
        grid.Resources.Clear();

        var styles = selectedRadioButton.Tag as Style[];
        if( styles != null )
        {
          //Add ColumnManagerRow style and MergedColumnManagerRow Style
          foreach( var style in styles )
          {
            grid.Resources.Add( style.TargetType, style );
          }
        }
      }
    }

    #endregion PRIVATE METHODS

    #region STATIC MEMBERS

    private static string[] names = new string[]
    {
      "XCEED", "YHOO", "MSFT", "AAPL", "GOOG", "IBM", "CSCO", "DELL", "INTC", "NTGR"
      , "KONE", "VISN", "IPCI", "SYRG", "BDEV", "NXTL", "BIOA", "PNXN", "NOAH", "SHIN"
      , "DACN", "STVN", "TRV", "JPM", "MMM", "MCD", "JNJ", "PFE", "GE", "CAT"
    };

    #endregion
  }
}
