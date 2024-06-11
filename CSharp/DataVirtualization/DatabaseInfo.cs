/*
 * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [DatabaseInfo.cs]
 *  
 * This class is used to configure a sql connection and 
 * provides auto creation of data used by the DataVirtualization Sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  [Serializable]
  public class DatabaseInfo : INotifyPropertyChanged
  {
    public DatabaseInfo()
    {
      this.ServerName = "localhost";
      this.DatabaseName = "XCEEDTEST";
      this.TableName = "Persons";
      this.RecordCount = 10000;
      this.UseIntegratedSecurity = true;
    }


    public string ServerName 
    {
      get
      {
        return m_serverName;
      }
      set
      {
        if( !string.Equals( m_serverName, value ) )
        {
          m_serverName = value;
          this.OnPropertyChanged( new PropertyChangedEventArgs( "ServerName" ) );
        }
      }
    }

    [XmlIgnore]
    public string DatabaseName
    {
      get
      {
        return m_databaseName;
      }
      private set
      {
        if( !string.Equals( m_databaseName, value ) )
        {
          m_databaseName = value;
          this.OnPropertyChanged( new PropertyChangedEventArgs( "DatabaseName" ) );
        }
      }
    }

    public string TableName
    {
      get
      {
        return m_tableName;
      }
      set
      {
        if( !string.Equals( m_tableName, value ) )
        {
          m_tableName = value;
          this.OnPropertyChanged( new PropertyChangedEventArgs( "TableName" ) );
        }
      }
    }

    public int RecordCount
    {
      get
      {
        return m_recordCount;
      }
      set
      {
        if( m_recordCount != value )
        {
          m_recordCount = value;
          this.OnPropertyChanged( new PropertyChangedEventArgs( "RecordCount" ) );
        }
      }
    }

    public bool UseIntegratedSecurity
    {
      get
      {
        return m_useIntegratedSecurity;
      }
      set
      {
        if( m_useIntegratedSecurity != value )
        {
          m_useIntegratedSecurity = value;
          this.OnPropertyChanged( new PropertyChangedEventArgs( "UseIntegratedSecurity" ) );
        }
      }
    }

    public string Username
    {
      get
      {
        return m_username;
      }
      set
      {
        m_username = value;
      }
    }


    #region DATABASE TOOLS

    private string GetRootConnectionString()
    {
      return this.GetConnectionString( "MASTER" );
    }
    
    internal string GetConnectionString()
    {
      return this.GetConnectionString( m_databaseName );
    }

    private string GetConnectionString( string initialCatalog)
    {
      string connectionString = "Data Source=" + m_serverName + ";Initial Catalog=" + initialCatalog;

      if( m_useIntegratedSecurity )
      {
        connectionString += ";Integrated Security=True";
      }
      else
      {
        if( string.IsNullOrEmpty( m_password ) )
          m_password = m_readPasswordDelegate.Invoke();

        connectionString += ";User ID = " + m_username + ";Password = " + m_password;
      }

      #if !NETCORE && !NET5
      // Needed for async SqlCommand execution, which are needed for optimal data virtualization.
      connectionString += ";Asynchronous Processing=true";
      #endif

      return connectionString;
    }

    internal void EnsureDatabase()
    {
      using( SqlConnection sqlConnection = new SqlConnection( this.GetRootConnectionString() ) )
      {
        sqlConnection.Open();

        try
        {
          SqlCommand sqlCommand = new SqlCommand(
            "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '" + m_databaseName + "') " +
            "CREATE DATABASE " + m_databaseName, sqlConnection );

          sqlCommand.ExecuteNonQuery();
        }
        finally
        {
          sqlConnection.Close();
        }
      }
    }

    internal void DropExistingTable()
    {
      using( SqlConnection sqlConnection = this.CreateSqlConnection() )
      {
        sqlConnection.Open();

        try
        {
          SqlCommand sqlCommand = new SqlCommand(
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + m_tableName + "]') AND type in (N'U')) " +
            "DROP TABLE [dbo].[" + m_tableName + "]", sqlConnection );

          sqlCommand.ExecuteNonQuery();
        }
        finally
        {
          sqlConnection.Close();
        }
      }
    }

    internal void CreateTable()
    {
      using( SqlConnection sqlConnection = this.CreateSqlConnection() )
      {
        try
        {
          sqlConnection.Open();

          SqlCommand sqlCommand = new SqlCommand( "CREATE TABLE [dbo].[" + m_tableName + "](" +
          "[PK] [int] NOT NULL, " +
          "[FirstName] [varchar](50) NOT NULL, " +
          "[LastName] [varchar](50) NOT NULL, " +
          "[Age] [int] NOT NULL, " +
          "[Children] [int] NOT NULL, " +
          "[PhoneNumber] [varchar](50) NULL, " +
          "[Employed] [bit] NOT NULL, " +
          "[Department] [varchar](50) NOT NULL, " +
          "[Boss] [varchar](50) NULL, " +
          "[HireDate] [datetime] NOT NULL, " +
          "[ReviewDate] [datetime] NULL, " +
          "CONSTRAINT [PK_" + m_tableName + "] PRIMARY KEY CLUSTERED " +
          "(" +
          "[PK] ASC " +
          ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]", sqlConnection );

          sqlCommand.ExecuteNonQuery();
        }
        finally
        {
          sqlConnection.Close();
        }
      }
    }

    internal void CreateRows( Dispatcher dispatcher, Action<int> progressCallback, Action cancelCallback )
    {
      using( SqlConnection sqlConnection = this.CreateSqlConnection() )
      {
        sqlConnection.Open();
        try
        {
          for( int i = 0; i < m_recordCount; i++ )
          {
            if( m_shouldCancelCreateData )
              break;

            dispatcher.Invoke( DispatcherPriority.Render, progressCallback, i );
            dispatcher.Invoke( DispatcherPriority.Background, new Action<int, SqlConnection>( this.AddPerson ), i, sqlConnection );
          }
        }
        finally
        {
          if( m_shouldCancelCreateData )
          {
            m_shouldCancelCreateData = false;
            dispatcher.Invoke( DispatcherPriority.Background, cancelCallback );
          }

          sqlConnection.Close();
        }
      }
    }

    internal void CreateIndexes( Dispatcher dispatcher, Action<int> progressCallback, Action cancelCallback )
    {
      string nonClusteredIndexScript = "CREATE NONCLUSTERED INDEX [IX_" + m_tableName + "_%ColumnName%] ON [dbo].[" + m_tableName + "] " +
        "(" +
        "  [%ColumnName%] ASC " +
        ")" +
        "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, " +
        "DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";

      using( SqlConnection sqlConnection = this.CreateSqlConnection() )
      {
        try
        {
          sqlConnection.Open();

          SqlCommand sqlCommand = null;
          for( int i = 1; i <= 10; i++ )
          {
            switch( i )
            {
              case 1:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "Age" ), sqlConnection );
                  break;
                }

              case 2:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "Boss" ), sqlConnection );
                  break;
                }

              case 3:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "Children" ), sqlConnection );
                  break;
                }

              case 4:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "Department" ), sqlConnection );
                  break;
                }

              case 5:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "Employed" ), sqlConnection );
                  break;
                }

              case 6:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "FirstName" ), sqlConnection );
                  break;
                }

              case 7:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "HireDate" ), sqlConnection );
                  break;
                }

              case 8:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "LastName" ), sqlConnection );
                  break;
                }

              case 9:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "PhoneNumber" ), sqlConnection );
                  break;
                }

              case 10:
                {
                  sqlCommand = new SqlCommand( nonClusteredIndexScript.Replace( "%ColumnName%", "ReviewDate" ), sqlConnection );
                  break;
                }
            }

            dispatcher.Invoke( DispatcherPriority.Render, progressCallback, ( Convert.ToInt32( m_recordCount * ( Convert.ToDouble( i ) / 10.0d ) ) ) );
            dispatcher.Invoke( DispatcherPriority.Background, new Func<int>( sqlCommand.ExecuteNonQuery ) );

            if( m_shouldCancelCreateData )
            {
              m_shouldCancelCreateData = false;
              dispatcher.Invoke( DispatcherPriority.Background, cancelCallback );
              return;
            }
          }
        }
        finally
        {
          sqlConnection.Close();
        }
      }
    }


    internal void AddPerson( int i, SqlConnection sqlConnection )
    {
      Person person = new Person();

      int PK = i + 1;
      string employed = ( person.Employed ) ? "TRUE" : "FALSE";

      SqlCommand sqlCommand = new SqlCommand( "INSERT INTO " + m_tableName + " VALUES( " +
        PK.ToString() + "," +
        "'" + person.FirstName + "'," +
        "'" + person.LastName + "'," +
        person.Age.ToString() + "," +
        person.Children.ToString() + "," +
        "'" + person.PhoneNumber.ToString() + "'," +
        "'" + employed + "'," +
        "'" + person.Department + "'," +
        "'" + person.Boss + "'," +
        "'" + person.HireDate.ToString( "d", CultureInfo.InvariantCulture.DateTimeFormat ) + "'," +
        "'" + person.ReviewDate.ToString( "d", CultureInfo.InvariantCulture.DateTimeFormat ) + "')",
        sqlConnection );

      sqlCommand.ExecuteNonQuery();
    }

    internal void CancelCreateData()
    {
      m_shouldCancelCreateData = true;
    }

    internal void SetReadPasswordDelegate( Func<string> readPasswordDelegate )
    {
      m_readPasswordDelegate = readPasswordDelegate;
    }


    internal void TestConnection()
    {
      SqlConnection connection = new SqlConnection( this.GetRootConnectionString() );

      connection.Open();
      connection.Close();
    }

    internal void CheckExistance()
    {
      SqlConnection connection = new SqlConnection( this.GetConnectionString() );
      connection.Open();
      try
      {
        string countQuery = "SELECT COUNT( * ) FROM " + m_tableName;

        SqlCommand command = new SqlCommand( countQuery, connection );
        command.ExecuteScalar();
      }
      finally
      {
        connection.Close();
      }
    }

    private SqlConnection CreateSqlConnection()
    {
      return new SqlConnection( this.GetConnectionString() );
    }

    #endregion DATABASE TOOLS


    #region PRIVATE FIELDS

    private string m_serverName;
    private string m_databaseName;
    private string m_tableName;

    private int m_recordCount;

    private bool m_useIntegratedSecurity;

    private string m_username;
    private string m_password;
    
    private bool m_shouldCancelCreateData;

    private Func<string> m_readPasswordDelegate;

    #endregion PRIVATE FIELDS

    #region INotifyPropertyChanged Members

    protected virtual void OnPropertyChanged( PropertyChangedEventArgs e )
    {
      if( this.PropertyChanged != null )
        this.PropertyChanged( this, e );
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

  }
}
