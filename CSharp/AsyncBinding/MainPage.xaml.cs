/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 *  
 * This sample demonstrates asynchronous binding, which provides a simple way
   of displaying data until synchronization is done. 
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
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

    #region Private Methods

    // Create the new collection of data items
    private void CreateDataItems()
    {
      PersonObservableCollection newSource = new PersonObservableCollection();

      for( double i = 0; i < 1000; i++ )
      {
        newSource.Add( new Person() );
      }

      this.Persons = new DataGridCollectionView( newSource );
    }

    #endregion

    #region Events Handler

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      // Create the initial data items.
      this.CreateDataItems();
    }

    private void OnReloadDataParametersClick( object sender, RoutedEventArgs e )
    {
      // Re-create the data items.
      this.CreateDataItems();
    }

    #endregion
  }
}
