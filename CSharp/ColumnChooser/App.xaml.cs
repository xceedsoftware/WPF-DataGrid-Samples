/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Chooser Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Column Chooser sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.ColumnChooser
{
  public partial class App : Application
  {
    #region PROTECTED METHODS

    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      m_suppliers = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet().Tables[ "Suppliers" ];

      base.OnStartup( e );
    }

    #endregion PROTECTED METHODS

    #region Suppliers property

    public DataTable Suppliers
    {
      get
      {
        return m_suppliers;
      }
    }

    private DataTable m_suppliers;

    #endregion Suppliers property
  }
}
