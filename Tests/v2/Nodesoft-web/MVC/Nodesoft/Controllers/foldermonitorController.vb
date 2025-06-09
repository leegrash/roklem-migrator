Namespace Nodesoft
    Public Class FolderMonitorController
        Inherits NodesoftBaseController

        Public Overrides ReadOnly Property FileName As String
            Get
                Return "foldermonitor.zip"
            End Get
        End Property

        <HttpGet("foldermonitor.xml")>
        Public Overrides Function GetXml() As IActionResult
            Return GetXmlFile()
        End Function
    End Class
End Namespace

' Changes made:
' - No changes to method names or types, original code is already compatible with .NET Core.
' - Ensure that all required NuGet packages are referenced correctly in the project file.