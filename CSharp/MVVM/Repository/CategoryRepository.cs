/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CategoryRepository.cs]
 *  
 * This class provides the necessary links between the data source and the view model so the business logic can be conducted on categories.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid.Samples.MVVM.Model;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
{
  public class CategoryRepository : RepositoryBase
  {
    public ObservableCollection<Category> GetAllCategories()
    {
      var categoriesDataView = this.LoadDataSource( "Categories" );
      return new ObservableCollection<Category>( this.GetAllEntities<Category>( categoriesDataView ) );
    }
  }
}
