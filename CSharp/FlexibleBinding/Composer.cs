/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Composer.cs]
 *  
 * This class contains information about a composer and advises its containing list
 * when the value of one of the properties is changing.
 * It is used by the ComposerBindingList and the ComposerCollection.
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

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  // The ComposerBindingList and the ComposerCollection use elements of this type.
  public class Composer
  {
    public Composer()
    {
    }

    // Create a new instance of the Composer class.
    public Composer( string lastName, string firstName, Period period, int birthYear, int deathYear )
    {
      m_lastName = lastName;
      m_firstName = firstName;
      m_period = period;
      m_birthYear = birthYear;
      m_deathYear = deathYear;
    }

    public string LastName
    {
      get
      {
        return m_lastName;
      }

      set
      {
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
        if( ( value > DateTime.Now.Year ) || ( value < 0 ) )
          throw new ArgumentOutOfRangeException( "BirthYear", value, "The date must be less than " + DateTime.Now.Year.ToString() + " and greater than 0" );

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
        if( ( value > DateTime.Now.Year ) || ( value < 0 ) )
          throw new ArgumentOutOfRangeException( "DeathYear", value, "The date must be less than " + DateTime.Now.Year.ToString() + " and greater than 0" );

        m_deathYear = value;

        // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
        if( m_containingList != null )
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
      }
    }

    // Raise the ListChanged event of our containing list.
    protected virtual void InvokeListChanged( ListChangedEventArgs e )
    {
      ( ( ComposersBindingList )m_containingList ).InvokeOnListChanged( e );
    }

    protected string m_lastName = string.Empty;
    protected string m_firstName = string.Empty;
    protected Period m_period = Period.Undefined;
    protected int m_birthYear = DateTime.MinValue.Year;
    protected int m_deathYear = DateTime.MaxValue.Year;

    internal IList m_containingList = null;
  }
}
