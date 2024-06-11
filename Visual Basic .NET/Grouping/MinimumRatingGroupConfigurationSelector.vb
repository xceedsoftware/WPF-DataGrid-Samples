'
' Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [MinimumRatingGroupConfigurationSelector.vb]
'
' This class implements a GroupConfigurationSelector that
' sets the ItemContainerStyle for the items whose Rating field is
' at least MinimumRating when the DataGridControl is grouped by
' Rating
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Data
Imports System.Diagnostics
Imports System.ComponentModel

Namespace Xceed.Wpf.DataGrid.Samples.Grouping
  Public Class MinimumRatingGroupConfigurationSelector
    Inherits GroupConfigurationSelector

#Region "PUBLIC CONSTRUCTORS"

    Public Sub New(ByVal minimumRating As Integer)
      Me.MinimumRating = minimumRating
    End Sub

#End Region

#Region "PUBLIC PROPERTIES"

    Public Property MinimumRating() As Integer
      Get
        Return m_minItemCount
      End Get
      Set(ByVal value As Integer)
        If Value <> m_minItemCount Then
          m_minItemCount = Value
        End If
      End Set
    End Property

#End Region

#Region "PUBLIC METHODS"

    Public Overrides Function SelectGroupConfiguration(ByVal groupLevel As Integer, ByVal collectionViewGroup As System.Windows.Data.CollectionViewGroup, ByVal groupDescription As System.ComponentModel.GroupDescription) As GroupConfiguration
      Dim groupConfiguration As GroupConfiguration = Nothing

      If collectionViewGroup Is Nothing Then
        Return MyBase.SelectGroupConfiguration(groupLevel, collectionViewGroup, groupDescription)
      End If

      Dim fieldName As String = String.Empty

      Dim dataGridGroupDescription As DataGridGroupDescription = TryCast(groupDescription, DataGridGroupDescription)
      If Not dataGridGroupDescription Is Nothing Then
        fieldName = dataGridGroupDescription.PropertyName
      Else
        Dim propertyGroupDescription As PropertyGroupDescription = TryCast(groupDescription, PropertyGroupDescription)
        If Not propertyGroupDescription Is Nothing Then
          fieldName = propertyGroupDescription.PropertyName
        End If
      End If

      If String.IsNullOrEmpty(fieldName) = True Then
        Return MyBase.SelectGroupConfiguration(groupLevel, collectionViewGroup, groupDescription)
      End If

      ' We want this Selector to react only for "Rating" field
      If fieldName.Equals("Rating") = False Then
        Return MyBase.SelectGroupConfiguration(groupLevel, collectionViewGroup, groupDescription)
      End If

      Dim rating As Integer = 0

      If Int32.TryParse(collectionViewGroup.Name.ToString(), rating) = True Then
        Dim higherThan As Boolean = (rating >= Me.MinimumRating)

        If m_ratingHigherThanGroupConfiguration.ContainsKey(higherThan) = True Then
          groupConfiguration = m_ratingHigherThanGroupConfiguration(higherThan)
        Else
          Dim style As Style = New Style(GetType(Xceed.Wpf.DataGrid.DataRow))

          If higherThan Then
            ' the group's rating is high enough
            style.Setters.Add(New Setter(DataRow.BackgroundProperty, New SolidColorBrush(Color.FromArgb(&H33, &H00, &H99, &Hcc))))
          Else
            ' the group's rating is not high enough
			      style.Setters.Add(New Setter(DataRow.BackgroundProperty, New SolidColorBrush(Color.FromArgb(&H33, &Hff, &H99, &H00))))
          End If

          ' Create a new GroupConfiguration for this rating
          groupConfiguration = New GroupConfiguration()
          groupConfiguration.ItemContainerStyle = style

          m_ratingHigherThanGroupConfiguration.Add(higherThan, groupConfiguration)
        End If
      Else
        Debug.WriteLine("Error while converting rating to Int32")
      End If

      Return groupConfiguration
    End Function

#End Region

#Region "PRIVATE FIELDS"

    Private m_minItemCount As Integer = 0
    Private m_ratingHigherThanGroupConfiguration As Dictionary(Of Boolean, GroupConfiguration) = New Dictionary(Of Boolean, GroupConfiguration)()

#End Region
  End Class
End Namespace
