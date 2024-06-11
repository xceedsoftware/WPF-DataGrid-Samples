' 
' * Xceed DataGrid for WPF - SAMPLE CODE - Data Virtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [ADODataBridge.vb] 
' * 
' * This class implements a dynamic SQL query generator for the 
' * purpose of providing data to the DataGridVirtualizingCollectionView. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' * 
'

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  Public Class ADODataBridge
#Region "Static Members"

    Private Const DefaultSortFilterQuery As String = "SELECT * FROM %TABLE% WHERE %PK% IN (SELECT %PK% FROM (SELECT ROW_NUMBER() OVER (%SORT CLAUSE%) AS ROWID, %PK% FROM %TABLE% %WHERE CLAUSE%)AS INNER_RESULT WHERE INNER_RESULT.ROWID BETWEEN %STARTROWID% AND %ENDROWID%)%SORT CLAUSE%"

    Private Const DefaultDistinctQuery As String = "SELECT DISTINCT( %FIELD% ) FROM %TABLE%"

    Private Const DefaultCountQuery As String = "SELECT COUNT( * ) FROM %TABLE% %WHERE CLAUSE%"

    Private Const DefaultUpdateQuery As String = "UPDATE %TABLE% SET %UPDATE CLAUSE% %WHERE CLAUSE%"

    Private Shared Function BuildCountQuery(ByVal dgvcv As DataGridVirtualizingCollectionView, ByVal groupPath As ReadOnlyCollection(Of DataGridGroupInfo), ByVal tableName As String) As String
      Dim whereClause As String = ADODataBridge.BuildWhereClause(dgvcv, groupPath)

      Dim query As String = DefaultCountQuery.Replace("%TABLE%", tableName).Replace("%WHERE CLAUSE%", whereClause).TrimEnd(" "c)

      Return query
    End Function

    Private Shared Function BuildSelectQuery(ByVal dgvcv As DataGridVirtualizingCollectionView, ByVal groupPath As ReadOnlyCollection(Of DataGridGroupInfo), ByVal pageStartIndex As Integer, ByVal count As Integer, ByVal pk As String, ByVal tableName As String) As String
      Dim query As String

      Dim startRowID As Integer = pageStartIndex + 1
      Dim endRowID As Integer = startRowID + count - 1

      query = ADODataBridge.DefaultSortFilterQuery.Replace("%PK%", pk).Replace("%TABLE%", tableName).Replace("%STARTROWID%", startRowID.ToString()).Replace("%ENDROWID%", endRowID.ToString())

      Dim whereClause As String = ADODataBridge.BuildWhereClause(dgvcv, groupPath)
      Dim sortClause As String = ADODataBridge.BuildSortClause(dgvcv, pk)

      query = query.Replace("%SORT CLAUSE%", sortClause)
      query = query.Replace("%WHERE CLAUSE%", whereClause)

      Return query
    End Function

    Private Shared Function BuildUpdateQuery(ByVal dgvcv As DataGridVirtualizingCollectionView, ByVal tableName As String, ByVal pk As String, ByVal virtualizedItemInfo As VirtualizedItemInfo) As String
      Dim query As String = ADODataBridge.DefaultUpdateQuery.Replace("%TABLE%", tableName)

      Dim dataRowView As System.Data.DataRowView = TryCast(virtualizedItemInfo.DataItem, System.Data.DataRowView)

      Dim updateClauseBuilder As New StringBuilder()

      For Each itemProperty As DataGridItemProperty In dgvcv.ItemProperties
        If itemProperty.IsReadOnly Then
          Continue For
        End If

        Dim fieldName As String = itemProperty.Name

        Dim newValue As Object = dataRowView(fieldName)
        Dim oldValue As Object = virtualizedItemInfo.OldValues(itemProperty.Name)

        If Object.Equals(newValue, oldValue) Then
          Continue For
        End If

        If updateClauseBuilder.Length <> 0 Then
          updateClauseBuilder.Append(", ")
        End If

        If (TypeOf newValue Is String) OrElse (TypeOf newValue Is DateTime) OrElse (TypeOf newValue Is Boolean) Then
          updateClauseBuilder.Append((fieldName & " = '") + newValue.ToString() & "'")
        Else
          updateClauseBuilder.Append((fieldName & " = ") + newValue.ToString())
        End If
      Next

      updateClauseBuilder.Append(" "c)

      query = query.Replace("%UPDATE CLAUSE%", updateClauseBuilder.ToString())

      query = query.Replace("%WHERE CLAUSE%", "WHERE " & ((pk & " = ") & virtualizedItemInfo.OldValues(pk)))

      Return query
    End Function

    Private Shared Function BuildWhereClause(ByVal dgvcv As DataGridVirtualizingCollectionView, ByVal groupPath As ReadOnlyCollection(Of DataGridGroupInfo)) As String
      Dim filterFoundForOneItemProperty As Boolean = False
      Dim whereClause As String = "WHERE "

      Dim groupLevel As Integer = groupPath.Count
      For i As Integer = 0 To groupLevel - 1
        If filterFoundForOneItemProperty Then
          whereClause += " AND "
        End If

        filterFoundForOneItemProperty = True

        whereClause += " ( "

        Dim itemPropertyName As String = groupPath(i).PropertyName
        Dim filterValue As Object = groupPath(i).Value

        If (TypeOf filterValue Is String) OrElse (TypeOf filterValue Is DateTime) OrElse (TypeOf filterValue Is Boolean) Then
          whereClause += (itemPropertyName & " = '") + filterValue.ToString() & "'"
        Else
          whereClause += (itemPropertyName & " = ") + filterValue.ToString()
        End If

        whereClause += " ) "
      Next


      For Each itemProperty As DataGridItemProperty In dgvcv.ItemProperties
        Dim itemPropertyName As String = itemProperty.Name
        Dim itemAutoFilterValues As IList = Nothing
        If dgvcv.AutoFilterValues.TryGetValue(itemProperty.Name, itemAutoFilterValues) Then
          Dim filterValueCount As Integer = itemAutoFilterValues.Count

          If filterValueCount = 0 Then
            Continue For
          End If

          If filterFoundForOneItemProperty Then
            If dgvcv.AutoFilterMode = AutoFilterMode.[And] Then
              whereClause += " AND "
            Else
              whereClause += " OR "
            End If
          End If

          whereClause += " ( "

          Dim filterFoundForItemProperty As Boolean = False

          For i As Integer = 0 To filterValueCount - 1
            If filterFoundForItemProperty Then
              whereClause += " OR "
            End If

            filterFoundForOneItemProperty = True
            filterFoundForItemProperty = True

            Dim filterValue As Object = itemAutoFilterValues(i)

            If (TypeOf filterValue Is String) OrElse (TypeOf filterValue Is DateTime) OrElse (TypeOf filterValue Is Boolean) Then
              whereClause += (itemPropertyName & " = '") + itemAutoFilterValues(i).ToString() & "'"
            Else
              whereClause += (itemPropertyName & " = ") + itemAutoFilterValues(i).ToString()
            End If
          Next

          whereClause += " ) "
        End If
      Next

      Return (If(filterFoundForOneItemProperty, whereClause, String.Empty))
    End Function

    Private Shared Function BuildSortClause(ByVal dgvcv As DataGridVirtualizingCollectionView, ByVal pk As String) As String
      Dim sort As String = "ORDER BY "

      Dim sortFound As Boolean = False

      For Each sortDescription As SortDescription In dgvcv.SortDescriptions
        If sortFound Then
          sort += ", "
        End If

        sortFound = True

        sort += sortDescription.PropertyName
        sort += If((sortDescription.Direction = ListSortDirection.Ascending), " ASC", " DESC")
      Next

      If Not sortFound Then
        sort += pk & " ASC"
      End If

      Return sort
    End Function

