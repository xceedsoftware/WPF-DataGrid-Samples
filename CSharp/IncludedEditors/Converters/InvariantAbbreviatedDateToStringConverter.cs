using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
{
  public class InvariantAbbreviatedDateToStringConverter : IValueConverter
  {
    #region IValueConverter Members

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Performance", "CA1800:DoNotCastUnnecessarily" )]
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( string ) ) )
        return DependencyProperty.UnsetValue;

      // Return string.Empty if the value is null.
      if( value == null )
        return string.Empty;

      // Return string.Empty if the value is an empty string.
      if( ( value is string ) && ( string.IsNullOrEmpty( value as string ) ) )
        return string.Empty;

      if( !( value is DateTime ) )
        return DependencyProperty.UnsetValue;

      DateTime dateTime = ( DateTime )value;

      return dateTime.ToString( "dd/MMM/yyyy", CultureInfo.InvariantCulture );
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( !targetType.IsAssignableFrom( typeof( DateTime ) ) )
        return DependencyProperty.UnsetValue;

      string text = value as string;

      if( string.IsNullOrEmpty( text ) )
        return null;

      DateTime dateTime;
      if( DateTime.TryParseExact( text, "dd/MMM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime ) )
        return dateTime;

      return null;
    }

    #endregion
  }
}
