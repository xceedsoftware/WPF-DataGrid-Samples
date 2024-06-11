/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [EmployeesDetailDescription.cs]
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

using System.Data;

namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
{
  public class EmployeesDetailDescription : DataGridDetailDescription
  {
    public EmployeesDetailDescription()
    {
      this.RelationName = "SubEmployees";
    }

    protected override System.Collections.IEnumerable GetDetailsForParentItem( DataGridCollectionViewBase parentCollectionViewBase, object parentItem )
    {
      DataRowView parentRowView = ( DataRowView )parentItem;

      if( parentRowView == null )
        return null;

      int employeeID = ( int )parentRowView[ "EmployeeID" ];

      DataView dataView = new DataView(
        parentRowView.Row.Table,
        "ReportsTo = " + employeeID.ToString(),
        null,
        DataViewRowState.CurrentRows );

      return dataView;
    }
  }
}
