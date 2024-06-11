/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 * 
 * This is the main application of the Summaries & Statistics sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use
 * and distribution of this Sample Code is subject to the terms
 * and conditions referring to "Sample Code" that are specified in
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Data;
using System.Collections.ObjectModel;
using Xceed.Wpf.DataGrid.Views;

namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup( System.Windows.StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      this.FillAvailableViews();

      DataSet dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      m_products = dataSet.Tables[ "Products" ];
      m_categories = dataSet.Tables[ "Categories" ];
      m_suppliers = dataSet.Tables[ "Suppliers" ];

      base.OnStartup( e );
    }

    #region Products property

    private DataTable m_products;

    public DataTable Products
    {
      get
      {
        return m_products;
      }
    }

    #endregion Products property

    #region Categories property

    private DataTable m_categories;

    public DataTable Categories
    {
      get
      {
        return m_categories;
      }
    }

    #endregion Categories property

    #region Suppliers property

    private DataTable m_suppliers;

    public DataTable Suppliers
    {
      get
      {
        return m_suppliers;
      }
    }

    #endregion Suppliers property

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

      foreach( Type expType in typeof( DataGridControl ).Assembly.GetExportedTypes() )
      {
        if( ( typeof( UIViewBase ).IsAssignableFrom( expType ) ) && ( !expType.IsAbstract ) )
        {
          m_views.Add( new ViewItem( expType.Name, expType ) );
        }
      }
    }

    #endregion AvailableViews Property

    public event EventHandler ViewChanged;

    public void OnViewChanged( EventArgs e )
    {
      if( this.ViewChanged != null )
        this.ViewChanged( this, e );
    }
  }
}