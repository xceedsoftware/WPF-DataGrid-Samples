'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [CategoryRepository.vb]
' *  
' * This class provides the necessary links between the data source and the view model so the business logic can be conducted on categories.
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
  Public Class CategoryRepository
    Inherits RepositoryBase
    Public Function GetAllCategories() As ObservableCollection(Of Category)
      Dim categoriesDataView = Me.LoadDataSource("Categories")
      Return New ObservableCollection(Of Category)(Me.GetAllEntities(Of Category)(categoriesDataView))
    End Function
  End Class
End Namespace