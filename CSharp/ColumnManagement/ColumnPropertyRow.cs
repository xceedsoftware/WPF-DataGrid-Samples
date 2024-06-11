/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ColumnPropertyRow.cs]
 *  
 * This class implements the ColumnPropertyRow class, which allows its cells to
 * edit any numeric value of their sibling cells contained in the same column.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public class ColumnPropertyRow : Row
  {
    #region ColumnProperty Property

    // This property gets or sets a Column's DependencyProperty that will be 
    // displayed and edited in the row's cells.

    public static readonly DependencyProperty ColumnPropertyProperty = DependencyProperty.Register(
      "ColumnProperty",
      typeof( DependencyProperty ),
      typeof( ColumnPropertyRow ),
      new UIPropertyMetadata( null ),
      new ValidateValueCallback( ColumnPropertyRow.ValidateColumnPropertyCallback ) );

    public DependencyProperty ColumnProperty
    {
      get
      {
        return ( DependencyProperty )this.GetValue( ColumnPropertyRow.ColumnPropertyProperty );
      }

      set
      {
        this.SetValue( ColumnPropertyRow.ColumnPropertyProperty, value );
      }
    }

    // Only a dependency property of type double or ColumnWidth is accepted.
    private static bool ValidateColumnPropertyCallback( object value )
    {
      if( value == null )
        return true;

      DependencyProperty dp = value as DependencyProperty;

      if( dp == null )
        return false;

      return ( dp.PropertyType == typeof( double ) ) || ( dp.PropertyType == typeof( ColumnWidth ) );
    }

    #endregion ColumnProperty Property

    protected override Cell CreateCell( ColumnBase column )
    {
      return new ColumnPropertyCell();
    }

    protected override bool IsValidCellType( Cell cell )
    {
      return ( cell is ColumnPropertyCell );
    }
  }
}
