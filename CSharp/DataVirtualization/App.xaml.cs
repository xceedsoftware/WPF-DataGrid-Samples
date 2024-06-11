/*
 * Xceed DataGrid for WPF - SAMPLE CODE - DataVirtualization Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [App.xaml.cs]
 *  
 * This class implements the startup logic of the DataVirtualization Sample.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Xceed.Wpf.DataGrid.Samples.DataVirtualization
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private const string ConfigFilename = "DatabaseConfig.xml";

    protected override void OnStartup( StartupEventArgs e )
    {
      XceedDeploymentLicense.SetLicense();

      base.OnStartup( e );
    }


    internal static DatabaseInfo ReadDatabaseInfo()
    {
      DatabaseInfo connectionInfo = null;

      if( File.Exists( App.ConfigFilename ) )
      {
        try
        {
          using( XmlReader xmlReader = XmlReader.Create( App.ConfigFilename ) )
          {
            XmlSerializer xmlSerializer = new XmlSerializer( typeof( DatabaseInfo ) );
            connectionInfo = xmlSerializer.Deserialize( xmlReader ) as DatabaseInfo;
          }
        }
        catch
        { 
        }
      }

      return connectionInfo;
    }

    internal static void WriteDatabaseInfo( DatabaseInfo databaseInfo )
    {
      if( databaseInfo == null )
        throw new ArgumentNullException( "databaseInfo" );

      try
      {
        using( XmlWriter xmlWriter = XmlWriter.Create( App.ConfigFilename ) )
        {
          XmlSerializer xmlSerializer = new XmlSerializer( typeof( DatabaseInfo ) );
          xmlSerializer.Serialize( xmlWriter, databaseInfo );
        }
      }
      catch
      {
      }
    }

  }
}