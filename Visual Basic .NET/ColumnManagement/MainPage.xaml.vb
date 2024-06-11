'
' Xceed DataGrid for WPF - SAMPLE CODE - Column Management Sample Application
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
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls

Imports Xceed.Wpf.DataGrid.Converters
Imports Xceed.Wpf.DataGrid.Views

Namespace Xceed.Wpf.DataGrid.Samples.ColumnManagement
  Partial Public Class MainPage
    Inherits Page
    Public Sub New()
      InitializeComponent()

      ConfigurationData.Singleton.DataGrid = Me.grid
      AddHandler ConfigurationData.Singleton.PropertyChanged, AddressOf ConfigurationData_PropertyChanged
      Me.SetDefaultPropertiesForStretchMode()
    End Sub

    Private Sub ConfigurationData_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
      Select Case e.PropertyName
        Case "ColumnStretchMode"
          Me.SetDefaultPropertiesForStretchMode()

        Case "UseAdvancedColumnManagement"
          Me.UseAdvancedColumnManagementChanged()
      End Select
    End Sub

    ' This method assigns default values to the various controls in the configuration panel
    ' and column properties, according to the currently selected "Column stretching mode".
    Private Sub SetDefaultPropertiesForStretchMode()
      ' A cell in edit mode will not refresh its content if the underlying data item
      ' is modified. We make sure to cancel a possible current edit process before 
      ' setting the new column widths.
      Me.configurationPanel.CancelColumnWidthGridEdit()

      Select Case ConfigurationData.Singleton.ColumnStretchMode
        Case ColumnStretchMode.None
          ' The ColumnStretchMinWidth property is not used with "starred-width" columns.
          ' Set a zero value to reflect that.
          ConfigurationData.Singleton.ColumnStretchMinWidth = 0.0
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = False
          ' It is not recommended to allow end-user column resizing when at least one 
          ' column width is "starred".
          ConfigurationData.Singleton.AllowColumnResizing = False
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = True

          Me.SetColumnWidths("SongName", "3*", 200.0, Nothing)
          Me.SetColumnWidths("Rating", "0.5*", Nothing, 70.0)
          Me.SetColumnWidths("LastPlayed", "*", Nothing, Nothing)
          Me.SetColumnWidths("ResetLastPlayed", "75", Nothing, Nothing)
          Me.SetColumnWidths("LastPlayedElapsed", "80", Nothing, Nothing)
          Me.SetColumnWidths("Artist", "2*", Nothing, Nothing)
          Me.SetColumnWidths("Country", "*", 100.0, Nothing)

        Case ColumnStretchMode.First
          ConfigurationData.Singleton.ColumnStretchMinWidth = 100.0
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = True
          ' It is not recommended to allow end-user column resizing when the first column
          ' is stretched.
          ConfigurationData.Singleton.AllowColumnResizing = False
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = True

          Me.SetColumnWidths("SongName", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("Rating", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("LastPlayed", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("ResetLastPlayed", "75", Nothing, Nothing)
          Me.SetColumnWidths("LastPlayedElapsed", "80", Nothing, Nothing)
          Me.SetColumnWidths("Artist", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("Country", Nothing, Nothing, Nothing)

        Case ColumnStretchMode.Last
          ConfigurationData.Singleton.ColumnStretchMinWidth = 70.0
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = True
          ConfigurationData.Singleton.AllowColumnResizing = True
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = True

          Me.SetColumnWidths("SongName", "250", Nothing, Nothing)
          Me.SetColumnWidths("Rating", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("LastPlayed", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("ResetLastPlayed", "75", Nothing, Nothing)
          Me.SetColumnWidths("LastPlayedElapsed", "80", Nothing, Nothing)
          Me.SetColumnWidths("Artist", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("Country", Nothing, Nothing, Nothing)

        Case ColumnStretchMode.All
          ConfigurationData.Singleton.ColumnStretchMinWidth = 50.0
          ConfigurationData.Singleton.IsColumnStretchMinWidthEnabled = True
          ' Setting the allowColumnResizingCheckBox is pointless because all of the 
          ' columns are auto sized and column resizing will be disabled no matter what.
          ConfigurationData.Singleton.IsAllowColumnResizingEnabled = False

          Me.SetColumnWidths("SongName", Nothing, 200.0, Nothing)
          Me.SetColumnWidths("Rating", Nothing, Nothing, 50.0)
          Me.SetColumnWidths("LastPlayed", Nothing, Nothing, 100.0)
          Me.SetColumnWidths("ResetLastPlayed", Nothing, 60.0, Nothing)
          Me.SetColumnWidths("LastPlayedElapsed", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("Artist", Nothing, Nothing, Nothing)
          Me.SetColumnWidths("Country", Nothing, Nothing, Nothing)

        Case Else
          Debug.Assert(False, "Unknown column stretch mode.")
      End Select
    End Sub

    Private Sub SetColumnWidths(ByVal fieldName As String, ByVal width As String, ByVal minWidth As Nullable(Of Double), ByVal maxWidth As Nullable(Of Double))
      Dim column As ColumnBase = Me.grid.Columns(fieldName)

      If width Is Nothing Then
        column.ClearValue(ColumnBase.WidthProperty)
      Else
        Dim converter As New ColumnWidthConverter()
        column.Width = CType(converter.ConvertFrom(Nothing, CultureInfo.InvariantCulture, width), ColumnWidth)
      End If

      If (minWidth.HasValue) AndAlso (ConfigurationData.Singleton.UseAdvancedColumnManagement) Then
        column.MinWidth = minWidth.Value
      Else
        column.ClearValue(ColumnBase.MinWidthProperty)
      End If

      If (maxWidth.HasValue) AndAlso (ConfigurationData.Singleton.UseAdvancedColumnManagement) Then
        column.MaxWidth = maxWidth.Value
      Else
        column.ClearValue(ColumnBase.MaxWidthProperty)
      End If
    End Sub

    Private Sub UseAdvancedColumnManagementChanged()
      ' Adds or removes specific Column MinWidth/MaxWidth according to whether the 
      ' advanced column management mode is used.
      Me.SetDefaultPropertiesForStretchMode()

      If ConfigurationData.Singleton.UseAdvancedColumnManagement Then
        Dim view As Xceed.Wpf.DataGrid.Views.ViewBase = Me.grid.View

        view.Headers.Add(CType(Me.FindResource("columnWidthRowTemplate"), DataTemplate))
        view.Headers.Add(CType(Me.FindResource("columnMinWidthRowTemplate"), DataTemplate))
        view.Headers.Add(CType(Me.FindResource("columnMaxWidthRowTemplate"), DataTemplate))
        view.Headers.Add(CType(Me.FindResource("headerSeparatorTemplate"), DataTemplate))
      Else
        Me.grid.View.Headers.Clear()
      End If
    End Sub

    Private Sub ResetLastPlayed_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
      Dim button As Button = CType(sender, Button)
      Dim dataRowView As System.Data.DataRowView = TryCast(button.DataContext, System.Data.DataRowView)

      If dataRowView IsNot Nothing Then
        dataRowView("LastPlayed") = DBNull.Value
      End If
    End Sub

    Private Sub LastPlayedElapsed_QueryValue(ByVal sender As Object, ByVal e As DataGridItemPropertyQueryValueEventArgs)
      Dim dataRowView As System.Data.DataRowView = TryCast(e.Item, System.Data.DataRowView)

      If dataRowView Is Nothing OrElse dataRowView("LastPlayed") Is DBNull.Value Then
        e.Value = ""
      Else
        Dim minutes As Long = Convert.ToInt64(Math.Floor((CType(DateTime.Now - CDate(dataRowView("LastPlayed")), TimeSpan)).TotalMinutes))

        If minutes < 0 Then
          e.Value = "n/a"
        ElseIf minutes >= 1051200 Then
          e.Value = Convert.ToInt64(Math.Floor(minutes / 525600)).ToString() & " years"
        ElseIf minutes >= 2880 Then
          e.Value = Convert.ToInt64(Math.Floor(minutes / 1440)).ToString() & " days"
        ElseIf minutes >= 120 Then
          e.Value = Convert.ToInt64(Math.Floor(minutes / 60)).ToString() & " hours"
        Else
          e.Value = minutes.ToString() & " minutes"
        End If
      End If
    End Sub
  End Class
End Namespace
