/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MontNamesDistinctValuesSortComparer.cs]
 *  
 * This class is used to compare month names in order to sort them
 * in logical order instead of alphabetical order
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
{
  public class MonthNamesDistinctValuesSortComparer : IComparer
  {
    public MonthNamesDistinctValuesSortComparer()
    {
      var monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

      for( int i = 0; i < monthNames.Length; i++ )
      {
        m_monthNameToIndex.Add( monthNames[ i ], i );
      }
    }

    #region IComparer Members

    public int Compare( object x, object y )
    {
      var xMonth = x as string;
      var yMonth = y as string;

      if( ( xMonth != null ) && ( yMonth != null ) )
      {
        var xIndex = m_monthNameToIndex[ xMonth ];
        var yIndex = m_monthNameToIndex[ yMonth ];

        if( xIndex < yIndex )
        {
          return -1;
        }
        else if( xIndex == yIndex )
        {
          return 0;
        }
        else
        {
          return 1;
        }
      }

      if( xMonth == null && yMonth == null )
        return 0;

      if( xMonth == null )
        return -1;

      if( yMonth == null )
        return 1;

      return 0;
    }

    #endregion

    private readonly Dictionary<string, int> m_monthNameToIndex = new Dictionary<string, int>();
  }
}
