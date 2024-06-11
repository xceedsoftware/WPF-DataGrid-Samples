/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Printing Sample Application
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
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.DataGrid.Print;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.Printing
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    public MainPage()
    {
      m_doingInitializeComponent = true;
      InitializeComponent();
      m_doingInitializeComponent = false;
    }

    private void ButtonPrint_Click( object sender, RoutedEventArgs e )
    {
      this.textProgressInformation.Text = string.Empty;

      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        this.textProgressInformation.Text += "The UI cannot be updated in XBAP even though the ProgressionCallBack is called.  Please wait...\n";
      }

      this.textProgressInformation.Text += "Document preparation started...";

      this.progressScrollViewer.ScrollToBottom();

      m_printButton.IsEnabled = false;
      m_exportButton.IsEnabled = false;
      m_printPreviewButton.IsEnabled = false;
      try
      {
        bool jobCompleted = grid.Print( "Xceed Printing Sample", true, new EventHandler<ProgressEventArgs>( this.ProgressionCallBack ), true );

        if( jobCompleted )
        {
          this.textProgressInformation.Text += "\n...Completed.";
        }
        else
        {
          this.textProgressInformation.Text += "\n...Canceled.";
        }

        this.progressScrollViewer.ScrollToBottom();
      }
      finally
      {
        m_printButton.IsEnabled = true;
        m_exportButton.IsEnabled = true;
        m_printPreviewButton.IsEnabled = true;
      }
    }

    private void ButtonPrintPreview_Click( object sender, RoutedEventArgs e )
    {
      this.textProgressInformation.Text = string.Empty;

      Size size = new Size( 8.5d * 96.0d, 11.0d * 96.0d );

      this.textProgressInformation.Text = "Generating Preview...";
      this.progressScrollViewer.ScrollToBottom();

      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        MessageBox.Show(
          "Due to restricted user-access permissions caused by running the Live Explorer as an XBAP browser application, a feature-limited popup must be used to display the print-preview dialog. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to access the full feature set.", "Feature limited" );

        this.textProgressInformation.Text = "The UI cannot be updated in XBAP even though the ProgressionCallBack is called.  Please wait...\n";

        this.textProgressInformation.Text += "Showing Print Preview Popup";

        grid.ShowPrintPreviewPopup(
          "Xceed Printing Sample",
          true,
          new EventHandler<ProgressEventArgs>( this.ProgressionCallBack ),
          true,
          new Size( ( ( this.ActualHeight * 8.5d ) / 11 ), this.ActualHeight ),
          size );
      }
      else
      {
        bool jobCompleted = false;

        this.textProgressInformation.Text = "Showing Print Preview Popup";

        jobCompleted = grid.ShowPrintPreviewWindow(
        "Xceed Printing Sample", true,
        new EventHandler<ProgressEventArgs>( this.ProgressionCallBack ),
        true, new Size( 600, 800 ), size );

        if( jobCompleted )
        {
          this.textProgressInformation.Text += "\n...Completed.";
        }
        else
        {
          this.textProgressInformation.Text += "\n...Canceled.";
        }
      }

      this.progressScrollViewer.ScrollToBottom();
    }

    private void ButtonExport_Click( object sender, RoutedEventArgs e )
    {
      if( System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted )
      {
        MessageBox.Show( "Due to restricted user-access permissions, this feature cannot be demonstrated when the Live Explorer is running as an XBAP browser application. Please download the full Xceed DataGrid for WPF package and run the Live Explorer as a desktop application to try out this feature.", "Feature unavailable" );
        return;
      }

      this.textProgressInformation.Text = "Document exportation started...";

      this.progressScrollViewer.ScrollToBottom();

      Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

      saveFileDialog.Filter = "XPS files (*.xps)|*.xps|All files (*.*)|*.*";

      bool jobCompleted = false;
      if( saveFileDialog.ShowDialog().GetValueOrDefault() )
      {
        Size size = new Size( 8.5d * 96.0d, 11.0d * 96.0d );

        m_printButton.IsEnabled = false;
        m_exportButton.IsEnabled = false;
        try
        {
          jobCompleted = grid.ExportToXps( saveFileDialog.FileName,
            size, new Rect( size ),
            new PageRange( 1, 0 ), CompressionOption.Normal,
            new EventHandler<ProgressEventArgs>( this.ProgressionCallBack ), true );
        }
        catch( System.IO.IOException )
        {
          jobCompleted = false;
        }
        finally
        {
          m_printButton.IsEnabled = true;
          m_exportButton.IsEnabled = true;
        }
      }

      if( jobCompleted )
      {
        this.textProgressInformation.Text += "\n...Completed.";
      }
      else
      {
        this.textProgressInformation.Text += "\n...Canceled.";
      }

      this.progressScrollViewer.ScrollToBottom();
    }

    private void ProgressionCallBack( object sender, ProgressEventArgs e )
    {
      this.textProgressInformation.Text += "\nPreparing page " + e.ProgressInfo.CurrentPageNumber.ToString();

      this.progressScrollViewer.ScrollToBottom();
    }

    private void SelectedPrintViewChanged( object sender, RoutedEventArgs e )
    {
      if( m_doingInitializeComponent )
        return;

      if( chkConfiguredPrintView.IsChecked.Value )
      {
        grid.PrintView = this.Resources[ "configuredPrintView" ] as PrintViewBase;
      }
      else if( chkCustomPrintView.IsChecked.Value )
      {
        grid.PrintView = this.Resources[ "customPrintView" ] as PrintViewBase;
      }
      else
      {
        grid.PrintView = null;
      }
    }

    public bool m_doingInitializeComponent; // = false;
  }
}
