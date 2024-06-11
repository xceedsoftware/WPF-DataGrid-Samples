'
' * Xceed DataGrid for WPF - SAMPLE CODE - BatchUpdating Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' *  
' * This sample demonstrates how Xceed DataGrid for WPF performs with
' * a large amount of columns for updating column's VisiblePosition and Visibility.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
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

Namespace Xceed.Wpf.DataGrid.Samples.BatchUpdating
  Partial Public Class MainPage
    Inherits Page
#Region "Constructors"

    Public Sub New()
      ' Set the window's DataContext to itself to allow the grid to bind directly to the
      ' DataContext rather than using an RelativeSource binding to find the window.
      Me.DataContext = Me

      InitializeComponent()
      AddHandler Loaded, AddressOf MainPage_Loaded
    End Sub

#End Region

#Region "Properties"

#Region "Persons Property"

    ' Using a DependencyProperty as the backing store for Persons.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly PersonsProperty As DependencyProperty = DependencyProperty.Register("Persons", GetType(DataGridCollectionView), GetType(MainPage), New UIPropertyMetadata(Nothing))

    ' Property that provides the Person data.
    Public Property Persons() As DataGridCollectionView
      Get
        Return CType(GetValue(PersonsProperty), DataGridCollectionView)
      End Get
      Set(ByVal value As DataGridCollectionView)
        SetValue(PersonsProperty, value)
      End Set
    End Property

#End Region

#Region "IsAddingColumnsButtonEnabled Property"

    Public Shared ReadOnly IsAddingColumnsButtonEnabledProperty As DependencyProperty = DependencyProperty.Register("IsAddingColumnsButtonEnabled", GetType(Boolean), GetType(MainPage), New PropertyMetadata(True))

    ''' <summary>
    ''' Gets or sets a value indicating whether the add/remove buttons should be enabled.
    ''' </summary>
    Public Property IsAddingColumnsButtonEnabled() As Boolean
      Get
        Return CBool(GetValue(IsAddingColumnsButtonEnabledProperty))
      End Get
      Set(ByVal value As Boolean)
        SetValue(IsAddingColumnsButtonEnabledProperty, value)
      End Set
    End Property

#End Region

#End Region

#Region "Events"

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' Create the initial data items.
      Me.CreateDataItems()
    End Sub

    Private Sub ButtonRemoveColumns_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If grid IsNot Nothing Then
        If (chkWithBatchUpdate IsNot Nothing) AndAlso chkWithBatchUpdate.IsChecked.HasValue AndAlso chkWithBatchUpdate.IsChecked.Value Then
          Using DataGridControl.GetDataGridContext(grid).DeferColumnsUpdate()
            Me.HideExtraColumns()
          End Using
        ElseIf (chkWithoutBatchUpdate IsNot Nothing) AndAlso chkWithoutBatchUpdate.IsChecked.HasValue AndAlso chkWithoutBatchUpdate.IsChecked.Value Then
          Me.HideExtraColumns()
        End If
      End If
    End Sub

    Private Sub ButtonAddColumns_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If grid IsNot Nothing Then
        If (chkWithBatchUpdate IsNot Nothing) AndAlso chkWithBatchUpdate.IsChecked.HasValue AndAlso chkWithBatchUpdate.IsChecked.Value Then
          Using DataGridControl.GetDataGridContext(grid).DeferColumnsUpdate()
            Me.ShowExtraColumns()
          End Using
        ElseIf (chkWithoutBatchUpdate IsNot Nothing) AndAlso chkWithoutBatchUpdate.IsChecked.HasValue AndAlso chkWithoutBatchUpdate.IsChecked.Value Then
          Me.ShowExtraColumns()
        End If
      End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub CreateDataItems()
      Dim newSource As New PersonObservableCollection()

      ' Add the desired number of rows and columns to the grid.
      For i As Double = 0 To m_rowCount - 1
        newSource.Add(New Person(m_columnCount - Person.DefaultPropertyCount))
      Next i
      Me.Persons = New DataGridCollectionView(newSource)

      Dim lastColumn As Integer = grid.Columns.Count
      ' Initial loading will hide all extra columns fast.
      Using DataGridControl.GetDataGridContext(grid).DeferColumnsUpdate()
        For i As Integer = Person.DefaultPropertyCount To lastColumn - 1
          grid.Columns(i).SetValue(ColumnBase.VisibleProperty, False)
        Next i
      End Using
    End Sub

    Private Sub ShowExtraColumns()
      WorkingText.Visibility = Visibility.Visible
      Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, New ThreadStart(AddressOf ShowExtraColumnsCore))
    End Sub

    Private Sub ShowExtraColumnsCore()
      Dim positionIndex As Integer = m_random.Next(0, 50)

      ' Show extra columns from the grid and modify the position of all columns from grid.
      For Each cb As ColumnBase In grid.Columns
        If (Not cb.Visible) Then
          cb.SetValue(ColumnBase.VisibleProperty, True)
        End If
        If cb.VisiblePosition <> positionIndex Then
          cb.SetCurrentValue(ColumnBase.VisiblePositionProperty, positionIndex)
        End If

        positionIndex += 1
      Next cb

      Me.IsAddingColumnsButtonEnabled = False
      WorkingText.Visibility = Visibility.Collapsed
    End Sub

    Private Sub HideExtraColumns()
      WorkingText.Visibility = Visibility.Visible
      Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, New ThreadStart(AddressOf HideExtraColumnsCore))
    End Sub

    Private Sub HideExtraColumnsCore()
      Dim lastColumn As Integer = grid.Columns.Count

      ' Hide extra columns from the grid.
      For i As Integer = Person.DefaultPropertyCount To lastColumn - 1
        Dim col As ColumnBase = grid.Columns(i)
        If col.Visible Then
          col.SetValue(ColumnBase.VisibleProperty, False)
        End If
      Next i

      Me.IsAddingColumnsButtonEnabled = True
      WorkingText.Visibility = Visibility.Collapsed
    End Sub

#End Region

#Region "Private Fields"

    Private m_rowCount As Double = 100
    Private m_columnCount As Integer = 310
    Private m_random As New Random()

#End Region ' Private Fields
  End Class
End Namespace