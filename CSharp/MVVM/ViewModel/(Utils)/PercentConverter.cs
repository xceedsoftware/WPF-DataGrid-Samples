/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PercentConverter.cs]
 *  
 * This class converts a value to a percentage, or vice versa.
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

namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
{
  [ValueConversion( typeof( float ), typeof( string ) )]
  public class PercentConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      //If no value, return 0%.
      if( ( value == null ) || object.Equals( string.Empty, value ) )
        return string.Format( CultureInfo.CurrentCulture, "{0}", value );

      Type valueType = value.GetType();

      // Only Float or Double values can be converted
      if( !valueType.IsAssignableFrom( typeof( float ) ) && !valueType.IsAssignableFrom( typeof( double ) ) )
        return Binding.DoNothing;

      return string.Format( CultureInfo.CurrentCulture, "{0:P0}", value );
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      var result = default( float );

      if( float.TryParse( value as string, NumberStyles.Float, CultureInfo.CurrentCulture, out result ) )
        return result;

      return Binding.DoNothing;
    }
  }
}
