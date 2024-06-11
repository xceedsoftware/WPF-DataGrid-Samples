'
'* Xceed DataGrid for WPF - SAMPLE CODE - Tableflow™ View Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
 '*
 '* [FilterDatePicker.xaml.cs]
 '*
 '* This class implements the various properties offered by the date range
 '* picker used in some FilterCells.
 '*
 '* This file is part of the Xceed DataGrid for WPF product. The use
 '* and distribution of this Sample Code is subject to the terms
 '* and conditions referring to "Sample Code" that are specified in
 '* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 '*
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls

Imports Xceed.Wpf.DataGrid.FilterCriteria

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  Public Partial Class FilterDatePicker
	  Inherits UserControl
	Public Sub New()
	  InitializeComponent()
	End Sub

	#Region "FilterCriterion Property"

    Public Shared ReadOnly FilterCriterionProperty As DependencyProperty = DependencyProperty.Register("FilterCriterion", GetType(FilterCriterion), GetType(FilterDatePicker), New FrameworkPropertyMetadata(Nothing, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, New PropertyChangedCallback(AddressOf FilterDatePicker.OnFilterCriterionChanged)))

	Public Property FilterCriterion() As FilterCriteria.FilterCriterion
	  Get
		Return CType(Me.GetValue(FilterDatePicker.FilterCriterionProperty), FilterCriteria.FilterCriterion)
	  End Get
	  Set
		Me.SetValue(FilterDatePicker.FilterCriterionProperty, Value)
	  End Set
	End Property

	' When the FilterCriterion is modified from outside this editor (with an initial
	' FilterCriterion on a DataGridItemProperty for instance), try our best to reflect
	' it in the StartDate and EndDate properties.
	Private Shared Sub OnFilterCriterionChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
	  Dim filterDatePicker As FilterDatePicker = CType(sender, FilterDatePicker)

	  If filterDatePicker.m_updating Then
		Return
	  End If

	  filterDatePicker.m_updating = True

	  Try
		Dim greaterThanOrEqualFilterCriterion As GreaterThanOrEqualToFilterCriterion = Nothing
		Dim lessThanOrEqualToFilterCriterion As LessThanOrEqualToFilterCriterion = Nothing
		Dim andFilterCriterion As AndFilterCriterion = TryCast(e.NewValue, AndFilterCriterion)

		If andFilterCriterion Is Nothing Then
		  greaterThanOrEqualFilterCriterion = TryCast(e.NewValue, GreaterThanOrEqualToFilterCriterion)
		  lessThanOrEqualToFilterCriterion = TryCast(e.NewValue, LessThanOrEqualToFilterCriterion)
		Else
		  ' Extract the StartDate and EndDate from this "AndFilterCriterion", if possible.
		  greaterThanOrEqualFilterCriterion = TryCast(andFilterCriterion.FirstFilterCriterion, GreaterThanOrEqualToFilterCriterion)
		  lessThanOrEqualToFilterCriterion = TryCast(andFilterCriterion.SecondFilterCriterion, LessThanOrEqualToFilterCriterion)

		  If greaterThanOrEqualFilterCriterion Is Nothing Then
			greaterThanOrEqualFilterCriterion = TryCast(andFilterCriterion.SecondFilterCriterion, GreaterThanOrEqualToFilterCriterion)
		  End If

		  If lessThanOrEqualToFilterCriterion Is Nothing Then
			lessThanOrEqualToFilterCriterion = TryCast(andFilterCriterion.FirstFilterCriterion, LessThanOrEqualToFilterCriterion)
		  End If
		End If

		If greaterThanOrEqualFilterCriterion Is Nothing Then
		  filterDatePicker.StartDate = DateTime.MinValue
		Else
		  filterDatePicker.StartDate = CDate(greaterThanOrEqualFilterCriterion.Value)
		End If

		If lessThanOrEqualToFilterCriterion Is Nothing Then
		  filterDatePicker.EndDate = DateTime.MaxValue
		Else
		  filterDatePicker.EndDate = CDate(lessThanOrEqualToFilterCriterion.Value)
		End If
	  Finally
		filterDatePicker.m_updating = False
	  End Try
	End Sub

	#End Region

	#Region "StartDate Property"

    Public Shared ReadOnly StartDateProperty As DependencyProperty = DependencyProperty.Register("StartDate", GetType(DateTime), GetType(FilterDatePicker), New FrameworkPropertyMetadata(DateTime.MinValue, New PropertyChangedCallback(AddressOf FilterDatePicker.OnStartDateChanged)))

	Public Property StartDate() As DateTime
	  Get
		Return CDate(Me.GetValue(FilterDatePicker.StartDateProperty))
	  End Get
	  Set
		Me.SetValue(FilterDatePicker.StartDateProperty, Value)
	  End Set
	End Property

	Private Shared Sub OnStartDateChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
	  CType(sender, FilterDatePicker).UpdateFilterCriterion()
	End Sub

	#End Region

	#Region "EndDate Property"

    Public Shared ReadOnly EndDateProperty As DependencyProperty = DependencyProperty.Register("EndDate", GetType(DateTime), GetType(FilterDatePicker), New FrameworkPropertyMetadata(DateTime.MaxValue, New PropertyChangedCallback(AddressOf FilterDatePicker.OnEndDateChanged)))

	Public Property EndDate() As DateTime
	  Get
		Return CDate(Me.GetValue(FilterDatePicker.EndDateProperty))
	  End Get
	  Set
		Me.SetValue(FilterDatePicker.EndDateProperty, Value)
	  End Set
	End Property

	Private Shared Sub OnEndDateChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
	  CType(sender, FilterDatePicker).UpdateFilterCriterion()
	End Sub

	#End Region

	Private Sub UpdateFilterCriterion()
	  If m_updating Then
		Return
	  End If

	  m_updating = True

	  Try
		If Me.StartDate > DateTime.MinValue Then
		  If Me.EndDate < DateTime.MaxValue Then
			Me.FilterCriterion = New AndFilterCriterion(New GreaterThanOrEqualToFilterCriterion(Me.StartDate), New LessThanOrEqualToFilterCriterion(New DateTime(Me.EndDate.Year, Me.EndDate.Month, Me.EndDate.Day, 23, 59, 59)))
		  Else
			Me.FilterCriterion = New GreaterThanOrEqualToFilterCriterion(Me.StartDate)
		  End If
		ElseIf Me.EndDate < DateTime.MaxValue Then
		  Me.FilterCriterion = New LessThanOrEqualToFilterCriterion(New DateTime(Me.EndDate.Year, Me.EndDate.Month, Me.EndDate.Day, 23, 59, 59))
		End If
	  Finally
		m_updating = False
	  End Try
	End Sub

	Private m_updating As Boolean = False
  End Class
End Namespace
