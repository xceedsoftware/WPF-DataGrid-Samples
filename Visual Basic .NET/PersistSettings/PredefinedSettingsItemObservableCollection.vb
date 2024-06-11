'
' Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [PredefinedSettingsItemObservableCollection.vb]
'  
' Collection used for the predefined combo box binding.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
  Public Class PredefinedSettingsItemObservableCollection
    Inherits ObservableCollection(Of PredefinedSettingsItem)
  End Class
End Namespace
