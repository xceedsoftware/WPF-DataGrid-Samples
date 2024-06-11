'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [MainPageViewModel.vb]
' *  
' * This class exposes the model to the view, and provides business logic interaction.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 


Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Input
Imports Xceed.Wpf.DataGrid.FilterCriteria
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Model
Imports Xceed.Wpf.DataGrid.Samples.MVVM.Repository

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
  Public Class MainPageViewModel
    Implements INotifyPropertyChanged
    Implements IWeakEventListener
    Public Sub New()
      m_supplierRepository = New SupplierRepository()
      Me.Suppliers = m_supplierRepository.GetAllSuppliers()

      m_categoryRepository = New CategoryRepository()
      Me.Categories = m_categoryRepository.GetAllCategories()

      m_productRepository = New ProductRepository()
      Me.Products = m_productRepository.GetAllProducts()

      'OrderDetails will be created as Products are loaded from the ProductRepository.

      Me.RegisterNotifications()
      Me.PrepareNextProduct()

      Me.FilterValue = m_filterValues(0)
      Me.ProductNameDescription = "This column represents the name of the product"
      Me.OrderDetailsDescription = "These represent orders that were fufilled for the corresponding product"
      Me.ProductsTitle = "Products"
      Me.OrderDetailsTitle = "Product Orders"
    End Sub

#Region "ProductsCollectionView Property"

    Public ReadOnly Property ProductsCollectionView() As ICollectionView
      Get
        If m_productsCollectionView IsNot Nothing Then
          Return m_productsCollectionView
        End If

        'Load the CollectionView to which the DataGrid is bound from the ProductsCollectionViewResources.xaml file.
        Return InlineAssignHelper(m_productsCollectionView, TryCast(TryCast(Application.Current.FindResource("productsCollectionViewSource"), CollectionViewSource).View, ICollectionView))
      End Get
    End Property

    Private m_productsCollectionView As ICollectionView

#End Region

#Region "Products Property"

    Public Property Products() As ObservableCollection(Of Product)
      Get
        Return m_Products
      End Get
      Private Set
        m_Products = Value
      End Set
    End Property
    Private m_Products As ObservableCollection(Of Product)

#End Region

#Region "Suppliers Property"

    Public Property Suppliers() As ObservableCollection(Of Supplier)
      Get
        Return m_Suppliers
      End Get
      Private Set
        m_Suppliers = Value
      End Set
    End Property
    Private m_Suppliers As ObservableCollection(Of Supplier)

#End Region

#Region "Categories Property"

    Public Property Categories() As ObservableCollection(Of Category)
      Get
        Return m_Categories
      End Get
      Private Set
        m_Categories = Value
      End Set
    End Property
    Private m_Categories As ObservableCollection(Of Category)

#End Region

#Region "FilterValues Property"

    Public ReadOnly Property FilterValues() As String()
      Get
        Return m_filterValues
      End Get
    End Property

    Private m_filterValues As String() = New String() {"All", "Maintained", "Discontinued"}

#End Region

#Region "FilterValue Property"

    Public Property FilterValue() As String
      Get
        Return m_filterValue
      End Get
      Set
        If Value <> m_filterValue Then
          m_filterValue = Value
          Me.OnPropertyChanged("FilterValue")
        End If
      End Set
    End Property

    Private m_filterValue As String

#End Region

#Region "MasterSelectedItems Property"

    'Contains items currently selected in the grid at the master level.
    Public ReadOnly Property MasterSelectedItems() As ObservableCollection(Of Product)
      Get
        Return m_masterSelectedItems
      End Get
    End Property

    Private m_masterSelectedItems As New ObservableCollection(Of Product)()

#End Region

#Region "SelectedItemsCount Property"

    'Provides feedback when selection is changing
    Public Property SelectedItemsCount() As Integer
      Get
        Return m_selectedItemsCount
      End Get
      Set
        If Value <> m_selectedItemsCount Then
          m_selectedItemsCount = Value
          Me.OnPropertyChanged("SelectedItemsCount")
        End If
      End Set
    End Property

    Private m_selectedItemsCount As Integer

#End Region

#Region "ProductNameDescription Property"

    Public Property ProductNameDescription() As String
      Get
        Return m_productNameDescription
      End Get
      Private Set
        If Value <> m_productNameDescription Then
          m_productNameDescription = Value
          Me.OnPropertyChanged("ProductNameDescription")
        End If
      End Set
    End Property

    Private m_productNameDescription As String

#End Region

#Region "OrderDetailsDescription Property"

    Public Property OrderDetailsDescription() As String
      Get
        Return m_orderDetailsDescription
      End Get
      Private Set
        If Value <> m_orderDetailsDescription Then
          m_orderDetailsDescription = Value
          Me.OnPropertyChanged("OrderDetailsDescription")
        End If
      End Set
    End Property

    Private m_orderDetailsDescription As String

