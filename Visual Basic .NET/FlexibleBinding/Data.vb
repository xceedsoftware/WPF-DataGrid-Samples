'
' Xceed DataGrid for WPF - SAMPLE CODE - Flexible Binding Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [Data.vb]
'  
' This class contains all the composers to use when filling our different collections
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System

Namespace Xceed.Wpf.DataGrid.Samples.FlexibleBinding
  Public Class Data
    ' A structure representing all of a composer's information.
    Public Structure ComposerData
      Public Sub New(ByVal lastName As String, ByVal firstName As String, ByVal period As Period, ByVal birthYear As Integer, ByVal deathYear As Integer)
        m_lastName = lastName
        m_firstName = firstName
        m_period = period
        m_birthYear = birthYear
        m_deathYear = deathYear
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

      Private m_lastName As String
      Private m_firstName As String
      Private m_period As Period
      Private m_birthYear As Integer
      Private m_deathYear As Integer
    End Structure

    ' All the information was retrieved from http://www.classical.net
    Public Shared Composers As ComposerData() = New ComposerData(29) _
    { _
      New ComposerData("von Bingen", "Hildegard", Period.Medieval, 1098, 1179), _
      New ComposerData("Monteverdi", "Claudio", Period.Renaissance, 1567, 1643), _
      New ComposerData("Purcell", "Henry", Period.Baroque, 1659, 1695), _
      New ComposerData("Mozart", "Wolfgang Amadeus", Period.Classical, 1756, 1791), _
      New ComposerData("Schubert", "Franz Peter", Period.Romantic, 1797, 1828), _
      New ComposerData("Khachaturian", "Aram", Period.TwentiethCentury, 1903, 1978), _
      New ComposerData("Takemitsu", "Toru", Period.Modern, 1930, 1996), _
      New ComposerData("Barraque", "Jean", Period.Modern, 1928, 1973), _
      New ComposerData("Haydn", "Franz Joseph", Period.Classical, 1732, 1809), _
      New ComposerData("Schobert", "Johann", Period.Classical, 1735, 1767), _
      New ComposerData("van Beethoven", "Ludwig", Period.Classical, 1770, 1827), _
      New ComposerData("Bach", "Carl Philipp Emanuel", Period.Classical, 1714, 1788), _
      New ComposerData("Tchaikovsky", "Piotr Ilyitch", Period.Romantic, 1840, 1893), _
      New ComposerData("Debussy", "Claude ", Period.Romantic, 1862, 1918), _
      New ComposerData("Dvorak", "Antonin", Period.Romantic, 1841, 1904), _
      New ComposerData("Handel", "George Frideric", Period.Baroque, 1685, 1759), _
      New ComposerData("Pachelbel", "Johann", Period.Baroque, 1653, 1706), _
      New ComposerData("van Wassenaer", "Unico Wilhelm", Period.Baroque, 1692, 1766), _
      New ComposerData("Zelenka", "Jan Dismas", Period.Baroque, 1679, 1745), _
      New ComposerData("von Wolkenstein", "Oswald", Period.Medieval, 1337, 1445), _
      New ComposerData("de Vitry", "Philippe", Period.Medieval, 1291, 1361), _
      New ComposerData("de Machaut", "Guillaume", Period.Medieval, 1300, 1377), _
      New ComposerData("Praetorius", "Micheal", Period.Renaissance, 1571, 1621), _
      New ComposerData("de Victoria", "Tomás Luis ", Period.Renaissance, 1549, 1611), _
      New ComposerData("Buxtehude", "Dietrich", Period.Baroque, 1637, 1707), _
      New ComposerData("Ciconia", "Johannes ", Period.Medieval, 1335, 1411), _
      New ComposerData("Landini", "Francesco ", Period.Medieval, 1325, 1397), _
      New ComposerData("de Cabezon", "Antonio", Period.Renaissance, 1510, 1566), _
      New ComposerData("Isaac", "Heinrich ", Period.Renaissance, 1450, 1517), _
      New ComposerData("Gombert", "Nicolas", Period.Renaissance, 1490, 1556) _
    }

    '
    ' Description for all the binding examples demonstrated in this sample. 
    '

    Public Shared StringArrayText As String = _
      "When binding the grid to a string array, new data rows cannot be added nor deleted. " & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is automatically created for each string in the array, and only a " & _
      "single column is created. The value of the cell in each data row corresponds to " & _
      "the actual string."

    Public Shared JaggedArrayText As String = _
      "When binding the grid to a jagged array, new data rows cannot be added nor deleted. " & _
      "Existing data rows can be modified." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each sub array, and a column is created for each element " & _
      "contained in the sub arrays. The values of the cells in each data row correspond to " & _
      "the values of the elements in the sub arrays."

    Public Shared XMLDocumentText As String = _
      "When binding the grid to a DataGridCollectionViewSource whose underlying data source is an " & _
      "XMLDocument, " & _
      "new data rows can be added, deleted, or modified by handling various events." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each node. " & _
      "The values of the cells in each data row correspond to the attributes or subnodes of each node."

    Public Shared XDocumentText As String = _
      "When binding the grid to a DataGridCollectionViewSource whose underlying data source is a " & _
      "LINQ query using XDocument, " & _
      "new data rows cannot be added nor deleted." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each object returned by the query. " & _
      "The values of the cells in each data row correspond to the property value of each object."

    Public Shared CollectionBaseText As String = _
      "When binding the grid to a Collection, new data rows cannot be added nor deleted. " & _
      "Existing data rows can be modified. If the class implements the ITypedList interface, " & _
      " it is used to create the appropriate columns." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each object in the collection, and a column is created for " & _
      "each property. The values of the cells in each data row correspond to the property " & _
      "value of each object"

    Public Shared CompleteBindingListText As String = _
      "When binding the grid to a DataGridCollectionViewSource " & _
      "whose underlying data source implements the IBindingList interface, " & _
      "new data rows can be added, deleted, or modified. If the class implements the " & _
      "ITypedList interface, it is used to create the appropriate columns." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each object in the collection, and a column is created for " & _
      "each property. The values of the cells in each data row correspond to the property " & _
      "value of each object. If IEditableObject is implemented, the class itself can define " & _
      "the behavior of transactions when editing rows."

    Public Shared DataGridCollectionViewText As String = _
      "When binding the grid to a DataGridCollectionViewSource whose type, by default, is a DataGridCollectionView, " & _
      "which uses a DataTable as its source, new data rows can be added, deleted, or modified." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each record in the table and a column is created for " & _
      "each field. The values of the cells in each data row correspond to the values of the record fields. "

    Public Shared DataGridCollectionViewWithMasterDetailText As String = _
      "When binding the grid to a DataGridCollectionViewSource whose type, by default, is a DataGridCollectionView, " & _
      "which uses a DataTable with detail relations as its source, new data rows can be added, deleted, or modified." & _
      Environment.NewLine & Environment.NewLine & _
      "A data row is created for each record in the table and a column is created for " & _
      "each field. A detail will be created under each master row. The values of the cells in each data row correspond to the values of the record fields. "

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
