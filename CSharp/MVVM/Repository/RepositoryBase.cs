/*
 * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [RepositoryBase.cs]
 *  
 * This class provides the necessary links between the data source and the view model so the business logic can be conducted on model entities.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Xceed.Wpf.DataGrid.Samples.MVVM.Repository
{
  public abstract class RepositoryBase
  {
    #region NorthwindDataSet Property

    private static DataSet NorthwindDataSet
    {
      get
      {
        if( m_dataSet != null )
          return m_dataSet;

        return m_dataSet = Xceed.Wpf.DataGrid.Samples.SampleData.DataProvider.GetNorthwindDataSet();
      }
    }

    private static DataSet m_dataSet;

    #endregion

    protected DataView LoadDataSource( string table )
    {
      return RepositoryBase.NorthwindDataSet.Tables[ table ].DefaultView;
    }

    protected IList<T> GetAllEntities<T>( DataView entitiesView ) where T : new()
    {
      //This is a generic method used to generate items from the source DataRows corresponding to models.
      var entity = default( T );
      var entityProperties = typeof( T ).GetProperties().ToList();
      var entities = new List<T>( entitiesView.Count );

      foreach( DataRowView entityDataRowView in entitiesView )
      {
        //Create a new entity containing all the values of the corresponding data item, and which corresponds to a model.
        entity = this.CreateModelEntity<T>( entityDataRowView, entityProperties );
        entities.Add( entity );
      }

      return entities;
    }

    private T CreateModelEntity<T>( DataRowView dataRowView, IList<PropertyInfo> entityProperties ) where T : new()
    {
      //Create a new item and fill its properties with the corresponding DataRow values.
      T entity = new T();

      foreach( var property in entityProperties )
      {
        if( property.Name == "IsModified" )
          continue;

        var propertyType = property.PropertyType;

        //Collection properties are filled with detail items through the appropriate repository (e.g. Product.OrderDetails).
        if( propertyType.IsGenericType && typeof( IEnumerable ).IsAssignableFrom( propertyType ) )
          continue;

        //Cannot convert a DBNull to a DateTime, use null instead.
        if( ( propertyType == typeof( DateTime? ) ) && ( dataRowView[ property.Name ] == DBNull.Value ) )
        {
          property.SetValue( entity, null, null );
          continue;
        }

        //Set the entity's property to the corresponding data source value.
        property.SetValue( entity, dataRowView[ property.Name ], null );
      }

      return entity;
    }
  }
}
