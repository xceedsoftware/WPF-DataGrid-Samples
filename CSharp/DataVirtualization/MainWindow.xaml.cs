/*
 * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainWindow.xaml.cs]
 *  
 * This class implements the various dynamic configuration options offered
 * by the configuration panel declared in MainWindow.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.DataContext = this;
      InitializeComponent();

      DatabaseInfo connectionInfo = App.ReadDatabaseInfo();

      if( connectionInfo != null )
      {
        this.DatabaseInfo = connectionInfo;
      }
      else
      {
        this.DatabaseInfo = new DatabaseInfo();
      }

      this.DatabaseInfo.SetReadPasswordDelegate( new Func<string>( delegate
      { return passwordBox.Password; } ) );
    }

    #region DatabaseInfo Dependency Property

    public DatabaseInfo DatabaseInfo
    {
      get { return ( DatabaseInfo )GetValue( DatabaseInfoProperty ); }
      set { SetValue( DatabaseInfoProperty, value ); }
    }

    public static readonly DependencyProperty DatabaseInfoProperty =
        DependencyProperty.Register( "DatabaseInfo", typeof( DatabaseInfo ), typeof( MainWindow ), new UIPropertyMetadata( null ) );

    #endregion DatabaseInfo Dependency Property

    #region IsLaunched Read-Only Dependency Property

    private static readonly DependencyPropertyKey IsLaunchedPropertyKey =
        DependencyProperty.RegisterReadOnly( "IsLaunched", typeof( bool ), typeof( MainWindow ),
        new PropertyMetadata( false, new PropertyChangedCallback( MainWindow.OnIsLaunchedPropertyChanged ), new CoerceValueCallback( MainWindow.OnIsLaunchedCoerceValueCallback ) ) );

    private static object OnIsLaunchedCoerceValueCallback( object sender, object value )
    {
      bool isLaunched = ( bool )value;

      if( isLaunched )
      {
        DatabaseInfo databaseInfo = ( ( MainWindow )sender ).DatabaseInfo;

        try
        {

          // Test root connection to server.
          try
          {
            databaseInfo.TestConnection();
          }
          catch( Exception ex )
          {
            MessageBox.Show( "Connection Error.  Please review the connection parameters and credentials."
              + Environment.NewLine + Environment.NewLine + ex.Message.ToString(),
              "Connection Error.", MessageBoxButton.OK, MessageBoxImage.Error );

            throw;
          }


          // Test if database and table exists.
          try
          {
            databaseInfo.CheckExistance();
          }
          catch( Exception ex )
          {
            MessageBox.Show( "Database or datatable not found.  Please use the 'Create Data' button before connecting."
              + Environment.NewLine + Environment.NewLine + ex.Message.ToString(),
              "Connection Error.", MessageBoxButton.OK, MessageBoxImage.Error );

            throw;
          }
        }
        catch
        {
          return false;
        }
      }

      return isLaunched;
    }

    private static void OnIsLaunchedPropertyChanged( object sender, DependencyPropertyChangedEventArgs e )
    {
      bool isLaunched = ( bool )e.NewValue;

      if( isLaunched )
      {
        MainWindow mainWindow = sender as MainWindow;

        App.WriteDatabaseInfo( mainWindow.DatabaseInfo );

        ( ( DataGridVirtualizingCollectionView )mainWindow.grid.ItemsSource ).Refresh();
      }
    }

    public static readonly DependencyProperty IsLaunchedProperty = MainWindow.IsLaunchedPropertyKey.DependencyProperty;

    public bool IsLaunched
    {
      get { return ( bool )this.GetValue( MainWindow.IsLaunchedProperty ); }
    }

    private void SetIsLaunched( bool value )
    {
      this.SetValue( MainWindow.IsLaunchedPropertyKey, value );
    }

    #endregion IsLaunched Read-Only Dependency Property

    #region CreatingData Read-Only Dependency Property

    private static readonly DependencyPropertyKey CreatingDataPropertyKey =
        DependencyProperty.RegisterReadOnly( "CreatingData", typeof( bool ), typeof( MainWindow ), new PropertyMetadata( false ) );

    public static readonly DependencyProperty CreatingDataProperty = MainWindow.CreatingDataPropertyKey.DependencyProperty;

    public bool CreatingData
    {
      get { return ( bool )this.GetValue( MainWindow.CreatingDataProperty ); }
    }

    private void SetCreatingData( bool value )
    {
      this.SetValue( MainWindow.CreatingDataPropertyKey, value );
    }

    #endregion CreatingData Read-Only Dependency Property


    #region INITIAL DATA CREATION

    private void CreateDatabase_ButtonClick( object sender, RoutedEventArgs e )
    {
      DatabaseInfo databaseInfo = this.DatabaseInfo;

      if( this.CreatingData )
      {
        if( MessageBox.Show( "Are you sure you want to cancel the data creation ?", "Cancel data creation ?",
          MessageBoxButton.OKCancel, MessageBoxImage.Question ) == MessageBoxResult.OK )
        {
          createDataButton.IsEnabled = false;
          databaseInfo.CancelCreateData();
        }

        return;
      }

      this.SetCreatingData( true );
      try
      {
        try
        {
          databaseInfo.TestConnection();
        }
        catch( Exception ex )
        {
          MessageBox.Show( "Connection Error.  Please review the connection parameters and credentials."
            + Environment.NewLine + Environment.NewLine + ex.Message.ToString(),
            "Connection Error.", MessageBoxButton.OK, MessageBoxImage.Error );

          return;
        }

        databaseInfo.EnsureDatabase();


        try
        {
          databaseInfo.CheckExistance();

          // Datatable found.  Ask to drop and recreate.
          if( MessageBox.Show( "Table " + databaseInfo.TableName + " already exists."
            + Environment.NewLine + "Drop table and recreate with " + databaseInfo.RecordCount.ToString() + " records ?", "Existing table found.",
            MessageBoxButton.OKCancel, MessageBoxImage.Warning ) != MessageBoxResult.OK )
          {
            return;
          }

          // Drop table.
          databaseInfo.DropExistingTable();
        }
        catch
        {
        }

        databaseInfo.CreateTable();

        createDataButton.Content = "Cancel";

        databaseInfo.CreateRows( this.Dispatcher, new Action<int>( this.OnProgress ), new Action( this.OnCancel ) );

        // If the creation was not aborted...
        if( this.CreatingData )
        {
          progressBar.Value = 0;
          databaseInfo.CreateIndexes( this.Dispatcher, new Action<int>( this.OnProgress ), new Action( this.OnCancel ) );

          // If the creation was not aborted...
          if( this.CreatingData )
            this.SetIsLaunched( true );
        }
      }
      catch( Exception ex )
      {
        MessageBox.Show( "Server error.  Please review the connection parameters."
          + Environment.NewLine + Environment.NewLine + ex.Message.ToString(),
           "Server Error.", MessageBoxButton.OK, MessageBoxImage.Error );
      }
      finally
      {
        createDataButton.Content = "Create Data";
        progressBar.Value = 0;
        this.SetCreatingData( false );
      }
    }

    private void OnProgress( int progressVal )
    {
      this.progressBar.Value = progressVal;
    }

    private void OnCancel()
    {
      this.SetCreatingData( false );
      createDataButton.ClearValue( Button.IsEnabledProperty );
    }

    #endregion INITIAL DATA CREATION

    #region LAUNCHING

    private void launch_ButtonClick( object sender, RoutedEventArgs e )
    {
      this.SetIsLaunched( true );
    }

    #endregion LAUNCHING


    #region DATA VIRTUALIZATION HOOKS

    private void DataGridVirtualizingCollectionViewSource_AbortQueryItemCount( object sender, QueryItemCountEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AbortReadCount( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_AbortQueryItems( object sender, QueryItemsEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AbortReadData( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_AbortQueryGroups( object sender, QueryGroupsEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AbortReadDistinctValuesAndCounts( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_AbortQueryAutoFilterDistinctValues( object sender, QueryAutoFilterDistinctValuesEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AbortReadDistinctValues( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_QueryItems( object sender, QueryItemsEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AsyncReadData( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_QueryGroups( object sender, QueryGroupsEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AsyncReadDistinctValuesAndCounts( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_QueryItemCount( object sender, QueryItemCountEventArgs e )
    {
      if( !this.IsLaunched )
      {
        e.Count = 0;
        return;
      }

      if( m_adoDataBridge == null )
        m_adoDataBridge = new ADODataBridge( this.DatabaseInfo.GetConnectionString(), this.DatabaseInfo.TableName );

      m_adoDataBridge.AsyncReadCount( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_QueryAutoFilterDistinctValues( object sender, QueryAutoFilterDistinctValuesEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AsyncReadDistinctValues( sender, e );
    }

    private void DataGridVirtualizingCollectionViewSource_CommitItems( object sender, CommitItemsEventArgs e )
    {
      if( m_adoDataBridge != null )
        m_adoDataBridge.AsyncCommitData( sender, e );
    }

    #endregion DATA VIRTUALIZATION HOOKS


    #region PRIVATE FIELDS

    private ADODataBridge m_adoDataBridge;

    #endregion PRIVATE FIELDS
  }
}
