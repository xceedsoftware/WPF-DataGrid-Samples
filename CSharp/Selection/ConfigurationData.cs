/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Selection™ View Sample Application
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Selection
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

    #region NavigationBehavior Property

    public NavigationBehavior NavigationBehavior
    {
      get
      {
        return m_navigationBehavior;
      }

      set
      {
        if( value != m_navigationBehavior )
        {
          m_navigationBehavior = value;
          this.RaisePropertyChanged( "NavigationBehavior" );
          this.RaisePropertyChanged( "NavigationBehaviorCellOnly" );
          this.RaisePropertyChanged( "NavigationBehaviorRowOnly" );
          this.RaisePropertyChanged( "NavigationBehaviorRowOrCell" );
          this.RaisePropertyChanged( "NavigationBehaviorNone" );
        }
      }
    }

    public bool NavigationBehaviorCellOnly
    {
      get
      {
        return ( this.NavigationBehavior == NavigationBehavior.CellOnly );
      }

      set
      {
        if( value )
        {
          this.NavigationBehavior = NavigationBehavior.CellOnly;
        }
      }
    }

    public bool NavigationBehaviorRowOnly
    {
      get
      {
        return ( this.NavigationBehavior == NavigationBehavior.RowOnly );
      }

      set
      {
        if( value )
        {
          this.NavigationBehavior = NavigationBehavior.RowOnly;
        }
      }
    }

    public bool NavigationBehaviorRowOrCell
    {
      get
      {
        return ( this.NavigationBehavior == NavigationBehavior.RowOrCell );
      }

      set
      {
        if( value )
        {
          this.NavigationBehavior = NavigationBehavior.RowOrCell;
        }
      }
    }

    public bool NavigationBehaviorNone
    {
      get
      {
        return ( this.NavigationBehavior == NavigationBehavior.None );
      }

      set
      {
        if( value )
        {
          this.NavigationBehavior = NavigationBehavior.None;
        }
      }
    }

    private NavigationBehavior m_navigationBehavior = NavigationBehavior.CellOnly;

    #endregion NavigationBehavior Property

    #region SelectionMode Property

    private static Dictionary<System.Windows.Controls.SelectionMode, string> s_selectionModeList = null;

    public Dictionary<System.Windows.Controls.SelectionMode, string> SelectionModeList
    {
      get
      {
        if( s_selectionModeList == null )
        {
          s_selectionModeList = new Dictionary<System.Windows.Controls.SelectionMode, string>();
          s_selectionModeList.Add( SelectionMode.Single, "Single" );
          s_selectionModeList.Add( SelectionMode.Multiple, "Multiple" );
          s_selectionModeList.Add( SelectionMode.Extended, "Extended" );
        }
        return s_selectionModeList;
      }
    }

    private SelectionMode m_selectionMode = SelectionMode.Extended;

    public SelectionMode SelectionMode
    {
      get
      {
        return m_selectionMode;
      }

      set
      {
        if( value != m_selectionMode )
        {
          m_selectionMode = value;
          this.RaisePropertyChanged( "SelectionMode" );
        }
      }
    }

    #endregion SelectionMode Property

    #region Selection Unit Property

    private static Dictionary<SelectionUnit, string> s_selectionUnitList = null;

    public Dictionary<SelectionUnit, string> SelectionUnitList
    {
      get
      {
        if( s_selectionUnitList == null )
        {
          s_selectionUnitList = new Dictionary<SelectionUnit, string>();
          s_selectionUnitList.Add( SelectionUnit.Cell, "Cell" );
          s_selectionUnitList.Add( SelectionUnit.Row, "Row" );
        }
        return s_selectionUnitList;
      }
    }

    private SelectionUnit m_selectionUnit = SelectionUnit.Row;

    public SelectionUnit SelectionUnit
    {
      get
      {
        return m_selectionUnit;
      }

      set
      {
        if( value != m_selectionUnit )
        {
          m_selectionUnit = value;
          this.RaisePropertyChanged( "SelectionUnit" );
        }
      }
    }

    #endregion

    #region IsGroupSelectionEnabled Property

    private bool m_isGroupSelectionEnabled = false;

    public bool IsGroupSelectionEnabled
    {
      get
      {
        return m_isGroupSelectionEnabled;
      }

      set
      {
        if( value != m_isGroupSelectionEnabled )
        {
          m_isGroupSelectionEnabled = value;
          this.RaisePropertyChanged( "IsGroupSelectionEnabled" );
        }
      }
    }

    #endregion

    #region EnableIsGroupSelectionEnabled

    private bool m_enableIsGroupSelectionEnabled = false;

    public bool EnableIsGroupSelectionEnabled
    {
      get
      {
        return m_enableIsGroupSelectionEnabled;
      }

      set
      {
        if( value != m_enableIsGroupSelectionEnabled )
        {
          m_enableIsGroupSelectionEnabled = value;
          this.RaisePropertyChanged( "EnableIsGroupSelectionEnabled" );
        }
      }
    }

    #endregion

    #region EnableDragSelection Property

    private bool m_enableDragSelection = false;

    public bool EnableDragSelection
    {
      get
      {
        return m_enableDragSelection;
      }

      set
      {
        if( value != m_enableDragSelection )
        {
          m_enableDragSelection = value;
          this.RaisePropertyChanged( "EnableDragSelection" );
        }
      }
    }

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

    #endregion INotifyPropertyChanged Implementation
  }
}
