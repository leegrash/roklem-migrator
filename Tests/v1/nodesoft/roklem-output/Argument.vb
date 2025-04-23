Option Strict On 'Of course

Namespace Common
    ''' <summary>
    ''' Hold's detailed info about an argument.
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    <DebuggerDisplay("{ArgumentName}: {ArgumentValue}")>
    Friend Class Argument
        ''' <summary>
        ''' The name of the argument
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ArgumentName As String

        ''' <summary>
        ''' The value of the argument
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ArgumentValue As String

        ''' <summary>
        ''' Constructor that takes argument name and argument value as parameters
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub New(name As String, value As String)
            If String.IsNullOrEmpty(name) Then Throw New ArgumentNullException(NameOf(name))

            ArgumentName = name.ToUpper()
            ArgumentValue = value
        End Sub
    End Class
End Namespace