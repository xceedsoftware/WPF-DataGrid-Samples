/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Edit Modes Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [EditTriggersItem.cs]
 *  
 * This class represents a ComboBoxItem holding a EditTriggers object.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.EditModes
{
  public class EditTriggersItem
  {
    public EditTriggersItem()
    {
    }

    public EditTriggersItem(
      EditTriggers editTriggers,
      string description,
      bool isChecked )
    {
      m_editTriggers = editTriggers;
      m_description = description;
      m_isChecked = isChecked;
    }

    private EditTriggers m_editTriggers;

    public EditTriggers EditTriggers
    {
      get
      {
        return m_editTriggers;
      }
      set
      {
        m_editTriggers = value;
      }
    }

    private string m_description;

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

    private string m_toolTip;

    public string ToolTip
    {
      get
      {
        return m_toolTip;
      }
      set
      {
        m_toolTip = value;
      }
    }

    private bool m_isChecked;

    public bool IsChecked
    {
      get
      {
        return m_isChecked;
      }
      set
      {
        m_isChecked = value;
      }
    }
  }
}
