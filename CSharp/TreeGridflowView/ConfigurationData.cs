/*
 * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ConfigurationData.cs]
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.ComponentModel;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    #region Singleton Property

    public static readonly ConfigurationData Singleton = new ConfigurationData();

    #endregion

    #region CONSTRUCTORS

    private ConfigurationData()
    {
    }

    #endregion

    #region FlowDirection Property

    public FlowDirection FlowDirection
    {
      get
      {
        return m_flowDirection;
      }

      set
      {
        if( value != m_flowDirection )
        {
          m_flowDirection = value;
          this.RaisePropertyChanged( "FlowDirection" );
          this.RaisePropertyChanged( "FlowDirectionLeftToRight" );
          this.RaisePropertyChanged( "FlowDirectionRightToLeft" );
        }
      }
    }

    public bool FlowDirectionLeftToRight
    {
      get
      {
        return ( this.FlowDirection == FlowDirection.LeftToRight );
      }

      set
      {
        if( value )
        {
          this.FlowDirection = FlowDirection.LeftToRight;
        }
      }
    }

    public bool FlowDirectionRightToLeft
    {
      get
      {
        return ( this.FlowDirection == FlowDirection.RightToLeft );
      }

      set
      {
        if( value )
        {
          this.FlowDirection = FlowDirection.RightToLeft;
        }
      }
    }

    private FlowDirection m_flowDirection = FlowDirection.LeftToRight;

    #endregion

    #region AllowFixedColumnSplitterDrag Property

    public bool AllowFixedColumnSplitterDrag
    {
      get
      {
        return m_allowFixedColumnSplitterDrag;
      }

      set
      {
        if( value != m_allowFixedColumnSplitterDrag )
        {
          m_allowFixedColumnSplitterDrag = value;
          this.RaisePropertyChanged( "AllowFixedColumnSplitterDrag" );
        }
      }
    }

    private bool m_allowFixedColumnSplitterDrag = true;

    #endregion AllowFixedColumnSplitterDrag Property

    #region ShowColumnManagerRow Property

    public bool ShowColumnManagerRow
    {
      get
      {
        return m_showColumnManagerRow;
      }

      set
      {
        if( value != m_showColumnManagerRow )
        {
          m_showColumnManagerRow = value;
          this.RaisePropertyChanged( "ShowColumnManagerRow" );
        }
      }
    }

    private bool m_showColumnManagerRow = true;

    #endregion

    #region ShowFixedColumnSplitter Property

    public bool ShowFixedColumnSplitter
    {
      get
      {
        return m_showFixedColumnSplitter;
      }

      set
      {
        if( value != m_showFixedColumnSplitter )
        {
          m_showFixedColumnSplitter = value;
          this.RaisePropertyChanged( "ShowFixedColumnSplitter" );
        }
      }
    }

    private bool m_showFixedColumnSplitter = true;

    #endregion

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged( string name )
    {
      if( this.PropertyChanged != null )
      {
        this.PropertyChanged( this, new PropertyChangedEventArgs( name ) );
      }
    }

    #endregion
  }
}
