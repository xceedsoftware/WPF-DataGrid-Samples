/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexibles Rows and Cells Sample Application
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

using System;

namespace Xceed.Wpf.DataGrid.Samples.FlexibleRowsCells
{
  public partial class MainPage : System.Windows.Controls.Page
  {
    #region PUBLIC CONSTRUCTOR

    public MainPage()
    {
      InitializeComponent();

      DataGridContext context = DataGridControl.GetDataGridContext( this.grid );

      if( context == null )
        throw new NullReferenceException( "context" );

      this.ContactCollection = new ContactInfoCollection()
      {
        new ContactInfo() { Name = "Brian Jones", Country = "Canada", Address = "150 Main Street", Phone = "450-598-6225", Title = "President", YearsOfexperience = 12 },
        new ContactInfo() { Name = "Tom Callahan", Country = "USA", Address = "18 Thomson drive", Phone = "714-856-9669", Title = "VP Marketing", YearsOfexperience = 3 },
        new ContactInfo() { Name = "Amanda Costa", Country = "Brazil", Address = "11 Milsa", Phone = "969-987-2552", Title = "Consultant", YearsOfexperience = 17 },
        new ContactInfo() { Name = "Jin Yang", Country = "China", Address = "3225 Fuy", Phone = "90-10-99-52-20", Title = "President", YearsOfexperience = 5 }
      };
    }

    #endregion

    #region PROPERTIES

    public ContactInfoCollection ContactCollection
    {
      get;
      private set;
    }

    #endregion
  }
}
