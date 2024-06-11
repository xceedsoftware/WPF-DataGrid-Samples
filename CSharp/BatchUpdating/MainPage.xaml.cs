/*
 * Xceed DataGrid for WPF - SAMPLE CODE - BatchUpdating Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 *  
 * This sample demonstrates how Xceed DataGrid for WPF performs with
 * a large amount of columns for updating column's VisiblePosition and Visibility.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Xceed.Wpf.DataGrid.Samples.BatchUpdating
{
  public partial class MainPage : Page
  {
    #region Constructors

    public MainPage()
    {
      // Set the window's DataContext to itself to allow the grid to bind directly to the
      // DataContext rather than using an RelativeSource binding to find the window.
      this.DataContext = this;

      InitializeComponent();
      this.Loaded += new RoutedEventHandler( MainPage_Loaded );
    }

    #endregion

    #region Properties

    #region Persons Property

    // Using a DependencyProperty as the backing store for Persons.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PersonsProperty = DependencyProperty.Register(
      "Persons",
      typeof( DataGridCollectionView ),
      typeof( MainPage ),
      new UIPropertyMetadata( null ) );

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

    #endregion

    #region IsAddingColumnsButtonEnabled Property

    public static readonly DependencyProperty IsAddingColumnsButtonEnabledProperty = DependencyProperty.Register(
        "IsAddingColumnsButtonEnabled",
        typeof( bool ),
        typeof( MainPage ),
        new PropertyMetadata( true ) );

    /// <summary>
    /// Gets or sets a value indicating whether the add/remove buttons should be enabled.
    /// </summary>
    public bool IsAddingColumnsButtonEnabled
    {
      get
      {
        return ( bool )GetValue( IsAddingColumnsButtonEnabledProperty );
      }
      set
      {
        SetValue( IsAddingColumnsButtonEnabledProperty, value );
      }
    }

    #endregion

    #endregion

    #region Events

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      // Create the initial data items.
      this.CreateDataItems();
    }

    private void ButtonRemoveColumns_Click( object sender, RoutedEventArgs e )
    {
      if( grid != null )
      {
        if( ( chkWithBatchUpdate != null ) && chkWithBatchUpdate.IsChecked.HasValue && chkWithBatchUpdate.IsChecked.Value )
        {
          using( grid.DeferColumnsUpdate() )
          {
            this.HideExtraColumns();
          }
        }
        else if( ( chkWithoutBatchUpdate != null ) && chkWithoutBatchUpdate.IsChecked.HasValue && chkWithoutBatchUpdate.IsChecked.Value )
        {
          this.HideExtraColumns();
        }
      }
    }

    private void ButtonAddColumns_Click( object sender, RoutedEventArgs e )
    {
      if( grid != null )
      {
        if( ( chkWithBatchUpdate != null ) && chkWithBatchUpdate.IsChecked.HasValue && chkWithBatchUpdate.IsChecked.Value )
        {
          using( grid.DeferColumnsUpdate() )
          {
            this.ShowExtraColumns();
          }
        }
        else if( ( chkWithoutBatchUpdate != null ) && chkWithoutBatchUpdate.IsChecked.HasValue && chkWithoutBatchUpdate.IsChecked.Value )
        {
          this.ShowExtraColumns();
        }
      }
    }

    #endregion

    #region Private Methods

    private void CreateDataItems()
    {
      PersonObservableCollection newSource = new PersonObservableCollection();

      // Add the desired number of rows and columns to the grid.
      for( double i = 0; i < m_rowCount; i++ )
      {
        newSource.Add( new Person( m_columnCount - Person.DefaultPropertyCount ) );
      }
      this.Persons = new DataGridCollectionView( newSource );

      int lastColumn = grid.Columns.Count;
      // Initial loading will hide all extra columns fast.
      using( grid.DeferColumnsUpdate() )
      {
        for( int i = Person.DefaultPropertyCount; i < lastColumn; ++i )
        {
          grid.Columns[ i ].SetValue( ColumnBase.VisibleProperty, false );
        }
      }
    }

    private void ShowExtraColumns()
    {
      WorkingText.Visibility = Visibility.Visible;
      Dispatcher.Invoke( DispatcherPriority.ApplicationIdle, ( Action )( ShowExtraColumnsCore ) );
    }

    private void ShowExtraColumnsCore()
    {
      int positionIndex = m_random.Next( 0, 50 );

      // Show extra columns from the grid and modify the position of all columns from grid.
      foreach( ColumnBase cb in grid.Columns )
      {
        if( !cb.Visible )
        {
          cb.SetValue( ColumnBase.VisibleProperty, true );
        }
        if( cb.VisiblePosition != positionIndex )
        {
          cb.SetCurrentValue( ColumnBase.VisiblePositionProperty, positionIndex );
        }

        positionIndex++;
      }

      this.IsAddingColumnsButtonEnabled = false;
      WorkingText.Visibility = Visibility.Collapsed;
    }

    private void HideExtraColumns()
    {
      WorkingText.Visibility = Visibility.Visible;
      Dispatcher.Invoke( DispatcherPriority.ApplicationIdle, ( Action )( HideExtraColumnsCore ) );
    }

    private void HideExtraColumnsCore()
    {
      int lastColumn = grid.Columns.Count;

      // Hide extra columns from the grid.
      for( int i = Person.DefaultPropertyCount; i < lastColumn; ++i )
      {
        ColumnBase col = grid.Columns[ i ];
        if( col.Visible )
        {
          col.SetValue( ColumnBase.VisibleProperty, false );
        }
      }

      this.IsAddingColumnsButtonEnabled = true;
      WorkingText.Visibility = Visibility.Collapsed;
    }

    #endregion

    #region Private Fields

    private double m_rowCount = 100;
    private int m_columnCount = 310;
    private Random m_random = new Random();

    #endregion Private Fields
  }
}
