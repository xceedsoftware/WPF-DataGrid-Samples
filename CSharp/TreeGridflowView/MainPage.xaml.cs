/*
 * Xceed DataGrid for WPF - SAMPLE CODE - TreeGridflowView View Sample Application
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
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.TreeGridflowView
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      DataView dataView = new DataView( ( ( App )Application.Current ).Employees, "ReportsTo is Null", null, DataViewRowState.CurrentRows );
      this.DataContext = dataView;

      InitializeComponent();

      ConfigurationData.Singleton.PropertyChanged += new PropertyChangedEventHandler( ConfigurationData_PropertyChanged );

      this.AdjustHeadersFooters();
    }

    private void ConfigurationData_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      switch( e.PropertyName )
      {
        case "ShowColumnManagerRow":
          this.AdjustHeadersFooters();
          break;
      }
    }

    private void DataGrid_ItemsSourceChangeCompleted( object sender, EventArgs e )
    {
      this.ExpandAllItems( DataGridControl.GetDataGridContext( ( DependencyObject )sender ) );
    }

    private void AdjustHeadersFooters()
    {
      // Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear();
      grid.View.Headers.Clear();

      // Add the column manager row in the FixedHeaders if desired
      if( ConfigurationData.Singleton.ShowColumnManagerRow )
      {
        grid.View.FixedHeaders.Add( this.FindResource( "columnManagerRowTemplate" ) as DataTemplate );
      }
    }

    private void ExpandAllItems( DataGridContext dataGridContext )
    {
      if( ( dataGridContext == null ) || !dataGridContext.HasDetails )
        return;

      foreach( var item in dataGridContext.Items )
      {
        if( dataGridContext.AreDetailsExpanded( item ) )
          continue;

        dataGridContext.ExpandDetails( item );
      }

      foreach( var childDataGridContext in dataGridContext.GetChildContexts() )
      {
        this.ExpandAllItems( childDataGridContext );
      }
    }
  }
}
