/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
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

using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    public static readonly ConfigurationData Singleton = new ConfigurationData();

    private ConfigurationData()
    {
    }

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

    private bool m_allowColumnChooser;

    #endregion AllowColumnChooser Property

    #region AllowColumnResizing Property

    public bool AllowColumnResizing
    {
      get
      {
        return m_allowColumnResizing;
      }

      set
      {
        if( value != m_allowColumnResizing )
        {
          m_allowColumnResizing = value;
          this.RaisePropertyChanged( "AllowColumnResizing" );
        }
      }
    }

    private bool m_allowColumnResizing = true;

    #endregion AllowColumnResizing Property

    #region ColumnChooserSortOrder Property

    public ColumnChooserSortOrder ColumnChooserSortOrder
    {
      get
      {
        return m_columnChooserSortOrder;
      }

      set
      {
        if( value != m_columnChooserSortOrder )
        {
          m_columnChooserSortOrder = value;
          this.RaisePropertyChanged( "ColumnChooserSortOrder" );
        }
      }
    }

    private ColumnChooserSortOrder m_columnChooserSortOrder = ColumnChooserSortOrder.VisiblePosition;

    #endregion ColumnChooserSortOrder Property

    #region ColumnStretchMinWidth Property

    public double ColumnStretchMinWidth
    {
      get
      {
        return m_columnStretchMinWidth;
      }

      set
      {
        if( value != m_columnStretchMinWidth )
        {
          m_columnStretchMinWidth = value;
          this.RaisePropertyChanged( "ColumnStretchMinWidth" );
        }
      }
    }

    private double m_columnStretchMinWidth = 50d;

    #endregion ColumnStretchMinWidth Property

    #region ColumnStretchMode Property

    public ColumnStretchMode ColumnStretchMode
    {
      get
      {
        return m_columnStretchMode;
      }

      set
      {
        if( value != m_columnStretchMode )
        {
          m_columnStretchMode = value;
          this.RaisePropertyChanged( "ColumnStretchMode" );
        }
      }
    }

    private ColumnStretchMode m_columnStretchMode = ColumnStretchMode.None;

    #endregion ColumnStretchMode Property

    #region DataGrid Property

    public DataGridControl DataGrid
    {
      get
      {
        return m_dataGrid;
      }

      set
      {
        if( value != m_dataGrid )
        {
          m_dataGrid = value;
          this.RaisePropertyChanged( "DataGrid" );
        }
      }
    }

    private DataGridControl m_dataGrid;

    #endregion DataGrid Property

    #region IsAllowColumnResizingEnabled Property

    public bool IsAllowColumnResizingEnabled
    {
      get
      {
        return m_isAllowColumnResizingEnabled;
      }

      set
      {
        if( value != m_isAllowColumnResizingEnabled )
        {
          m_isAllowColumnResizingEnabled = value;
          this.RaisePropertyChanged( "IsAllowColumnResizingEnabled" );
        }
      }
    }

    private bool m_isAllowColumnResizingEnabled = true;

    #endregion IsAllowColumnResizingEnabled Property

    #region IsColumnStretchMinWidthEnabled Property

    public bool IsColumnStretchMinWidthEnabled
    {
      get
      {
        return m_isColumnStretchMinWidthEnabled;
      }

      set
      {
        if( value != m_isColumnStretchMinWidthEnabled )
        {
          m_isColumnStretchMinWidthEnabled = value;
          this.RaisePropertyChanged( "IsColumnStretchMinWidthEnabled" );
        }
      }
    }

    private bool m_isColumnStretchMinWidthEnabled = true;

    #endregion IsColumnStretchMinWidthEnabled Property

    #region UseAdvancedColumnManagement Property

    public bool UseAdvancedColumnManagement
    {
      get
      {
        return m_useAdvancedColumnManagement;
      }

      set
      {
        if( value != m_useAdvancedColumnManagement )
        {
          m_useAdvancedColumnManagement = value;
          this.RaisePropertyChanged( "UseAdvancedColumnManagement" );
        }
      }
    }

    private bool m_useAdvancedColumnManagement;

    #endregion UseAdvancedColumnManagement Property

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
