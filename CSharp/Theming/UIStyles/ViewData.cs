/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ViewData.cs]
 * 
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */
using System;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
{
  public class ViewData : DependencyObject
  {
    #region CONSTRUCTORS

    public ViewData()
    {
    }

    #endregion CONSTRUCTORS

    #region ImageDataTemplate Property

    public static readonly DependencyProperty ImageDataTemplateProperty = DependencyProperty.Register( "ImageDataTemplate", typeof( DataTemplate ), typeof( ViewData ) );

    public DataTemplate ImageDataTemplate
    {
      get
      {
        return ( DataTemplate )this.GetValue( ViewData.ImageDataTemplateProperty );
      }
      set
      {
        this.SetValue( ViewData.ImageDataTemplateProperty, value );
      }
    }

    #endregion ImageDataTemplate Property

    #region Description Property

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register( "Description", typeof( string ), typeof( ViewData ) );

    public string Description
    {
      get
      {
        return ( string )this.GetValue( ViewData.DescriptionProperty );
      }
      set
      {
        this.SetValue( ViewData.DescriptionProperty, value );
      }
    }

    #endregion Description Property

    #region IsView3D Property

    public static readonly DependencyProperty IsView3DProperty = DependencyProperty.Register( "IsView3D", typeof( bool ), typeof( ViewData ) );

    public bool IsView3D
    {
      get
      {
        return ( bool )this.GetValue( ViewData.IsView3DProperty );
      }
      set
      {
        this.SetValue( ViewData.IsView3DProperty, value );
      }
    }

    #endregion IsView3D Property

    #region ViewType Property

    public static readonly DependencyProperty ViewTypeProperty = DependencyProperty.Register( "ViewType", typeof( Type ), typeof( ViewData ) );

    public Type ViewType
    {
      get
      {
        return ( Type )this.GetValue( ViewData.ViewTypeProperty );
      }
      set
      {
        this.SetValue( ViewData.ViewTypeProperty, value );
      }
    }

    #endregion View Property
  }
}
