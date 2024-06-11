/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ProductRepository.cs]
 *  
 * This class provides the necessary links between the data source and the view model so the business logic can be conducted on products.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Xceed.Wpf.DataGrid.Samples.MVVM.Model;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
{
  public class ProductRepository : RepositoryBase
  {
    public ObservableCollection<Product> GetAllProducts()
    {
      var productsDataView = this.LoadDataSource( "Products" );
      var products = new ObservableCollection<Product>( this.GetAllEntities<Product>( productsDataView ) );

      foreach( var product in products )
      {
        product.OrderDetails = OrderDetailRepository.Singleton.GetAllOrderDetails( product.ProductID.ToString() );
      }

      return products;
    }

    public Product GetNewProduct( int productID )
    {
      //This simulates a new product received from the DB.
      var product = new Product();
      product.ProductID = productID;
      product.ProductName = RandomValueGenerator.GetRandomProductName();
      product.SupplierID = RandomValueGenerator.GetRandomInteger( 1, 30 );
      product.CategoryID = RandomValueGenerator.GetRandomInteger( 1, 9 );
      product.QuantityPerUnit = RandomValueGenerator.GetRandomQuantityPerUnit();
      product.UnitPrice = RandomValueGenerator.GetRandomDecimal();
      product.UnitsInStock = RandomValueGenerator.GetRandomShort( 0, 126 );
      product.UnitsOnOrder = RandomValueGenerator.GetRandomShort( 0, 101 );
      product.ReorderLevel = RandomValueGenerator.GetRandomShort( 0, 31 );
      product.Discontinued = RandomValueGenerator.GetRandomBool();
      product.ReorderDate = RandomValueGenerator.GetRandomDateTime( 2009, 2016 );
      this.GetGenericProductPhoto( product );
      product.OrderDetails = OrderDetailRepository.Singleton.GetNewOrderDetails( productID, product.UnitPrice );

      return product;
    }

    public void AddNewProduct( Product item )
    {
      //The necessary code to send the user's added product to the DB would be implemented here.
    }

    public void RemoveProduct( Product item )
    {
      //The necessary code to remove the product from the DB would be implemented here, along with calls to OrderDetailRepository.Singleton.RemoveOrderDetail() to remove corresponding order details.
    }

    public void UpdateProduct( Product item )
    {
      //The necessary code to update the product in the DB would be implemented here.
    }

    private void GetGenericProductPhoto( Product product )
    {
      var imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream( "Xceed.Wpf.DataGrid.Samples.MVVM.Resources.GenericProductPic.jpg" );
      using( var image = Image.FromStream( imageStream ) )
      {
        using( MemoryStream destination = new MemoryStream() )
        {
          image.Save( destination, ImageFormat.Jpeg );
          product.Photo = destination.ToArray();
        }
      }
    }
  }
}
