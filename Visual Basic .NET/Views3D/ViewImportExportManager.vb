'
'* Xceed DataGrid for WPF - SAMPLE CODE - Views 3D Sample Application
'* Copyright (c) 2007-2024 Xceed Software Inc.
'*
'* [ViewImportExportManager.vb]
'*
'* This class implements various methods used to import and export view
'* settings. The import/export format is a simple ResourceDictionary saved
'* as a XAML file and containing at least one Style having its TargetType
'* set to the imported/exported View type.
'* 
'* Since the exported file structure consists of a ResourceDictionary containing 
'* an implicit Style with its TargetType set to the type of the exported
'* View, the file can be added to a project as-is and be referenced as a 
'* MergedDictionary to style all the views in the scope.
'*
'* This file is part of the Xceed DataGrid for WPF product. The use
'* and distribution of this Sample Code is subject to the terms
'* and conditions referring to "Sample Code" that are specified in
'* the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'*
'

Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Markup
Imports System.Xml
Imports Microsoft.Win32

Imports Xceed.Wpf.DataGrid.Views

Public Class ViewImportExportManager
  Private Sub New()
  End Sub

  Public Shared Sub ExportView(ByVal view As ViewBase)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    Dim saveDialog As SaveFileDialog = New SaveFileDialog()

    saveDialog.AddExtension = True
    saveDialog.CheckPathExists = True
    saveDialog.DefaultExt = "xaml"
    saveDialog.Filter = "Resource dictionary (*.xaml)|*.xaml"
    saveDialog.OverwritePrompt = True
    saveDialog.Title = "Export View Settings"
    saveDialog.ValidateNames = True

    If saveDialog.ShowDialog().GetValueOrDefault() Then
      ViewImportExportManager.ExportViewToXaml(view, saveDialog.FileName)
    End If
  End Sub

  Public Shared Sub ExportViewToXaml(ByVal view As ViewBase, ByVal fileName As String)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    Dim value As Object
    Dim dp As DependencyProperty
    Dim resourceDictionary As ResourceDictionary = New ResourceDictionary()
    Dim viewType As Type = view.GetType()
    Dim viewStyle As Style = New Style(viewType)
    Dim fieldInfo As FieldInfo

    For Each fieldInfo In viewType.GetFields(BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.Static)
      dp = TryCast(fieldInfo.GetValue(view), DependencyProperty)

      If Not dp Is Nothing Then
        value = view.ReadLocalValue(dp)

        If value IsNot DependencyProperty.UnsetValue Then
          viewStyle.Setters.Add(New Setter(dp, value))
        End If
      End If
    Next

    resourceDictionary.Add(viewType, viewStyle)

    Dim settings As XmlWriterSettings = New XmlWriterSettings()
    settings.Indent = True
    settings.OmitXmlDeclaration = True

    Dim xmlWriter As XmlWriter = xmlWriter.Create(fileName, settings)

    Try
      XamlWriter.Save(resourceDictionary, xmlWriter)
    Finally
      xmlWriter.Close()
    End Try
  End Sub

  Public Shared Sub ImportView(ByVal view As ViewBase)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    Dim openDialog As OpenFileDialog = New OpenFileDialog()

    openDialog.AddExtension = False
    openDialog.CheckFileExists = True
    openDialog.DefaultExt = "xaml"
    openDialog.Filter = "Resource dictionary (*.xaml)|*.xaml"
    openDialog.Multiselect = False
    openDialog.ShowReadOnly = False
    openDialog.Title = "Import View Settings"
    openDialog.ValidateNames = True

    If openDialog.ShowDialog().GetValueOrDefault() Then
      ViewImportExportManager.ImportViewFromXaml(view, openDialog.FileName)
    End If
  End Sub

  ' In a typical application, this way of applying a style would be undesirable. The 
  ' style would rather simply be added to a ResourceDictionary as implicit.
  ' Here, we want to set all CardflowView3D properties so that the ConfigurationPanel
  ' reflects them.
  Public Shared Sub ImportViewFromStyle(ByVal view As ViewBase, ByVal viewStyle As Style)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    ' Remove the local values on the CardflowView3D so that the newly selected preset
    ' is applied in its entirety. The properties set in a style have a priority less 
    ' than a local value (set by the various sliders and checkboxes).
    ViewImportExportManager.ClearViewPropertyValues(view)

    If Not viewStyle Is Nothing Then
      Dim setter As Setter
      For Each setter In viewStyle.Setters
        view.SetValue(setter.Property, setter.Value)
      Next
    End If
  End Sub

  Public Shared Sub ImportViewFromXaml(ByVal view As ViewBase, ByVal fileName As String)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    Dim streamReader As StreamReader = New StreamReader(fileName)

    Try
      Dim resourceDictionary As ResourceDictionary = TryCast(XamlReader.Load(streamReader.BaseStream), ResourceDictionary)

      If Not resourceDictionary Is Nothing Then
        Dim viewType As Type = view.GetType()
        ' Try extracting a implicit style for the specified view from the ResourceDictionary.
        Dim viewStyle As Style = TryCast(resourceDictionary.Item(viewType), Style)

        If viewStyle Is Nothing Then
          ' There is no implicit style for the specified view in the ResourceDictionary. 
          ' Use the first Style having the view type as TargetType.
          Dim tempStyle As Style
          Dim value As Object

          For Each value In resourceDictionary.Values
            tempStyle = TryCast(value, Style)

            If Not tempStyle Is Nothing Then
              If tempStyle.TargetType Is viewType Then
                viewStyle = tempStyle
                Exit For
              End If
            End If
          Next
        End If

        ViewImportExportManager.ImportViewFromStyle(view, viewStyle)
      End If
    Finally
      streamReader.Close()
    End Try
  End Sub

  Private Shared Sub ClearViewPropertyValues(ByVal view As ViewBase)
    If view Is Nothing Then
      Throw New ArgumentNullException("view")
    End If

    Dim dp As DependencyProperty
    Dim fieldInfo As FieldInfo

    For Each fieldInfo In view.GetType().GetFields(BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.Static)
      dp = TryCast(fieldInfo.GetValue(view), DependencyProperty)

      If Not dp Is Nothing Then
        view.ClearValue(dp)
      End If
    Next
  End Sub
End Class
