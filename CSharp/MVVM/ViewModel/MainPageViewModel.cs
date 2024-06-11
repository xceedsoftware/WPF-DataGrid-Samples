/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [MainPageViewModel.cs]
 *  
 * This class exposes the model to the view, and provides business logic interaction.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.DataGrid.FilterCriteria;
using Xceed.Wpf.DataGrid.Samples.MVVM.Model;
using Xceed.Wpf.DataGrid.Samples.MVVM.Repository;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
{
  public class MainPageViewModel : INotifyPropertyChanged, IWeakEventListener
  {
    public MainPageViewModel()
    {
      m_supplierRepository = new SupplierRepository();
      this.Suppliers = m_supplierRepository.GetAllSuppliers();

      m_categoryRepository = new CategoryRepository();
      this.Categories = m_categoryRepository.GetAllCategories();

      m_productRepository = new ProductRepository();
      this.Products = m_productRepository.GetAllProducts();

      //OrderDetails will be created as Products are loaded from the ProductRepository.

      this.RegisterNotifications();
      this.PrepareNextProduct();

      this.FilterValue = m_filterValues[ 0 ];
      this.ProductNameDescription = "This column represents the name of the product";
      this.OrderDetailsDescription = "These represent orders that were fufilled for the corresponding product";
      this.ProductsTitle = "Products";
      this.OrderDetailsTitle = "Product Orders";
    }

    #region ProductsCollectionView Property

    public ICollectionView ProductsCollectionView
    {
      get
      {
        if( m_productsCollectionView != null )
          return m_productsCollectionView;

        //Load the CollectionView to which the DataGrid is bound from the ProductsCollectionViewResources.xaml file.
        return m_productsCollectionView = ( Application.Current.FindResource( "productsCollectionViewSource" ) as CollectionViewSource ).View as ICollectionView;
      }
    }

    private ICollectionView m_productsCollectionView;

    #endregion

    #region Products Property

    public ObservableCollection<Product> Products
    {
      get;
      private set;
    }

    #endregion

    #region Suppliers Property

    public ObservableCollection<Supplier> Suppliers
    {
      get;
      private set;
    }

    #endregion

    #region Categories Property

    public ObservableCollection<Category> Categories
    {
      get;
      private set;
    }

    #endregion

    #region FilterValues Property

    public string[] FilterValues
    {
      get
      {
        return m_filterValues;
      }
    }

    private string[] m_filterValues = new string[] { "All", "Maintained", "Discontinued" };

    #endregion

    #region FilterValue Property

    public string FilterValue
    {
      get
      {
        return m_filterValue;
      }
      set
      {
        if( value != m_filterValue )
        {
          m_filterValue = value;
          this.OnPropertyChanged( "FilterValue" );
        }
      }
    }

    private string m_filterValue;

    #endregion

    #region MasterSelectedItems Property

    //Contains items currently selected in the grid at the master level.
    public ObservableCollection<Product> MasterSelectedItems
    {
      get
      {
        return m_masterSelectedItems;
      }
    }

    private ObservableCollection<Product> m_masterSelectedItems = new ObservableCollection<Product>();

    #endregion

    #region SelectedItemsCount Property

    //Provides feedback when selection is changing
    public int SelectedItemsCount
    {
      get
      {
        return m_selectedItemsCount;
      }
      set
      {
        if( value != m_selectedItemsCount )
        {
          m_selectedItemsCount = value;
          this.OnPropertyChanged( "SelectedItemsCount" );
        }
      }
    }

    private int m_selectedItemsCount;

    #endregion

    #region ProductNameDescription Property

    public string ProductNameDescription
    {
      get
      {
        return m_productNameDescription;
      }
      private set
      {
        if( value != m_productNameDescription )
        {
          m_productNameDescription = value;
          this.OnPropertyChanged( "ProductNameDescription" );
        }
      }
    }

    private string m_productNameDescription;

    #endregion

    #region OrderDetailsDescription Property

    public string OrderDetailsDescription
    {
      get
      {
        return m_orderDetailsDescription;
      }
      private set
      {
        if( value != m_orderDetailsDescription )
        {
          m_orderDetailsDescription = value;
          this.OnPropertyChanged( "OrderDetailsDescription" );
        }
      }
    }

    private string m_orderDetailsDescription;

    #endregion

    #region ProductsTitle Property

    public string ProductsTitle
    {
      get
      {
        return m_productsTitle;
      }
      set
      {
        if( value != m_productsTitle )
        {
          m_productsTitle = value;
          this.OnPropertyChanged( "ProductsTitle" );
        }
      }
    }

    private string m_productsTitle;

    #endregion

    #region OrderDetailsTitle Property

    public string OrderDetailsTitle
    {
      get
      {
        return m_orderDetailsTitle;
      }
      private set
      {
        if( value != m_orderDetailsTitle )
        {
          m_orderDetailsTitle = value;
          this.OnPropertyChanged( "OrderDetailsTitle" );
        }
      }
    }

    private string m_orderDetailsTitle;

    #endregion

    #region CurrentItem Property

    public object CurrentItem
    {
      get
      {
        return m_currentItem;
      }
      set
      {
        if( value != m_currentItem )
        {
          m_currentItem = value;
          this.OnPropertyChanged( "CurrentItem" );
        }
      }
    }

    private object m_currentItem;

    #endregion

    #region CurrentColumnFieldName Property

    public string CurrentColumnFieldName
    {
      get
      {
        return m_currentColumnFieldName;
      }
      set
      {
        if( value != m_currentColumnFieldName )
        {
          m_currentColumnFieldName = value;
          this.OnPropertyChanged( "CurrentColumnFieldName" );
        }
      }
    }

    private string m_currentColumnFieldName;

    #endregion

    #region InitialProductIDValue Property

    //Used to initialize the ProductID value of the insertion row.
    public int InitialProductIDValue
    {
      get
      {
        return m_initialProductIDValue;
      }
      set
      {
        if( value != m_initialProductIDValue )
        {
          m_initialProductIDValue = value;
          this.OnPropertyChanged( "InitialProductIDValue" );
        }
      }
    }

    private int m_initialProductIDValue;

    #endregion

    public void ProvideSelectedList( object sender, EventArgs e )
    {
      //This method is hooked to the DetailsExpanding event in the grid, and allows to provide a list of selected items for every expanded detail grid.
      var eventArgs = e as DetailsExpansionChangingEventArgs;
      var masterItem = eventArgs.MasterItem;
      var selectedItemsSource = default( ObservableCollection<OrderDetail> );

      //If the detail grid has been expanded in the pass, a SelectedItemsSource aready exist, so get and use it.
      if( m_collpasedDetailsSelectedItems.TryGetValue( masterItem, out selectedItemsSource ) )
      {
        //But remove it first from the collapsed collection.
        m_collpasedDetailsSelectedItems.Remove( masterItem );
      }

      //If none was return, create one.
      if( selectedItemsSource == null )
      {
        selectedItemsSource = new ObservableCollection<OrderDetail>();
      }

      //Add listener so the ViewModel can react when SelectedIems change.
      CollectionChangedEventManager.AddListener( selectedItemsSource, this );

      //Keep a reference to it, and provide it to the DataGrid.
      m_currentDetailsSelectedItems.Add( masterItem, selectedItemsSource );
      eventArgs.SelectedItemsSources.Add( "OrderDetails", selectedItemsSource );

      //And update the count that is displayed in the View.
      this.SelectedItemsCount += selectedItemsSource.Count;
    }

    public void PreserveSelectedList( object sender, EventArgs e )
    {
      //This method is hooked to the DetailsCollapsing event in the grid, and allows to keep a reference to selected items in a collapsing detail grid,
      //so it can be provided back when the detail is expanded again.
      var eventArgs = e as DetailsExpansionChangingEventArgs;
      var masterItem = eventArgs.MasterItem;
      var selectedItemsSource = default( ObservableCollection<OrderDetail> );

      //There should always be a corresponding SelectedItemsSource
      if( m_currentDetailsSelectedItems.TryGetValue( masterItem, out selectedItemsSource ) )
      {
        //Remove the listener that was added in the ProvideSelectedList method.
        CollectionChangedEventManager.RemoveListener( selectedItemsSource, this );

        //Remove it from the dictionary, as this list will get cleared by the grid after the detail grid is collapsed.
        m_currentDetailsSelectedItems.Remove( masterItem );

        //Create a new list, to which a reference is kept, and in which items still present in the SelectedItemsSource are copied to.
        var selectedItemsToKeep = new ObservableCollection<OrderDetail>( selectedItemsSource as ObservableCollection<OrderDetail> );
        m_collpasedDetailsSelectedItems.Add( masterItem, selectedItemsToKeep );

        //And update the count that is displayed in the View (selected items of a collapsed detail grid are not part of SelectedItems in the DataGrid any longer).
        this.SelectedItemsCount -= selectedItemsToKeep.Count;
      }
    }

    private void RegisterNotifications()
    {
      //The ViewModel needs to be notified when lists or items to which the DataGrid is bound change.
      CollectionChangedEventManager.AddListener( this.Products, this );
      CollectionChangedEventManager.AddListener( this.MasterSelectedItems, this );

      foreach( var product in this.Products )
      {
        CollectionChangedEventManager.AddListener( product.OrderDetails, this );
        PropertyChangedEventManager.AddListener( product, this, "" );

        foreach( var orderDetail in product.OrderDetails )
        {
          PropertyChangedEventManager.AddListener( orderDetail, this, "" );
        }
      }
    }

    private void OnProductsCollectionChanged( NotifyCollectionChangedEventArgs e )
    {
      switch( e.Action )
      {
        case NotifyCollectionChangedAction.Add:
          {
            //If the item is coming from the DB, no need to add it, it's already present.
            if( m_isAddingFromDB )
              break;

            //For an item provided through the InsertionRow, add it to the DB.
            foreach( Product item in e.NewItems )
            {
              m_productRepository.AddNewProduct( item );
            }

            //Since this part of the code only runs when an item has been added by the end user (utilizing the InsertionRow),
            //get the next product right away so the next InitialProductIDValue value is valid when using the InsertionRow again.
            this.PrepareNextProduct();
            break;
          }

        case NotifyCollectionChangedAction.Remove:
          {
            foreach( Product item in e.OldItems )
            {
              //Remove the item that was deleted by the end user.
              m_productRepository.RemoveProduct( item );
            }
            break;
          }

        default:
          {
            break;
          }
      }
    }

    private void OnOrderDetailsCollectionChanged( NotifyCollectionChangedEventArgs e )
    {
      if( e.Action == NotifyCollectionChangedAction.Remove )
      {
        foreach( OrderDetail item in e.OldItems )
        {
          //Remove the item that was deleted by the end user.
          OrderDetailRepository.Singleton.RemoveOrderDetail( item );
        }
      }
    }

    private void OnProductPropertyChanged( Product item )
    {
      if( m_modifiedProducts.Contains( item ) )
        return;

      //Keep a reference to items that are modified by the end user.
      m_modifiedProducts.Add( item );

      //Notify the View that the item is modifed, so a visual cue is displayed.
      item.IsModified = true;
    }

    private void OnOrderDetailPropertyChanged( OrderDetail item )
    {
      if( m_modifiedOrderDetails.Contains( item ) )
        return;

      //Keep a reference to items that are modified by the end user.
      m_modifiedOrderDetails.Add( item );

      //Notify the View that the item is modifed, so a visual cue is displayed.
      item.IsModified = true;
    }

    private void OnSelectedItemsChanged()
    {
      var count = 0;

      count = this.MasterSelectedItems.Count;

      //Only count selected items of expanded detail grids.
      foreach( var list in m_currentDetailsSelectedItems.Values )
      {
        count += list.Count;
      }

      this.SelectedItemsCount = count;
    }

    private void PrepareNextProduct()
    {
      //This will serve either when simulating a new item coming from the DB, or when an item is added by the end user through the InsertionRow.
      m_newProduct = m_productRepository.GetNewProduct( this.Products.Count + 1 );

      this.InitialProductIDValue = m_newProduct.ProductID;
    }

    #region CommitModifiedItemsCommand Comand

    public ICommand CommitModifiedItemsCommand
    {
      get
      {
        if( m_commitModifiedItemsCommand != null )
          return m_commitModifiedItemsCommand;

        return m_commitModifiedItemsCommand = new RelayCommand( execute => this.CommitModifiedItems() );
      }
    }

    private ICommand m_commitModifiedItemsCommand;

    public void CommitModifiedItems()
    {
      foreach( var item in m_modifiedOrderDetails )
      {
        OrderDetailRepository.Singleton.UpdateOrderDetail( item );

        //Notify the View that the item is no longer modified, so a visual cue is removed.
        item.IsModified = false;
      }
      m_modifiedOrderDetails.Clear();

      foreach( var item in m_modifiedProducts )
      {
        m_productRepository.UpdateProduct( item );

        //Notify the View that the item is no longer modified, so a visual cue is removed.
        item.IsModified = false;
      }
      m_modifiedProducts.Clear();
    }

    #endregion

    #region GetNewItemCommand Comand

    public ICommand GetNewItemCommand
    {
      get
      {
        if( m_getNewItemCommand != null )
          return m_getNewItemCommand;

        return m_getNewItemCommand = new RelayCommand( execute => this.GetNewItem() );
      }
    }

    private ICommand m_getNewItemCommand;

    public void GetNewItem()
    {
      //This simulates a new item received from the DB.  The m_newProduct member already contains a new Product.
      m_isAddingFromDB = true;

      //First add it to Products, so it gets added to the CollectionView, and as a result, to the DataGrid.
      this.Products.Add( m_newProduct );

      //Then set current item and column, so the grid will bring the new item into view.
      this.CurrentItem = m_newProduct;
      this.CurrentColumnFieldName = "ProductName";

      //Prepare the next product right away.  This is to accomodate an insertion by the end user in the View (via the InsertionRow).
      this.PrepareNextProduct();

      m_isAddingFromDB = false;
    }

    #endregion

    #region ApplyFilterCommand Comand

    public ICommand ApplyFilterCommand
    {
      get
      {
        if( m_applyFilterCommand != null )
          return m_applyFilterCommand;

        return m_applyFilterCommand = new RelayCommand( execute => this.ApplyFilter() );
      }
    }

    private ICommand m_applyFilterCommand;

    public void ApplyFilter()
    {
      //Build a filter criterion that corresponds to the value selected by the end user in the ComboBox in the View.
      var filterCriterion = default( FilterCriterion );

      switch( this.FilterValue )
      {
        case "Maintained":
          {
            filterCriterion = new EqualToFilterCriterion( false );
            break;
          }

        case "Discontinued":
          {
            filterCriterion = new EqualToFilterCriterion( true );
            break;
          }

        default:
          {
            filterCriterion = null;
            break;
          }
      }

      //Apply the filter criterion to the grid, via its collection view.
      var dataGridCollectionView = this.ProductsCollectionView as DataGridCollectionView;
      dataGridCollectionView.ItemProperties[ "Discontinued" ].FilterCriterion = filterCriterion;
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged( string propertyName )
    {
      var handler = this.PropertyChanged;
      if( handler == null )
        return;

      handler.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }

    #endregion

    #region IWeakEventListener Members

    bool IWeakEventListener.ReceiveWeakEvent( Type managerType, object sender, EventArgs e )
    {
      if( managerType == typeof( CollectionChangedEventManager ) )
      {
        var eventArgs = e as NotifyCollectionChangedEventArgs;
        if( sender == this.MasterSelectedItems )
        {
          this.OnSelectedItemsChanged();
        }
        else if( sender == this.Products )
        {
          this.OnProductsCollectionChanged( eventArgs );
        }
        else
        {
          //Verify if the CollectionChanged event is coming from a detail grid's SelectedItemsSource list, or from a Product's OrderDetails list.
          var isDetailSelectedItemsSource = false;
          foreach( var list in m_currentDetailsSelectedItems.Values )
          {
            if( sender == list )
            {
              isDetailSelectedItemsSource = true;
              break;
            }
          }

          if( isDetailSelectedItemsSource )
          {
            this.OnSelectedItemsChanged();
          }
          else
          {
            this.OnOrderDetailsCollectionChanged( eventArgs );
          }
        }
        return true;
      }

      if( managerType == typeof( PropertyChangedEventManager ) )
      {
        //If this notification is coming from the ViewModel modifying the item, there is no need to process it any further.
        var eventArgs = e as PropertyChangedEventArgs;
        if( eventArgs.PropertyName == "IsModified" )
          return true;

        var item = sender as Product;
        if( item != null )
        {
          this.OnProductPropertyChanged( item );
        }
        else
        {
          this.OnOrderDetailPropertyChanged( sender as OrderDetail );
        }
        return true;
      }

      return false;
    }

    #endregion

    private Product m_newProduct;
    private bool m_isAddingFromDB;
    private ProductRepository m_productRepository;
    private SupplierRepository m_supplierRepository;
    private CategoryRepository m_categoryRepository;
    private HashSet<Product> m_modifiedProducts = new HashSet<Product>();
    private HashSet<OrderDetail> m_modifiedOrderDetails = new HashSet<OrderDetail>();
    private Dictionary<object, ObservableCollection<OrderDetail>> m_currentDetailsSelectedItems = new Dictionary<object, ObservableCollection<OrderDetail>>();
    private Dictionary<object, ObservableCollection<OrderDetail>> m_collpasedDetailsSelectedItems = new Dictionary<object, ObservableCollection<OrderDetail>>();
  }
}
