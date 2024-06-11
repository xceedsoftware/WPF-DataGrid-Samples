/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 *  
 * This sample demonstrates how Xceed DataGrid for WPF performs with
 * a large amount of rows and/or columns.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
{
  public partial class MainPage : Page
  {
    // Generic Delegate for DataSet refresh
    private delegate void GenericDelegate();

    public MainPage()
    {
      // Set the window's DataContext to itself to allow the grid to bind directly to the
      // DataContext rather than using an RelativeSource binding to find the window.
      this.DataContext = this;

      InitializeComponent();
      this.Loaded += new RoutedEventHandler( MainPage_Loaded );
    }

    void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      // Create the initial data items.
      this.CreateDataItems();
    }

    #region Persons Property

    // Property that provides the Person data.
    public DataGridCollectionView Persons
    {
      get
      {
        return ( DataGridCollectionView )GetValue( PersonsProperty );
      }
      set
      {
        SetValue( PersonsProperty, value );
      }
    }

    // Using a DependencyProperty as the backing store for Persons.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PersonsProperty =
        DependencyProperty.Register( "Persons", typeof( DataGridCollectionView ), typeof( MainPage ), new UIPropertyMetadata( null ) );

    #endregion

    #region IsApplyButtonEnabled

    public bool IsApplyButtonEnabled
    {
      get
      {
        return ( bool )this.GetValue( IsApplyButtonEnabledProperty );
      }
      private set
      {
        this.SetValue( IsApplyButtonEnabledPropertyKey, value );
      }
    }

    private static readonly DependencyPropertyKey IsApplyButtonEnabledPropertyKey = DependencyProperty.RegisterReadOnly( "IsApplyButtonEnabled", typeof( bool ), typeof( MainPage ), new UIPropertyMetadata( false ) );

    public static readonly DependencyProperty IsApplyButtonEnabledProperty = IsApplyButtonEnabledPropertyKey.DependencyProperty;

    #endregion

    // Create the new collection of data items on a background thread.
    private void CreateDataItems()
    {
      // Disable RadioButtons while generating
      this.columnsGroupBox.IsEnabled = false;
      this.rowsGroupBox.IsEnabled = false;

      // Disable ApplyButton before DataSet generation. It will
      // be re-enabled when one of the Column / Row count option
      // is modified
      this.IsApplyButtonEnabled = false;

      if( this.grid != null )
      {
        // Hide the grid to display a wait cursor and progress information instead.
        this.grid.Visibility = Visibility.Hidden;
      }

      // Generate the rows in a background thread.
      if( ThreadPool.QueueUserWorkItem( new WaitCallback( this.BackgroundWorker ) ) == false )
        throw new ApplicationException( "Unable to generate the new data items." );
    }

    #region Background Thread Item Creation

    // Background worker thread
    private void BackgroundWorker( object param )
    {
      // Recreate a new collection in the background.
      PersonObservableCollection newSource = new PersonObservableCollection();

      // Add the desired number of rows and columns to the grid. For each item that is added
      // we will update the progress bar in the UI thread.
      for( double i = 0; i < m_rowCount; i++ )
      {
        newSource.Add( new Person( m_columnCount - Person.DefaultPropertyCount ) );

        lock( m_lockObject )
        {
          if( m_updateDispatcherOperation == null )
          {
            m_updateDispatcherOperation = this.Dispatcher.BeginInvoke( DispatcherPriority.Normal, new WaitCallback( this.OnProgress ), null );
          }

          m_percent = ( i / m_rowCount ) * 100;
        }
      }

      this.Dispatcher.BeginInvoke( DispatcherPriority.Send, new FinalizeDelegate( this.OnFinalize ), newSource );
    }

    private void OnFinalize( PersonObservableCollection newSource )
    {
      // The grid will preserve its columns even when it is assigned a new data source;
      // therefore, clear the grid's columns before assigning the new DataGridCollectionView
      // so that the "extra" columns are removed.
      this.grid.Columns.Clear();

      this.Persons = new DataGridCollectionView( newSource );

      // Force Garbage Collection to avoid excessive memory usage
      GC.Collect();

      // Show the grid.
      this.grid.Visibility = Visibility.Visible;

      // Enable RadioButtons after DataSet generation
      this.columnsGroupBox.IsEnabled = true;
      this.rowsGroupBox.IsEnabled = true;
    }

    // Display progress information.
    private void OnProgress( object param )
    {
      lock( m_lockObject )
      {
        this.progressBar.Value = m_percent;

        m_updateDispatcherOperation = null;
      }
    }

    #endregion Background Thread Item Creation

    #region Radio Button Selection-Changed Events

    private void ColumnRadioButtonChanged_Checked( object sender, RoutedEventArgs e )
    {
      // Enable the Apply button to refresh the DataSource
      this.IsApplyButtonEnabled = true;

      RadioButton selectedRadioButton = sender as RadioButton;

      if( sender == null )
        return;

      string radioButtonTag = selectedRadioButton.Tag as String;

      if( string.IsNullOrEmpty( radioButtonTag ) == true )
        return;

      try
      {
        // Get the Column count from the RadioButton.Tag property
        m_columnCount = Int32.Parse( radioButtonTag );
      }
      catch( Exception )
      {
        Debug.WriteLine( "Unable to convert Columns count from RadioButton.Tag" );
      }

    }

    private void RowRadioButtonChanged_Checked( object sender, RoutedEventArgs e )
    {
      // Enable the Apply button to refresh the DataSource
      this.IsApplyButtonEnabled = true;

      RadioButton selectedRadioButton = sender as RadioButton;

      if( sender == null )
        return;

      string radioButtonTag = selectedRadioButton.Tag as String;

      if( string.IsNullOrEmpty( radioButtonTag ) == true )
        return;

      try
      {
        // Get the Row count from the RadioButton.Tag property
        m_rowCount = Double.Parse( radioButtonTag );
      }
      catch( Exception )
      {
        Debug.WriteLine( "Unable to convert Rows count from RadioButton.Tag" );
      }
    }

    #endregion Radio Button Selection-Changed Events

    #region Apply Button Click Events

    private void OnApplySourceParametersClick( object sender, RoutedEventArgs e )
    {
      this.CreateDataItems();
    }

    #endregion

    #region Private Fields

    private double m_rowCount = 0;
    private int m_columnCount = 0;
    private double m_percent = 0;

    private delegate void ProgressDelegate( double percent );
    private delegate void FinalizeDelegate( PersonObservableCollection newCollection );

    private DispatcherOperation m_updateDispatcherOperation = null;
    private object m_lockObject = new object();

    #endregion Private Fields
  }
}
