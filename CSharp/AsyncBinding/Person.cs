/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Person.cs]
 *  
 * Class that exposes information about a person.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
{
  public class Person : INotifyPropertyChanged
  {
    #region Constructors

    public Person()
      : base()
    {
      string[] names = RandomValueGenerator.GetRandomName().Split( new char[ 1 ] { ' ' } );
      m_firstName = names[ 0 ].Trim();
      m_lastName = names[ 1 ].Trim();

      var currentYear = DateTime.UtcNow.Year;

      m_id = RandomValueGenerator.GetRandomInteger( 100, 1000 );
      m_age = RandomValueGenerator.GetRandomInteger( 18, 65 );
      m_hireDate = RandomValueGenerator.GetRandomDateTime( 1990, currentYear );
    }

    #endregion

    #region Person Properties

    public int ID
    {
      get
      {
        return m_id;
      }
      set
      {
        m_id = value;
        this.OnPropertyChanged( "ID" );
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
        this.OnPropertyChanged( "FirstName" );
      }
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
        this.OnPropertyChanged( "LastName" );
      }
    }

    public int Age
    {
      get
      {
        return m_age;
      }
      set
      {
        m_age = value;
        this.OnPropertyChanged( "Age" );
      }
    }

    public string Department
    {
      get
      {
        if( string.IsNullOrEmpty( m_department ) )
        {
          System.Threading.Thread.Sleep( 1000 );
          m_department = RandomValueGenerator.GetRandomDepartment();
        }
        return m_department;
      }
      set
      {
        if( m_department == value )
          return;
        m_department = value;
        this.OnPropertyChanged( "Department" );
      }
    }

    public string Boss
    {
      get
      {
        if( string.IsNullOrEmpty( m_boss ) )
        {
          System.Threading.Thread.Sleep( 2500 );
          m_boss = RandomValueGenerator.GetRandomName();
        }
        return m_boss;
      }
      set
      {
        if( m_boss == value )
          return;
        m_boss = value;
        this.OnPropertyChanged( "Boss" );
      }
    }

    public DateTime HireDate
    {
      get
      {
        return m_hireDate;
      }
      set
      {
        m_hireDate = value;
        this.OnPropertyChanged( "HireDate" );
      }
    }

    #endregion Person Properties

    #region INotifyPropertyChanged handling

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged( string propertyName )
    {
      if( PropertyChanged != null )
      {
        PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
      }
    }

    #endregion

    #region Private Fields

    // Person properties
    private string m_firstName = string.Empty;
    private string m_lastName = string.Empty;
    private string m_department = string.Empty;
    private string m_boss = string.Empty;
    private DateTime m_hireDate = DateTime.MinValue;
    private int m_age = 18;
    private int m_id = 100;

    #endregion
  }
}
