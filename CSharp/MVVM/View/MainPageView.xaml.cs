/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPageView.xaml.cs]
 *  
 * This class implements the required logic for the View.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.DataGrid.Export;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.View
{
  public partial class MainPageView : Page
  {
    public MainPageView()
    {
      //Initialize the ViewModel.
      this.DataContext = ( ( App )Application.Current ).MainPageViewModelInstance;
      InitializeComponent();

      //This binding is used to obtain the next ProductID value from the ViewModel, and set it for the end user when initializing the InsertionRow.
      BindingOperations.SetBinding( this, MainPageView.NextProductIDProperty, new Binding( "InitialProductIDValue" ) { Source = this.DataContext, Mode = BindingMode.OneWay } );
    }

    #region NextProductID Property

    public static readonly DependencyProperty NextProductIDProperty = DependencyProperty.Register(
      "NextProductID",
      typeof( int ),
      typeof( MainPageView ),
      new PropertyMetadata( 0 ) );

    public int NextProductID
    {
      get
      {
        return ( int )this.GetValue( MainPageView.NextProductIDProperty );
      }
      set
      {
        this.SetValue( MainPageView.NextProductIDProperty, value );
      }
    }

    #endregion

    private void OnInitializingInsertionRow( object sender, InitializingInsertionRowEventArgs e )
    {
      //Set the default values when the end user clicks on the InsertionRow.
      var insertionRowCells = e.InsertionRow.Cells;
      insertionRowCells[ "ProductID" ].Content = this.NextProductID;
      insertionRowCells[ "SupplierID" ].Content = 1;
      insertionRowCells[ "CategoryID" ].Content = 1;
      insertionRowCells[ "ReorderDate" ].Content = DateTime.Today;
    }

    private void OnExportButtonClick( object sender, RoutedEventArgs e )
    {
      // Even though it is the data that is exported, it is closely linked to the UI as it is presented to the end user -
      // for instance, ForeignKey columns can be exported using the displayed value, not the key value; only visible columns are exported, and in the order they appear -
      // which is the reason why the Export feature is available through the DataGrid, thus the following code is implemented on the View side.
      Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
      saveFileDialog.Filter = "XML Spreadsheet (*.xml)|*.xml|All files (*.*)|*.*";

      if( saveFileDialog.ShowDialog().GetValueOrDefault() )
      {
        try
        {
          using( Stream stream = saveFileDialog.OpenFile() )
          {
            var excelExporter = new ExcelExporter( grid ) { DetailDepth = 1, ShowStatsInDetails = true, StatFunctionDepth = 1 };
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
