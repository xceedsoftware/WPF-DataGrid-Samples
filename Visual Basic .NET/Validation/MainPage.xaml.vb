'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [MainPage.xaml.vb]
' 
' This class implements the various dynamic configuration options offered 
' by the configuration panel declared in MainPage.xaml.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  Partial Public Class MainPage
    Inherits Page
    Public Shared ComposersBindingListComplete As ComposersBindingListComplete

    Shared Sub New()
      ' With this binding, Adding, removing, and modifying data will work.
      MainPage.ComposersBindingListComplete = New ComposersBindingListComplete()

      ' Put the collection in batch initialization to prevent the ListChanged event from raising.
      MainPage.ComposersBindingListComplete.BeginInit()

      ' Fill the collection with composers
      For Each composer As Data.ComposerData In Data.Composers
        MainPage.ComposersBindingListComplete.Add(New ComposerEditable(composer.LastName, composer.FirstName, composer.Period, composer.BirthYear, composer.DeathYear, composer.CompositionCount, _
         composer.LastUpdate))
      Next

      ' End the batch initialization of the collection.
      MainPage.ComposersBindingListComplete.EndInit()
    End Sub


    Public Sub New()
      InitializeComponent()
    End Sub
  End Class
End Namespace
