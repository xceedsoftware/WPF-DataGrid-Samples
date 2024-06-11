/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Spanned Cells Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [CustomSpannedCellConfigurationSelector.cs]
 *  
 * This class implements the business object that customizes the spanned cells.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace Xceed.Wpf.DataGrid.Samples.SpannedCells
{
  public class CustomSpannedCellConfigurationSelector : SpannedCellConfigurationSelector
  {
    #region Static Fields

    private static readonly DataTemplate s_countryCityTemplate;
    private static readonly DataTemplate s_dateTimeTemplate;

    #endregion

    static CustomSpannedCellConfigurationSelector()
    {
      s_countryCityTemplate = CustomSpannedCellConfigurationSelector.CreateCountryCityTemplate();
      s_countryCityTemplate.Seal();

      s_dateTimeTemplate = CustomSpannedCellConfigurationSelector.CreateDateTimeTemplate();
      s_dateTimeTemplate.Seal();
    }

    public CustomSpannedCellConfigurationSelector( ConfigurationData configuration )
    {
      if( configuration == null )
        throw new ArgumentNullException( "configuration" );

      m_configuration = configuration;
    }

    public override ISpannedCellConfiguration SelectConfiguration( object content, IEnumerable<SpannedCellFragment> fragments )
    {
      var configuration = new CustomSpannedCellConfiguration( m_configuration );

      if( content is CountryCityData )
      {
        configuration.ContentTemplate = s_countryCityTemplate;
      }
      else if( content is DateTime )
      {
        configuration.ContentTemplate = s_dateTimeTemplate;
      }
      else
      {
        var templates = fragments.Select( item => item.Cell.CoercedContentTemplate ).Distinct().ToList();
        if( templates.Count == 1 )
        {
          configuration.ContentTemplate = templates.Single();
        }
      }

      return configuration;
    }

    private static DataTemplate CreateCountryCityTemplate()
    {
      return XamlReader.Parse(
        @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
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
          </DataTemplate>" ) as DataTemplate;
    }

    private static DataTemplate CreateDateTimeTemplate()
    {
      var dataGridControlType = typeof( DataGridControl );
      var assemblyName = dataGridControlType.Assembly.GetName().Name;

      var context = new ParserContext();
      var typeMapper = new XamlTypeMapper(
                         new string[] { assemblyName },
                         new NamespaceMapEntry[] { new NamespaceMapEntry( "xcdg", assemblyName, dataGridControlType.Namespace ) } );

      context.XamlTypeMapper = typeMapper;

      return XamlReader.Parse(
        @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                        xmlns:xcdg=""http://schemas.xceed.com/wpf/xaml/datagrid"">
            <DataTemplate.Resources>
              <xcdg:DateTimeToStringConverter x:Key=""dateTimeToStringConverter"" />
            </DataTemplate.Resources>

            <TextBlock Text=""{Binding Converter={StaticResource dateTimeToStringConverter}, ConverterParameter='ShortDate'}"" />
          </DataTemplate>", context ) as DataTemplate;
    }

    private readonly ConfigurationData m_configuration;

    #region CustomSpannedCellConfiguration Private Class

    private sealed class CustomSpannedCellConfiguration : SpannedCellConfiguration, IWeakEventListener
    {
      internal CustomSpannedCellConfiguration( ConfigurationData configuration )
      {
        m_configuration = configuration;

        PropertyChangedEventManager.AddListener( configuration, this, string.Empty );

        this.HorizontalContentAlignment = configuration.HorizontalContentAlignment;
        this.VerticalContentAlignment = configuration.VerticalContentAlignment;
      }

      bool IWeakEventListener.ReceiveWeakEvent( Type managerType, object sender, EventArgs e )
      {
        if( typeof( PropertyChangedEventManager ) == managerType )
        {
          if( sender == m_configuration )
          {
            var propertyName = ( ( PropertyChangedEventArgs )e ).PropertyName;

            if( string.IsNullOrEmpty( propertyName ) || ( propertyName == "HorizontalContentAlignment" ) )
            {
              this.HorizontalContentAlignment = m_configuration.HorizontalContentAlignment;
            }

            if( string.IsNullOrEmpty( propertyName ) || ( propertyName == "VerticalContentAlignment" ) )
            {
              this.VerticalContentAlignment = m_configuration.VerticalContentAlignment;
            }
          }
        }
        else
        {
          return false;
        }

        return true;
      }

      private readonly ConfigurationData m_configuration;
    }

    #endregion
  }
}
