'
' Xceed DataGrid for WPF - SAMPLE CODE - Validation Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [Data.vb]
'  
' This class contains all the composers to use when filling our collection.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
' 

Imports System

Namespace Xceed.Wpf.DataGrid.Samples.Validation
  Public Class Data
    ' A structure representing all of a composer's information.
    Public Structure ComposerData
      Public Sub New(ByVal lastName As String, ByVal firstName As String, ByVal period As Period, ByVal birthYear As Integer, ByVal deathYear As Integer, ByVal compositionCount As Integer, _
       ByVal lastUpdate As DateTime)
        m_lastName = lastName
        m_firstName = firstName
        m_period = period
        m_birthYear = birthYear
        m_deathYear = deathYear
        m_compositionCount = compositionCount
        m_lastUpdate = lastUpdate
      End Sub

      Public ReadOnly Property LastName() As String
        Get
          Return m_lastName
        End Get
      End Property

      Public ReadOnly Property FirstName() As String
        Get
          Return m_firstName
        End Get
      End Property

      Public ReadOnly Property Period() As Period
        Get
          Return m_period
        End Get
      End Property

      Public ReadOnly Property BirthYear() As Integer
        Get
          Return m_birthYear
        End Get
      End Property

      Public ReadOnly Property DeathYear() As Integer
        Get
          Return m_deathYear
        End Get
      End Property

      Public ReadOnly Property CompositionCount() As Integer
        Get
          Return m_compositionCount
        End Get
      End Property

      Public ReadOnly Property LastUpdate() As DateTime
        Get
          Return m_lastUpdate
        End Get
      End Property

      Private m_lastName As String
      Private m_firstName As String
      Private m_period As Period
      Private m_birthYear As Integer
      Private m_deathYear As Integer
      Private m_compositionCount As Integer
      Private m_lastUpdate As DateTime
    End Structure

    ' Most of the information was retrieved from http://www.classical.net
    Public Shared Composers As ComposerData() = New ComposerData(29) {New ComposerData("von Bingen", "Hildegard", Period.Medieval, 1098, 1179, 37, _
     New DateTime(2008, 7, 11)), New ComposerData("von Wolkenstein", "Oswald", Period.Medieval, 1337, 1445, 19, _
     New DateTime(2006, 11, 19)), New ComposerData("Monteverdi", "Claudio", Period.Renaissance, 1567, 1643, 23, _
     New DateTime(2008, 7, 9)), New ComposerData("Purcell", "Henry", Period.Baroque, 1659, 1695, 42, _
     New DateTime(2008, 6, 24)), New ComposerData("Mozart", "Wolfgang Amadeus", Period.Classical, 1756, 1791, 65, _
     New DateTime(2007, 3, 21)), New ComposerData("Schubert", "Franz Peter", Period.Romantic, 1797, 1828, 84, _
     New DateTime(2008, 5, 1)), New ComposerData("Khachaturian", "Aram", Period.TwentiethCentury, 1903, 1978, 13, _
     New DateTime(2006, 11, 27)), _
     New ComposerData("Takemitsu", "Toru", Period.Modern, 1930, 1996, 21, _
     New DateTime(2005, 9, 19)), New ComposerData("Barraque", "Jean", Period.Modern, 1928, 1973, 28, _
     New DateTime(2008, 1, 5)), New ComposerData("Haydn", "Franz Joseph", Period.Classical, 1732, 1809, 32, _
     New DateTime(2006, 12, 21)), New ComposerData("Schobert", "Johann", Period.Classical, 1735, 1767, 37, _
     New DateTime(2002, 10, 9)), New ComposerData("van Beethoven", "Ludwig", Period.Classical, 1770, 1827, 41, _
     New DateTime(2008, 2, 16)), New ComposerData("Bach", "Carl Philipp Emanuel", Period.Classical, 1714, 1788, 23, _
     New DateTime(2004, 7, 22)), _
     New ComposerData("Tchaikovsky", "Piotr Ilyitch", Period.Romantic, 1840, 1893, 14, _
     New DateTime(2007, 9, 2)), New ComposerData("Debussy", "Claude ", Period.Romantic, 1862, 1918, 21, _
     New DateTime(2006, 8, 26)), New ComposerData("Dvorak", "Antonin", Period.Romantic, 1841, 1904, 16, _
     New DateTime(2001, 2, 21)), New ComposerData("Handel", "George Frideric", Period.Baroque, 1685, 1759, 11, _
     New DateTime(2007, 10, 4)), New ComposerData("Pachelbel", "Johann", Period.Baroque, 1653, 1706, 25, _
     New DateTime(2008, 1, 11)), New ComposerData("van Wassenaer", "Unico Wilhelm", Period.Baroque, 1692, 1766, 17, _
     New DateTime(2005, 5, 17)), _
     New ComposerData("Zelenka", "Jan Dismas", Period.Baroque, 1679, 1745, 16, _
     New DateTime(2008, 12, 3)), New ComposerData("de Vitry", "Philippe", Period.Medieval, 1291, 1361, 14, _
     New DateTime(2007, 5, 16)), New ComposerData("de Machaut", "Guillaume", Period.Medieval, 1300, 1377, 19, _
     New DateTime(2008, 1, 5)), New ComposerData("Praetorius", "Micheal", Period.Renaissance, 1571, 1621, 21, _
     New DateTime(2004, 10, 2)), New ComposerData("de Victoria", "Tomás Luis ", Period.Renaissance, 1549, 1611, 51, _
     New DateTime(2005, 3, 14)), _
     New ComposerData("Buxtehude", "Dietrich", Period.Baroque, 1637, 1707, 49, _
     New DateTime(2006, 9, 29)), New ComposerData("Ciconia", "Johannes ", Period.Medieval, 1335, 1411, 27, _
     New DateTime(2004, 2, 7)), New ComposerData("Landini", "Francesco ", Period.Medieval, 1325, 1397, 32, _
     New DateTime(2007, 7, 2)), New ComposerData("de Cabezon", "Antonio", Period.Renaissance, 1510, 1566, 37, _
     New DateTime(2005, 12, 9)), New ComposerData("Isaac", "Heinrich ", Period.Renaissance, 1450, 1517, 7, _
     New DateTime(2008, 4, 13)), New ComposerData("Gombert", "Nicolas", Period.Renaissance, 1490, 1556, 16, _
     New DateTime(2006, 6, 25))}

  End Class

  ' Define an enum of the musical period.
  Public Enum Period
    Medieval
    Renaissance
    Baroque
    Classical
    Romantic
    TwentiethCentury
    Modern
    Undefined
  End Enum
End Namespace
