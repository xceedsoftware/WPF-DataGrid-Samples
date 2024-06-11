'
' * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Rows & Cells Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [ContactInfo.vb]
' *  
' * This class is used to save a collection of Contact Info data.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleRowsCells
  Public Class ContactInfoCollection
	  Inherits ObservableCollection(Of ContactInfo)
  End Class
End Namespace