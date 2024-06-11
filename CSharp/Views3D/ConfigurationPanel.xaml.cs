/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ConfigurationPanel.xaml.cs]
 *  
 * This class implements the various dynamic configuration options offered
 * in this sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.Views3D
{
  public partial class ConfigurationPanel : UserControl
  {
    public ConfigurationPanel()
    {
      InitializeComponent();

      this.Loaded += new RoutedEventHandler( ConfigurationPanel_Loaded );
    }

    #region DataGridControl Property

    public static readonly DependencyProperty DataGridControlProperty = DependencyProperty.Register(
      "DataGridControl",
      typeof( DataGridControl ),
      typeof( ConfigurationPanel ),
      new FrameworkPropertyMetadata( null ) );

    public DataGridControl DataGridControl
    {
      get
      {
        return ( DataGridControl )this.GetValue( ConfigurationPanel.DataGridControlProperty );
      }
      set
      {
        this.SetValue( ConfigurationPanel.DataGridControlProperty, value );
      }
    }

    #endregion

    private void ConfigurationPanel_Loaded( object sender, RoutedEventArgs e )
    {
      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        this.exportButton.Visibility = Visibility.Hidden;
        this.importButton.Visibility = Visibility.Hidden;
      }

      // Initially choose the default view settings (with its associated background
      // via the Presets_SelectionChanged event handler).
      // This is done in the Loaded event to allow a consumer of ConfigurationPanel
      // the time to subscribe to its events (ApplyNewDataGridBackground).
      this.presetComboBox.SelectedIndex = 0;
    }

    private void Presets_SelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      PresetItem item = presetComboBox.SelectedItem as PresetItem;

      if( item != null )
      {
        Style viewStyle = null;

        if( item.ResourceDictionary != null )
          viewStyle = ( Style )item.ResourceDictionary[ typeof( Xceed.Wpf.DataGrid.Views.CardflowView3D ) ];

        this.presetComboBox.SelectedItem = null;

        if( item.PreferredDataGridBackgroundBrush == null )
        {
          this.GridBackgroundBrush.SelectedIndex = 0;
        }
        else
        {
          this.GridBackgroundBrush.SelectedValue = item.PreferredDataGridBackgroundBrush;
        }

        ViewImportExportManager.ImportViewFromStyle( this.DataGridControl.View, viewStyle );
      }
    }

    private void Export_Clicked( object sender, RoutedEventArgs e )
    {
      ViewImportExportManager.ExportView( this.DataGridControl.View );
    }

    private void Import_Clicked( object sender, RoutedEventArgs e )
    {
      ViewImportExportManager.ImportView( this.DataGridControl.View );
    }

    private void CardBackgroundBrush_Changed( object sender, SelectionChangedEventArgs e )
    {
      if( this.DataGridControl != null )
      {
        this.DataGridControl.Resources.Clear();

        if( e.AddedItems.Count > 0 )
        {
          this.DataGridControl.Resources.Add( typeof( DataRow ), ( ( DictionaryEntry )e.AddedItems[ 0 ] ).Value as Style );
        }
      }
    }

    private void GridBackgroundBrush_Changed( object sender, SelectionChangedEventArgs e )
    {
      if( this.DataGridControl != null )
      {
        if( e.AddedItems.Count > 0 )
        {
          this.DataGridControl.Background = ( ( DictionaryEntry )e.AddedItems[ 0 ] ).Value as Brush;
        }
        else
        {
          this.DataGridControl.ClearValue( DataGridControl.BackgroundProperty );
        }
      }
    }
  }
}
