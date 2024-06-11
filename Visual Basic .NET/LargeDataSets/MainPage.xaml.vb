'
'  Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [MainPage.xaml.vb]
'  
' This sample demonstrates how Xceed DataGrid for WPF performs with
' a large amount of rows and/or columns.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports Xceed.Wpf.DataGrid
Imports System.Threading
Imports System.Diagnostics
Imports System.Collections.Specialized
Imports System.Windows.Threading
Imports System.Windows.Media.Animation

Namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
  Partial Public Class MainPage
    Inherits Page
    ' Generic Delegate for DataSet refresh
    Private Delegate Sub GenericDelegate()

    Public Sub New()
      ' Set the window's DataContext to itself to allow the grid to bind directly to the
      ' DataContext rather than using an RelativeSource binding to find the window.
      Me.DataContext = Me

      InitializeComponent()
      AddHandler Loaded, AddressOf MainPage_Loaded
    End Sub

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Create the initial data items.
      Me.CreateDataItems()
    End Sub

#Region "Persons Property"

    ' Property that provides the Person data.
    Public Property Persons() As DataGridCollectionView
      Get
        Return CType(GetValue(PersonsProperty), DataGridCollectionView)
      End Get
      Set(ByVal value As DataGridCollectionView)
        SetValue(PersonsProperty, Value)
      End Set
    End Property

    ' Using a DependencyProperty as the backing store for Persons.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly PersonsProperty As DependencyProperty = DependencyProperty.Register("Persons", GetType(DataGridCollectionView), GetType(MainPage), New UIPropertyMetadata(Nothing))

#End Region

#Region "IsApplyButtonEnabled"

    Public Property IsApplyButtonEnabled() As Boolean
      Get
        Return CBool(Me.GetValue(IsApplyButtonEnabledProperty))
      End Get
      Private Set(ByVal value As Boolean)
        Me.SetValue(IsApplyButtonEnabledPropertyKey, Value)
      End Set
    End Property

    Private Shared ReadOnly IsApplyButtonEnabledPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("IsApplyButtonEnabled", GetType(Boolean), GetType(MainPage), New UIPropertyMetadata(False))

    Public Shared ReadOnly IsApplyButtonEnabledProperty As DependencyProperty = IsApplyButtonEnabledPropertyKey.DependencyProperty

#End Region

    ' Create the new collection of data items on a background thread.
    Private Sub CreateDataItems()
      ' Disable RadioButtons while generating
      Me.columnsGroupBox.IsEnabled = False
      Me.rowsGroupBox.IsEnabled = False

      ' Disable ApplyButton before DataSet generation. It will
      ' be re-enabled when one of the Column / Row count option
      ' is modified
      Me.IsApplyButtonEnabled = False

      If Not Me.grid Is Nothing Then
        ' Hide the grid to display a wait cursor and progress information instead.
        Me.grid.Visibility = Visibility.Hidden
      End If

      ' Generate the rows in a background thread.
      If ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.BackgroundWorker)) = False Then
        Throw New ApplicationException("Unable to generate the new data items.")
      End If
    End Sub

#Region "Background Thread Item Creation"

    ' Background worker thread
    Private Sub BackgroundWorker(ByVal param As Object)
      ' Recreate a new collection in the background.
      Dim newSource As PersonObservableCollection = New PersonObservableCollection()

      ' Add the desired number of rows and columns to the grid. For each item that is added
      ' we will update the progress bar in the UI thread.
      Dim i As Double = 0
      Do While i < m_rowCount
        newSource.Add(New Person(m_columnCount - Person.DefaultPropertyCount))

        SyncLock m_lockObject
          If m_updateDispatcherOperation Is Nothing Then
            m_updateDispatcherOperation = Me.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New WaitCallback(AddressOf Me.OnProgress), Nothing)
          End If

          m_percent = (i / m_rowCount) * 100
        End SyncLock
        i += 1
      Loop

      Me.Dispatcher.BeginInvoke(DispatcherPriority.Send, New FinalizeDelegate(AddressOf Me.OnFinalize), newSource)
    End Sub

    Private Sub OnFinalize(ByVal newSource As PersonObservableCollection)
      ' The grid will preserve its columns even when it is assigned a new data source;
      ' therefore, clear the grid's columns before assigning the new DataGridCollectionView
      ' so that the "extra" columns are removed.
      Me.grid.Columns.Clear()

      Me.Persons = New DataGridCollectionView(newSource)

      ' Force Garbage Collection to avoid excessive memory usage
      GC.Collect()

      ' Show the grid.
      Me.grid.Visibility = Visibility.Visible

      ' Enable RadioButtons after DataSet generation
      Me.columnsGroupBox.IsEnabled = True
      Me.rowsGroupBox.IsEnabled = True
    End Sub

    ' Display progress information.
    Private Sub OnProgress(ByVal param As Object)
      SyncLock m_lockObject
        Me.progressBar.Value = m_percent

        m_updateDispatcherOperation = Nothing
      End SyncLock
    End Sub

#End Region ' Background Thread Item Creation

#Region "Radio Button Selection-Changed Events"

    Private Sub ColumnRadioButtonChanged_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Enable the Apply button to refresh the DataSource
      Me.IsApplyButtonEnabled = True

      Dim selectedRadioButton As RadioButton = TryCast(sender, RadioButton)

      If sender Is Nothing Then
        Return
      End If

      Dim radioButtonTag As String = TryCast(selectedRadioButton.Tag, String)

      If String.IsNullOrEmpty(radioButtonTag) = True Then
        Return
      End If

      Try
        ' Get the Column count from the RadioButton.Tag property
        m_columnCount = Int32.Parse(radioButtonTag)
      Catch e1 As Exception
        Debug.WriteLine("Unable to convert Columns count from RadioButton.Tag")
      End Try

    End Sub

    Private Sub RowRadioButtonChanged_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Enable the Apply button to refresh the DataSource
      Me.IsApplyButtonEnabled = True

      Dim selectedRadioButton As RadioButton = TryCast(sender, RadioButton)

      If sender Is Nothing Then
        Return
      End If

      Dim radioButtonTag As String = TryCast(selectedRadioButton.Tag, String)

      If String.IsNullOrEmpty(radioButtonTag) = True Then
        Return
      End If

      Try
        ' Get the Row count from the RadioButton.Tag property
        m_rowCount = Double.Parse(radioButtonTag)
      Catch e1 As Exception
        Debug.WriteLine("Unable to convert Rows count from RadioButton.Tag")
      End Try
    End Sub

#End Region ' Radio Button Selection-Changed Events

#Region "Appply Button Click Events"

    Private Sub OnApplySourceParametersClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.CreateDataItems()
    End Sub

#End Region

#Region "Private Fields"

    Private m_rowCount As Double = 0
    Private m_columnCount As Integer = 0
    Private m_percent As Double = 0

    Private Delegate Sub ProgressDelegate(ByVal percent As Double)
    Private Delegate Sub FinalizeDelegate(ByVal newCollection As PersonObservableCollection)

    Private m_updateDispatcherOperation As DispatcherOperation = Nothing
    Private m_lockObject As Object = New Object()

#End Region ' Private Fields
  End Class
End Namespace
