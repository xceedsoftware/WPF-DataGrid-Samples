/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Grouping
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    #region PUBLIC CONSTRUCTOR

    public MainPage()
    {
      InitializeComponent();

      DataGridContext context = DataGridControl.GetDataGridContext( this.grid );

      if( context == null )
        throw new NullReferenceException( "context" );

      this.groupCombo.SelectedIndex = 0;

      // Register to DataGridContext's GroupLevelDescriptions CollectiionChanged
      ( ( INotifyCollectionChanged )context.GroupLevelDescriptions ).CollectionChanged += new NotifyCollectionChangedEventHandler( this.GroupLevelDescriptionsCollectionChanged );
    }

    #endregion

    #region PRIVATE PROPERTIES

    public bool IsRatingLastGroupLevelDescription
    {
      get
      {
        bool returnedValue = false;

        int count = this.grid.GroupLevelDescriptions.Count;

        // If the last GroupLevelDescription is Rating, enable custom group
        if( ( count > 0 ) && ( this.grid.GroupLevelDescriptions[ count - 1 ].FieldName.Equals( "Rating" ) ) )
          returnedValue = true;

        return returnedValue;
      }
    }

    #endregion

    #region PRIVATE METHODS

    private void GroupLevelDescriptionsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
    {
      // Update useCustomGroupConfigurationSelector ComboBox state      
      useCustomGroupConfigurationSelector.IsEnabled = this.IsRatingLastGroupLevelDescription;

      ObservableCollection<GroupDescription> gridGroupDescriptions = this.grid.Items.GroupDescriptions;

      if( gridGroupDescriptions.Count == 0 )
      {
        removeAllGroupsButton.IsEnabled = false;
        removeFirstGroupLevelButton.IsEnabled = false;
      }
      else
      {
        removeAllGroupsButton.IsEnabled = true;
        removeFirstGroupLevelButton.IsEnabled = true;
      }

      if( m_updatingGrouping )
        return;

      GroupingItem currentGrouping = null;

      foreach( GroupingItem item in this.groupCombo.Items )
      {
        if( item.Equals( gridGroupDescriptions ) )
        {
          currentGrouping = item;
          break;
        }
      }

      this.groupCombo.SelectedItem = currentGrouping;
    }

    private void GroupComboSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      GroupingItem groupingItem = this.groupCombo.SelectedItem as GroupingItem;
      ObservableCollection<GroupDescription> gridGroupDescriptions = this.grid.Items.GroupDescriptions;

      if( ( groupingItem != null ) &&
          ( !groupingItem.Equals( gridGroupDescriptions ) ) )
      {
        m_updatingGrouping = true;

        using( this.grid.Items.DeferRefresh() )
        {
          gridGroupDescriptions.Clear();

          foreach( GroupDescription groupDesc in groupingItem.GroupDescriptions )
          {
            gridGroupDescriptions.Add( groupDesc );
          }
        }

        m_updatingGrouping = false;
      }
    }

    private void MinimumRatingComboBoxSelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      this.UpdateGroupConfigurationSelectorValue();
    }

    private void RemoveAllGroupsClicked( object sender, RoutedEventArgs e )
    {
      this.grid.Items.GroupDescriptions.Clear();
    }

    private void RemoveFirstGroupLevelClicked( object sender, RoutedEventArgs e )
    {
      if( this.grid.Items.GroupDescriptions.Count > 0 )
        this.grid.Items.GroupDescriptions.RemoveAt( 0 );
    }

    private void UpdateGroupConfigurationSelectorValue()
    {
      if( this.grid == null )
        return;

      CheckBox checkBox = this.useCustomGroupConfigurationSelector;

      if( checkBox == null )
        return;

      ComboBox comboBox = this.minimumRatingComboBox;

      if( comboBox == null )
        return;

      if( ( comboBox.SelectedIndex == -1 ) || ( checkBox.IsChecked.GetValueOrDefault() == false ) )
      {
        this.grid.GroupConfigurationSelector = null;
      }
      else
      {
        int selectedRating = 0;

        if( Int32.TryParse( comboBox.SelectedItem.ToString(), out selectedRating ) == false )
        {
          // There was an error while converting value
          this.grid.GroupConfigurationSelector = null;
        }
        else
        {
          this.grid.GroupConfigurationSelector = new MinimumRatingGroupConfigurationSelector( selectedRating );
        }
      }
    }

    private void UseCustomGroupConfigurationSelectorCheckBoxChecked( object sender, RoutedEventArgs e )
    {
      CheckBox checkBox = sender as CheckBox;

      if( checkBox == null )
        return;

      if( checkBox.IsChecked.GetValueOrDefault() == true )
      {
        ISupportInitialize iSupportInitialize = this.grid as ISupportInitialize;

        if( iSupportInitialize != null )
          iSupportInitialize.BeginInit();

        this.UpdateGroupConfigurationSelectorValue();

        if( iSupportInitialize != null )
          iSupportInitialize.EndInit();
      }
      else
      {
        this.grid.GroupConfigurationSelector = null;
      }
    }

    #endregion

    #region PRIVATE FIELDS

    private bool m_updatingGrouping = false;

    #endregion
  }
}
