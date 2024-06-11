Imports System
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Data
Imports Xceed.Wpf.Controls

Namespace Xceed.Wpf.DataGrid.Samples.IncludedEditors
  Public Class InvariantAbbreviatedDateToStringConverter
    Implements IValueConverter
#Region "IValueConverter Members"

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")> _
    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
      If Not targetType.IsAssignableFrom(GetType(String)) Then
        Return DependencyProperty.UnsetValue
      End If

      ' Return string.Empty if the value is null.
      If value Is Nothing Then
        Return String.Empty
      End If

      ' Return string.Empty if the value is an empty string.
      If (TypeOf value Is String) AndAlso (String.IsNullOrEmpty(TryCast(value, String))) Then
        Return String.Empty
      End If

      If Not (TypeOf value Is DateTime) Then
        Return DependencyProperty.UnsetValue
      End If

      Dim dateTime As DateTime = DirectCast(value, DateTime)

      Return dateTime.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
      If Not targetType.IsAssignableFrom(GetType(DateTime)) Then
        Return DependencyProperty.UnsetValue
      End If

      Dim text As String = TryCast(value, String)

      If String.IsNullOrEmpty(text) Then
        Return Nothing
      End If

      Dim dateTime As DateTime

      If Date.TryParseExact(text, "dd/MMM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, dateTime) Then
        Return dateTime
      End If

      Return Nothing
    End Function

#End Region
  End Class
End Namespace
