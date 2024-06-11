/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [RandomValueGenerator.cs]
 *  
 * Utility class that provides random values, and which is used to populate properties of model objects.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
{
  internal static class RandomValueGenerator
  {
    public static int GetRandomInteger( int min, int max )
    {
      return RandomValueGenerator.Randomizer.Next( min, max );
    }

    public static bool GetRandomBool()
    {
      return ( RandomValueGenerator.Randomizer.Next( 0, 2 ) == 1 );
    }

    public static string GetRandomProductName()
    {
      var wordCount = RandomValueGenerator.Randomizer.Next( 1, 5 );
      var nameBuilder = new StringBuilder();

      for( int i = 0; i < wordCount; i++ )
      {
        var syllableLength = RandomValueGenerator.Randomizer.Next( 2, 6 );

        for( int j = 0; j < syllableLength; j++ )
        {
          var consonnant = RandomValueGenerator.Consonnants[ RandomValueGenerator.Randomizer.Next( 0, RandomValueGenerator.Consonnants.Length ) ];
          var vowel = RandomValueGenerator.Vowels[ RandomValueGenerator.Randomizer.Next( 0, RandomValueGenerator.Vowels.Length ) ];

          if( j == 0 )
          {
            nameBuilder.Append( consonnant.ToString().ToUpper() );
          }
          else
          {
            nameBuilder.Append( consonnant );
          }

          nameBuilder.Append( vowel );

          if( ( j == syllableLength - 1 ) && ( i < wordCount - 1 ) )
          {
            nameBuilder.Append( ' ' );
          }
        }
      }

      return nameBuilder.ToString();
    }

    public static string GetRandomQuantityPerUnit()
    {
      return RandomValueGenerator.QuantityPerUnit[ Randomizer.Next( 0, RandomValueGenerator.QuantityPerUnit.Length ) ];
    }

    public static decimal GetRandomDecimal()
    {
      return Convert.ToDecimal( RandomValueGenerator.Randomizer.NextDouble() * 100 );
    }

    public static short GetRandomShort( int min, int max )
    {
      return Convert.ToInt16( RandomValueGenerator.Randomizer.Next( min, max ) );
    }

    public static DateTime GetRandomDateTime( int minYear, int maxYear )
    {
      return DateTime.Parse( RandomValueGenerator.Randomizer.Next( minYear, maxYear ) + "-" +
                             RandomValueGenerator.Randomizer.Next( 1, 13 ) + "-" +
                             RandomValueGenerator.Randomizer.Next( 1, 28 ) +
                             "T00:00:00-05:00" );
    }

    private static Random Randomizer = new Random();
    private const string Consonnants = "bcdfgjklmnprstv";
    private const string Vowels = "aeiou";
    private static string[] QuantityPerUnit = new string[] { "10 boxes x 20 bags",
                                                             "24 - 12 oz bottles",
                                                             "12 - 550 ml bottles",
                                                             "48 - 6 oz jars",
                                                             "36 boxes",
                                                             "12 - 8 oz jars",
                                                             "12 - 1 lb pkgs.",
                                                             "12 - 12 oz jars",
                                                             "18 - 500 g pkgs.",
                                                             "12 - 200 ml jars",
                                                             "1 kg pkg.",
                                                             "2 kg box",
                                                             "40 - 100 g pkgs.",
                                                             "24 - 250 ml bottles",
                                                             "32 - 500 g boxes" };
  }
}
