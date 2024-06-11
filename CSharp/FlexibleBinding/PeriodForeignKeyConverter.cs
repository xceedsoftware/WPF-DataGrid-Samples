/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Master/Detail Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [PeriodForeignKeyConverter.cs]
 *  
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  public class PeriodForeignKeyConverter : ForeignKeyConverter
  {
    public override object GetValueFromKey( object key, DataGridForeignKeyDescription description )
    {
      return Enum.GetName( typeof( Period ), key );
    }
  }
}
