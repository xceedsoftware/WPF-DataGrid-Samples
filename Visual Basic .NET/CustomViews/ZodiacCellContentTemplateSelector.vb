'
' Xceed DataGrid for WPF - SAMPLE CODE - Custom Views Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [ZodiacCellContentTemplateSelector.vb]
'  
' This class a DataTemplateSelector that displays a date in its "Short" representation
' and its associated zodiac symbol.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.CustomViews
  Public Class ZodiacCellContentTemplateSelector
    Inherits DataTemplateSelector

    Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
      Dim dataTemplate As New DataTemplate()
      Dim grid As New FrameworkElementFactory(GetType(Grid))
      Dim viewbox As New FrameworkElementFactory(GetType(Viewbox))
      Dim zodiacSymbol As New FrameworkElementFactory(GetType(TextBlock))
      Dim textBlock As New FrameworkElementFactory(GetType(TextBlock))

      Dim symbol As Char

      If IsDate(item) = True Then
        symbol = Microsoft.VisualBasic.ChrW(FirstZodiacSymbolCode + Me.GetZodiacSign(CDate(item)))
        textBlock.SetValue(System.Windows.Controls.TextBlock.TextProperty, CDate(item).ToShortDateString())
      Else
        symbol = "?"c
        textBlock.SetValue(System.Windows.Controls.TextBlock.TextProperty, "unknown")
      End If

      zodiacSymbol.SetValue(System.Windows.Controls.TextBlock.FontFamilyProperty, New FontFamily("Wingdings"))
      zodiacSymbol.SetValue(System.Windows.Controls.TextBlock.OpacityProperty, 0.3)
      zodiacSymbol.SetValue(System.Windows.Controls.TextBlock.TextProperty, symbol.ToString())
      zodiacSymbol.SetValue(System.Windows.Controls.TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)
      zodiacSymbol.SetValue(System.Windows.Controls.TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)

      viewbox.AppendChild(zodiacSymbol)
      ' We don't want the zodiac sign to be too big.
      viewbox.SetValue(System.Windows.Controls.Viewbox.MaxHeightProperty, 60.0)
      grid.AppendChild(viewbox)
      grid.AppendChild(textBlock)

      dataTemplate.VisualTree = grid

      Return dataTemplate
    End Function

    Private Function GetZodiacSign(ByVal dateTime As DateTime) As Integer
      '   Start        End
      ' Month  Day   Month   Day
      Dim zodiacRanges(,) As Byte = New Byte(11, 3) { _
      {3, 21, 4, 20}, _
      {4, 21, 5, 21}, _
      {5, 22, 6, 21}, _
      {6, 22, 7, 23}, _
      {7, 24, 8, 23}, _
      {8, 24, 9, 23}, _
      {9, 24, 10, 23}, _
      {10, 24, 11, 22}, _
      {11, 23, 12, 21}, _
      {12, 22, 1, 20}, _
      {1, 21, 2, 18}, _
      {2, 19, 3, 20}}


      Dim i As Integer
      For i = 0 To 12 - 1
        If (((dateTime.Month = zodiacRanges(i, 0)) AndAlso (dateTime.Day >= zodiacRanges(i, 1))) OrElse _
    ((dateTime.Month = zodiacRanges(i, 2)) AndAlso (dateTime.Day <= zodiacRanges(i, 3)))) Then
          Return i
        End If
      Next i

      Return 0
    End Function

    Private Shared ReadOnly FirstZodiacSymbolCode As Integer = 94
  End Class
End Namespace
