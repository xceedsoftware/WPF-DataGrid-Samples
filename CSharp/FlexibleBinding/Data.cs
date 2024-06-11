/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Data.cs]
 *  
 * This class contains all the composers to use when filling our different collections
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;

namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
{
  public class Data
  {
    // A structure representing all of a composer's information.
    public struct ComposerData
    {
      public ComposerData(
        string lastName,
        string firstName,
        Period period,
        int birthYear,
        int deathYear )
      {
        m_lastName = lastName;
        m_firstName = firstName;
        m_period = period;
        m_birthYear = birthYear;
        m_deathYear = deathYear;
      }

      public string LastName
      {
        get
        {
          return m_lastName;
        }
      }

      public string FirstName
      {
        get
        {
          return m_firstName;
        }
      }

      public Period Period
      {
        get
        {
          return m_period;
        }
      }

      public int BirthYear
      {
        get
        {
          return m_birthYear;
        }
      }

      public int DeathYear
      {
        get
        {
          return m_deathYear;
        }
      }

      private string m_lastName;
      private string m_firstName;
      private Period m_period;
      private int m_birthYear;
      private int m_deathYear;
    }

    // All the information was retrieved from http://www.classical.net
    public static ComposerData[] Composers = new ComposerData[ 30 ]
    {
      new ComposerData( "von Bingen" , "Hildegard" , Period.Medieval , 1098, 1179 ),
      new ComposerData( "Monteverdi", "Claudio", Period.Renaissance, 1567, 1643 ),
      new ComposerData( "Purcell", "Henry", Period.Baroque, 1659, 1695 ),
      new ComposerData( "Mozart", "Wolfgang Amadeus", Period.Classical, 1756, 1791 ),
      new ComposerData( "Schubert", "Franz Peter", Period.Romantic, 1797, 1828 ),
      new ComposerData( "Khachaturian", "Aram", Period.TwentiethCentury, 1903, 1978 ),
      new ComposerData( "Takemitsu", "Toru", Period.Modern, 1930, 1996 ),
      new ComposerData( "Barraque" , "Jean" , Period.Modern, 1928, 1973 ),
      new ComposerData( "Haydn" , "Franz Joseph" , Period.Classical, 1732, 1809 ),
      new ComposerData( "Schobert" , "Johann", Period.Classical, 1735, 1767 ),
      new ComposerData( "van Beethoven" , "Ludwig", Period.Classical, 1770, 1827 ),
      new ComposerData( "Bach" , "Carl Philipp Emanuel", Period.Classical, 1714, 1788 ),
      new ComposerData( "Tchaikovsky" , "Piotr Ilyitch", Period.Romantic , 1840, 1893 ),
      new ComposerData( "Debussy" , "Claude ", Period.Romantic, 1862, 1918 ),
      new ComposerData( "Dvorak" , "Antonin", Period.Romantic, 1841, 1904 ),
      new ComposerData( "Handel" , "George Frideric", Period.Baroque, 1685, 1759 ),
      new ComposerData( "Pachelbel" , "Johann", Period.Baroque, 1653, 1706 ),
      new ComposerData( "van Wassenaer" , "Unico Wilhelm", Period.Baroque, 1692, 1766 ),
      new ComposerData( "Zelenka" , "Jan Dismas", Period.Baroque, 1679, 1745 ),
      new ComposerData( "von Wolkenstein" , "Oswald", Period.Medieval, 1337, 1445 ),
      new ComposerData( "de Vitry" , "Philippe", Period.Medieval, 1291, 1361 ),
      new ComposerData( "de Machaut" , "Guillaume", Period.Medieval, 1300, 1377 ),
      new ComposerData( "Praetorius" , "Micheal", Period.Renaissance, 1571, 1621 ),
      new ComposerData( "de Victoria" , "Tomás Luis ", Period.Renaissance, 1549, 1611 ),
      new ComposerData( "Buxtehude" , "Dietrich", Period.Baroque, 1637, 1707 ),
      new ComposerData( "Ciconia" , "Johannes ", Period.Medieval, 1335, 1411 ),
      new ComposerData( "Landini" , "Francesco ", Period.Medieval, 1325, 1397 ),
      new ComposerData( "de Cabezon" , "Antonio", Period.Renaissance, 1510, 1566 ),
      new ComposerData( "Isaac" , "Heinrich ", Period.Renaissance, 1450, 1517 ),
      new ComposerData( "Gombert" , "Nicolas", Period.Renaissance, 1490, 1556 )
    };

    //
    // Description for all the binding examples demonstrated in this sample. 
    //

    public static string StringArrayText =
      "When binding the grid to a string array, new data rows cannot be added nor deleted. " +
      Environment.NewLine + Environment.NewLine +
      "A data row is automatically created for each string in the array, and only a " +
      "single column is created. The value of the cell in each data row corresponds to " +
      "the actual string.";

    public static string JaggedArrayText =
      "When binding the grid to a jagged array, new data rows cannot be added nor deleted. " +
      "Existing data rows can be modified." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each sub array, and a column is created for each element " +
      "contained in the sub arrays. The values of the cells in each data row correspond to " +
      "the values of the elements in the sub arrays.";

    public static string XMLDocumentText =
      "When binding the grid to a DataGridCollectionViewSource whose underlying data source is an " +
      "XMLDocument, " +
      "new data rows can be added, deleted, or modified by handling various events." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each node. " +
      "The values of the cells in each data row correspond to the attributes or subnodes of each node.";

    public static string XDocumentText =
      "When binding the grid to a DataGridCollectionViewSource whose underlying data source is a " +
      "LINQ query using XDocument, " +
      "new data rows cannot be added nor deleted." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each object returned by the query. " +
      "The values of the cells in each data row correspond to the property value of each object.";

    public static string CollectionBaseText =
      "When binding the grid to a Collection, new data rows cannot be added nor deleted. " +
      "Existing data rows can be modified. If the class implements the ITypedList interface, " +
      " it is used to create the appropriate columns." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each object in the collection, and a column is created for " +
      "each property. The values of the cells in each data row correspond to the property " +
      "value of each object";

    public static string CompleteBindingListText =
      "When binding the grid to a DataGridCollectionViewSource whose underlying data source implements the " +
      "IBindingList interface, " +
      "new data rows can be added, deleted, or modified. If the class implements the " +
      "ITypedList interface, it is used to create the appropriate columns." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each object in the collection, and a column is created for " +
      "each property. The values of the cells in each data row correspond to the property " +
      "value of each object. If IEditableObject is implemented, the class itself can define " +
      "the behavior of transactions when editing rows.";

    public static string DataGridCollectionViewText =
      "When binding the grid to a DataGridCollectionViewSource whose type, by default, is a DataGridCollectionView, " +
      "which uses a DataTable as its source, new data rows can be added, deleted, or modified." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each record in the table and a column is created for " +
      "each field. The values of the cells in each data row correspond to the values of the record fields. ";

    public static string DataGridCollectionViewWithMasterDetailText =
      "When binding the grid to a DataGridCollectionViewSource whose type, by default, is a DataGridCollectionView, " +
      "which uses a DataTable with detail relations as its source, new data rows can be added, deleted, or modified." +
      Environment.NewLine + Environment.NewLine +
      "A data row is created for each record in the table and a column is created for " +
      "each field. A detail will be created under each master row. The values of the cells in each data row correspond to the values of the record fields. ";
  }

  // Define an enum of the musical period.
  public enum Period
  {
    Medieval,
    Renaissance,
    Baroque,
    Classical,
    Romantic,
    TwentiethCentury,
    Modern,
    Undefined
  }
}
