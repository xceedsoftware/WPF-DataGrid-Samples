'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
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
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports Xceed.Wpf.DataGrid.Settings
Imports System.Configuration
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Windows.Resources

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
  Partial Public Class MainPage
    Inherits Page
#Region "CONSTRUCTORS"

    Public Sub New()
      Me.InitializeComponent()

      AddHandler Me.Loaded, AddressOf Me.MainPage_Loaded
      AddHandler Me.Unloaded, AddressOf Me.MainPage_Unloaded
    End Sub

#End Region

#Region "CurrentSettings Property"

    Public Shared ReadOnly CurrentSettingsProperty As DependencyProperty = DependencyProperty.Register("CurrentSettings", GetType(SettingsRepository), GetType(MainPage))

    Public Property CurrentSettings() As SettingsRepository
      Get
        Return CType(Me.GetValue(MainPage.CurrentSettingsProperty), SettingsRepository)
      End Get
      Set(ByVal value As SettingsRepository)
        Me.SetValue(MainPage.CurrentSettingsProperty, value)
      End Set
    End Property

#End Region

#Region "EVENT HANDLERS"

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      AddHandler CType(Application.Current, App).ViewChanged, AddressOf Me.Application_ViewChanged
      AddHandler CType(Application.Current, App).ViewChanging, AddressOf Me.Application_ViewChanging
      AddHandler Application.Current.Exit, AddressOf Me.Application_Exit

      ' Reload the saved application settings
      My.Settings.Reload()

      ' If settings were saved for the grid, apply them.
      Me.ChangeCurrentSettings(My.Settings.GridSettings)
    End Sub

    Private Sub MainPage_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      ' It is prudent to sever the link between the Application and this Page. If we 
      ' were in a context of a NavigationWindow where the user could go from one page to
      ' another, this event "unsubscription" would allow this page to be freed on the next
      ' garbage collection.
      RemoveHandler CType(Application.Current, App).ViewChanged, AddressOf Me.Application_ViewChanged
      RemoveHandler CType(Application.Current, App).ViewChanging, AddressOf Me.Application_ViewChanging
      RemoveHandler Application.Current.Exit, AddressOf Me.Application_Exit

      ' Persist the current grid settings.
      Me.SaveSettings()

      ' Save the grid settings with the application settings.
      My.Settings.GridSettings = Me.CurrentSettings
      My.Settings.Save()
    End Sub

    Private Sub Application_Exit(ByVal sender As Object, ByVal e As ExitEventArgs)
      ' Persist the current grid settings.
      Me.SaveSettings()

      ' Save the grid settings with the application settings.
      My.Settings.GridSettings = Me.CurrentSettings
      My.Settings.Save()
    End Sub

    Private Sub Application_ViewChanging(ByVal sender As Object, ByVal e As EventArgs)
      ' We are about to change the grid's view, we want to keep 
      ' the current grid settings.
      Me.SaveSettings()
    End Sub

    Private Sub Application_ViewChanged(ByVal sender As Object, ByVal e As EventArgs)
      ' The view changed. We re-apply the grid settings.
      Me.LoadSettings()
    End Sub

    Private Sub applySettingsButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim item As PredefinedSettingsItem = TryCast(predefinedSettingsCombo.SelectedItem, PredefinedSettingsItem)

      If item Is Nothing Then
        Return
      End If

      ' Loads the appropriate Xml settings file.
      Dim serializer As XmlSerializer = New XmlSerializer(GetType(SettingsRepository))
      Dim settings As SettingsRepository = TryCast(serializer.Deserialize(Application.GetResourceStream(New Uri(item.XmlUri)).Stream), SettingsRepository)

      ' Change the current applied settings.
      If Not settings Is Nothing Then
        Me.ChangeCurrentSettings(settings)
      End If
    End Sub

#End Region

#Region "PRIVATE METHODS"

    Private Sub ChangeCurrentSettings(ByVal settings As SettingsRepository)
      Me.CurrentSettings = settings
      Me.LoadSettings()
    End Sub

    Private Function GetUserSettings() As UserSettings
      Dim userSettings As UserSettings = userSettings.None

      If chkCardWidths.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.CardWidths
      End If

      If chkColumnPositions.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.ColumnPositions
      End If

      If chkColumnVisibilities.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.ColumnVisibilities
      End If

      If chkColumnWidths.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.ColumnWidths
      End If

      If chkFixedColumnCounts.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.FixedColumnCounts
      End If

      If chkGrouping.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.Grouping
      End If

      If chkSorting.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.Sorting
      End If

      If chkFilterCriteria.IsChecked.GetValueOrDefault(False) Then
        userSettings = userSettings Or userSettings.FilterCriteria
      End If

      Return userSettings
    End Function

    Private Sub LoadSettings()
      If Not Me.CurrentSettings Is Nothing Then
        Me.grid.LoadUserSettings(Me.CurrentSettings, Me.GetUserSettings())
      End If
    End Sub

    Private Sub SaveSettings()
      If Me.CurrentSettings Is Nothing Then
        Me.CurrentSettings = New SettingsRepository()
      End If

      Me.grid.SaveUserSettings(Me.CurrentSettings, Me.GetUserSettings())
    End Sub

#End Region
  End Class
End Namespace
