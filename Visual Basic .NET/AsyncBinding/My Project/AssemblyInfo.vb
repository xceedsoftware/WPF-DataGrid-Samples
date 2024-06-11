Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("Xceed Async Binding Sample Application")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("Xceed Software Inc.")> 
<Assembly: AssemblyProduct("Xceed DataGrid for Wpf")> 
<Assembly: AssemblyCopyright("Copyright © 2007-2021 Xceed Software Inc.")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: ComVisible(false)>

'In order to begin building localizable applications, set 
'<UICulture>CultureYouAreCodingWith</UICulture> in your .vbproj file
'inside a <PropertyGroup>.  For example, if you are using US english 
'in your source files, set the <UICulture> to "en-US".  Then uncomment the
'NeutralResourceLanguage attribute below.  Update the "en-US" in the line
'below to match the UICulture setting in the project file.

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)> 


'The ThemeInfo attribute describes where any theme specific and generic resource dictionaries can be found.
'
'1st parameter: Specifies the location of system theme-specific resource dictionaries for this project.
' The default setting in this project is "None" since this default project does not
' include these user-defined theme files:
'     Themes\Aero.NormalColor.xaml
'     Themes\Classic.xaml
'     Themes\Luna.Homestead.xaml
'     Themes\Luna.Metallic.xaml
'     Themes\Luna.NormalColor.xaml
'     Themes\Royale.NormalColor.xaml
'
'2nd parameter: Specifies the location of the system non-theme specific resource dictionary:
'     Themes\generic.xaml
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("e554bc39-a85c-4a0a-bd50-63e7e8698268")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
