/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Table View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [FilterDatePicker.xaml.cs]
 * 
 * This class implements the various properties offered by the date range
 * picker used in some FilterCells.
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

using Xceed.Wpf.DataGrid.FilterCriteria;

namespace Xceed.Wpf.DataGrid.Samples.TableView
{
  public partial class FilterDatePicker : UserControl
  {
    public FilterDatePicker()
    {
      InitializeComponent();
    }

    #region FilterCriterion Property

    public static readonly DependencyProperty FilterCriterionProperty = DependencyProperty.Register(
      "FilterCriterion",
      typeof( FilterCriterion ),
      typeof( FilterDatePicker ),
      new FrameworkPropertyMetadata(
        null,
        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        new PropertyChangedCallback( FilterDatePicker.OnFilterCriterionChanged ) ) );

    public FilterCriteria.FilterCriterion FilterCriterion
    {
      get
      {
        return ( FilterCriteria.FilterCriterion )this.GetValue( FilterDatePicker.FilterCriterionProperty );
      }
      set
      {
        this.SetValue( FilterDatePicker.FilterCriterionProperty, value );
      }
    }

    // When the FilterCriterion is modified from outside this editor (with an initial
    // FilterCriterion on a DataGridItemProperty for instance), try our best to reflect
    // it in the StartDate and EndDate properties.
    private static void OnFilterCriterionChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
    {
      FilterDatePicker filterDatePicker = ( FilterDatePicker )sender;

      if( filterDatePicker.m_updating )
        return;

      filterDatePicker.m_updating = true;

      try
      {
        GreaterThanOrEqualToFilterCriterion greaterThanOrEqualFilterCriterion = null;
        LessThanOrEqualToFilterCriterion lessThanOrEqualToFilterCriterion = null;
        AndFilterCriterion andFilterCriterion = e.NewValue as AndFilterCriterion;

        if( andFilterCriterion == null )
        {
          greaterThanOrEqualFilterCriterion = e.NewValue as GreaterThanOrEqualToFilterCriterion;
          lessThanOrEqualToFilterCriterion = e.NewValue as LessThanOrEqualToFilterCriterion;
        }
        else
        {
          // Extract the StartDate and EndDate from this "AndFilterCriterion", if possible.
          greaterThanOrEqualFilterCriterion = andFilterCriterion.FirstFilterCriterion as GreaterThanOrEqualToFilterCriterion;
          lessThanOrEqualToFilterCriterion = andFilterCriterion.SecondFilterCriterion as LessThanOrEqualToFilterCriterion;

          if( greaterThanOrEqualFilterCriterion == null )
          {
            greaterThanOrEqualFilterCriterion = andFilterCriterion.SecondFilterCriterion as GreaterThanOrEqualToFilterCriterion;
          }

          if( lessThanOrEqualToFilterCriterion == null )
          {
            lessThanOrEqualToFilterCriterion = andFilterCriterion.FirstFilterCriterion as LessThanOrEqualToFilterCriterion;
          }
        }

        if( greaterThanOrEqualFilterCriterion == null )
        {
          filterDatePicker.StartDate = DateTime.MinValue;
        }
        else
        {
          filterDatePicker.StartDate = ( DateTime )greaterThanOrEqualFilterCriterion.Value;
        }

        if( lessThanOrEqualToFilterCriterion == null )
        {
          filterDatePicker.EndDate = DateTime.MaxValue;
        }
        else
        {
          filterDatePicker.EndDate = ( DateTime )lessThanOrEqualToFilterCriterion.Value;
        }
      }
      finally
      {
        filterDatePicker.m_updating = false;
      }
    }

    #endregion

    #region StartDate Property

    public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(
      "StartDate",
      typeof( DateTime ),
      typeof( FilterDatePicker ),
      new FrameworkPropertyMetadata(
        DateTime.MinValue,
        new PropertyChangedCallback( FilterDatePicker.OnStartDateChanged ) ) );

    public DateTime StartDate
    {
      get
      {
        return ( DateTime )this.GetValue( FilterDatePicker.StartDateProperty );
      }
      set
      {
        this.SetValue( FilterDatePicker.StartDateProperty, value );
      }
    }

    private static void OnStartDateChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
    {
      ( ( FilterDatePicker )sender ).UpdateFilterCriterion();
    }

    #endregion

    #region EndDate Property

    public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(
      "EndDate",
      typeof( DateTime ),
      typeof( FilterDatePicker ),
      new FrameworkPropertyMetadata(
        DateTime.MaxValue,
        new PropertyChangedCallback( FilterDatePicker.OnEndDateChanged ) ) );

    public DateTime EndDate
    {
      get
      {
        return ( DateTime )this.GetValue( FilterDatePicker.EndDateProperty );
      }
      set
      {
        this.SetValue( FilterDatePicker.EndDateProperty, value );
      }
    }

    private static void OnEndDateChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
    {
      ( ( FilterDatePicker )sender ).UpdateFilterCriterion();
    }

    #endregion

    private void UpdateFilterCriterion()
    {
      if( m_updating )
        return;

      m_updating = true;

      try
      {
        if( this.StartDate > DateTime.MinValue )
        {
          if( this.EndDate < DateTime.MaxValue )
          {
            this.FilterCriterion = new AndFilterCriterion(
              new GreaterThanOrEqualToFilterCriterion( this.StartDate ),
              new LessThanOrEqualToFilterCriterion(
                new DateTime( this.EndDate.Year, this.EndDate.Month, this.EndDate.Day, 23, 59, 59 ) ) );
          }
          else
          {
            this.FilterCriterion = new GreaterThanOrEqualToFilterCriterion( this.StartDate );
          }
        }
        else if( this.EndDate < DateTime.MaxValue )
        {
          this.FilterCriterion = new LessThanOrEqualToFilterCriterion(
            new DateTime( this.EndDate.Year, this.EndDate.Month, this.EndDate.Day, 23, 59, 59 ) );
        }
      }
      finally
      {
        m_updating = false;
      }
    }

    private bool m_updating = false;
  }
}
