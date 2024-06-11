/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [GreaterThanConverter.cs]
 *  
 * This class demonstrates how to create a converter that compares
 * a numeric value to an comparison value (0 by default) and returns 
 * true if it is greater.
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

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  [ValueConversion( typeof( object ), typeof( bool ) )]
  public class GreaterThanConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( bool ) ) )
        return DependencyProperty.UnsetValue;

      if( value == null )
        return false;

      double number = 0d;
      double comparedToValue = 0d;

      try
      {
        number = System.Convert.ToDouble( value );
      }
      catch
      {
        return false;
      }

      if( parameter != null )
      {
        try
        {
          comparedToValue = System.Convert.ToDouble( parameter );
        }
        catch
        {
          return DependencyProperty.UnsetValue;
        }
      }

      return number > comparedToValue;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      return Binding.DoNothing;
    }
  }
}
