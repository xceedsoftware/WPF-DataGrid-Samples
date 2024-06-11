'
' Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MainPage.xaml.vb]
'
' This class implements the various dynamic configuration options offered
' by the configuration panel declared in MainPage.xaml.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Data
Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Grouping
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page
#Region "PUBLIC CONSTRUCTOR"

    Public Sub New()
      InitializeComponent()

      Dim context As DataGridContext = DataGridControl.GetDataGridContext(Me.grid)

      If context Is Nothing Then
        Throw New NullReferenceException("TODODOC")
      End If

      Me.groupCombo.SelectedIndex = 0

      ' Register to DataGridContext's GroupLevelDescriptions CollectiionChanged
      AddHandler (CType(context.GroupLevelDescriptions, INotifyCollectionChanged)).CollectionChanged, AddressOf GroupLevelDescriptionsCollectionChanged
    End Sub

#End Region

#Region "PRIVATE PROPERTIES"

    Public ReadOnly Property IsRatingLastGroupLevelDescription() As Boolean
      Get
        Dim returnedValue As Boolean = False

        Dim count As Integer = Me.grid.GroupLevelDescriptions.Count

        ' If the last GroupLevelDescription is Rating, enable custom group
        If (count > 0) AndAlso (Me.grid.GroupLevelDescriptions(count - 1).FieldName.Equals("Rating")) Then
          returnedValue = True
        End If

        Return returnedValue
      End Get
    End Property

#End Region

#Region "PRIVATE METHODS"

    Private Sub GroupLevelDescriptionsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
      ' Update useCustomGroupConfigurationSelector ComboBox state      
      useCustomGroupConfigurationSelector.IsEnabled = Me.IsRatingLastGroupLevelDescription

      Dim gridGroupDescriptions As ObservableCollection(Of GroupDescription) = Me.grid.Items.GroupDescriptions

      If gridGroupDescriptions.Count = 0 Then
        removeAllGroupsButton.IsEnabled = False
        removeFirstGroupLevelButton.IsEnabled = False
      Else
        removeAllGroupsButton.IsEnabled = True
        removeFirstGroupLevelButton.IsEnabled = True
      End If

      If m_updatingGrouping Then
        Return
      End If

      Dim currentGrouping As GroupingItem = Nothing

      For Each item As GroupingItem In Me.groupCombo.Items
        If item.Equals(gridGroupDescriptions) Then
          currentGrouping = item
          Exit For
        End If
      Next item

      Me.groupCombo.SelectedItem = currentGrouping
    End Sub

    Private Sub GroupComboSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Dim groupingItem As GroupingItem = TryCast(Me.groupCombo.SelectedItem, GroupingItem)
      Dim gridGroupDescriptions As ObservableCollection(Of GroupDescription) = Me.grid.Items.GroupDescriptions

      If (Not groupingItem Is Nothing) AndAlso ((Not groupingItem.Equals(gridGroupDescriptions))) Then
        m_updatingGrouping = True

        Using Me.grid.Items.DeferRefresh()
          gridGroupDescriptions.Clear()

          For Each groupDesc As GroupDescription In groupingItem.GroupDescriptions
            gridGroupDescriptions.Add(groupDesc)
          Next groupDesc
        End Using

        m_updatingGrouping = False
      End If
    End Sub

    Private Sub MinimumRatingComboBoxSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Me.UpdateGroupConfigurationSelectorValue()
    End Sub

    Private Sub RemoveAllGroupsClicked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.grid.Items.GroupDescriptions.Clear()
    End Sub

    Private Sub RemoveFirstGroupLevelClicked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Me.grid.Items.GroupDescriptions.Count > 0 Then
        Me.grid.Items.GroupDescriptions.RemoveAt(0)
      End If
    End Sub

    Private Sub UpdateGroupConfigurationSelectorValue()
      If Me.grid Is Nothing Then
        Return
      End If

      Dim checkBox As CheckBox = Me.useCustomGroupConfigurationSelector

      If checkBox Is Nothing Then
        Return
      End If

      Dim comboBox As ComboBox = Me.minimumRatingComboBox

      If comboBox Is Nothing Then
        Return
      End If

      If (comboBox.SelectedIndex = -1) OrElse (checkBox.IsChecked.GetValueOrDefault() = False) Then
        Me.grid.GroupConfigurationSelector = Nothing
      Else
        Dim selectedRating As Integer = 0

        If Int32.TryParse(comboBox.SelectedItem.ToString(), selectedRating) = False Then
          ' There was an error while converting value
          Me.grid.GroupConfigurationSelector = Nothing
        Else
          Me.grid.GroupConfigurationSelector = New MinimumRatingGroupConfigurationSelector(selectedRating)
        End If
      End If
    End Sub

    Private Sub UseCustomGroupConfigurationSelectorCheckBoxChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim checkBox As CheckBox = TryCast(sender, CheckBox)

      If checkBox Is Nothing Then
        Return
      End If

      If checkBox.IsChecked.GetValueOrDefault() = True Then
        Dim iSupportInitialize As ISupportInitialize = TryCast(Me.grid, ISupportInitialize)

        If Not iSupportInitialize Is Nothing Then
          iSupportInitialize.BeginInit()
        End If

        Me.UpdateGroupConfigurationSelectorValue()

        If Not iSupportInitialize Is Nothing Then
          iSupportInitialize.EndInit()
        End If
      Else
        Me.grid.GroupConfigurationSelector = Nothing
      End If
    End Sub

#End Region

#Region "PRIVATE FIELDS"

    Private m_updatingGrouping As Boolean = False

#End Region
  End Class
End Namespace
