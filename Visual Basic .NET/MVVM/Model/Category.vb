'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [Category.vb]
' *  
' * This class represents a category item.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Model
  Public Class Category
#Region "CategoryID Property"

    Private privateCategoryID As Integer
    Public Property CategoryID() As Integer
      Get
        Return privateCategoryID
      End Get
      Set(ByVal value As Integer)
        privateCategoryID = value
      End Set
    End Property

#End Region

#Region "CategoryName Property"

    Private privateCategoryName As String
    Public Property CategoryName() As String
      Get
        Return privateCategoryName
      End Get
      Set(ByVal value As String)
        privateCategoryName = value
      End Set
    End Property

#End Region

#Region "Picture Property"

    Private privatePicture As Byte()
    Public Property Picture() As Byte()
      Get
        Return privatePicture
      End Get
      Set(ByVal value As Byte())
        privatePicture = value
      End Set
    End Property

#End Region
  End Class
End Namespace