#End Region

    Public Sub New(ByVal connectionString As String, ByVal tableName As String)
      If [String].IsNullOrEmpty(connectionString) Then
        Throw New ArgumentNullException(connectionString)
      End If

      If [String].IsNullOrEmpty(tableName) Then
        Throw New ArgumentNullException("tableName")
      End If

      m_connectionString = connectionString
      m_tableName = tableName

      Dim connection As New SqlConnection(m_connectionString)
      connection.Open()
      Try
        ' Fill a dataSet with the DataTable's schema.
        Dim dataAdapter As New SqlDataAdapter("SELECT * FROM " & m_tableName, connection)

        Dim schema As New DataSet()
        dataAdapter.FillSchema(schema, SchemaType.Source)

        ' Keep a copy of the DataTable's schema
        m_schema = schema.Tables(0)

        m_pk = m_schema.PrimaryKey(0).ColumnName
      Finally
        connection.Close()
      End Try
    End Sub


#Region "Schema Property"

    Public ReadOnly Property Schema() As DataTable
      Get
        Return m_schema
      End Get
    End Property

#End Region

    Public Sub AbortReadCount(ByVal sender As Object, ByVal e As QueryItemCountEventArgs)
      SyncLock Me
        Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

        If command IsNot Nothing Then
          command.Cancel()
          Debug.WriteLine("CANCELED SELECT STATEMENT USER SIDE !")
        End If
      End SyncLock
    End Sub

    Public Sub AbortReadData(ByVal sender As Object, ByVal e As QueryItemsEventArgs)
      SyncLock Me
        Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

        If command IsNot Nothing Then
          command.Cancel()
          Debug.WriteLine("CANCELED SELECT STATEMENT USER SIDE !")
        End If
      End SyncLock
    End Sub

    Public Sub AbortReadDistinctValuesAndCounts(ByVal sender As Object, ByVal e As QueryGroupsEventArgs)
      SyncLock Me
        Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

        If command IsNot Nothing Then
          command.Cancel()
          Debug.WriteLine("CANCELED SELECT STATEMENT USER SIDE !")
        End If
      End SyncLock
    End Sub

    Public Sub AbortReadDistinctValues(ByVal sender As Object, ByVal e As QueryAutoFilterDistinctValuesEventArgs)
      SyncLock Me
        Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

        If command IsNot Nothing Then
          command.Cancel()
          Debug.WriteLine("CANCELED SELECT STATEMENT USER SIDE !")
        End If
      End SyncLock
    End Sub

    Public Sub AsyncReadCount(ByVal sender As Object, ByVal e As QueryItemCountEventArgs)
      Dim connection As New SqlConnection(m_connectionString)
      connection.Open()

      SyncLock Me
        Try
          Dim query As String = ADODataBridge.BuildCountQuery(e.CollectionView, e.GroupPath, m_tableName)
          Dim command As New SqlCommand(query, connection)

          e.IsAsync = True
          e.AsyncQueryInfo.AsyncState = command

          command.BeginExecuteReader(AddressOf Me.EndReadCount, e, CommandBehavior.CloseConnection)
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
          connection.Close()
        End Try
      End SyncLock
    End Sub

    Public Sub AsyncReadData(ByVal sender As Object, ByVal e As QueryItemsEventArgs)
      Dim startIndex As Integer = e.AsyncQueryInfo.StartIndex
      Dim requestedItemCount As Integer = e.AsyncQueryInfo.RequestedItemCount

      If requestedItemCount = 0 Then
        Dim retval As Object() = New Object(0) {}

        e.IsAsync = False
        e.AsyncQueryInfo.EndQuery(retval)
        Exit Sub
      End If

      Dim connection As New SqlConnection(m_connectionString)
      connection.Open()

      SyncLock Me
        Try
          Dim query As String = ADODataBridge.BuildSelectQuery(e.CollectionView, e.GroupPath, startIndex, requestedItemCount, m_pk, m_tableName)
          Dim command As New SqlCommand(query, connection)

          e.IsAsync = True
          e.AsyncQueryInfo.AsyncState = command

          command.BeginExecuteReader(AddressOf Me.EndReadTablePage, e, CommandBehavior.CloseConnection)
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
          connection.Close()
        End Try
      End SyncLock
    End Sub

    Public Sub AsyncReadDistinctValuesAndCounts(ByVal sender As Object, ByVal e As QueryGroupsEventArgs)
      Dim itemPropertyName As String = e.ChildGroupPropertyName
      Dim connection As New SqlConnection(m_connectionString)
      connection.Open()

      SyncLock Me
        Try
          Dim query As String = ("SELECT " & itemPropertyName & ", COUNT( * ) FROM ") + m_tableName

          query = (query & " ") + ADODataBridge.BuildWhereClause(e.CollectionView, e.GroupPath)
          query += " GROUP BY " & itemPropertyName

          If e.ChildSortDirection = SortDirection.Descending Then
            query += " ORDER BY " & itemPropertyName & " DESC"
          ElseIf e.ChildSortDirection = SortDirection.Ascending Then
            query += " ORDER BY " & itemPropertyName & " ASC"
          End If

          Dim command As New SqlCommand(query, connection)

          e.IsAsync = True
          e.AsyncQueryInfo.AsyncState = command

          command.BeginExecuteReader(AddressOf Me.EndReadDistinctValuesAndCounts, e, CommandBehavior.CloseConnection)

          'Using reader As SqlDataReader = command.ExecuteReader()
          '  Dim table As New DataTable()
          '  table.Load(reader)

          '  Dim distinctValueCount As Integer = table.Rows.Count

          '  For i As Integer = 0 To distinctValueCount - 1
          '    e.ChildGroupNameCountPairs.Add(New GroupNameCountPair(table.Rows(i)(0), CInt(table.Rows(i)(1))))
          '  Next
          'End Using
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
          connection.Close()
        End Try
      End SyncLock
    End Sub

    Public Sub AsyncReadDistinctValues(ByVal sender As Object, ByVal e As QueryAutoFilterDistinctValuesEventArgs)
      Dim itemPropertyName As String = e.ItemProperty.Name
      Dim query As String = ADODataBridge.DefaultDistinctQuery.Replace("%TABLE%", m_tableName).Replace("%FIELD%", itemPropertyName)
      Dim connection As New SqlConnection(m_connectionString)
      connection.Open()

      SyncLock Me
        Try
          Dim command As New SqlCommand(query, connection)

          e.IsAsync = True
          e.AsyncQueryInfo.AsyncState = command

          command.BeginExecuteReader(AddressOf Me.EndReadDistinctValues, e, CommandBehavior.CloseConnection)
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
          connection.Close()
        End Try
      End SyncLock

      'Dim distinctValues As Object()

      'Dim connection As New SqlConnection(m_connectionString)
      'connection.Open()
      'Try
      '  Dim command As New SqlCommand(query, connection)

      '  Using reader As SqlDataReader = command.ExecuteReader()
      '    Dim table As New DataTable()
      '    table.Load(reader)
      '    distinctValues = New Object(table.Rows.Count - 1) {}

      '    For i As Integer = 0 To distinctValues.Length - 1
      '      e.DistinctValues.Add(table.Rows(i)(itemPropertyName))
      '    Next
      '  End Using
      'Catch
      '  e.DistinctValues.Clear()
      'Finally
      '  connection.Close()
      'End Try
    End Sub

    Public Sub AsyncCommitData(ByVal sender As Object, ByVal e As CommitItemsEventArgs)
      Dim virtualizedItemInfos As VirtualizedItemInfo() = e.AsyncCommitInfo.VirtualizedItemInfos
      Dim connection As SqlConnection = Nothing

      Try
        e.IsAsync = True
        e.AsyncCommitInfo.AsyncState = virtualizedItemInfos.Length

        For i As Integer = 0 To virtualizedItemInfos.Length - 1
          Dim virtualizedItemInfo As VirtualizedItemInfo = virtualizedItemInfos(i)

          Dim query As String = ADODataBridge.BuildUpdateQuery(TryCast(e.CollectionView, DataGridVirtualizingCollectionView), m_tableName, m_pk, virtualizedItemInfos(i))

          connection = New SqlConnection(m_connectionString)
          connection.Open()

          Dim updateCommand As New SqlCommand(query, connection)
          updateCommand.BeginExecuteNonQuery(New AsyncCallback(AddressOf Me.EndUpdateTable), New Object() {e, connection})
        Next
      Catch exception As Exception
        e.AsyncCommitInfo.Error = exception

        If connection IsNot Nothing Then
          connection.Close()
        End If
      End Try
    End Sub

    Private Sub EndReadCount(ByVal asyncResult As IAsyncResult)
      Dim e As QueryItemCountEventArgs = DirectCast(asyncResult.AsyncState, QueryItemCountEventArgs)
      Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

      If e.AsyncQueryInfo.ShouldAbort Then
        command.Connection.Close()
        Exit Sub
      End If

      Dim count As Integer = 0

      SyncLock Me
        Try
          ' EndExecuteReader will close the command's connection.
          Using reader As SqlDataReader = command.EndExecuteReader(asyncResult)
            If (reader.FieldCount > 0) And reader.Read() Then
              count = reader.GetInt32(0)
            End If
          End Using
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
        End Try
      End SyncLock

      e.AsyncQueryInfo.AsyncState = Nothing
      e.AsyncQueryInfo.EndQuery(count)
    End Sub

    Private Sub EndReadTablePage(ByVal asyncResult As IAsyncResult)
      Dim e As QueryItemsEventArgs = DirectCast(asyncResult.AsyncState, QueryItemsEventArgs)
      Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

      If e.AsyncQueryInfo.ShouldAbort Then
        command.Connection.Close()
        Exit Sub
      End If

      Dim rows As Object() = Nothing

      SyncLock Me
        Try
          ' EndExecuteReader will close the command's connection.
          Using reader As SqlDataReader = command.EndExecuteReader(asyncResult)
            Dim table As New DataTable()
            table.Load(reader)
            rows = New Object(table.Rows.Count - 1) {}
            table.DefaultView.CopyTo(rows, 0)
          End Using
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
        End Try
      End SyncLock

      e.AsyncQueryInfo.AsyncState = Nothing
      e.AsyncQueryInfo.EndQuery(rows)
    End Sub

    Private Sub EndReadDistinctValuesAndCounts(ByVal asyncResult As IAsyncResult)
      Dim e As QueryGroupsEventArgs = DirectCast(asyncResult.AsyncState, QueryGroupsEventArgs)
      Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

      If e.AsyncQueryInfo.ShouldAbort Then
        command.Connection.Close()
        Exit Sub
      End If

      Dim pairs As List(Of GroupNameCountPair) = New List(Of GroupNameCountPair)()

      SyncLock Me
        Try
          ' EndExecuteReader will close the command's connection.
          Using reader As SqlDataReader = command.EndExecuteReader(asyncResult)
            Dim table As New DataTable()
            table.Load(reader)

            Dim distinctValueCount As Integer = table.Rows.Count

            For i As Integer = 0 To distinctValueCount - 1
              pairs.Add(New GroupNameCountPair(table.Rows(i)(0), CInt(table.Rows(i)(1))))
            Next
          End Using
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
        End Try
      End SyncLock

      e.AsyncQueryInfo.AsyncState = Nothing
      e.AsyncQueryInfo.EndQuery(pairs)
    End Sub

    Private Sub EndReadDistinctValues(ByVal asyncResult As IAsyncResult)
      Dim e As QueryAutoFilterDistinctValuesEventArgs = DirectCast(asyncResult.AsyncState, QueryAutoFilterDistinctValuesEventArgs)
      Dim command As SqlCommand = DirectCast(e.AsyncQueryInfo.AsyncState, SqlCommand)

      If e.AsyncQueryInfo.ShouldAbort Then
        command.Connection.Close()
        Exit Sub
      End If

      Dim values As List(Of Object) = New List(Of Object)()

      SyncLock Me
        Try
          ' EndExecuteReader will close the command's connection.
          Using reader As SqlDataReader = command.EndExecuteReader(asyncResult)
            Dim table As New DataTable()
            table.Load(reader)

            Dim distinctValueCount As Integer = table.Rows.Count

            For i As Integer = 0 To distinctValueCount - 1
              values.Add(table.Rows(i)(e.ItemProperty.Name))
            Next
          End Using
        Catch exception As Exception
          e.AsyncQueryInfo.Error = exception
        End Try
      End SyncLock

      e.AsyncQueryInfo.AsyncState = Nothing
      e.AsyncQueryInfo.EndQuery(values)
    End Sub

    Private Sub EndUpdateTable(ByVal asyncResult As IAsyncResult)
      Dim parameters As Object() = DirectCast(asyncResult.AsyncState, Object())
      Dim e As CommitItemsEventArgs = DirectCast(parameters(0), CommitItemsEventArgs)
      Dim connection As SqlConnection = DirectCast(parameters(1), SqlConnection)

      connection.Close()

      SyncLock Me
        Dim updateCommandCount As Integer = CInt(e.AsyncCommitInfo.AsyncState)

        updateCommandCount -= 1

        If updateCommandCount = 0 Then
          e.AsyncCommitInfo.EndCommit()
        End If

        e.AsyncCommitInfo.AsyncState = updateCommandCount
      End SyncLock
    End Sub

    Private m_connectionString As String
    Private m_tableName As String
    Private m_schema As DataTable
    Private m_pk As String

  End Class
End Namespace
