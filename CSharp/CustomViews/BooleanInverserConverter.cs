/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BooleanInverserConverter.cs]
 *  
 * This class is a Converter (typically used in a Binding) that returns the inverse 
 * value of a boolean.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.CustomViews
{
  [ ValueConversion( typeof( bool ), typeof( bool ) ) ]
  public class BooleanInverserConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value is bool )
        return !( bool )value;

      return value;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value is bool )
        return !( bool )value;

      return value;
    }
  }
}
