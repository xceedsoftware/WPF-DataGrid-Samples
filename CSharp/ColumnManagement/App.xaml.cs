/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the Column Management sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
{
  public partial class App : Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      Xceed.Wpf.DataGrid.Samples.XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetMusicLibraryDataSet();
      m_songs = dataSet.Tables[ "Songs" ];
      
      base.OnStartup( e );
    }

    #region Songs property

    private DataTable m_songs;

    public DataTable Songs
    {
      get
      {
        return m_songs;
      }
    }

    #endregion Songs property
  }
}
