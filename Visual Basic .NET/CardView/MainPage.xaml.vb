'
'* Xceed DataGrid for WPF - SAMPLE CODE - Card View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [MainPage.xaml.vb]
'*
'* This class implements the various dynamic configuration options offered
'* by the configuration panel declared in MainPage.xaml.
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'*
'

Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace Xceed.Wpf.DataGrid.Samples.CardView
  Partial Public Class MainPage
    Inherits Page
    Implements IWeakEventListener
    Public Sub New()
      PropertyChangedEventManager.AddListener(ConfigurationData.Singleton, Me, String.Empty)

      Me.InitializeComponent()
      Me.AdjustHeadersFooters()

      AddHandler Me.Loaded, AddressOf MainPage_Loaded
      AddHandler Me.Unloaded, AddressOf MainPage_Unloaded
    End Sub

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      PropertyChangedEventManager.RemoveListener(ConfigurationData.Singleton, Me, String.Empty)
      PropertyChangedEventManager.AddListener(ConfigurationData.Singleton, Me, String.Empty)

      AddHandler (CType(Application.Current, App)).ViewChanged, AddressOf OnViewChanged
    End Sub

    Private Sub MainPage_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
      PropertyChangedEventManager.RemoveListener(ConfigurationData.Singleton, Me, String.Empty)
      RemoveHandler (CType(Application.Current, App)).ViewChanged, AddressOf OnViewChanged
    End Sub

    Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      Select Case e.PropertyName
        Case "FilteringMethod", "ShowGridHeaders", "ShowInsertionRow"
          Me.AdjustHeadersFooters()
      End Select
    End Sub

    Private Sub AdjustHeadersFooters()
      ' Show / hide the element base on the current configuration.

      grid.View.FixedHeaders.Clear()
      grid.View.Headers.Clear()
      Dim template As DataTemplate

      If ConfigurationData.Singleton.ShowGridHeaders Then
        template = TryCast(Me.FindResource("cardViewColumnManagerRowAndGroupByControl"), DataTemplate)
        grid.View.FixedHeaders.Add(template)
      End If

      Dim collectionView As DataGridCollectionView = TryCast(Me.grid.ItemsSource, DataGridCollectionView)

      If collectionView IsNot Nothing Then
        ' Add the filter row in the FixedHeaders if desired
        If ConfigurationData.Singleton.FilteringMethod = FilteringMethod.FilterRow Then
          grid.View.Headers.Add(TryCast(Me.FindResource("filterRowTemplate"), DataTemplate))
          collectionView.FilterCriteriaMode = FilterCriteriaMode.And
        Else
          collectionView.FilterCriteriaMode = FilterCriteriaMode.None
        End If

        ' Simply activate or deactive auto filtering. The actual drop-down presence in 
        ' ColumnManagerCell is handled in XAML.
        If ConfigurationData.Singleton.FilteringMethod = FilteringMethod.AutoFilter Then
          collectionView.AutoFilterMode = AutoFilterMode.And
        Else
          collectionView.AutoFilterMode = AutoFilterMode.None
        End If
      End If

      ' Add the insertion row in the Headers if desired
      If ConfigurationData.Singleton.ShowInsertionRow Then
        template = TryCast(Me.FindResource("cardViewInsertionRow"), DataTemplate)
        grid.View.Headers.Add(template)
      End If
    End Sub

    Private Sub OnViewChanged(ByVal sender As Object, ByVal e As EventArgs)
      Me.AdjustHeadersFooters()
    End Sub

    Private Function ReceiveWeakEvent(ByVal managerType As Type, ByVal sender As Object, ByVal e As EventArgs) As Boolean Implements IWeakEventListener.ReceiveWeakEvent
      If managerType = GetType(PropertyChangedEventManager) Then
        If sender Is ConfigurationData.Singleton Then
          Me.ConfigurationData_PropertyChanged(sender, e)
        End If

        Return True
      End If

      Return False
    End Function
  End Class
End Namespace
