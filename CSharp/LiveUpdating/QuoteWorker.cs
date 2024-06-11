/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [QuoteWorker.cs]
 * 
 * This class is used to refresh a quote information asynchronously.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  public class QuoteWorker : DependencyObject
  {
    private static Random LatencyRandomizer = new Random();

    public QuoteWorker( int dataRowIndex, string symbol )
    {
      m_dataRowIndex = dataRowIndex;
      m_quoteInfo = MarketSimulator.GenerateQuoteInfo( symbol );
    }

    #region MinLatency Dependency Property

    public int MinLatency
    {
      get { return ( int )GetValue( MinLatencyProperty ); }
      set { SetValue( MinLatencyProperty, value ); }
    }

    public static readonly DependencyProperty MinLatencyProperty =
        DependencyProperty.Register( "MinLatency", typeof( int ), typeof( QuoteWorker ), new UIPropertyMetadata( 0 ) );

    #endregion MinLatency Dependency Property

    #region MaxLatency Dependency Property

    public int MaxLatency
    {
      get { return ( int )GetValue( MaxLatencyProperty ); }
      set { SetValue( MaxLatencyProperty, value ); }
    }

    public static readonly DependencyProperty MaxLatencyProperty =
        DependencyProperty.Register( "MaxLatency", typeof( int ), typeof( QuoteWorker ), new UIPropertyMetadata( int.MaxValue ) );

    #endregion MaxLatency Dependency Property

    #region DataRowIndex PROPERTY

    private int m_dataRowIndex;

    public int DataRowIndex
    {
      get { return m_dataRowIndex; }
    }

    #endregion DataRowIndex PROPERTY

    #region QuoteInfo PROPERTY

    private QuoteInfo m_quoteInfo;

    public QuoteInfo QuoteInfo
    {
      get { return m_quoteInfo; }
      set { m_quoteInfo = value; }
    }

    #endregion QuoteInfo PROPERTY

    #region Started PROPERTY

    public bool Started
    {
      get
      {
        return m_started;
      }
    }

    private bool m_started;

    #endregion Started PROPERTY


    #region PUBLIC METHODS

    public void Start()
    {
      if( m_started )
        return;

      m_started = true;

      this.Continue();
    }

    public void Stop()
    {
      m_started = false;
    }

    private void Continue()
    {
      if( !m_started )
        return;

      DispatcherOperationDelegate d = delegate ()
      {
        ThreadPool.QueueUserWorkItem( new WaitCallback( this.UpdateQuoteInfoAsync ), new int[] { this.MinLatency, this.MaxLatency } );
      };

      // Invoke the QueueUserWorkItem method on the Dispatcher thread for the MinLatency and MaxLatency DPs to be accessible.
      this.Dispatcher.BeginInvoke( DispatcherPriority.Background, d );
    }

    #endregion PUBLIC METHODS

    private void UpdateQuoteInfoAsync( object state )
    {
      if( !m_started )
        return;

      // Simulate a lag in collecting data.
      int[] latencyMinMax = ( int[] )state;
      Thread.Sleep( QuoteWorker.LatencyRandomizer.Next( latencyMinMax[ 0 ], latencyMinMax[ 1 ] ) );

      // Simulate a variation in the market.
      double lastTrade = MarketSimulator.SimulateChange( m_quoteInfo );

      m_quoteInfo.SetLastTrade( lastTrade, DateTime.Now );

      this.OnQuoteInfoUpdated();

      this.Continue();
    }

    protected virtual void OnQuoteInfoUpdated()
    {
      if( this.QuoteInfoUpdated != null )
        this.QuoteInfoUpdated( this, EventArgs.Empty );
    }

    public event EventHandler QuoteInfoUpdated;

    #region PRIVATE FIELDS

    private delegate void DispatcherOperationDelegate();

    #endregion PRIVATE FIELDS

  }
}
