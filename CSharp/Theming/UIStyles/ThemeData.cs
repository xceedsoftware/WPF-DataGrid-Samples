/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ThemeData.cs]
 * 
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */
using System.Windows;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
{
  public class ThemeData : DependencyObject
  {
    #region CONSTRUCTORS

    public ThemeData()
    {
    }

    #endregion CONSTRUCTORS

    #region ImageDataTemplate Property

    public static readonly DependencyProperty ImageDataTemplateProperty = DependencyProperty.Register( "ImageDataTemplate", typeof( DataTemplate ), typeof( ThemeData ) );

    public DataTemplate ImageDataTemplate
    {
      get
      {
        return ( DataTemplate )this.GetValue( ThemeData.ImageDataTemplateProperty );
      }
      set
      {
        this.SetValue( ThemeData.ImageDataTemplateProperty, value );
      }
    }

    #endregion ImageDataTemplate Property

    #region Description Property

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register( "Description", typeof( string ), typeof( ThemeData ) );

    public string Description
    {
      get
      {
        return ( string )this.GetValue( ThemeData.DescriptionProperty );
      }
      set
      {
        this.SetValue( ThemeData.DescriptionProperty, value );
      }
    }

    #endregion Description Property

    #region Theme Property

    public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register( "Theme", typeof( Theme ), typeof( ThemeData ) );

    public Theme Theme
    {
      get
      {
        return ( Theme )this.GetValue( ThemeData.ThemeProperty );
      }
      set
      {
        this.SetValue( ThemeData.ThemeProperty, value );
      }
    }

    #endregion Theme Property

    #region ThemeGroup Property

    public static readonly DependencyProperty ThemeGroupProperty = DependencyProperty.Register( "ThemeGroup", typeof( string ), typeof( ThemeData ) );

    public string ThemeGroup
    {
      get
      {
        return ( string )this.GetValue( ThemeData.ThemeGroupProperty );
      }
      set
      {
        this.SetValue( ThemeData.ThemeGroupProperty, value );
      }
    }

    #endregion ThemeGroup Property

    #region IsTheme3D Property

    public static readonly DependencyProperty IsTheme3DProperty = DependencyProperty.Register( "IsTheme3D", typeof( bool ), typeof( ThemeData ) );

    public bool IsTheme3D
    {
      get
      {
        return ( bool )this.GetValue( ThemeData.IsTheme3DProperty );
      }
      set
      {
        this.SetValue( ThemeData.IsTheme3DProperty, value );
      }
    }

    #endregion IsTheme3D Property

    #region UseSystemTheme Property

    public static readonly DependencyProperty UseSystemThemeProperty = DependencyProperty.Register( "UseSystemTheme", typeof( bool ), typeof( ThemeData ) );

    public bool UseSystemTheme
    {
      get
      {
        return ( bool )this.GetValue( ThemeData.UseSystemThemeProperty );
      }
      set
      {
        this.SetValue( ThemeData.UseSystemThemeProperty, value );
      }
    }

    #endregion UseSystemTheme Property

    #region IsNewTheme Property

    public static readonly DependencyProperty IsNewThemeProperty = DependencyProperty.Register(
      "IsNewTheme",
      typeof( bool ),
      typeof( ThemeData ),
      new FrameworkPropertyMetadata( ( bool )false ) );

    public bool IsNewTheme
    {
      get
      {
        return ( bool )this.GetValue( ThemeData.IsNewThemeProperty );
      }
      set
      {
        this.SetValue( ThemeData.IsNewThemeProperty, value );
      }
    }

    #endregion

  }
}
