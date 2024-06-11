/*
 * Xceed DataGrid for WPF - SAMPLE CODE - Exporting Sample Application
 * Copyright (c) 2007-2024 Xceed Software Inc.
 * 
 * [ConfigurationData.cs]
 *  
 * This class implements the business object containing various dynamic configuration 
 * options offered by the configuration panel.
 * 
 * This file is part of the Xceed DataGrid for WPF product. The use 
 * and distribution of this Sample Code is subject to the terms 
 * and conditions referring to "Sample Code" that are specified in 
 * the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
 *
 */

using System.ComponentModel;

namespace Xceed.Wpf.DataGrid.Samples.Exporting
{
  public class ConfigurationData : INotifyPropertyChanged
  {
    public static readonly ConfigurationData Singleton = new ConfigurationData();

    private ConfigurationData()
    {
    }

    // Common export settings

    #region DetailDepth Property

    public int DetailDepth
    {
      get
      {
        return m_detailDepth;
      }

      set
      {
        if( value != m_detailDepth )
        {
          m_detailDepth = value;
          this.RaisePropertyChanged( "DetailDepth" );
        }
      }
    }

    private int m_detailDepth;

    #endregion DetailDepth Property

    #region IncludeColumnHeaders Property

    public bool IncludeColumnHeaders
    {
      get
      {
        return m_includeColumnHeaders;
      }

      set
      {
        if( value != m_includeColumnHeaders )
        {
          m_includeColumnHeaders = value;
          this.RaisePropertyChanged( "IncludeColumnHeaders" );
        }
      }
    }

    private bool m_includeColumnHeaders = true;

    #endregion IncludeColumnHeaders Property

    #region RepeatParentData Property

    public bool RepeatParentData
    {
      get
      {
        return m_repeatParentData;
      }

      set
      {
        if( value != m_repeatParentData )
        {
          m_repeatParentData = value;
          this.RaisePropertyChanged( "RepeatParentData" );
        }
      }
    }

    private bool m_repeatParentData;

    #endregion RepeatParentData Property

    #region UseFieldNamesInHeader Property

    public bool UseFieldNamesInHeader
    {
      get
      {
        return m_useFieldNamesInHeader;
      }

      set
      {
        if( value != m_useFieldNamesInHeader )
        {
          m_useFieldNamesInHeader = value;
          this.RaisePropertyChanged( "UseFieldNamesInHeader" );
        }
      }
    }

    private bool m_useFieldNamesInHeader;

    #endregion UseFieldNamesInHeader Property

    // Excel export settings

    #region ExportStatFunctionsAsFormulas Property

    public bool ExportStatFunctionsAsFormulas
    {
      get
      {
        return m_exportStatFunctionsAsFormulas;
      }

      set
      {
        if( value != m_exportStatFunctionsAsFormulas )
        {
          m_exportStatFunctionsAsFormulas = value;
          this.RaisePropertyChanged( "ExportStatFunctionsAsFormulas" );
        }
      }
    }

    private bool m_exportStatFunctionsAsFormulas;

    #endregion ExportStatFunctionsAsFormulas Property

    #region IsHeaderFixed Property

    public bool IsHeaderFixed
    {
      get
      {
        return m_isHeaderFixed;
      }

      set
      {
        if( value != m_isHeaderFixed )
        {
          m_isHeaderFixed = value;
          this.RaisePropertyChanged( "IsHeaderFixed" );
        }
      }
    }

    private bool m_isHeaderFixed = true;

    #endregion IsHeaderFixed Property

    #region StatFunctionDepth Property

    public int StatFunctionDepth
    {
      get
      {
        return m_statFunctionDepth;
      }

      set
      {
        if( value != m_statFunctionDepth )
        {
          m_statFunctionDepth = value;
          this.RaisePropertyChanged( "StatFunctionDepth" );
        }
      }
    }

    private int m_statFunctionDepth;

    #endregion StatFunctionDepth Property

    // CSV export settings

    #region DateTimeFormat Property

    public string DateTimeFormat
    {
      get
      {
        return m_dateTimeFormat;
      }

      set
      {
        if( value != m_dateTimeFormat )
        {
          m_dateTimeFormat = value;
          this.RaisePropertyChanged( "DateTimeFormat" );
        }
      }
    }

    private string m_dateTimeFormat;

    #endregion DateTimeFormat Property

    #region NumberFormat Property

    public string NumberFormat
    {
      get
      {
        return m_numberFormat;
      }

      set
      {
        if( value != m_numberFormat )
        {
          m_numberFormat = value;
          this.RaisePropertyChanged( "NumberFormat" );
        }
      }
    }

    private string m_numberFormat;

    #endregion NumberFormat Property

    #region Separator Property

    public char Separator
    {
      get
      {
        return m_separator;
      }

      set
      {
        if( value != m_separator )
        {
          m_separator = value;
          this.RaisePropertyChanged( "Separator" );
        }
      }
    }

    private char m_separator;

    #endregion Separator Property

    #region TextQualifier Property

    public char TextQualifier
    {
      get
      {
        return m_textQualifier;
      }

      set
      {
        if( value != m_textQualifier )
        {
          m_textQualifier = value;
          this.RaisePropertyChanged( "TextQualifier" );
        }
      }
    }

    private char m_textQualifier;

    #endregion TextQualifier Property

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged( string name )
    {
      if( this.PropertyChanged != null )
      {
        this.PropertyChanged( this, new PropertyChangedEventArgs( name ) );
      }
    }

    #endregion
  }
}
