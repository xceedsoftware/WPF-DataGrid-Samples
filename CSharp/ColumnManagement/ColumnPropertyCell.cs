/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ColumnPropertyCell.cs]
 *  
 * This class implements a custom Cell used by the ColumnPropertyRow class.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public class ColumnPropertyCell : Cell
  {
    static ColumnPropertyCell()
    {
      ColumnPropertyCellResources res = new ColumnPropertyCellResources();
      s_textBoxTemplate = ( DataTemplate )res[ "textBoxEditorTemplate" ];

      ColumnPropertyCell.IsColumnPropertySetProperty = ColumnPropertyCell.IsColumnPropertySetPropertyKey.DependencyProperty;
    }

    public ColumnPropertyCell()
    {
      // This cell type is not used to display and edit the content of a cell but the
      // column's size. Force the ReadOnly property to False.
      this.ReadOnly = false;

      this.ContextMenu = new ContextMenu();

      MenuItem menuItem = new MenuItem();

      menuItem.Header = "_Clear value";
      menuItem.CommandBindings.Add( new CommandBinding( ColumnPropertyCell.ClearValueCommand, this.ClearValueCommandExecuted, this.ClearValueCommandCanExecute ) );
      menuItem.Command = ColumnPropertyCell.ClearValueCommand;
      this.ContextMenu.Items.Add( menuItem );

      menuItem = new MenuItem();

      menuItem.Header = "Clear this _row's values";
      menuItem.CommandBindings.Add( new CommandBinding( ColumnPropertyCell.ClearRowValuesCommand, this.ClearRowValuesCommandExecuted, this.ClearRowValuesCommandCanExecute ) );
      menuItem.Command = ColumnPropertyCell.ClearRowValuesCommand;
      this.ContextMenu.Items.Add( menuItem );
    }

    #region ClearValueCommand Command

    // This command will clear the parent column's dependency property specified by the
    // parent row (ColumnPropertyRow.ColumnProperty).

    public static RoutedCommand ClearValueCommand = new RoutedCommand();

    private void ClearValueCommandExecuted( object sender, ExecutedRoutedEventArgs e )
    {
      this.ClearColumnPropertyValue();
    }

    private void ClearValueCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
    {
      e.CanExecute = this.IsColumnPropertySet;
    }

    private void ClearColumnPropertyValue()
    {
      ColumnBase parentColumn = this.ParentColumn;
      ColumnPropertyRow parentRow = this.ParentRow as ColumnPropertyRow;

      if( ( parentColumn != null ) && ( parentRow != null ) )
      {
        parentColumn.ClearValue( parentRow.ColumnProperty );
        // In the case where the value before the ClearValue is equal to the Column
        // property's default value, the PropertyChanged won't be triggered.
        // Force the IsColumnPropertySet update.
        this.UpdateIsColumnPropertySet( parentRow, parentColumn );
      }
    }

    #endregion ClearValueCommand Command

    #region ClearRowValuesCommand Command

    // This command will clear all the column dependency property specified by the
    // parent row (ColumnPropertyRow.ColumnProperty).

    public static RoutedCommand ClearRowValuesCommand = new RoutedCommand();

    private void ClearRowValuesCommandExecuted( object sender, ExecutedRoutedEventArgs e )
    {
      ColumnPropertyRow parentRow = this.ParentRow as ColumnPropertyRow;

      if( parentRow != null )
      {
        foreach( ColumnPropertyCell cell in parentRow.Cells )
        {
          cell.ClearColumnPropertyValue();
        }
      }
    }

    private void ClearRowValuesCommandCanExecute( object sender, CanExecuteRoutedEventArgs e )
    {
      e.CanExecute = true;
    }

    #endregion ClearRowValuesCommand Command

    #region IsColumnPropertySet Read-Only Property

    // This read-only dependency property returns True if the parent column has a local 
    // value associated with the parent row's ColumnProperty property. It allows the cell
    // to display its content in highlight and activate the "Clear value" command.

    protected static readonly DependencyPropertyKey IsColumnPropertySetPropertyKey =
        DependencyProperty.RegisterReadOnly( "IsColumnPropertySet", typeof( bool ), typeof( ColumnPropertyCell ), new PropertyMetadata( false ) );

    public static readonly DependencyProperty IsColumnPropertySetProperty;

    public bool IsColumnPropertySet
    {
      get
      {
        return ( bool )this.GetValue( ColumnPropertyCell.IsColumnPropertySetProperty );
      }
    }

    private void SetIsColumnPropertySet( bool value )
    {
      this.SetValue( ColumnPropertyCell.IsColumnPropertySetPropertyKey, value );
    }

    #endregion IsColumnPropertySet Read-Only Property

    // Initialize this cell content binding according to the parent row's 
    // (ColumnPropertyRow) ColumnProperty value.
    protected override void InitializeCore( DataGridContext dataGridContext, Row parentRow, ColumnBase parentColumn )
    {
      string path = null;
      ColumnPropertyRow columnPropertyRow = parentRow as ColumnPropertyRow;

      base.InitializeCore( dataGridContext, parentRow, parentColumn );

      if( ( columnPropertyRow != null ) && ( columnPropertyRow.ColumnProperty != null ) )
      {
        path = columnPropertyRow.ColumnProperty.Name;
      }

      if( ( parentColumn != null ) && ( path != null ) )
      {
        Binding binding = new Binding( path );
        binding.Mode = BindingMode.TwoWay;
        binding.Source = parentColumn;
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        if( columnPropertyRow.ColumnProperty.PropertyType == typeof( ColumnWidth ) )
        {
          binding.ValidationRules.Add( s_columnWidthValidationRule );
        }
        else
        {
          binding.Converter = new DoubleInfinityConverter();
        }

        BindingOperations.SetBinding( this, Cell.ContentProperty, binding );
      }
    }

    // This cell provides its own CellEditor. It's always a NumericTextBox, which means 
    // that, most of the time, it will be different than the default parent column's
    // CellEditor.
    protected override CellEditor GetCellEditor()
    {
      CellEditor cellEditor = new CellEditor();
      cellEditor.EditTemplate = s_textBoxTemplate;
      cellEditor.ActivationGestures.Add( new TextInputActivationGesture() );

      return cellEditor;
    }

    protected override void OnContentChanged( object oldContent, object newContent )
    {
      base.OnContentChanged( oldContent, newContent );

      // The parent column's property used in the cell's content binding has changed. 
      // Refresh the IsColumnPropertySet property.
      this.UpdateIsColumnPropertySet( this.ParentRow as ColumnPropertyRow, this.ParentColumn );
    }

    private void UpdateIsColumnPropertySet( ColumnPropertyRow parentRow, ColumnBase parentColumn )
    {
      if( ( parentColumn == null ) || ( parentRow == null ) )
        return;

      this.SetIsColumnPropertySet( parentColumn.ReadLocalValue( parentRow.ColumnProperty ) != DependencyProperty.UnsetValue );
    }

    private static readonly DataTemplate s_textBoxTemplate;
    private static readonly ColumnWidthValidationRule s_columnWidthValidationRule = new ColumnWidthValidationRule();
  }
}
