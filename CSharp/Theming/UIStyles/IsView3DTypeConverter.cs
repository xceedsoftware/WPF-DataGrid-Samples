/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [IsView3DTypeConverter.cs]
 * 
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
{
  public class IsView3DTypeConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null )
        || ( value.GetType() != typeof( bool ) ) )
      {
        return DependencyProperty.UnsetValue;
      }

      bool isView3D = ( bool )value;

      if( isView3D )
      {
        return "3D Views";
      }
      else
      {
        return "2D Views";
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      return Binding.DoNothing;
    }

    #endregion
  }
}
