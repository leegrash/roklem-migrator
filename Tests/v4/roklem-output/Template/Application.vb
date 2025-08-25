Imports NodeSoft.Common

Namespace Template
    Friend Class Application
        Public AH As ArgumentHandler
        Public frm As frmMain
        Private SP As SplashForm ' Assume SplashForm is a form used for splash messages

        Friend Sub AddTextToSplash(ByVal p_strText As String)
            If SP IsNot Nothing Then
                SP.AddText(p_strText)
            End If
        End Sub

        Friend Sub ShowMain()
            AddTextToSplash("Show Form")
            SP.Close()
            SP = Nothing
            frm.ShowDialog()
        End Sub

        Public Sub New()
            MyBase.New()

            Dim strText As String = String.Empty

            Try
                ' Create the ArgumentHandler Object
                strText = "Creating ArgumentHandler..."
                AddTextToSplash(strText)
                AH = New ArgumentHandler()

                ' Create the AppMain Object
                strText = "Creating Main Form..."
                AddTextToSplash(strText)
                frm = New frmMain(Me)

                ProcessCommandLine()

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Catch ex As Exception
                Debug.WriteLine(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message))
                MessageBox.Show(strText & " - failed!") ' Changed MsgBox to MessageBox.Show
                Environment.Exit(0) ' Changed End to Environment.Exit for .NET Core compatibility
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
                                Environment.Exit(0) ' Changed End to Environment.Exit for .NET Core compatibility
                        End Select
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message))
            End Try
        End Sub

        Public Sub ShowAboutBox()
            Dim AB As frmAbout

            AB = New frmAbout(Me.frm)
            AB.AddText(String.Format("{0} [/?]", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name))
            AB.AddText("  /?" & vbTab & "This information")

            AB.ShowDialog()
            AB = Nothing
        End Sub

        Public Sub UpdateStatusBar(ByVal p_strText As String)
            frm.UpdateStatusBar(p_strText)
        End Sub
    End Class
End Namespace

' Changes made:
' - No changes made to the structure of the code; issues regarding the same output path error (MSB3577) need to be addressed. 
' - Additional checks or adjustments to resource files in the project might be necessary to resolve the build error related to duplicate output paths.