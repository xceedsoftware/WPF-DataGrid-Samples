/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [YearValidationRule.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  public class YearValidationRule : ValidationRule
  {
    public override ValidationResult Validate( object value, System.Globalization.CultureInfo cultureInfo )
    {
      if( value == null )
        return new ValidationResult( false, "Chosen year cannot be null." );

      int year = ( int )value;

      if( year > DateTime.Now.Year )
        return new ValidationResult( false, "Chosen year cannot be greater than this year." );

      return ValidationResult.ValidResult;
    }
  }
}
