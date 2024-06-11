' 
' * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [DatabaseInfo.vb] 
' * 
' * This class is used to configure a sql connection and 
' * provides auto creation of data used by the DataVirtualization Sample. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' 


Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Windows.Threading
Imports System.Xml.Serialization

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  <Serializable()> _
  Public Class DatabaseInfo
    Implements INotifyPropertyChanged
    Public Sub New()
      Me.ServerName = "localhost"
      Me.DatabaseName = "XCEEDTEST"
      Me.TableName = "Persons"
      Me.RecordCount = 10000
      Me.UseIntegratedSecurity = True
    End Sub


    Public Property ServerName() As String
      Get
        Return m_serverName
      End Get
      Set(ByVal value As String)
        If Not String.Equals(m_serverName, value) Then
          m_serverName = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("ServerName"))
        End If
      End Set
    End Property

    <XmlIgnore()> _
    Public Property DatabaseName() As String
      Get
        Return m_databaseName
      End Get
      Private Set(ByVal value As String)
        If Not String.Equals(m_databaseName, value) Then
          m_databaseName = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("DatabaseName"))
        End If
      End Set
    End Property

    Public Property TableName() As String
      Get
        Return m_tableName
      End Get
      Set(ByVal value As String)
        If Not String.Equals(m_tableName, value) Then
          m_tableName = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("TableName"))
        End If
      End Set
    End Property

    Public Property RecordCount() As Integer
      Get
        Return m_recordCount
      End Get
      Set(ByVal value As Integer)
        If m_recordCount <> value Then
          m_recordCount = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("RecordCount"))
        End If
      End Set
    End Property

    Public Property UseIntegratedSecurity() As Boolean
      Get
        Return m_useIntegratedSecurity
      End Get
      Set(ByVal value As Boolean)
        If m_useIntegratedSecurity <> value Then
          m_useIntegratedSecurity = value
          Me.OnPropertyChanged(New PropertyChangedEventArgs("UseIntegratedSecurity"))
        End If
      End Set
    End Property

    Public Property Username() As String
      Get
        Return m_username
      End Get
      Set(ByVal value As String)
        m_username = value
      End Set
    End Property


#Region "DATABASE TOOLS"

    Private Function GetRootConnectionString() As String
      Return Me.GetConnectionString("MASTER")
    End Function

    Friend Function GetConnectionString() As String
      Return Me.GetConnectionString(m_databaseName)
    End Function

    Private Function GetConnectionString(ByVal initialCatalog As String) As String
      Dim connectionString As String = ("Data Source=" & m_serverName & ";Initial Catalog=") + initialCatalog

      If m_useIntegratedSecurity Then
        connectionString += ";Integrated Security=True"
      Else
        If String.IsNullOrEmpty(m_password) Then
          m_password = m_readPasswordDelegate.Invoke()
        End If

        connectionString += (";User ID = " & m_username & ";Password = ") + m_password
      End If

#If (Not NETCORE) Then
      ' Needed for async SqlCommand execution, which are needed for optimal data virtualization. 
      connectionString += ";Asynchronous Processing=true"
