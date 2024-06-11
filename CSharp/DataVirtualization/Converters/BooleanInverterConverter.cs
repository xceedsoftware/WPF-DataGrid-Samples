/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [BooleanInverterConverter.cs]
 *  
 * This class implements a IValueConverter which inverts a boolean value.
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
  public class BooleanInverterConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null ) || ( !( value is bool ) ) )
        return false;

      bool inverseValue = !( bool )value;

      return inverseValue;
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
