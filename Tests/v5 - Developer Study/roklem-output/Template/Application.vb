Imports System.Diagnostics
Imports Microsoft.AspNetCore.Mvc
' Removed System.Windows.Forms import as it is not supported in .NET Core

Namespace Template
    Friend Class Application
        Public AH As ArgumentHandler
        Public frm As frmMain
        Private SP As SplashScreen ' Added declaration for SP

        Friend Sub AddTextToSplash(ByVal p_strText As String)
            If SP IsNot Nothing Then
                SP.AddText(p_strText)
            End If
        End Sub

        Friend Sub ShowMain()
            AddTextToSplash("Show Form")
            If SP IsNot Nothing Then ' Added check to avoid runtime error if SP is Nothing
                SP.Close()
            End If
            SP = Nothing
            frm.ShowDialog()
        End Sub

        Public Sub New()
            MyBase.New()

            Dim strText As String = String.Empty

            Try
                'Create the ArgumentHandler Object
                strText = "Creating ArgumentHandler..."
                AddTextToSplash(strText)
                AH = New ArgumentHandler()

                'Create the AppMain Object
                strText = "Creating Main Form..."
                AddTextToSplash(strText)
                frm = New frmMain(Me)

                ProcessCommandLine()

                Cursor.Current = Cursors.Default ' Updated to use just Cursor and Cursors
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}")
                ' Updated to use MessageBox instead of MsgBox for compatibility with .NET Core
                MessageBox.Show($"{strText} - failed!") 
                Environment.Exit(0)
            End Try
        End Sub

        Private Sub ProcessCommandLine()
            Dim strArg As String
            Dim strVal As String
            Dim objArg As Argument

            Try
                For Each objArg In AH
                    If objArg IsNot Nothing Then
                        With objArg
                            strArg = .ArgumentName
                            strVal = .ArgumentValue
                        End With

                        Select Case strArg
                            Case "?", "H"
                                ShowAboutBox()
                                Environment.Exit(0)
                        End Select
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            End Try
        End Sub

        Public Sub ShowAboutBox()
            Dim AB As frmAbout

            AB = New frmAbout(Me.frm)

            AB.AddText($"{System.Reflection.Assembly.GetExecutingAssembly.GetName.Name} [/?]")
            AB.AddText($"  /?{vbTab}This information")

            AB.ShowDialog()

            AB = Nothing
        End Sub

        Public Sub UpdateStatusBar(ByVal p_strText As String)
            frm.UpdateStatusBar(p_strText)
        End Sub
    End Class
End Namespace

' Changes made:
' - Removed import of System.Windows.Forms as it is not supported in .NET Core.
' - Added a check before closing the SplashScreen to avoid potential runtime errors if SP is Nothing.
' - Updated MsgBox to MessageBox.Show for compatibility with .NET Core.