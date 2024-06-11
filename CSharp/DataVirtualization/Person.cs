/*
 * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Person.cs]
 *  
 * This class represents a typical business object used by
 * the DataVirtualization Sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  internal class Person
  {
    private static string[] Bosses = new string[ 10 ]
    {
      "Mr. " + RandomValueGenerator.GetRandomName(),
      "Mr. " + RandomValueGenerator.GetRandomName(),
      "Ms. " + RandomValueGenerator.GetRandomName(),
      "Mr. " + RandomValueGenerator.GetRandomName(),
      "Mr. " + RandomValueGenerator.GetRandomName(),
      "Dr. " + RandomValueGenerator.GetRandomName(),
      "Mlle. " + RandomValueGenerator.GetRandomName(),
      "Mr. " + RandomValueGenerator.GetRandomName(),
      "Ms. " + RandomValueGenerator.GetRandomName(),
      "Mr. " + RandomValueGenerator.GetRandomName(),
    };

    private static int CurrentYear = DateTime.Now.Year;

    public Person()
      : base()
    {
      string[] names = RandomValueGenerator.GetRandomName().Split( new char[ 1 ] { ' ' } );
      m_firstName = names[ 0 ].Trim();
      m_lastName = names[ 1 ].Trim();

      m_age = RandomValueGenerator.GetRandomInteger( 18, 65 );
      m_hireDate = RandomValueGenerator.GetRandomDateTime( 1990, DateTime.Now.Year );

      if( RandomValueGenerator.GetRandomBool() )
      {
        m_children = RandomValueGenerator.GetRandomInteger( 0, 3 );
      }
      else if( RandomValueGenerator.GetRandomBool() )
      {
        m_children = RandomValueGenerator.GetRandomInteger( 1, 5 );
      }
      else
      {
        m_children = RandomValueGenerator.GetRandomInteger( 3, 6 );
      }


      m_employed = RandomValueGenerator.GetRandomBool();
      m_phone = RandomValueGenerator.GetRandomPhoneNumber();
      m_department = RandomValueGenerator.GetRandomDepartment();

      m_boss = Person.Bosses[ RandomValueGenerator.GetRandomInteger( 0, 10 ) ];


      m_reviewDate = RandomValueGenerator.GetRandomDateTime( m_hireDate.Year, DateTime.Now.Year );
    }

    #region Default Person Properties

    public string FirstName
    {
      get
      {
        return m_firstName;
      }
    }

    public string LastName
    {
      get
      {
        return m_lastName;
      }
    }

    public int Age
    {
      get
      {
        return m_age;
      }
    }

    public DateTime HireDate
    {
      get
      {
        return m_hireDate;
      }
    }

    public int Children
    {
      get
      {
        return m_children;
      }
    }

    public bool Employed
    {
      get
      {
        return m_employed;
      }
    }

    public string PhoneNumber
    {
      get
      {
        return m_phone;
      }
    }

    public string Department
    {
      get
      {
        return m_department;
      }
    }

    public string Boss
    {
      get
      {
        return m_boss;
      }
    }

    public DateTime ReviewDate
    {
      get
      {
        return m_reviewDate;
      }
    }

    #endregion Default Person Properties

    // Default Person properties
    private string m_firstName = string.Empty;
    private string m_lastName = string.Empty;
    private string m_phone = string.Empty;
    private string m_department = string.Empty;
    private string m_boss = string.Empty;
    private DateTime m_reviewDate = DateTime.MinValue;
    private DateTime m_hireDate = DateTime.MinValue;
    private int m_age = 18;
    private int m_children = 0;
    private bool m_employed = true;
  }
}
