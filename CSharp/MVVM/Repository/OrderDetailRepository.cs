/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [OrderDetailRepository.cs]
 *  
 * This class provides the necessary links between the data source and the view model so the business logic can be conducted on OrderDetails.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.ObjectModel;
using System.Data;
using Xceed.Wpf.DataGrid.Samples.MVVM.Model;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
{
  public class OrderDetailRepository : RepositoryBase
  {
    #region Singleton static Property

    //Use a singleton so the whole detail data is loaded only once, instead of having each master item loading it every time.
    public static OrderDetailRepository Singleton
    {
      get
      {
        return m_singleton;
      }
    }

    private static OrderDetailRepository m_singleton = new OrderDetailRepository();

    #endregion

    private OrderDetailRepository()
    {
      m_orderDetailsView = this.LoadDataSource( "OrderDetails" );

      //Extract the last OrderID in order to generate unique and consecutive OrderIDs when adding new orders.
      m_orderDetailsView.ApplyDefaultSort = true;
      var lastOrderDetail = m_orderDetailsView[ m_orderDetailsView.Count - 1 ];
      m_lastOrderID = Convert.ToInt32( lastOrderDetail[ "OrderID" ] );
    }

    public ObservableCollection<OrderDetail> GetAllOrderDetails( string productID )
    {
      m_orderDetailsView.RowFilter = "ProductID = " + productID.ToString();
      return new ObservableCollection<OrderDetail>( this.GetAllEntities<OrderDetail>( m_orderDetailsView ) );
    }

    public ObservableCollection<OrderDetail> GetNewOrderDetails( int productID, decimal unitPrice )
    {
      //This simulates a new list of order details for a new product received from the DB.
      var count = RandomValueGenerator.GetRandomInteger( 1, 4 );
      var orderDetail = default( OrderDetail );
      var orderDetails = new ObservableCollection<OrderDetail>();

      for( int i = 0; i < count; i++ )
      {
        orderDetail = new OrderDetail();
        orderDetail.OrderID = ++m_lastOrderID;
        orderDetail.ProductID = productID;
        orderDetail.UnitPrice = unitPrice;
        orderDetail.Quantity = RandomValueGenerator.GetRandomShort( 1, 31 );
        orderDetail.Discount = 0;

        orderDetails.Add( orderDetail );
      }

      return orderDetails;
    }

    public void AddNewOrderDetail( OrderDetail orderDetail )
    {
      //The necessary code to send an added order detail to the DB would be implemented here.
    }

    public void RemoveOrderDetail( OrderDetail item )
    {
      //The necessary code to remove the order detail from the DB would be implemented here.
    }

    public void UpdateOrderDetail( OrderDetail item )
    {
      //The necessary code to update the order detail in the DB would be implemented here.
    }

    private DataView m_orderDetailsView;
    private int m_lastOrderID;
  }
}
