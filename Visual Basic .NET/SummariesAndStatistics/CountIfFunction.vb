'
' Xceed DataGrid for WPF - SAMPLE CODE - Summaries & Statistics Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CountIfFunction.vb]
'  
' This file demonstrates how to create a custom statistical function that can be used in 
' the Xceed DataGridControl.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Text.RegularExpressions
Imports Xceed.Wpf.DataGrid.Stats

Namespace Xceed.Wpf.DataGrid.Samples.SummariesAndStatistics
  ' This statistical function derives from CumulativeStatFunction because it can 
  ' accumulate "partial" results. For instance, results of sub-group. This allows
  ' for better performance.
  Public Class CountIfFunction
    Inherits CumulativeStatFunction

    ' A parameterless constructor is necessary to use the class in XAML.
    Public Sub New()
      MyBase.New()
      m_conditions = New ObservableCollection(Of String)()
      AddHandler m_conditions.CollectionChanged, AddressOf m_conditions_CollectionChanged
    End Sub
    ' Initialize a new instance of the CountIfFunction specifying the ResultPropertyName 
    ' and the SourcePropertyName.
    Public Sub New(ByVal resultPropertyName As String, ByVal sourcePropertyNames As String)
      MyBase.New(resultPropertyName, sourcePropertyNames)
      m_conditions = New ObservableCollection(Of String)()
      AddHandler m_conditions.CollectionChanged, AddressOf m_conditions_CollectionChanged
    End Sub

    ' Each condition applies to the values of the corresponding source property name
    ' (e.g., the first condition applies to the values of the first source property name).
    ' Gets the conditions that will be applied to the various values.
    Public ReadOnly Property Conditions() As ObservableCollection(Of String)
      Get
        Return m_conditions
      End Get
    End Property

    ' When the grid needs to create temporary CountIfFunction instances for its 
    ' calculation, this method will be called. Be sure to initialize everything 
    ' having an impact on the result here.
    Protected Overrides Sub InitializeFromInstance(ByVal source As StatFunction)
      MyBase.InitializeFromInstance(source)

      For Each condition As String In (CType(source, CountIfFunction)).Conditions
        Me.Conditions.Add(condition)
      Next condition
    End Sub

    ' Validate the CountIf statistical function to make sure that it is capable
    ' to calculate its result. In our case, we need to make sure that a ResultPropertyName
    ' has been specified and that we have the same number of source property names
    ' as conditions.
    Protected Overrides Sub Validate()
      If (Me.ResultPropertyName Is Nothing) OrElse (Me.ResultPropertyName = String.Empty) OrElse (m_conditions.Count <> Me.SourcePropertyName.Split(","c).Length) Then
        Throw New InvalidOperationException()
      End If
    End Sub

    ' This method will be called when a new calculation is about to begin.
    Protected Overrides Sub Reset()
      m_count = 0
    End Sub

    ' This method will be called for each data item that is part of the set (a group or 
    ' the grid).
    Protected Overrides Sub Accumulate(ByVal values As Object())
      Dim i As Integer = 0
      Do While i < m_conditions.Count
        ' As soon as one condition does not match is associated value, we simply
        ' return without having done the accumulation (the count increment).
        If (Not Regex.IsMatch(values(i).ToString(), m_conditions(i))) Then
          Return
        End If
        i += 1
      Loop

      ' The compilation configuration will cause this line to throw
      ' if an OverflowException occurs.
      m_count += 1
    End Sub

    ' This method will be called when calculating the result of a group having 
    ' sub-groups. Obviously, it will be called once for each sub-group.
    Protected Overrides Sub AccumulateChildResult(ByVal childResult As StatResult)
      m_count += Convert.ToInt64(childResult.Value)
    End Sub

    ' This method should return the result calculated so far.
    Protected Overrides Function GetResult() As StatResult
      Return New StatResult(m_count)
    End Function

    ' The addition of the Conditions property, which influences the result of the
    ' statistical function, the CountIf function requires us to override IsEquivalent 
    ' and GetEquivalenceKey to return a new key when 2 instances are compared.
    Protected Overrides Function IsEquivalent(ByVal statFunction As StatFunction) As Boolean
      Dim countIfFunction As CountIfFunction = TryCast(statFunction, CountIfFunction)

      If countIfFunction Is Nothing Then
        Return False
      End If

      If m_conditions.Count <> countIfFunction.Conditions.Count Then
        Return False
      End If

      Dim i As Integer = 0
      Do While i < m_conditions.Count
        If m_conditions(i) <> countIfFunction.Conditions(i) Then
          Return False
        End If
        i += 1
      Loop

      Return MyBase.IsEquivalent(statFunction)
    End Function

    Protected Overrides Function GetEquivalenceKey() As Integer
      Dim hashCode As Integer = MyBase.GetEquivalenceKey()

      Dim i As Integer = 0
      Do While i < m_conditions.Count
        hashCode = hashCode Xor m_conditions(i).GetHashCode()
        i += 1
      Loop

      Return hashCode
    End Function

    ' Do not allow the Conditions property to be changed if the statistical function has
    ' been sealed (i.e, assigned to the DataGridCollectionView.StatFunctions property).
    Private Sub m_conditions_CollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
      Me.CheckSealed()
    End Sub

    Private m_conditions As ObservableCollection(Of String)
    Private m_count As Long
  End Class

End Namespace
