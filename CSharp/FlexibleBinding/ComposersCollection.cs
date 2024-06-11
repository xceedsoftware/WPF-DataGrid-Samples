/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ComposersCollection.cs]
 *  
 * This class contains a basic collection of composer objects
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
using System.Diagnostics;

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  public class ComposersCollection : CollectionBase, ITypedList
  {
    public ComposersCollection()
    {
    }

    #region Implementation for the typed collection

    // Add a composer to the collection.  Used to fill the collection.
    public void Add( Composer composer )
    {
      this.InnerList.Add( composer );
    }

    public Composer this[ int index ]
    {
      get
      {
        return ( Composer )this.InnerList[ index ];
      }
      set
      {
        this.InnerList[ index ] = value;
      }
    }

    protected override void OnValidate( object value )
    {
      // Check to make sure that the element is not null
      base.OnValidate( value );

      // Check to make sure that the element is a ComposerEditable object
      if( !( value is Composer ) )
        throw new ArgumentException( "Only Composer objects can be added to the collection!" );
    }

    #endregion Implementation for the typed collection

    #region Implementation of ITypedList

    public PropertyDescriptorCollection GetItemProperties( PropertyDescriptor[] properties )
    {
      // We sort the property descriptor to have the column in our grid in that order by default.
      PropertyDescriptorCollection original = TypeDescriptor.GetProperties( typeof( Composer ) );
      PropertyDescriptorCollection sorted = new PropertyDescriptorCollection( null );

      sorted.Add( original[ "LastName" ] );
      sorted.Add( original[ "FirstName" ] );
      sorted.Add( original[ "Period" ] );
      sorted.Add( original[ "BirthYear" ] );
      sorted.Add( original[ "DeathYear" ] );

      Debug.Assert( original.Count == sorted.Count );

      return sorted;
    }

    public string GetListName( PropertyDescriptor[] listAccessors )
    {
      return this.GetType().Name;
    }

    #endregion Implementation of ITypedList
  }
}
