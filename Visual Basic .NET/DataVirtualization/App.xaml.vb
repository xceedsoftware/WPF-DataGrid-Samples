' 
' * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application 
' * Copyright (c) 2007-2024 Xceed Software Inc. 
' * 
' * [App.xaml.vb] 
' * 
' * This class implements the startup logic of the DataVirtualization Sample. 
' * 
' * This file is part of the Xceed DataGrid for WPF product. The use 
' * and distribution of this Sample Code is subject to the terms 
' * and conditions referring to "Sample Code" that are specified in 
' * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product. 
' 


Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports System.Xml.Serialization
Imports System.Xml
Imports System.IO

Namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
  ''' <summary> 
  ''' Interaction logic for App.xaml 
  ''' </summary> 
  Partial Public Class App
    Inherits System.Windows.Application

    Private Const ConfigFilename As String = "DatabaseConfig.xml"

    Protected Overloads Overrides Sub OnStartup(ByVal e As StartupEventArgs)
      XceedDeploymentLicense.SetLicense()

      MyBase.OnStartup(e)
    End Sub


    Friend Shared Function ReadDatabaseInfo() As DatabaseInfo
      Dim connectionInfo As DatabaseInfo = Nothing

      If File.Exists(App.ConfigFilename) Then
        Try
          Using xmlReader__1 As XmlReader = XmlReader.Create(App.ConfigFilename)
            Dim xmlSerializer As New XmlSerializer(GetType(DatabaseInfo))
            connectionInfo = TryCast(xmlSerializer.Deserialize(xmlReader__1), DatabaseInfo)
          End Using
        Catch
        End Try
      End If

      Return connectionInfo
    End Function

    Friend Shared Sub WriteDatabaseInfo(ByVal databaseInfo As DatabaseInfo)
      If databaseInfo Is Nothing Then
        Throw New ArgumentNullException("databaseInfo")
      End If

      Try
        Using xmlWriter__1 As XmlWriter = XmlWriter.Create(App.ConfigFilename)
          Dim xmlSerializer As New XmlSerializer(GetType(DatabaseInfo))
          xmlSerializer.Serialize(xmlWriter__1, databaseInfo)
        End Using
      Catch
      End Try
    End Sub
  End Class
End Namespace
