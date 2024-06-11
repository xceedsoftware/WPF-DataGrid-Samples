'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [MainPage.xaml.vb]
'  
' This class implements the various dynamic configuration options offered
' by the configuration panel declared in MainPage.xaml.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Xml
Imports System.Xml.Linq
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  Partial Public Class MainPage
    Inherits Page
    Public Sub New()
      m_doingInitializeComponent = True
      InitializeComponent()
      m_doingInitializeComponent = False
      Me.ChangeDataSource(TryCast(cboDataSource.SelectedValue, String))
    End Sub

    Private Sub cboDataSource_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      If m_doingInitializeComponent Then
        Return
      End If

      Me.ChangeDataSource(TryCast(CType(e.AddedItems(0), DictionaryEntry).Key, String))
    End Sub

    Private Sub ChangeDataSource(ByVal selectedSource As String)
      Select Case selectedSource
        Case "1"
          ' String Array (an example of a simple type)
          Me.BindToStringArray()

        Case "2"
          ' Jagged Array
          Me.BindToJaggedArray()

        Case "3"
          ' Collection
          Me.BindToCollectionBase()

        Case "4"
          ' DataGridCollectionView bound to a XmlDocument
          Me.BindToXmlDocument()

        Case "5"
          ' DataGridCollectionView bound to a LINQ query using XDocument
          Me.BindToXDocument()

        Case "6"
          ' DataGridCollectionView bound to a Collection implementing IBindingList
          Me.BindToBindingListComplete()

        Case "7"
          ' DataGridCollectionView bound to a DataTable
          Me.BindToDataGridCollectionView()

        Case "8"
          ' DataGridCollectionView bound to a DataTable
          Me.BindToDataGridCollectionViewWithMasterDetail()

      End Select
    End Sub

    Private Sub ResetGrid(ByVal showInsertionRow As Boolean, ByVal showGroupByControl As Boolean, ByVal isMasterDetail As Boolean)
      ' Clear the grid.
      grid.ClearValue(DataGridControl.ItemsSourceProperty)
      grid.Items.Clear()
      grid.Items.SortDescriptions.Clear()
      grid.Items.GroupDescriptions.Clear()
      grid.Columns.Clear()
      grid.DetailConfigurations.Clear()

      ' Put the grid in read/write mode.
      grid.ReadOnly = False

      ' Prevent automatic creation of the detail configurations.
      grid.AutoCreateDetailConfigurations = False

      Me.AdjustHeadersFooters(showInsertionRow, showGroupByControl, isMasterDetail)
    End Sub

    Private Sub AdjustHeadersFooters(ByVal showInsertionRow As Boolean, ByVal showGroupByControl As Boolean, ByVal isMasterDetail As Boolean)
      grid.View.FixedHeaders.Clear()
      grid.View.Headers.Clear()
      Dim template As DataTemplate

      If TypeOf grid.View Is CardView Then
        ' When in card view, we select the correct template for the 
        ' ColumnManagerRow and GroupByControl instead of just adding them 
        ' directly to the FixedHeaders to have a more attractive and compact look
        If showGroupByControl Then
          template = TryCast(Me.FindResource("cardViewColumnManagerRowAndGroupByControl"), DataTemplate)
          grid.View.FixedHeaders.Add(template)
        End If

        ' Add the insertion row in the Headers if desired
        If showInsertionRow Then
          template = New DataTemplate()
          template.VisualTree = New FrameworkElementFactory(GetType(InsertionRow))
          grid.View.Headers.Add(template)
        End If
      Else
        ' When in table view, we simply add a ColumnManagerRow and GroupByControl
        ' in the FixedHeaders
        If showGroupByControl Then
          Dim groupByControl As FrameworkElementFactory = New FrameworkElementFactory(GetType(GroupByControl))

          ' In most case, it is desirable to prevent the GroupByControl to scroll horizontally.
          groupByControl.SetValue(TableView.CanScrollHorizontallyProperty, False)
          template = New DataTemplate()
          template.VisualTree = groupByControl
          grid.View.FixedHeaders.Add(template)
        End If

        template = New DataTemplate()
        template.VisualTree = New FrameworkElementFactory(GetType(ColumnManagerRow))
        grid.View.FixedHeaders.Add(template)

        ' Add the insertion row in the FixedHeaders if desired
        If showInsertionRow Then
          template = New DataTemplate()
          Dim insertionRowFactory As FrameworkElementFactory = New FrameworkElementFactory(GetType(InsertionRow))
          template.VisualTree = insertionRowFactory
          grid.View.FixedHeaders.Add(template)
        End If
      End If
    End Sub

    Private Function GetPeriodForeignKeyDescription() As DataGridForeignKeyDescription
      ' Create a ForeignKeyDescription so sorting will be based on the string value.
      ' Moreover, once this will be set on the corresponding ItemProperty, and  since AutoCreateForeignKeyConfiguration on DataGridControl is set to true,
      ' the corresponding ForeignKeyConfiguration will be generated, and as a result the default ForeignKey CellEditor (a ComboBox), GroupValueTemplate,
      ' ScrollTip.ContentTemplate and AutoFilterControl.DistinctValueItemTemplate will be correctly set.
      Dim foreignKeyDescription As DataGridForeignKeyDescription = New DataGridForeignKeyDescription()
      foreignKeyDescription.ItemsSource = System.Enum.GetValues(GetType(Period))
      foreignKeyDescription.ValuePath = "."
      foreignKeyDescription.ForeignKeyConverter = New PeriodForeignKeyConverter()

      Return foreignKeyDescription
    End Function

    Private Sub BindToStringArray()
      txtDescription.Text = Data.StringArrayText

      ' Reset the grid to its initial state.
      Me.ResetGrid(False, False, False)

      Dim stringArray As String() = System.Enum.GetNames(GetType(Key))

      ' Bind the grid to the string array.
      grid.ItemsSource = stringArray

      ' Set the grid to readonly, as the items are value type and the created column 
      ' will be bound to self (edition would be ineffective)
      grid.ReadOnly = True

      ' Change the Title of the column.
      grid.Columns(0).Title = "System.Windows.Input.Key names"
      grid.Columns(0).Width = 400
    End Sub

    Private Sub BindToJaggedArray()
      ' With this binding, only data modifications will work.
      txtDescription.Text = Data.JaggedArrayText
      Dim composers As String()() = New String(Data.Composers.Length - 1)() {}

      ' Fill the jagged array with composers
      Dim i As Integer = 0
      Do While i <> Data.Composers.Length
        composers(i) = New String(4) {}
        composers(i)(0) = Data.Composers(i).LastName
        composers(i)(1) = Data.Composers(i).FirstName
        composers(i)(2) = Data.Composers(i).Period.ToString()
        composers(i)(3) = Data.Composers(i).BirthYear.ToString()
        composers(i)(4) = Data.Composers(i).DeathYear.ToString()
        i += 1
      Loop

      ' Reset the grid to its initial state.
      Me.ResetGrid(False, True, False)

      ' Bind the grid to the jagged array.
      grid.ItemsSource = composers

      ' Change the titles of the columns.
      grid.Columns(0).Title = "Last Name"
      grid.Columns(1).Title = "First Name"
      grid.Columns(2).Title = "Period"
      grid.Columns(3).Title = "Birth Year"
      grid.Columns(4).Title = "Death Year"

      Dim periodColumn As Column
      periodColumn = grid.Columns(2)

      ' Configure comboBoxes.
      periodColumn.CellContentTemplate = TryCast(Me.FindResource("periodDataTemplate"), DataTemplate)

    End Sub

    Private Sub BindToXmlDocument()
      ' With this binding, adding, removing, and modifying data will work.

      txtDescription.Text = Data.XMLDocumentText
      Dim xmlDocument As XmlDocument = New XmlDocument()
      xmlDocument.Load(Application.GetResourceStream(New Uri("pack://application:,,,/Data/Composers.xml")).Stream)

      ' Reset the grid to its initial state.
      Me.ResetGrid(True, True, False)

      Dim dataGridCollectionView As DataGridCollectionView = New DataGridCollectionView(xmlDocument.SelectSingleNode("Composers"), Nothing, False, False)

      ' Create the item properties for the collection view
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("LastName", "LastName", Nothing, GetType(String)) With {.Title = "Last Name"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("FirstName", "FirstName", Nothing, GetType(String)) With {.Title = "First Name"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("Period", "Period", Nothing, GetType(Period)) With {.ForeignKeyDescription = Me.GetPeriodForeignKeyDescription()})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("BirthYear", "BirthYear", Nothing, GetType(Integer)) With {.Title = "Birth Year"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("DeathYear", "DeathYear", Nothing, GetType(Integer)) With {.Title = "Death Year"})

      ' Subscribe to various events to handle the adding and removing of nodes
      AddHandler dataGridCollectionView.CancelingNewItem, AddressOf dataGridCollectionView_CancelingNewItem
      AddHandler dataGridCollectionView.CommittingNewItem, AddressOf dataGridCollectionView_CommittingNewItem
      AddHandler dataGridCollectionView.CreatingNewItem, AddressOf dataGridCollectionView_CreatingNewItem
      AddHandler dataGridCollectionView.RemovingItem, AddressOf dataGridCollectionView_RemovingItem

      ' Bind the grid to the CollectionView.
      grid.ItemsSource = dataGridCollectionView
    End Sub

    Private Sub dataGridCollectionView_CreatingNewItem(ByVal sender As Object, ByVal e As DataGridCreatingNewItemEventArgs)
      ' Creating an empty node that will be commited to the source in the CommittingNewItem event
      Dim parentNode As XmlNode = DirectCast(e.CollectionView.SourceCollection, XmlNode)
      Dim xmlDocument As XmlDocument = parentNode.OwnerDocument

      Dim newElement As XmlElement = xmlDocument.CreateElement("Composer")
      newElement.AppendChild(xmlDocument.CreateElement("LastName"))
      newElement.AppendChild(xmlDocument.CreateElement("FirstName"))
      newElement.AppendChild(xmlDocument.CreateElement("Period"))
      newElement.AppendChild(xmlDocument.CreateElement("BirthYear"))
      newElement.AppendChild(xmlDocument.CreateElement("DeathYear"))

      e.NewItem = newElement

      ' We must "handle" this event when manualy handling insertion
      e.Handled = True
    End Sub

    Private Sub dataGridCollectionView_CommittingNewItem(ByVal sender As Object, ByVal e As DataGridCommittingNewItemEventArgs)
      ' Commit the new node that was created in the CreatingNewItem event
      Dim parentNode As XmlNode = DirectCast(e.CollectionView.SourceCollection, XmlNode)
      parentNode.AppendChild(DirectCast(e.Item, XmlNode))
      e.NewCount = parentNode.ChildNodes.Count
      e.Index = e.NewCount - 1

      ' We must "handle" this event when manualy handling insertion
      e.Handled = True
    End Sub

    Private Sub dataGridCollectionView_CancelingNewItem(ByVal sender As Object, ByVal e As DataGridItemHandledEventArgs)
      ' We must "handle" this event when manualy handling insertion even if nothing specific is done
      e.Handled = True
    End Sub

    Private Sub dataGridCollectionView_RemovingItem(ByVal sender As Object, ByVal e As DataGridRemovingItemEventArgs)
      ' Remove the node from the source
      Dim childNode As XmlNode = DirectCast(e.Item, XmlNode)

      If childNode Is Nothing Then
        Throw New InvalidOperationException()
      End If

      Dim parentNode As XmlNode = DirectCast(e.CollectionView.SourceCollection, XmlNode)
      parentNode.RemoveChild(childNode)

      ' We must "handle" this event when manualy handling the removal of items
      e.Handled = True
    End Sub

    Private Sub BindToXDocument()
      ' With this binding, adding, removing, and modifying data will not work.

      txtDescription.Text = Data.XDocumentText

      Dim xDocument As XDocument = xDocument.Load( _
        XmlReader.Create( _
          Application.GetResourceStream(New Uri("pack://application:,,,/Data/Composers.xml")).Stream))

      ' Reset the grid to its initial state.
      Me.ResetGrid(False, True, False)

      Dim items As IEnumerable = From item In xDocument.Element("Composers").Descendants("Composer") _
                                 Select New With _
                                  { _
                                    .LastName = item.Element("LastName").Value, _
                                    .FirstName = item.Element("FirstName").Value, _
                                    .Period = item.Element("Period").Value, _
                                    .BirthYear = item.Element("BirthYear").Value, _
                                    .DeathYear = item.Element("DeathYear").Value _
                                  }

      Dim dataGridCollectionView As DataGridCollectionView = New DataGridCollectionView(items, Nothing, False, False)

      ' Create the item properties for the collection view with correct type to have proper grouping and sorting
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("LastName", GetType(String), True) With {.Title = "Last Name"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("FirstName", GetType(String), True) With {.Title = "First Name"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("Period", GetType(Period), True) With {.ForeignKeyDescription = Me.GetPeriodForeignKeyDescription()})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("BirthYear", GetType(Integer), True) With {.Title = "Birth Year"})
      dataGridCollectionView.ItemProperties.Add(New DataGridItemProperty("DeathYear", GetType(Integer), True) With {.Title = "Death Year"})

      ' Bind the grid to the CollectionView.
      grid.ItemsSource = dataGridCollectionView
    End Sub

    Private Sub BindToCollectionBase()
      ' With this binding, only data modifications will work.
      txtDescription.Text = Data.CollectionBaseText
      Dim collection As ComposersCollection = New ComposersCollection()

      ' Fill the collection with composers
      For Each composer As Data.ComposerData In Data.Composers
        collection.Add(New Composer(composer.LastName, composer.FirstName, composer.Period, composer.BirthYear, composer.DeathYear))
      Next composer

      ' Reset the grid to its initial state.
      Me.ResetGrid(False, True, False)

      ' Bind the grid to the composer collection. 
      grid.ItemsSource = collection

      ' Change the titles of the columns.
      grid.Columns(0).Title = "Last Name"
      grid.Columns(1).Title = "First Name"
      grid.Columns(3).Title = "Birth Year"
      grid.Columns(4).Title = "Death Year"

      Dim periodColumn As Column
      periodColumn = grid.Columns("Period")

      ' Configure comboBoxes.
      periodColumn.CellContentTemplate = TryCast(Me.FindResource("periodDataTemplate"), DataTemplate)

      ' Create a ForeignKeyConfiguration in order to get the default
      ' ComboBox CellEditor and GroupValueTemplate, ScrollTip.ContentTemplate and
      ' AutoFilterControl.DistinctValueItemTemplate using the CellContentTemplate
      Dim fkConfiguration As ForeignKeyConfiguration = New ForeignKeyConfiguration()
      fkConfiguration.ItemsSource = System.Enum.GetValues(GetType(Period))
      fkConfiguration.ValuePath = "."
      periodColumn.ForeignKeyConfiguration = fkConfiguration
    End Sub

    Private Sub BindToBindingListComplete()
      ' With this binding, Adding, removing, and modifying data will work.

      txtDescription.Text = Data.CompleteBindingListText
      Dim collection As ComposersBindingListComplete = New ComposersBindingListComplete()

      ' Put the collection in batch initialization to prevent the ListChanged event from raising.
      collection.BeginInit()

      ' Fill the collection with composers
      For Each composer As Data.ComposerData In Data.Composers
        collection.Add(New ComposerEditable(composer.LastName, composer.FirstName, composer.Period, composer.BirthYear, composer.DeathYear))
      Next composer

      ' End the batch initialization of the collection.
      collection.EndInit()

      ' Reset the grid to its initial state.
      Me.ResetGrid(True, True, False)

      ' Bind the grid to the composer collection via DataGridCollectionView.
      Dim dataGridCollectionView As DataGridCollectionView = New DataGridCollectionView(collection)
      grid.ItemsSource = dataGridCollectionView

      Dim periodColumn As Column
      periodColumn = grid.Columns("Period")

      dataGridCollectionView.ItemProperties("Period").ForeignKeyDescription = Me.GetPeriodForeignKeyDescription()

      ' Change the titles of the columns.
      grid.Columns(0).Title = "Last Name"
      grid.Columns(1).Title = "First Name"
      grid.Columns(3).Title = "Birth Year"
      grid.Columns(4).Title = "Death Year"
    End Sub

    Private Sub BindToDataGridCollectionView()
      ' With this binding, Adding, removing, and modifying data will work.
      txtDescription.Text = Data.DataGridCollectionViewText

      ' Reset the grid to its initial state.
      Me.ResetGrid(True, True, False)

      ' Create an Xceed DataGridCollectionView using the Products DataTable as its source.
      Dim dataGridCollectionView As DataGridCollectionView = New DataGridCollectionView((CType(Application.Current, App)).Products.DefaultView, _
                                                                                          Nothing, _
                                                                                          True, _
                                                                                          True, _
                                                                                          True)

      ' Set ForeignKeys so these columns can be sorted based on display value rather than ID value.
      Dim foreignKeyDescription As DataTableForeignKeyDescription = New DataTableForeignKeyDescription() With { _
        .ItemsSource = DirectCast(Application.Current, App).Suppliers.DefaultView, _
        .ValuePath = "SupplierID", _
        .DisplayMemberPath = "CompanyName"}
      dataGridCollectionView.ItemProperties("SupplierID").ForeignKeyDescription = foreignKeyDescription

      foreignKeyDescription = New DataTableForeignKeyDescription() With { _
         .ItemsSource = DirectCast(Application.Current, App).Categories.DefaultView, _
        .ValuePath = "CategoryID", _
        .DisplayMemberPath = "CategoryName"}
      dataGridCollectionView.ItemProperties("CategoryID").ForeignKeyDescription = foreignKeyDescription

      ' Bind the grid to the Xceed DataGridCollectionView.
      grid.ItemsSource = dataGridCollectionView

      ' Hide unwanted columns.
      grid.Columns("ProductID").Visible = False
      grid.Columns("ProductID").ShowInColumnChooser = False
      grid.Columns("ReorderLevel").Visible = False
      grid.Columns("ReorderLevel").ShowInColumnChooser = False
      grid.Columns("UnitsInStock").Visible = False
      grid.Columns("UnitsInStock").ShowInColumnChooser = False
      grid.Columns("QuantityPerUnit").Visible = False
      grid.Columns("QuantityPerUnit").ShowInColumnChooser = False
      grid.Columns("Photo").Visible = False
      grid.Columns("Photo").ShowInColumnChooser = False

      ' Format visible columns
      grid.Columns("ProductName").Width = 200
      grid.Columns("ProductName").Title = "Product"
      grid.Columns("SupplierID").Title = "Supplier"
      grid.Columns("SupplierID").Width = 200
      grid.Columns("CategoryID").Title = "Category"
      grid.Columns("UnitPrice").CellContentTemplate = TryCast(Me.FindResource("currencyCellDataTemplate"), DataTemplate)
      grid.Columns("UnitPrice").Title = "Unit Price"
      grid.Columns("UnitsOnOrder").Title = "Units On Order"
      grid.Columns("ReorderDate").CellContentTemplate = TryCast(Me.FindResource("shortDateCellDataTemplate"), DataTemplate)
      grid.Columns("ReorderDate").Title = "Reorder Date"
    End Sub

    Private Sub BindToDataGridCollectionViewWithMasterDetail()

      ' With this binding, Adding, removing, and modifying data will work.
      txtDescription.Text = Data.DataGridCollectionViewWithMasterDetailText

      ' Reset the grid to its initial state.
      Me.ResetGrid(True, True, True)

      ' Create an Xceed DataGridCollectionView using the Orders DataTable as its source.
      Dim dataGridCollectionView As DataGridCollectionView = New DataGridCollectionView((CType(Application.Current, App)).Orders.DefaultView, _
                                                                                          Nothing, _
                                                                                          True, _
                                                                                          True, _
                                                                                          True)

      ' Set ForeignKeys so these columns can be sorted based on display value rather than ID value.
      Dim foreignKeyDescription As DataTableForeignKeyDescription = New DataTableForeignKeyDescription() With { _
        .ItemsSource = DirectCast(Application.Current, App).Customers.DefaultView, _
        .ValuePath = "CustomerID", _
        .ForeignKeyConverter = New CustomerForeignKeyConverter()}
      dataGridCollectionView.ItemProperties("CustomerID").ForeignKeyDescription = foreignKeyDescription

      foreignKeyDescription = New DataTableForeignKeyDescription() With { _
         .ItemsSource = DirectCast(Application.Current, App).Products.DefaultView, _
        .ValuePath = "ProductID", _
        .DisplayMemberPath = "ProductName"}
      dataGridCollectionView.DetailDescriptions(0).ItemProperties.Add(New DataGridItemProperty("ProductID", GetType(Integer)) With { _
                                                                      .ForeignKeyDescription = foreignKeyDescription})

      ' Automaticaly create the detail configurations.
      grid.AutoCreateDetailConfigurations = True

      ' Bind the grid to the Xceed DataGridCollectionView.
      grid.ItemsSource = dataGridCollectionView

      ' Hide unwanted columns.
      grid.Columns("OrderID").Visible = False
      grid.Columns("OrderID").ShowInColumnChooser = False
      grid.Columns("EmployeeID").Visible = False
      grid.Columns("EmployeeID").ShowInColumnChooser = False
      grid.Columns("ShipRegion").Visible = False
      grid.Columns("ShipRegion").ShowInColumnChooser = False
      grid.Columns("ShippedDate").Visible = False
      grid.Columns("ShippedDate").ShowInColumnChooser = False
      grid.Columns("RequiredDate").Visible = False
      grid.Columns("RequiredDate").ShowInColumnChooser = False
      grid.Columns("ShipVia").Visible = False
      grid.Columns("ShipVia").ShowInColumnChooser = False
      grid.Columns("ShipName").Visible = False
      grid.Columns("ShipName").ShowInColumnChooser = False

      ' Format visible columns
      grid.Columns("OrderDate").CellContentTemplate = TryCast(Me.FindResource("shortDateCellDataTemplate"), DataTemplate)
      grid.Columns("OrderDate").Title = "Order Date"
      grid.Columns("Freight").CellContentTemplate = TryCast(Me.FindResource("currencyCellDataTemplate"), DataTemplate)
      grid.Columns("ShipCountry").CellContentTemplate = TryCast(Me.FindResource("countryCellContentTemplate"), DataTemplate)
      grid.Columns("ShipCountry").GroupValueTemplate = TryCast(Me.FindResource("countryCellContentTemplate"), DataTemplate)
      grid.Columns("ShipCountry").VisiblePosition = 8
      grid.Columns("ShipCountry").Title = "Country"
      grid.Columns("ShipAddress").Title = "Address"
      grid.Columns("ShipCity").Title = "City"
      grid.Columns("ShipPostalCode").Title = "Postal Code"

      'Because the complex CellContentTemplate defined on the CustomerID column, a ForeignKeyValueConverter must be defined so the search feature will work correctly on this column.
      Dim customerForeignKeyConfig As ForeignKeyConfiguration = New ForeignKeyConfiguration() With {
        .ItemsSource = DirectCast(Application.Current, App).Customers.DefaultView,
        .ValuePath = "CustomerID",
        .ForeignKeyValueConverter = New CustomerConverter()}

      Dim customerIDColumn As Column = TryCast(grid.Columns("CustomerID"), Column)
      customerIDColumn.Title = "Customer"
      customerIDColumn.Width = 200
      customerIDColumn.VisiblePosition = 0
      customerIDColumn.CellContentTemplate = TryCast(Me.FindResource("customerDataTemplate"), DataTemplate)
      customerIDColumn.ForeignKeyConfiguration = customerForeignKeyConfig

      ' Configure detail grid
      Dim productsDetailConfiguration As DetailConfiguration = grid.DetailConfigurations(0)
      productsDetailConfiguration.Title = "Products"
      productsDetailConfiguration.AutoCreateForeignKeyConfigurations = True

      Dim column As Column = New Column()
      column.FieldName = "Discount"
      column.Visible = False
      column.ShowInColumnChooser = False
      productsDetailConfiguration.Columns.Add(column)

      column = New Column()
      column.FieldName = "OrderID"
      column.Visible = False
      column.ShowInColumnChooser = False
      productsDetailConfiguration.Columns.Add(column)

      column = New Column()
      column.FieldName = "ProductID"
      column.Title = "Product"
      column.Width = 200
      productsDetailConfiguration.Columns.Add(column)

      column = New Column()
      column.FieldName = "UnitPrice"
      column.Title = "Unit Price"
      column.CellContentTemplate = TryCast(Me.FindResource("currencyCellDataTemplate"), DataTemplate)
      productsDetailConfiguration.Columns.Add(column)
    End Sub

    Public m_doingInitializeComponent As Boolean ' = false
  End Class
End Namespace
