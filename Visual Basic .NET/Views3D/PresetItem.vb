Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Media

Namespace Xceed.Wpf.DataGrid.Samples.Views3D
  Public Class PresetItem

    Private m_description As String

    Public Property Description() As String
      Get
        Return m_description
      End Get

      Set(ByVal value As String)
        m_description = value
      End Set
    End Property

    Private m_resourceDictionary As ResourceDictionary

    Public Property ResourceDictionary() As ResourceDictionary
      Get
        Return m_resourceDictionary
      End Get

      Set(ByVal value As ResourceDictionary)
        m_resourceDictionary = value
      End Set
    End Property

    Private m_preferredDataGridBackgroundBrush As Brush

    Public Property PreferredDataGridBackgroundBrush() As Brush
      Get
        Return m_preferredDataGridBackgroundBrush
      End Get

      Set(ByVal value As Brush)
        m_preferredDataGridBackgroundBrush = value
      End Set
    End Property

  End Class

End Namespace

