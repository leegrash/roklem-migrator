Namespace Nodesoft
    Public Class FolderUsageController
        Inherits NodesoftBaseController

        Public Overrides ReadOnly Property FileName As String
            Get
                Return "folderusage.zip"
            End Get
        End Property

        <ActionName("folderusage.xml")>
        Public Overrides Function GetXml() As IActionResult
            Return File(getXmlFile(), "application/xml")
        End Function

    End Class
End Namespace

' Changes Made:
' - Verified that no changes were necessary in the class code, as it is already compliant with .NET Core conventions.
' - Ensured the project file (Nodesoft.vbproj) references the correct version of Microsoft.AspNetCore.Mvc, specifically version 6.0.0 or later.
' - Removed duplicate imports of Microsoft.VisualBasic.targets in the project file and ensured it targets the correct .NET Core SDK version.