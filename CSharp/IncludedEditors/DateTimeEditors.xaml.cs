/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Included Editors Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [DateTimeEditors.xaml.cs]
 *  
 * This class implements the event handlers referenced by some editors of this
 * ResourceDictionary.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Text;
using System.Windows;

using Xceed.Wpf.Controls;

namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
{
  public partial class DateTimeEditors : ResourceDictionary
  {
    private void InvariantAbbreviatedDateTimeEditor_QueryTextFromValue( object sender, QueryTextFromValueEventArgs e )
    {
      if( ( e.Value == null ) || ( e.Value == DBNull.Value ) )
      {
        e.Text = string.Empty;
      }
      else
      {
        e.Text = ( ( DateTime )e.Value ).ToString( "dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture );
      }
    }

    private void InvariantAbbreviatedDateTimeEditor_QueryValueFromText( object sender, QueryValueFromTextEventArgs e )
    {
      e.Value = null;
      e.HasParsingError = true;

      DateTime parsedDateTime;

      if( DateTime.TryParse( e.Text, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDateTime ) )
      {
        e.HasParsingError = false;
        e.Value = parsedDateTime;
      }
    }

    private void InvariantAbbreviatedDateTimeEditor_AutoCompletingMask( object sender, AutoCompletingMaskEventArgs e )
    {
      StringBuilder autoCompletionBuilder = new StringBuilder();

      System.ComponentModel.MaskedTextProvider provider = e.MaskedTextProvider;
      int startPosition = e.StartPosition;

      e.AutoCompleteStartPosition = provider.FindNonEditPositionFrom( startPosition, false ) + 1;
      int nextSeparatorIndex = provider.FindNonEditPositionFrom( startPosition, true );

      bool validAutoCompletion = false;

      if( e.AutoCompleteStartPosition == 0 )
      {
        // auto completing the day.
        for( int i = e.AutoCompleteStartPosition; i < nextSeparatorIndex; i++ )
        {
          if( provider.IsAvailablePosition( i ) )
          {
            autoCompletionBuilder.Insert( 0, '0' );
          }
          else
          {
            char alreadyEnteredCharacter = provider[ i ];

            if( alreadyEnteredCharacter != '0' )
              validAutoCompletion = true;

            autoCompletionBuilder.Append( alreadyEnteredCharacter );
          }
        }
      }
      else if( e.AutoCompleteStartPosition == 3 )
      {
        // auto completing the abbreviated month name.
        for( int i = e.AutoCompleteStartPosition; i < nextSeparatorIndex; i++ )
        {
          if( !provider.IsAvailablePosition( i ) )
            autoCompletionBuilder.Append( provider[ i ] );
        }

        string[] abbreviatedMonthNames = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames;

        string partialInput = autoCompletionBuilder.ToString();
        string uniqueCorrespondingMonthName = null;

        foreach( string monthName in abbreviatedMonthNames )
        {
          if( monthName.ToUpper().StartsWith( partialInput.ToUpper() ) )
          {
            if( uniqueCorrespondingMonthName != null )
            {
              uniqueCorrespondingMonthName = null;
              break;
            }

            uniqueCorrespondingMonthName = monthName;
          }
        }

        if( uniqueCorrespondingMonthName != null )
        {
          autoCompletionBuilder = new StringBuilder( uniqueCorrespondingMonthName );
          validAutoCompletion = true;
        }
      }
      else
      {
        // auto completing the year.
        for( int i = e.AutoCompleteStartPosition; i < nextSeparatorIndex; i++ )
        {
          if( !provider.IsAvailablePosition( i ) )
            autoCompletionBuilder.Append( provider[ i ] );
        }

        // Because different calendars behave differently, autocompletion is only possible
        // if exactly one or two digits are already entered.
        if( autoCompletionBuilder.Length == 1 )
          autoCompletionBuilder.Insert( 0, '0' );

        if( autoCompletionBuilder.Length == 2 )
        {
          int partialYear = Int32.Parse( autoCompletionBuilder.ToString() );

          System.Globalization.DateTimeFormatInfo dateTimeFormatInfo =
            System.Globalization.DateTimeFormatInfo.GetInstance( System.Globalization.CultureInfo.InvariantCulture );

          int fullYear = dateTimeFormatInfo.Calendar.ToFourDigitYear( partialYear );

          autoCompletionBuilder = new StringBuilder( fullYear.ToString() );
          validAutoCompletion = true;
        }
      }

      e.AutoCompleteText = autoCompletionBuilder.ToString();
      e.Cancel = !validAutoCompletion;
    }
  }
}
