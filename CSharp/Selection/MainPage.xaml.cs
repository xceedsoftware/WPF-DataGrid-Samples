/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Selection™ View Sample Application
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
using System.ComponentModel;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Selection
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      InitializeComponent();

      ConfigurationData.Singleton.PropertyChanged += new PropertyChangedEventHandler( ConfigurationData_PropertyChanged );
      this.EnableDragSelectionChanged();
      this.UpdateGroupSelectionEnabled();
    }

    private void ConfigurationData_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      switch( e.PropertyName )
      {
        case "NavigationBehavior":
          this.NavigationBehaviorChanged();
          break;

        case "EnableDragSelection":
          this.EnableDragSelectionChanged();
          break;

        case "SelectionMode":
        case "SelectionUnit":
          this.UpdateGroupSelectionEnabled();
          break;
      }
    }

    private void NavigationBehaviorChanged()
    {
      if( ConfigurationData.Singleton.NavigationBehavior == NavigationBehavior.None )
      {
        this.grid.CurrentItem = null;
        this.grid.CurrentColumn = null;
        this.grid.SelectedItems.Clear();
      }
    }

    private void EnableDragSelectionChanged()
    {
      if( ConfigurationData.Singleton.EnableDragSelection )
      {
        grid.AllowDrag = true;
        grid.DragBehavior = DataGridDragBehavior.Select;
      }
      else
      {
        grid.AllowDrag = false;
      }
    }

    private void UpdateGroupSelectionEnabled()
    {
      if( ConfigurationData.Singleton.SelectionMode == SelectionMode.Single
        || ConfigurationData.Singleton.SelectionUnit == SelectionUnit.Cell )
      {
        ConfigurationData.Singleton.EnableIsGroupSelectionEnabled = false;
        ConfigurationData.Singleton.IsGroupSelectionEnabled = false;
      }
      else
      {
        ConfigurationData.Singleton.EnableIsGroupSelectionEnabled = true;
      }
    }
  }
}
