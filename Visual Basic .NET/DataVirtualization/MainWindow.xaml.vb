' 
' * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [MainWindow.xaml.vb] 
' * 
' * This class implements the various dynamic configuration options offered 
' * by the configuration panel declared in MainWindow.xaml. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' 


Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  Partial Public Class MainWindow
    Inherits Window
    Public Sub New()
      Me.DataContext = Me
      InitializeComponent()

      Dim connectionInfo As DatabaseInfo = App.ReadDatabaseInfo()

      If connectionInfo IsNot Nothing Then
        Me.DatabaseInfo = connectionInfo
      Else
        Me.DatabaseInfo = New DatabaseInfo()
      End If

      Me.DatabaseInfo.SetReadPasswordDelegate(New Func(Of String)(Function() passwordBox.Password))
    End Sub


#Region "DatabaseInfo Dependency Property"

    Public Property DatabaseInfo() As DatabaseInfo
      Get
        Return DirectCast(GetValue(DatabaseInfoProperty), DatabaseInfo)
      End Get
      Set(ByVal value As DatabaseInfo)
        SetValue(DatabaseInfoProperty, value)
      End Set
    End Property

    Public Shared ReadOnly DatabaseInfoProperty As DependencyProperty = DependencyProperty.Register("DatabaseInfo", GetType(DatabaseInfo), GetType(MainWindow), New UIPropertyMetadata(Nothing))

#End Region

#Region "IsLaunched Read-Only Dependency Property"

    Private Shared ReadOnly IsLaunchedPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("IsLaunched", GetType(Boolean), GetType(MainWindow), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf MainWindow.OnIsLaunchedPropertyChanged), New CoerceValueCallback(AddressOf MainWindow.OnIsLaunchedCoerceValueCallback)))

    Private Shared Function OnIsLaunchedCoerceValueCallback(ByVal sender As Object, ByVal value As Object) As Object
      Dim isLaunched As Boolean = CBool(value)

      If isLaunched Then
        Dim databaseInfo As DatabaseInfo = DirectCast(sender, MainWindow).DatabaseInfo

        Try

          ' Test root connection to server. 
          Try
            databaseInfo.TestConnection()
          Catch ex As Exception
            MessageBox.Show(("Connection Error. Please review the connection parameters and credentials." & Environment.NewLine) + Environment.NewLine + ex.Message.ToString(), "Connection Error.", MessageBoxButton.OK, MessageBoxImage.[Error])

            Throw
          End Try


          ' Test if database and table exists. 
          Try
            databaseInfo.CheckExistance()
          Catch ex As Exception
            MessageBox.Show(("Database or datatable not found. Please use the 'Create Data' button before connecting." & Environment.NewLine) + Environment.NewLine + ex.Message.ToString(), "Connection Error.", MessageBoxButton.OK, MessageBoxImage.[Error])

            Throw
          End Try
        Catch
          Return False
        End Try
      End If

      Return isLaunched
    End Function

    Private Shared Sub OnIsLaunchedPropertyChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
      Dim isLaunched As Boolean = CBool(e.NewValue)

      If isLaunched Then
        Dim mainWindow As MainWindow = TryCast(sender, MainWindow)

        App.WriteDatabaseInfo(mainWindow.DatabaseInfo)

        DirectCast(mainWindow.grid.ItemsSource, DataGridVirtualizingCollectionView).Refresh()
      End If
    End Sub

    Public Shared ReadOnly IsLaunchedProperty As DependencyProperty = MainWindow.IsLaunchedPropertyKey.DependencyProperty

    Public ReadOnly Property IsLaunched() As Boolean
      Get
        Return CBool(Me.GetValue(MainWindow.IsLaunchedProperty))
      End Get
    End Property

    Private Sub SetIsLaunched(ByVal value As Boolean)
      Me.SetValue(MainWindow.IsLaunchedPropertyKey, value)
    End Sub

#End Region

#Region "CreatingData Read-Only Dependency Property"

    Private Shared ReadOnly CreatingDataPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("CreatingData", GetType(Boolean), GetType(MainWindow), New PropertyMetadata(False))

    Public Shared ReadOnly CreatingDataProperty As DependencyProperty = MainWindow.CreatingDataPropertyKey.DependencyProperty

    Public ReadOnly Property CreatingData() As Boolean
      Get
        Return CBool(Me.GetValue(MainWindow.CreatingDataProperty))
      End Get
    End Property

    Private Sub SetCreatingData(ByVal value As Boolean)
      Me.SetValue(MainWindow.CreatingDataPropertyKey, value)
    End Sub

#End Region


