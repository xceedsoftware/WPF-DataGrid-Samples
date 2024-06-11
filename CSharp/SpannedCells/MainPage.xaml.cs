/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();

      this.grid.CurrentItem = null;
      this.grid.CurrentColumn = null;
      this.grid.SelectedItems.Clear();

      m_spannedCellContainerStyle = new Style( typeof( SpannedCell ) );
      m_spannedCellContainerStyle.Setters.Add( new Setter( SpannedCell.CellContainerStyleProperty, this.FindResource( "spannedCellContainerStyle" ) ) );

      ConfigurationData.Singleton.PropertyChanged += new PropertyChangedEventHandler( OnConfigurationDataPropertyChanged );
    }

    private void OnConfigurationDataPropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      if( e.PropertyName == "AddBorders" )
      {
        this.SetSpannedCellContainerStyle();
      }
    }

    private void SetSpannedCellContainerStyle()
    {
      if( ConfigurationData.Singleton.AddBorders )
      {
        this.Resources.Add( typeof( SpannedCell ), m_spannedCellContainerStyle );
      }
      else
      {
        this.Resources.Remove( typeof( SpannedCell ) );
      }
    }

    private Style m_spannedCellContainerStyle;
  }
}
