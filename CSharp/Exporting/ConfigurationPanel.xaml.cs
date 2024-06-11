/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
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

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  public partial class ConfigurationPanel : UserControl
  {
    public ConfigurationPanel()
    {
      InitializeComponent();

      // Typically, an application should use the ListSeparator defined in the regional
      // settings of the system.
      string listSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

      if( listSeparator == "," )
      {
        this.separatorComboBox.SelectedIndex = 0;
      }
      else if( listSeparator == ";" )
      {
        this.separatorComboBox.SelectedIndex = 1;
      }
      else
      {
        this.separatorComboBox.SelectedIndex = 2;
      }

      this.textQualifierComboBox.SelectedIndex = 0;
      this.dateTimeFormatComboBox.SelectedIndex = 0;
      this.numberFormatComboBox.SelectedIndex = 0;
    }

    public event Action<ExportFormat> ExportRequested;

    private void export_Click( object sender, RoutedEventArgs e )
    {
      if( this.ExportRequested != null )
        this.ExportRequested( ( ExportFormat )this.exportFormatComboBox.SelectedValue );
    }
  }
}
