'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [OrderDetailRepository.vb]
' *  
' * This class provides the necessary links between the data source and the view model so the business logic can be conducted on OrderDetails.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.ObjectModel
Imports System.Data
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Model

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
  Public Class OrderDetailRepository
    Inherits RepositoryBase
#Region "Singleton static Property"

    'Use a singleton so the whole detail data is loaded only once, instead of having each master item loading it every time.
    Public Shared ReadOnly Property Singleton() As OrderDetailRepository
      Get
        Return m_singleton
      End Get
    End Property

    Private Shared m_singleton As New OrderDetailRepository()

#End Region

    Private Sub New()
      m_orderDetailsView = Me.LoadDataSource("OrderDetails")

      'Extract the last OrderID in order to generate unique and consecutive OrderIDs when adding new orders.
      m_orderDetailsView.ApplyDefaultSort = True
      Dim lastOrderDetail = m_orderDetailsView(m_orderDetailsView.Count - 1)
      m_lastOrderID = Convert.ToInt32(lastOrderDetail("OrderID"))
    End Sub

    Public Function GetAllOrderDetails(ByVal productID As String) As ObservableCollection(Of OrderDetail)
      m_orderDetailsView.RowFilter = "ProductID = " & productID.ToString()
      Return New ObservableCollection(Of OrderDetail)(Me.GetAllEntities(Of OrderDetail)(m_orderDetailsView))
    End Function

    Public Function GetNewOrderDetails(ByVal productID As Integer, ByVal unitPrice As Decimal) As ObservableCollection(Of OrderDetail)
      'This simulates a new list of order details for a new product received from the DB.
      Dim count = RandomValueGenerator.GetRandomInteger(1, 4)
      Dim orderDetail = Nothing
      Dim orderDetails = New ObservableCollection(Of OrderDetail)()

      For i As Integer = 0 To count - 1
        orderDetail = New OrderDetail()
        m_lastOrderID += 1
        orderDetail.OrderID = m_lastOrderID
        orderDetail.ProductID = productID
        orderDetail.UnitPrice = unitPrice
        orderDetail.Quantity = RandomValueGenerator.GetRandomShort(1, 31)
        orderDetail.Discount = 0

        orderDetails.Add(orderDetail)
      Next i

      Return orderDetails
    End Function

    Public Sub AddNewOrderDetail(ByVal orderDetail As OrderDetail)
      'The necessary code to send an added order detail to the DB would be implemented here.
    End Sub

    Public Sub RemoveOrderDetail(ByVal item As OrderDetail)
      'The necessary code to remove the order detail from the DB would be implemented here.
    End Sub

    Public Sub UpdateOrderDetail(ByVal item As OrderDetail)
      'The necessary code to update the order detail in the DB would be implemented here.
    End Sub

    Private m_orderDetailsView As DataView
    Private m_lastOrderID As Integer
  End Class
End Namespace