#End Region

#Region "ProductsTitle Property"

    Public Property ProductsTitle() As String
      Get
        Return m_productsTitle
      End Get
      Set
        If Value <> m_productsTitle Then
          m_productsTitle = Value
          Me.OnPropertyChanged("ProductsTitle")
        End If
      End Set
    End Property

    Private m_productsTitle As String

#End Region

#Region "OrderDetailsTitle Property"

    Public Property OrderDetailsTitle() As String
      Get
        Return m_orderDetailsTitle
      End Get
      Private Set
        If Value <> m_orderDetailsTitle Then
          m_orderDetailsTitle = Value
          Me.OnPropertyChanged("OrderDetailsTitle")
        End If
      End Set
    End Property

    Private m_orderDetailsTitle As String

#End Region

#Region "CurrentItem Property"

    Public Property CurrentItem() As Object
      Get
        Return m_currentItem
      End Get
      Set
        If Value IsNot m_currentItem Then
          m_currentItem = Value
          Me.OnPropertyChanged("CurrentItem")
        End If
      End Set
    End Property

    Private m_currentItem As Object

#End Region

#Region "CurrentColumnFieldName Property"

    Public Property CurrentColumnFieldName() As String
      Get
        Return m_currentColumnFieldName
      End Get
      Set
        If Value <> m_currentColumnFieldName Then
          m_currentColumnFieldName = Value
          Me.OnPropertyChanged("CurrentColumnFieldName")
        End If
      End Set
    End Property

    Private m_currentColumnFieldName As String

#End Region

#Region "InitialProductIDValue Property"

    'Used to initialize the ProductID value of the insertion row.
    Public Property InitialProductIDValue() As Integer
      Get
        Return m_initialProductIDValue
      End Get
      Set
        If Value <> m_initialProductIDValue Then
          m_initialProductIDValue = Value
          Me.OnPropertyChanged("InitialProductIDValue")
        End If
      End Set
    End Property

    Private m_initialProductIDValue As Integer

