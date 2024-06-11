/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [SupplierRepository.cs]
 *  
 * This class provides the necessary links between the data source and the view model so the business logic can be conducted on suppliers.
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
  public class SupplierRepository : RepositoryBase
  {
    public ObservableCollection<Supplier> GetAllSuppliers()
    {
      var suppliersDataView = this.LoadDataSource( "Suppliers" );
      return new ObservableCollection<Supplier>( this.GetAllEntities<Supplier>( suppliersDataView ) );
    }
  }
}
