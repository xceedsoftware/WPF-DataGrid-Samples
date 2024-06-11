/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ConfigurationPanel.xaml.cs]
 *  
 * This class implements the various dynamic configuration options offered
 * in this sample. The underlying business object is ConfigurationData.Singleton.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public partial class ConfigurationPanel : UserControl
  {
    public ConfigurationPanel()
    {
      InitializeComponent();

      // If the actual LicenseKey prevents the usage of the ColumnChooser, disable the 
      // appropriate CheckBox.
      try
      {
        new Xceed.Wpf.DataGrid.Views.TableView().AllowColumnChooser = true;
      }
      catch( System.ComponentModel.LicenseException )
      {
        this.chkAllowColumnChooser.IsEnabled = false;
      }
    }

    public void CancelColumnWidthGridEdit()
    {
      this.columnWidthGrid.CancelEdit();
    }

    private void ColumnStretchMode_SelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( this.IsInitialized )
      {
        if( ConfigurationData.Singleton.ColumnStretchMode == Xceed.Wpf.DataGrid.Views.ColumnStretchMode.All )
        {
          // Disable the column width grid because the Column Width property have
          // no effect when all columns have "auto-calculated" widths.
          this.disablingBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
          this.columnWidthGrid.IsEnabled = false;
        }
        else
        {
          this.disablingBorder.HorizontalAlignment = HorizontalAlignment.Left;
          this.columnWidthGrid.IsEnabled = true;
        }
      }
    }
  }
}
