/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Category.cs]
 *  
 * This class represents a category item.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
{
  public class Category
  {
    #region CategoryID Property

    public int CategoryID
    {
      get;
      set;
    }

    #endregion

    #region CategoryName Property

    public string CategoryName
    {
      get;
      set;
    }

    #endregion

    #region Picture Property

    public byte[] Picture
    {
      get;
      set;
    }

    #endregion
  }
}