#End Region

    Public Sub ProvideSelectedList(sender As Object, e As EventArgs)
      'This method is hooked to the DetailsExpanding event in the grid, and allows to provides a list of selected items for every expanded detail grid.
      Dim eventArgs = TryCast(e, DetailsExpansionChangingEventArgs)
      Dim masterItem = eventArgs.MasterItem
      Dim selectedItemsSource = Nothing

      'If the detail grid has been expanded in the pass, a SelectedItemsSource aready exist, so get and use it.
      If m_collpasedDetailsSelectedItems.TryGetValue(masterItem, selectedItemsSource) Then
        'But remove it first from the collapsed collection.
        m_collpasedDetailsSelectedItems.Remove(masterItem)
      End If

      'If none was return, create one.
      If selectedItemsSource Is Nothing Then
        selectedItemsSource = New ObservableCollection(Of OrderDetail)()
      End If

      'Add listener so the ViewModel can react when SelectedIems change.
      CollectionChangedEventManager.AddListener(selectedItemsSource, Me)

      'Keep a reference to it, and provide it to the DataGrid.
      m_currentDetailsSelectedItems.Add(masterItem, selectedItemsSource)
      eventArgs.SelectedItemsSources.Add("OrderDetails", selectedItemsSource)

      'And update the count that is displayed in the View.
      Me.SelectedItemsCount += selectedItemsSource.Count
    End Sub

    Public Sub PreserveSelectedList(sender As Object, e As EventArgs)
      'This method is hooked to the DetailsCollapsing event in the grid, and allows to keep a reference to selected items in a collapsing detail grid,
      'so it can be provided back when the detail is expanded again.
      Dim eventArgs = TryCast(e, DetailsExpansionChangingEventArgs)
      Dim masterItem = eventArgs.MasterItem
      Dim selectedItemsSource = Nothing

      'There should always be a corresponding SelectedItemsSource
      If m_currentDetailsSelectedItems.TryGetValue(masterItem, selectedItemsSource) Then
        'Remove the listener that was added in the ProvideSelectedList method.
        CollectionChangedEventManager.RemoveListener(selectedItemsSource, Me)

        'Remove it from the dictionary, as this list will get cleared by the grid after the detail grid is collapsed.
        m_currentDetailsSelectedItems.Remove(masterItem)

        'Create a new list, to which a reference is kept, and in which items still present in the SelectedItemsSource are copied to.
        Dim selectedItemsToKeep = New ObservableCollection(Of OrderDetail)(TryCast(selectedItemsSource, ObservableCollection(Of OrderDetail)))
        m_collpasedDetailsSelectedItems.Add(masterItem, selectedItemsToKeep)

        'And update the count that is displayed in the View (selected items of a collapsed detail grid are not part of SelectedItems in the DataGrid any longer).
        Me.SelectedItemsCount -= selectedItemsToKeep.Count
      End If
    End Sub

    Private Sub RegisterNotifications()
      'The ViewModel needs to be notified when lists or items to which the DataGrid is bound change.
      CollectionChangedEventManager.AddListener(Me.Products, Me)
      CollectionChangedEventManager.AddListener(Me.MasterSelectedItems, Me)

      For Each product As Product In Me.Products
        CollectionChangedEventManager.AddListener(product.OrderDetails, Me)
        PropertyChangedEventManager.AddListener(product, Me, "")

        For Each orderDetail As OrderDetail In product.OrderDetails
          PropertyChangedEventManager.AddListener(orderDetail, Me, "")
        Next
      Next
    End Sub

    Private Sub OnProductsCollectionChanged(e As NotifyCollectionChangedEventArgs)
      Select Case e.Action
        Case NotifyCollectionChangedAction.Add
          If True Then
            'If the item is coming from the DB, no need to add it, it's already present.
            If m_isAddingFromDB Then
              Exit Select
            End If

            'For an item provided through the InsertionRow, add it to the DB.
            For Each item As Product In e.NewItems
              m_productRepository.AddNewProduct(item)
            Next

            'Since this part of the code only runs when an item has been added by the end user (utilizing the InsertionRow),
            'get the next product right away so the next InitialProductIDValue value is valid when using the InsertionRow again.
            Me.PrepareNextProduct()
            Exit Select
          End If

        Case NotifyCollectionChangedAction.Remove
          If True Then
            For Each item As Product In e.OldItems
              'Remove the item that was deleted by the end user.
              m_productRepository.RemoveProduct(item)
            Next
            Exit Select
          End If
        Case Else

          If True Then
            Exit Select
          End If
      End Select
    End Sub

    Private Sub OnOrderDetailsCollectionChanged(e As NotifyCollectionChangedEventArgs)
      If e.Action = NotifyCollectionChangedAction.Remove Then
        For Each item As OrderDetail In e.OldItems
          'Remove the item that was deleted by the end user.
          OrderDetailRepository.Singleton.RemoveOrderDetail(item)
        Next
      End If
    End Sub

    Private Sub OnProductPropertyChanged(item As Product)
      If m_modifiedProducts.Contains(item) Then
        Return
      End If

      'Keep a reference to items that are modified by the end user.
      m_modifiedProducts.Add(item)

      'Notify the View that the item is modifed, so a visual cue is displayed.
      item.IsModified = True
    End Sub

    Private Sub OnOrderDetailPropertyChanged(item As OrderDetail)
      If m_modifiedOrderDetails.Contains(item) Then
        Return
      End If

      'Keep a reference to items that are modified by the end user.
      m_modifiedOrderDetails.Add(item)

      'Notify the View that the item is modifed, so a visual cue is displayed.
      item.IsModified = True
    End Sub

    Private Sub OnSelectedItemsChanged()
      Dim count = 0

      count = Me.MasterSelectedItems.Count

      'Only count selected items of expanded detail grids.
      For Each list As ObservableCollection(Of OrderDetail) In m_currentDetailsSelectedItems.Values
        count += list.Count
      Next

      Me.SelectedItemsCount = count
    End Sub

    Private Sub PrepareNextProduct()
      'This will serve either when simulating a new item coming from the DB, or when an item is added by the end user through the InsertionRow.
      m_newProduct = m_productRepository.GetNewProduct(Me.Products.Count + 1)

      Me.InitialProductIDValue = m_newProduct.ProductID
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
      target = value
      Return value
    End Function

#Region "CommitModifiedItemsCommand Comand"

    Public ReadOnly Property CommitModifiedItemsCommand() As ICommand
      Get
        If m_commitModifiedItemsCommand IsNot Nothing Then
          Return m_commitModifiedItemsCommand
        End If

        Return InlineAssignHelper(m_commitModifiedItemsCommand, New RelayCommand(AddressOf CommitModifiedItems))
      End Get
    End Property

    Private m_commitModifiedItemsCommand As ICommand

    Public Sub CommitModifiedItems()
      For Each item As OrderDetail In m_modifiedOrderDetails
        OrderDetailRepository.Singleton.UpdateOrderDetail(item)

        'Notify the View that the item is no longer modified, so a visual cue is removed.
        item.IsModified = False
      Next
      m_modifiedOrderDetails.Clear()

      For Each item As Product In m_modifiedProducts
        m_productRepository.UpdateProduct(item)

        'Notify the View that the item is no longer modified, so a visual cue is removed.
        item.IsModified = False
      Next
      m_modifiedProducts.Clear()
    End Sub

#End Region

