'
' * Xceed DataGrid for WPF - SAMPLE CODE - Async Binding Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [PersonObservableCollection.vb]
' *  
' * Custom ObservableCollection of Person objects that is used as the source
' * of the DataGridCollectionView to which the grid is bound.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' 

Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace Xceed.Wpf.DataGrid.Samples.AsyncBinding
  Friend Class PersonObservableCollection
	  Inherits ObservableCollection(Of Person)
	Friend Sub New()
		MyBase.New()
	End Sub
  End Class
End Namespace