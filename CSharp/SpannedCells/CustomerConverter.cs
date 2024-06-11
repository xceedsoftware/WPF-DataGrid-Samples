/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CustomerConverter.cs]
 *  
 * This file demonstrates how to create a converter to display a company name
 * followed by an optional contact name between parentheses.
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

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public class CustomerConverter : IValueConverter
  {
    object IValueConverter.Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value == null )
        return null;

      //By default, the parameter will contain the ItemsSource define on the ForeignKeyConfiguration when this converter is set as a ForeignKeyValueConverter.
      var customers = parameter as DataView;
      if( customers == null )
        return null;

      customers.Sort = "CustomerID";

      var index = customers.Find( value );
      var dataRow = customers[ index ];

      return dataRow[ "CompanyName" ] + " - " + dataRow[ "ContactName" ];
    }

    object IValueConverter.ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      return null;
    }
  }

  public class CustomerMultiConverter : IMultiValueConverter
  {
    public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
    {
      if( values[ 0 ] == DependencyProperty.UnsetValue )
        values[ 0 ] = string.Empty;

      if( values[ 1 ] == DependencyProperty.UnsetValue )
        values[ 1 ] = string.Empty;

      string customerName = ( string )values[ 0 ];

      if( !string.IsNullOrEmpty( values[ 1 ] as string ) )
        customerName += " - " + ( string )values[ 1 ];

      return customerName;
    }

    public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
    {
      return null;
    }
  }

  public class CustomerForeignKeyConverter : DataTableForeignKeyConverter
  {
    //Only the GetValueFormKey method overload with the DataGridForeignKeyDescription parameter needs to be overridden,
    //for the base class DataTableForeignKeyConverter already provides an implementation of the other two overridable methods.
    public override object GetValueFromKey( object key, DataGridForeignKeyDescription description )
    {
      if( key == null )
        return null;

      var dataView = description.ItemsSource as DataView;
      if( dataView != null )
      {
        dataView.Sort = description.ValuePath;

        var index = dataView.Find( key );
        var dataRow = dataView[ index ];

        //Return a value built in this order, so sorting is done on company name, then contact name.
        return dataRow[ "CompanyName" ] + " - " + dataRow[ "ContactName" ];
      }

      return key;
    }
  }
}
