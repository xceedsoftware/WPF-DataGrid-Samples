/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [AccentData.cs]
 * 
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
{
  public class AccentData : DependencyObject
  {

    #region CONSTRUCTORS

    public AccentData()
    {
    }

    #endregion CONSTRUCTORS

    #region ImageDataTemplate Property

    public static readonly DependencyProperty ImageDataTemplateProperty = DependencyProperty.Register( "ImageDataTemplate", typeof( DataTemplate ), typeof( AccentData ) );

    public DataTemplate ImageDataTemplate
    {
      get
      {
        return ( DataTemplate )this.GetValue( AccentData.ImageDataTemplateProperty );
      }
      set
      {
        this.SetValue( AccentData.ImageDataTemplateProperty, value );
      }
    }

    #endregion ImageDataTemplate Property

    #region Description Property

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register( "Description", typeof( string ), typeof( AccentData ) );

    public string Description
    {
      get
      {
        return ( string )this.GetValue( AccentData.DescriptionProperty );
      }
      set
      {
        this.SetValue( AccentData.DescriptionProperty, value );
      }
    }

    #endregion Description Property

    #region Accent Property

    public static readonly DependencyProperty AccentProperty = DependencyProperty.Register( "Accent", typeof( string ), typeof( AccentData ) );

    public string Accent
    {
      get
      {
        return ( string )this.GetValue( AccentData.AccentProperty );
      }
      set
      {
        this.SetValue( AccentData.AccentProperty, value );
      }
    }

    #endregion Accent Property

    #region IsNewTheme Property

    public static readonly DependencyProperty IsNewThemeProperty = DependencyProperty.Register(
      "IsNewTheme",
      typeof( bool ),
      typeof( AccentData ),
      new FrameworkPropertyMetadata( ( bool )false ) );

    public bool IsNewTheme
    {
      get
      {
        return ( bool )this.GetValue( AccentData.IsNewThemeProperty );
      }
      set
      {
        this.SetValue( AccentData.IsNewThemeProperty, value );
      }
    }

    #endregion
  }
}
