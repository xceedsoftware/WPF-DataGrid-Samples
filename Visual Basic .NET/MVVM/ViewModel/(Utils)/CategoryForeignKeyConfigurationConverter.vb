'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [CategoryForeignKeyConfigurationConverter.vb]
' *  
' * This class retrieves the category item (as a whole) that is associated with the category property value of a given product.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.Collections.ObjectModel
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Model

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
  Public Class CategoryForeignKeyConfigurationConverter
    Inherits ForeignKeyConverter
    Public Overrides Function GetValueFromKey(ByVal key As Object, ByVal configuration As ForeignKeyConfiguration) As Object
      If key Is Nothing Then
        Return Nothing
      End If

      Dim value = CInt(Fix(key))
      Dim itemsSource = TryCast(configuration.ItemsSource, ObservableCollection(Of Category))

      'Find and return the category item that matches the CategoryID value.
      Return itemsSource.FirstOrDefault(Function(item) item.CategoryID = value)
    End Function
  End Class
End Namespace