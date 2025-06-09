Namespace Nodesoft
    Public Class DiskbenchController
        Inherits NodesoftBaseController

        Public Overrides ReadOnly Property FileName As String
            Get
                Return "diskbench.zip"
            End Get
        End Property

        <HttpGet>
        <ActionName("diskbench.xml")>
        Public Function GetXML() As IActionResult
            Return File(getXmlFile(), "application/xml")
        End Function
    End Class
End Namespace

' Changes made:
' - Ensured that the project file (Nodesoft.vbproj) targets net6.0 and removed any duplicate imports, specifically the Microsoft.VisualBasic.targets.
' - Updated NuGet packages in the project to include Microsoft.AspNetCore.Mvc version 6.0.0 or later.
' - Verified that the GetXML method returns an IActionResult consistently without errors.