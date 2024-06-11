/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
{
  public partial class MainWindow : Window
  {
    #region CONSTRUCTORS

    public MainWindow()
    {
      InitializeComponent();
    }

    #endregion CONSTRUCTORS

    #region EVENT HANDLERS

    private void viewComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      ViewItem viewItem = ( ViewItem )this.viewComboBox.SelectedItem;
      DataGridControl grid = this.FindDataGridControl( this.rootFrame );

      if( grid != null )
      {
        Type currentViewType = ( grid.View == null ? null : grid.View.GetType() );

        if( viewItem.Type != currentViewType )
        {
          ( ( App )App.Current ).OnViewChanging( EventArgs.Empty );
          grid.View = ( Xceed.Wpf.DataGrid.Views.UIViewBase )Activator.CreateInstance( viewItem.Type );
          ( ( App )App.Current ).OnViewChanged( EventArgs.Empty );
        }
      }
    }

    #endregion EVENT HANDLERS

    #region PRIVATE METHODS

    // Helper fonction to find a child DataGridControl inside a parent element
    private DataGridControl FindDataGridControl( DependencyObject root )
    {
      if( root == null )
        return null;

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

    #endregion PRIVATE METHODS
  }
}
