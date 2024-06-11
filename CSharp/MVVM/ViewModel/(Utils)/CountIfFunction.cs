/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CountIfFunction.cs]
 *  
 * This class implements a custom statistic used for the Discontinued column.  It only counts items that have a specific boolean value.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using Xceed.Wpf.DataGrid.Stats;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
{
  public class CountIfFunction : CumulativeStatFunction
  {
    public CountIfFunction()
      : base()
    {
    }

    #region ValueToCountIsTrue Property

    public bool ValueToCountIsTrue
    {
      get
      {
        return m_valueToCountIsTrue;
      }
      set
      {
        this.CheckSealed();
        m_valueToCountIsTrue = value;
      }
    }

    private bool m_valueToCountIsTrue = false;

    #endregion

    protected override void InitializeFromInstance( StatFunction source )
    {
      base.InitializeFromInstance( source );

      //This determines if true values are counted, or false values.
      this.ValueToCountIsTrue = ( ( CountIfFunction )source ).ValueToCountIsTrue;
    }

    protected override void Reset()
    {
      m_count = 0;
    }

    protected override void Accumulate( object[] values )
    {
      if( values.Length > 0 )
      {
        var value = Convert.ToBoolean( values[ 0 ] );

        if( ( this.ValueToCountIsTrue && value ) || ( !this.ValueToCountIsTrue && !value ) )
        {
          checked
          {
            m_count++;
          }
        }
      }
    }

    protected override void AccumulateChildResult( StatResult childResult )
    {
      checked
      {
        m_count += Convert.ToInt64( childResult.Value );
      }
    }

    protected override StatResult GetResult()
    {
      return new StatResult( m_count );
    }

    protected override bool IsEquivalent( StatFunction statFunction )
    {
      var countIfFunction = statFunction as CountIfFunction;

      return ( base.IsEquivalent( countIfFunction ) ) && ( countIfFunction.ValueToCountIsTrue == this.ValueToCountIsTrue );
    }

    private long m_count;
  }
}
