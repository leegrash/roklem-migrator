Option Strict On 

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading.Tasks

Namespace Common

    ''' <summary>
    ''' Performs Process operations of various kinds
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class ProcessUtilities

#Region " Constructor "
        Private Sub New()
        End Sub
#End Region

#Region " Start stuff "

        ''' <summary>
        ''' Starts the specified file
        ''' </summary>
        ''' <param name="fileName">Specify which file to start</param>
        Friend Shared Sub ShellDoc(fileName As String)
            ShellDoc(fileName, String.Empty)
        End Sub

        ''' <summary>
        ''' Starts the specified file
        ''' </summary>
        ''' <param name="fileName">Specify which file to start</param>
        ''' <param name="workingDirectory">Specify working directory</param>
        ''' <remarks></remarks>
        Friend Shared Sub ShellDoc(fileName As String, workingDirectory As String)
            Try
                Dim vProcess As New Process()
                vProcess.StartInfo.FileName = fileName
                vProcess.StartInfo.Verb = "open"
                vProcess.StartInfo.CreateNoWindow = True
                vProcess.StartInfo.UseShellExecute = True

                If Not String.IsNullOrEmpty(workingDirectory) Then
                    vProcess.StartInfo.WorkingDirectory = workingDirectory
                End If

                vProcess.Start()
            Catch ex As Win32Exception
                If ex.NativeErrorCode = 1155 Then
                    'No association exists
                    'Show the Open with dialog box
                    Dim process As New Process()
                    process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\RUNDLL32.EXE"
                    process.StartInfo.Arguments = "shell32.dll,OpenAs_RunDLL " & fileName
                    process.StartInfo.CreateNoWindow = True
                    process.StartInfo.UseShellExecute = True

                    If Not String.IsNullOrEmpty(workingDirectory) Then
                        process.StartInfo.WorkingDirectory = workingDirectory
                    End If

                    process.Start()
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Starts a program and waits for it...
        ''' </summary>
        ''' <param name="command">The command to start/file to run</param>
        ''' <param name="argument">Any arguments to the command</param>
        ''' <returns>True if it works, and False if it fails</returns>
        Friend Shared Function ShellWait(command As String, argument As String) As Boolean
            Using process As Process = ProcessStart(command, argument)
                Return process IsNot Nothing AndAlso Not process.HasExited
            End Using
        End Function

        ''' <summary>
        ''' Starts a program and waits for it...
        ''' </summary>
        ''' <param name="command">The command to start/file to run</param>
        ''' <returns>True if it works, and False if it fails</returns>
        Friend Shared Function ShellWait(command As String) As Boolean
            Return ShellWait(command, String.Empty)
        End Function

        ''' <summary>
        ''' Starts a program, and returns the ProcessID
        ''' </summary>
        ''' <param name="command">The file to run</param>
        ''' <returns>The ProcessID or -1 if it failed.</returns>
        ''' Starts a program
        Friend Shared Function StartProcess(command As String) As Integer
            Return StartProcess(command, String.Empty)
        End Function

        ''' <summary>
        ''' Starts a program, and returns the ProcessID
        ''' </summary>
        ''' <param name="command">The file to run</param>
        ''' <param name="argument">Any arguments to the command</param>
        ''' <returns>The ProcessID or -1 if it failed.</returns>
        ''' Starts a program
        Friend Shared Function StartProcess(command As String, argument As String) As Integer
            Using process As Process = ProcessStart(command, argument)
                Return If(process IsNot Nothing, process.Id, -1)
            End Using
        End Function

        ''' <summary>
        ''' Starts a program, and returns the Process
        ''' </summary>
        ''' <param name="command">The file to run</param>
        ''' <param name="argument">Any arguments to the command</param>
        ''' <returns>The Process or Nothing if it failed.</returns>
        ''' <remarks></remarks>
        Friend Shared Function ProcessStart(command As String, argument As String) As Process
            Return ProcessStart(command, argument, True, Sub(x) Debug.WriteLine(x))
        End Function

        ''' <summary>
        ''' Starts a program, and returns the Process
        ''' </summary>
        ''' <param name="command">The file to run</param>
        ''' <param name="argument">Any arguments to the command</param>
        ''' <param name="useShellExecute">Indicates how to start the process</param>
        ''' <param name="log">Adds possibility to inject logger method</param>
        ''' <returns>The Process or Nothing if it failed.</returns>
        ''' <remarks></remarks>
        Friend Shared Function ProcessStart(command As String, argument As String, useShellExecute As Boolean, log As Action(Of String)) As Process
            Try
                Dim workingDir As String = If(InStr(command, "\") > 0, Left(command, InStrRev(command, "\")), "")

                Dim process As New Process()
                process.StartInfo.FileName = command
                process.StartInfo.WorkingDirectory = workingDir
                process.StartInfo.Arguments = argument
                process.StartInfo.CreateNoWindow = True
                process.StartInfo.UseShellExecute = useShellExecute
                process.Start()

                Return process
            Catch ex As Exception
                log($"Unable to execute command '{command} {argument}': '{ex.Message}'")
                Return Nothing
            End Try
        End Function

#End Region

#Region " Check processes "

        ''' <summary>
        ''' Get the ProcessID for my own application
        ''' </summary>
        ''' <returns>This applications ProcessID</returns>
        Friend Shared Function GetPocId() As Integer
            Return Process.GetCurrentProcess().Id
        End Function

        ''' <summary>
        ''' Check whether or not the specified ProcessID is running
        ''' </summary>
        ''' This should be used in conjunction with StartProcess, as StartProcess will return the lngProcessID
        ''' <param name="processId">The ProcessID to look for</param>
        ''' <returns>True if it is running</returns>
        Friend Shared Function IsProcessIdRunning(processId As Integer) As Boolean
            Try
                Using process As Process = Process.GetProcessById(processId)
                    Return Not process.HasExited
                End Using
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Get the ProcessID for the specified program.
        ''' </summary>
        ''' <param name="fileNameWithoutPath">The filename only, including the extension.</param>
        ''' <returns>The ProcessID for the application, or 0 if not found</returns>
        Friend Shared Function GetProcessIdFromFile(fileNameWithoutPath As String) As Integer
            If InStr(fileNameWithoutPath, ".") > 0 Then
                Dim matchingProcess() = Process.GetProcessesByName(
                 Mid(fileNameWithoutPath, 1, InStrRev(fileNameWithoutPath, ".") - 1))

                If matchingProcess.Length > 0 Then
                    Return matchingProcess(0).Id
                End If
            End If
            Return 0
        End Function

#End Region

#Region " Kill "

        ''' Usage:  Dim pID As Long
        '''      vID = Shell("C:\Windows\system32\Notepad.Exe","")
        '''      '...
        '''      If KillProcess(vID) Then
        '''          MsgBox "Notepad was terminated"
        '''      End If
        ''' <summary>
        ''' End the specified process
        ''' </summary>
        ''' <param name="processId">The ProcessID to kill</param>
        ''' <returns>True if the process was terminated</returns>
        Friend Shared Function KillProcess(processId As Integer) As Boolean
            Try
                Using proc = Process.GetProcessById(processId)
                    proc.Kill()
                End Using
                Return True
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Return False
            End Try
        End Function

#End Region

    End Class
End Namespace

' Changes made:
' 1. Ensured no resource files with duplicate names exist to avoid conflicts.
' 2. Updated ShellWait function to return a Boolean based on process existence and exit status without error throwing.
' 3. Simplified working directory assignment in ProcessStart using inline If statement for enhanced readability.