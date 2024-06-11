/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [GroupingItem.cs]
 *  
 * This class implements a ComboBoxItem which contains a Grouping definition (1 or more 
 * GroupDescriptions with a description).
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.Grouping
{
  public class GroupingItem
  {
    public GroupingItem()
    {
    }

    private string m_description;

    public string Description
    {
      get
      {
        return m_description;
      }
      set
      {
        m_description = value;
      }
    }

    private ObservableCollection<GroupDescription> m_groupDescriptions = new ObservableCollection<GroupDescription>();

    public ObservableCollection<GroupDescription> GroupDescriptions
    {
      get
      {
        return m_groupDescriptions;
      }
    }

    public bool Equals( ObservableCollection<GroupDescription> groupDescriptions )
    {
      if( groupDescriptions == null )
        return false;

      if( groupDescriptions.Count != m_groupDescriptions.Count )
        return false;

      for( int i = 0; i < m_groupDescriptions.Count; i++ )
      {
        if( m_groupDescriptions[ i ] != groupDescriptions[ i ] )
        {
          PropertyGroupDescription propertyGroupDesc1 = m_groupDescriptions[ i ] as PropertyGroupDescription;
          PropertyGroupDescription propertyGroupDesc2 = groupDescriptions[ i ] as PropertyGroupDescription;

          if( ( propertyGroupDesc1 != null ) && ( propertyGroupDesc2 != null ) )
          {
            if( propertyGroupDesc1.PropertyName != propertyGroupDesc2.PropertyName )
              return false;
          }
          else
          {
            DataGridGroupDescription dataGridGroupDesc1 = m_groupDescriptions[ i ] as DataGridGroupDescription;
            DataGridGroupDescription dataGridGroupDesc2 = groupDescriptions[ i ] as DataGridGroupDescription;

            if( ( dataGridGroupDesc1 != null ) && ( dataGridGroupDesc2 != null ) )
            {
              if( dataGridGroupDesc1.PropertyName != dataGridGroupDesc2.PropertyName )
                return false;
            }
            else
            {
              return false;
            }
          }
        }
      }

      return true;
    }
  }
}
