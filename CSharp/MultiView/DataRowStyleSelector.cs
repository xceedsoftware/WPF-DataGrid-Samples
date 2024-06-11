/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [DataRowStyleSelector.cs]
 *  
 * This class defines a StyleSelector for data rows.
 * It returns the appropriate Style depending of the grid's
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
  public class DataRowStyleSelector : StyleSelector
  {
    #region Singleton Property

    public static DataRowStyleSelector Singleton
    {
      get
      {
        if( _singleton == null )
          _singleton = new DataRowStyleSelector();

        return _singleton;
      }
    }

    private static DataRowStyleSelector _singleton;

    #endregion Singleton Property

    #region PUBLIC METHODS

    public override System.Windows.Style SelectStyle( object item, DependencyObject container )
    {
      DataGridContext dataGridContext = DataGridControl.GetDataGridContext( container );

      DataGridControl parentDataGrid = ( dataGridContext != null )
        ? dataGridContext.DataGridControl
        : null;

      if( parentDataGrid != null )
      {
        if( ( parentDataGrid.View == null )
          || ( parentDataGrid.View is Xceed.Wpf.DataGrid.Views.TableView ) )
        {
          return parentDataGrid.FindResource( "tableViewDataRowStyle" ) as Style;
        }
        else if( ( parentDataGrid.View is Xceed.Wpf.DataGrid.Views.CardView )
          || ( parentDataGrid.View is Xceed.Wpf.DataGrid.Views.CompactCardView ) )
        {
          return parentDataGrid.FindResource( "cardViewDataRowStyle" ) as Style;
        }
      }

      return base.SelectStyle( item, container );
    }

    #endregion PUBLIC METHODS
  }
}
