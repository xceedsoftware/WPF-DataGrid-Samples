/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [SeparatorItem.cs]
 *  
 * This class represents an item that will be used in the separators
 * ComboBox of the CSV exporter settings.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  public class SeparatorItem
  {
    public string Description
    {
      get;
      set;
    }

    public char Separator
    {
      get;
      set;
    }
  }
}
