/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Selection Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BoolToVisibilityConverter.cs]
 * 
 * This is the converter from bool to Visibility.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
{
  public class BoolToVisibilityConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null )
        || ( value.GetType() != typeof( bool ) ) )
      {
        return DependencyProperty.UnsetValue;
      }

      bool boolValue = ( bool )value;

      bool isReversed = false;
      string stringParameter = ( string )parameter;
      if( !string.IsNullOrEmpty( stringParameter ) )
      {
        isReversed = true;
      }

      if( ( boolValue && !isReversed )
        || ( !boolValue && isReversed ) )
      {
        return Visibility.Visible;
      }
      else
      {
        return Visibility.Collapsed;
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null )
        || ( value.GetType() != typeof( Visibility ) ) )
      {
        return DependencyProperty.UnsetValue;
      }

      Visibility visibility = ( Visibility )value;

      switch( visibility )
      {
        case Visibility.Collapsed:
        case Visibility.Hidden:
          return false;

        default:
          return true;
      }
    }

    #endregion
  }
}
