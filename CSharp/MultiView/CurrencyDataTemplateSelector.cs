/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CurrencyDataTemplateSelector.cs]
 *  
 * This class defines a DataTemplateSelector for currency values.
 * It returns the appropriate DataTemplate depending of the grid's
 * current view.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.MultiView
{
  public class CurrencyDataTemplateSelector : DataTemplateSelector
  {
    #region Singleton Property

    public static CurrencyDataTemplateSelector Singleton
    {
      get
      {
        if( _singleton == null )
          _singleton = new CurrencyDataTemplateSelector();

        return _singleton;
      }
    }

    private static CurrencyDataTemplateSelector _singleton;

    #endregion Singleton Property

    #region PUBLIC METHODS

    public override DataTemplate SelectTemplate( object item, DependencyObject container )
    {
      DataGridContext dataGridContext = DataGridControl.GetDataGridContext( container );

      DataGridControl parentDataGrid = ( dataGridContext != null )
        ? dataGridContext.DataGridControl
        : null;

      if( parentDataGrid == null )
        return base.SelectTemplate( item, container );

      // When the View is null, it is internally set to TableView.
      if( ( parentDataGrid.View == null )
        || ( parentDataGrid.View is Xceed.Wpf.DataGrid.Views.TableView ) )
      {
        // When the grid's view is TableView, we want to right-align the value.
        return parentDataGrid.FindResource( "currencyCellDataTemplateRightAligned" ) as DataTemplate;
      }

      return parentDataGrid.FindResource( "currencyCellDataTemplateDefaultAligned" ) as DataTemplate;
    }

    #endregion PUBLIC METHODS
  }
}
