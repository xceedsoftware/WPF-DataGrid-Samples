/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
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
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Converters;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      InitializeComponent();

      ConfigurationData.Singleton.DataGrid = this.grid;
      ConfigurationData.Singleton.PropertyChanged += new PropertyChangedEventHandler( ConfigurationData_PropertyChanged );
      this.SetDefaultPropertiesForStretchMode();
    }

    void ConfigurationData_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      switch( e.PropertyName )
      {
        case "ColumnStretchMode":
          this.SetDefaultPropertiesForStretchMode();
          break;

        case "UseAdvancedColumnManagement":
          this.UseAdvancedColumnManagementChanged();
          break;
      }
    }

    // This method assigns default values to the various controls in the configuration panel
    // and column properties, according to the currently selected "Column stretching mode".
    private void SetDefaultPropertiesForStretchMode()
    {
      // A cell in edit mode will not refresh its content if the underlying data item
      // is modified. We make sure to cancel a possible current edit process before 
      // setting the new column widths.
      this.configurationPanel.CancelColumnWidthGridEdit();

      switch( ConfigurationData.Singleton.ColumnStretchMode )
      {
        case ColumnStretchMode.None:
          // The ColumnStretchMinWidth property is not used with "starred-width" columns.
          // Set a zero value to reflect that.
          ConfigurationData.Singleton.ColumnStretchMinWidth = 0d;
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = false;
          // It is not recommended to allow end-user column resizing when at least one 
          // column width is "starred".
          ConfigurationData.Singleton.AllowColumnResizing = false;
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = true;

          this.SetColumnWidths( "SongName", "3*", 200d, null );
          this.SetColumnWidths( "Rating", "0.5*", null, 70d );
          this.SetColumnWidths( "LastPlayed", "*", null, null );
          this.SetColumnWidths( "ResetLastPlayed", "75", null, null );
          this.SetColumnWidths( "LastPlayedElapsed", "80", null, null );
          this.SetColumnWidths( "Artist", "2*", null, null );
          this.SetColumnWidths( "Country", "*", 100d, null );
          break;

        case ColumnStretchMode.First:
          ConfigurationData.Singleton.ColumnStretchMinWidth = 100d;
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = true;
          // It is not recommended to allow end-user column resizing when the first column
          // is stretched.
          ConfigurationData.Singleton.AllowColumnResizing = false;
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = true;

          this.SetColumnWidths( "SongName", null, null, null );
          this.SetColumnWidths( "Rating", null, null, null );
          this.SetColumnWidths( "LastPlayed", null, null, null );
          this.SetColumnWidths( "ResetLastPlayed", "75", null, null );
          this.SetColumnWidths( "LastPlayedElapsed", "80", null, null );
          this.SetColumnWidths( "Artist", null, null, null );
          this.SetColumnWidths( "Country", null, null, null );
          break;

        case ColumnStretchMode.Last:
          ConfigurationData.Singleton.ColumnStretchMinWidth = 70d;
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = true;
          ConfigurationData.Singleton.AllowColumnResizing = true;
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = true;

          this.SetColumnWidths( "SongName", "250", null, null );
          this.SetColumnWidths( "Rating", null, null, null );
          this.SetColumnWidths( "LastPlayed", null, null, null );
          this.SetColumnWidths( "ResetLastPlayed", "75", null, null );
          this.SetColumnWidths( "LastPlayedElapsed", "80", null, null );
          this.SetColumnWidths( "Artist", null, null, null );
          this.SetColumnWidths( "Country", null, null, null );
          break;

        case ColumnStretchMode.All:
          ConfigurationData.Singleton.ColumnStretchMinWidth = 50d;
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = true;
          // Setting the allowColumnResizingCheckBox is pointless because all of the 
          // columns are auto sized and column resizing will be disabled no matter what.
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = false;

          this.SetColumnWidths( "SongName", null, 200d, null );
          this.SetColumnWidths( "Rating", null, null, 50d );
          this.SetColumnWidths( "LastPlayed", null, null, 100d );
          this.SetColumnWidths( "ResetLastPlayed", null, 60d, null );
          this.SetColumnWidths( "LastPlayedElapsed", null, null, null );
          this.SetColumnWidths( "Artist", null, null, null );
          this.SetColumnWidths( "Country", null, null, null );
          break;

        default:
          Debug.Assert( false, "Unknown column stretch mode." );
          break;
      }
    }

    private void SetColumnWidths( string fieldName, string width, double? minWidth, double? maxWidth )
    {
      ColumnBase column = this.grid.Columns[ fieldName ];

      if( width == null )
      {
        column.ClearValue( Column.WidthProperty );
      }
      else
      {
        ColumnWidthConverter converter = new ColumnWidthConverter();
        column.Width = ( ColumnWidth )converter.ConvertFrom( null, CultureInfo.InvariantCulture, width );
      }

      if( ( minWidth.HasValue ) && ( ConfigurationData.Singleton.UseAdvancedColumnManagement ) )
      {
        column.MinWidth = minWidth.Value;
      }
      else
      {
        column.ClearValue( Column.MinWidthProperty );
      }

      if( ( maxWidth.HasValue ) && ( ConfigurationData.Singleton.UseAdvancedColumnManagement ) )
      {
        column.MaxWidth = maxWidth.Value;
      }
      else
      {
        column.ClearValue( Column.MaxWidthProperty );
      }
    }

    private void UseAdvancedColumnManagementChanged()
    {
      // Adds or removes specific Column MinWidth/MaxWidth according to whether the 
      // advanced column management mode is used.
      this.SetDefaultPropertiesForStretchMode();

      if( ConfigurationData.Singleton.UseAdvancedColumnManagement )
      {
        Xceed.Wpf.DataGrid.Views.ViewBase view = this.grid.View;

        view.Headers.Add( ( DataTemplate )this.FindResource( "columnWidthRowTemplate" ) );
        view.Headers.Add( ( DataTemplate )this.FindResource( "columnMinWidthRowTemplate" ) );
        view.Headers.Add( ( DataTemplate )this.FindResource( "columnMaxWidthRowTemplate" ) );
        view.Headers.Add( ( DataTemplate )this.FindResource( "headerSeparatorTemplate" ) );
      }
      else
      {
        this.grid.View.Headers.Clear();
      }
    }

    private void ResetLastPlayed_Click( object sender, RoutedEventArgs e )
    {
      Button button = ( Button )sender;
      System.Data.DataRowView dataRowView = button.DataContext as System.Data.DataRowView;

      if( dataRowView != null )
      {
        dataRowView[ "LastPlayed" ] = DBNull.Value;
      }
    }

    private void LastPlayedElapsed_QueryValue( object sender, DataGridItemPropertyQueryValueEventArgs e )
    {
      System.Data.DataRowView dataRowView = e.Item as System.Data.DataRowView;

      if( ( dataRowView == null ) || ( dataRowView[ "LastPlayed" ] == DBNull.Value ) )
      {
        e.Value = "";
      }
      else
      {
        long minutes = Convert.ToInt64( Math.Floor( ( ( TimeSpan )( DateTime.Now - ( DateTime )dataRowView[ "LastPlayed" ] ) ).TotalMinutes ) );

        if( minutes < 0 )
        {
          e.Value = "n/a";
        }
        else if( minutes >= 1051200 )
        {
          e.Value = Convert.ToInt64( Math.Floor( minutes / 525600d ) ).ToString() + " years";
        }
        else if( minutes >= 2880 )
        {
          e.Value = Convert.ToInt64( Math.Floor( minutes / 1440d ) ).ToString() + " days";
        }
        else if( minutes >= 120 )
        {
          e.Value = Convert.ToInt64( Math.Floor( minutes / 60d ) ).ToString() + " hours";
        }
        else
        {
          e.Value = minutes.ToString() + " minutes";
        }
      }
    }
  }
}
