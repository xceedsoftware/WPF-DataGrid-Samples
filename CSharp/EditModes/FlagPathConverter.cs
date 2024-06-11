/*
 * Xceed DataGrid for WPF - SAMPLE CODE - EditModes Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [FlagPathConverter.cs]
 *  
 * This class is used to convert a country name to a path to the corresponding
 * embedded resource and returns the resource
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections;

namespace Xceed.Wpf.DataGrid.Samples.EditModes
{
  [ValueConversion( typeof( object ), typeof( BitmapSource ) )]
  public class FlagPathConverter : IValueConverter
  {
    private const string FlagsManifestResourcePath = "Xceed.Wpf.DataGrid.Samples.EditModes.Flags.";
    private const string NotFoundFlagFileName = "notfound.png";
    private const string NotFoundFlagManifestResourcePath = FlagPathConverter.FlagsManifestResourcePath + FlagPathConverter.NotFoundFlagFileName;

    public FlagPathConverter()
    {
      m_flagsHashtable = new Hashtable();
    }

    public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      if( String.IsNullOrEmpty( value as string ) )
        return null;

      string flagFileName = ( String.IsNullOrEmpty( value as string ) ) ? FlagPathConverter.NotFoundFlagFileName : value.ToString().ToLower() + ".png";

      System.IO.Stream fileStream = this.GetType().Assembly.GetManifestResourceStream( FlagPathConverter.FlagsManifestResourcePath + flagFileName );

      if( ( fileStream == null ) && ( flagFileName != FlagPathConverter.NotFoundFlagFileName ) )
      {
        flagFileName = FlagPathConverter.NotFoundFlagFileName;
        fileStream = this.GetType().Assembly.GetManifestResourceStream( FlagPathConverter.NotFoundFlagManifestResourcePath );
      }

      if( fileStream != null )
      {
        BitmapFrame bitmapFrame = null;
        if( !m_flagsHashtable.Contains( flagFileName ) )
        {
          PngBitmapDecoder bitmapDecoder = new PngBitmapDecoder( fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default );
          bitmapFrame = bitmapDecoder.Frames[ 0 ];
          m_flagsHashtable.Add( flagFileName, bitmapFrame );
        }
        else
        {
          bitmapFrame = ( BitmapFrame )m_flagsHashtable[ flagFileName ];
        }
        return bitmapFrame;
      }
      else
      {
        System.Diagnostics.Debug.WriteLine( "Unable to get resource: " + flagFileName );
        return new BitmapImage();
      }
    }

    public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
    {
      return Binding.DoNothing;
    }

    private static Hashtable m_flagsHashtable;
  }
}