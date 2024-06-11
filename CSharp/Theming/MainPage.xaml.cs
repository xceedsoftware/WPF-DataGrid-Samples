/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
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

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Samples.Theming.UIStyles;
using Xceed.Wpf.DataGrid.ThemePack;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.Theming
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
      this.Unloaded += new RoutedEventHandler( this.MainPage_Unloaded );

      m_orderIDColumnWidth = grid.Columns[ "OrderID" ].Width;
    }

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      ( ( App )Application.Current ).ViewChanged += this.MainPage_ViewChanged;

      this.LoadViewsCombo( grid );
      this.LoadThemesCombo( grid );
      this.UpdateGridTheme();
    }

    private void MainPage_Unloaded( object sender, RoutedEventArgs e )
    {
      ( ( App )Application.Current ).ViewChanged -= this.MainPage_ViewChanged;
    }

    private void MainPage_ViewChanged( object sender, EventArgs e )
    {
      grid.DetailConfigurations.Clear();

      if( grid.View is Xceed.Wpf.DataGrid.Views.TreeGridflowView )
      {
        //bigger column to see the details
        grid.Columns[ "OrderID" ].Width = 120d;

        DetailConfiguration detailConfig = this.Resources[ "CustomersDetailsConfiguration" ] as DetailConfiguration;
        if( detailConfig != null )
        {
          grid.DetailConfigurations.Add( detailConfig );
        }
      }
      else
      {
        grid.Columns[ "OrderID" ].Width = m_orderIDColumnWidth;
      }
    }

    private void OnViewsComboSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( e.AddedItems.Count == 0 )
        return;

      var viewData = e.AddedItems[ 0 ] as ViewData;
      if( viewData == null )
        return;

      this.FilterThemes( viewData );

      this.UpdateGridTheme();
      ( ( App )Application.Current ).OnViewChanged( EventArgs.Empty );

      e.Handled = true;
    }

    private void OnThemesComboSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      this.LoadAccentsCombo( grid );
      this.UpdateGridTheme();

      e.Handled = true;
    }

    private void OnAccentComboSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      this.UpdateGridTheme();

      e.Handled = true;
    }

    private void LoadViewsCombo( DataGridControl dataGrid )
    {
      var viewsDataSource = this.FindResource( "viewsDataSource" ) as DataGridCollectionViewSource;
      if( viewsDataSource == null )
        return;

      viewsDataSource.View.Refresh();

      // Set the current view of the grid
      if( ( dataGrid != null ) && ( dataGrid.View != null ) )
      {
        var viewType = dataGrid.View.GetType();

        foreach( ViewData viewData in viewsDataSource.View )
        {
          if( viewData.ViewType == viewType )
          {
            viewsDataSource.View.MoveCurrentTo( viewData );
            break;
          }
        }
      }

      if( viewsCombo != null )
      {
        viewsCombo.SelectionChanged -= this.OnViewsComboSelectionChanged;
        viewsCombo.ItemsSource = viewsDataSource.View;
        viewsCombo.SelectionChanged += this.OnViewsComboSelectionChanged;
      }
    }

    private void LoadThemesCombo( DataGridControl dataGrid )
    {
      var themesDataSource = this.FindResource( "themesDataSource" ) as DataGridCollectionViewSource;
      if( themesDataSource == null )
        return;

      var viewsDataSource = this.FindResource( "viewsDataSource" ) as DataGridCollectionViewSource;

      // Apply filtering before binding to the combo.
      if( ( viewsDataSource != null )
        && ( viewsDataSource.View != null )
        && ( viewsDataSource.View.CurrentItem != null ) )
      {
        var currentView = ( ViewData )viewsDataSource.View.CurrentItem;

        themesDataSource.View.Filter = new Predicate<object>( delegate ( object s )
        {
          return this.AcceptTheme( currentView, s as ThemeData );
        } );
      }

      // Set the current theme of the grid
      if( ( dataGrid != null )
        && ( dataGrid.View != null )
        && ( dataGrid.View.Theme != null ) )
      {
        var themeType = dataGrid.View.Theme.GetType();

        foreach( ThemeData themeData in themesDataSource.View )
        {
          if( themeData.Theme == null )
            continue;

          if( themeData.Theme.GetType() == themeType )
          {
            themesDataSource.View.MoveCurrentTo( themeData );
            break;
          }
        }
      }

      if( themesCombo != null )
      {
        themesCombo.SelectionChanged -= this.OnThemesComboSelectionChanged;
        themesCombo.ItemsSource = themesDataSource.View;
        themesCombo.SelectionChanged += this.OnThemesComboSelectionChanged;
      }

      this.LoadAccentsCombo( dataGrid );
    }

    private void LoadAccentsCombo( DataGridControl dataGrid )
    {
      if( accentCombo == null )
        return;

      accentCombo.SelectionChanged -= this.OnAccentComboSelectionChanged;

      var themeData = this.GetCurrentThemeData();
      var accentDataSource = this.FindThemeAccentColorDataSource( themeData );

      if( accentDataSource != null )
      {
        accentCombo.ItemsSource = accentDataSource.View;

        if( ( dataGrid != null ) && ( dataGrid.View != null ) )
        {
          var accentDataList = accentCombo.Items.Cast<AccentData>().ToList();
          var themeAccentColor = this.GetThemeAccentColor( dataGrid.View.Theme );

          //# For now : Assume the accent color will always match AccentData.Description
          var selectedAccentData = accentDataList.FirstOrDefault( x => x.Description == themeAccentColor );
          if( selectedAccentData != null )
          {
            accentCombo.SelectedItem = selectedAccentData;
          }
        }

        accentCombo.Visibility = Visibility.Visible;
      }
      else
      {
        accentCombo.Visibility = Visibility.Hidden;
      }

      accentCombo.SelectionChanged += this.OnAccentComboSelectionChanged;
    }

    private void FilterThemes( ViewData view )
    {
      var dataSource = ( DataGridCollectionView )themesCombo.ItemsSource;
      if( dataSource == null )
        return;

      dataSource.Filter = new Predicate<object>( delegate ( object s )
        {
          return this.AcceptTheme( view, s as ThemeData );
        } );

      dataSource.Refresh();

      if( themesCombo.SelectedItem == null )
      {
        themesCombo.SelectedIndex = 0;
      }

      this.LoadAccentsCombo( grid );
    }

    private bool AcceptTheme( ViewData view, ThemeData theme )
    {
      if( view == null )
        return false;

      if( theme == null )
        return false;

      if( theme.Theme != null )
      {
        return theme.Theme.IsViewSupported( view.ViewType );
      }
      else if( theme.UseSystemTheme )
      {
        return ( theme.IsTheme3D == view.IsView3D );
      }
      else
      {
        return false;
      }
    }

    private void UpdateGridTheme()
    {
      // Update the view
      var viewData = this.GetCurrentViewData();

      if( ( viewData != null ) && ( grid.View.GetType() != viewData.ViewType ) )
      {
        grid.View = Activator.CreateInstance( viewData.ViewType ) as UIViewBase;
      }

      // Update the theme
      var themeData = this.GetCurrentThemeData();
      if( themeData != null )
      {
        if( themeData.UseSystemTheme )
        {
          grid.View.ClearValue( Xceed.Wpf.DataGrid.Views.ViewBase.ThemeProperty );
        }
        else
        {
          grid.Resources.MergedDictionaries.Clear();

          switch( themeData.ThemeGroup )
          {
            case "Metro Themes":
              {
                var accentData = this.GetCurrentAccentData();

                grid.View.Theme = this.CreateMetroTheme( themeData, accentData );
              }
              break;

            case "Material Design":
              {
                var accentData = this.GetCurrentAccentData();

                grid.View.Theme = this.CreateMaterialDesignTheme( themeData, accentData );
              }
              break;

            case "Fluent Design":
              {
                var accentData = this.GetCurrentAccentData();

                grid.View.Theme = this.CreateFluentDesignTheme( themeData, accentData );
              }
              break;

            default:
              {
                grid.View.Theme = themeData.Theme;
              }
              break;
          }
        }
      }
    }

    private MetroTheme CreateMetroTheme( ThemeData themeData, AccentData accentData )
    {
      var themeResourceDictionary = ( themeData.Description == "Metro Light" )
                                      ? ( MetroThemeResourceDictionaryBase )new MetroLightThemeResourceDictionary()
                                      : ( MetroThemeResourceDictionaryBase )new MetroDarkThemeResourceDictionary();
      MetroAccentColor accentColor;

      if( ( accentData != null ) && Enum.TryParse<MetroAccentColor>( accentData.Description, out accentColor ) )
      {
        themeResourceDictionary.AccentColor = accentColor;
      }
      else
      {
        themeResourceDictionary.AccentColor = MetroAccentColor.Orange;
      }

      return new MetroTheme( themeResourceDictionary );
    }

    private MaterialDesignTheme CreateMaterialDesignTheme( ThemeData themeData, AccentData accentData )
    {
      var themeResourceDictionary = new MaterialDesignResourceDictionary()
      {
        Mode = ( themeData.Description == "Material Design Light" )
                 ? MaterialDesignColorMode.Light
                 : MaterialDesignColorMode.Dark
      };
      MaterialDesignColor accentColor;

      if( ( accentData != null ) && Enum.TryParse<MaterialDesignColor>( accentData.Description, out accentColor ) )
      {
        themeResourceDictionary.PrimaryColor = accentColor;
      }
      else
      {
        themeResourceDictionary.PrimaryColor = MaterialDesignColor.Orange;
      }

      return new MaterialDesignTheme( themeResourceDictionary );
    }

    private FluentDesignTheme CreateFluentDesignTheme( ThemeData themeData, AccentData accentData )
    {
      var themeResourceDictionary = new FluentDesignResourceDictionary()
      {
        Mode = ( themeData.Description == "Fluent Design Light" )
                 ? EnvironmentMode.Light
                 : EnvironmentMode.Dark
      };
      FluentDesignAccentColor accentColor;

      if( ( accentData != null ) && Enum.TryParse<FluentDesignAccentColor>( accentData.Description, out accentColor ) )
      {
        themeResourceDictionary.AccentColor = accentColor;
      }
      else
      {
        themeResourceDictionary.AccentColor = FluentDesignAccentColor.DarkOrange;
      }

      return new FluentDesignTheme( themeResourceDictionary );
    }

    private ViewData GetCurrentViewData()
    {
      if( viewsCombo != null )
        return viewsCombo.SelectedItem as ViewData;

      return null;
    }

    private ThemeData GetCurrentThemeData()
    {
      if( themesCombo != null )
        return themesCombo.SelectedItem as ThemeData;

      return null;
    }

    private AccentData GetCurrentAccentData()
    {
      if( accentCombo != null )
        return accentCombo.SelectedItem as AccentData;

      return null;
    }

    private DataGridCollectionViewSource FindThemeAccentColorDataSource( ThemeData themeData )
    {
      if( themeData == null )
        return null;

      switch( themeData.ThemeGroup )
      {
        case "Metro Themes":
          return this.FindResource( "accentDataSource" ) as DataGridCollectionViewSource;

        case "Material Design":
          return this.FindResource( "primaryColorDataSource" ) as DataGridCollectionViewSource;

        case "Fluent Design":
          return this.FindResource( "fluentAccentDataSource" ) as DataGridCollectionViewSource;

        default:
          return null;
      }
    }

    private string GetThemeAccentColor( Theme theme )
    {
      var metroTheme = theme as MetroTheme;
      if( metroTheme != null )
        return ( ( MetroThemeResourceDictionaryBase )metroTheme.ThemeResourceDictionary ).AccentColor.ToString();

      var materialDesignTheme = theme as MaterialDesignTheme;
      if( materialDesignTheme != null )
        return ( ( MaterialDesignResourceDictionary )materialDesignTheme.ThemeResourceDictionary ).PrimaryColor.ToString();

      var fluentDesignTheme = theme as FluentDesignTheme;
      if( fluentDesignTheme != null )
        return ( ( FluentDesignResourceDictionary )fluentDesignTheme.ThemeResourceDictionary ).AccentColor.ToString();

      return null;
    }

    private double m_orderIDColumnWidth;
  }
}
