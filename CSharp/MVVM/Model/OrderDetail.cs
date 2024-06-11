/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [OrderDetail.cs]
 *  
 * This class represents a detail item of an order of a specific product.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
{
  public class OrderDetail : ModelBase
  {
    #region OrderID property

    public int OrderID
    {
      get
      {
        return m_orderID;
      }
      set
      {
        if( value != m_orderID )
        {
          m_orderID = value;
          this.OnPropertyChanged( "OrderID" );
        }
      }
    }

    private int m_orderID;

    #endregion

    #region ProductID property

    public int ProductID
    {
      get
      {
        return m_productID;
      }
      set
      {
        if( value != m_productID )
        {
          m_productID = value;
          this.OnPropertyChanged( "ProductID" );
        }
      }
    }

    private int m_productID;

    #endregion

    #region UnitPrice Property

    public decimal UnitPrice
    {
      get
      {
        return m_unitPrice;
      }
      set
      {
        if( value != m_unitPrice )
        {
          m_unitPrice = value;
          this.OnPropertyChanged( "UnitPrice" );
        }
      }
    }

    private decimal m_unitPrice;

    #endregion

    #region Quantity Property

    public short Quantity
    {
      get
      {
        return m_quantity;
      }
      set
      {
        if( value != m_quantity )
        {
          m_quantity = value;
          this.OnPropertyChanged( "Quantity" );
        }
      }
    }

    private short m_quantity;

    #endregion

    #region Discount Property

    public float Discount
    {
      get
      {
        return m_discount;
      }
      set
      {
        if( value != m_discount )
        {
          m_discount = value;
          this.OnPropertyChanged( "Discount" );
        }
      }
    }

    private float m_discount;

    #endregion

    #region IsModified Property

    public bool IsModified
    {
      get
      {
        return m_isModified;
      }
      set
      {
        if( value != m_isModified )
        {
          m_isModified = value;
          this.OnPropertyChanged( "IsModified" );
        }
      }
    }

    private bool m_isModified;

    #endregion
  }
}
