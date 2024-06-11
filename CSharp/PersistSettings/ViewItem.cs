/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ViewItem.cs]
 *  
 * This class represent a View that can be selected to change the look of the grid
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
{
  public class ViewItem : DependencyObject
  {
    public ViewItem()
    {
    }

    public ViewItem( string description, Type viewType )
      : this()
    {
      m_description = description;
      m_type = viewType;
    }

    private Type m_type;

    // Represents the Type to instantiate to have this view
    public Type Type
    {
      get
      {
        return m_type;
      }
      set
      {
        m_type = value;
      }
    }

    private string m_description;

    // Represents the description for the view
    public string Description
    {
      get
      {
        return m_description;
      }
      set
      {
        m_description = value;
      }
    }
  }
}
