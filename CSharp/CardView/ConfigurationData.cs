/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.CardView
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    public static readonly ConfigurationData Singleton = new ConfigurationData();

    private ConfigurationData()
    {
    }

    #region AllowCardResize Property

    public bool AllowCardResize
    {
      get
      {
        return m_allowCardResize;
      }

      set
      {
        if( value != m_allowCardResize )
        {
          m_allowCardResize = value;
          this.RaisePropertyChanged( "AllowCardResize" );
        }
      }
    }

    private bool m_allowCardResize;

    #endregion AllowCardResize Property

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

    #region DistinctValuesConstraint Property

    public DistinctValuesConstraint DistinctValuesConstraint
    {
      get
      {
        return m_distinctValuesConstraint;
      }

      set
      {
        if( value != m_distinctValuesConstraint )
        {
          m_distinctValuesConstraint = value;
          this.RaisePropertyChanged( "DistinctValuesConstraint" );
          this.RaisePropertyChanged( "DistinctValuesConstraintAll" );
          this.RaisePropertyChanged( "DistinctValuesConstraintFiltered" );
        }
      }
    }

    public bool DistinctValuesConstraintAll
    {
      get
      {
        return ( this.DistinctValuesConstraint == DistinctValuesConstraint.All );
      }

      set
      {
        if( value )
        {
          this.DistinctValuesConstraint = DistinctValuesConstraint.All;
        }
      }
    }

    public bool DistinctValuesConstraintFiltered
    {
      get
      {
        return ( this.DistinctValuesConstraint == DistinctValuesConstraint.Filtered );
      }

      set
      {
        if( value )
        {
          this.DistinctValuesConstraint = DistinctValuesConstraint.Filtered;
        }
      }
    }

    private DistinctValuesConstraint m_distinctValuesConstraint = DistinctValuesConstraint.All;

    #endregion DistinctValuesConstraint Property

    #region HideEmptyCells Property

    public bool HideEmptyCells
    {
      get
      {
        return m_hideEmptyCells;
      }

      set
      {
        if( value != m_hideEmptyCells )
        {
          m_hideEmptyCells = value;
          this.RaisePropertyChanged( "HideEmptyCells" );
        }
      }
    }

    private bool m_hideEmptyCells = true;

    #endregion HideEmptyCells Property

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

    private FlowDirection m_flowDirection = FlowDirection.LeftToRight;

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

    #endregion FlowDirection Property

    #region ShowGridHeaders Property

    public bool ShowGridHeaders
    {
      get
      {
        return m_showGridHeaders;
      }

      set
      {
        if( value != m_showGridHeaders )
        {
          m_showGridHeaders = value;
          this.RaisePropertyChanged( "ShowGridHeaders" );
        }
      }
    }

    private bool m_showGridHeaders = true;

    #endregion ShowGridHeaders Property

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