#Region "INITIAL DATA CREATION"

    Private Sub CreateDatabase_ButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim databaseInfo As DatabaseInfo = Me.DatabaseInfo

      If Me.CreatingData Then
        If MessageBox.Show("Are you sure you want to cancel the data creation ?", "Cancel data creation ?", MessageBoxButton.OKCancel, MessageBoxImage.Question) = MessageBoxResult.OK Then
          createDataButton.IsEnabled = False
          databaseInfo.CancelCreateData()
        End If
        Exit Sub

      End If

      Me.SetCreatingData(True)
      Try
        Try
          databaseInfo.TestConnection()
        Catch ex As Exception
          MessageBox.Show(("Connection Error. Please review the connection parameters and credentials." & Environment.NewLine) + Environment.NewLine + ex.Message.ToString(), "Connection Error.", MessageBoxButton.OK, MessageBoxImage.[Error])
          Exit Sub

        End Try

        databaseInfo.EnsureDatabase()


        Try
          databaseInfo.CheckExistance()

          ' Datatable found. Ask to drop and recreate. 
          If MessageBox.Show((("Table " & databaseInfo.TableName & " already exists.") + Environment.NewLine & "Drop table and recreate with ") + databaseInfo.RecordCount.ToString() & " records ?", "Existing table found.", MessageBoxButton.OKCancel, MessageBoxImage.Warning) <> MessageBoxResult.OK Then
            Exit Sub
          End If

          ' Drop table. 
          databaseInfo.DropExistingTable()
        Catch
        End Try

        databaseInfo.CreateTable()

        createDataButton.Content = "Cancel"

        databaseInfo.CreateRows(Me.Dispatcher, New Action(Of Integer)(AddressOf Me.OnProgress), New Action(AddressOf Me.OnCancel))

        ' If the creation was not aborted... 
        If Me.CreatingData Then
          progressBar.Value = 0
          databaseInfo.CreateIndexes(Me.Dispatcher, New Action(Of Integer)(AddressOf Me.OnProgress), New Action(AddressOf Me.OnCancel))

          ' If the creation was not aborted... 
          If Me.CreatingData Then
            Me.SetIsLaunched(True)
          End If
        End If
      Catch ex As Exception
        MessageBox.Show(("Server error. Please review the connection parameters." & Environment.NewLine) + Environment.NewLine + ex.Message.ToString(), "Server Error.", MessageBoxButton.OK, MessageBoxImage.[Error])
      Finally
        createDataButton.Content = "Create Data"
        progressBar.Value = 0
        Me.SetCreatingData(False)
      End Try
    End Sub

    Private Sub OnProgress(ByVal progressVal As Integer)
      Me.progressBar.Value = progressVal
    End Sub

    Private Sub OnCancel()
      Me.SetCreatingData(False)
      createDataButton.ClearValue(Button.IsEnabledProperty)
    End Sub

#End Region

#Region "LAUNCHING"

    Private Sub launch_ButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.SetIsLaunched(True)
    End Sub

#End Region


#Region "DATA VIRTUALIZATION HOOKS"

    Private Sub DataGridVirtualizingCollectionViewSource_AbortQueryItemCount(ByVal sender As Object, ByVal e As QueryItemCountEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AbortReadCount(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_AbortQueryItems(ByVal sender As Object, ByVal e As QueryItemsEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AbortReadData(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_AbortQueryGroups(ByVal sender As Object, ByVal e As QueryGroupsEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AbortReadDistinctValuesAndCounts(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_AbortQueryAutoFilterDistinctValues(ByVal sender As Object, ByVal e As QueryAutoFilterDistinctValuesEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AbortReadDistinctValues(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_QueryItems(ByVal sender As Object, ByVal e As QueryItemsEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AsyncReadData(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_QueryGroups(ByVal sender As Object, ByVal e As QueryGroupsEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AsyncReadDistinctValuesAndCounts(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_QueryItemCount(ByVal sender As Object, ByVal e As QueryItemCountEventArgs)
      If Not Me.IsLaunched Then
        e.Count = 0
        Exit Sub
      End If

      If m_adoDataBridge Is Nothing Then
        m_adoDataBridge = New ADODataBridge(Me.DatabaseInfo.GetConnectionString(), Me.DatabaseInfo.TableName)
      End If

      m_adoDataBridge.AsyncReadCount(sender, e)
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_QueryAutoFilterDistinctValues(ByVal sender As Object, ByVal e As QueryAutoFilterDistinctValuesEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AsyncReadDistinctValues(sender, e)
      End If
    End Sub

    Private Sub DataGridVirtualizingCollectionViewSource_CommitItems(ByVal sender As Object, ByVal e As CommitItemsEventArgs)
      If m_adoDataBridge IsNot Nothing Then
        m_adoDataBridge.AsyncCommitData(sender, e)
      End If
    End Sub

#End Region


#Region "PRIVATE FIELDS"

    Private m_adoDataBridge As ADODataBridge

#End Region

  End Class

End Namespace
