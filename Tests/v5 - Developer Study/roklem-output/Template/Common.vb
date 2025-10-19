Imports System.Windows.Forms
Imports System.Threading

Namespace Template
    Module Common

        Public SP As frmSplash

        Public Sub Main()
            Dim prg As Application

            Application.EnableVisualStyles()

            SP = New frmSplash()
            SP.Show()

            Application.DoEvents()

            SetUpExceptionHandling()

            prg = New Application()
            prg.ShowMain()
        End Sub

#Region " OnException "

        Private Sub SetUpExceptionHandling()
#If CONFIG = "Release" Then
            'Catch unhandled exceptions from main thread
            AddHandler Application.ThreadException, AddressOf OnThreadException

            'Catch unhandled exceptions from other threads
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
#End If
        End Sub

        Public Sub OnUnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            If e.ExceptionObject IsNot Nothing _
              AndAlso TypeOf e.ExceptionObject Is Exception Then
                OnException(sender, DirectCast(e.ExceptionObject, Exception))
            End If
        End Sub

        Public Sub OnThreadException(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            OnException(sender, e.Exception)
        End Sub

        Private Sub OnException(ByVal sender As Object, ByVal e As Exception)
            Try
                MessageBox.Show(e.Message) ' Changed MsgBox to MessageBox.Show for .NET Core compatibility
            Catch ex As Exception
                MessageBox.Show(String.Format("Error displaying error! ({0})", ex.Message)) ' Same change here
            End Try
        End Sub

#End Region

    End Module
End Namespace

' Changes made:
' - Ensured that the appropriate reference to 'System.Windows.Forms' is present in the project file and that a compatible version (>= 6.0.0) is available.
' - Verified that MessageBox.Show is used for compatibility with .NET Core. 
' - No code changes related to the functionality were necessary as the package issue is unrelated to the code itself.