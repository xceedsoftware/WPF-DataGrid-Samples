'
' Xceed DataGrid for WPF - SAMPLE CODE
' Copyright (c) 2007-2021 Xceed Software Inc.
' 
' [XceedDeploymentLicense.vb]
'  
' This static class provides an easy way to license the Xceed DataGridControl.
' 
' This file is part of the Xceed DataGrid for WPF product. The use 
' and distribution of this Sample Code is subject to the terms 
' and conditions referring to "Sample Code" that are specified in 
' the XCEED SOFTWARE LICENSE AGREEMENT accompanying this product.
'

Namespace Xceed.Wpf.DataGrid.Samples
  Public Class XceedDeploymentLicense
    Public Shared Sub SetLicense()
      '================================
      ' How to license Xceed components 
      ' ================================
      '
      ' To license (unlock) your component, set the LicenseKey property with your 
      ' license key in the entry point of the application. This will ensure the component
      ' is licensed before any of its methods are called.
      '  
      ' If the component is used in a DLL project (no entry point is available), it is 
      ' recommended that the LicenseKey property be set in a static constructor of a 
      ' class that will be accessed systematically before any component is instantiated,
      ' or you can simply set the LicenseKey property immediately BEFORE 
      ' instantiation of the component. 
      ' 
      ' To deploy this sample, your license key should be set in the OnStartup() method.
      '
      ' For more information, consult the "Licensing" topics in the product documentation. 
      '
      ' Xceed DataGrid for WPF can be unlocked with either a license key 
      ' that starts with the letters "DGF", "TDV" or "DGP".
      '

      ' Please uncomment the following line to set your own license key.
      ' Xceed.Wpf.DataGrid.Licenser.LicenseKey = "DGXX-XXXXX-XXXXX-XXXX"

    End Sub
  End Class

End Namespace
