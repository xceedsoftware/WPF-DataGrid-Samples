/*
 * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [RandomValueGenerator.cs]
 *  
 * This class provides a mean of generating random data in
 * the DataVirtualization Sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  internal class RandomValueGenerator
  {
    static RandomValueGenerator()
    {
      RandomValueGenerator.DepartmentsList.Add( "Development" );
      RandomValueGenerator.DepartmentsList.Add( "Direction" );
      RandomValueGenerator.DepartmentsList.Add( "Human Resources" );
      RandomValueGenerator.DepartmentsList.Add( "Networking" );
      RandomValueGenerator.DepartmentsList.Add( "Sales" );
      RandomValueGenerator.DepartmentsList.Add( "Support" );
    }


    public static object GetRandomValue( Type type )
    {
      if( type == typeof( bool ) )
      {
        return RandomValueGenerator.GetRandomBool();
      }
      else if( type == typeof( int ) )
      {
        return RandomValueGenerator.GetRandomInteger( 0, 1000 );
      }
      else if( type == typeof( string ) )
      {
        return RandomValueGenerator.GetRandomName();
      }
      else if( type == typeof( DateTime ) )
      {
        return RandomValueGenerator.GetRandomDateTime( 1990, DateTime.Now.Year );
      }
      else
      {
        return RandomValueGenerator.GetRandomPhoneNumber();
      }
    }

    public static bool GetRandomBool()
    {
      return ( RandomValueGenerator.Randomizer.Next( 0, 2 ) == 1 );
    }

    public static string GetRandomName()
    {
      int firstNameSyllableLength = RandomValueGenerator.Randomizer.Next( 2, 5 );
      int lastNameSyllableLength = RandomValueGenerator.Randomizer.Next( 3, 7 );

      int totalSyllableLength = firstNameSyllableLength + lastNameSyllableLength;

      StringBuilder nameBuilder = new StringBuilder();

      for( int i = 0; i < totalSyllableLength; i++ )
      {
        char consonnant = RandomValueGenerator.Consonnants[ RandomValueGenerator.Randomizer.Next( 0, RandomValueGenerator.Consonnants.Length ) ];
        char vowel = RandomValueGenerator.Vowels[ RandomValueGenerator.Randomizer.Next( 0, RandomValueGenerator.Vowels.Length ) ];

        if( i == firstNameSyllableLength )
        {
          nameBuilder.Append( ' ' );
        }

        if( ( i == 0 ) || ( i == firstNameSyllableLength ) )
        {
          nameBuilder.Append( consonnant.ToString().ToUpper() );
        }
        else
        {
          nameBuilder.Append( consonnant );
        }

        nameBuilder.Append( vowel );
      }

      return nameBuilder.ToString();
    }

    public static int GetRandomInteger( int min, int max )
    {
      return RandomValueGenerator.Randomizer.Next( min, max );
    }

    public static string GetRandomPhoneNumber()
    {
      return "(555) 555-" +
        RandomValueGenerator.Randomizer.Next( 0, 10 ) +
        RandomValueGenerator.Randomizer.Next( 0, 10 ) +
        RandomValueGenerator.Randomizer.Next( 0, 10 ) +
        RandomValueGenerator.Randomizer.Next( 0, 10 );
    }

    public static string GetRandomDepartment()
    {
      return RandomValueGenerator.DepartmentsList[ Randomizer.Next( 0, RandomValueGenerator.DepartmentsList.Count ) ];
    }

    public static DateTime GetRandomDateTime( int minYear, int maxYear )
    {
      return new DateTime(
             RandomValueGenerator.Randomizer.Next( minYear, maxYear ),
             RandomValueGenerator.Randomizer.Next( 1, 13 ),
             RandomValueGenerator.Randomizer.Next( 1, 28 ),
             RandomValueGenerator.Randomizer.Next( 0, 23 ),
             RandomValueGenerator.Randomizer.Next( 0, 60 ),
             RandomValueGenerator.Randomizer.Next( 0, 60 ) );
    }

    public static List<String> Departments
    {
      get
      {
        return RandomValueGenerator.DepartmentsList;
      }
    }


    private static Random Randomizer = new Random();

    private static List<string> DepartmentsList = new List<string>();

    private const string Consonnants = "bcdfgjklmnprstv";
    private const string Vowels = "aeiou";
  }
}
