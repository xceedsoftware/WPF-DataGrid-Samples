/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PersonObservableCollection.cs]
 *  
 * Custom ObservableCollection of Person objects that is used as the source
 * of the DataGridCollectionView to which the grid is bound.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System.Collections.ObjectModel;

namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
{
  internal class PersonObservableCollection : ObservableCollection<Person>
  {
    internal PersonObservableCollection()
      : base()
    {
    }
  }
}
