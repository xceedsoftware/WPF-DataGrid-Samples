/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Composer.cs]
 *  
 * This class contains information about a composer and advises its containing list
 * when the value of one of the properties is changing.
 * It is used by the ComposerBindingList.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Collections;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  // The ComposerBindingList and the ComposerCollection use elements of this type.
  public class Composer : IDataErrorInfo
  {
    public Composer()
    {
    }

    // Create a new instance of the Composer class.
    public Composer( string lastName, string firstName, Period period, int birthYear, int deathYear, int compositionCount, DateTime lastUpdate )
    {
      m_lastName = lastName;
      m_firstName = firstName;
      m_period = period;
      m_birthYear = birthYear;
      m_deathYear = deathYear;
      m_compositionCount = compositionCount;
      m_lastUpdate = lastUpdate;
    }

    public string LastName
    {
      get
      {
        return m_lastName;
      }

      set
      {
        if( string.IsNullOrEmpty( value ) )
          throw new ArgumentException( "The composer last name is required.", LastName );

        m_lastName = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }

    public string FirstName
    {
      get
      {
        return m_firstName;
      }

      set
      {
        m_firstName = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }

    public Period Period
    {
      get
      {
        return m_period;
      }

      set
      {
        m_period = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }

    public int BirthYear
    {
      get
      {
        return m_birthYear;
      }

      set
      {
        // Check the year to make sure that it is something reasonable
        if( value < 0 )
          throw new ArgumentOutOfRangeException( "BirthYear", value, "The date must be greater than 0" );

        m_birthYear = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }

    public int DeathYear
    {
      get
      {
        return m_deathYear;
      }

      set
      {
        // Check the year to make sure that it is something reasonable
        if( value < 0 )
          throw new ArgumentOutOfRangeException( "DeathYear", value, "The date must be greater than 0" );

        m_deathYear = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }


    public int CompositionCount
    {
      get
      {
        return m_compositionCount;
      }
      set
      {
        m_compositionCount = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }


    public DateTime LastUpdate
    {
      get
      {
        return m_lastUpdate;
      }
      set
      {
        // Check the year to make sure that it is something reasonable
        if( value > DateTime.Now )
          throw new ArgumentOutOfRangeException( "LastUpdate", value, "The date must be less than " + DateTime.Now.ToString() );

        m_lastUpdate = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }


    // Raise the ListChanged event of our containing list.
    protected virtual void InvokeListChanged( ListChangedEventArgs e )
    {
      ( ( ComposersBindingListComplete )m_containingList ).InvokeOnListChanged( e );
    }

    #region IDataErrorInfo Members

    public string Error
    {
      get { return string.Empty; }
    }

    public string this[ string columnName ]
    {
      get
      {
        string error = string.Empty;

        switch( columnName )
        {
          case "BirthYear":
          case "DeathYear":
            {
              if( ( m_deathYear - BirthYear ) > 99 )
                error = "The composer would have lived a hundred years!";

              break;
            }

          case "Period":
            {
              if( m_period == Period.Undefined )
                error = "The composer's period should be specified if possible.";

              break;
            }

          case "FirstName":
            {
              if( String.IsNullOrEmpty( m_firstName ) )
                error = "The composer's first name should be specified if possible.";

              break;
            }

        }

        return error;
      }
    }

    #endregion

    internal IList m_containingList = null;

    protected string m_lastName = string.Empty;
    protected string m_firstName = string.Empty;
    protected Period m_period = Period.Undefined;
    protected int m_birthYear = DateTime.MinValue.Year;
    protected int m_deathYear = DateTime.MaxValue.Year;
    protected int m_compositionCount = 1;
    protected DateTime m_lastUpdate = DateTime.Now;

  }
}
