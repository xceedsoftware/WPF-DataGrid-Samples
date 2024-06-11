/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ZodiacCellContentTemplateSelector.cs]
 *  
 * This class a DataTemplateSelector that displays a date in its "Short" representation
 * and its associated zodiac symbol.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xceed.Wpf.DataGrid.Samples.CustomViews
{
  public class ZodiacCellContentTemplateSelector : DataTemplateSelector
  {
    public override DataTemplate SelectTemplate( object item, DependencyObject container )
    {
      DataTemplate dataTemplate = new DataTemplate();
      FrameworkElementFactory grid = new FrameworkElementFactory( typeof( Grid ) );
      FrameworkElementFactory viewbox = new FrameworkElementFactory( typeof( Viewbox ) );
      FrameworkElementFactory zodiacSymbol = new FrameworkElementFactory( typeof( TextBlock ) );
      FrameworkElementFactory textBlock = new FrameworkElementFactory( typeof( TextBlock ) );

      char symbol;
      if( item is DateTime )
      {
        symbol = ( char )( FirstZodiacSymbolCode + this.GetZodiacSign( ( DateTime )item ) );
        textBlock.SetValue( TextBlock.TextProperty, ( ( DateTime )item ).ToShortDateString() );
      }
      else
      {
        symbol = '?';
        textBlock.SetValue( TextBlock.TextProperty, "unknown" );
      }

      zodiacSymbol.SetValue( TextBlock.FontFamilyProperty, new FontFamily( "Wingdings" ) );
      zodiacSymbol.SetValue( TextBlock.OpacityProperty, 0.3d );
      zodiacSymbol.SetValue( TextBlock.TextProperty, symbol.ToString() );
      zodiacSymbol.SetValue( TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center );
      zodiacSymbol.SetValue( TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center );

      viewbox.AppendChild( zodiacSymbol );
      // We don't want the zodiac sign to be too big.
      viewbox.SetValue( Viewbox.MaxHeightProperty, 60d );
      grid.AppendChild( viewbox );


      grid.AppendChild( textBlock );

      dataTemplate.VisualTree = grid;
      dataTemplate.Seal();

      return dataTemplate;
    }

    private int GetZodiacSign( DateTime dateTime )
    {
      byte[,] zodiacRanges = new byte[ 12, 4 ]
      {
        //   Start        End
        // Month  Day   Month   Day
        {  3,     21,   4,      20 }, // Aries
        {  4,     21,   5,      21 }, // Taurus
        {  5,     22,   6,      21 }, // Gemini
        {  6,     22,   7,      23 }, // Cancer
        {  7,     24,   8,      23 }, // Leo
        {  8,     24,   9,      23 }, // Virgo
        {  9,     24,   10,     23 }, // Libra
        {  10,    24,   11,     22 }, // Scorpio
        {  11,    23,   12,     21 }, // Sagittarius
        {  12,    22,   1,      20 }, // Capricorn
        {  1,     21,   2,      18 }, // Aquarius
        {  2,     19,   3,      20 }  // Pisces
      };

      for( int i = 0; i < 12; i++ )
      {
        if( ( ( dateTime.Month == zodiacRanges[ i, 0 ] ) && ( dateTime.Day >= zodiacRanges[ i, 1 ] ) ) ||
            ( ( dateTime.Month == zodiacRanges[ i, 2 ] ) && ( dateTime.Day <= zodiacRanges[ i, 3 ] ) ) )
          return i;
      }

      return 0;
    }

    private static readonly int FirstZodiacSymbolCode = 94;
  }
}
