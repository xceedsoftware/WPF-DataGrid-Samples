/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [DateGroupDescription.cs]
 *  
 * This class implements a GroupDescription that can be used in a CollectionViewSource.
 * It groups the item by range of date (today, yesterday, this week, last week, 
 * last month, older).
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections;
using System.Globalization;

namespace Xceed.Wpf.DataGrid.Samples.Grouping
{
  public class DateGroupDescription : DataGridGroupDescription
  {
    public DateGroupDescription()
      : base()
    {
      this.SortComparer = m_dateGroupComparer;
    }

    public DateGroupDescription( string propertyName )
      : base( propertyName )
    {
      this.SortComparer = m_dateGroupComparer;
    }

    public override object GroupNameFromItem( object item, int level, CultureInfo cultureInfo )
    {
      object value = base.GroupNameFromItem( item, level, cultureInfo );

      try
      {
        DateTime dateValue = Convert.ToDateTime( value );

        int daysToStartOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek;

        // A positive number of days to the start of this week means that the 
        // FirstDayOfWeek is not a sunday and that today's weekday is between a sunday 
        // (included) and the first day of the week (excluded). Correct this delta.
        if( daysToStartOfWeek > 0 )
          daysToStartOfWeek -= 7;

        DateTime thisWeek = DateTime.Today.AddDays( daysToStartOfWeek );

        if( dateValue >= DateTime.Today )
        {
          value = "Today";
        }
        else if( dateValue >= DateTime.Today.AddDays( -1 ) )
        {
          value = "Yesterday";
        }
        else if( dateValue >= thisWeek )
        {
          value = "Previously This Week";
        }
        else if( dateValue >= thisWeek.AddDays( -7 ) )
        {
          value = "Last Week";
        }
        else if( dateValue >= thisWeek.AddDays( -30 ) )
        {
          value = "Last Month";
        }
        else
        {
          value = "Old stuff!";
        }
      }
      catch( InvalidCastException )
      {
      }

      return value;
    }

    private static DateGroupComparer m_dateGroupComparer = new DateGroupComparer();

    private class DateGroupComparer : IComparer
    {
      static DateGroupComparer()
      {
        m_nameValue.Add( "Old stuff!", 1 );
        m_nameValue.Add( "Last Month", 2 );
        m_nameValue.Add( "Last Week", 3 );
        m_nameValue.Add( "Previously This Week", 4 );
        m_nameValue.Add( "Yesterday", 5 );
        m_nameValue.Add( "Today", 6 );
      }

      #region IComparer Members

      public int Compare( object x, object y )
      {
        object xNameValue = m_nameValue[ x ];
        object yNameValue = m_nameValue[ y ];

        if( ( xNameValue == null ) || ( yNameValue == null ) )
          throw new InvalidOperationException( "An attempt was made to Compare two values when one or both values to compare does not exist in the column." );

        return ( int )xNameValue - ( int )yNameValue;
      }

      #endregion

      private static Hashtable m_nameValue = new Hashtable();
    }
  }
}