#End If

      Return connectionString
    End Function

    Friend Sub EnsureDatabase()
      Using sqlConnection As New SqlConnection(Me.GetRootConnectionString())
        sqlConnection.Open()

        Try
          Dim sqlCommand As New SqlCommand(("IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '" & m_databaseName & "') " & "CREATE DATABASE ") + m_databaseName, sqlConnection)

          sqlCommand.ExecuteNonQuery()
        Finally
          sqlConnection.Close()
        End Try
      End Using
    End Sub

    Friend Sub DropExistingTable()
      Using sqlConnection As SqlConnection = Me.CreateSqlConnection()
        sqlConnection.Open()

        Try
          Dim sqlCommand As New SqlCommand(("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" & m_tableName & "]') AND type in (N'U')) " & "DROP TABLE [dbo].[") + m_tableName & "]", sqlConnection)

          sqlCommand.ExecuteNonQuery()
        Finally
          sqlConnection.Close()
        End Try
      End Using
    End Sub

    Friend Sub CreateTable()
      Using sqlConnection As SqlConnection = Me.CreateSqlConnection()
        Try
          sqlConnection.Open()

          Dim sqlCommand As New SqlCommand(("CREATE TABLE [dbo].[" & m_tableName & "](" & "[PK] [int] NOT NULL, " & "[FirstName] [varchar](50) NOT NULL, " & "[LastName] [varchar](50) NOT NULL, " & "[Age] [int] NOT NULL, " & "[Children] [int] NOT NULL, " & "[PhoneNumber] [varchar](50) NULL, " & "[Employed] [bit] NOT NULL, " & "[Department] [varchar](50) NOT NULL, " & "[Boss] [varchar](50) NULL, " & "[HireDate] [datetime] NOT NULL, " & "[ReviewDate] [datetime] NULL, " & "CONSTRAINT [PK_") + m_tableName & "] PRIMARY KEY CLUSTERED " & "(" & "[PK] ASC " & ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]", sqlConnection)

          sqlCommand.ExecuteNonQuery()
        Finally
          sqlConnection.Close()
        End Try
      End Using
    End Sub

    Friend Sub CreateRows(ByVal dispatcher As Dispatcher, ByVal progressCallback As Action(Of Integer), ByVal cancelCallback As Action)
      Using sqlConnection As SqlConnection = Me.CreateSqlConnection()
        sqlConnection.Open()
        Try
          For i As Integer = 0 To m_recordCount - 1
            If m_shouldCancelCreateData Then
              Exit For
            End If

            dispatcher.Invoke(DispatcherPriority.Render, progressCallback, i)
            dispatcher.Invoke(DispatcherPriority.Background, New Action(Of Integer, SqlConnection)(AddressOf Me.AddPerson), i, sqlConnection)
          Next
        Finally
          If m_shouldCancelCreateData Then
            m_shouldCancelCreateData = False
            dispatcher.Invoke(DispatcherPriority.Background, cancelCallback)
          End If

          sqlConnection.Close()
        End Try
      End Using
    End Sub

    Friend Sub CreateIndexes(ByVal dispatcher As Dispatcher, ByVal progressCallback As Action(Of Integer), ByVal cancelCallback As Action)
      Dim nonClusteredIndexScript As String = ("CREATE NONCLUSTERED INDEX [IX_" & m_tableName & "_%ColumnName%] ON [dbo].[") + m_tableName & "] " & "(" & " [%ColumnName%] ASC " & ")" & "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, " & "DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"

      Using sqlConnection As SqlConnection = Me.CreateSqlConnection()
        Try
          sqlConnection.Open()

          Dim sqlCommand As SqlCommand = Nothing
          For i As Integer = 1 To 10
            Select Case i
              Case 1
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "Age"), sqlConnection)
                  Exit Select
                End If

              Case 2
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "Boss"), sqlConnection)
                  Exit Select
                End If

              Case 3
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "Children"), sqlConnection)
                  Exit Select
                End If

              Case 4
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "Department"), sqlConnection)
                  Exit Select
                End If

              Case 5
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "Employed"), sqlConnection)
                  Exit Select
                End If

              Case 6
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "FirstName"), sqlConnection)
                  Exit Select
                End If

              Case 7
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "HireDate"), sqlConnection)
                  Exit Select
                End If

              Case 8
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "LastName"), sqlConnection)
                  Exit Select
                End If

              Case 9
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "PhoneNumber"), sqlConnection)
                  Exit Select
                End If

              Case 10
                If True Then
                  sqlCommand = New SqlCommand(nonClusteredIndexScript.Replace("%ColumnName%", "ReviewDate"), sqlConnection)
                  Exit Select
                End If
            End Select

            dispatcher.Invoke(DispatcherPriority.Render, progressCallback, (Convert.ToInt32(m_recordCount * (Convert.ToDouble(i) / 10.0R))))
            dispatcher.Invoke(DispatcherPriority.Background, New Func(Of Integer)(AddressOf sqlCommand.ExecuteNonQuery))

            If m_shouldCancelCreateData Then
              m_shouldCancelCreateData = False
              dispatcher.Invoke(DispatcherPriority.Background, cancelCallback)
              Exit Sub
            End If
          Next
        Finally
          sqlConnection.Close()
        End Try
      End Using
    End Sub


    Friend Sub AddPerson(ByVal i As Integer, ByVal sqlConnection As SqlConnection)
      Dim person As New Person()

      Dim PK As Integer = i + 1
      Dim employed As String = If((person.Employed), "TRUE", "FALSE")

      Dim sqlCommand As New SqlCommand(((((((((((("INSERT INTO " & m_tableName & " VALUES( ") + PK.ToString() & "," & "'") + person.FirstName & "'," & "'") + person.LastName & "',") + person.Age.ToString() & ",") + person.Children.ToString() & "," & "'") + person.PhoneNumber.ToString() & "'," & "'") + employed & "'," & "'") + person.Department & "'," & "'") + person.Boss & "'," & "'") + person.HireDate.ToString("d", CultureInfo.InvariantCulture.DateTimeFormat) & "'," & "'") + person.ReviewDate.ToString("d", CultureInfo.InvariantCulture.DateTimeFormat) & "')", sqlConnection)

      sqlCommand.ExecuteNonQuery()
    End Sub

    Friend Sub CancelCreateData()
      m_shouldCancelCreateData = True
    End Sub

    Friend Sub SetReadPasswordDelegate(ByVal readPasswordDelegate As Func(Of String))
      m_readPasswordDelegate = readPasswordDelegate
    End Sub


    Friend Sub TestConnection()
      Dim connection As New SqlConnection(Me.GetRootConnectionString())

      connection.Open()
      connection.Close()
    End Sub

    Friend Sub CheckExistance()
      Dim connection As New SqlConnection(Me.GetConnectionString())
      connection.Open()
      Try
        Dim countQuery As String = "SELECT COUNT( * ) FROM " & m_tableName

        Dim command As New SqlCommand(countQuery, connection)
        command.ExecuteScalar()
      Finally
        connection.Close()
      End Try
    End Sub

    Private Function CreateSqlConnection() As SqlConnection
      Return New SqlConnection(Me.GetConnectionString())
    End Function

#End Region


#Region "PRIVATE FIELDS"

    Private m_serverName As String
    Private m_databaseName As String
    Private m_tableName As String

    Private m_recordCount As Integer

    Private m_useIntegratedSecurity As Boolean

    Private m_username As String
    Private m_password As String

    Private m_shouldCancelCreateData As Boolean

    Private m_readPasswordDelegate As Func(Of String)

#End Region

#Region "INotifyPropertyChanged Members"

    Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
      RaiseEvent PropertyChanged(Me, e)
    End Sub

    Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

  End Class


End Namespace
