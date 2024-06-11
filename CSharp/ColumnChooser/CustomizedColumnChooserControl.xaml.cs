/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Chooser Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 * 
 * This class implements the Customized Column Chooser Control.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
{
  public partial class CustomizedColumnChooserControl : UserControl
  {
    public CustomizedColumnChooserControl()
    {
      InitializeComponent();
    }

    private void ShowHideButton_Click( object sender, RoutedEventArgs e )
    {
      Button button = sender as Button;
      if( button != null )
      {
        ColumnBase col = button.DataContext as ColumnBase;
        if( col != null )
        {
          col.Visible = !col.Visible;
        }
      }
    }
  }
}
