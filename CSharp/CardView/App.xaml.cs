/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This is the Application class of the Card Viewsample.
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

namespace Xceed.Wpf.DataGrid.Samples.CardView
{
  public partial class App : Application
  {
    protected override void OnStartup( StartupEventArgs e )
    {
      Xceed.Wpf.DataGrid.Samples.XceedDeploymentLicense.SetLicense();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetContactsDataSet();
      m_contacts = dataSet.Tables[ "Contacts" ];

      base.OnStartup( e );
    }

    #region Contacts property

    private DataTable m_contacts;

    public DataTable Contacts
    {
      get
      {
        return m_contacts;
      }
    }

    #endregion

    public event EventHandler ViewChanged;

    public void OnViewChanged( EventArgs e )
    {
      if( this.ViewChanged != null )
        this.ViewChanged( this, e );
    }
  }
}
