'
' Xceed DataGrid for WPF - SAMPLE CODE - Grouping Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [GroupingItem.vb]
'
' This class implements a ComboBoxItem which contains a Grouping definition (1 or more
' GroupDescriptions with a description).
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Windows.Data

Namespace Xceed.Wpf.DataGrid.Samples.Grouping
  Public Class GroupingItem
    Public Sub New()
    End Sub

    Private m_description As String

    Public Property Description() As String
      Get
        Return m_description
      End Get
      Set(ByVal value As String)
        m_description = value
      End Set
    End Property

    Private m_groupDescriptions As ObservableCollection(Of GroupDescription) = New ObservableCollection(Of GroupDescription)()

    Public ReadOnly Property GroupDescriptions() As ObservableCollection(Of GroupDescription)
      Get
        Return m_groupDescriptions
      End Get
    End Property

    Public Overloads Function Equals(ByVal groupDescriptions As ObservableCollection(Of GroupDescription)) As Boolean
      If groupDescriptions Is Nothing Then
        Return False
      End If

      If groupDescriptions.Count <> m_groupDescriptions.Count Then
        Return False
      End If

      Dim i As Integer = 0

      For i = 0 To m_groupDescriptions.Count - 1
        If Not m_groupDescriptions(i) Is groupDescriptions(i) Then
          Dim propertyGroupDesc1 As PropertyGroupDescription = TryCast(m_groupDescriptions(i), PropertyGroupDescription)
          Dim propertyGroupDesc2 As PropertyGroupDescription = TryCast(groupDescriptions(i), PropertyGroupDescription)

          If (Not propertyGroupDesc1 Is Nothing) And (Not propertyGroupDesc2 Is Nothing) Then
            If propertyGroupDesc1.PropertyName <> propertyGroupDesc2.PropertyName Then
              Return False
            End If
          Else
            Dim dataGridGroupDesc1 As DataGridGroupDescription = TryCast(m_groupDescriptions(i), DataGridGroupDescription)
            Dim dataGridGroupDesc2 As DataGridGroupDescription = TryCast(groupDescriptions(i), DataGridGroupDescription)

            If (Not dataGridGroupDesc1 Is Nothing) And (Not dataGridGroupDesc2 Is Nothing) Then
              If dataGridGroupDesc1.PropertyName <> dataGridGroupDesc2.PropertyName Then
                Return False
              End If
            Else
              Return False
            End If
          End If
        End If
      Next

      Return True
    End Function

  End Class
End Namespace
