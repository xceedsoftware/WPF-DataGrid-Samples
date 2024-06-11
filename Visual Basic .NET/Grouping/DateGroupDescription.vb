'
' Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [DateGroupDescription.vb]
'
' This class implements a GroupDescription that can be used in a CollectionViewSource.
' It groups the item by range of date (today, yesterday, this week, last week,
' last month, older).
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Globalization
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Grouping
  Public Class DateGroupDescription
    Inherits DataGridGroupDescription
    Public Sub New()
      MyBase.New()
      Me.SortComparer = m_dateGroupComparer
    End Sub

    Public Sub New(ByVal propertyName As String)
      MyBase.New(propertyName)
      Me.SortComparer = m_dateGroupComparer
    End Sub

    Public Overrides Function GroupNameFromItem(ByVal item As Object, ByVal level As Integer, ByVal cultureInfo As CultureInfo) As Object
      Dim value As Object = MyBase.GroupNameFromItem(item, level, cultureInfo)

      Try
        Dim dateValue As DateTime = Convert.ToDateTime(value)
        Dim daysToStartOfWeek As Integer = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - DateTime.Today.DayOfWeek
        Dim thisWeek As DateTime

        ' A positive number of days to the start of this week means that the 
        ' FirstDayOfWeek is not a sunday and that today's weekday is between a sunday 
        ' (included) and the first day of the week (excluded). Correct this delta.
        If daysToStartOfWeek > 0 Then
          daysToStartOfWeek -= 7
        End If

        thisWeek = DateTime.Today.AddDays(daysToStartOfWeek)

        If dateValue >= DateTime.Today Then
          value = "Today"
        ElseIf dateValue >= DateTime.Today.AddDays(-1) Then
          value = "Yesterday"
        ElseIf dateValue >= thisWeek Then
          value = "Previously This Week"
        ElseIf dateValue >= thisWeek.AddDays(-7) Then
          value = "Last Week"
        ElseIf dateValue >= thisWeek.AddDays(-30) Then
          value = "Last Month"
        Else
          value = "Old stuff!"
        End If
      Catch e1 As InvalidCastException
      End Try

      Return value
    End Function

    Private Shared m_dateGroupComparer As DateGroupComparer = New DateGroupComparer()

    Private Class DateGroupComparer
      Implements IComparer

      Shared Sub New()
        m_nameValue.Add("Old stuff!", 1)
        m_nameValue.Add("Last Month", 2)
        m_nameValue.Add("Last Week", 3)
        m_nameValue.Add("Previously This Week", 4)
        m_nameValue.Add("Yesterday", 5)
        m_nameValue.Add("Today", 6)
      End Sub

      Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim xNameValue As Object = m_nameValue(x)
        Dim yNameValue As Object = m_nameValue(y)

        If (xNameValue Is Nothing) Or (yNameValue Is Nothing) Then
          Throw New InvalidOperationException("An attempt was made to Compare two values when one or both values to compare does not exist in the column.")
        End If

        Return CType(xNameValue, Integer) - CType(yNameValue, Integer)
      End Function

      Private Shared m_nameValue As Hashtable = New Hashtable()
    End Class
  End Class
End Namespace

