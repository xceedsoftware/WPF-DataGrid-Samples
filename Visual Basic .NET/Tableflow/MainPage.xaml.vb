'
' * Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPage.xaml.vb]
' * 
' * This class implements the various dynamic configuration options offered 
' * by the configuration panel declared in MainPage.xaml.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Partial Public Class MainPage
    Inherits Page
    Implements IWeakEventListener
    Public Sub New()
      InitializeComponent()
      SetDetailConfigurations()
      AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf ConfigurationData_PropertyChanged
      Me.AdjustHeadersFooters()
      PropertyChangedEventManager.AddListener(grid, Me, "HasSearchControl")
    End Sub

    Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      Select Case e.PropertyName
        Case "FilteringMethod", "ShowGroupByControl", "ShowColumnManagerRow", "ShowInsertionRow"
          Me.AdjustHeadersFooters()

        Case "ShowMergedHeaders"
          Me.AdjustHeadersFooters(True)

        Case "ShowMasterDetail"
          Me.ShowOrdersDetail()

        Case "ShowSearchControl"
          Me.ShowSearchControl()

      End Select
    End Sub

    Private Sub grid_DeletingSelectedItems(ByVal sender As Object, ByVal e As CancelRoutedEventArgs)
      e.Cancel = (MessageBoxResult.No = MessageBox.Show("Are you sure you want to delete the selected items?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question))
    End Sub

    Private Sub grid_DeletingSelectedItemError(ByVal sender As Object, ByVal e As DeletingSelectedItemErrorRoutedEventArgs)
      ' When an exception occur while deleting a row, you have 
      ' the chance to rectify the situation and retry the operation.
      ' You can also skip the item in error or abort the operation.
      ' For the purpose of this sample, we will skip on error.
      e.Action = DeletingSelectedItemErrorAction.Skip
    End Sub

    Private Sub AdjustHeadersFooters(Optional ByVal isShowMergedHeaderChanged As Boolean = False)
      ' Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear()
      grid.View.Headers.Clear()

      ' Add the group by control in the FixedHeaders if desired
      If ConfigurationData.Singleton.ShowGroupByControl Then
        If (ConfigurationData.Singleton.ShowInsertionRow) OrElse (ConfigurationData.Singleton.ShowColumnManagerRow) Then
          grid.View.FixedHeaders.Add(TryCast(Me.FindResource("hierarchicalGroupByControlTemplate"), DataTemplate))
        Else
          grid.View.FixedHeaders.Add(TryCast(Me.FindResource("aloneHierarchicalGroupByControlTemplate"), DataTemplate))
        End If
      End If

      ' Add the merged column manager row in the FixedHeaders if desired
      If ConfigurationData.Singleton.ShowMergedHeaders Then
        grid.View.FixedHeaders.Add(TryCast(Me.FindResource("mergedColumnManagerRowTemplate"), DataTemplate))
      End If

      If isShowMergedHeaderChanged Then
        Me.SetupUpMergedColumns(ConfigurationData.Singleton.ShowMergedHeaders)
      End If

      ' Add the column manager row in the FixedHeaders if desired
      If ConfigurationData.Singleton.ShowColumnManagerRow Then
        grid.View.FixedHeaders.Add(TryCast(Me.FindResource("columnManagerRowTemplate"), DataTemplate))
      End If

      Dim collectionView As DataGridCollectionView = TryCast(Me.grid.ItemsSource, DataGridCollectionView)

      If collectionView IsNot Nothing Then
        ' Add the filter row in the FixedHeaders if desired
        If ConfigurationData.Singleton.FilteringMethod = FilteringMethod.FilterRow Then
          grid.View.FixedHeaders.Add(TryCast(Me.FindResource("filterRowTemplate"), DataTemplate))
          collectionView.FilterCriteriaMode = FilterCriteriaMode.And
          collectionView.AutoFilterMode = AutoFilterMode.None
        ElseIf ConfigurationData.Singleton.FilteringMethod = FilteringMethod.AutoFilter Then
          collectionView.FilterCriteriaMode = FilterCriteriaMode.And
          collectionView.AutoFilterMode = AutoFilterMode.And
        Else
          collectionView.FilterCriteriaMode = FilterCriteriaMode.None
          collectionView.AutoFilterMode = AutoFilterMode.None
        End If
      End If

      ' Add the insertion row in the FixedHeaders if desired
      If ConfigurationData.Singleton.ShowInsertionRow Then
        grid.View.FixedHeaders.Add(TryCast(Me.FindResource("insertionRowTemplate"), DataTemplate))
      End If
    End Sub

    Private Sub ShowOrdersDetail()
      Me.SetDetailConfigurations()
    End Sub

    Private Sub SetDetailConfigurations()
      If Me.grid Is Nothing Then
        Return
      End If

      Me.grid.DetailConfigurations.Clear()

      If ConfigurationData.Singleton.ShowMasterDetail Then
        Me.grid.DetailConfigurations.Add(CType(Me.Resources("ordersDetailConfiguration"), DetailConfiguration))
      End If

      ' Collapse all details and re-expand them.
      Dim dataGridContexts As List(Of DataGridContext) = New List(Of DataGridContext)(Me.grid.GetChildContexts())

      For Each dataGridContext As DataGridContext In dataGridContexts
        dataGridContext.ParentDataGridContext.CollapseDetails(dataGridContext.ParentItem)
        dataGridContext.ParentDataGridContext.ExpandDetails(dataGridContext.ParentItem)
      Next dataGridContext

    End Sub

    Private Sub SetupUpMergedColumns(ByVal attach As Boolean)
      Dim tableflowView = TryCast(grid.View, TableflowView)
      Dim fixedColumnCount = tableflowView.FixedColumnCount
      tableflowView.FixedColumnCount = 0
      Dim mergedColumns As MergedColumnCollection

      grid.BeginInit()

      If attach Then
        grid.MergedHeaders.Add(m_mergedHeader)
        For Each column As MergedColumn In m_mergedColumnArray
          m_mergedHeader.MergedColumns.Add(column)
        Next column

        mergedColumns = m_mergedHeader.MergedColumns

        Dim mergedColumn = TryCast(mergedColumns("IDs"), MergedColumn)
        mergedColumn.ChildColumnNames = "OrderID EmployeeID"

        mergedColumn = TryCast(mergedColumns("OrderDetails"), MergedColumn)
        mergedColumn.ChildColumnNames = "CustomerID OrderDate ShipVia Freight"

        mergedColumn = TryCast(mergedColumns("AddressDetails"), MergedColumn)
        mergedColumn.ChildColumnNames = "ShipCity ShipCountry ShipName ShipAddress ShipPostalCode ShipRegion"
      Else
        m_mergedHeader = grid.MergedHeaders(0)
        mergedColumns = m_mergedHeader.MergedColumns
        m_mergedColumnArray = New MergedColumn(mergedColumns.Count - 1) {}
        mergedColumns.CopyTo(m_mergedColumnArray, 0)

        grid.MergedHeaders.Clear()

        grid.Columns("RequiredDate").VisiblePosition = Integer.MaxValue
        grid.Columns("ShippedDate").VisiblePosition = Integer.MaxValue
      End If

      grid.EndInit()

      tableflowView.FixedColumnCount = fixedColumnCount
    End Sub

    Private Sub ShowSearchControl()

      If ConfigurationData.Singleton.ShowSearchControl Then
        If DataGridCommands.OpenSearch.CanExecute(Nothing, grid) Then
          DataGridCommands.OpenSearch.Execute(Nothing, grid)
        End If
      Else
        If DataGridCommands.CloseSearch.CanExecute(Nothing, grid) Then
          DataGridCommands.CloseSearch.Execute(Nothing, grid)
        End If
      End If
    End Sub

    Private Sub UpdateShowSearchControlCheckBox()
      If grid.HasSearchControl Then
        ConfigurationData.Singleton.ShowSearchControl = True
      Else
        ConfigurationData.Singleton.ShowSearchControl = False
      End If
    End Sub

    Private Function ReceiveWeakEvent(ByVal managerType As Type, ByVal sender As Object, ByVal e As EventArgs) As Boolean Implements IWeakEventListener.ReceiveWeakEvent
      If managerType Is GetType(PropertyChangedEventManager) Then
        Me.UpdateShowSearchControlCheckBox()
        Return True
      End If

      Return False
    End Function


    Private m_mergedColumnArray() As MergedColumn
    Private m_mergedHeader As MergedHeader
  End Class
End Namespace