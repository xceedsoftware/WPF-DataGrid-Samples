'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ProductRepository.vb]
' *  
' * This class provides the necessary links between the data source and the view model so the business logic can be conducted on products.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.Collections.ObjectModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Model
Imports Xceed.Wpf.DataGrid.Samples.Properties

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
  Public Class ProductRepository
    Inherits RepositoryBase
    Public Function GetAllProducts() As ObservableCollection(Of Product)
      Dim productsDataView = Me.LoadDataSource("Products")
      Dim products = New ObservableCollection(Of Product)(Me.GetAllEntities(Of Product)(productsDataView))

      For Each product In products
        product.OrderDetails = OrderDetailRepository.Singleton.GetAllOrderDetails(product.ProductID.ToString())
      Next product

      Return products
    End Function

    Public Function GetNewProduct(ByVal productID As Integer) As Product
      'This simulates a new product received from the DB.
      Dim product = New Product()
      product.ProductID = productID
      product.ProductName = RandomValueGenerator.GetRandomProductName()
      product.SupplierID = RandomValueGenerator.GetRandomInteger(1, 30)
      product.CategoryID = RandomValueGenerator.GetRandomInteger(1, 9)
      product.QuantityPerUnit = RandomValueGenerator.GetRandomQuantityPerUnit()
      product.UnitPrice = RandomValueGenerator.GetRandomDecimal()
      product.UnitsInStock = RandomValueGenerator.GetRandomShort(0, 126)
      product.UnitsOnOrder = RandomValueGenerator.GetRandomShort(0, 101)
      product.ReorderLevel = RandomValueGenerator.GetRandomShort(0, 31)
      product.Discontinued = RandomValueGenerator.GetRandomBool()
      product.ReorderDate = RandomValueGenerator.GetRandomDateTime(2009, 2016)
      Me.GetGenericProductPhoto(product)
      product.OrderDetails = OrderDetailRepository.Singleton.GetNewOrderDetails(productID, product.UnitPrice)

      Return product
    End Function

    Public Sub AddNewProduct(ByVal item As Product)
      'The necessary code to send the user's added product to the DB would be implemented here.
    End Sub

    Public Sub RemoveProduct(ByVal item As Product)
      'The necessary code to remove the product from the DB would be implemented here, along with calls to OrderDetailRepository.Singleton.RemoveOrderDetail() to remove corresponding order details.
    End Sub

    Public Sub UpdateProduct(ByVal item As Product)
      'The necessary code to update the product in the DB would be implemented here.
    End Sub

    Private Sub GetGenericProductPhoto(ByVal product As Product)
      Dim names = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()
      Dim imageStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("GenericProductPic.jpg")
      Using image = System.Drawing.Image.FromStream(imageStream)
        Using destination As New MemoryStream()
          image.Save(destination, ImageFormat.Jpeg)
          product.Photo = destination.ToArray()
        End Using
      End Using
    End Sub
  End Class
End Namespace