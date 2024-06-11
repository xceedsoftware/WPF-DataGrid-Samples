/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ComposerEditable.cs]
 *  
 * This class derives from the Composer class and 
 * implements the IEditableObject interface needed for the 
 * ComposerBindingListComplete to work correctly.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  // The ComposerBindingListComplete uses elements of this type.
  public class ComposerEditable : Composer, IEditableObject
  {
    public ComposerEditable()
      : base()
    {
    }

    // Create a new instance of the ComposerEditable class.
    public ComposerEditable( string lastName, string firstName, Period period, int birthYear, int deathYear, int compositionCount, DateTime lastUpdate )
      : base( lastName, firstName, period, birthYear, deathYear, compositionCount, lastUpdate )
    {
    }

    #region Implementation of IEditableObject

    public void BeginEdit()
    {
      if( m_isBeingEdited )
        return;

      m_isBeingEdited = true;

      // Keep a copy of the original values in case CancelEdit is called.
      m_oldValues[ 0 ] = m_firstName;
      m_oldValues[ 1 ] = m_lastName;
      m_oldValues[ 2 ] = m_period;
      m_oldValues[ 3 ] = m_birthYear;
      m_oldValues[ 4 ] = m_deathYear;
      m_oldValues[ 5 ] = m_compositionCount;
      m_oldValues[ 6 ] = m_lastUpdate;
    }

    public void CancelEdit()
    {
      // Make sure that the object is actually being edited.
      if( !m_isBeingEdited )
        return;

      // Return the values to their original values
      m_firstName = ( string )m_oldValues[ 0 ];
      m_lastName = ( string )m_oldValues[ 1 ];
      m_period = ( Period )m_oldValues[ 2 ];
      m_birthYear = ( int )m_oldValues[ 3 ];
      m_deathYear = ( int )m_oldValues[ 4 ];
      m_compositionCount = ( int )m_oldValues[ 5 ];
      m_lastUpdate = ( DateTime )m_oldValues[ 6 ];

      m_isBeingEdited = false;

      if( m_containingList != null )
      {
        if( ( ( ComposersBindingListComplete )m_containingList ).LastAddedComposer == this ) //AddNew
        {
          // If the element is coming from an AddNew and EndEdit has not been called yet,
          // we have to remove the row from our containing list.
          m_containingList.Remove( this );
        }
        else
        {
          // Advise the grid of the modification via the ListChanged event of the IBindingList interface.
          this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemChanged, m_containingList.IndexOf( this ) ) );
        }
      }
    }

    public void EndEdit()
    {
      // Make sure that the object is actually being edited.
      if( !m_isBeingEdited )
        return;

      // EndEdit validation
      if( m_birthYear >= m_deathYear )
        throw new InvalidOperationException( "The composer's death year must be greater than its birth year." );

      // There is nothing to do with the temporary values that we kept
      // since the values are already committed. Therefore, we clear
      // the temporary object array.
      Array.Clear( m_oldValues, 0, m_oldValues.Length );
      m_isBeingEdited = false;

      if( ( ( ComposersBindingListComplete )m_containingList ).LastAddedComposer == this ) // AddNew
      {
        // If the element is coming from an AddNew and EndEdit was never called before,
        // we have to advise the grid by raising a ListChanged with ListChangedType.ItemAdded a second time.
        this.InvokeListChanged( new ListChangedEventArgs( ListChangedType.ItemAdded, m_containingList.IndexOf( this ) ) );
        ( ( ComposersBindingListComplete )m_containingList ).LastAddedComposer = null;
      }
    }

    #endregion Implementation of IEditableObject

    // Raise the ListChanged event of our containing list.
    protected override void InvokeListChanged( ListChangedEventArgs e )
    {
      if( !m_isBeingEdited )
      {
        if( m_containingList != null )
          ( ( ComposersBindingListComplete )m_containingList ).InvokeOnListChanged( e );
      }
    }

    private object[] m_oldValues = new object[ 7 ];
    private bool m_isBeingEdited = false;
  }
}
