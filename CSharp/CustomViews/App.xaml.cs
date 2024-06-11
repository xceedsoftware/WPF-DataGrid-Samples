/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the Custom Views sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace Xceed.Wpf.DataGrid.Samples.CustomViews
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      Xceed.Wpf.DataGrid.Samples.XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_employees = dataSet.Tables[ "Employees" ];

      base.OnStartup( e );
    }

    #region Employees property

    private DataTable m_employees;

    public DataTable Employees
    {
      get
      {
        return m_employees;
      }
    }

    #endregion Employees property
  }
}