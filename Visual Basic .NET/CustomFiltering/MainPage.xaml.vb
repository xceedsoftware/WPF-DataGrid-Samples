'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Filtering Sample Application
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
'

Imports System
Imports System.Collections
Imports System.Data
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.CustomFiltering
  Partial Public Class MainPage
    Inherits Page
#Region "CONSTRUCTOR"

    Public Sub New()
      Me.InitializeComponent()
    End Sub

#End Region

#Region "ContextMenu creation and event handlers"

    Private Sub DataRow_ContextMenuOpening(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim obj As DependencyObject = TryCast(e.OriginalSource, DependencyObject)

      Do While Not obj Is Nothing
        If TypeOf obj Is DataRow Then
          Dim contextMenu As ContextMenu = grid.ContextMenu

          contextMenu.DataContext = (CType(obj, DataRow)).DataContext

          contextMenu.Items.Clear()

          ' Bind the MenuItem.Header to the DataRow.DataContext for ShipCountry field
          Dim shipCountryFilterMenuItem As MenuItem = New MenuItem()
          shipCountryFilterMenuItem.Tag = "ShipCountry"

          Dim shipCountryBinding As Binding = New Binding("[ShipCountry]")
          shipCountryFilterMenuItem.SetBinding(MenuItem.HeaderProperty, shipCountryBinding)

          AddHandler shipCountryFilterMenuItem.Click, AddressOf OnShipContryMenuItemSelected

          contextMenu.Items.Add(shipCountryFilterMenuItem)


          ' Bind the MenuItem.Header to the DataRow.DataContext for ShipCity field
          Dim shipCityFilterMenuItem As MenuItem = New MenuItem()
          shipCityFilterMenuItem.Tag = "ShipCity"

          Dim shipCityBinding As Binding = New Binding("[ShipCity]")
          shipCityFilterMenuItem.SetBinding(MenuItem.HeaderProperty, shipCityBinding)

          AddHandler shipCityFilterMenuItem.Click, AddressOf OnShipCityMenuItemSelected

          contextMenu.Items.Add(shipCityFilterMenuItem)

          ' Bind the MenuItem.Header to the DataRow.DataContext for OrderDate field
          Dim orderDateFilterMenuItem As MenuItem = New MenuItem()
          orderDateFilterMenuItem.Tag = "OrderDate"

          Dim orderDateBinding As Binding = New Binding("[OrderDate].Month")
          orderDateBinding.Converter = TryCast(Me.FindResource("monthNameConverter"), IValueConverter)
          orderDateFilterMenuItem.SetBinding(MenuItem.HeaderProperty, orderDateBinding)

          AddHandler orderDateFilterMenuItem.Click, AddressOf OnOrderMonthMenuItemSelected

          contextMenu.Items.Add(orderDateFilterMenuItem)

          contextMenu.Items.Add(New Separator())

          Dim clearFiltersMenuItem As MenuItem = New MenuItem()
          clearFiltersMenuItem.Header = "Clear All Filters"
          AddHandler clearFiltersMenuItem.Click, AddressOf OnClearAllFiltersMenuItemSelected

          contextMenu.Items.Add(clearFiltersMenuItem)

          Return
        End If

        obj = VisualTreeHelper.GetParent(obj)
      Loop

      ' The context menu was not triggered over a DataRow. Do nothing.
      e.Handled = True
    End Sub

    Private Sub OnShipContryMenuItemSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim item As MenuItem = TryCast(sender, MenuItem)

      If Not item.Header Is Nothing Then
        m_countryFilterCombo.SelectedItem = item.Header.ToString()
      End If
    End Sub

    Private Sub OnShipCityMenuItemSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim item As MenuItem = TryCast(sender, MenuItem)

      If Not item.Header Is Nothing Then
        m_cityFilterCombo.SelectedItem = item.Header.ToString()
      End If
    End Sub

    Private Sub OnOrderMonthMenuItemSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim item As MenuItem = TryCast(sender, MenuItem)

      If Not item.Header Is Nothing Then
        Me.UpdateOrderMonthFilterComboBox(item.Header.ToString())
      End If
    End Sub

    Private Sub OnClearAllFiltersMenuItemSelected(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.UpdateOrderMonthFilterComboBox(String.Empty)

      Dim dataGridContext As DataGridContext = DataGridControl.GetDataGridContext(Me.grid)

      If dataGridContext Is Nothing Then
        Throw New InvalidOperationException("The grid does not have a DataGridContext.")
      End If

      If dataGridContext.Items Is Nothing Then
        Return
      End If

      If dataGridContext.Items.SourceCollection Is Nothing Then
        Return
      End If

      'In the master level, the Items is an ItemCollection that wraps a DataGridCollectionView
      Dim dataGridCollectionView As DataGridCollectionView = TryCast(dataGridContext.Items.SourceCollection, DataGridCollectionView)

      If dataGridCollectionView Is Nothing Then
        Throw New InvalidOperationException("Automatic filtering is only supported if the data source is a DataGridCollectionView.")
      End If

      Using dataGridCollectionView.DeferRefresh()
        Dim autoFilterValuesCount As Integer = dataGridCollectionView.AutoFilterValues.Keys.Count

        ' Reset all AutoFilterValues lists
        For Each fieldName As String In dataGridCollectionView.AutoFilterValues.Keys
          Dim autoFilterValues As IList = TryCast(dataGridCollectionView.AutoFilterValues(fieldName), IList)

          If Not autoFilterValues Is Nothing Then
            autoFilterValues.Clear()
          End If
        Next fieldName

        m_cityFilterCombo.SelectedIndex = -1
        m_countryFilterCombo.SelectedIndex = -1
        m_countryFilterCombo.IsEnabled = True
      End Using
    End Sub

#End Region

#Region "ShipCountry ComboBox event handlers"

    Private Sub OnShipCountryComboBoxInitialized(ByVal sender As Object, ByVal e As EventArgs)
      m_countryFilterCombo = TryCast(sender, ComboBox)
    End Sub

    Private Sub OnClearShipCountryButtonInitialized(ByVal sender As Object, ByVal e As EventArgs)
      m_clearCountryButton = TryCast(sender, Button)
    End Sub

#End Region

#Region "Ship City Combobox and Clear Button event handlers"

    Private Sub OnShipCityComboBoxInitialized(ByVal sender As Object, ByVal e As EventArgs)
      m_cityFilterCombo = TryCast(sender, ComboBox)
    End Sub

    Private Sub OnShipCitySelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      ' We don't want to allow the Country to be modified
      ' if a City is set
      Dim isCountryEnabled As Boolean = True

      If Not m_cityFilterCombo.SelectedItem Is Nothing Then
        isCountryEnabled = False
      End If

      m_countryFilterCombo.IsEnabled = isCountryEnabled
      m_clearCountryButton.IsEnabled = isCountryEnabled
    End Sub

#End Region

#Region "Order Month ComboBox and Clear Button event handlers"

    Private Sub OnOrderMonthComboBoxInitialized(ByVal sender As Object, ByVal e As EventArgs)
      m_orderMonthFilterCombo = TryCast(sender, ComboBox)
    End Sub

    Private Sub OnOrderMonthComboBoxSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
      Dim newValue As String = String.Empty

      If (e.AddedItems.Count > 0) AndAlso (Not e.AddedItems(0) Is Nothing) Then
        newValue = e.AddedItems(0).ToString()
      End If

      Me.UpdateOrderMonthFilterComboBox(newValue)
    End Sub

    Private Sub OnClearMonthButtonInitialized(ByVal sender As Object, ByVal e As EventArgs)
      m_clearOrderMonthButton = TryCast(sender, Button)
    End Sub

    Private Sub OnClearOrderMonthButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Me.UpdateOrderMonthFilterComboBox(String.Empty)
    End Sub

    Private Sub UpdateOrderMonthFilterComboBox(ByVal month As String)
      m_orderMonth = month
      Dim collectionView As DataGridCollectionView = TryCast(Me.grid.ItemsSource, DataGridCollectionView)

      If Not collectionView Is Nothing Then
        collectionView.Refresh()
      End If

      Dim isClearMonthButtonEnabled As Boolean = True

      If String.IsNullOrEmpty(month) Then
        isClearMonthButtonEnabled = False
      End If

      m_clearOrderMonthButton.IsEnabled = isClearMonthButtonEnabled
      m_orderMonthFilterCombo.SelectedValue = m_orderMonth
    End Sub

#End Region

#Region "OrderDate Month filtering using DataGridCollectionView filter predicate delegate"

    Private Sub OnDataGridCollectionViewSourceFilter(ByVal sender As Object, ByVal e As FilterEventArgs)
      If m_orderMonth <> String.Empty Then
        Dim dataRowView As DataRowView = TryCast(e.Item, DataRowView)

        If Not dataRowView Is Nothing Then
          Try
            Dim orderDate As DateTime = CDate(dataRowView("OrderDate"))

            If DateTimeFormatInfo.InvariantInfo.GetMonthName(orderDate.Month) = m_orderMonth Then
              e.Accepted = True
              Return
            End If
          Catch e1 As Exception
            ' Exception
          End Try
        End If

        e.Accepted = False
      End If
    End Sub

#End Region '.

#Region "ShipName CustomDistinctValues Calculation"

    ' Called when the DistinctValues for the field ShipName are calculated
    Private Sub ShipNameQueryDistinctValue(ByVal sender As Object, ByVal e As QueryDistinctValueEventArgs)
      If String.IsNullOrEmpty(TryCast(e.DataSourceValue, String)) Then
        e.DistinctValue = "(Other)"
      Else
        Dim name As String = TryCast(e.DataSourceValue, String)

        Dim firstChar As Char = name.ToLower()(0)

        If (firstChar >= "0"c) AndAlso (firstChar <= "9"c) Then
          e.DistinctValue = "0 - 9"
        ElseIf (firstChar >= "a"c) AndAlso (firstChar <= "c"c) Then
          e.DistinctValue = "A - C"
        ElseIf (firstChar >= "d"c) AndAlso (firstChar <= "f"c) Then
          e.DistinctValue = "D - F"
        ElseIf (firstChar >= "g"c) AndAlso (firstChar <= "i"c) Then
          e.DistinctValue = "G - I"
        ElseIf (firstChar >= "j"c) AndAlso (firstChar <= "l"c) Then
          e.DistinctValue = "J - L"
        ElseIf (firstChar >= "m"c) AndAlso (firstChar <= "o"c) Then
          e.DistinctValue = "M - O"
        ElseIf (firstChar >= "p"c) AndAlso (firstChar <= "r"c) Then
          e.DistinctValue = "P - R"
        ElseIf (firstChar >= "s"c) AndAlso (firstChar <= "u"c) Then
          e.DistinctValue = "S - U"
        ElseIf (firstChar >= "v"c) AndAlso (firstChar <= "z"c) Then
          e.DistinctValue = "V - Z"
        Else
          e.DistinctValue = "(Other)"
        End If
      End If
    End Sub

#End Region

#Region "ShippedDate CustomDistinctValues Calculation"

    ' Called when the DistinctValues for the field ShippedDate are calculated
    Private Sub OnShippedDateQueryDistinctValue(ByVal sender As Object, ByVal e As QueryDistinctValueEventArgs)
      If TypeOf e.DataSourceValue Is DateTime Then
        Dim dateTime As DateTime = CDate(e.DataSourceValue)

        If Not dateTime = Nothing Then
          e.DistinctValue = DateTimeFormatInfo.InvariantInfo.GetMonthName(dateTime.Month)
        End If
      End If
    End Sub

#End Region

#Region "PRIVATE FIELDS"

    Private m_orderMonth As String = String.Empty
    Private m_cityFilterCombo As ComboBox ' = null;
    Private m_countryFilterCombo As ComboBox ' = null;
    Private m_clearCountryButton As Button ' = null;
    Private m_orderMonthFilterCombo As ComboBox ' = null;
    Private m_clearOrderMonthButton As Button ' = null;

#End Region
  End Class
End Namespace
