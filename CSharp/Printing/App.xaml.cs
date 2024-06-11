/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Printing Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Printing sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;

namespace Xceed.Wpf.DataGrid.Samples.Printing
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( System.Windows.StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      DataSet musicDataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetMusicLibraryDataSet();
      m_songs = musicDataSet.Tables[ "Songs" ];

      base.OnStartup( e );
    }

    #region Songs Property

    private DataTable m_songs;

    public DataTable Songs
    {
      get
      {
        return m_songs;
      }
    }

    #endregion Songs Property
  }
}