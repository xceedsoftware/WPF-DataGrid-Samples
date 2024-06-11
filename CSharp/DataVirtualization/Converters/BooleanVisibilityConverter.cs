/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BooleanVisibilityConverter.cs]
 *  
 * This class implements a IValueConverter which converts
 * a boolean value into a Visibility.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  public class BooleanVisibilityConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null ) || ( !( value is bool ) ) )
        return Binding.DoNothing;

      Visibility elementVisibility;
      bool shouldCollapse = ( ( bool )value );

      if( parameter != null )
      {
        try
        {
          bool inverse = System.Convert.ToBoolean( parameter );

          if( inverse )
            shouldCollapse = !shouldCollapse;
        }
        catch
        {
        }
      }

      elementVisibility = shouldCollapse ? Visibility.Collapsed : Visibility.Visible;
      return elementVisibility;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
