/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [FormatItem.cs]
 *  
 * This class represents an item that will be used in the number formats
 * ComboBox and the Date/time format ComboBox of the CSV exporter settings.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  public class FormatItem
  {
    public string Description
    {
      get;
      set;
    }

    public string Format
    {
      get;
      set;
    }
  }
}
