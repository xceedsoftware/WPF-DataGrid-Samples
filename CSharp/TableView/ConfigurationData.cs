/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ConfigurationData.cs]
 *  
 * This class implements the business object containing various dynamic configuration 
 * options offered by the configuration panel.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.ComponentModel;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    public static readonly ConfigurationData Singleton = new ConfigurationData();

    private ConfigurationData()
    {

    }

    #region FilteringMethod Property

    public FilteringMethod FilteringMethod
    {
      get
      {
        return m_filteringMethod;
      }

      set
      {
        if( value != m_filteringMethod )
        {
          m_filteringMethod = value;
          this.RaisePropertyChanged( "FilteringMethod" );
          this.RaisePropertyChanged( "FilteringMethodAuto" );
          this.RaisePropertyChanged( "FilteringMethodFilterRow" );
          this.RaisePropertyChanged( "FilteringMethodNone" );
        }
      }
    }

    public bool FilteringMethodAuto
    {
      get
      {
        return ( this.FilteringMethod == FilteringMethod.AutoFilter );
      }

      set
      {
        if( value )
        {
          this.FilteringMethod = FilteringMethod.AutoFilter;
        }
      }
    }

    public bool FilteringMethodFilterRow
    {
      get
      {
        return ( this.FilteringMethod == FilteringMethod.FilterRow );
      }

      set
      {
        if( value )
        {
          this.FilteringMethod = FilteringMethod.FilterRow;
          this.ShowInsertionRow = false;
        }
      }
    }

    public bool FilteringMethodNone
    {
      get
      {
        return ( this.FilteringMethod == FilteringMethod.None );
      }

      set
      {
        if( value )
        {
          this.FilteringMethod = FilteringMethod.None;
        }
      }
    }

    private FilteringMethod m_filteringMethod = FilteringMethod.AutoFilter;

    #endregion FilteringMethod Property

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

    #region ItemScrollingBehavior Property

    public ItemScrollingBehavior ItemScrollingBehavior
    {
      get
      {
        return m_itemScrollingBehavior;
      }

      set
      {
        if( value != m_itemScrollingBehavior )
        {
          m_itemScrollingBehavior = value;
          this.RaisePropertyChanged( "ItemScrollingBehavior" );
          this.RaisePropertyChanged( "DeferredScrollingEnabled" );
        }
      }
    }

    public bool DeferredScrollingEnabled
    {
      get
      {
        return ( this.ItemScrollingBehavior == ItemScrollingBehavior.Deferred );
      }

      set
      {
        if( value )
        {
          this.ItemScrollingBehavior = ItemScrollingBehavior.Deferred;
        }
        else
        {
          this.ItemScrollingBehavior = ItemScrollingBehavior.Immediate;
        }
      }
    }

    private ItemScrollingBehavior m_itemScrollingBehavior = ItemScrollingBehavior.Immediate;

    #endregion ItemScrollingBehavior Property

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

    #endregion FlowDirection Property

    #region IsAlternatingRowStyleEnabled Property

    public bool IsAlternatingRowStyleEnabled
    {
      get
      {
        return m_isAlternatingRowStyleEnabled;
      }

      set
      {
        if( value != m_isAlternatingRowStyleEnabled )
        {
          m_isAlternatingRowStyleEnabled = value;
          this.RaisePropertyChanged( "IsAlternatingRowStyleEnabled" );
        }
      }
    }

    private bool m_isAlternatingRowStyleEnabled = true;

    #endregion IsAlternatingRowStyleEnabled Property

    #region ShowHeadersFootersEditor Property

    public bool ShowHeadersFootersEditor
    {
      get
      {
        return m_showHeadersFootersEditor;
      }

      set
      {
        if( value != m_showHeadersFootersEditor )
        {
          m_showHeadersFootersEditor = value;
          this.RaisePropertyChanged( "ShowHeadersFootersEditor" );
        }
      }
    }

    private bool m_showHeadersFootersEditor = false;

    #endregion

    #region ShowSearchControl Property

    public bool ShowSearchControl
    {
      get
      {
        return m_showSearchControl;
      }

      set
      {
        if( value != m_showSearchControl )
        {
          m_showSearchControl = value;
          this.RaisePropertyChanged( "ShowSearchControl" );
        }
      }
    }

    private bool m_showSearchControl = false;

    #endregion

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

    #endregion ShowColumnManagerRow Property

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

    #region ShowGroupByControl Property

    public bool ShowGroupByControl
    {
      get
      {
        return m_showGroupByControl;
      }

      set
      {
        if( value != m_showGroupByControl )
        {
          m_showGroupByControl = value;
          this.RaisePropertyChanged( "ShowGroupByControl" );
        }
      }
    }

    private bool m_showGroupByControl = true;

    #endregion ShowGroupByControl Property

    #region ShowInsertionRow Property

    public bool ShowInsertionRow
    {
      get
      {
        return m_showInsertionRow;
      }

      set
      {
        if( value != m_showInsertionRow )
        {
          m_showInsertionRow = value;
          this.RaisePropertyChanged( "ShowInsertionRow" );
        }
      }
    }

    private bool m_showInsertionRow = true;

    #endregion ShowInsertionRow Property

    #region ShowSortIndex Property

    public bool ShowSortIndex
    {
      get
      {
        return m_showSortIndex;
      }

      set
      {
        if( value != m_showSortIndex )
        {
          m_showSortIndex = value;
          this.RaisePropertyChanged( "ShowSortIndex" );
        }
      }
    }

    private bool m_showSortIndex;

    #endregion ShowSortIndex Property

    #region InsertionMode Property

    public InsertionMode InsertionMode
    {
      get
      {
        return m_insertionMode;
      }
      set
      {
        if( value != m_insertionMode )
        {
          m_insertionMode = value;
          this.RaisePropertyChanged( "InsertionMode" );
        }
      }
    }

    private InsertionMode m_insertionMode = InsertionMode.Default;

    #endregion InsertionMode Property

    #region ShowScrollTip Property

    public bool ShowScrollTip
    {
      get
      {
        return m_showScrollTip;
      }

      set
      {
        if( value != m_showScrollTip )
        {
          m_showScrollTip = value;
          this.RaisePropertyChanged( "ShowScrollTip" );
        }
      }
    }

    private bool m_showScrollTip;

    #endregion ShowScrollTip Property

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
