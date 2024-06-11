'
' Xceed DataGrid for WPF - SAMPLE CODE - Large Data Sets Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CustomPropertyDescriptor.vb]
'  
' Custom PropertyDescriptor that will be used to add additional properties (columns)
' on the Person objects.
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

Namespace Xceed.Wpf.DataGrid.Samples.LargeDataSets
  Friend Class CustomPropertyDescriptor
    Inherits PropertyDescriptor

    Public Sub New(ByVal propertyName As String, ByVal componentType As Type, ByVal propertyType As Type, ByVal isReadOnly As Boolean)
      MyBase.New(propertyName, Nothing)
      m_propertyName = propertyName
      m_componentType = componentType
      m_propertyType = propertyType
      m_readOnly = isReadOnly
    End Sub

    Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
      Return False
    End Function

    Public Overrides ReadOnly Property ComponentType() As Type
      Get
        Return m_componentType
      End Get
    End Property

    Public Overrides Function GetValue(ByVal component As Object) As Object
      Return RandomValueGenerator.GetRandomValue(m_propertyType)
    End Function

    Public Overrides ReadOnly Property IsReadOnly() As Boolean
      Get
        Return m_readOnly
      End Get
    End Property

    Public Overrides ReadOnly Property PropertyType() As Type
      Get
        Return m_propertyType
      End Get
    End Property

    Public Overrides Sub ResetValue(ByVal component As Object)
      Throw New NotImplementedException()
    End Sub

    Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
    End Sub

    Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
      Return False
    End Function

    Private m_propertyName As String
    Private m_componentType As Type
    Private m_propertyType As Type
    Private m_readOnly As Boolean
  End Class
End Namespace
