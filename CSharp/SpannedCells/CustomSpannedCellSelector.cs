/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CustomSpannedCellSelector.cs]
 *  
 * This class implements the business object that defines which cells could be merged.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections.Generic;
using System.Linq;

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public class CustomSpannedCellSelector : SpannedCellSelector
  {
    public CustomSpannedCellSelector()
    {
      m_comparer = EqualityComparer<object>.Default;
    }

    public override bool CanMerge( SpannedCellFragment x, SpannedCellFragment y )
    {
      if( ( CustomSpannedCellSelector.IsCountryColumn( x.Column ) && CustomSpannedCellSelector.IsCityColumn( y.Column ) )
        || ( CustomSpannedCellSelector.IsCityColumn( x.Column ) && CustomSpannedCellSelector.IsCountryColumn( y.Column ) ) )
        return true;

      return m_comparer.Equals( x.Content, y.Content );
    }

    public override object SelectContent( IEnumerable<SpannedCellFragment> fragments )
    {
      var items = fragments.ToList();

      if( items.Count >= 2 )
      {
        var countryFragment = default( SpannedCellFragment );
        var cityFragment = default( SpannedCellFragment );

        foreach( var fragment in fragments )
        {
          if( ( countryFragment != null ) && ( cityFragment != null ) )
            break;

          if( ( countryFragment == null ) && CustomSpannedCellSelector.IsCountryColumn( fragment.Column ) )
          {
            countryFragment = fragment;
          }

          if( ( cityFragment == null ) && CustomSpannedCellSelector.IsCityColumn( fragment.Column ) )
          {
            cityFragment = fragment;
          }
        }

        if( ( countryFragment != null ) && ( cityFragment != null ) )
          return new CountryCityData( countryFragment.Content, cityFragment.Content );
      }

      if( items.Count > 0 )
        return items[ 0 ].Content;

      return base.SelectContent( fragments );
    }

    private static bool IsCountryColumn( ColumnBase column )
    {
      return ( column != null )
          && ( column.FieldName == "ShipCountry" );
    }

    private static bool IsCityColumn( ColumnBase column )
    {
      return ( column != null )
          && ( column.FieldName == "ShipCity" );
    }

    private readonly EqualityComparer<object> m_comparer;
  }
}
