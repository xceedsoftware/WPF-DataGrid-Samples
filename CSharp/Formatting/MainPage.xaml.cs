/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Formatting Sample Application
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using Xceed.Wpf.DataGrid.Export;

namespace Xceed.Wpf.DataGrid.Samples.Formatting
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      this.Cultures = new List<CultureInfo>() { new CultureInfo( "en-US" ),
                                                new CultureInfo("fr-FR"),
                                                new CultureInfo("zh-CN"),
                                                new CultureInfo( "en-GB" ) };

      InitializeComponent();
    }

    public List<CultureInfo> Cultures
    {
      get;
      private set;
    }

    private void ExportButton_Click( object sender, RoutedEventArgs e )
    {
      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        MessageBox.Show( "Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable" );
        return;
      }

      // The simplest way to export in Excel format is to call the 
      // DataGridControl.ExportToExcel() method. However, if you want to specify export
      // settings, you have to take the longer, more descriptive and flexible route: 
      // the ExcelExporter class.

      // excelExporter.FixedColumnCount will automatically be set to the specified
      // grid's FixedColumnCount value.
      ExcelExporter excelExporter = new ExcelExporter( this.grid );

      Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
      saveFileDialog.Filter = "XML Spreadsheet (*.xml)|*.xml|All files (*.*)|*.*";
      if( saveFileDialog.ShowDialog().GetValueOrDefault() )
      {
        try
        {
          using( Stream stream = saveFileDialog.OpenFile() )
          {
            excelExporter.Export( stream );
          }
        }
        catch( IOException IOe )
        {
          MessageBox.Show( IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning );
        }
        catch( Exception ex )
        {
          MessageBox.Show( "There seems to be a problem exporting the data. Please report this error to Xceed support."
            + "\n\r" + ex.Message
            + "\n\r" + ex.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }
      }
    }
  }
}
