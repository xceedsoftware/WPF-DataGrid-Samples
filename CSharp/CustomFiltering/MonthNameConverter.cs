/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MontNameConverter.cs]
 *  
 * This class is used to get the Month name from an integer from 1 to 12
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
{
  public class MonthNameConverter : IValueConverter
  {
    public MonthNameConverter()
    {
      var monthNames = DateTimeFormatInfo.InvariantInfo.MonthNames;

      for( int i = 0; i < monthNames.Length; i++ )
      {
        var monthName = monthNames[ i ];

        m_indexToMonthName.Add( i + 1, monthName );
        m_monthNameToIndex.Add( monthName, i + 1 );
      }
    }

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value == null )
        return Binding.DoNothing;

      try
      {
        return m_indexToMonthName[ ( int )value ];
      }
      catch
      {
        return DependencyProperty.UnsetValue;
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value == null )
        return Binding.DoNothing;

      try
      {
        return m_monthNameToIndex[ ( string )value ];
      }
      catch
      {
        return DependencyProperty.UnsetValue;
      }
    }

    private readonly Dictionary<string, int> m_monthNameToIndex = new Dictionary<string, int>();
    private readonly Dictionary<int, string> m_indexToMonthName = new Dictionary<int, string>();
  }
}
