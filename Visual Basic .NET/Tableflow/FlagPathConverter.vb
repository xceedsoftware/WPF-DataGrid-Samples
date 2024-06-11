'
' Xceed DataGrid for WPF - SAMPLE CODE - Tableflow View Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
'
' [FlagPathConverter.vb]
'
' This class is used to convert a country name to a path that corresponds
' to an embedded resource and returns the resource as an image.
'
' This file is part of the Xceed DataGrid for WPF product. The use
' and distribution of this Sample Code is subject to the terms
' and conditions referring to "Sample Code" that are specified in
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'
'

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports System.Windows
Imports System.Collections

Namespace Xceed.Wpf.DataGrid.Samples.Tableflow
  <ValueConversion(GetType(Object), GetType(BitmapSource))> _
  Public Class FlagPathConverter
    Implements IValueConverter

    Private Const NotFoundFlagFileName As String = "notfound.png"

    Public Sub New()
      m_flagsHashtable = New Hashtable()
    End Sub

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
      If [String].IsNullOrEmpty(TryCast(value, String)) Then
        Return Nothing
      End If

      Dim flagFileName As String = If(([String].IsNullOrEmpty(TryCast(value, String))), FlagPathConverter.NotFoundFlagFileName, value.ToString().ToLower() & ".png")

      Dim fileStream As System.IO.Stream = Me.[GetType]().Assembly.GetManifestResourceStream(flagFileName)

      If (fileStream Is Nothing) AndAlso (flagFileName <> FlagPathConverter.NotFoundFlagFileName) Then
        flagFileName = FlagPathConverter.NotFoundFlagFileName
        fileStream = Me.[GetType]().Assembly.GetManifestResourceStream(NotFoundFlagFileName)
      End If

      If fileStream IsNot Nothing Then
        Dim bitmapFrame As BitmapFrame = Nothing
        If Not m_flagsHashtable.Contains(flagFileName) Then
          Dim bitmapDecoder As New PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.[Default])
          bitmapFrame = bitmapDecoder.Frames(0)
          m_flagsHashtable.Add(flagFileName, bitmapFrame)
        Else
          bitmapFrame = DirectCast(m_flagsHashtable(flagFileName), BitmapFrame)
        End If
        Return bitmapFrame
      Else
        System.Diagnostics.Debug.WriteLine("Unable to get resource: " & flagFileName)
        Return New BitmapImage()
      End If
    End Function


    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack

      Throw New NotSupportedException()
    End Function

    Private Shared m_flagsHashtable As Hashtable
  End Class
End Namespace
