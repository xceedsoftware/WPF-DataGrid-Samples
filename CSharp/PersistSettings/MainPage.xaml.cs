/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 * 
 * This class implements the various dynamic configuration options offered 
 * by the configuration panel declared in MainPage.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Xceed.Wpf.DataGrid.Settings;

namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
{
  public partial class MainPage : Page
  {
    #region CONSTRUCTORS

    public MainPage()
    {
      this.InitializeComponent();
      this.Loaded += new RoutedEventHandler( this.MainPage_Loaded );
      this.Unloaded += new RoutedEventHandler( this.MainPage_Unloaded );
    }

    #endregion CONSTRUCTORS

    #region CurrentSettings Property

    public static readonly DependencyProperty CurrentSettingsProperty = DependencyProperty.Register(
      "CurrentSettings",
      typeof( SettingsRepository ),
      typeof( MainPage ) );

    public SettingsRepository CurrentSettings
    {
      get
      {
        return ( SettingsRepository )this.GetValue( MainPage.CurrentSettingsProperty );
      }
      set
      {
        this.SetValue( MainPage.CurrentSettingsProperty, value );
      }
    }

    #endregion

    #region EVENT HANDLERS

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      ( ( App )Application.Current ).ViewChanged += new EventHandler( this.Application_ViewChanged );
      ( ( App )Application.Current ).ViewChanging += new EventHandler( this.Application_ViewChanging );
      Application.Current.Exit += new ExitEventHandler( this.Application_Exit );

      // Reload the saved application settings
      Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.Reload();

      // If settings were saved for the grid, apply them.
      this.ChangeCurrentSettings( Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.GridSettings );
    }

    private void MainPage_Unloaded( object sender, RoutedEventArgs e )
    {
      // It is prudent to sever the link between the Application and this Page. If we 
      // were in a context of a NavigationWindow where the user could go from one page to
      // another, this event "unsubscription" would allow this page to be freed on the next
      // garbage collection.
      ( ( App )Application.Current ).ViewChanged -= new EventHandler( this.Application_ViewChanged );
      ( ( App )Application.Current ).ViewChanging -= new EventHandler( this.Application_ViewChanging );
      Application.Current.Exit -= new ExitEventHandler( this.Application_Exit );

      // Persist the current grid settings.
      this.SaveSettings();

      // Save the grid settings with the application settings.
      Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.GridSettings = this.CurrentSettings;
      Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.Save();
    }

    private void Application_Exit( object sender, ExitEventArgs e )
    {
      // Persist the current grid settings.
      this.SaveSettings();

      // Save the grid settings with the application settings.
      Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.GridSettings = this.CurrentSettings;
      Xceed.Wpf.DataGrid.Samples.PersistSettings.Properties.Settings.Default.Save();
    }

    private void Application_ViewChanging( object sender, EventArgs e )
    {
      // We are about to change the grid's view, we want to keep 
      // the current grid settings.
      this.SaveSettings();
    }

    private void Application_ViewChanged( object sender, EventArgs e )
    {
      // The view changed. We re-apply the grid settings.
      this.LoadSettings();
    }

    private void applySettingsButton_Click( object sender, RoutedEventArgs e )
    {
      var item = predefinedSettingsCombo.SelectedItem as PredefinedSettingsItem;
      if( item == null )
        return;

      // Loads the appropriate Xml settings file.
      var serializer = new XmlSerializer( typeof( SettingsRepository ) );
      var settings = serializer.Deserialize( Application.GetResourceStream( new Uri( item.XmlUri ) ).Stream ) as SettingsRepository;

      // Change the current applied settings.
      if( settings != null )
      {
        this.ChangeCurrentSettings( settings );
      }
    }

    #endregion EVENT HANDLERS

    #region PRIVATE METHODS

    private void ChangeCurrentSettings( SettingsRepository settings )
    {
      this.CurrentSettings = settings;
      this.LoadSettings();
    }

    private UserSettings GetUserSettings()
    {
      var userSettings = UserSettings.None;

      if( chkCardWidths.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.CardWidths;

      if( chkColumnPositions.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.ColumnPositions;

      if( chkColumnTitles.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.ColumnTitle;

      if( chkColumnVisibilities.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.ColumnVisibilities;

      if( chkColumnWidths.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.ColumnWidths;

      if( chkFixedColumnCounts.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.FixedColumnCounts;

      if( chkGrouping.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.Grouping;

      if( chkSorting.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.Sorting;

      if( chkFilterCriteria.IsChecked.GetValueOrDefault( false ) )
        userSettings |= UserSettings.FilterCriteria;

      return userSettings;
    }

    private void LoadSettings()
    {
      if( this.CurrentSettings != null )
      {
        grid.LoadUserSettings( this.CurrentSettings, this.GetUserSettings() );
      }
    }

    private void SaveSettings()
    {
      if( this.CurrentSettings == null )
      {
        this.CurrentSettings = new SettingsRepository();
      }

      grid.SaveUserSettings( this.CurrentSettings, this.GetUserSettings() );
    }

    #endregion PRIVATE METHODS
  }
}
