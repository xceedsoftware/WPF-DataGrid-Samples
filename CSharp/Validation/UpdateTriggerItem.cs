/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [UpdateTriggerItem.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  public class UpdateTriggerItem
  {
    public UpdateTriggerItem()
    {
    }

    public string Description
    {
      get;
      set;
    }

    public string ToolTip
    {
      get;
      set;
    }

    public DataGridUpdateSourceTrigger DataGridUpdateSourceTrigger
    {
      get;
      set;
    }
  }
}
