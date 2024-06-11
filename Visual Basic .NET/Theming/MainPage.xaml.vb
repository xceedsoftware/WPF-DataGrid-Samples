'
' * Xceed DataGrid for WPF - SAMPLE CODE - Theming Sample Application
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

Imports System.Windows
Imports System.Windows.Controls
Imports Xceed.Wpf.DataGrid.Samples.Theming.UIStyles
Imports Xceed.Wpf.DataGrid.ThemePack
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.Theming
  Partial Public Class MainPage
    Inherits System.Windows.Controls.Page

    Public Sub New()
      Me.InitializeComponent()

      AddHandler Loaded, AddressOf MainPage_Loaded
      AddHandler Unloaded, AddressOf MainPage_Unloaded

      m_orderIDColumnWidth = Me.grid.Columns("OrderID").Width
    End Sub

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      AddHandler(CType(Application.Current, App)).ViewChanged, AddressOf MainPage_ViewChanged

      Me.LoadViewsCombo(Me.grid)
      Me.LoadThemesCombo(Me.grid)
      Me.UpdateGridTheme()
    End Sub

    Private Sub MainPage_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      RemoveHandler(CType(Application.Current, App)).ViewChanged, AddressOf MainPage_ViewChanged
    End Sub

    Private Sub MainPage_ViewChanged(ByVal sender As Object, ByVal e As EventArgs)
      Me.grid.DetailConfigurations.Clear()

      If TypeOf Me.grid.View Is Xceed.Wpf.DataGrid.Views.TreeGridflowView Then
        'bigger column to see the details
        Me.grid.Columns("OrderID").Width = 120.0R

        Dim detailConfig As DetailConfiguration = TryCast(Me.Resources("CustomersDetailsConfiguration"), DetailConfiguration)
        If detailConfig IsNot Nothing Then
          Me.grid.DetailConfigurations.Add(detailConfig)
        End If
      Else
        Me.grid.Columns("OrderID").Width = m_orderIDColumnWidth
      End If
    End Sub

    Private Sub OnViewsComboSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If e.AddedItems.Count = 0 Then
        Return
      End If

      Dim viewData As ViewData = TryCast(e.AddedItems(0), ViewData)

      If viewData Is Nothing Then
        Return
      End If

      Me.FilterThemes(viewData)

      Me.UpdateGridTheme()
      CType(Application.Current, App).OnViewChanged(EventArgs.Empty)

      e.Handled = True
    End Sub

    Private Sub OnThemesComboSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Me.LoadAccentsCombo(grid)
      Me.UpdateGridTheme()

      e.Handled = True
    End Sub

    Private Sub OnAccentComboSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Me.UpdateGridTheme()

      e.Handled = True
    End Sub

    Private Sub LoadViewsCombo(ByVal dataGrid As DataGridControl)
      Dim viewsDataSource As DataGridCollectionViewSource = TryCast(Me.FindResource("viewsDataSource"), DataGridCollectionViewSource)

      If viewsDataSource Is Nothing Then
        Exit Sub
      End If

      viewsDataSource.View.Refresh()

      ' Set the current view of the grid
      If (dataGrid IsNot Nothing) AndAlso (dataGrid.View IsNot Nothing) Then
        Dim viewType As Type = dataGrid.View.GetType()

        For Each viewData As ViewData In viewsDataSource.View
          If viewData.ViewType Is viewType Then
            viewsDataSource.View.MoveCurrentTo(viewData)
            Exit For
          End If
        Next viewData
      End If

      If viewsCombo IsNot Nothing Then
        RemoveHandler viewsCombo.SelectionChanged, AddressOf OnViewsComboSelectionChanged
        viewsCombo.ItemsSource = viewsDataSource.View
        AddHandler viewsCombo.SelectionChanged, AddressOf OnViewsComboSelectionChanged
      End If
    End Sub

    Private Sub LoadThemesCombo(dataGrid As DataGridControl)
      Dim themesDataSource As DataGridCollectionViewSource = TryCast(Me.FindResource("themesDataSource"), DataGridCollectionViewSource)

      If themesDataSource Is Nothing Then
        Exit Sub
      End If

      Dim viewsDataSource As DataGridCollectionViewSource = TryCast(Me.FindResource("viewsDataSource"), DataGridCollectionViewSource)

      ' Apply filtering before binding to the combo.
      If (viewsDataSource IsNot Nothing) AndAlso (viewsDataSource.View IsNot Nothing) AndAlso (viewsDataSource.View.CurrentItem IsNot Nothing) Then
        Dim currentView As ViewData = DirectCast(viewsDataSource.View.CurrentItem, ViewData)

        themesDataSource.View.Filter = New Predicate(Of Object)(Function(s As Object) Me.AcceptTheme(currentView, TryCast(s, ThemeData)))
      End If

      ' Set the current theme of the grid
      If (dataGrid IsNot Nothing) AndAlso (dataGrid.View IsNot Nothing) AndAlso (dataGrid.View.Theme IsNot Nothing) Then
        Dim themeType As Type = dataGrid.View.Theme.[GetType]()

        For Each themeData As ThemeData In themesDataSource.View
          If themeData.Theme Is Nothing Then
            Continue For
          End If

          If themeData.Theme.[GetType]() = themeType Then
            themesDataSource.View.MoveCurrentTo(themeData)
            Exit For
          End If
        Next
      End If

      If themesCombo IsNot Nothing Then
        RemoveHandler themesCombo.SelectionChanged, AddressOf OnThemesComboSelectionChanged
        themesCombo.ItemsSource = themesDataSource.View
        AddHandler themesCombo.SelectionChanged, AddressOf OnThemesComboSelectionChanged
      End If

      Me.LoadAccentsCombo(grid)
    End Sub

    Private Sub LoadAccentsCombo(dataGrid As DataGridControl)
      If accentCombo Is Nothing Then
        Exit Sub
      End If

      RemoveHandler accentCombo.SelectionChanged, AddressOf OnAccentComboSelectionChanged

      Dim themeData As ThemeData = Me.GetCurrentThemeData()
      Dim accentDataSource As DataGridCollectionViewSource = Me.FindThemeAccentColorDataSource(themeData)

      If accentDataSource IsNot Nothing Then
        accentCombo.ItemsSource = accentDataSource.View

        If dataGrid IsNot Nothing AndAlso dataGrid.View IsNot Nothing Then
          Dim accentDataList As List(Of AccentData) = accentCombo.Items.Cast(Of AccentData)().ToList()
          Dim themeAccentColor As String = Me.GetThemeAccentColor(dataGrid.View.Theme)

          '# For now : Assume AccentColor.ToString() will always match AccentData.Description
          Dim selectedAccentData As AccentData = accentDataList.FirstOrDefault(Function(x) x.Description = themeAccentColor)
          If selectedAccentData IsNot Nothing Then
            accentCombo.SelectedItem = selectedAccentData
          End If
        End If

        accentCombo.Visibility = Visibility.Visible
      Else
        accentCombo.Visibility = Visibility.Hidden
      End If

      AddHandler accentCombo.SelectionChanged, AddressOf OnAccentComboSelectionChanged
    End Sub

    Private Sub FilterThemes(view As ViewData)
      Dim dataSource As DataGridCollectionView = DirectCast(themesCombo.ItemsSource, DataGridCollectionView)
      If dataSource IsNot Nothing Then
        dataSource.Filter = New Predicate(Of Object)(Function(s As Object) Me.AcceptTheme(view, TryCast(s, ThemeData)))

        dataSource.Refresh()

        If themesCombo.SelectedItem Is Nothing Then
          themesCombo.SelectedIndex = 0
        End If

        Me.LoadAccentsCombo(grid)
      End If
    End Sub

    Private Function AcceptTheme(ByVal view As ViewData, ByVal theme As ThemeData) As Boolean
      If view Is Nothing Then
        Return False
      End If

      If theme Is Nothing Then
        Return False
      End If

      If theme.Theme IsNot Nothing Then
        Return theme.Theme.IsViewSupported(view.ViewType)
      ElseIf theme.UseSystemTheme Then
        Return (theme.IsTheme3D = view.IsView3D)
      Else
        Return False
      End If
    End Function

    Private Sub UpdateGridTheme()
      ' Update the view
      Dim viewData As ViewData = Me.GetCurrentViewData()

      If (viewData IsNot Nothing) AndAlso (Me.grid.View.GetType() IsNot viewData.ViewType) Then
        Me.grid.View = TryCast(Activator.CreateInstance(viewData.ViewType), UIViewBase)
      End If

      ' Update the theme
      Dim themeData As ThemeData = Me.GetCurrentThemeData()
      If themeData IsNot Nothing Then
        If themeData.UseSystemTheme Then
          Me.grid.View.ClearValue(Xceed.Wpf.DataGrid.Views.ViewBase.ThemeProperty)
        Else
          Me.grid.Resources.MergedDictionaries.Clear()

          Select Case themeData.ThemeGroup
            Case "Metro Themes"
              Dim accentData As AccentData = Me.GetCurrentAccentData()

              grid.View.Theme = Me.CreateMetroTheme(themeData, accentData)
            Case "Material Design"
              Dim accentData As AccentData = Me.GetCurrentAccentData()

              grid.View.Theme = Me.CreateMaterialDesignTheme(themeData, accentData)
            Case "Fluent Design"
              Dim accentData As AccentData = Me.GetCurrentAccentData()

              grid.View.Theme = Me.CreateFluentDesignTheme(themeData, accentData)
            Case Else
              Me.grid.View.Theme = themeData.Theme
          End Select
        End If
      End If
    End Sub

    Private Function CreateMetroTheme(themeData As ThemeData, accentData As AccentData) As MetroTheme
      Dim themeResourceDictionary As MetroThemeResourceDictionaryBase

      If themeData.Description = "Metro Light" Then
        themeResourceDictionary = New MetroLightThemeResourceDictionary
      Else
        themeResourceDictionary = New MetroDarkThemeResourceDictionary
      End If

      Dim accentColor As MetroAccentColor

      If accentData IsNot Nothing AndAlso [Enum].TryParse(Of MetroAccentColor)(accentData.Description, accentColor) Then
        themeResourceDictionary.AccentColor = accentColor
      Else
        themeResourceDictionary.AccentColor = MetroAccentColor.Orange
      End If

      Return New MetroTheme(themeResourceDictionary)
    End Function

    Private Function CreateMaterialDesignTheme(themeData As ThemeData, accentData As AccentData) As MaterialDesignTheme
      Dim themeResourceDictionary As MaterialDesignResourceDictionary = New MaterialDesignResourceDictionary

      If themeData.Description = "Material Design Light" Then
        themeResourceDictionary.Mode = MaterialDesignColorMode.Light
      Else
        themeResourceDictionary.Mode = MaterialDesignColorMode.Dark
      End If

      Dim accentColor As MaterialDesignColor

      If accentData IsNot Nothing AndAlso [Enum].TryParse(Of MaterialDesignColor)(accentData.Description, accentColor) Then
        themeResourceDictionary.PrimaryColor = accentColor
      Else
        themeResourceDictionary.PrimaryColor = MaterialDesignColor.Orange
      End If

      Return New MaterialDesignTheme(themeResourceDictionary)
    End Function

    Private Function CreateFluentDesignTheme(themeData As ThemeData, accentData As AccentData) As FluentDesignTheme
      Dim themeResourceDictionary As FluentDesignResourceDictionary = New FluentDesignResourceDictionary

      If themeData.Description = "Fluent Design Light" Then
        themeResourceDictionary.Mode = EnvironmentMode.Light
      Else
        themeResourceDictionary.Mode = EnvironmentMode.Dark
      End If

      Dim accentColor As FluentDesignAccentColor

      If accentData IsNot Nothing AndAlso [Enum].TryParse(Of FluentDesignAccentColor)(accentData.Description, accentColor) Then
        themeResourceDictionary.AccentColor = accentColor
      Else
        themeResourceDictionary.AccentColor = FluentDesignAccentColor.DarkOrange
      End If

      Return New FluentDesignTheme(themeResourceDictionary)
    End Function

    Private Function GetCurrentViewData() As ViewData
      Dim viewData As ViewData = Nothing

      If viewsCombo IsNot Nothing Then
        viewData = TryCast(viewsCombo.SelectedItem, ViewData)
      End If

      Return viewData
    End Function

    Private Function GetCurrentThemeData() As ThemeData
      Dim themeData As ThemeData = Nothing

      If themesCombo IsNot Nothing Then
        themeData = TryCast(themesCombo.SelectedItem, ThemeData)
      End If

      Return themeData
    End Function

    Private Function GetCurrentAccentData() As AccentData
      Dim accentData As AccentData = Nothing

      If accentCombo IsNot Nothing Then
        accentData = TryCast(accentCombo.SelectedItem, AccentData)
      End If

      Return accentData
    End Function

    Private Function FindThemeAccentColorDataSource(themeData As ThemeData) As DataGridCollectionViewSource
      If themeData Is Nothing Then
        Return Nothing
      End If

      Select Case themeData.ThemeGroup
        Case "Metro Themes"
          Return TryCast(Me.FindResource("accentDataSource"), DataGridCollectionViewSource)

        Case "Material Design"
          Return TryCast(Me.FindResource("primaryColorDataSource"), DataGridCollectionViewSource)

        Case "Fluent Design"
          Return TryCast(Me.FindResource("fluentAccentDataSource"), DataGridCollectionViewSource)

        Case Else
          Return Nothing

      End Select
    End Function

    Private Function GetThemeAccentColor(theme As Theme) As String
      Dim metroTheme As MetroTheme = TryCast(theme, MetroTheme)
      If metroTheme IsNot Nothing Then
        Return DirectCast(metroTheme.ThemeResourceDictionary, MetroThemeResourceDictionaryBase).AccentColor.ToString()
      End If

      Dim materialDesignTheme As MaterialDesignTheme = TryCast(theme, MaterialDesignTheme)
      If materialDesignTheme IsNot Nothing Then
        Return DirectCast(materialDesignTheme.ThemeResourceDictionary, MaterialDesignResourceDictionary).PrimaryColor.ToString()
      End If

      Dim fluentDesignTheme As FluentDesignTheme = TryCast(theme, FluentDesignTheme)
      If fluentDesignTheme IsNot Nothing Then
        Return DirectCast(fluentDesignTheme.ThemeResourceDictionary, FluentDesignResourceDictionary).AccentColor.ToString()
      End If

      Return Nothing
    End Function

    Private m_orderIDColumnWidth As Double
  End Class
End Namespace