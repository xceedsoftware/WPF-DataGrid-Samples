/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
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

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Xceed.Wpf.DataGrid.Samples.MasterDetail
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      InitializeComponent();
      this.SetDetailConfigurations();
    }

    private void ShowSubEmployeesDetailChecked( object sender, RoutedEventArgs e )
    {
      this.SetDetailConfigurations();
    }

    private void ShowOrdersDetailChecked( object sender, RoutedEventArgs e )
    {
      this.SetDetailConfigurations();
    }

    private void SetDetailConfigurations()
    {
      if( this.grid == null )
        return;

      this.grid.DetailConfigurations.Clear();

      if( this.showSubEmployeesDetail.IsChecked.Value )
        this.grid.DetailConfigurations.Add( ( DetailConfiguration )this.Resources[ "subEmployeesDetailConfiguration" ] );

      if( this.showOrdersDetail.IsChecked.Value )
        this.grid.DetailConfigurations.Add( ( DetailConfiguration )this.Resources[ "ordersDetailConfiguration" ] );

      // Collapse all details and re-expand them.
      List<DataGridContext> dataGridContexts = new List<DataGridContext>( this.grid.GetChildContexts() );

      foreach( DataGridContext dataGridContext in dataGridContexts )
      {
        dataGridContext.ParentDataGridContext.CollapseDetails( dataGridContext.ParentItem );
        dataGridContext.ParentDataGridContext.ExpandDetails( dataGridContext.ParentItem );
      }
    }

    private void grid_DeletingSelectedItems( object sender, CancelRoutedEventArgs e )
    {
      e.Cancel = ( MessageBoxResult.No == MessageBox.Show(
        "Are you sure you want to delete the selected items?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question ) );
    }

    private void grid_DeletingSelectedItemError( object sender, DeletingSelectedItemErrorRoutedEventArgs e )
    {
      // When an exception occur while deleting a row, you have 
      // the chance to rectify the situation and retry the operation.
      // You can also skip the item in error or abort the operation.
      // For the purpose of this sample, we will skip on error.
      e.Action = DeletingSelectedItemErrorAction.Skip;
    }

    // Handled in the DataGridContext template to navigate to the corresponding
    // item in the "clicked" context.
    private void titleTextBlock_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
    {
      if( sender is TextBlock )
      {
        DataGridContext mouseDownContext = ( ( TextBlock )sender ).DataContext as DataGridContext;
        DataGridContext currentContext = DataGridControl.GetDataGridContext( sender as DependencyObject );
        DataGridContext parentContext = currentContext.ParentDataGridContext;

        do
        {
          if( mouseDownContext.Equals( parentContext ) )
          {
            mouseDownContext.CurrentItem = currentContext.ParentItem;
            mouseDownContext.SelectedItems.Add( mouseDownContext.CurrentItem );
            return;
          }

          currentContext = parentContext;
          parentContext = ( parentContext != null ) ? parentContext.ParentDataGridContext : parentContext;
        }
        while( parentContext != null );

        if( parentContext == null )
        {
          if( mouseDownContext.Items.Count > 0 )
          {
            mouseDownContext.CurrentItem = mouseDownContext.Items.GetItemAt( 0 );
            mouseDownContext.SelectedItems.Add( mouseDownContext.CurrentItem );
          }
        }
      }
    }
  }
}
