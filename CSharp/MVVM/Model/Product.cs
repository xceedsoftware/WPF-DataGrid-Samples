/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Product.cs]
 *  
 * This class represents a product item.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
{
  public class Product : ModelBase, IDataErrorInfo
  {
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

    #region ProductName Property

    public string ProductName
    {
      get
      {
        return m_productName;
      }
      set
      {
        if( value != m_productName )
        {
          m_productName = value;
          this.OnPropertyChanged( "ProductName" );
        }
      }
    }

    private string m_productName;

    #endregion

    #region SupplierID Property

    public int SupplierID
    {
      get
      {
        return m_supplierID;
      }
      set
      {
        if( value != m_supplierID )
        {
          m_supplierID = value;
          this.OnPropertyChanged( "SupplierID" );
        }
      }
    }

    private int m_supplierID;

    #endregion

    #region CategoryID Property

    public int CategoryID
    {
      get
      {
        return m_categoryID;
      }
      set
      {
        if( value != m_categoryID )
        {
          m_categoryID = value;
          this.OnPropertyChanged( "CategoryID" );
        }
      }
    }

    private int m_categoryID;

    #endregion

    #region QuantityPerUnit Property

    public string QuantityPerUnit
    {
      get
      {
        return m_quantityPerUnit;
      }
      set
      {
        if( value != m_quantityPerUnit )
        {
          m_quantityPerUnit = value;
          this.OnPropertyChanged( "QuantityPerUnit" );
        }
      }
    }

    private string m_quantityPerUnit;

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

    #region UnitsInStock Property

    public short UnitsInStock
    {
      get
      {
        return m_unitsInStock;
      }
      set
      {
        if( value != m_unitsInStock )
        {
          m_unitsInStock = value;
          this.OnPropertyChanged( "UnitsInStock" );
        }
      }
    }

    private short m_unitsInStock;

    #endregion

    #region UnitsOnOrder Property

    public short UnitsOnOrder
    {
      get
      {
        return m_unitsOnOrder;
      }
      set
      {
        if( value != m_unitsOnOrder )
        {
          m_unitsOnOrder = value;
          this.OnPropertyChanged( "UnitsOnOrder" );
        }
      }
    }

    private short m_unitsOnOrder;

    #endregion

    #region ReorderLevel Property

    public short ReorderLevel
    {
      get
      {
        return m_reorderLevel;
      }
      set
      {
        if( value != m_reorderLevel )
        {
          m_reorderLevel = value;
          this.OnPropertyChanged( "ReorderLevel" );
        }
      }
    }

    private short m_reorderLevel;

    #endregion

    #region Discontinued Property

    public bool Discontinued
    {
      get
      {
        return m_discontinued;
      }
      set
      {
        if( value != m_discontinued )
        {
          m_discontinued = value;
          this.OnPropertyChanged( "Discontinued" );
        }
      }
    }

    private bool m_discontinued;

    #endregion

    #region ReorderDate Property

    public DateTime? ReorderDate
    {
      get
      {
        return m_reorderDate;
      }
      set
      {
        if( value != m_reorderDate )
        {
          m_reorderDate = value;
          this.OnPropertyChanged( "ReorderDate" );
        }
      }
    }

    private DateTime? m_reorderDate;

    #endregion

    #region Photo Property

    public byte[] Photo
    {
      get
      {
        return m_photo;
      }
      set
      {
        if( value != m_photo )
        {
          m_photo = value;
          this.OnPropertyChanged( "Photo" );
        }
      }
    }

    private byte[] m_photo;

    #endregion

    #region OrderDetails property

    public ObservableCollection<OrderDetail> OrderDetails
    {
      get
      {
        return m_orderDetails;
      }
      set
      {
        if( value != m_orderDetails )
        {
          m_orderDetails = value;
          this.OnPropertyChanged( "OrderDetails" );
        }
      }
    }

    private ObservableCollection<OrderDetail> m_orderDetails;

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

    #region IDataErrorInfo Members

    string IDataErrorInfo.Error
    {
      get
      {
        return string.Empty;
      }
    }

    string IDataErrorInfo.this[ string columnName ]
    {
      get
      {
        if( columnName != "ReorderLevel" || this.ReorderLevel >= 0 )
          return string.Empty;

        return "The reorder level cannot be negative";
      }
    }

    #endregion
  }
}
