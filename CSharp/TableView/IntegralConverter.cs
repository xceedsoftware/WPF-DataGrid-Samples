/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [IntegralConverter.cs]
 *  
 * This file demonstrates how to create a converter to display a long
 * in a numerical formatting ( adding a thousand separator )
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

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  [ValueConversion( typeof( long ), typeof( string ) )]
  public class IntegralConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( ( value != null ) && ( !object.Equals( string.Empty, value ) ) )
      {
        try
        {
          // Convert the string value provided by an editor to a long before formatting. 
          long tempLong = System.Convert.ToInt64( value, CultureInfo.CurrentCulture );
          return string.Format( CultureInfo.CurrentCulture, "{0:#,##0}", tempLong );
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
      return long.Parse( value as string, NumberStyles.Integer, CultureInfo.CurrentCulture );
    }
  }
}
