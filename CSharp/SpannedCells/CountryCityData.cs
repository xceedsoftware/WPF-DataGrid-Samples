﻿/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CountryCityData.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */


namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public class CountryCityData
  {
    public CountryCityData( object country, object city )
    {
      m_country = country;
      m_city = city;
    }

    public object Country
    {
      get
      {
        return m_country;
      }
    }

    public object City
    {
      get
      {
        return m_city;
      }
    }

    private readonly object m_country;
    private readonly object m_city;
  }
}