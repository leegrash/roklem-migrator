Option Strict On 'Of course

Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Reflection

Namespace Common

    ''' <summary>
    ''' Manages the arguments.
    ''' 
    ''' Ex:
    ''' 			_argumentHandler = New ArgumentHandler
    ''' 
    ''' or:
    ''' 			_argumentHandler = New ArgumentHandler(Microsoft.VisualBasic.Command())
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class ArgumentHandler
        Implements IEnumerable(Of Argument)

#Region " Constructors "

        ''' <summary>
        ''' Creates a new list of argument from the command line
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()

            ParseCommandLine(Command)
        End Sub

        ''' <summary>
        ''' Creates a new list of argument from the command line specified.
        ''' </summary>
        ''' <param name="commandLine">Command line to parse</param>
        ''' <remarks></remarks>
        Public Sub New(commandLine As String)
            MyBase.New()

            ParseCommandLine(commandLine)
        End Sub

#End Region

#Region " Constants "

        Private Const cPath As String = "PATH"

        Public Const cFirstPath As String = "PATH1"
        Public Const cSecondPath As String = "PATH2"
        Public Const cThirdPath As String = "PATH3"
        Public Const cFourthPath As String = "PATH4"

#End Region

#Region " ArgumentsCollection "

        Private Class ArgumentsCollection
            Inherits KeyedCollection(Of String, Argument)

            Protected Overrides Function GetKeyForItem(item As Argument) As String
                If item Is Nothing Then Throw New ArgumentNullException("item")

                Return item.ArgumentName
            End Function
        End Class

#End Region

#Region " Arguments "

        Private _arguments As ArgumentsCollection

        ''' <summary>
        ''' Returns an Argument object (by key)
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetArgument(key As String) As Argument
            Try
                Return _arguments.Item(key.ToUpper)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Returns an Argument value (by key)
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetArgumentValue(key As String) As String
            Dim vArg = _arguments.Item(key.ToUpper)

            If vArg Is Nothing Then Throw New ArgumentException($"Argument {key} not specified as command line parameter")

            Return vArg.ArgumentValue
        End Function

        ''' <summary>
        ''' Checks if an Argument exists (by key)
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Contains(key As String) As Boolean
            If String.IsNullOrEmpty(key) Then Throw New ArgumentNullException("key")

            Try
                Return _arguments.Contains(key.ToUpper)
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Creates a new argument and adds it to the list
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub Add(name As String, value As String)
            Add(New Argument(name, value))
        End Sub

        ''' <summary>
        ''' Creates a new argument and adds it to the list
        ''' </summary>
        ''' <param name="argument"></param>
        ''' <remarks></remarks>
        Public Sub Add(argument As Argument)
            If argument Is Nothing Then Throw New ArgumentNullException("argument")

            Try
                _arguments.Add(argument)
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name} Duplicate Keys: {argument.ArgumentName}: {ex.Message}"
                Debug.WriteLine(vError)

                MsgBox($"Duplicate Argument: {argument.ArgumentName}", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            End Try
        End Sub

#End Region

#Region " Parse "

        ''' <summary>
        ''' Creates a list of arguments from a command line
        ''' </summary>
        ''' <param name="commandLine">Command line to parse</param>
        ''' <remarks></remarks>
        Private Sub ParseCommandLine(commandLine As String)
            Try
                'Erase previous values
                _arguments = New ArgumentsCollection

                Dim pathNumber = 1

                For i = 1 To StringUtilities.GetPosNum(commandLine, " ", True)
                    Dim arg = Trim(StringUtilities.GetPosSep(commandLine, " ", i, True))

                    If arg <> "" Then
                        If StringUtilities.StartsWith(Trim(arg), "/") Then
                            If StringUtilities.GetPosNum(arg, ":", True) = 1 Then
                                Add(StringUtilities.LTrimText(Trim(arg), "/"), "")
                            Else
                                Dim vValue = Trim(StringUtilities.GetPosSep(arg, ":", 1, True))
                                Add(StringUtilities.LTrimText(vValue, "/"), StringUtilities.TrimText(Trim(StringUtilities.LTrimText(arg, vValue & ":", True)), """"))
                            End If
                        ElseIf StringUtilities.StartsWith(Trim(arg), "-") Then
                            If StringUtilities.GetPosNum(arg, ":", True) = 1 Then
                                Add(StringUtilities.LTrimText(Trim(arg), "-"), "")
                            Else
                                Dim vValue = Trim(StringUtilities.GetPosSep(arg, ":", 1, True))
                                Add(StringUtilities.LTrimText(vValue, "-"), StringUtilities.TrimText(Trim(StringUtilities.LTrimText(arg, vValue & ":", True)), """"))
                            End If
                        Else
                            Add(cPath & pathNumber, StringUtilities.TrimText(Trim(arg), """"))
                            pathNumber += 1
                        End If
                    End If
                Next
            Catch ex As Exception
                Dim errorMessage As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                Debug.WriteLine(errorMessage)
            End Try
        End Sub

#End Region

#Region " IEnumerable "

        Public Function GetEnumeratorOfArgument() As IEnumerator(Of Argument) Implements IEnumerable(Of Argument).GetEnumerator
            Return _arguments.GetEnumerator
        End Function

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _arguments.GetEnumerator
        End Function

#End Region

    End Class
End Namespace