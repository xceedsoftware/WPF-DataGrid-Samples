'
' Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
' Copyright (c) 2007-2024 Xceed Software Inc.
' 
' [CustomSpannedCellConfigurationSelector.vb]
'
' This class implements the business object that customizes the spanned cells.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Markup

Namespace Xceed.Wpf.DataGrid.Samples.SpannedCells

  Public Class CustomSpannedCellConfigurationSelector
    Inherits SpannedCellConfigurationSelector

    Shared Sub New()
      s_countryCityTemplate = CustomSpannedCellConfigurationSelector.CreateCountryCityTemplate()
      s_countryCityTemplate.Seal()

      s_dateTimeTemplate = CustomSpannedCellConfigurationSelector.CreateDateTimeTemplate()
      s_dateTimeTemplate.Seal()
    End Sub

    Public Sub New(configuration As ConfigurationData)
      If (configuration Is Nothing) Then
        Throw New ArgumentNullException("configuration")
      End If

      m_configuration = configuration
    End Sub

    Public Overrides Function SelectConfiguration(content As Object, fragments As IEnumerable(Of SpannedCellFragment)) As ISpannedCellConfiguration
      Dim configuration = New CustomSpannedCellConfiguration(m_configuration)

      If (TypeOf content Is CountryCityData) Then
        configuration.ContentTemplate = s_countryCityTemplate
      ElseIf (TypeOf content Is DateTime) Then
        configuration.ContentTemplate = s_dateTimeTemplate
      Else
        Dim templates = fragments.Select(Function(item) item.Cell.CoercedContentTemplate).Distinct().ToList()

        If (templates.Count = 1) Then
          configuration.ContentTemplate = templates.Single()
        End If
      End If

      Return configuration
    End Function

    Private Shared Function CreateCountryCityTemplate() As DataTemplate
      Return TryCast(XamlReader.Parse(
                    "<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                 <StackPanel Orientation=""Horizontal"">
                   <TextBlock Text=""{Binding City}"" />

                   <Grid Margin=""5,0,0,0"">
                     <Grid.ColumnDefinitions>
                       <ColumnDefinition Width=""Auto"" />
                       <ColumnDefinition Width=""*"" />
                       <ColumnDefinition Width=""Auto"" />
                     </Grid.ColumnDefinitions>

                     <TextBlock Grid.Column=""0""
                                Text=""("" />

                     <TextBlock Grid.Column=""1""
                                FontStyle=""Italic""
                                Text=""{Binding Country}"" />

                     <TextBlock Grid.Column=""2""
                                Text="")"" />
                   </Grid>
                 </StackPanel>
              </DataTemplate>"), DataTemplate)
    End Function

    Private Shared Function CreateDateTimeTemplate() As DataTemplate
      Dim dataGridControlType = GetType(DataGridControl)
      Dim assemblyName = dataGridControlType.Assembly.GetName().Name

      Dim context = New ParserContext()
      Dim typeMapper = New XamlTypeMapper(
                                   New String() {assemblyName},
                                   New NamespaceMapEntry() {New NamespaceMapEntry("xcdg", assemblyName, dataGridControlType.Namespace)})

      context.XamlTypeMapper = typeMapper

      Return TryCast(XamlReader.Parse(
                  "<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                           xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                           xmlns:xcdg=""http://schemas.xceed.com/wpf/xaml/datagrid"">
               <DataTemplate.Resources>
                 <xcdg:DateTimeToStringConverter x:Key=""dateTimeToStringConverter"" />
               </DataTemplate.Resources>

               <TextBlock Text=""{Binding Converter={StaticResource dateTimeToStringConverter}, ConverterParameter='ShortDate'}"" />
             </DataTemplate>", context), DataTemplate)
    End Function

    Private Shared ReadOnly s_countryCityTemplate As DataTemplate
    Private Shared ReadOnly s_dateTimeTemplate As DataTemplate
    Private ReadOnly m_configuration As ConfigurationData

    Private Class CustomSpannedCellConfiguration
      Inherits SpannedCellConfiguration
      Implements IWeakEventListener

      Friend Sub New(ByVal configuration As ConfigurationData)
        m_configuration = configuration

        PropertyChangedEventManager.AddListener(configuration, Me, String.Empty)

        Me.HorizontalContentAlignment = configuration.HorizontalContentAlignment
        Me.VerticalContentAlignment = configuration.VerticalContentAlignment
      End Sub

      Public Function ReceiveWeakEvent(managerType As Type, sender As Object, e As EventArgs) As Boolean Implements IWeakEventListener.ReceiveWeakEvent
        If (GetType(PropertyChangedEventManager) = managerType) Then
          If Object.Equals(sender, m_configuration) Then
            Dim propertyName = DirectCast(e, PropertyChangedEventArgs).PropertyName

            If (String.IsNullOrEmpty(propertyName) OrElse (propertyName = "HorizontalContentAlignment")) Then
              Me.HorizontalContentAlignment = m_configuration.HorizontalContentAlignment
            End If

            If (String.IsNullOrEmpty(propertyName) OrElse (propertyName = "VerticalContentAlignment")) Then
              Me.VerticalContentAlignment = m_configuration.VerticalContentAlignment
            End If
          End If
        Else
          Return False
        End If

        Return True
      End Function

      Private ReadOnly m_configuration As ConfigurationData
    End Class
  End Class
End Namespace