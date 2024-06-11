/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [InverseVisibilityConverter.cs]
 *  
 * Converter that converts a Hidden or Collapsed Visibility value to Visible
 * and vice-versa.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
{
  public class InverseVisibilityConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( !targetType.IsAssignableFrom( typeof( Visibility ) ) ) &&
        ( !value.GetType().IsAssignableFrom( typeof( Visibility ) ) ) )
      {
        return DependencyProperty.UnsetValue;
      }

      switch( ( Visibility )value )
      {
        case Visibility.Collapsed:
        case Visibility.Hidden:
          {
            return Visibility.Visible;
          }

        case Visibility.Visible:
          {
            return Visibility.Hidden;
          }
      }

      return ( Visibility )value;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      throw new NotImplementedException();
    }
  }
}
