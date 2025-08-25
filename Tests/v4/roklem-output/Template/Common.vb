Imports NodeSoft.Common

Namespace Template
    Module Common

        Public SP As frmSplash

        Public Sub Main()
            ' There should be only one entry point for the entire application, adapt accordingly.
            Dim prg As Application

            SetUpExceptionHandling()

            ' Initialize application and start main logic, consider using 
            ' Middleware for web requests in ASP.NET Core
            prg = New Application()
            ' Uncomment the line below for your specific entry pattern
            ' prg.ShowMain() ' This should correspond to an ASP.NET Core entry point
        End Sub

#Region " OnException "

        Private Sub SetUpExceptionHandling()
#If CONFIG = "Release" Then
            ' Configure global exception handling for ASP.NET Core applications appropriately
#End If
        End Sub

        Public Sub OnUnhandledException(ByVal sender As Object, ByVal e As System.UnhandledExceptionEventArgs)
            If e.ExceptionObject IsNot Nothing AndAlso TypeOf e.ExceptionObject Is Exception Then
                OnException(sender, DirectCast(e.ExceptionObject, Exception))
            End If
        End Sub

        Public Sub OnThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
            OnException(sender, e.Exception)
        End Sub

        Private Sub OnException(ByVal sender As Object, ByVal e As Exception)
            Try
                ' Utilize a logging framework instead of console messages in ASP.NET Core
                Console.WriteLine(e.Message)
            Catch ex As Exception
                Console.WriteLine($"Error displaying error! ({ex.Message})")
            End Try
        End Sub

#End Region

    End Module
End Namespace

' Changes made:
' 1. Ensured there are no duplicated output file definitions or resource entries for frmAbout, prioritizing singular references.
' 2. The direct call to ShowMain() remains commented out to meet ASP.NET Core architectural practices.
' 3. Adjusted the method implementations to align with the best practices for handling errors in ASP.NET Core, suggesting logging frameworks instead of console output.