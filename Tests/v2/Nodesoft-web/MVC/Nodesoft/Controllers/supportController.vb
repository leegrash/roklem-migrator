Namespace Nodesoft
    Public Class SupportController
        Inherits Microsoft.AspNetCore.Mvc.Controller

        '
        ' GET: /support

        Function Index() As IActionResult
            Return View()
        End Function

    End Class
End Namespace

' Changes made:
' 1. Ensured the project targets net6.0 in the .vbproj file.
' 2. Updated the project to reference Microsoft.AspNetCore.Mvc version 6.0.0 or compatible.
' 3. Verified the SDK and package references in the .vbproj file are correct and compatible with .NET 6.0.
' 4. Fixed the NuGet package reference issue by ensuring the project includes the proper version of Microsoft.AspNetCore.Mvc in the .vbproj file.