'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [OrderDetail.vb]
' *  
' * This class represents a detail item of an order of a specific product.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
  Public Class OrderDetail
    Inherits ModelBase
#Region "OrderID property"

    Public Property OrderID() As Integer
      Get
        Return m_orderID
      End Get
      Set(ByVal value As Integer)
        If value <> m_orderID Then
          m_orderID = value
          Me.OnPropertyChanged("OrderID")
        End If
      End Set
    End Property

    Private m_orderID As Integer

#End Region

#Region "ProductID property"

    Public Property ProductID() As Integer
      Get
        Return m_productID
      End Get
      Set(ByVal value As Integer)
        If value <> m_productID Then
          m_productID = value
          Me.OnPropertyChanged("ProductID")
        End If
      End Set
    End Property

    Private m_productID As Integer

#End Region

#Region "UnitPrice Property"

    Public Property UnitPrice() As Decimal
      Get
        Return m_unitPrice
      End Get
      Set(ByVal value As Decimal)
        If value <> m_unitPrice Then
          m_unitPrice = value
          Me.OnPropertyChanged("UnitPrice")
        End If
      End Set
    End Property

    Private m_unitPrice As Decimal

#End Region

#Region "Quantity Property"

    Public Property Quantity() As Short
      Get
        Return m_quantity
      End Get
      Set(ByVal value As Short)
        If value <> m_quantity Then
          m_quantity = value
          Me.OnPropertyChanged("Quantity")
        End If
      End Set
    End Property

    Private m_quantity As Short

#End Region

#Region "Discount Property"

    Public Property Discount() As Single
      Get
        Return m_discount
      End Get
      Set(ByVal value As Single)
        If value <> m_discount Then
          m_discount = value
          Me.OnPropertyChanged("Discount")
        End If
      End Set
    End Property

    Private m_discount As Single

#End Region

#Region "IsModified Property"

    Public Property IsModified() As Boolean
      Get
        Return m_isModified
      End Get
      Set(ByVal value As Boolean)
        If value <> m_isModified Then
          m_isModified = value
          Me.OnPropertyChanged("IsModified")
        End If
      End Set
    End Property

    Private m_isModified As Boolean

#End Region
  End Class
End Namespace