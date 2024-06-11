/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [NameConverter.cs]
 *  
 * This file demonstrates how to create a converter to display a combined
 * name from 2 values
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.Selection
{
  public class NameConverter : IValueConverter
  {
    object IValueConverter.Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value == null )
        return null;

      //By default, the parameter will contain the ItemsSource define on the ForeignKeyConfiguration when this converter is set as a ForeignKeyValueConverter.
      var employees = parameter as DataView;
      if( employees == null )
        return null;

      employees.Sort = "EmployeeID";

      var index = employees.Find( value );
      var dataRow = employees[ index ];

      return dataRow[ "LastName" ] + ", " + dataRow[ "FirstName" ];
    }

    object IValueConverter.ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      return null;
    }
  }

  public class NameMutliConverter : IMultiValueConverter
  {
    public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
    {
      if( values[ 0 ] == DependencyProperty.UnsetValue )
        values[ 0 ] = string.Empty;

      if( values[ 1 ] == DependencyProperty.UnsetValue )
        values[ 1 ] = string.Empty;

      string name;

      if( string.IsNullOrEmpty( values[ 0 ] as string ) )
        return values[ 1 ] as string;

      if( string.IsNullOrEmpty( values[ 1 ] as string ) )
        return values[ 0 ] as string;

      switch( ( string )parameter )
      {
        case "FormatLastFirst":
          name = ( string )values[ 1 ] + ", " + ( string )values[ 0 ];
          break;

        case "FormatNormal":
        default:
          name = ( string )values[ 0 ] + " " + ( string )values[ 1 ];
          break;
      }

      return name;
    }

    public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
    {
      return null;
    }
  }
}
