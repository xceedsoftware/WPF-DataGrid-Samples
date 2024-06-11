/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MinimumRatingGroupConfigurationSelector.cs]
 *  
 * This class implements a GroupConfigurationSelector that
 * sets the ItemContainerStyle for the items whose Rating field is
 * at least MinimumRating when the DataGridControl is grouped by
 * Rating
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.Grouping
{
  public class MinimumRatingGroupConfigurationSelector : GroupConfigurationSelector
  {

    #region PUBLIC CONSTRUCTORS

    public MinimumRatingGroupConfigurationSelector( int minimumRating )
    {
      this.MinimumRating = minimumRating;
    }

    #endregion

    #region PUBLIC PROPERTIES

    public int MinimumRating
    {
      get
      {
        return m_minItemCount;
      }
      set
      {
        if( value != m_minItemCount )
          m_minItemCount = value;
      }
    }

    #endregion

    #region PUBLIC METHODS

    public override GroupConfiguration SelectGroupConfiguration( int groupLevel,
                   System.Windows.Data.CollectionViewGroup collectionViewGroup,
                   System.ComponentModel.GroupDescription groupDescription )
    {
      GroupConfiguration groupConfiguration = null;

      if( collectionViewGroup == null )
        return base.SelectGroupConfiguration( groupLevel, collectionViewGroup, groupDescription );

      string fieldName = string.Empty;

      DataGridGroupDescription dataGridGroupDescription = groupDescription as DataGridGroupDescription;
      if( dataGridGroupDescription != null )
      {
        fieldName = dataGridGroupDescription.PropertyName;
      }
      else
      {
        PropertyGroupDescription propertyGroupDescription = groupDescription as PropertyGroupDescription;
        if( propertyGroupDescription != null )
        {
          fieldName = propertyGroupDescription.PropertyName;
        }
      }

      if( String.IsNullOrEmpty( fieldName ) == true )
        return base.SelectGroupConfiguration( groupLevel, collectionViewGroup, groupDescription );

      // We want this Selector to react only for "Rating" field
      if( fieldName.Equals( "Rating" ) == false )
        return base.SelectGroupConfiguration( groupLevel, collectionViewGroup, groupDescription );

      int rating = 0;

      if( Int32.TryParse( collectionViewGroup.Name.ToString(), out rating ) == true )
      {
        bool higherThan = ( rating >= this.MinimumRating );

        if( m_ratingHigherThanGroupConfiguration.ContainsKey( higherThan ) == true )
        {
          groupConfiguration = m_ratingHigherThanGroupConfiguration[ higherThan ];
        }
        else
        {
          Style style = new Style( typeof( Xceed.Wpf.DataGrid.DataRow ) );

          if( higherThan )
          {
            // the group's rating is high enough
            style.Setters.Add( new Setter( DataRow.BackgroundProperty, new SolidColorBrush( Color.FromArgb( 0x33, 0x00, 0x99, 0xcc ) ) ) );
          }
          else
          {
            // the group's rating is not high enough
            style.Setters.Add( new Setter( DataRow.BackgroundProperty, new SolidColorBrush( Color.FromArgb( 0x33, 0xff, 0x99, 0x00 ) ) ) );
          }

          // Create a new GroupConfiguration for this rating
          groupConfiguration = new GroupConfiguration();
          groupConfiguration.ItemContainerStyle = style;

          m_ratingHigherThanGroupConfiguration.Add( higherThan, groupConfiguration );
        }
      }
      else
      {
        Debug.WriteLine( "Error while converting rating to Int32" );
      }

      return groupConfiguration;
    }

    #endregion

    #region PRIVATE FIELDS

    private int m_minItemCount = 0;
    private Dictionary<bool, GroupConfiguration> m_ratingHigherThanGroupConfiguration = new Dictionary<bool, GroupConfiguration>();

    #endregion
  }
}
