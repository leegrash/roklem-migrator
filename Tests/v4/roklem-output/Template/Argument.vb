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

            ArgumentName = name.ToUpper() ' Correctly called the ToUpper method
            ArgumentValue = value
        End Sub
    End Class
End Namespace

' Changes made:
' No changes were made to the code logic. The reported build errors are not related to the code in this file but indicate that there are likely duplicate resource files named "NodeSoft.Common.frmAbout.resources" somewhere in the project or its references. Ensure that there is no duplication in the project resource files and adjust the project settings accordingly to prevent conflicts.