'
' Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
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
Imports System.Windows

Namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page
    Public Sub New()
      Me.InitializeComponent()
      Me.UpdateHeadersFooters()

      AddHandler Loaded, AddressOf MainPage_Loaded
      AddHandler Unloaded, AddressOf MainPage_Unloaded
    End Sub

    Private Sub UpdateHeadersFooters()
      ' Add the various headers and footers element defined in the resources according
      ' to the current view.
      If TypeOf Me.grid.View Is Xceed.Wpf.DataGrid.Views.TableView Then
        Me.grid.View.Headers.Add(CType(Me.FindResource("tableViewHeader"), DataTemplate))
        Me.labelRadioButton.Visibility = Visibility.Visible

      Else
        Me.grid.View.Headers.Add(CType(Me.FindResource("cardViewHeader"), DataTemplate))
        Me.labelRadioButton.Visibility = Visibility.Collapsed
      End If

      Me.UpdateDefaultGroupConfiguration()
    End Sub

    Private Sub UpdateDefaultGroupConfiguration()

      If TypeOf Me.grid.View Is Xceed.Wpf.DataGrid.Views.TableView Then

        Me.grid.View.FixedFooters.Clear()

        If Me.summaryRadioButton.IsChecked.GetValueOrDefault(False) Then
          Me.grid.View.FixedFooters.Add(CType(Me.FindResource("tableViewFixedFooter"), DataTemplate))
          Me.grid.DefaultGroupConfiguration = CType(Me.FindResource("tableViewGroupConfigurationSummary"), GroupConfiguration)

        ElseIf Me.labelRadioButton.IsChecked.GetValueOrDefault(False) Then
          Me.grid.View.FixedFooters.Add(CType(Me.FindResource("tableViewFixedFooterWithStatCellLabel"), DataTemplate))
          Me.grid.DefaultGroupConfiguration = CType(Me.FindResource("tableViewGroupConfigurationLabelStatCell"), GroupConfiguration)

        Else
          Me.grid.View.FixedFooters.Add(CType(Me.FindResource("tableViewFixedFooter"), DataTemplate))
          Me.grid.DefaultGroupConfiguration = CType(Me.FindResource("tableViewGroupConfigurationExpandedCollapsed"), GroupConfiguration)
        End If

      Else
        If Me.labelRadioButton.IsChecked.GetValueOrDefault(False) Then
          Me.expandedRadioButton.IsChecked = True
        End If

        If Me.summaryRadioButton.IsChecked.GetValueOrDefault(False) Then
          Me.grid.DefaultGroupConfiguration = CType(Me.FindResource("cardViewGroupConfigurationSummary"), GroupConfiguration)

        Else
          Me.grid.DefaultGroupConfiguration = CType(Me.FindResource("cardViewGroupConfigurationExpandedCollapsed"), GroupConfiguration)
        End If
      End If

      ' The GroupLevelConfiguration's InitiallyExpanded property dictates 
      ' whether the groups are expanded by default when the grid is filled.
      If Me.expandedRadioButton.IsChecked.GetValueOrDefault() OrElse Me.labelRadioButton.IsChecked.GetValueOrDefault() Then
        Me.grid.DefaultGroupConfiguration.InitiallyExpanded = True

      Else
        Me.grid.DefaultGroupConfiguration.InitiallyExpanded = False
      End If
    End Sub
    '
    '* Event handlers
    '

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      AddHandler(CType(Application.Current, App)).ViewChanged, AddressOf Application_ViewChanged
    End Sub

    Private Sub MainPage_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' It is prudent to sever the link between the Application and this Page. If we 
      ' were in a context of a NavigationWindow where the user could go from one page to
      ' another, this event "unsubscription" would allow this page to be freed on the next
      ' garbage collection.
      RemoveHandler(CType(Application.Current, App)).ViewChanged, AddressOf Application_ViewChanged
    End Sub

    Private Sub Application_ViewChanged(ByVal sender As Object, ByVal e As EventArgs)
      Dim tableView As Views.TableView = TryCast(Grid.View, Views.TableView)
      If tableView IsNot Nothing Then
        tableView.AllowStatsEditor = True
      End If

      Me.UpdateHeadersFooters()
    End Sub

    Private Sub SummaryTypeChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
      If Not Me.grid Is Nothing Then
        Me.UpdateDefaultGroupConfiguration()

        If Me.grid.IsBeingEdited Then
          Me.grid.EndEdit()
        End If

        ' Refresh the grid items.
        Dim source As System.Collections.IEnumerable = Me.grid.ItemsSource
        Me.grid.ItemsSource = Nothing
        Me.grid.ItemsSource = source
      End If
    End Sub
  End Class
End Namespace
