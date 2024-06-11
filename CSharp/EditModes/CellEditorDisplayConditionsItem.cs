/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Edit Modes Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CellEditorDisplayConditionsItem.cs]
 *  
 * This class represents a ComboBoxItem holding a CellEditorDisplayConditions object.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.EditModes
{
  public class CellEditorDisplayConditionsItem
  {
    public CellEditorDisplayConditionsItem()
    {
    }

    public CellEditorDisplayConditionsItem(
      CellEditorDisplayConditions cellEditorDisplayConditions,
      string description,
      bool isChecked )
    {
      m_cellEditorDisplayConditions = cellEditorDisplayConditions;
      m_description = description;
      m_isChecked = isChecked;
    }

    private CellEditorDisplayConditions m_cellEditorDisplayConditions;

    public CellEditorDisplayConditions CellEditorDisplayConditions
    {
      get
      {
        return m_cellEditorDisplayConditions;
      }
      set
      {
        m_cellEditorDisplayConditions = value;
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
