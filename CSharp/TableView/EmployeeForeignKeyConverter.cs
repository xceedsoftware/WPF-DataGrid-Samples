/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Selection™ View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [EmployeeForeignKeyConverter.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System.Data;

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  //Only the GetValueFormKey method overload with the DataGridForeignKeyDescription parameter needs to be overridden,
  //for the base class DataTableForeignKeyConverter already provides an implementation of the other two overridable methods.
  public class EmployeeForeignKeyConverter : DataTableForeignKeyConverter
  {
    public override object GetValueFromKey( object key, DataGridForeignKeyDescription description )
    {
      if( key == null )
        return null;

      var dataView = description.ItemsSource as DataView;
      if( dataView != null )
      {
        dataView.Sort = description.ValuePath;

        var index = dataView.Find( key );
        var dataRow = dataView[ index ];

        //Return a value built in this order, so sorting is done on last name, then first name.
        return dataRow[ "LastName" ] + ", " + dataRow[ "FirstName" ];
      }

      return key;
    }
  }
}
