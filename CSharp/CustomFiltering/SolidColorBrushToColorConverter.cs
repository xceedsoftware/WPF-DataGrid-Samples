/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [SolidColorBrushToColorConverter.cs]
 *  
 * This class is used to get the Color of a SolidColorBrush.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
{
  public class SolidColorBrushToColorConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      SolidColorBrush brush = value as SolidColorBrush;
      if( brush != null )
        return brush.Color;

      return default( Color );
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value != null )
      {
        Color color = ( Color )value;
        return new SolidColorBrush( color );
      }

      return default( SolidColorBrush );
    }
  }
}
