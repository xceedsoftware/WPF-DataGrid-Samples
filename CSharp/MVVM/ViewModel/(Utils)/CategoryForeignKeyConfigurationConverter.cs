/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CategoryForeignKeyConfigurationConverter.cs]
 *  
 * This class retrieves the category item (as a whole) that is associated with the category property value of a given product.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections.ObjectModel;
using System.Linq;
using Xceed.Wpf.DataGrid.Samples.MVVM.Model;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
{
  public class CategoryForeignKeyConfigurationConverter : ForeignKeyConverter
  {
    public override object GetValueFromKey( object key, ForeignKeyConfiguration configuration )
    {
      if( key == null )
        return null;

      var value = ( int )key;
      var itemsSource = configuration.ItemsSource as ObservableCollection<Category>;

      //Find and return the category item that matches the CategoryID value.
      return itemsSource.FirstOrDefault( item => item.CategoryID == value );
    }
  }
}
