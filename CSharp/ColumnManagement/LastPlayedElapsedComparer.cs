/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [LastPlayedElapsedComparer.cs]
 *  
 * This file demonstrates how to create a comparer for two strings representing
 * a formated time span.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public class LastPlayedElapsedComparer : IComparer
  {
    public int Compare( object x, object y )
    {
      string xStrValue = x as string;
      string yStrValue = y as string;

      if( string.IsNullOrEmpty( xStrValue ) )
      {
        if( string.IsNullOrEmpty( yStrValue ) )
        {
          return 0;
        }
        else
        {
          return -1;
        }
      }
      else
      {
        if( string.IsNullOrEmpty( yStrValue ) )
          return 1;
      }

      string[] xValueParts = xStrValue.Split( ' ' );
      string[] yValueParts = yStrValue.Split( ' ' );
      long xValue = long.Parse( xValueParts[ 0 ] );
      long yValue = long.Parse( yValueParts[ 0 ] );
      string xUnit = xValueParts[ 1 ];
      string yUnit = yValueParts[ 1 ];

      if( xUnit == yUnit )
      {
        if( xValue < yValue )
          return -1;

        if( xValue > yValue )
          return 1;

        return 0;
      }
      else
      {
        if( xUnit == "years" )
          return 1;

        if( yUnit == "years" )
          return -1;

        if( xUnit == "days" )
          return 1;

        if( yUnit == "days" )
          return -1;

        if( xUnit == "hours" )
          return 1;

        if( yUnit == "hours" )
          return -1;

        return 0;
      }
    }
  }
}
