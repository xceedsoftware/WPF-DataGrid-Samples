'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [Supplier.vb]
' *  
' * This class represents a supplier item.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
  Public Class Supplier
#Region "SupplierID Property"

    Private privateSupplierID As Integer
    Public Property SupplierID() As Integer
      Get
        Return privateSupplierID
      End Get
      Set(ByVal value As Integer)
        privateSupplierID = value
      End Set
    End Property

#End Region

#Region "CompanyName Property"

    Private privateCompanyName As String
    Public Property CompanyName() As String
      Get
        Return privateCompanyName
      End Get
      Set(ByVal value As String)
        privateCompanyName = value
      End Set
    End Property

#End Region
  End Class
End Namespace