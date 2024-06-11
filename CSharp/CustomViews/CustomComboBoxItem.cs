/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CustomComboBoxItem.cs]
 *  
 * This file contains the classes used to populate various ComboBox of the
 * application. They are used in ItemsSources.xaml and in the MainPage source code.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.CustomViews
{
  /* 
   * This is the base class of all our ComboBoxItem classes.
   */
  public class CustomComboBoxItem
  {
    public CustomComboBoxItem()
    {
    }

    public CustomComboBoxItem( string description )
    {
      m_description = description;
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
  }

  /* 
   * This class represents a ComboBoxItem holding a Pen object.
   */
  public class PenItem : CustomComboBoxItem
  {
    private Pen m_pen;

    public Pen Pen
    {
      get
      {
        return m_pen;
      }
      set
      {
        m_pen = value;
      }
    }
  }

  /* 
   * This class represents a ComboBoxItem holding a Glyph definition via a DataTemplate.
   */
  public class GlyphItem : CustomComboBoxItem
  {
    private DataTemplate m_glyph;

    public DataTemplate Glyph
    {
      get
      {
        return m_glyph;
      }
      set
      {
        m_glyph = value;
      }
    }
  }

  /* 
   * This class represents a ComboBoxItem holding values used to define a vertical and
   * an horizontal GridLine.
   */
  public class GridLinesItem : CustomComboBoxItem
  {
    private Brush m_verticalBrush;

    public Brush VerticalBrush
    {
      get
      {
        return m_verticalBrush;
      }
      set
      {
        m_verticalBrush = value;
      }
    }

    private Brush m_horizontalBrush;

    public Brush HorizontalBrush
    {
      get
      {
        return m_horizontalBrush;
      }
      set
      {
        m_horizontalBrush = value;
      }
    }

    private double m_verticalThickness;

    public double VerticalThickness
    {
      get
      {
        return m_verticalThickness;
      }
      set
      {
        m_verticalThickness = value;
      }
    }

    private double m_horizontalThickness;

    public double HorizontalThickness
    {
      get
      {
        return m_horizontalThickness;
      }
      set
      {
        m_horizontalThickness = value;
      }
    }
  }
}
