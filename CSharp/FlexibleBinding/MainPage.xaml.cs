/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
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
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  public partial class MainPage : Page
  {
    public MainPage()
    {
      m_doingInitializeComponent = true;
      InitializeComponent();
      m_doingInitializeComponent = false;
      this.ChangeDataSource( cboDataSource.SelectedValue as string );
    }

    private void cboDataSource_SelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( m_doingInitializeComponent )
        return;

      this.ChangeDataSource( ( ( DictionaryEntry )e.AddedItems[ 0 ] ).Key as string );
    }

    private void ChangeDataSource( string selectedSource )
    {
      switch( selectedSource )
      {
        // String Array (an example of a simple type)
        case "1":
          this.BindToStringArray();
          break;

        // Jagged Array
        case "2":
          this.BindToJaggedArray();
          break;

        // Collection
        case "3":
          this.BindToCollectionBase();
          break;

        // DataGridCollectionView bound to a XmlDocument
        case "4":
          this.BindToXmlDocument();
          break;

        // DataGridCollectionView bound to a LINQ query using XDocument
        case "5":
          this.BindToXDocument();
          break;

        // DataGridCollectionView bound to a Collection implementing IBindingList
        case "6":
          this.BindToBindingListComplete();
          break;

        // DataGridCollectionView bound to a DataTable
        case "7":
          this.BindToDataGridCollectionView();
          break;

        // DataGridCollectionView bound to a DataTable
        case "8":
          this.BindToDataGridCollectionViewWithMasterDetail();
          break;
      }
    }

    private void ResetGrid( bool showInsertionRow, bool showGroupByControl, bool isMasterDetail )
    {
      // Clear the grid.
      grid.ClearValue( DataGridControl.ItemsSourceProperty );
      grid.Items.Clear();
      grid.Items.SortDescriptions.Clear();
      grid.Items.GroupDescriptions.Clear();
      grid.Columns.Clear();
      grid.DetailConfigurations.Clear();

      // Put the grid in read/write mode.
      grid.ReadOnly = false;

      // Prevent automatic creation of the detail configurations.
      grid.AutoCreateDetailConfigurations = false;

      this.AdjustHeadersFooters( showInsertionRow, showGroupByControl, isMasterDetail );
    }

    private void AdjustHeadersFooters( bool showInsertionRow, bool showGroupByControl, bool isMasterDetail )
    {
      grid.View.FixedHeaders.Clear();
      grid.View.Headers.Clear();
      DataTemplate template;

      if( grid.View is Xceed.Wpf.DataGrid.Views.CardView )
      {
        // When in card view, we select the correct template for the 
        // ColumnManagerRow and GroupByControl instead of just adding them 
        // directly to the FixedHeaders to have a more attractive and compact look
        if( showGroupByControl )
        {
          template = this.FindResource( "cardViewColumnManagerRowAndGroupByControl" ) as DataTemplate;
          grid.View.FixedHeaders.Add( template );
        }

        // Add the insertion row in the Headers if desired
        if( showInsertionRow )
        {
          template = new DataTemplate();
          template.VisualTree = new FrameworkElementFactory( typeof( InsertionRow ) );
          template.Seal();
          grid.View.Headers.Add( template );
        }
      }
      else
      {
        // When in table view, we simply add a ColumnManagerRow and GroupByControl
        // in the FixedHeaders
        if( showGroupByControl )
        {
          FrameworkElementFactory groupByControl;

          if( isMasterDetail )
          {
            groupByControl = new FrameworkElementFactory( typeof( HierarchicalGroupByControl ) );
          }
          else
          {
            groupByControl = new FrameworkElementFactory( typeof( GroupByControl ) );
          }

          // In most case, it is desirable to prevent the GroupByControl to scroll horizontally.
          groupByControl.SetValue( Xceed.Wpf.DataGrid.Views.TableView.CanScrollHorizontallyProperty, false );
          template = new DataTemplate();
          template.VisualTree = groupByControl;
          template.Seal();
          grid.View.FixedHeaders.Add( template );
        }

        template = new DataTemplate();
        template.VisualTree = new FrameworkElementFactory( typeof( ColumnManagerRow ) );
        template.Seal();
        grid.View.FixedHeaders.Add( template );

        // Add the insertion row in the FixedHeaders if desired
        if( showInsertionRow )
        {
          template = new DataTemplate();
          FrameworkElementFactory insertionRowFactory = new FrameworkElementFactory( typeof( InsertionRow ) );
          template.VisualTree = insertionRowFactory;
          template.Seal();
          grid.View.FixedHeaders.Add( template );
        }
      }
    }

    private DataGridForeignKeyDescription GetPeriodForeignKeyDescription()
    {
      // Create a ForeignKeyDescription so sorting will be based on the string value.
      // Moreover, once this will be set on the corresponding ItemProperty, and  since AutoCreateForeignKeyConfiguration on DataGridControl is set to true,
      // the corresponding ForeignKeyConfiguration will be generated, and as a result the default ForeignKey CellEditor (a ComboBox), GroupValueTemplate,
      // ScrollTip.ContentTemplate and AutoFilterControl.DistinctValueItemTemplate will be correctly set.
      var foreignKeyDescription = new DataGridForeignKeyDescription();
      foreignKeyDescription.ItemsSource = Enum.GetValues( typeof( Period ) );
      foreignKeyDescription.ValuePath = ".";
      foreignKeyDescription.ForeignKeyConverter = new PeriodForeignKeyConverter();

      return foreignKeyDescription;
    }

    #region StringArray data binding

    private void BindToStringArray()
    {
      txtDescription.Text = Data.StringArrayText;

      // Reset the grid to its initial state.
      this.ResetGrid( false, false, false );

      string[] stringArray = Enum.GetNames( typeof( Key ) );

      // Bind the grid to the string array.
      grid.ItemsSource = stringArray;

      //Set the Grid to readonly, as the items are value type and the created column will be bound to self (edition would be innefective)
      grid.ReadOnly = true;

      // Change the Title of the column.
      grid.Columns[ 0 ].Title = "Input.Key";
      grid.Columns[ 0 ].Width = 400;
    }

    #endregion StringArray data binding

    #region JaggedArray data binding

    private void BindToJaggedArray()
    {
      // With this binding, only data modifications will work.
      txtDescription.Text = Data.JaggedArrayText;
      string[][] composers = new string[ Data.Composers.Length ][];

      // Fill the jagged array with composers
      for( int i = 0; i != Data.Composers.Length; i++ )
      {
        composers[ i ] = new String[ 5 ];
        composers[ i ][ 0 ] = Data.Composers[ i ].LastName;
        composers[ i ][ 1 ] = Data.Composers[ i ].FirstName;
        composers[ i ][ 2 ] = Data.Composers[ i ].Period.ToString();
        composers[ i ][ 3 ] = Data.Composers[ i ].BirthYear.ToString();
        composers[ i ][ 4 ] = Data.Composers[ i ].DeathYear.ToString();
      }

      // Reset the grid to its initial state.
      this.ResetGrid( false, true, false );

      // Bind the grid to the jagged array.
      grid.ItemsSource = composers;

      // Change the titles of the columns.
      grid.Columns[ 0 ].Title = "Last Name";
      grid.Columns[ 1 ].Title = "First Name";
      grid.Columns[ 2 ].Title = "Period";
      grid.Columns[ 3 ].Title = "Birth Year";
      grid.Columns[ 4 ].Title = "Death Year";

      // Configure Period ComboBox.
      Column periodColumn = grid.Columns[ 2 ] as Column;
      periodColumn.CellContentTemplate = this.FindResource( "periodDataTemplate" ) as DataTemplate;
    }

    #endregion JaggedArray data binding

    #region XmlDocument data binding

    private void BindToXmlDocument()
    {
      // With this binding, adding, removing, and modifying data will work.

      txtDescription.Text = Data.XMLDocumentText;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load( Application.GetResourceStream( new Uri( "pack://application:,,,/Data/Composers.xml" ) ).Stream );

      // Reset the grid to its initial state.
      this.ResetGrid( true, true, false );

      DataGridCollectionView dataGridCollectionView = new DataGridCollectionView( xmlDocument.SelectSingleNode( "Composers" ), null, false, false );

      // Create the item properties for the collection view
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "LastName", "LastName", null, typeof( string ) )
      {
        Title = "Last Name"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "FirstName", "FirstName", null, typeof( string ) )
      {
        Title = "First Name"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "Period", "Period", null, typeof( Period ) )
      {
        ForeignKeyDescription = this.GetPeriodForeignKeyDescription()
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "BirthYear", "BirthYear", null, typeof( int ) )
      {
        Title = "Birth Year"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "DeathYear", "DeathYear", null, typeof( int ) )
      {
        Title = "Death Year"
      } );

      // Subscribe to various events to handle the adding and removing of nodes
      dataGridCollectionView.CancelingNewItem += new EventHandler<DataGridItemHandledEventArgs>( dataGridCollectionView_CancelingNewItem );
      dataGridCollectionView.CommittingNewItem += new EventHandler<DataGridCommittingNewItemEventArgs>( dataGridCollectionView_CommittingNewItem );
      dataGridCollectionView.CreatingNewItem += new EventHandler<DataGridCreatingNewItemEventArgs>( dataGridCollectionView_CreatingNewItem );
      dataGridCollectionView.RemovingItem += new EventHandler<DataGridRemovingItemEventArgs>( dataGridCollectionView_RemovingItem );

      // Bind the grid to the CollectionView.
      grid.ItemsSource = dataGridCollectionView;
    }

    private void dataGridCollectionView_CreatingNewItem( object sender, DataGridCreatingNewItemEventArgs e )
    {
      // Creating an empty node that will be commited to the source in the CommittingNewItem event
      XmlNode parentNode = e.CollectionView.SourceCollection as XmlNode;
      XmlDocument xmlDocument = parentNode.OwnerDocument;

      XmlElement newElement = xmlDocument.CreateElement( "Composer" );
      newElement.AppendChild( xmlDocument.CreateElement( "LastName" ) );
      newElement.AppendChild( xmlDocument.CreateElement( "FirstName" ) );
      newElement.AppendChild( xmlDocument.CreateElement( "Period" ) );
      newElement.AppendChild( xmlDocument.CreateElement( "BirthYear" ) );
      newElement.AppendChild( xmlDocument.CreateElement( "DeathYear" ) );

      e.NewItem = newElement;

      // We must "handle" this event when manualy handling insertion
      e.Handled = true;
    }

    private void dataGridCollectionView_CommittingNewItem( object sender, DataGridCommittingNewItemEventArgs e )
    {
      // Commit the new node that was created in the CreatingNewItem event
      XmlNode parentNode = e.CollectionView.SourceCollection as XmlNode;
      parentNode.AppendChild( e.Item as XmlNode );
      e.NewCount = parentNode.ChildNodes.Count;
      e.Index = e.NewCount - 1;

      // We must "handle" this event when manualy handling insertion
      e.Handled = true;
    }

    private void dataGridCollectionView_CancelingNewItem( object sender, DataGridItemHandledEventArgs e )
    {
      // We must "handle" this event when manualy handling insertion even if nothing specific is done
      e.Handled = true;
    }

    private void dataGridCollectionView_RemovingItem( object sender, DataGridRemovingItemEventArgs e )
    {
      // Remove the node from the source
      XmlNode childNode = e.Item as XmlNode;

      if( childNode == null )
        throw new InvalidOperationException();

      XmlNode parentNode = e.CollectionView.SourceCollection as XmlNode;
      parentNode.RemoveChild( childNode );

      // We must "handle" this event when manualy handling the removal of items
      e.Handled = true;
    }

    #endregion XmlDocument data binding

    #region LINQ query using XDocument data binding

    private void BindToXDocument()
    {
      // With this binding, adding, removing, and modifying data will not work.

      txtDescription.Text = Data.XDocumentText;

      XDocument xDocument = XDocument.Load(
        XmlReader.Create(
          Application.GetResourceStream( new Uri( "pack://application:,,,/Data/Composers.xml" ) ).Stream ) );

      // Reset the grid to its initial state.
      this.ResetGrid( false, true, false );

      IEnumerable items = from item in xDocument.Element( "Composers" ).Descendants( "Composer" )
                          select new Composer(
                            item.Element( "LastName" ).Value,
                            item.Element( "FirstName" ).Value,
                            ( Period )Enum.Parse( typeof( Period ), item.Element( "Period" ).Value ),
                            int.Parse( item.Element( "BirthYear" ).Value ),
                            int.Parse( item.Element( "DeathYear" ).Value ) );

      DataGridCollectionView dataGridCollectionView = new DataGridCollectionView( items, null, false, false );

      // Create the item properties for the collection view with correct type to have proper grouping and sorting
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "LastName", typeof( string ) )
      {
        Title = "Last Name"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "FirstName", typeof( string ) )
      {
        Title = "First Name"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "Period", typeof( Period ) )
      {
        ForeignKeyDescription = this.GetPeriodForeignKeyDescription()
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "BirthYear", typeof( int ) )
      {
        Title = "Birth Year"
      } );
      dataGridCollectionView.ItemProperties.Add( new DataGridItemProperty( "DeathYear", typeof( int ) )
      {
        Title = "Death Year"
      } );

      // Bind the grid to the CollectionView.
      grid.ItemsSource = dataGridCollectionView;
    }

    #endregion LINQ query using XDocument data binding

    #region Collection data binding

    private void BindToCollectionBase()
    {
      // With this binding, only data modifications will work.
      txtDescription.Text = Data.CollectionBaseText;
      ComposersCollection collection = new ComposersCollection();

      // Fill the collection with composers
      foreach( Data.ComposerData composer in Data.Composers )
      {
        collection.Add( new Composer( composer.LastName, composer.FirstName, composer.Period, composer.BirthYear, composer.DeathYear ) );
      }

      // Reset the grid to its initial state.
      this.ResetGrid( false, true, false );

      // Bind the grid to the composer collection. 
      grid.ItemsSource = collection;

      // Change the titles of the columns.
      grid.Columns[ 0 ].Title = "Last Name";
      grid.Columns[ 1 ].Title = "First Name";
      grid.Columns[ 3 ].Title = "Birth Year";
      grid.Columns[ 4 ].Title = "Death Year";

      // Configure comboBoxes.
      var periodColumn = grid.Columns[ "Period" ] as Column;
      periodColumn.CellContentTemplate = this.FindResource( "periodDataTemplate" ) as DataTemplate;

      // Create a ForeignKeyConfiguration in order to get the default
      // ComboBox CellEditor and GroupValueTemplate, ScrollTip.ContentTemplate and
      // AutoFilterControl.DistinctValueItemTemplate using the CellContentTemplate
      ForeignKeyConfiguration fkConfiguration = new ForeignKeyConfiguration();
      fkConfiguration.ItemsSource = Enum.GetValues( typeof( Period ) );
      fkConfiguration.ValuePath = ".";
      periodColumn.ForeignKeyConfiguration = fkConfiguration;
    }

    #endregion Collection data binding

    #region Collection implementing IBindingList data binding

    private void BindToBindingListComplete()
    {
      // With this binding, adding, removing, and modifying data will work.

      txtDescription.Text = Data.CompleteBindingListText;
      ComposersBindingListComplete collection = new ComposersBindingListComplete();

      // Put the collection in batch initialization to prevent the ListChanged event from raising.
      collection.BeginInit();

      // Fill the collection with composers
      foreach( Data.ComposerData composer in Data.Composers )
      {
        collection.Add( new ComposerEditable( composer.LastName, composer.FirstName, composer.Period, composer.BirthYear, composer.DeathYear ) );
      }

      // End the batch initialization of the collection.
      collection.EndInit();

      // Reset the grid to its initial state.
      this.ResetGrid( true, true, false );

      // Bind the grid to the composer collection via DataGridCollectionView.
      DataGridCollectionView dataGridCollectionView = new DataGridCollectionView( collection );
      grid.ItemsSource = dataGridCollectionView;

      dataGridCollectionView.ItemProperties[ "Period" ].ForeignKeyDescription = this.GetPeriodForeignKeyDescription();

      // Change the titles of the columns.
      grid.Columns[ 0 ].Title = "Last Name";
      grid.Columns[ 1 ].Title = "First Name";
      grid.Columns[ 3 ].Title = "Birth Year";
      grid.Columns[ 4 ].Title = "Death Year";
    }

    #endregion Collection implementing IBindingList data binding

    #region DataTable data binding

    private void BindToDataGridCollectionView()
    {
      // With this binding, adding, removing, and modifying data will work.
      txtDescription.Text = Data.DataGridCollectionViewText;

      // Reset the grid to its initial state.
      this.ResetGrid( true, true, false );

      // Create an Xceed DataGridCollectionView using the Products DataTable as its source.
      DataGridCollectionView dataGridCollectionView = new DataGridCollectionView( ( ( App )Application.Current ).Products.DefaultView, null, true, true, true );

      // Set ForeignKeys so these columns can be sorted based on display value rather than ID value.
      var foreignKeyDescription = new DataTableForeignKeyDescription()
      {
        ItemsSource = ( ( App )Application.Current ).Suppliers.DefaultView,
        ValuePath = "SupplierID",
        DisplayMemberPath = "CompanyName"
      };
      dataGridCollectionView.ItemProperties[ "SupplierID" ].ForeignKeyDescription = foreignKeyDescription;

      foreignKeyDescription = new DataTableForeignKeyDescription()
      {
        ItemsSource = ( ( App )Application.Current ).Categories.DefaultView,
        ValuePath = "CategoryID",
        DisplayMemberPath = "CategoryName"
      };
      dataGridCollectionView.ItemProperties[ "CategoryID" ].ForeignKeyDescription = foreignKeyDescription;

      // Bind the grid to the Xceed DataGridCollectionView.
      grid.ItemsSource = dataGridCollectionView;

      // Hide unwanted columns.
      grid.Columns[ "ProductID" ].Visible = false;
      grid.Columns[ "ProductID" ].ShowInColumnChooser = false;
      grid.Columns[ "ReorderLevel" ].Visible = false;
      grid.Columns[ "ReorderLevel" ].ShowInColumnChooser = false;
      grid.Columns[ "UnitsInStock" ].Visible = false;
      grid.Columns[ "UnitsInStock" ].ShowInColumnChooser = false;
      grid.Columns[ "QuantityPerUnit" ].Visible = false;
      grid.Columns[ "QuantityPerUnit" ].ShowInColumnChooser = false;
      grid.Columns[ "Photo" ].Visible = false;
      grid.Columns[ "Photo" ].ShowInColumnChooser = false;

      // Format visible columns
      grid.Columns[ "ProductName" ].Width = 200;
      grid.Columns[ "ProductName" ].Title = "Product";
      grid.Columns[ "SupplierID" ].Title = "Supplier";
      grid.Columns[ "SupplierID" ].Width = 200;
      grid.Columns[ "CategoryID" ].Title = "Category";
      grid.Columns[ "UnitPrice" ].CellContentTemplate = this.FindResource( "currencyCellDataTemplate" ) as DataTemplate;
      grid.Columns[ "UnitPrice" ].Title = "Unit Price";
      grid.Columns[ "UnitsOnOrder" ].Title = "Units On Order";
      grid.Columns[ "ReorderDate" ].CellContentTemplate = this.FindResource( "shortDateCellDataTemplate" ) as DataTemplate;
      grid.Columns[ "ReorderDate" ].Title = "Reorder Date";
    }

    #endregion DataTable data binding

    #region DataTable data binding with master/detail

    private void BindToDataGridCollectionViewWithMasterDetail()
    {
      // With this binding, adding, removing, and modifying data will work.
      txtDescription.Text = Data.DataGridCollectionViewWithMasterDetailText;

      // Reset the grid to its initial state.
      this.ResetGrid( true, true, true );

      // Create an Xceed DataGridCollectionView using the Orders DataTable as its source.
      DataGridCollectionView dataGridCollectionView = new DataGridCollectionView( ( ( App )Application.Current ).Orders.DefaultView, null, true, true, true );

      // Set a ForeignKey so customers are sorted based on names rather than ID.
      var foreignKeyDescription = new DataTableForeignKeyDescription()
      {
        ItemsSource = ( ( App )Application.Current ).Customers.DefaultView,
        ValuePath = "CustomerID",
        ForeignKeyConverter = new CustomerForeignKeyConverter()
      };
      dataGridCollectionView.ItemProperties[ "CustomerID" ].ForeignKeyDescription = foreignKeyDescription;

      // Set a ForeignKey at detail level so products are sorted based on names rather than ID.
      foreignKeyDescription = new DataTableForeignKeyDescription()
      {
        ItemsSource = ( ( App )Application.Current ).Products.DefaultView,
        ValuePath = "ProductID",
        DisplayMemberPath = "ProductName"
      };
      dataGridCollectionView.DetailDescriptions[ 0 ].ItemProperties.Add( new DataGridItemProperty( "ProductID", typeof( int ) )
      {
        ForeignKeyDescription = foreignKeyDescription
      } );

      // Automaticaly create the detail configurations.
      grid.AutoCreateDetailConfigurations = true;

      // Bind the grid to the Xceed DataGridCollectionView.
      grid.ItemsSource = dataGridCollectionView;

      // Hide unwanted columns.
      grid.Columns[ "OrderID" ].Visible = false;
      grid.Columns[ "OrderID" ].ShowInColumnChooser = false;
      grid.Columns[ "EmployeeID" ].Visible = false;
      grid.Columns[ "EmployeeID" ].ShowInColumnChooser = false;
      grid.Columns[ "ShipRegion" ].Visible = false;
      grid.Columns[ "ShipRegion" ].ShowInColumnChooser = false;
      grid.Columns[ "ShippedDate" ].Visible = false;
      grid.Columns[ "ShippedDate" ].ShowInColumnChooser = false;
      grid.Columns[ "RequiredDate" ].Visible = false;
      grid.Columns[ "RequiredDate" ].ShowInColumnChooser = false;
      grid.Columns[ "ShipVia" ].Visible = false;
      grid.Columns[ "ShipVia" ].ShowInColumnChooser = false;
      grid.Columns[ "ShipName" ].Visible = false;
      grid.Columns[ "ShipName" ].ShowInColumnChooser = false;

      // Format visible columns
      grid.Columns[ "OrderDate" ].CellContentTemplate = this.FindResource( "shortDateCellDataTemplate" ) as DataTemplate;
      grid.Columns[ "OrderDate" ].Title = "Order Date";
      grid.Columns[ "Freight" ].CellContentTemplate = this.FindResource( "currencyCellDataTemplate" ) as DataTemplate;
      grid.Columns[ "ShipCountry" ].CellContentTemplate = this.FindResource( "countryCellContentTemplate" ) as DataTemplate;
      grid.Columns[ "ShipCountry" ].GroupValueTemplate = this.FindResource( "countryCellContentTemplate" ) as DataTemplate;
      grid.Columns[ "ShipCountry" ].VisiblePosition = 8;
      grid.Columns[ "ShipCountry" ].Title = "Country";
      grid.Columns[ "ShipAddress" ].Title = "Address";
      grid.Columns[ "ShipCity" ].Title = "City";
      grid.Columns[ "ShipPostalCode" ].Title = "Postal Code";

      //Because the complex CellContentTemplate defined on the CustomerID column, a ForeignKeyValueConverter must be defined so the search feature will work correctly on this column.
      var customerForeignKeyConfig = new ForeignKeyConfiguration()
      {
        ItemsSource = ( ( App )Application.Current ).Customers.DefaultView,
        ValuePath = "CustomerID",
        ForeignKeyValueConverter = new CustomerConverter()
      };

      var customerIDColumn = grid.Columns[ "CustomerID" ] as Column;
      customerIDColumn.Title = "Customer";
      customerIDColumn.Width = 200;
      customerIDColumn.VisiblePosition = 0;
      customerIDColumn.CellContentTemplate = this.FindResource( "customerDataTemplate" ) as DataTemplate;
      customerIDColumn.ForeignKeyConfiguration = customerForeignKeyConfig;

      // Configure detail grid
      var productsDetailConfiguration = grid.DetailConfigurations[ 0 ];
      productsDetailConfiguration.Title = "Products";
      productsDetailConfiguration.AutoCreateForeignKeyConfigurations = true;

      var column = new Column();
      column.FieldName = "Discount";
      column.Visible = false;
      column.ShowInColumnChooser = false;
      productsDetailConfiguration.Columns.Add( column );

      column = new Column();
      column.FieldName = "OrderID";
      column.Visible = false;
      column.ShowInColumnChooser = false;
      productsDetailConfiguration.Columns.Add( column );

      column = new Column();
      column.FieldName = "ProductID";
      column.Title = "Product";
      column.Width = 200;
      productsDetailConfiguration.Columns.Add( column );

      column = new Column();
      column.FieldName = "UnitPrice";
      column.Title = "Unit Price";
      column.CellContentTemplate = this.FindResource( "currencyCellDataTemplate" ) as DataTemplate;
      productsDetailConfiguration.Columns.Add( column );

    }

    #endregion DataTable data binding with master/detail

    public bool m_doingInitializeComponent; // = false;
  }
}
