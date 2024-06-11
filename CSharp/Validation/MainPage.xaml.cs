/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPage.xaml.cs]
 * 
 * This class implements the various dynamic configuration options offered 
 * by the configuration panel declared in MainPage.xaml.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.Validation
{
  public partial class MainPage : Page
  {
    public static ComposersBindingListComplete ComposersBindingListComplete;

    static MainPage()
    {
      // With this binding, Adding, removing, and modifying data will work.
      MainPage.ComposersBindingListComplete = new ComposersBindingListComplete();

      // Put the collection in batch initialization to prevent the ListChanged event from raising.
      MainPage.ComposersBindingListComplete.BeginInit();

      // Fill the collection with composers
      foreach( Data.ComposerData composer in Data.Composers )
      {
        MainPage.ComposersBindingListComplete.Add( new ComposerEditable(
          composer.LastName,
          composer.FirstName,
          composer.Period,
          composer.BirthYear,
          composer.DeathYear,
          composer.CompositionCount,
          composer.LastUpdate ) );
      }

      // End the batch initialization of the collection.
      MainPage.ComposersBindingListComplete.EndInit();
    }


    public MainPage()
    {
      InitializeComponent();
    }
  }
}
