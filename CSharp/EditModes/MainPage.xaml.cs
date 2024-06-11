/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Edit Modes Sample Application
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

namespace Xceed.Wpf.DataGrid.Samples.EditModes
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      InitializeComponent();

      this.UpdateGridCellEditorDisplayConditions();
      this.UpdateGridEditTriggers();
    }

    /*
     * Set the Xceed DataGridControl's CellEditorDisplayConditions property value
     * according to the currently checked options in the configuration panel.
     */
    private void UpdateGridCellEditorDisplayConditions()
    {
      CellEditorDisplayConditions conditions = CellEditorDisplayConditions.None;

      foreach( CellEditorDisplayConditionsItem conditionsItem in this.cellEditorDisplayConditionsItemsControl.Items )
      {
        if( conditionsItem.IsChecked )
          conditions = conditions | conditionsItem.CellEditorDisplayConditions;
      }

      this.grid.CellEditorDisplayConditions = conditions;
    }

    /*
     * Set the Xceed DataGridControl's EditTriggers property value
     * according to the currently checked options in the configuration panel.
     */
    private void UpdateGridEditTriggers()
    {
      EditTriggers triggers = EditTriggers.None;

      foreach( EditTriggersItem triggersItem in this.editTriggersItemsControl.Items )
      {
        if( triggersItem.IsChecked )
          triggers = triggers | triggersItem.EditTriggers;
      }

      this.grid.EditTriggers = triggers;
    }

    /*
     * Event Handlers
     */

    private void CellEditorDisplayConditionsChanged( object sender, RoutedEventArgs e )
    {
      this.UpdateGridCellEditorDisplayConditions();
    }

    private void EditTriggersChanged( object sender, RoutedEventArgs e )
    {
      this.UpdateGridEditTriggers();
    }
  }
}
