/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Merged Headers Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.MergedHeaders
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    #region Singleton Property

    public static readonly ConfigurationData Singleton = new ConfigurationData();

    #endregion Singleton Property

    #region CONSTRUCTORS

    private ConfigurationData()
    {
    }

    #endregion CONSTRUCTORS

    #region AllowColumnChooser Property

    public bool AllowColumnChooser
    {
      get
      {
        return m_allowColumnChooser;
      }

      set
      {
        if( value != m_allowColumnChooser )
        {
          m_allowColumnChooser = value;
          this.RaisePropertyChanged( "AllowColumnChooser" );
        }
      }
    }

    private bool m_allowColumnChooser = true;

    #endregion AllowColumnChooser Property

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

    #endregion ShowFixedColumnSplitter Property

    #region AllowColumnReorder Property

    public bool AllowColumnReorder
    {
      get
      {
        return m_allowColumnReorder;
      }
      set
      {
        if( m_allowColumnReorder != value )
        {
          m_allowColumnReorder = value;
          this.RaisePropertyChanged( "AllowColumnReorder" );
        }
      }
    }

    private bool m_allowColumnReorder; //false

    #endregion AllowColumnsReordering Property

    #region AllowColumnResize Property

    public bool AllowColumnResize
    {
      get
      {
        return m_allowColumnResize;
      }
      set
      {
        if( m_allowColumnResize != value )
        {
          m_allowColumnResize = value;
          this.RaisePropertyChanged( "AllowColumnResize" );
        }
      }
    }

    private bool m_allowColumnResize = true;

    #endregion AllowColumnResize Property

    #region AllowMergedHeadersReorder Property

    public bool AllowMergedHeadersReorder
    {
      get
      {
        return m_allowMergedHeadersReorder;
      }
      set
      {
        if( m_allowMergedHeadersReorder != value )
        {
          m_allowMergedHeadersReorder = value;
          this.RaisePropertyChanged( "AllowMergedHeadersReorder" );
        }
      }
    }

    private bool m_allowMergedHeadersReorder = true;

    #endregion AllowMergedHeadersReorder Property

    #region AllowMergedHeadersResize Property

    public bool AllowMergedHeadersResize
    {
      get
      {
        return m_allowMergedHeadersResize;
      }
      set
      {
        if( m_allowMergedHeadersResize != value )
        {
          m_allowMergedHeadersResize = value;
          this.RaisePropertyChanged( "AllowMergedHeadersResize" );
        }
      }
    }

    private bool m_allowMergedHeadersResize = true;

    #endregion AllowMergedHeadersResize Property

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged( string name )
    {
      if( this.PropertyChanged != null )
      {
        this.PropertyChanged( this, new PropertyChangedEventArgs( name ) );
      }
    }

    #endregion INotifyPropertyChanged Implementation
  }
}
