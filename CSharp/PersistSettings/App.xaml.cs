/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Persist User Settings Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Persist User Settings sample.
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

namespace Xceed.Wpf.DataGrid.Samples.PersistSettings
{
  public partial class App : Application
  {
    #region PROTECTED METHODS

    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      this.FillAvailableViews();

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

    #region ViewChanging Event

    public event EventHandler ViewChanging;

    public void OnViewChanging( EventArgs e )
    {
      if( this.ViewChanging != null )
        this.ViewChanging( this, e );
    }

    #endregion ViewChanging Event

    #region ViewChanged Event

    public event EventHandler ViewChanged;

    public void OnViewChanged( EventArgs e )
    {
      if( this.ViewChanged != null )
        this.ViewChanged( this, e );
    }

    #endregion ViewChanged Event

    #region AvailableViews Property

    public ObservableCollection<ViewItem> AvailableViews
    {
      get
      {
        return m_views;
      }
    }

    private ObservableCollection<ViewItem> m_views = new ObservableCollection<ViewItem>();

    private void FillAvailableViews()
    {
      m_views.Clear();

      m_views.Add( new ViewItem( "TableView", typeof( TableView ) ) );
      m_views.Add( new ViewItem( "CardView", typeof( CardView ) ) );
      m_views.Add( new ViewItem( "CompactCardView", typeof( CompactCardView ) ) );
    }

    #endregion AvailableViews Property
  }
}
