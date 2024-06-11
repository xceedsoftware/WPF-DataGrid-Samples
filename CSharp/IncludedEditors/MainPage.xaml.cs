/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
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
using System.Net;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    private static DataTable RandomDataTable;
    private static Random Randomizer = new Random();

    private static List<string> DepartmentsList;


    private const string Consonnants = "bcdfgjklmnprstv";
    private const string Vowels = "aeiou";

    private static IPAddress GetRandomIPAddress()
    {
      byte[] bytes = new byte[ 4 ];

      MainPage.Randomizer.NextBytes( bytes );

      return new IPAddress( bytes );
    }

    private static bool GetRandomBool()
    {
      return ( MainPage.Randomizer.Next( 0, 2 ) == 1 );
    }

    private static string GetRandomName()
    {
      int firstNameSyllableLength = MainPage.Randomizer.Next( 2, 5 );
      int lastNameSyllableLength = MainPage.Randomizer.Next( 3, 7 );

      int totalSyllableLength = firstNameSyllableLength + lastNameSyllableLength;

      StringBuilder nameBuilder = new StringBuilder();

      for( int i = 0; i < totalSyllableLength; i++ )
      {
        char consonnant = MainPage.Consonnants[ MainPage.Randomizer.Next( 0, MainPage.Consonnants.Length ) ];
        char vowel = MainPage.Vowels[ MainPage.Randomizer.Next( 0, MainPage.Vowels.Length ) ];

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

    private static int GetRandomInteger()
    {
      return MainPage.Randomizer.Next( 0, 13 );
    }

    private static double GetRandomDouble( double multiplier )
    {
      return Math.Round( ( MainPage.Randomizer.NextDouble() * multiplier ), 2 );
    }

    private static string GetRandomPhoneNumber()
    {
      return "(555) 555-" +
        MainPage.Randomizer.Next( 0, 10 ) +
        MainPage.Randomizer.Next( 0, 10 ) +
        MainPage.Randomizer.Next( 0, 10 ) +
        MainPage.Randomizer.Next( 0, 10 );
    }

    private static string GetRandomDepartment()
    {
      return MainPage.DepartmentsList[ Randomizer.Next( 0, MainPage.DepartmentsList.Count ) ];
    }

    private static DateTime GetRandomDateTime()
    {
      return new DateTime(
        MainPage.Randomizer.Next( 1990, 2010 ),
        MainPage.Randomizer.Next( 1, 13 ),
        MainPage.Randomizer.Next( 1, 28 ),
        MainPage.Randomizer.Next( 0, 23 ),
        MainPage.Randomizer.Next( 0, 60 ),
        MainPage.Randomizer.Next( 0, 60 ) );
    }

    static MainPage()
    {
      MainPage.DepartmentsList = new List<string>();
      MainPage.DepartmentsList.Add( "Development" );
      MainPage.DepartmentsList.Add( "Direction" );
      MainPage.DepartmentsList.Add( "Human Resources" );
      MainPage.DepartmentsList.Add( "Networking" );
      MainPage.DepartmentsList.Add( "Sales" );
      MainPage.DepartmentsList.Add( "Support" );

      MainPage.RandomDataTable = new DataTable();

      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Included", typeof( bool ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Name", typeof( string ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "IPAddress", typeof( IPAddress ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "PhoneNumber", typeof( string ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Category", typeof( string ) ) );

      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Price", typeof( double ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Scientific", typeof( double ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "Quantity", typeof( int ) ) );

      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime1", typeof( DateTime ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime2", typeof( DateTime ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime3", typeof( DateTime ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime4", typeof( DateTime ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime5", typeof( DateTime ) ) );
      MainPage.RandomDataTable.Columns.Add( new DataColumn( "DateTime6", typeof( DateTime ) ) );

      for( int i = 0; i < 30; i++ )
      {
        object[] values = new object[] {
          MainPage.GetRandomBool(),
          MainPage.GetRandomName(),
          MainPage.GetRandomIPAddress(),
          MainPage.GetRandomPhoneNumber(),
          MainPage.GetRandomDepartment(),
          MainPage.GetRandomDouble( 100 ),
          MainPage.GetRandomDouble( double.MaxValue ),
          MainPage.GetRandomInteger(),
          MainPage.GetRandomDateTime(),
          MainPage.GetRandomDateTime(),
          MainPage.GetRandomDateTime(),
          MainPage.GetRandomDateTime(),
          MainPage.GetRandomDateTime(),
          MainPage.GetRandomDateTime() };

        MainPage.RandomDataTable.Rows.Add( values );
      }
    }

    public MainPage()
    {
      InitializeComponent();
    }

    public DataTable RandomData
    {
      get
      {
        return MainPage.RandomDataTable;
      }
    }

    public List<String> Departments
    {
      get
      {
        return MainPage.DepartmentsList;
      }
    }
  }
}
