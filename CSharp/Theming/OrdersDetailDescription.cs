/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [OrdersDetailDescription.cs]
 * 
 * Custom detail description that returns the appropriate details for the
 * parent item received in the GetDetailsForParentItem override.
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections;
using System.Data;

namespace Xceed.Wpf.DataGrid.Samples.Theming
{
  public class OrdersDetailDescription : DataGridDetailDescription
  {
    public OrdersDetailDescription()
    {
      this.RelationName = "RelatedCustomers";
    }

    protected override IEnumerable GetDetailsForParentItem( DataGridCollectionViewBase parentCollectionViewBase, object parentItem )
    {
      DataRowView parentRowView = ( DataRowView )parentItem;

      if( parentRowView == null )
        return null;

      string CustomerID = ( string )parentRowView[ "CustomerID" ];

      DataView dataView = new DataView(
        parentRowView.Row.Table,
        "CustomerID = " + "'" + CustomerID + "'",
        null,
        DataViewRowState.CurrentRows );

      return dataView;
    }
  }
}
