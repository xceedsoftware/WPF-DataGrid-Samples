'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [SupplierRepository.vb]
' *  
' * This class provides the necessary links between the data source and the view model so the business logic can be conducted on suppliers.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Model

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
  Public Class SupplierRepository
    Inherits RepositoryBase
    Public Function GetAllSuppliers() As ObservableCollection(Of Supplier)
      Dim suppliersDataView = Me.LoadDataSource("Suppliers")
      Return New ObservableCollection(Of Supplier)(Me.GetAllEntities(Of Supplier)(suppliersDataView))
    End Function
  End Class
End Namespace