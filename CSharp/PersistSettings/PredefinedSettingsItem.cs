/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PredefinedSettingsItem.cs]
 *  
 * Structure used for the predefined combo box binding.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
{
  public class PredefinedSettingsItem : INotifyPropertyChanged
  {
    #region DisplayName Property

    public string DisplayName
    {
      get
      {
        return m_displayName;
      }
      set
      {
        if( value == m_displayName )
          return;

        m_displayName = value;
        this.OnPropertyChanged( new PropertyChangedEventArgs( "DisplayName" ) );
      }
    }

    #endregion DisplayName Property

    #region XmlUri Property

    public string XmlUri
    {
      get
      {
        return m_xmlUri;
      }
      set
      {
        if( value == m_xmlUri )
          return;

        m_xmlUri = value;
        this.OnPropertyChanged( new PropertyChangedEventArgs( "XmlUri" ) );
      }
    }

    #endregion XmlUri Property

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged( PropertyChangedEventArgs e )
    {
      if( this.PropertyChanged != null )
        this.PropertyChanged( this, e );
    }

    #endregion INotifyPropertyChanged Members

    #region PRIVATE FIELDS

    private string m_displayName;
    private string m_xmlUri;

    #endregion PRIVATE FIELDS
  }
}
