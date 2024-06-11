/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Person.cs]
 *  
 * Class that exposes information about a person and to which "extra"
 * properties can be added through a custom PropertyDescriptor.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
{
  public class Person : ICustomTypeDescriptor
  {
    #region Constructors

    public Person()
      : base()
    {
      string[] names = RandomValueGenerator.GetRandomName().Split( new char[ 1 ] { ' ' } );
      m_firstName = names[ 0 ].Trim();
      m_lastName = names[ 1 ].Trim();

      var currentYear = DateTime.UtcNow.Year;

      m_age = RandomValueGenerator.GetRandomInteger( 18, 65 );
      m_hireDate = RandomValueGenerator.GetRandomDateTime( 1990, currentYear );
      m_children = RandomValueGenerator.GetRandomInteger( 0, 4 );
      m_employed = RandomValueGenerator.GetRandomBool();
      m_phone = RandomValueGenerator.GetRandomPhoneNumber();
      m_department = RandomValueGenerator.GetRandomDepartment();
      m_boss = RandomValueGenerator.GetRandomName();
      m_reviewDate = RandomValueGenerator.GetRandomDateTime( m_hireDate.Year, currentYear );
    }

    public Person( int additionalFieldCount )
      : this()
    {
      m_additionalFieldCount = additionalFieldCount;
    }

    #endregion

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

    // Get the default property count, which is used to determine the actaul
    // number of custom PropertyDescriptors to "attach" to the Person class.
    public static int DefaultPropertyCount
    {
      get
      {
        if( m_defaultPropertyCount == 0 )
          m_defaultPropertyCount = TypeDescriptor.GetProperties( typeof( Person ) ).Count;

        return m_defaultPropertyCount;
      }
    }

    internal object GetDynamicPropertyValue( CustomPropertyDescriptor propertyDescriptor )
    {
      string propertyName = propertyDescriptor.Name;
      object propertyValue;

      if( !m_dynamicPropertyValues.TryGetValue( propertyName, out propertyValue ) )
      {
        propertyValue = RandomValueGenerator.GetRandomValue( propertyDescriptor.PropertyType );
        m_dynamicPropertyValues.Add( propertyName, propertyValue );
      }

      return propertyValue;
    }

    #region ICustomTypeDescriptor

    public AttributeCollection GetAttributes()
    {
      return AttributeCollection.Empty;
    }

    public string GetClassName()
    {
      return null;
    }

    public string GetComponentName()
    {
      return null;
    }

    public TypeConverter GetConverter()
    {
      return null;
    }

    public EventDescriptor GetDefaultEvent()
    {
      return null;
    }

    public PropertyDescriptor GetDefaultProperty()
    {
      return null;
    }

    public object GetEditor( Type editorBaseType )
    {
      return null;
    }

    public EventDescriptorCollection GetEvents( Attribute[] attributes )
    {
      return EventDescriptorCollection.Empty;
    }

    public EventDescriptorCollection GetEvents()
    {
      return EventDescriptorCollection.Empty;
    }

    public PropertyDescriptorCollection GetProperties( Attribute[] attributes )
    {
      // If there are no additional fields to add, return the default properties.
      if( m_additionalFieldCount <= 0 )
        return TypeDescriptor.GetProperties( this, true );

      if( m_combinedProperties == null )
      {
        // Get a list of the default properties exposed by the Person class and add them to
        // a collection that will also contain the new PropertyDescriptors
        List<PropertyDescriptor> list = new List<PropertyDescriptor>();
        PropertyDescriptorCollection baseProperties = TypeDescriptor.GetProperties( this, true );

        foreach( PropertyDescriptor property in baseProperties )
        {
          list.Add( property );
        }

        // Create a new PropertyDescriptor according to the type at the specified index. 
        int index = 0;
        for( int i = 0; i < m_additionalFieldCount; i++ )
        {
          list.Add( this.GetNewPropertyDescriptor( Person.AvailableColumnTypes[ index ] ) );

          if( index < Person.AvailableColumnTypes.Count - 1 )
          {
            index++;
          }
          else
          {
            index = 0;
          }
        }

        // Add the new PropertyDescriptors to the collection.
        m_combinedProperties = new PropertyDescriptorCollection( list.ToArray() );
      }

      return m_combinedProperties;
    }

    // Returns a new PropertyDescriptor that is created based on the specified type.
    private PropertyDescriptor GetNewPropertyDescriptor( Type type )
    {
      PropertyDescriptor newProperty = new CustomPropertyDescriptor( type.ToString().Remove( 0, 7 ) + " Column " + m_index.ToString(), typeof( Person ), type, true );

      m_index++;
      return newProperty;
    }

    public PropertyDescriptorCollection GetProperties()
    {
      return this.GetProperties( null );
    }

    public object GetPropertyOwner( PropertyDescriptor pd )
    {
      return this;
    }

    #endregion ICustomTypeDescriptor

    #region Private Fields

    private static int m_defaultPropertyCount = 0;
    private static List<Type> AvailableColumnTypes = new List<Type>( new Type[] { typeof( string ), typeof( int ), typeof( DateTime ), typeof( bool ) } );

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

    // Used in GetProperties and GetNewPropertyDescriptor methods to create
    // custom PropertyDescriptors.
    private PropertyDescriptorCollection m_combinedProperties;
    private int m_additionalFieldCount = 0;
    private int m_index = 0;

    private Dictionary<string, object> m_dynamicPropertyValues = new Dictionary<string, object>();

    #endregion
  }
}
