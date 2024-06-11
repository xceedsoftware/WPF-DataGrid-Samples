'
' * Xceed DataGrid for WPF - SAMPLE CODE - MVVM Sample Application
' * Copyright (c) 2007-2024 Xceed Software Inc.
' * 
' * [CountIfFunction.vb]
' *  
' * This class implements a custom statistic used for the Discontinued column.  It only counts items that have a specific boolean value.
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
' *
' 

Imports System
Imports Xceed.Wpf.DataGrid.Stats

Namespace Xceed.Wpf.DataGrid.Samples.MVVM.ViewModel
  Public Class CountIfFunction
    Inherits CumulativeStatFunction
    Public Sub New()
      MyBase.New()
    End Sub

#Region "ValueToCountIsTrue Property"

    Public Property ValueToCountIsTrue() As Boolean
      Get
        Return m_valueToCountIsTrue
      End Get
      Set(ByVal value As Boolean)
        Me.CheckSealed()
        m_valueToCountIsTrue = value
      End Set
    End Property

    Private m_valueToCountIsTrue As Boolean = False

#End Region

    Protected Overrides Sub InitializeFromInstance(ByVal source As StatFunction)
      MyBase.InitializeFromInstance(source)

      'This determines if true values are counted, or false values.
      Me.ValueToCountIsTrue = (CType(source, CountIfFunction)).ValueToCountIsTrue
    End Sub

    Protected Overrides Sub Reset()
      m_count = 0
    End Sub

    Protected Overrides Sub Accumulate(ByVal values() As Object)
      If values.Length > 0 Then
        Dim value = Convert.ToBoolean(values(0))

        If (Me.ValueToCountIsTrue AndAlso value) OrElse ((Not Me.ValueToCountIsTrue) AndAlso (Not value)) Then
          m_count += 1
        End If
      End If
    End Sub

    Protected Overrides Sub AccumulateChildResult(ByVal childResult As StatResult)
      m_count += Convert.ToInt64(childResult.Value)
    End Sub

    Protected Overrides Function GetResult() As StatResult
      Return New StatResult(m_count)
    End Function

    Protected Overrides Function IsEquivalent(ByVal statFunction As StatFunction) As Boolean
      Dim countIfFunction = TryCast(statFunction, CountIfFunction)

      Return (MyBase.IsEquivalent(countIfFunction)) AndAlso (countIfFunction.ValueToCountIsTrue = Me.ValueToCountIsTrue)
    End Function

    Private m_count As Long
  End Class
End Namespace