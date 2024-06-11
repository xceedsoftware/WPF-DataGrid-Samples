/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BooleanToVisibilityConverter.cs]
 *  
 * This file demonstrates how to create a converter from a boolean
 * value to a Visibility value.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( Visibility ) ) )
        return DependencyProperty.UnsetValue;

      if( value == null || ( bool )value == false )
        return Visibility.Collapsed;

      return Visibility.Visible;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      return Binding.DoNothing;
    }
  }
}
