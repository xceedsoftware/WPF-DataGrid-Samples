/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PeriodVSCompositionCountCellValidationRule.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows.Controls;
using Xceed.Wpf.DataGrid.ValidationRules;

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  public class PeriodVSCompositionCountCellValidationRule : CellValidationRule
  {
    public override ValidationResult Validate( object value, System.Globalization.CultureInfo culture, CellValidationContext context )
    {
      Row parentRow = context.Cell.ParentRow;

      Period period;
      int compositionCount;

      if( context.Cell.FieldName == "Period" )
      {
        period = ( Period )value;

        object compositionCountCellCount = parentRow.Cells[ "CompositionCount" ].Content;

        compositionCount = ( compositionCountCellCount == null ) ? 0 : ( int )compositionCountCellCount;
      }
      else
      {
        period = ( Period )parentRow.Cells[ "Period" ].Content;

        compositionCount = ( value == null ) ? 0 : ( int )value;
      }

      if( ( period == Period.Modern ) && compositionCount > 49 )
        return new ValidationResult( false, "Composition count must be less than 50 when the period is set to Modern." );

      return ValidationResult.ValidResult;
    }
  }
}
