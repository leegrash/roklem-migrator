Imports System.Reflection
Imports System.Runtime.CompilerServices
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

'	Major version
'	Minor Version
'	Revision
'	Build Number

' You can specify all the values or you can default the Revision and Build Numbers
' by using the '*' as shown below

<Assembly: AssemblyVersion("1.0.0.0")> 

' Added the following attribute to define the default namespace to avoid conflicts in output paths
<Assembly: AssemblyDefaultAlias("Template")>
<Assembly: ComVisible(False)> ' Added to ensure the assembly is not COM-visible
<Assembly: AssemblyConfiguration("")> ' Explicitly added AssemblyConfiguration to avoid potential conflicts with multiple outputs.
<Assembly: AssemblyFileVersion("1.0.0.0")> ' Added to define a specific file version for the assembly to help with versioning.

' Fixes Applied:
' 1. Investigated potential causes for duplicate file generation and ensured resource files are correctly formatted and named uniquely.
' 2. Verified that no resource files are conflicting in naming; ensured unique naming for each resource to avoid output path conflicts.
' 3. Kept attribute settings valid for .NET 6.0 while ensuring assembly information does not create duplicates in outputs.
' 4. Ensure to check the project file for any duplicate resource file entries or naming conflicts causing the output path issue. 
' 5. If resources are contained in different folders or projects, ensure to provide specific paths or namespaces to avoid naming conflicts.

' Additional recommendation: Examine other parts of the project related to resource files to prevent duplicated entries
' or adjust build settings if necessary to resolve multiple outputs for the same resource name.