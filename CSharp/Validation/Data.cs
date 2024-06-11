/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [Data.cs]
 *  
 * This class contains all the composers to use when filling our collection.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;

namespace Xceed.Wpf.DataGrid.Samples.Validation
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
        int deathYear,
        int compositionCount,
        DateTime lastUpdate )
      {
        m_lastName = lastName;
        m_firstName = firstName;
        m_period = period;
        m_birthYear = birthYear;
        m_deathYear = deathYear;
        m_compositionCount = compositionCount;
        m_lastUpdate = lastUpdate;
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

      public int CompositionCount
      {
        get
        {
          return m_compositionCount;
        }
      }

      public DateTime LastUpdate
      {
        get
        {
          return m_lastUpdate;
        }
      }

      private string m_lastName;
      private string m_firstName;
      private Period m_period;
      private int m_birthYear;
      private int m_deathYear;
      private int m_compositionCount;
      private DateTime m_lastUpdate;
    }

    // Most of the information was retrieved from http://www.classical.net
    public static ComposerData[] Composers = new ComposerData[ 30 ]
    {
      new ComposerData( "von Bingen" , "Hildegard" , Period.Medieval , 1098, 1179, 37, new DateTime( 2008, 7, 11 ) ),
      new ComposerData( "von Wolkenstein" , "Oswald", Period.Medieval, 1337, 1445, 19, new DateTime( 2006, 11, 19 ) ),
      new ComposerData( "Monteverdi", "Claudio", Period.Renaissance, 1567, 1643, 23, new DateTime( 2008, 7, 9 ) ),
      new ComposerData( "Purcell", "Henry", Period.Baroque, 1659, 1695, 42, new DateTime( 2008, 6, 24 ) ),
      new ComposerData( "Mozart", "Wolfgang Amadeus", Period.Classical, 1756, 1791, 65, new DateTime( 2007, 3, 21 ) ),
      new ComposerData( "Schubert", "Franz Peter", Period.Romantic, 1797, 1828, 84, new DateTime( 2008, 5, 1 ) ),
      new ComposerData( "Khachaturian", "Aram", Period.TwentiethCentury, 1903, 1978, 13, new DateTime( 2006, 11, 27 ) ),
      new ComposerData( "Takemitsu", "Toru", Period.Modern, 1930, 1996, 21, new DateTime( 2005, 9, 19 ) ),
      new ComposerData( "Barraque" , "Jean" , Period.Modern, 1928, 1973, 28, new DateTime( 2008, 1, 5 ) ),
      new ComposerData( "Haydn" , "Franz Joseph" , Period.Classical, 1732, 1809, 32, new DateTime( 2006, 12, 21 ) ),
      new ComposerData( "Schobert" , "Johann", Period.Classical, 1735, 1767, 37, new DateTime( 2002, 10, 9 ) ),
      new ComposerData( "van Beethoven" , "Ludwig", Period.Classical, 1770, 1827, 41, new DateTime( 2008, 2, 16 ) ),
      new ComposerData( "Bach" , "Carl Philipp Emanuel", Period.Classical, 1714, 1788, 23, new DateTime( 2004, 7, 22 ) ),
      new ComposerData( "Tchaikovsky" , "Piotr Ilyitch", Period.Romantic , 1840, 1893, 14, new DateTime( 2007, 9, 2 ) ),
      new ComposerData( "Debussy" , "Claude ", Period.Romantic, 1862, 1918, 21, new DateTime( 2006, 8, 26 ) ),
      new ComposerData( "Dvorak" , "Antonin", Period.Romantic, 1841, 1904, 16, new DateTime( 2001, 2, 21 ) ),
      new ComposerData( "Handel" , "George Frideric", Period.Baroque, 1685, 1759, 11, new DateTime( 2007, 10, 4 ) ),
      new ComposerData( "Pachelbel" , "Johann", Period.Baroque, 1653, 1706, 25, new DateTime( 2008, 1, 11 ) ),
      new ComposerData( "van Wassenaer" , "Unico Wilhelm", Period.Baroque, 1692, 1766, 17, new DateTime( 2005, 5, 17 ) ),
      new ComposerData( "Zelenka" , "Jan Dismas", Period.Baroque, 1679, 1745, 16, new DateTime( 2008, 12, 3 ) ),
      new ComposerData( "de Vitry" , "Philippe", Period.Medieval, 1291, 1361, 14, new DateTime( 2007, 5, 16 ) ),
      new ComposerData( "de Machaut" , "Guillaume", Period.Medieval, 1300, 1377, 19, new DateTime( 2008, 1, 5 ) ),
      new ComposerData( "Praetorius" , "Micheal", Period.Renaissance, 1571, 1621, 21, new DateTime( 2004, 10, 2 ) ),
      new ComposerData( "de Victoria" , "Tomás Luis ", Period.Renaissance, 1549, 1611, 51, new DateTime( 2005, 3, 14 ) ),
      new ComposerData( "Buxtehude" , "Dietrich", Period.Baroque, 1637, 1707, 49, new DateTime( 2006, 9, 29 ) ),
      new ComposerData( "Ciconia" , "Johannes ", Period.Medieval, 1335, 1411, 27, new DateTime( 2004, 2, 7 ) ),
      new ComposerData( "Landini" , "Francesco ", Period.Medieval, 1325, 1397, 32, new DateTime( 2007, 7, 2 ) ),
      new ComposerData( "de Cabezon" , "Antonio", Period.Renaissance, 1510, 1566, 37, new DateTime( 2005, 12, 9 ) ),
      new ComposerData( "Isaac" , "Heinrich ", Period.Renaissance, 1450, 1517, 7, new DateTime( 2008, 4, 13 ) ),
      new ComposerData( "Gombert" , "Nicolas", Period.Renaissance, 1490, 1556, 16, new DateTime( 2006, 6, 25 ) )
    };

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
