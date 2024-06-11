/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [EqualityConverter.cs]
 *  
 * This class implements a IValueConverter which converts
 * a value and a parameter into a boolean value corresponding to their
 * equality.
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
  public class EqualityConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( ( value == null ) || ( !( value is int ) ) )
        return Binding.DoNothing; ;

      if( parameter == null )
        return Binding.DoNothing; ;

      try
      {
        int num1 = System.Convert.ToInt32( value );
        int num2 = System.Convert.ToInt32( parameter );

        return Int32.Equals( num1, num2 );
      }
      catch
      {
        return Binding.DoNothing;
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( !( value is bool ) )
        return Binding.DoNothing;

      if( parameter == null )
        return Binding.DoNothing; ;

      bool isChecked = ( bool )value;

      if( !isChecked )
        return Binding.DoNothing;

      int returnedNum;
      try
      {
        returnedNum = System.Convert.ToInt32( parameter );

        return returnedNum;
      }
      catch
      {
        return Binding.DoNothing;
      }
    }

    #endregion
  }
}
