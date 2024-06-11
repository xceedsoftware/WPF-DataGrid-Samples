/*
 * Xceed DataGrid for WPF - SAMPLE CODE -  Summaries & Statistics Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainWindow.xaml.cs]
 * 
 * This class implements the various dynamic configuration options offered 
 * by the configuration panel of the MainWindow.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Windows;
using Xceed.Wpf.DataGrid.Views;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
{
  public partial class MainWindow : System.Windows.Window
  {
    public MainWindow()
    {
      InitializeComponent();

      // When the Window has finished loading, select the appropriate view (the grid's
      // current view) in the ComboBox.
      this.Loaded += new RoutedEventHandler( MainWindow_Loaded );
    }

    private void MainWindow_Loaded( object sender, RoutedEventArgs e )
    {
      DataGridControl grid = this.FindDataGridControl( this.rootFrame );

      if( grid != null )
      {
        UIViewBase view = grid.View;
        
        foreach( ViewItem item in this.viewComboBox.Items )
        {
          if( item.Type == view.GetType() )
          {
            this.viewComboBox.SelectedItem = item;
            break;
          }
        }
      }
    }

    // Set a new View on the grid based on the one selected in the combobox by the user
    private void viewComboBoxChanged( object sender, SelectionChangedEventArgs e )
    {
      ViewItem viewItem = ( ViewItem )this.viewComboBox.SelectedItem;
      DataGridControl grid = this.FindDataGridControl( this.rootFrame );

      if( grid != null )
      {
        Type currentViewType = ( grid.View == null ? null : grid.View.GetType() );

        if( viewItem.Type != currentViewType )
        {
          grid.View = ( UIViewBase )Activator.CreateInstance( viewItem.Type );
          // Assign a theme that blends well with our custom styles and templates.
          grid.View.Theme = ( Theme )this.FindResource( "defaultTheme" );
          ( ( App )App.Current ).OnViewChanged( EventArgs.Empty );
        }
      }
    }

    // Helper fonction to find a child DataGridControl inside a parent element
    private DataGridControl FindDataGridControl( DependencyObject root )
    {
      int childrenCount = VisualTreeHelper.GetChildrenCount( root );
      DependencyObject child;
      DataGridControl foundGrid;

      for( int i = 0; i < childrenCount; i++ )
      {
        child = VisualTreeHelper.GetChild( root, i );

        if( child is DataGridControl )
          return ( DataGridControl )child;

        foundGrid = this.FindDataGridControl( child );

        if( foundGrid != null )
          return foundGrid;
      }

      return null;
    }
  }
}