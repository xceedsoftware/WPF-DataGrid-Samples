/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Solid Foundation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [GreaterThanZeroConverter.cs]
 *  
 * This file demonstrates how to create a converter to evaluate if a 
 * double is greater than zero.
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

namespace Xceed.Wpf.DataGrid.Samples.LiveUpdating
{
  [ValueConversion( typeof( double ), typeof( bool ) )]
  public class GreaterThanZeroConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( bool ) ) )
        return DependencyProperty.UnsetValue;

      if( value == null )
        return DependencyProperty.UnsetValue;

      double number = 0;

      try
      {
        number = System.Convert.ToDouble( value );
      }
      catch
      {
        return DependencyProperty.UnsetValue;
      }

      return number > 0;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( double ) ) )
        return DependencyProperty.UnsetValue;

      if( value == null )
        return 0.0d;

      bool isGreaterThanZero = ( bool )value;

      if( isGreaterThanZero )
        return 1.0d;

      return 0.0d;
    }

    #endregion
  }
}
