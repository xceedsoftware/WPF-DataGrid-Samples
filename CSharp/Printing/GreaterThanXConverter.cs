/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Printing Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [GreaterThanXConverter.cs]
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

namespace Xceed.Wpf.DataGrid.Samples.Printing
{
  [ValueConversion( typeof( object ), typeof( bool ) )]
  public class GreaterThanXConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( bool ) ) )
        return DependencyProperty.UnsetValue;

      if( ( value == null ) || ( value.Equals( string.Empty ) ) )
        return false;

      double number = 0;
      double numberParameter = 0;

      try
      {
        number = System.Convert.ToDouble( value );
        numberParameter = System.Convert.ToDouble( parameter );
      }
      catch
      {
        return false;
      }

      return number > numberParameter;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      return Binding.DoNothing;
    }

    #endregion
  }
}
