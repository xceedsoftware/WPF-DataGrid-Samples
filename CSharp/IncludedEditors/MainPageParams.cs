/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPageParams.cs]
 *  
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
{
  public class MainPageParams : INotifyPropertyChanged
  {
    public static readonly MainPageParams Singleton = new MainPageParams();

    #region CONSTRUCTORS

    private MainPageParams()
    {
    }

    #endregion CONSTRUCTORS

    #region FreeTextColumnsVisible

    private bool m_freeTextColumnsVisible = true;

    public bool FreeTextColumnsVisible
    {
      get
      {
        return m_freeTextColumnsVisible;
      }

      set
      {
        if( m_freeTextColumnsVisible == value )
          return;

        m_freeTextColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "FreeTextColumnsVisible" ) );
      }
    }

    #endregion FreeTextColumnsVisible

    #region MaskedTextColumnsVisible

    private bool m_maskedTextColumnsVisible = true;

    public bool MaskedTextColumnsVisible
    {
      get
      {
        return m_maskedTextColumnsVisible;
      }

      set
      {
        if( m_maskedTextColumnsVisible == value )
          return;

        m_maskedTextColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "MaskedTextColumnsVisible" ) );
      }
    }

    #endregion MaskedTextColumnsVisible

    #region DateTimeColumnsVisible

    private bool m_dateTimeColumnsVisible = true;

    public bool DateTimeColumnsVisible
    {
      get
      {
        return m_dateTimeColumnsVisible;
      }

      set
      {
        if( m_dateTimeColumnsVisible == value )
          return;

        m_dateTimeColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "DateTimeColumnsVisible" ) );
      }
    }

    #endregion DateTimeColumnsVisible

    #region NumericColumnsVisible

    private bool m_numericColumnsVisible = true;

    public bool NumericColumnsVisible
    {
      get
      {
        return m_numericColumnsVisible;
      }

      set
      {
        if( m_numericColumnsVisible == value )
          return;

        m_numericColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "NumericColumnsVisible" ) );
      }
    }

    #endregion NumericColumnsVisible

    #region ComboBoxColumnsVisible

    private bool m_comboBoxColumnsVisible = true;

    public bool ComboBoxColumnsVisible
    {
      get
      {
        return m_comboBoxColumnsVisible;
      }

      set
      {
        if( m_comboBoxColumnsVisible == value )
          return;

        m_comboBoxColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "ComboBoxColumnsVisible" ) );
      }
    }

    #endregion ComboBoxColumnsVisible

    #region CheckBoxColumnsVisible

    private bool m_checkBoxColumnsVisible = true;

    public bool CheckBoxColumnsVisible
    {
      get
      {
        return m_checkBoxColumnsVisible;
      }

      set
      {
        if( m_checkBoxColumnsVisible == value )
          return;

        m_checkBoxColumnsVisible = value;

        this.OnPropertyChanged( new PropertyChangedEventArgs( "CheckBoxColumnsVisible" ) );
      }
    }

    #endregion CheckBoxColumnsVisible


    #region INotifyPropertyChanged Members

    protected void OnPropertyChanged( PropertyChangedEventArgs e )
    {
      if( this.PropertyChanged != null )
        this.PropertyChanged( this, e );
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
