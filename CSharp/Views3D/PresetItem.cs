/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PresetItem.cs]
 *  
 * This class represents an item that will be used in the predefined
 * view settings combo box.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.Views3D
{
  public class PresetItem
  {
    public string Description
    {
      get;
      set;
    }

    public ResourceDictionary ResourceDictionary
    {
      get;
      set;
    }

    public Brush PreferredDataGridBackgroundBrush
    {
      get;
      set;
    }
  }
}
