/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CustomPropertyDescriptor.cs]
 *  
 * Custom PropertyDescriptor that will be used to add additional properties (columns)
 * on the Person objects.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
{
  internal class CustomPropertyDescriptor : PropertyDescriptor
  {
    public CustomPropertyDescriptor( string propertyName, Type componentType, Type propertyType, bool readOnly )
      : base( propertyName, null )
    {
      m_propertyName = propertyName;
      m_componentType = componentType;
      m_propertyType = propertyType;
      m_readOnly = readOnly;
    }

    public override bool CanResetValue( object component )
    {
      return false;
    }

    public override Type ComponentType
    {
      get
      {
        return m_componentType;
      }
    }

    public override object GetValue( object component )
    {
      return ( ( Person )component ).GetDynamicPropertyValue( this );
    }

    public override bool IsReadOnly
    {
      get
      {
        return m_readOnly;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return m_propertyType;
      }
    }

    public override void ResetValue( object component )
    {
      throw new NotImplementedException();
    }

    public override void SetValue( object component, object value )
    {
    }

    public override bool ShouldSerializeValue( object component )
    {
      return false;
    }

    private string m_propertyName;
    private Type m_componentType;
    private Type m_propertyType;
    private bool m_readOnly;
  }
}
