/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
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
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.UpdateHeadersFooters();

      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
      this.Unloaded += new RoutedEventHandler( this.MainPage_Unloaded );
    }

    private void UpdateHeadersFooters()
    {
      // Add the various headers and footers element defined in the resources according
      // to the current view.
      if( this.grid.View is Xceed.Wpf.DataGrid.Views.TableView )
      {
        this.grid.View.Headers.Add( ( DataTemplate )this.FindResource( "tableViewHeader" ) );
        this.labelRadioButton.Visibility = Visibility.Visible;

      }
      else
      {
        this.grid.View.Headers.Add( ( DataTemplate )this.FindResource( "cardViewHeader" ) );
        this.labelRadioButton.Visibility = Visibility.Collapsed;
      }

      this.UpdateDefaultGroupConfiguration();
    }

    private void UpdateDefaultGroupConfiguration()
    {
      if( this.grid.View is Xceed.Wpf.DataGrid.Views.TableView )
      {
        this.grid.View.FixedFooters.Clear();

        if( this.summaryRadioButton.IsChecked.GetValueOrDefault( false ) )
        {
          this.grid.View.FixedFooters.Add( ( DataTemplate )this.FindResource( "tableViewFixedFooter" ) );
          this.grid.DefaultGroupConfiguration = ( GroupConfiguration )this.FindResource( "tableViewGroupConfigurationSummary" );
        }
        else if( this.labelRadioButton.IsChecked.GetValueOrDefault( false ) )
        {
          this.grid.View.FixedFooters.Add( ( DataTemplate )this.FindResource( "tableViewFixedFooterWithStatCellLabel" ) );
          this.grid.DefaultGroupConfiguration = ( GroupConfiguration )this.FindResource( "tableViewGroupConfigurationLabelStatCell" );
        }
        else
        {
          this.grid.View.FixedFooters.Add( ( DataTemplate )this.FindResource( "tableViewFixedFooter" ) );
          this.grid.DefaultGroupConfiguration = ( GroupConfiguration )this.FindResource( "tableViewGroupConfigurationExpandedCollapsed" );
        }
      }
      else
      {
        if( this.labelRadioButton.IsChecked.GetValueOrDefault( false ) )
        {
          this.expandedRadioButton.IsChecked = true;
        }

        this.grid.DefaultGroupConfiguration = ( this.summaryRadioButton.IsChecked.GetValueOrDefault( false ) )
                                                ? ( GroupConfiguration )this.FindResource( "cardViewGroupConfigurationSummary" )
                                                : ( GroupConfiguration )this.FindResource( "cardViewGroupConfigurationExpandedCollapsed" );
      }

      // The GroupLevelConfiguration's InitiallyExpanded property dictates 
      // whether the groups are expanded by default when the grid is filled.
      if( this.expandedRadioButton.IsChecked.GetValueOrDefault() || this.labelRadioButton.IsChecked.GetValueOrDefault() )
      {
        this.grid.DefaultGroupConfiguration.InitiallyExpanded = true;
      }
      else
      {
        this.grid.DefaultGroupConfiguration.InitiallyExpanded = false;
      }
    }

    /*
     * Event handlers
     */

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      ( ( App )Application.Current ).ViewChanged += new EventHandler( this.Application_ViewChanged );
    }

    private void MainPage_Unloaded( object sender, RoutedEventArgs e )
    {
      ( ( App )Application.Current ).ViewChanged -= new EventHandler( this.Application_ViewChanged );
    }

    private void Application_ViewChanged( object sender, EventArgs e )
    {
      var tableView = grid.View as Views.TableView;
      if( tableView != null )
      {
        tableView.AllowStatsEditor = true;
        tableView.AllowHeadersFootersEditor = true;
      }

      this.UpdateHeadersFooters();
    }

    private void SummaryTypeChanged( object sender, RoutedEventArgs e )
    {
      if( this.grid != null )
      {
        this.UpdateDefaultGroupConfiguration();

        if( this.grid.IsBeingEdited )
        {
          this.grid.EndEdit();
        }

        //Refresh the grid items.
        var source = this.grid.ItemsSource;
        this.grid.ItemsSource = null;
        this.grid.ItemsSource = source;
      }
    }
  }
}
