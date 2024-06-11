/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [QuoteInfo.cs]
 * 
 * This class represents a quote fetched information.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  public class QuoteInfo
  {
    public QuoteInfo( string symbol )
      : this( symbol, 0.0d, 0.0d, 0.0d, MarketSimulator.Popularities.Passive, MarketSimulator.Tendencies.Stable )
    {
    }

    internal QuoteInfo(
      string symbol,
      double previousClose,
      double openingValue,
      double lastTrade,
      MarketSimulator.Popularities popularity,
      MarketSimulator.Tendencies tendency )
    {
      m_symbol = symbol;
      m_previousClose = previousClose;
      m_openingValue = openingValue;
      m_lastTrade = lastTrade;
      m_tradeTime = "N/A";

      m_popularity = popularity;
      m_tendency = tendency;
    }

    #region Symbol PROPERTY

    public string Symbol
    {
      get { return m_symbol; }
    }

    private string m_symbol;

    #endregion Symbol PROPERTY

    #region PreviousClose PROPERTY

    public double PreviousClose
    {
      get { return m_previousClose; }
    }

    private double m_previousClose;

    #endregion PreviousClose PROPERTY

    #region OpeningValue PROPERTY

    public double OpeningValue
    {
      get { return m_openingValue; }
    }

    private double m_openingValue;

    #endregion OpeningValue PROPERTY

    #region LastTrade PROPERTY

    private double m_lastTrade;

    public double LastTrade
    {
      get { return m_lastTrade; }
    }

    #endregion LastTrade PROPERTY

    #region TradeTime PROPERTY

    private string m_tradeTime;

    public string TradeTime
    {
      get { return m_tradeTime; }
    }

    #endregion TradeTime PROPERTY

    #region INTERNAL METHODS

    internal void SetLastTrade( double lastTrade, DateTime tradeTime )
    {
      m_lastTrade = lastTrade;
      m_tradeTime = tradeTime.ToString( "HH:mm:ss:FF" );
    }

    #endregion INTERNAL METHODS

    #region INTERNAL PROPERTIES

    internal MarketSimulator.Popularities Popularity
    {
      get
      {
        return m_popularity;
      }
    }

    private MarketSimulator.Popularities m_popularity;

    internal MarketSimulator.Tendencies Tendency
    {
      get
      {
        return m_tendency;
      }
    }

    private MarketSimulator.Tendencies m_tendency;

    #endregion INTERNAL PROPERTIES
  }
}
