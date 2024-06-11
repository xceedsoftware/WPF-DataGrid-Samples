/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
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
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.CustomViews
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      InitializeComponent();

      // There is some important code run when we select a view mode.
      // We have to "select" the default view after InitializeComponent is completed.
      this.customTableViewRadioButton.IsChecked = true;
    }

    /*
     * Assign the currently selected "CurrentItemGlyph" to the DataGrid's View.
     */
    private void SetCurrentItemGlyph( Xceed.Wpf.DataGrid.Views.UIViewBase view )
    {
      if( view != null )
      {
        GlyphItem item = ( GlyphItem )this.currentItemGlyphComboBox.SelectedItem;

        if( item == null )
        {
          view.ClearValue( Xceed.Wpf.DataGrid.Views.UIViewBase.CurrentItemGlyphProperty );
        }
        else if( item.Glyph == null )
        {
          view.ClearValue( Xceed.Wpf.DataGrid.Views.UIViewBase.CurrentItemGlyphProperty );
        }
        else
        {
          view.CurrentItemGlyph = item.Glyph;
        }
      }
    }

    /*
     * To enable (or disable) our CardView custom DataRow layout we simply add an 
     * implicit style for DataRow (or remove it) in the Page's resources.
     * Usually, this would be done by declaring directly the implicit Style in XAML, 
     * but here, we want it dynamically on or off.
     */
    private void SetCustomDataRowTemplate()
    {
      this.Resources.Remove( typeof( DataRow ) );

      if( this.useCustomDataRowTemplate.IsChecked.GetValueOrDefault() )
        this.Resources.Add( typeof( DataRow ), this.FindResource( "customCardViewDataRow" ) );
    }

    /*
     * Assign (or reset) the "Card Title Template" to the DataGrid's View.
     */
    private void SetAlternativeCardTitle( Xceed.Wpf.DataGrid.Views.CardView cardView )
    {
      if( cardView != null )
      {
        if( this.useAlternativeCardTitle.IsChecked.GetValueOrDefault() )
        {
          cardView.CardTitleTemplate = this.FindResource( "alternativeCardTitleTemplate" ) as DataTemplate;
        }
        else
        {
          cardView.ClearValue( Xceed.Wpf.DataGrid.Views.CardView.CardTitleTemplateProperty );
        }
      }
    }

    /*
     * The HideEmptyCells property will completely hide the Cell if its value is Null.
     */
    private void SetHideEmptyCells( Xceed.Wpf.DataGrid.Views.CardView cardView )
    {
      if( cardView != null )
      {
        cardView.HideEmptyCells = this.hideEmptyCellsCheckBox.IsChecked.GetValueOrDefault();
      }
    }

    /*
     * The column separator is a CardView property not to be confused with the 
     * DataGridControl's Columns. A CardView Column can contain many items (DataRow, 
     * Headers, Footers).
     */
    private void SetColumnSeparator( Xceed.Wpf.DataGrid.Views.CardView cardView )
    {
      if( cardView != null )
      {
        PenItem item = ( PenItem )this.columnSeparatorComboBox.SelectedItem;

        if( item == null )
        {
          cardView.ClearValue( Xceed.Wpf.DataGrid.Views.CardView.SeparatorLinePenProperty );
        }
        else if( item.Pen == null )
        {
          cardView.ClearValue( Xceed.Wpf.DataGrid.Views.CardView.SeparatorLinePenProperty );
        }
        else
        {
          cardView.SeparatorLinePen = item.Pen;
        }
      }
    }

    /*
     * To enable (or disable) our TableView custom DataRow layout we simply add an 
     * implicit style for DataRow (or remove it) in the Page's resources.
     * Usually, this would be done by declaring directly the implicit Style in XAML, 
     * but here, we want it dynamically on or off.
     */
    private void SetCustomDataRowStyle()
    {
      this.Resources.Remove( typeof( DataRow ) );
      this.Resources.Remove( typeof( ColumnManagerRow ) );

      if( this.useCustomDataRowStyle.IsChecked.GetValueOrDefault() )
      {
        this.Resources.Add( typeof( DataRow ), this.FindResource( "customTableViewDataRowStyle" ) );
      }
    }

    /*
     * The RowSelectorPane is accessible via the TableView.
     */
    private void SetShowRowSelectorPane( Xceed.Wpf.DataGrid.Views.TableView tableView )
    {
      if( tableView != null )
      {
        tableView.ShowRowSelectorPane = this.showRowSelectorPaneCheckBox.IsChecked.GetValueOrDefault();
      }
    }

    /*
     * The Horizontal and Vertical GridLines are handled separately.
     */
    private void SetGridLine( Xceed.Wpf.DataGrid.Views.TableView tableView )
    {
      if( tableView != null )
      {
        GridLinesItem gridLines = ( GridLinesItem )this.gridLineComboBox.SelectedItem;

        if( gridLines == null )
        {
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.HorizontalGridLineBrushProperty );
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.HorizontalGridLineThicknessProperty );
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.VerticalGridLineBrushProperty );
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.VerticalGridLineThicknessProperty );
          return;
        }

        if( gridLines.HorizontalBrush == null )
        {
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.HorizontalGridLineBrushProperty );
        }
        else
        {
          tableView.HorizontalGridLineBrush = gridLines.HorizontalBrush;
        }

        if( gridLines.HorizontalThickness < 0 )
        {
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.HorizontalGridLineThicknessProperty );
        }
        else
        {
          tableView.HorizontalGridLineThickness = gridLines.HorizontalThickness;
        }

        if( gridLines.VerticalBrush == null )
        {
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.VerticalGridLineBrushProperty );
        }
        else
        {
          tableView.VerticalGridLineBrush = gridLines.VerticalBrush;
        }

        if( gridLines.VerticalThickness < 0 )
        {
          tableView.ClearValue( Xceed.Wpf.DataGrid.Views.TableView.VerticalGridLineThicknessProperty );
        }
        else
        {
          tableView.VerticalGridLineThickness = gridLines.VerticalThickness;
        }
      }
    }

    /*
     * The CellContentTemplateSelector allows you to specify a different Template for
     * cells according to their value or other criterias.
     */
    private void SetUseCustomContentTemplateSelector()
    {
      if( this.grid != null )
      {
        if( this.useCustomContentTemplateSelector.IsChecked.GetValueOrDefault() )
        {
          this.grid.Columns[ "BirthDate" ].CellContentTemplateSelector = new ZodiacCellContentTemplateSelector();
        }
        else
        {
          this.grid.Columns[ "BirthDate" ].ClearValue( Column.CellContentTemplateSelectorProperty );
        }
      }
    }

    /****************************************
     * Event Handlers
     ****************************************/
    private void CardViewSelected( object sender, RoutedEventArgs e )
    {
      // Extract the custom CardView declaration from the resources.
      CompactCardView cardView = this.FindResource( "cardViewInstance" ) as CompactCardView;

      // Show the configuration panel associated with our custom Card View
      this.customCardViewParametersPanel.Visibility = Visibility.Visible;
      this.customTableViewParametersPanel.Visibility = Visibility.Collapsed;

      this.SetCustomDataRowTemplate();
      // this.SetCurrentItemGlyph( cardView );
      this.SetHideEmptyCells( cardView );
      this.SetColumnSeparator( cardView );
      this.SetAlternativeCardTitle( cardView );

      this.grid.View = cardView;

      // We don't want to show the zodiac sign watermark in the custom CardView layout.
      this.grid.Columns[ "BirthDate" ].ClearValue( Column.CellContentTemplateSelectorProperty );
    }

    private void TableViewSelected( object sender, RoutedEventArgs e )
    {
      // Extract the custom TableView declaration from the resources.
      Xceed.Wpf.DataGrid.Views.TableView tableView = this.FindResource( "tableViewInstance" ) as Xceed.Wpf.DataGrid.Views.TableView;

      // Show the configuration panel associated with our custom Table View
      this.customCardViewParametersPanel.Visibility = Visibility.Collapsed;
      this.customTableViewParametersPanel.Visibility = Visibility.Visible;

      this.SetCustomDataRowStyle();
      this.SetCurrentItemGlyph( tableView );
      this.SetShowRowSelectorPane( tableView );
      this.SetUseCustomContentTemplateSelector();
      this.SetGridLine( tableView );

      this.grid.View = tableView;
    }

    private void CurrentItemGlyphSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( this.grid != null )
        this.SetCurrentItemGlyph( this.grid.View );
    }

    /****************************************
     * CardView Event Handlers
     ****************************************/
    private void UseCustomDataRowTemplateChange( object sender, RoutedEventArgs e )
    {
      this.SetCustomDataRowTemplate();
    }

    private void UseAlternativeCardTitleChange( object sender, RoutedEventArgs e )
    {
      if( this.grid != null )
        this.SetAlternativeCardTitle( this.grid.View as Xceed.Wpf.DataGrid.Views.CardView );
    }

    private void HideEmptyCellsCheckedChange( object sender, RoutedEventArgs e )
    {
      if( this.grid != null )
        this.SetHideEmptyCells( this.grid.View as Xceed.Wpf.DataGrid.Views.CardView );
    }

    private void ColumnSeparatorSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( this.grid != null )
        this.SetColumnSeparator( this.grid.View as Xceed.Wpf.DataGrid.Views.CardView );
    }

    /****************************************
     * TableView Event Handlers
     ****************************************/
    private void UseCustomDataRowStyleChange( object sender, RoutedEventArgs e )
    {
      this.SetCustomDataRowStyle();
    }

    private void ShowRowSelectorPaneCheckedChanged( object sender, RoutedEventArgs e )
    {
      if( this.grid != null )
        this.SetShowRowSelectorPane( this.grid.View as Xceed.Wpf.DataGrid.Views.TableView );
    }

    private void UseCustomContentTemplateSelectorChanged( object sender, RoutedEventArgs e )
    {
      this.SetUseCustomContentTemplateSelector();
    }

    private void GridLineSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( this.grid != null )
        this.SetGridLine( this.grid.View as Xceed.Wpf.DataGrid.Views.TableView );
    }
  }
}
