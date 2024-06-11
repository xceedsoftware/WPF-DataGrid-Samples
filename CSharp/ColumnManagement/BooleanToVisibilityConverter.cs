/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BooleanToVisibilityConverter.cs]
 *  
 * This file demonstrates how to create a converter that converters 
 * a boolean value to a Visibility value.
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
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( Visibility ) ) )
        return DependencyProperty.UnsetValue;

      Visibility visibilityWhenTrue = Visibility.Hidden;

      if( parameter is string )
        visibilityWhenTrue = ( Visibility )Enum.Parse( typeof( Visibility ), parameter as string, true );

      if( ( value != null ) && ( ( bool )value == true ) )
        return visibilityWhenTrue;

      if( visibilityWhenTrue == Visibility.Visible )
      {
        return Visibility.Collapsed;
      }
      else
      {
        return Visibility.Visible;
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      return Binding.DoNothing;
    }
  }
}
