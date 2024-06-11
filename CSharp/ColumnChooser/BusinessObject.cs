/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BusinessObject.cs]
 *  
 * This class implements the business object containing various  
 * data offered by the sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
{
  public sealed class BusinessObject : INotifyPropertyChanged
  {
    #region Properties

    #region Change Property

    public decimal Change
    {
      get
      {
        return m_change;
      }
      set
      {
        m_change = value;
        this.RaisePropertyChanged( "Change" );
      }
    }

    private decimal m_change;

    #endregion

    #region DailyHigh Property

    public decimal DailyHigh
    {
      get
      {
        return m_dailyHigh;
      }
      set
      {
        m_dailyHigh = value;
        this.RaisePropertyChanged( "DailyHigh" );
      }
    }

    private decimal m_dailyHigh;

    #endregion

    #region DailyLow Property

    public decimal DailyLow
    {
      get
      {
        return m_dailyLow;
      }
      set
      {
        m_dailyLow = value;
        this.RaisePropertyChanged( "DailyLow" );
      }
    }

    private decimal m_dailyLow;

    #endregion

    #region DailyVolume Property

    public decimal DailyVolume
    {
      get
      {
        return m_dailyVolume;
      }
      set
      {
        m_dailyVolume = value;
        this.RaisePropertyChanged( "DailyVolume" );
      }
    }

    private decimal m_dailyVolume;

    #endregion

    #region LastTrade Property

    public decimal LastTrade
    {
      get
      {
        return m_lastTrade;
      }
      set
      {
        m_lastTrade = value;
        this.RaisePropertyChanged( "LastTrade" );
      }
    }

    private decimal m_lastTrade;

    #endregion

    #region LastTradeTime Property

    public string LastTradeTime
    {
      get
      {
        return m_lastTradeTime;
      }
      set
      {
        m_lastTradeTime = value;
        this.RaisePropertyChanged( "LastTradeTime" );
      }
    }

    private string m_lastTradeTime;

    #endregion

    #region Open Property

    public decimal Open
    {
      get
      {
        return m_open;
      }
      set
      {
        m_open = value;
        this.RaisePropertyChanged( "Open" );
      }
    }

    private decimal m_open;

    #endregion

    #region PreviousClose Property

    public decimal PreviousClose
    {
      get
      {
        return m_previousClose;
      }
      set
      {
        m_previousClose = value;
        this.RaisePropertyChanged( "PreviousClose" );
      }
    }

    private decimal m_previousClose;

    #endregion

    #region Symbol Property

    public string Symbol
    {
      get
      {
        return m_symbol;
      }
      set
      {
        m_symbol = value;
        this.RaisePropertyChanged( "Symbol" );
      }
    }

    private string m_symbol;

    #endregion

    #region TrailingAvgVolume Property

    public decimal TrailingAvgVolume
    {
      get
      {
        return m_trailingAvgVolume;
      }
      set
      {
        m_trailingAvgVolume = value;
        this.RaisePropertyChanged( "TrailingAvgVolume" );
      }
    }

    private decimal m_trailingAvgVolume;

    #endregion

    #region TrailingEPS Property

    public decimal TrailingEPS
    {
      get
      {
        return m_trailingEPS;
      }
      set
      {
        m_trailingEPS = value;
        this.RaisePropertyChanged( "TrailingEPS" );
      }
    }

    private decimal m_trailingEPS;

    #endregion

    #region TrailingGrossProfit Property

    public decimal TrailingGrossProfit
    {
      get
      {
        return m_trailingGrossProfit;
      }
      set
      {
        m_trailingGrossProfit = value;
        this.RaisePropertyChanged( "TrailingGrossProfit" );
      }
    }

    private decimal m_trailingGrossProfit;

    #endregion

    #region TrailingHigh Property

    public decimal TrailingHigh
    {
      get
      {
        return m_trailingHigh;
      }
      set
      {
        m_trailingHigh = value;
        this.RaisePropertyChanged( "TrailingHigh" );
      }
    }

    private decimal m_trailingHigh;

    #endregion

    #region TrailingLow Property

    public decimal TrailingLow
    {
      get
      {
        return m_trailingLow;
      }
      set
      {
        m_trailingLow = value;
        this.RaisePropertyChanged( "TrailingLow" );
      }
    }

    private decimal m_trailingLow;

    #endregion

    #region TrailingPE Property

    public decimal TrailingPE
    {
      get
      {
        return m_trailingPE;
      }
      set
      {
        m_trailingPE = value;
        this.RaisePropertyChanged( "TrailingPE" );
      }
    }

    private decimal m_trailingPE;

    #endregion

    #region TrailingPriceOverSales Property

    public decimal TrailingPriceOverSales
    {
      get
      {
        return m_trailingPriceOverSales;
      }
      set
      {
        m_trailingPriceOverSales = value;
        this.RaisePropertyChanged( "TrailingPriceOverSales" );
      }
    }

    private decimal m_trailingPriceOverSales;

    #endregion

    #region TrailingRevenu Property

    public decimal TrailingRevenu
    {
      get
      {
        return m_trailingRevenu;
      }
      set
      {
        m_trailingRevenu = value;
        this.RaisePropertyChanged( "TrailingRevenu" );
      }
    }

    private decimal m_trailingRevenu;

    #endregion

    #region TrailingRevenuPerShare Property

    public decimal TrailingRevenuPerShare
    {
      get
      {
        return m_trailingRevenuPerShare;
      }
      set
      {
        m_trailingRevenuPerShare = value;
        this.RaisePropertyChanged( "TrailingRevenuPerShare" );
      }
    }

    private decimal m_trailingRevenuPerShare;

    #endregion

    #region YearAvgVolume Property

    public decimal YearAvgVolume
    {
      get
      {
        return m_yearAvgVolume;
      }
      set
      {
        m_yearAvgVolume = value;
        this.RaisePropertyChanged( "YearAvgVolume" );
      }
    }

    private decimal m_yearAvgVolume;

    #endregion

    #region YearEPS Property

    public decimal YearEPS
    {
      get
      {
        return m_yearEPS;
      }
      set
      {
        m_yearEPS = value;
        this.RaisePropertyChanged( "YearEPS" );
      }
    }

    private decimal m_yearEPS;

    #endregion

    #region YearGrossProfit Property

    public decimal YearGrossProfit
    {
      get
      {
        return m_yearGrossProfit;
      }
      set
      {
        m_yearGrossProfit = value;
        this.RaisePropertyChanged( "YearGrossProfit" );
      }
    }

    private decimal m_yearGrossProfit;

    #endregion

    #region YearHigh Property

    public decimal YearHigh
    {
      get
      {
        return m_yearHigh;
      }
      set
      {
        m_yearHigh = value;
        this.RaisePropertyChanged( "YearHigh" );
      }
    }

    private decimal m_yearHigh;

    #endregion

    #region YearLow Property

    public decimal YearLow
    {
      get
      {
        return m_yearLow;
      }
      set
      {
        m_yearLow = value;
        this.RaisePropertyChanged( "YearLow" );
      }
    }

    private decimal m_yearLow;

    #endregion

    #region YearPE Property

    public decimal YearPE
    {
      get
      {
        return m_yearPE;
      }
      set
      {
        m_yearPE = value;
        this.RaisePropertyChanged( "YearPE" );
      }
    }

    private decimal m_yearPE;

    #endregion

    #region YearPriceOverSales Property

    public decimal YearPriceOverSales
    {
      get
      {
        return m_yearPriceOverSales;
      }
      set
      {
        m_yearPriceOverSales = value;
        this.RaisePropertyChanged( "YearPriceOverSales" );
      }
    }

    private decimal m_yearPriceOverSales;

    #endregion

    #region YearRevenu Property

    public decimal YearRevenu
    {
      get
      {
        return m_yearRevenu;
      }
      set
      {
        m_yearRevenu = value;
        this.RaisePropertyChanged( "YearRevenu" );
      }
    }

    private decimal m_yearRevenu;

    #endregion

    #region YearRevenuPerShare Property

    public decimal YearRevenuPerShare
    {
      get
      {
        return m_yearRevenuPerShare;
      }
      set
      {
        m_yearRevenuPerShare = value;
        this.RaisePropertyChanged( "YearRevenuPerShare" );
      }
    }

    private decimal m_yearRevenuPerShare;

    #endregion

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged( string propertyName )
    {
      if( this.PropertyChanged != null )
      {
        this.PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
      }
    }

    #endregion
  }
}
