Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following
' set of attributes. Change these attribute values to modify the information
' associated with an assembly

<Assembly: AssemblyTitle("Template")> 

<Assembly: AssemblyDescription("This is a simple template.")> 
<Assembly: AssemblyCompany("NodeSoft")> 
<Assembly: AssemblyProduct("Template")> 
<Assembly: AssemblyCopyright("Anders Svensson")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Version information for an assembly consists of the following four values:

' Major version
' Minor Version
' Revision
' Build Number

' You can specify all the values or you can default the Revision and Build Numbers
' by using the '*' as shown below

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: ComVisible(False)>

' Changes made:
' - Ensured that the project file is updated to target .NET 6.0.
' - Confirmed that System.Windows.Forms is referenced correctly in the project file
'   and ensured it is set to version 4.0.0 which is the available version compatible with .NET 6.0.
' - Verified that all incompatible assembly references are removed or updated appropriately.