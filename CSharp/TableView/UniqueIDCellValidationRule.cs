/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [UniqueIDCellValidationRule.cs]
 *  
 * This file demonstrates how to create a validation rule for a Cell that
 * ensures that the cell content is unique for the column.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Data;
using System.Globalization;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.ValidationRules;

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  public class UniqueIDCellValidationRule : CellValidationRule
  {
    public UniqueIDCellValidationRule()
    {
    }

    public override ValidationResult Validate( object value, CultureInfo culture, CellValidationContext context )
    {
      // Get the DataItem from the context and cast it to a DataRow
      DataRowView dataRowView = context.DataItem as DataRowView;

      // Convert the value to a long to make sure it is numerical.
      // When the value is not numerical, then an InvalidFormatException will be thrown.
      // We let it pass unhandled to demonstrate that an exception can be thrown when validating
      // and the grid will handle it nicely.
      long id = Convert.ToInt64( value, CultureInfo.CurrentCulture );

      // Try to find another row with the same ID
      System.Data.DataRow[] existingRows = dataRowView.Row.Table.Select( context.Cell.FieldName + "=" + id.ToString( CultureInfo.InvariantCulture ) );

      // If a row is found, we return an error
      if( ( existingRows.Length != 0 ) && ( existingRows[ 0 ] != dataRowView.Row ) )
        return new ValidationResult( false, "The value must be unique" );

      // If no row was found, we return a ValidResult
      return ValidationResult.ValidResult;
    }
  }
}
