/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CountIfFunction.cs]
 *  
 * This file demonstrates how to create a custom statistical function that can be used in 
 * the Xceed DataGridControl.
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
using System.Text.RegularExpressions;
using Xceed.Wpf.DataGrid.Stats;

namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
{
  // This statistical function derives from CumulativeStatFunction because it can 
  // accumulate "partial" results. For instance, results of sub-group. This allows
  // for better performance.
  public class CountIfFunction : CumulativeStatFunction
  {
    // A parameterless constructor is necessary to use the class in XAML.
    public CountIfFunction()
      : base()
    {
      m_conditions = new ObservableCollection<string>();
      m_conditions.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler( m_conditions_CollectionChanged );
    }

    // Initialize a new instance of the CountIfFunction specifying the ResultPropertyName 
    // and the SourcePropertyName.
    public CountIfFunction( string resultPropertyName, string sourcePropertyNames )
      : base( resultPropertyName, sourcePropertyNames )
    {
      m_conditions = new ObservableCollection<string>();
      m_conditions.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler( m_conditions_CollectionChanged );
    }

    // Each condition applies to the values of the corresponding source property name
    // (e.g., the first condition applies to the values of the first source property name).
    // Gets the conditions that will be applied to the various values.
    public ObservableCollection<string> Conditions
    {
      get
      {
        return m_conditions;
      }
    }

    // When the grid needs to create temporary CountIfFunction instances for its 
    // calculation, this method will be called. Be sure to initialize everything 
    // having an impact on the result here.
    protected override void InitializeFromInstance( StatFunction source )
    {
      base.InitializeFromInstance( source );

      foreach( string condition in ( ( CountIfFunction )source ).Conditions )
        this.Conditions.Add( condition );
    }

    // Validate the CountIf statistical function to make sure that it is capable
    // to calculate its result. In our case, we need to make sure that a ResultPropertyName
    // has been specified and that we have the same number of source property names
    // as conditions.
    protected override void Validate()
    {
      if( ( this.ResultPropertyName == null ) ||
          ( this.ResultPropertyName == string.Empty ) ||
          ( m_conditions.Count != this.SourcePropertyName.Split( ',' ).Length ) )
      {
        throw new InvalidOperationException();
      }
    }

    // This method will be called when a new calculation is about to begin.
    protected override void Reset()
    {
      m_count = 0;
    }

    // This method will be called for each data item that is part of the set (a group or 
    // the grid).
    protected override void Accumulate( object[] values )
    {
      for( int i = 0; i < m_conditions.Count; i++ )
      {
        // As soon as one condition does not match is associated value, we simply
        // return without having done the accumulation (the count increment).
        if( !Regex.IsMatch( values[ i ].ToString(), m_conditions[ i ] ) )
          return;
      }

      // In case of an overflow, we want to stop the calculation and report the error.
      checked
      {
        m_count++;
      }
    }

    // This method will be called when calculating the result of a group having 
    // sub-groups. Obviously, it will be called once for each sub-group.
    protected override void AccumulateChildResult( StatResult childResult )
    {
      checked
      {
        m_count += Convert.ToInt64( childResult.Value );
      }
    }

    // This method should return the result calculated so far.
    protected override StatResult GetResult()
    {
      return new StatResult( m_count );
    }

    // The addition of the Conditions property, which influences the result of the
    // statistical function, the CountIf function requires us to override IsEquivalent 
    // and GetEquivalenceKey to return a new key when 2 instances are compared.
    protected override bool IsEquivalent( StatFunction statFunction )
    {
      CountIfFunction countIfFunction = statFunction as CountIfFunction;

      if( countIfFunction == null )
        return false;

      if( m_conditions.Count != countIfFunction.Conditions.Count )
        return false;

      for( int i = 0; i < m_conditions.Count; i++ )
      {
        if( m_conditions[ i ] != countIfFunction.Conditions[ i ] )
          return false;
      }

      return base.IsEquivalent( statFunction );
    }

    protected override int GetEquivalenceKey()
    {
      int hashCode = base.GetEquivalenceKey();

      for( int i = 0; i < m_conditions.Count; i++ )
        hashCode ^= m_conditions[ i ].GetHashCode();

      return hashCode;
    }

    // Do not allow the Conditions property to be changed if the statistical function has
    // been sealed (i.e, assigned to the DataGridCollectionView.StatFunctions property).
    private void m_conditions_CollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
    {
      this.CheckSealed();
    }

    private ObservableCollection<string> m_conditions;
    private long m_count;
  }
}
