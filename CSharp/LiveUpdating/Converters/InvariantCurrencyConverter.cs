/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Solid Foundation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [InvariantCurrencyConverter.cs]
 *  
 * This file demonstrates how to create a converter to display a double
 * in a currency formatting
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  [ValueConversion( typeof( double ), typeof( string ) )]
  public class USCurrencyConverter : IValueConverter
  {
    private CultureInfo USCultureInfo = CultureInfo.GetCultureInfo( "EN-US" );

    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( ( value != null ) && ( !object.Equals( string.Empty, value ) ) )
      {
        try
        {
          return string.Format( USCultureInfo, "{0:C}", value );
        }
        catch( FormatException )
        {
        }
        catch( OverflowException )
        {
        }
      }

      return string.Format( CultureInfo.CurrentCulture, "{0}", value );
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      return double.Parse( value as string, NumberStyles.Currency, USCultureInfo );
    }
  }
}
