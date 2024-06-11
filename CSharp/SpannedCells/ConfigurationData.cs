/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    public static readonly ConfigurationData Singleton = new ConfigurationData();

    private ConfigurationData()
    {
      m_spannedCellSelector = new CustomSpannedCellSelector();
      m_spannedCellConfigurationSelector = new CustomSpannedCellConfigurationSelector( this );
    }

    #region AllowCellSpanning Property

    public bool AllowCellSpanning
    {
      get
      {
        return m_allowCellSpanning;
      }
      set
      {
        if( value == m_allowCellSpanning )
          return;

        m_allowCellSpanning = value;

        this.RaisePropertyChanged( "AllowCellSpanning" );
      }
    }

    private bool m_allowCellSpanning = true;

    #endregion

    #region ConflictResolutionMode Property

    public SpannedCellConflictResolutionMode ConflictResolutionMode
    {
      get
      {
        return m_conflictResolutionMode;
      }
      set
      {
        if( value == m_conflictResolutionMode )
          return;

        m_conflictResolutionMode = value;

        this.RaisePropertyChanged( "ConflictResolutionMode" );
      }
    }

    private SpannedCellConflictResolutionMode m_conflictResolutionMode = SpannedCellConflictResolutionMode.ColumnFirst;

    #endregion

    #region UseCustomConfiguration Property

    public bool UseCustomConfiguration
    {
      get
      {
        return m_useCustomConfiguration;
      }
      set
      {
        if( value == m_useCustomConfiguration )
          return;

        m_useCustomConfiguration = value;

        this.RaisePropertyChanged( "UseCustomConfiguration" );
        this.RaisePropertyChanged( "SpannedCellSelector" );
        this.RaisePropertyChanged( "SpannedCellConfigurationSelector" );
      }
    }

    private bool m_useCustomConfiguration = true;

    #endregion

    #region HorizontalContentAlignment Property

    public HorizontalAlignment HorizontalContentAlignment
    {
      get
      {
        return m_horizontalContentAlignment;
      }
      set
      {
        if( value == m_horizontalContentAlignment )
          return;

        m_horizontalContentAlignment = value;

        this.RaisePropertyChanged( "HorizontalContentAlignment" );
      }
    }

    private HorizontalAlignment m_horizontalContentAlignment = HorizontalAlignment.Stretch;

    #endregion

    #region VerticalContentAlignment Property

    public VerticalAlignment VerticalContentAlignment
    {
      get
      {
        return m_verticalContentAlignment;
      }
      set
      {
        if( value == m_verticalContentAlignment )
          return;

        m_verticalContentAlignment = value;

        this.RaisePropertyChanged( "VerticalContentAlignment" );
      }
    }

    private VerticalAlignment m_verticalContentAlignment = VerticalAlignment.Stretch;

    #endregion

    #region AddBorders Property

    public bool AddBorders
    {
      get
      {
        return m_addBorders;
      }
      set
      {
        if( value == m_addBorders )
          return;

        m_addBorders = value;
        this.RaisePropertyChanged( "AddBorders" );
      }
    }

    private bool m_addBorders;

    #endregion

    #region EmployeeSpanningDirection Property

    public CellSpanningDirection EmployeeSpanningDirection
    {
      get
      {
        return m_employeeSpanningDirection;
      }
      set
      {
        if( value == m_employeeSpanningDirection )
          return;

        m_employeeSpanningDirection = value;

        this.RaisePropertyChanged( "EmployeeSpanningDirection" );
      }
    }

    private CellSpanningDirection m_employeeSpanningDirection = CellSpanningDirection.Row;

    #endregion

    #region CustomerSpanningDirection Property

    public CellSpanningDirection CustomerSpanningDirection
    {
      get
      {
        return m_customerSpanningDirection;
      }
      set
      {
        if( value == m_customerSpanningDirection )
          return;

        m_customerSpanningDirection = value;

        this.RaisePropertyChanged( "CustomerSpanningDirection" );
      }
    }

    private CellSpanningDirection m_customerSpanningDirection = CellSpanningDirection.Row;

    #endregion

    #region ShipViaSpanningDirection Property

    public CellSpanningDirection ShipViaSpanningDirection
    {
      get
      {
        return m_shipViaSpanningDirection;
      }
      set
      {
        if( value == m_shipViaSpanningDirection )
          return;

        m_shipViaSpanningDirection = value;

        this.RaisePropertyChanged( "ShipViaSpanningDirection" );
      }
    }

    private CellSpanningDirection m_shipViaSpanningDirection = CellSpanningDirection.Row;

    #endregion

    #region CitySpanningDirection Property

    public CellSpanningDirection CitySpanningDirection
    {
      get
      {
        return m_citySpanningDirection;
      }
      set
      {
        if( value == m_citySpanningDirection )
          return;

        m_citySpanningDirection = value;

        this.RaisePropertyChanged( "CitySpanningDirection" );
      }
    }

    private CellSpanningDirection m_citySpanningDirection = CellSpanningDirection.Both;

    #endregion

    #region CountrySpanningDirection Property

    public CellSpanningDirection CountrySpanningDirection
    {
      get
      {
        return m_countrySpanningDirection;
      }
      set
      {
        if( value == m_countrySpanningDirection )
          return;

        m_countrySpanningDirection = value;

        this.RaisePropertyChanged( "CountrySpanningDirection" );
      }
    }

    private CellSpanningDirection m_countrySpanningDirection = CellSpanningDirection.Both;

    #endregion

    #region SpannedCellSelector Property

    public SpannedCellSelector SpannedCellSelector
    {
      get
      {
        return ( this.UseCustomConfiguration ) ? m_spannedCellSelector : null;
      }
    }

    private readonly SpannedCellSelector m_spannedCellSelector;

    #endregion

    #region SpannedCellConfigurationSelector Property

    public SpannedCellConfigurationSelector SpannedCellConfigurationSelector
    {
      get
      {
        return ( this.UseCustomConfiguration ) ? m_spannedCellConfigurationSelector : null;
      }
    }

    private readonly SpannedCellConfigurationSelector m_spannedCellConfigurationSelector;

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged( string name )
    {
      var handler = this.PropertyChanged;
      if( handler == null )
        return;

      handler.Invoke( this, new PropertyChangedEventArgs( name ) );
    }

    #endregion
  }
}
