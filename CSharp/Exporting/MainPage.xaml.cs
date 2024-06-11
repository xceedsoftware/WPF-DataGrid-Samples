/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
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
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using Xceed.Wpf.DataGrid.Export;

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      InitializeComponent();

      this.configurationPanel.ExportRequested += new Action<ExportFormat>( configurationPanel_ExportRequested );
    }

    private void configurationPanel_ExportRequested( ExportFormat exportFormat )
    {
      if( exportFormat == ExportFormat.Excel )
      {
        this.ExportToExcel();
      }
      else
      {
        this.ExportToCsv();
      }
    }

    private void ExportToCsv()
    {
      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        MessageBox.Show( "Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable" );
        return;
      }

      // The simplest way to export in CSV format is to call the 
      // DataGridControl.ExportToCsv() method. However, if you want to specify export
      // settings, you have to take the longer, more descriptive and flexible route: 
      // the CsvExporter class.
      CsvExporter csvExporter = new CsvExporter( this.grid );

      // By setting the Culture to the CurrentCulture (system culture by default), the
      // date and number formats set in the regional settings will be used.
      csvExporter.FormatSettings.Culture = CultureInfo.CurrentCulture;

      csvExporter.FormatSettings.DateTimeFormat = ConfigurationData.Singleton.DateTimeFormat;
      csvExporter.FormatSettings.NumericFormat = ConfigurationData.Singleton.NumberFormat;
      csvExporter.FormatSettings.Separator = ConfigurationData.Singleton.Separator;
      csvExporter.FormatSettings.TextQualifier = ConfigurationData.Singleton.TextQualifier;

      csvExporter.IncludeColumnHeaders = ConfigurationData.Singleton.IncludeColumnHeaders;
      csvExporter.RepeatParentData = ConfigurationData.Singleton.RepeatParentData;
      csvExporter.DetailDepth = ConfigurationData.Singleton.DetailDepth;
      csvExporter.UseFieldNamesInHeader = ConfigurationData.Singleton.UseFieldNamesInHeader;

      Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

      saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*";

      if( saveFileDialog.ShowDialog().GetValueOrDefault() )
      {
        try
        {
          using( Stream stream = saveFileDialog.OpenFile() )
          {
            csvExporter.Export( stream );
          }
        }
        catch( IOException IOe )
        {
          MessageBox.Show( IOe.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning );
        }
        catch( Exception e )
        {
          MessageBox.Show( "There seems to be a problem exporting the data. Please report this error to Xceed support."
            + "\n\r" + e.Message
            + "\n\r" + e.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }
      }
    }

    private void ExportToExcel()
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

      excelExporter.ExportStatFunctionsAsFormulas = ConfigurationData.Singleton.ExportStatFunctionsAsFormulas;
      excelExporter.IncludeColumnHeaders = ConfigurationData.Singleton.IncludeColumnHeaders;
      excelExporter.IsHeaderFixed = ConfigurationData.Singleton.IsHeaderFixed;
      excelExporter.RepeatParentData = ConfigurationData.Singleton.RepeatParentData;
      excelExporter.DetailDepth = ConfigurationData.Singleton.DetailDepth;
      excelExporter.StatFunctionDepth = ConfigurationData.Singleton.StatFunctionDepth;
      excelExporter.UseFieldNamesInHeader = ConfigurationData.Singleton.UseFieldNamesInHeader;

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
        catch( Exception e )
        {
          MessageBox.Show( "There seems to be a problem exporting the data. Please report this error to Xceed support."
            + "\n\r" + e.Message
            + "\n\r" + e.StackTrace, "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }
      }
    }
  }
}
