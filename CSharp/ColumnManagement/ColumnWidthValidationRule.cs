/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ColumnWidthValidationRule.cs]
 *  
 * This class implements the ColumnWidthValidationRule class which validates values
 * assigned to a ColumnWidth property. It will be used by the ColumnPropertyCell
 * class CellEditor.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Globalization;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public class ColumnWidthValidationRule : ValidationRule
  {
    public override ValidationResult Validate( object value, CultureInfo cultureInfo )
    {
      if( value == null )
        return new ValidationResult( false, "Null is not a valid value." );

      if( !s_columnWidthConverter.CanConvertFrom( value.GetType() ) )
        return new ValidationResult( false, "Cannot convert from type " + value.GetType().Name + "." );

      try
      {
        s_columnWidthConverter.ConvertFrom( null, cultureInfo, value );
      }
      catch( FormatException )
      {
        return new ValidationResult( false, "Invalid value." );
      }
      catch( OverflowException )
      {
        return new ValidationResult( false, "The provided value was outside of the accepted range." );
      }
      catch( Exception ex )
      {
        return new ValidationResult( false, ex.Message );
      }

      return new ValidationResult( true, null );
    }

    private static Xceed.Wpf.DataGrid.Converters.ColumnWidthConverter s_columnWidthConverter = new Converters.ColumnWidthConverter();
  }
}
