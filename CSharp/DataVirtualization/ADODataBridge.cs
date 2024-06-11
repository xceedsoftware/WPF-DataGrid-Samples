/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ADODataBridge.cs]
 *  
 * This class implements a dynamic SQL query generator for the
 * purpose of providing data to the DataGridVirtualizingCollectionView.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  public class ADODataBridge
  {
    #region Static Members

    private const string DefaultSortFilterQuery =
      "SELECT * FROM %TABLE% WHERE %PK% IN (SELECT %PK% FROM (SELECT ROW_NUMBER() OVER (%SORT CLAUSE%) AS ROWID, %PK% FROM %TABLE% %WHERE CLAUSE%)AS INNER_RESULT WHERE INNER_RESULT.ROWID BETWEEN %STARTROWID% AND %ENDROWID%)%SORT CLAUSE%";

    private const string DefaultDistinctQuery =
      "SELECT DISTINCT( %FIELD% ) FROM %TABLE%";

    private const string DefaultCountQuery =
      "SELECT COUNT( * ) FROM %TABLE% %WHERE CLAUSE%";

    private const string DefaultUpdateQuery =
      "UPDATE %TABLE% SET %UPDATE CLAUSE% %WHERE CLAUSE%";

    private static string BuildCountQuery( DataGridVirtualizingCollectionView dgvcv, ReadOnlyCollection<DataGridGroupInfo> groupPath, string tableName )
    {
      var whereClause = ADODataBridge.BuildWhereClause( dgvcv, groupPath );
      var query = DefaultCountQuery.Replace( "%TABLE%", tableName ).Replace( "%WHERE CLAUSE%", whereClause ).TrimEnd( ' ' );

      return query;
    }

    private static string BuildSelectQuery(
      DataGridVirtualizingCollectionView dgvcv,
      ReadOnlyCollection<DataGridGroupInfo> groupPath,
      int pageStartIndex, int count,
      string pk, string tableName )
    {
      var startRowID = pageStartIndex + 1;
      var endRowID = startRowID + count - 1;

      var query = ADODataBridge.DefaultSortFilterQuery.Replace(
        "%PK%", pk ).Replace(
        "%TABLE%", tableName ).Replace(
        "%STARTROWID%", startRowID.ToString() ).Replace(
        "%ENDROWID%", endRowID.ToString() );

      var whereClause = ADODataBridge.BuildWhereClause( dgvcv, groupPath );
      var sortClause = ADODataBridge.BuildSortClause( dgvcv, pk );

      query = query.Replace( "%SORT CLAUSE%", sortClause );
      query = query.Replace( "%WHERE CLAUSE%", whereClause );

      return query;
    }

    private static string BuildUpdateQuery( DataGridVirtualizingCollectionView dgvcv, string tableName, string pk, VirtualizedItemInfo virtualizedItemInfo )
    {
      var query = ADODataBridge.DefaultUpdateQuery.Replace( "%TABLE%", tableName );
      var dataRowView = virtualizedItemInfo.DataItem as System.Data.DataRowView;
      var updateClauseBuilder = new StringBuilder();

      foreach( DataGridItemProperty itemProperty in dgvcv.ItemProperties )
      {
        if( itemProperty.IsReadOnly )
          continue;

        var fieldName = itemProperty.Name;

        var newValue = dataRowView[ fieldName ];
        var oldValue = virtualizedItemInfo.OldValues[ itemProperty.Name ];

        if( object.Equals( newValue, oldValue ) )
          continue;

        if( updateClauseBuilder.Length != 0 )
          updateClauseBuilder.Append( ", " );

        if( ( newValue is string ) || ( newValue is DateTime ) || ( newValue is bool ) )
        {
          updateClauseBuilder.Append( fieldName + " = '" + newValue.ToString() + "'" );
        }
        else
        {
          updateClauseBuilder.Append( fieldName + " = " + newValue.ToString() );
        }
      }

      updateClauseBuilder.Append( ' ' );

      query = query.Replace( "%UPDATE CLAUSE%", updateClauseBuilder.ToString() );

      query = query.Replace( "%WHERE CLAUSE%", "WHERE " + ( pk + " = " + virtualizedItemInfo.OldValues[ pk ] ) );

      return query;
    }

    private static string BuildWhereClause( DataGridVirtualizingCollectionView dgvcv, ReadOnlyCollection<DataGridGroupInfo> groupPath )
    {
      var filterFoundForOneItemProperty = false;
      var whereClause = "WHERE ";

      var groupLevel = groupPath.Count;
      for( int i = 0; i < groupLevel; i++ )
      {
        if( filterFoundForOneItemProperty )
        {
          whereClause += " AND ";
        }

        filterFoundForOneItemProperty = true;

        whereClause += " ( ";

        var itemPropertyName = groupPath[ i ].PropertyName;
        var filterValue = groupPath[ i ].Value;

        if( ( filterValue is string ) || ( filterValue is DateTime ) || ( filterValue is bool ) )
        {
          whereClause += itemPropertyName + " = '" + filterValue.ToString() + "'";
        }
        else
        {
          whereClause += itemPropertyName + " = " + filterValue.ToString();
        }

        whereClause += " ) ";
      }


      foreach( DataGridItemProperty itemProperty in dgvcv.ItemProperties )
      {
        var itemPropertyName = itemProperty.Name;

        IList itemAutoFilterValues;
        if( dgvcv.AutoFilterValues.TryGetValue( itemProperty.Name, out itemAutoFilterValues ) )
        {
          var filterValueCount = itemAutoFilterValues.Count;
          if( filterValueCount == 0 )
            continue;

          if( filterFoundForOneItemProperty )
          {
            if( dgvcv.AutoFilterMode == AutoFilterMode.And )
            {
              whereClause += " AND ";
            }
            else
            {
              whereClause += " OR ";
            }
          }

          whereClause += " ( ";

          var filterFoundForItemProperty = false;

          for( int i = 0; i < filterValueCount; i++ )
          {
            if( filterFoundForItemProperty )
              whereClause += " OR ";

            filterFoundForOneItemProperty = true;
            filterFoundForItemProperty = true;

            var filterValue = itemAutoFilterValues[ i ];

            if( ( filterValue is string ) || ( filterValue is DateTime ) || ( filterValue is bool ) )
            {
              whereClause += itemPropertyName + " = '" + itemAutoFilterValues[ i ].ToString() + "'";
            }
            else
            {
              whereClause += itemPropertyName + " = " + itemAutoFilterValues[ i ].ToString();
            }
          }

          whereClause += " ) ";
        }
      }

      return ( filterFoundForOneItemProperty ? whereClause : string.Empty );
    }

    private static string BuildSortClause( DataGridVirtualizingCollectionView dgvcv, string pk )
    {
      var sort = "ORDER BY ";
      var sortFound = false;

      foreach( SortDescription sortDescription in dgvcv.SortDescriptions )
      {
        if( sortFound )
          sort += ", ";

        sortFound = true;

        sort += sortDescription.PropertyName;
        sort += ( sortDescription.Direction == ListSortDirection.Ascending ) ? " ASC" : " DESC";
      }

      if( !sortFound )
        sort += pk + " ASC";

      return sort;
    }

    #endregion

    public ADODataBridge( string connectionString, string tableName )
    {
      if( string.IsNullOrEmpty( connectionString ) )
        throw new ArgumentNullException( connectionString );

      if( string.IsNullOrEmpty( tableName ) )
        throw new ArgumentNullException( "tableName" );

      m_connectionString = connectionString;
      m_tableName = tableName;

      var connection = new SqlConnection( m_connectionString );
      connection.Open();
      try
      {
        // Fill a dataSet with the DataTable's schema.
        var dataAdapter = new SqlDataAdapter( "SELECT * FROM " + m_tableName, connection );
        var schema = new DataSet();
        dataAdapter.FillSchema( schema, SchemaType.Source );

        // Keep a copy of the DataTable's schema
        m_schema = schema.Tables[ 0 ];

        m_pk = m_schema.PrimaryKey[ 0 ].ColumnName;
      }
      finally
      {
        connection.Close();
      }
    }

    #region Schema Property

    public DataTable Schema
    {
      get
      {
        return m_schema;
      }
    }

    #endregion

    public void AbortReadCount( object sender, QueryItemCountEventArgs e )
    {
      lock( this )
      {
        var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;
        if( command == null )
          return;

        command.Cancel();
        Debug.WriteLine( "CANCELED SELECT STATEMENT USER SIDE !" );
      }
    }

    public void AbortReadData( object sender, QueryItemsEventArgs e )
    {
      lock( this )
      {
        var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;
        if( command == null )
          return;

        command.Cancel();
        Debug.WriteLine( "CANCELED SELECT STATEMENT USER SIDE !" );
      }
    }

    public void AbortReadDistinctValuesAndCounts( object sender, QueryGroupsEventArgs e )
    {
      lock( this )
      {
        var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;
        if( command == null )
          return;

        command.Cancel();
        Debug.WriteLine( "CANCELED SELECT STATEMENT USER SIDE !" );
      }
    }

    public void AbortReadDistinctValues( object sender, QueryAutoFilterDistinctValuesEventArgs e )
    {
      lock( this )
      {
        var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;
        if( command == null )
          return;

        command.Cancel();
        Debug.WriteLine( "CANCELED SELECT STATEMENT USER SIDE !" );
      }
    }

    public void AsyncReadCount( object sender, QueryItemCountEventArgs e )
    {
      var connection = new SqlConnection( m_connectionString );
      connection.Open();

      lock( this )
      {
        try
        {
          var query = ADODataBridge.BuildCountQuery( e.CollectionView, e.GroupPath, m_tableName );
          var command = new SqlCommand( query, connection );

          e.IsAsync = true;
          e.AsyncQueryInfo.AsyncState = command;

          command.BeginExecuteReader( this.EndReadCount, e, CommandBehavior.CloseConnection );
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
          connection.Close();
        }
      }
    }

    public void AsyncReadData( object sender, QueryItemsEventArgs e )
    {
      var startIndex = e.AsyncQueryInfo.StartIndex;
      var requestedItemCount = e.AsyncQueryInfo.RequestedItemCount;

      if( requestedItemCount == 0 )
      {
        e.IsAsync = false;
        e.AsyncQueryInfo.EndQuery( new object[ 0 ] );
        return;
      }

      var connection = new SqlConnection( m_connectionString );
      connection.Open();

      lock( this )
      {
        try
        {
          var query = ADODataBridge.BuildSelectQuery( e.CollectionView, e.GroupPath, startIndex, requestedItemCount, m_pk, m_tableName );
          var command = new SqlCommand( query, connection );

          e.IsAsync = true;
          e.AsyncQueryInfo.AsyncState = command;

          command.BeginExecuteReader( this.EndReadTablePage, e, CommandBehavior.CloseConnection );
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
          connection.Close();
        }
      }
    }

    public void AsyncReadDistinctValuesAndCounts( object sender, QueryGroupsEventArgs e )
    {
      var itemPropertyName = e.ChildGroupPropertyName;
      var connection = new SqlConnection( m_connectionString );
      connection.Open();

      lock( this )
      {
        try
        {
          var query = "SELECT " + itemPropertyName + ", COUNT( * ) FROM " + m_tableName;
          query = query + " " + ADODataBridge.BuildWhereClause( e.CollectionView, e.GroupPath );
          query += " GROUP BY " + itemPropertyName;

          if( e.ChildSortDirection == SortDirection.Descending )
          {
            query += " ORDER BY " + itemPropertyName + " DESC";
          }
          else if( e.ChildSortDirection == SortDirection.Ascending )
          {
            query += " ORDER BY " + itemPropertyName + " ASC";
          }

          var command = new SqlCommand( query, connection );

          e.IsAsync = true;
          e.AsyncQueryInfo.AsyncState = command;

          command.BeginExecuteReader( this.EndReadDistinctValuesAndCounts, e, CommandBehavior.CloseConnection );
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
          connection.Close();
        }
      }
    }

    public void AsyncReadDistinctValues( object sender, QueryAutoFilterDistinctValuesEventArgs e )
    {
      var itemPropertyName = e.ItemProperty.Name;
      var query = ADODataBridge.DefaultDistinctQuery.Replace(
        "%TABLE%", m_tableName ).Replace(
        "%FIELD%", itemPropertyName );

      var connection = new SqlConnection( m_connectionString );
      connection.Open();

      lock( this )
      {
        try
        {
          var command = new SqlCommand( query, connection );

          e.IsAsync = true;
          e.AsyncQueryInfo.AsyncState = command;

          command.BeginExecuteReader( this.EndReadDistinctValues, e, CommandBehavior.CloseConnection );
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
          connection.Close();
        }
      }
    }

    public void AsyncCommitData( object sender, CommitItemsEventArgs e )
    {
      var virtualizedItemInfos = e.AsyncCommitInfo.VirtualizedItemInfos;
      var connection = default( SqlConnection );

      try
      {
        e.IsAsync = true;
        e.AsyncCommitInfo.AsyncState = virtualizedItemInfos.Length;

        for( int i = 0; i < virtualizedItemInfos.Length; i++ )
        {
          var virtualizedItemInfo = virtualizedItemInfos[ i ];
          var query = ADODataBridge.BuildUpdateQuery( e.CollectionView as DataGridVirtualizingCollectionView, m_tableName, m_pk, virtualizedItemInfos[ i ] );

          connection = new SqlConnection( m_connectionString );
          connection.Open();

          var updateCommand = new SqlCommand( query, connection );
          updateCommand.BeginExecuteNonQuery( new AsyncCallback( this.EndUpdateTable ), new object[] { e, connection } );
        }
      }
      catch( Exception exception )
      {
        e.AsyncCommitInfo.Error = exception;

        if( connection != null )
        {
          connection.Close();
        }
      }
    }

    private void EndReadCount( IAsyncResult asyncResult )
    {
      var e = ( QueryItemCountEventArgs )asyncResult.AsyncState;
      var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;

      if( e.AsyncQueryInfo.ShouldAbort )
      {
        command.Connection.Close();
        return;
      }

      var count = 0;

      lock( this )
      {
        try
        {
          // EndExecuteReader will close the command's connection.
          using( var reader = command.EndExecuteReader( asyncResult ) )
          {
            if( ( reader.FieldCount > 0 ) && reader.Read() )
            {
              count = reader.GetInt32( 0 );
            }
          }
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
        }
      }

      e.AsyncQueryInfo.AsyncState = null;
      e.AsyncQueryInfo.EndQuery( count );
    }

    private void EndReadTablePage( IAsyncResult asyncResult )
    {
      var e = ( QueryItemsEventArgs )asyncResult.AsyncState;
      var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;

      if( e.AsyncQueryInfo.ShouldAbort )
      {
        command.Connection.Close();
        return;
      }

      var rows = default( object[] );

      lock( this )
      {
        try
        {
          // EndExecuteReader will close the command's connection.
          using( var reader = command.EndExecuteReader( asyncResult ) )
          {
            var table = new DataTable();
            table.Load( reader );
            rows = new object[ table.Rows.Count ];
            table.DefaultView.CopyTo( rows, 0 );
          }
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
        }
      }

      e.AsyncQueryInfo.AsyncState = null;
      e.AsyncQueryInfo.EndQuery( rows );
    }

    private void EndReadDistinctValuesAndCounts( IAsyncResult asyncResult )
    {
      var e = ( QueryGroupsEventArgs )asyncResult.AsyncState;
      var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;

      if( e.AsyncQueryInfo.ShouldAbort )
      {
        command.Connection.Close();
        return;
      }

      var pairs = new List<GroupNameCountPair>();

      lock( this )
      {
        try
        {
          // EndExecuteReader will close the command's connection.
          using( var reader = command.EndExecuteReader( asyncResult ) )
          {
            var table = new DataTable();
            table.Load( reader );

            var distinctValueCount = table.Rows.Count;

            for( int i = 0; i < distinctValueCount; i++ )
            {
              pairs.Add( new GroupNameCountPair( table.Rows[ i ][ 0 ], ( int )table.Rows[ i ][ 1 ] ) );
            }
          }
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
        }
      }

      e.AsyncQueryInfo.AsyncState = null;
      e.AsyncQueryInfo.EndQuery( pairs );
    }

    private void EndReadDistinctValues( IAsyncResult asyncResult )
    {
      var e = ( QueryAutoFilterDistinctValuesEventArgs )asyncResult.AsyncState;
      var command = ( SqlCommand )e.AsyncQueryInfo.AsyncState;

      if( e.AsyncQueryInfo.ShouldAbort )
      {
        command.Connection.Close();
        return;
      }

      var values = new List<object>();

      lock( this )
      {
        try
        {
          // EndExecuteReader will close the command's connection.
          using( var reader = command.EndExecuteReader( asyncResult ) )
          {
            var table = new DataTable();
            table.Load( reader );

            var distinctValueCount = table.Rows.Count;

            for( int i = 0; i < distinctValueCount; i++ )
            {
              values.Add( table.Rows[ i ][ e.ItemProperty.Name ] );
            }
          }
        }
        catch( Exception exception )
        {
          e.AsyncQueryInfo.Error = exception;
        }
      }

      e.AsyncQueryInfo.AsyncState = null;
      e.AsyncQueryInfo.EndQuery( values );
    }

    private void EndUpdateTable( IAsyncResult asyncResult )
    {
      var parameters = ( object[] )asyncResult.AsyncState;
      var e = ( CommitItemsEventArgs )parameters[ 0 ];
      var connection = ( SqlConnection )parameters[ 1 ];

      connection.Close();

      lock( this )
      {
        var updateCommandCount = ( int )e.AsyncCommitInfo.AsyncState;

        updateCommandCount--;

        if( updateCommandCount == 0 )
        {
          e.AsyncCommitInfo.EndCommit();
        }

        e.AsyncCommitInfo.AsyncState = updateCommandCount;
      }
    }

    private string m_connectionString;
    private string m_tableName;
    private DataTable m_schema;
    private string m_pk;
  }
}
