/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ComposersBindingList.cs]
 *  
 * This class contains a collection of composer object which implement the IBindingList 
 * interface with the minumum functionnality needed to support deletion, 
 * insertion and modification.
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
  public class ComposersBindingList : CollectionBase, IBindingList, ITypedList, ISupportInitialize
  {
    public ComposersBindingList()
    {
    }

    #region Implementation for the typed collection

    // Add a composer to the collection.  Used to fill the collection.
    public void Add( Composer composer )
    {
      this.List.Add( composer );
    }

    public Composer this[ int index ]
    {
      get
      {
        return ( Composer )this.List[ index ];
      }
      set
      {
        this.List[ index ] = value;
      }
    }

    protected override void OnValidate( object value )
    {
      // Check to make sure that the element is not null
      base.OnValidate( value );

      // Check to make sure that the element is a Composer object
      if( !( value is Composer ) )
        throw new ArgumentException( "Only Composer objects can be added to the collection!" );
    }

    #endregion Implementation for the typed collection

    #region Implementation of IBindingList

    // Return a new Composer object already added to the collection.
    // The grid will use it to insert a new row.
    public object AddNew()
    {
      // Create a new Composer object
      Composer composer = new Composer();

      // Add the composer to the collection.
      this.Add( composer );

      return composer;
    }

    // To tell that we support the AddNew method.
    public bool AllowNew
    {
      get
      {
        return true;
      }
    }

    // To tell that we support the removing of elements.
    public bool AllowRemove
    {
      get
      {
        return true;
      }
    }

    // To tell that we support the modification of our elements.
    public bool AllowEdit
    {
      get
      {
        return true;
      }
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public bool SupportsSorting
    {
      get
      {
        return false;
      }
    }

    // Since the grid does not need to search, we do not need
    // to enable searching in the collection.
    public bool SupportsSearching
    {
      get
      {
        return false;
      }
    }

    // To tell that we will raise the ListChanged event.
    public bool SupportsChangeNotification
    {
      get
      {
        return true;
      }
    }

    // Since the grid does not need to search, we do not need
    // to enable searching in the collection.
    public void AddIndex( PropertyDescriptor property )
    {
      throw new NotSupportedException();
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public void ApplySort( PropertyDescriptor property, ListSortDirection direction )
    {
      throw new NotSupportedException();
    }

    // Since the grid does not need to search, we do not need
    // to enable searching in the collection.
    public int Find( PropertyDescriptor property, object key )
    {
      throw new NotSupportedException();
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public void RemoveSort()
    {
      throw new NotSupportedException();
    }

    // Since the grid does not need to search, we do not need
    // to enable searching in the collection.
    public void RemoveIndex( PropertyDescriptor property )
    {
      throw new NotSupportedException();
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public PropertyDescriptor SortProperty
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public bool IsSorted
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    // Since the grid will take care of sorting the data rows
    // we do not need to enable sorting in the collection.
    public ListSortDirection SortDirection
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    // Raise the ListChanged event to advise the grid of any changes.
    protected virtual void OnListChanged( ListChangedEventArgs e )
    {
      if( this.ListChanged != null )
        this.ListChanged( this, e );
    }

    protected override void OnRemoveComplete( int index, object value )
    {
      ( ( Composer )value ).m_containingList = null;

      // Advise the grid that a row has been removed.
      this.InvokeOnListChanged( new ListChangedEventArgs( ListChangedType.ItemDeleted, index ) );
    }

    protected override void OnInsertComplete( int index, object value )
    {
      ( ( Composer )value ).m_containingList = this;

      // Advise the grid that a new row has been inserted.
      this.InvokeOnListChanged( new ListChangedEventArgs( ListChangedType.ItemAdded, index ) );
    }

    protected override void OnClearComplete()
    {
      // Advise the grid that the collection has been reset.
      this.InvokeOnListChanged( new ListChangedEventArgs( ListChangedType.Reset, -1, -1 ) );
    }

    protected override void OnClear()
    {
      foreach( Composer composer in this )
      {
        composer.m_containingList = null;
      }
    }

    protected override void OnSetComplete( int index, object oldValue, object newValue )
    {
      if( oldValue == newValue )
        return;

      // We do not check if the old and new values are null since it is impossible.      
      ( ( Composer )oldValue ).m_containingList = null;
      ( ( Composer )newValue ).m_containingList = this;

      this.InvokeOnListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, index ) );
    }

    public event ListChangedEventHandler ListChanged;

    #endregion Implementation of IBindingList

    #region Implementation of ITypedList

    public PropertyDescriptorCollection GetItemProperties( PropertyDescriptor[] properties )
    {
      // We sort the property descriptor to have the columns in our grid in that order by default.
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

    #region Implementation of ISupportInitialize

    // Indicates if we are in batch initialization.
    public bool InBatchInitialization
    {
      get
      {
        return m_initCounter > 0;
      }
    }

    // Begins the batch initialization.
    public void BeginInit()
    {
      m_initCounter++;
    }

    // Ends the batch initialization.
    public void EndInit()
    {
      if( m_initCounter > 0 )
      {
        m_initCounter--;

        if( m_initCounter == 0 )
          this.OnListChanged( new ListChangedEventArgs( ListChangedType.Reset, -1, -1 ) );
      }
    }

    #endregion Implementation of ISupportInitialize

    // Raises the ListChanged event.
    internal void InvokeOnListChanged( ListChangedEventArgs e )
    {
      // Prevents the raise of the ListChanged event if we are in batch initialisation.
      if( !this.InBatchInitialization )
        this.OnListChanged( e );
    }

    private int m_initCounter = 0;
  }
}
