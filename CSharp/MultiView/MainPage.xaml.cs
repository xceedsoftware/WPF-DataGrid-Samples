/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Multi-View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 * 
 * This class implements the various dynamic configuration options offered 
 * by the configuration panel declared in MainPage.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.MultiView
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void TableflowViewRadio_Checked( object sender, RoutedEventArgs e )
    {
      if( grid == null )
        return;

      Xceed.Wpf.DataGrid.Views.TableflowView tableflowView = new Xceed.Wpf.DataGrid.Views.TableflowView();
      tableflowView.ContainerHeight = 40d;

      grid.View = tableflowView;

      // When the sample is hosted in the Live Explorer, a default might be exposed.
      Theme defaultTheme = grid.TryFindResource( "defaultTheme" ) as Theme;

      if( defaultTheme != null )
        grid.View.Theme = defaultTheme;
    }

    private void TableViewRadio_Checked( object sender, RoutedEventArgs e )
    {
      if( grid == null )
        return;

      grid.View = new Xceed.Wpf.DataGrid.Views.TableView();

      // When the sample is hosted in the Live Explorer, a default might be exposed.
      Theme defaultTheme = grid.TryFindResource( "defaultTheme" ) as Theme;

      if( defaultTheme != null )
        grid.View.Theme = defaultTheme;
    }

    private void CardViewRadio_Checked( object sender, RoutedEventArgs e )
    {
      if( grid == null )
        return;

      grid.View = new Xceed.Wpf.DataGrid.Views.CardView();

      // When the sample is hosted in the Live Explorer, a default might be exposed.
      Theme defaultTheme = grid.TryFindResource( "defaultTheme" ) as Theme;

      if( defaultTheme != null )
        grid.View.Theme = defaultTheme;
    }

    private void CompactCardViewRadio_Checked( object sender, RoutedEventArgs e )
    {
      if( grid == null )
        return;

      grid.View = new Xceed.Wpf.DataGrid.Views.CompactCardView();

      // When the sample is hosted in the Live Explorer, a default might be exposed.
      Theme defaultTheme = grid.TryFindResource( "defaultTheme" ) as Theme;

      if( defaultTheme != null )
        grid.View.Theme = defaultTheme;
    }

    private void Cardflow3DView_Checked( object sender, RoutedEventArgs e )
    {
      if( grid == null )
        return;

      grid.View = new Xceed.Wpf.DataGrid.Views.CardflowView3D();
    }
  }
}