#Region "GetNewItemCommand Comand"

    Public ReadOnly Property GetNewItemCommand() As ICommand
      Get
        If m_getNewItemCommand IsNot Nothing Then
          Return m_getNewItemCommand
        End If

        Return InlineAssignHelper(m_getNewItemCommand, New RelayCommand(AddressOf GetNewItem))
      End Get
    End Property

    Private m_getNewItemCommand As ICommand

    Public Sub GetNewItem()
      'This simulates a new item received from the DB.  The m_newProduct member already contains a new Product.
      m_isAddingFromDB = True

      'First add it to Products, so it gets added to the CollectionView, and as a result, to the DataGrid.
      Me.Products.Add(m_newProduct)

      'Then set current item and column, so the grid will bring the new item into view.
      Me.CurrentItem = m_newProduct
      Me.CurrentColumnFieldName = "ProductName"

      'Prepare the next product right away.  This is to accomodate an insertion by the end user in the View (via the InsertionRow).
      Me.PrepareNextProduct()

      m_isAddingFromDB = False
    End Sub

#End Region

#Region "ApplyFilterCommand Comand"

    Public ReadOnly Property ApplyFilterCommand() As ICommand
      Get
        If m_applyFilterCommand IsNot Nothing Then
          Return m_applyFilterCommand
        End If

        Return InlineAssignHelper(m_applyFilterCommand, New RelayCommand(AddressOf ApplyFilter))
      End Get
    End Property

    Private m_applyFilterCommand As ICommand

    Public Sub ApplyFilter()
      'Build a filter criterion that corresponds to the value selected by the end user in the ComboBox in the View.
      Dim filterCriterion = Nothing

      Select Case Me.FilterValue
        Case "Maintained"
          If True Then
            filterCriterion = New EqualToFilterCriterion(False)
            Exit Select
          End If

        Case "Discontinued"
          If True Then
            filterCriterion = New EqualToFilterCriterion(True)
            Exit Select
          End If
        Case Else

          If True Then
            filterCriterion = Nothing
            Exit Select
          End If
      End Select

      'Apply the filter criterion to the grid, via its collection view.
      Dim dataGridCollectionView = TryCast(Me.ProductsCollectionView, DataGridCollectionView)
      dataGridCollectionView.ItemProperties("Discontinued").FilterCriterion = filterCriterion
    End Sub

#End Region

#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
      Dim handler = Me.PropertyChangedEvent
      If handler Is Nothing Then
        Return
      End If

      handler.Invoke(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#End Region

#Region "IWeakEventListener Members"

    Private Function IWeakEventListener_ReceiveWeakEvent(managerType As Type, sender As Object, e As EventArgs) As Boolean Implements IWeakEventListener.ReceiveWeakEvent
      If managerType = GetType(CollectionChangedEventManager) Then
        Dim eventArgs = TryCast(e, NotifyCollectionChangedEventArgs)
        If sender Is Me.MasterSelectedItems Then
          Me.OnSelectedItemsChanged()
        ElseIf sender Is Me.Products Then
          Me.OnProductsCollectionChanged(eventArgs)
        Else
          'Verify if the CollectionChanged event is coming from a detail grid's SelectedItemsSource list, or from a Product's OrderDetails list.
          Dim isDetailSelectedItemsSource = False
          For Each list As ObservableCollection(Of OrderDetail) In m_currentDetailsSelectedItems.Values
            If sender Is list Then
              isDetailSelectedItemsSource = True
              Exit For
            End If
          Next

          If isDetailSelectedItemsSource Then
            Me.OnSelectedItemsChanged()
          Else
            Me.OnOrderDetailsCollectionChanged(eventArgs)
          End If
        End If
        Return True
      End If

      If managerType = GetType(PropertyChangedEventManager) Then
        'If this notification is coming from the ViewModel modifying the item, there is no need to process it any further.
        Dim eventArgs = TryCast(e, PropertyChangedEventArgs)
        If eventArgs.PropertyName = "IsModified" Then
          Return True
        End If

        Dim item = TryCast(sender, Product)
        If item IsNot Nothing Then
          Me.OnProductPropertyChanged(item)
        Else
          Me.OnOrderDetailPropertyChanged(TryCast(sender, OrderDetail))
        End If
        Return True
      End If

      Return False
    End Function

#End Region

    Private m_newProduct As Product
    Private m_isAddingFromDB As Boolean
    Private m_productRepository As ProductRepository
    Private m_supplierRepository As SupplierRepository
    Private m_categoryRepository As CategoryRepository
    Private m_modifiedProducts As New HashSet(Of Product)()
    Private m_modifiedOrderDetails As New HashSet(Of OrderDetail)()
    Private m_currentDetailsSelectedItems As New Dictionary(Of Object, ObservableCollection(Of OrderDetail))()
    Private m_collpasedDetailsSelectedItems As New Dictionary(Of Object, ObservableCollection(Of OrderDetail))()
  End Class
End Namespace

