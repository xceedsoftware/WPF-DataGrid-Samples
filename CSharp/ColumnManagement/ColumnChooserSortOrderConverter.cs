/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ColumnChooserSortOrderEnumToStringValueConverter.cs]
 *  
 * This class implements the ColumnChooserSortOrderEnumToStringValueConverter 
 * class which translate the ColumnChooserSortOrder enum values to a displayable
 * description.
 * 
 * This class will be used in the ColumnPropertyCell class.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows.Data;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  [ValueConversion( typeof( ColumnChooserSortOrder ), typeof( string ) )]
  public class ColumnChooserSortOrderConverter : IValueConverter
  {
    #region Singleton Property

    public static ColumnChooserSortOrderConverter Singleton
    {
      get
      {
        if( _singleton == null )
          _singleton = new ColumnChooserSortOrderConverter();

        return _singleton;
      }
    }

    private static ColumnChooserSortOrderConverter _singleton;

    #endregion Singleton Property

    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( value == null )
        return string.Empty;

      ColumnChooserSortOrder sortOrder = ( ColumnChooserSortOrder )value;

      switch( sortOrder )
      {
        case ColumnChooserSortOrder.None:
          return "None";

        case ColumnChooserSortOrder.TitleAscending:
          return "Title (ascending)";

        case ColumnChooserSortOrder.TitleDescending:
          return "Title (descending)";

        case ColumnChooserSortOrder.VisiblePosition:
          return "Visible position";

        default:
          return string.Empty;
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      throw new NotImplementedException();
    }

    #endregion IValueConverter Members
  }
}
