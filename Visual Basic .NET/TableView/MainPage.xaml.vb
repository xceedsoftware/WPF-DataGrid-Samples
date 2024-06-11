'
' * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
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
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.TableView
  Partial Public Class MainPage
    Inherits Page
    Implements IWeakEventListener
    Public Sub New()
      InitializeComponent()

      AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf ConfigurationData_PropertyChanged
      Me.AdjustHeadersFooters()
      PropertyChangedEventManager.AddListener(grid, Me, "HasSearchControl")
    End Sub

    Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      Select Case e.PropertyName
        Case "FilteringMethod", "ShowGroupByControl", "ShowColumnManagerRow", "ShowInsertionRow"
          Me.AdjustHeadersFooters()

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

    Private Sub AdjustHeadersFooters()
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

  End Class
End Namespace