/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.Tableflow
{
  public class MainPageParams : INotifyPropertyChanged
  {
    public static readonly MainPageParams Singleton = new MainPageParams();

    #region CONSTRUCTORS

    private MainPageParams()
    {
    }

    #endregion CONSTRUCTORS

    #region AllowDetailToggle Property

    public bool AllowDetailToggle
    {
      get
      {
        return _allowDetailToggle;
      }
      set
      {
        if( _allowDetailToggle == value )
          return;

        _allowDetailToggle = value;
        this.OnPropertyChanged( new PropertyChangedEventArgs( "AllowDetailToggle" ) );
      }
    }

    private bool _allowDetailToggle = true;

    #endregion AllowDetailToggle Property

    #region UseCustomDetailHeader Property

    private bool _useCustomDetailHeader;

    public bool UseCustomDetailHeader
    {
      get
      {
        return _useCustomDetailHeader;
      }
      set
      {
        if( _useCustomDetailHeader == value )
          return;

        _useCustomDetailHeader = value;
        this.OnPropertyChanged( new PropertyChangedEventArgs( "UseCustomDetailHeader" ) );
      }
    }

    #endregion UseCustomDetailHeader Property

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
