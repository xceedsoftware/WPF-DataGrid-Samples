/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Live Updating Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MarketSimulator.cs]
 * 
 * This class provides simulated data to the Live Updating sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  internal static class MarketSimulator
  {
    private const double MaxValue = 999.99d;
    private const double MinValue = 1.00d;

    private const double MaxChangePercentageErratic = 0.8d;
    private const double MaxChangePercentageStable = 0.01d;
    private const double MaxChangePercentageSteadyGainer = 0.03d;
    private const double MaxChangePercentageSteadyLooser = 0.03d;

    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static Random Randomizer = new Random();

    private static int Next()
    {
      lock( MarketSimulator.Randomizer )
      {
        return MarketSimulator.Randomizer.Next();
      }
    }

    private static int Next( int minValue, int maxValue )
    {
      lock( MarketSimulator.Randomizer )
      {
        return MarketSimulator.Randomizer.Next( minValue, maxValue );
      }
    }

    private static double NextDouble()
    {
      lock( MarketSimulator.Randomizer )
      {
        return MarketSimulator.Randomizer.NextDouble();
      }
    }

    internal static string GenerateRandomSymbol()
    {
      StringBuilder stringBuilder = new StringBuilder();

      for( int i = 0; i < MarketSimulator.Randomizer.Next( 3, 5 ); i++ )
      {
        stringBuilder.Append( MarketSimulator.Letters[ MarketSimulator.Randomizer.Next( 0, Letters.Length ) ] );
      }

      return stringBuilder.ToString();
    }

    internal static QuoteInfo GenerateQuoteInfo( string symbol )
    {
      Popularities popularity;
      Tendencies tendency;

      if( String.Equals( symbol, "XCEED" ) )
      {
        popularity = Popularities.Active;
        tendency = Tendencies.SteadyGainer;
      }
      else
      {
        popularity = ( Popularities )MarketSimulator.Randomizer.Next( 0, Enum.GetValues( typeof( Popularities ) ).Length );
        tendency = ( Tendencies )MarketSimulator.Randomizer.Next( 0, Enum.GetValues( typeof( Tendencies ) ).Length );
      }

      double previousClose = MarketSimulator.Randomizer.Next( 5, 150 );
      double openingValue = MarketSimulator.SimulateChange( previousClose, popularity, tendency );
      double lastTrade = MarketSimulator.SimulateChange( openingValue, popularity, tendency );

      return new QuoteInfo(
        symbol,
        previousClose,
        openingValue,
        lastTrade,
        popularity,
        tendency );
    }

    public static double SimulateChange( QuoteInfo quoteInfo )
    {
      return MarketSimulator.SimulateChange( quoteInfo.LastTrade, quoteInfo.Popularity, quoteInfo.Tendency );
    }

    private static double SimulateChange( double currentValue, Popularities popularity, Tendencies tendency )
    {
      double newValue = currentValue;

      double activityFactor = ( MarketSimulator.NextDouble() * 100 );

      if( popularity == Popularities.Passive )
      {
        if( activityFactor < 66 )
          return newValue;
      }
      else if( activityFactor < 33 )
      {
        return newValue;
      }

      if( ( tendency == Tendencies.Stable ) && ( activityFactor < 33 ) )
        return newValue;


      double changeFactor = ( MarketSimulator.NextDouble() * 100 );

      switch( tendency )
      {
        case Tendencies.Erratic:
          {
            double change = currentValue * ( MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageErratic );

            if( MarketSimulator.NextDouble() < 0.5 )
            {
              newValue = ( currentValue - change );
            }
            else
            {
              newValue = ( currentValue + change );
            }

            break;
          }

        case Tendencies.SteadyGainer:
          {
            double change = currentValue * ( MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageSteadyGainer );

            if( MarketSimulator.NextDouble() < 0.25 )
            {
              newValue = ( currentValue - change );
            }
            else
            {
              newValue = ( currentValue + change );
            }

            break;
          }

        case Tendencies.SteadyLoser:
          {
            double change = currentValue * ( MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageSteadyLooser );

            if( MarketSimulator.NextDouble() < 0.75 )
            {
              newValue = ( currentValue - change );
            }
            else
            {
              newValue = ( currentValue + change );
            }

            break;
          }

        default:
          {
            double change = currentValue * ( MarketSimulator.NextDouble() * MarketSimulator.MaxChangePercentageStable );

            if( MarketSimulator.NextDouble() < 0.5 )
            {
              newValue = ( currentValue - change );
            }
            else
            {
              newValue = ( currentValue + change );
            }

            break;
          }
      }

      return Math.Min(
        Math.Max( MarketSimulator.MinValue, newValue ),
        MarketSimulator.MaxValue );
    }

    #region NESTED ENUMS

    public enum Tendencies
    {
      Stable = 0,
      Erratic = 1,
      SteadyGainer = 2,
      SteadyLoser = 3
    }

    public enum Popularities
    {
      Passive = 0,
      Active = 1
    }

    #endregion NESTED ENUMS
  }
}
