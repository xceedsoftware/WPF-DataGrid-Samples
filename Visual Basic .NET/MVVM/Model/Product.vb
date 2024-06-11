'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [Product.vb]
' *  
' * This class represents a product item.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
  Public Class Product
    Inherits ModelBase
    Implements IDataErrorInfo
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

#Region "ProductName Property"

    Public Property ProductName() As String
      Get
        Return m_productName
      End Get
      Set(ByVal value As String)
        If value <> m_productName Then
          m_productName = value
          Me.OnPropertyChanged("ProductName")
        End If
      End Set
    End Property

    Private m_productName As String

#End Region

#Region "SupplierID Property"

    Public Property SupplierID() As Integer
      Get
        Return m_supplierID
      End Get
      Set(ByVal value As Integer)
        If value <> m_supplierID Then
          m_supplierID = value
          Me.OnPropertyChanged("SupplierID")
        End If
      End Set
    End Property

    Private m_supplierID As Integer

#End Region

#Region "CategoryID Property"

    Public Property CategoryID() As Integer
      Get
        Return m_categoryID
      End Get
      Set(ByVal value As Integer)
        If value <> m_categoryID Then
          m_categoryID = value
          Me.OnPropertyChanged("CategoryID")
        End If
      End Set
    End Property

    Private m_categoryID As Integer

#End Region

#Region "QuantityPerUnit Property"

    Public Property QuantityPerUnit() As String
      Get
        Return m_quantityPerUnit
      End Get
      Set(ByVal value As String)
        If value <> m_quantityPerUnit Then
          m_quantityPerUnit = value
          Me.OnPropertyChanged("QuantityPerUnit")
        End If
      End Set
    End Property

    Private m_quantityPerUnit As String

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

#Region "UnitsInStock Property"

    Public Property UnitsInStock() As Short
      Get
        Return m_unitsInStock
      End Get
      Set(ByVal value As Short)
        If value <> m_unitsInStock Then
          m_unitsInStock = value
          Me.OnPropertyChanged("UnitsInStock")
        End If
      End Set
    End Property

    Private m_unitsInStock As Short

#End Region

#Region "UnitsOnOrder Property"

    Public Property UnitsOnOrder() As Short
      Get
        Return m_unitsOnOrder
      End Get
      Set(ByVal value As Short)
        If value <> m_unitsOnOrder Then
          m_unitsOnOrder = value
          Me.OnPropertyChanged("UnitsOnOrder")
        End If
      End Set
    End Property

    Private m_unitsOnOrder As Short

#End Region

#Region "ReorderLevel Property"

    Public Property ReorderLevel() As Short
      Get
        Return m_reorderLevel
      End Get
      Set(ByVal value As Short)
        If value <> m_reorderLevel Then
          m_reorderLevel = value
          Me.OnPropertyChanged("ReorderLevel")
        End If
      End Set
    End Property

    Private m_reorderLevel As Short

#End Region

#Region "Discontinued Property"

    Public Property Discontinued() As Boolean
      Get
        Return m_discontinued
      End Get
      Set(ByVal value As Boolean)
        If value <> m_discontinued Then
          m_discontinued = value
          Me.OnPropertyChanged("Discontinued")
        End If
      End Set
    End Property

    Private m_discontinued As Boolean

#End Region

#Region "ReorderDate Property"

    Public Property ReorderDate() As Nullable(Of DateTime)
      Get
        Return m_reorderDate
      End Get
      Set(ByVal value As Nullable(Of DateTime))
        If value.Equals(m_reorderDate) Then
          m_reorderDate = value
          Me.OnPropertyChanged("ReorderDate")
        End If
      End Set
    End Property

    Private m_reorderDate As Nullable(Of DateTime)

#End Region

#Region "Photo Property"

    Public Property Photo() As Byte()
      Get
        Return m_photo
      End Get
      Set(ByVal value As Byte())
        If value IsNot m_photo Then
          m_photo = value
          Me.OnPropertyChanged("Photo")
        End If
      End Set
    End Property

    Private m_photo() As Byte

#End Region

#Region "OrderDetails property"

    Public Property OrderDetails() As ObservableCollection(Of OrderDetail)
      Get
        Return m_orderDetails
      End Get
      Set(ByVal value As ObservableCollection(Of OrderDetail))
        If value IsNot m_orderDetails Then
          m_orderDetails = value
          Me.OnPropertyChanged("OrderDetails")
        End If
      End Set
    End Property

    Private m_orderDetails As ObservableCollection(Of OrderDetail)

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

#Region "IDataErrorInfo Members"

    Private ReadOnly Property IDataErrorInfo_Error() As String Implements IDataErrorInfo.Error
      Get
        Return String.Empty
      End Get
    End Property

    Public ReadOnly Property IDataErrorInfo_Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
      Get
        If columnName <> "ReorderLevel" OrElse Me.ReorderLevel >= 0 Then
          Return String.Empty
        End If

        Return "The reorder level cannot be negative"
      End Get
    End Property

#End Region
  End Class
End Namespace