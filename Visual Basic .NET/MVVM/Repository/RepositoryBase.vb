'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [RepositoryBase.vb]
' *  
' * This class provides the necessary links between the data source and the view model so the business logic can be conducted on model entities.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System.Data
Imports System.Reflection

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
  Public MustInherit Class RepositoryBase
#Region "NorthwindDataSet Property"

    Private Shared ReadOnly Property NorthwindDataSet() As DataSet
      Get
        If m_dataSet IsNot Nothing Then
          Return m_dataSet
        End If

        Return InlineAssignHelper(m_dataSet, Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet())
      End Get
    End Property

    Private Shared m_dataSet As DataSet

#End Region

    Protected Function LoadDataSource(ByVal table As String) As DataView
      Return RepositoryBase.NorthwindDataSet.Tables(table).DefaultView
    End Function

    Protected Function GetAllEntities(Of T As New)(ByVal entitiesView As DataView) As IList(Of T)
      'This is a generic method used to generate items from the source DataRows corresponding to models.
      Dim entity = Nothing
      Dim entityProperties = GetType(T).GetProperties().ToList()
      Dim entities = New List(Of T)(entitiesView.Count)

      For Each entityDataRowView As DataRowView In entitiesView
        'Create a new entity containing all the values of the corresponding data item, and which corresponds to a model.
        entity = Me.CreateModelEntity(Of T)(entityDataRowView, entityProperties)
        entities.Add(entity)
      Next entityDataRowView

      Return entities
    End Function

    Private Function CreateModelEntity(Of T As New)(ByVal dataRowView As DataRowView, ByVal entityProperties As IList(Of PropertyInfo)) As T
      'Create a new item and fill its properties with the corresponding DataRow values.
      Dim entity As New T()

      For Each [property] In entityProperties
        If [property].Name = "IsModified" OrElse [property].Name = "IDataErrorInfo_Item" Then
          Continue For
        End If

        Dim propertyType = [property].PropertyType

        'Collection properties are filled with detail items through the appropriate repository (e.g. Product.OrderDetails).
        If propertyType.IsGenericType AndAlso GetType(IEnumerable).IsAssignableFrom(propertyType) Then
          Continue For
        End If

        'Cannot convert a DBNull to a DateTime, use null instead.
        If (propertyType Is GetType(Nullable(Of DateTime))) AndAlso (dataRowView([property].Name) Is DBNull.Value) Then
          [property].SetValue(entity, Nothing, Nothing)
          Continue For
        End If

        'Set the entity's property to the corresponding data source value.
        [property].SetValue(entity, dataRowView([property].Name), Nothing)
      Next [property]

      Return entity
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
      target = value
      Return value
    End Function
  End Class
End Namespace