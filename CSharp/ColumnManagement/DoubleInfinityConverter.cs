/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [DoubleInfinityConverter.cs]
 *  
 * This class implements the DoubleInfinityConverter class which allows a string to be
 * converted to a double. This converter accepts the string "infinity" and other variants 
 * as a value (converted to Double.PositiveInfinity).
 * 
 * This class will be used in the ColumnPropertyCell class.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  [ValueConversion( typeof( double ), typeof( string ) )]
  public class DoubleInfinityConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      // We don't want to use the CurrentUICulture (the language) received in the culture 
      // parameter, but rather the CurrentCulture (the culture/ControlPanel settings).
      if( value is double )
        return System.Convert.ToString( ( double )value, CultureInfo.CurrentCulture.NumberFormat );

      return value;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( targetType != typeof( double ) )
        return DependencyProperty.UnsetValue;

      string strValue = value as string;

      if( strValue != null )
      {
        double dblValue;

        strValue.ToLower( culture );
        strValue.Trim();

        if( ( strValue == "infinity" ) ||
            ( strValue == "+infinity" ) ||
            ( strValue == "+inf" ) ||
            ( strValue == "inf" ) )
          return double.PositiveInfinity;

        if( double.TryParse( strValue, out dblValue ) )
          return dblValue;
      }

      return value;
    }
  }
}
