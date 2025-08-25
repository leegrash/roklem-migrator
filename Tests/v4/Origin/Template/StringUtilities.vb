Option Strict On 'Of course

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Common
    ''' <summary>
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    <ComVisible(False)>
    Friend Class StringUtilities
        Private Sub New()

        End Sub

        Private Const cDF As String = """"

        ''' <summary>
        ''' Gets the filename, including extension if any
        ''' </summary>
        ''' Extract the filname from a path
        ''' <param name="path">The path and filename</param>
        ''' <returns>The filename (with extension if any)</returns>
        Public Shared Function ExtractFilename(path As String) As String
            Return IO.Path.GetFileName(path)
        End Function

        ''' <summary>
        ''' Gets the extension of the file without the "."
        ''' </summary>
        ''' Extract the file-extension from a path
        ''' <param name="file">The filename wth the extension, with or without a path</param>
        ''' <returns>The file extension</returns>
        Public Shared Function ExtractExtension(file As String) As String
            Return Path.GetExtension(file).Replace(".", String.Empty)
        End Function

        ''' <summary>
        ''' Get only the path with the trailing "\", excluding the filename
        ''' </summary>
        ''' Extract the path from a filename with path
        ''' <param name="path">The path with the filename</param>
        ''' <returns>The path</returns>
        Public Shared Function ExtractPath(path As String) As String
            Return IO.Path.GetDirectoryName(path) & IO.Path.DirectorySeparatorChar
        End Function

        ''' <summary>
        ''' Get the C:\ or the \\computer\share$
        ''' </summary>
        ''' <param name="path">The path with the filename</param>
        ''' <returns>The root path</returns>
        Public Shared Function ExtractRootDriveOrUNC(path As String) As String
            If InStr(path, ":") > 0 Then
                Return Left(path, 3)   'C:\
            Else
                Return Left(path, GetSepPos(path, "\", 4))    '\\computer\share$
            End If
        End Function

        ' Replaces fromstring to tostring in instring
        ' 
        ' Ex:
        ' ReplaceString("'1','2'","'","´")
        ' ReplaceString("Hello World","World","Everyone")

        ''' <summary>
        ''' Replace, that is able to be case insensitive.
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="fromString">What to search for</param>
        ''' <param name="toString">What the found text should be replaced to</param>
        ''' <returns>A string with the new text, instead of the FromString text.</returns>
        Public Shared Function ReplaceString(source As String, fromString As String, toString As String) As String
            Return ReplaceString(source, fromString, toString, False)
        End Function

        ''' <summary>
        ''' Replace, that is able to be case insensitive.
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="fromString">What to search for</param>
        ''' <param name="toString">What the found text should be replaced to</param>
        ''' <param name="ignoreCase">If search should be case insensitive</param>
        ''' <returns>A string with the new text, instead of the FromString text.</returns>
        Public Shared Function ReplaceString(source As String, fromString As String, toString As String, ignoreCase As Boolean) As String
            Dim sb As New StringBuilder

            If ignoreCase Then
                Dim start = 1

                Dim fromStringUpper = UCase(fromString)
                Dim sourceUpper = UCase(source)

                While InStr(start, sourceUpper, fromStringUpper) > 0
                    Dim stopPos = InStr(start, sourceUpper, fromStringUpper)
                    Dim temp = Mid(source, start, stopPos - start)
                    sb.Append(temp & toString)

                    start = stopPos + Len(fromString)
                End While

                If start <= Len(source) Then
                    sb.Append(Mid(source, start, Len(source) - start + 1))
                End If
            Else
                sb.Append(Replace(source, fromString, toString))
            End If

            Return sb.ToString
        End Function

        '
        ' The function BinaryInStringToHexInString converts unreadable binary data in a
        '     string variable, and returns readable hexadecimal format. Note that the
        '     returned string isn't headed with the standard &H notation.
        '
        ' The passed parameters are:
        '     ConvertThis - the string of unreadable binary data to be converted to
        '         hexedecimal format.
        '
        ' The function performs the following tasks in corresponding order:
        '     1) Initiate a For loop that iterates once for each character in the string.
        '         A character is a byte of binary data represented by two hexadecimal
        '         digits.
        '     2) Translate the loop counter's character/byte into hexadecimal.
        '     3) If the hexadecimal translation is one character long, add a "0" leader.
        '     4) Append the two hexadecimal characters to the buffer.
        '     5) Return the completed buffer to the calling procedure.
        '
        Public Shared Function BinaryInStringToHexInString(convertThis As String, outFormat As Integer) As String
            Dim sb As New StringBuilder            'Storage string for the hexadecimal translation
            Dim hexPacket As String                 'The two character hexadecimal translation for the byte of binary data
            Dim decPacket As String

            ' Initiate a For loop

            If outFormat = 1 Then
                sb.Append(String.Format("{0}szBin={1}{1}; ", vbTab, cDF))
            End If

            For ii = 1 To Len(convertThis) Step 1
                If outFormat = 0 Then
                    ' Translate the loop counter's character/byte into hexadecimal
                    hexPacket = Hex(Asc(Mid(convertThis, ii, 1)))

                    ' If the hexadecimal translation is one character long, add a "0" leader
                    If Len(hexPacket) = 1 Then
                        hexPacket = "0" & hexPacket
                    End If

                    ' Append the two hexadecimal characters to the buffer
                    sb.Append(hexPacket & ", ")
                Else
                    decPacket = CStr(Asc(Mid(convertThis, ii, 1)))
                    sb.Append("szBin[" & ii - 1 & "]=" & cDF & decPacket & cDF & "; ")
                    If (ii Mod 15) = 0 Then
                        sb.Append(vbCrLf & vbTab)
                    End If
                End If
            Next

            ' Return the completed buffer to the calling procedure
            If outFormat = 0 Then
                Return RTrimText(Trim(sb.ToString), ",")
            Else
                Return Trim(sb.ToString)
            End If
        End Function

        ''' <summary>
        ''' Makes sure that there is a backslash at the end of the path
        ''' </summary>
        ''' Adds \ at the end of a path if it's not there
        ''' <param name="path">The path to verify/change</param>
        ''' <returns>The path with a trailing backslash</returns>
        Public Shared Function CheckPathEnding(path As String) As String
            If Trim(path) = String.Empty Then
                Throw New ArgumentException($"StringUtilities.CheckPathEnding: Valid path not specified! '{path}'")
            End If

            If Right(path, 1) <> "\" Then
                Return path & "\"
            Else
                Return path
            End If
        End Function

        ''' <summary>
        ''' Remove trailing backslash from path
        ''' </summary>
        ''' Removes \ from the end of a path if it's there
        ''' <param name="path">Path to remove trailing backslash</param>
        ''' <returns>Path whithout trailing backslash</returns>
        Public Shared Function RemovePathEnding(path As String) As String
            Return RTrimText(path, "\")
        End Function

        ''' <summary>
        ''' Get the position of the Nth separator
        ''' </summary>
        ''' Returns the position of the valid separator
        ''' 
        ''' Ex:
        ''' GetSepPos("a1 a2 a3"," ",2) -> 6
        ''' <param name="source">The normal string</param>
        ''' <param name="separator">The separator/string to find</param>
        ''' <param name="whichSeparator">The Nth separator from the left</param>
        Public Shared Function GetSepPos(source As String, separator As String, whichSeparator As Integer) As Integer
            Dim separatorPosition As Integer

            If source = String.Empty Then
                Return 0
            End If

            separatorPosition = 0

            For i = 1 To whichSeparator
                separatorPosition = InStr(separatorPosition + 1, source, separator)
            Next

            Return separatorPosition
        End Function

        ''' <summary>
        ''' Get the position of the separator, in the string. (instr on steroids)
        ''' </summary>
        ''' Counts positions in a separated string
        ''' 
        ''' Ex:
        ''' GetPosSep("a1 a2 a3"," ")
        ''' <param name="source">The normal string</param>
        ''' <param name="separator">The separator/string to find</param>
        Public Shared Function GetPosNum(source As String, separator As String) As Integer
            Return GetPosNum(source, separator, False)
        End Function

        ''' <summary>
        ''' Get the position of the separator, in the string. (instr on steroids)
        ''' </summary>
        ''' Counts positions in a separated string
        ''' 
        ''' Ex:
        ''' GetPosSep("a1 a2 a3"," ")
        ''' <param name="source">The normal string</param>
        ''' <param name="separator">The separator/string to find</param>
        ''' <param name="keepTogether">Treat "ab cd de" as one unbreakable unit</param>
        ''' <returns>The position the separator is in the string</returns>
        Public Shared Function GetPosNum(source As String, separator As String, keepTogether As Boolean) As Integer
            Dim separatorLength = Len(separator)
            If separatorLength = 0 Then
                separatorLength = 1
            End If

            Dim sourceLength = Len(source)
            Dim position = 1
            Dim vLoop = 0

            Do
                position = InStrKeep(position, source, separator, keepTogether)

                If (position = 0) Or (position > sourceLength) Then                  'Not found
                    Exit Do
                Else
                    vLoop += 1
                    position += separatorLength
                End If
            Loop

            Return vLoop + 1
        End Function

        ''' <summary>
        ''' Get the string between the Nth separator position, and the Nth+1
        ''' </summary>
        ''' Extracts a string on the specified position of a separated string
        ''' Ex:
        ''' GetPosSep("a1 a2 a3"," ",2) -> a2
        ''' GetPosSep("a1;a2;a3",";",3) -> a3
        ''' GetPosSep("a1;;a2;;a3",";;",2) -> a2
        Public Shared Function GetPosSep(source As String, separator As String, position As Integer) As String
            Return GetPosSep(source, separator, position, False)
        End Function

        ''' <summary>
        ''' Get the string between the Nth separator position, and the Nth+1
        ''' </summary>
        ''' Extracts a string on the specified position of a separated string
        ''' Ex:
        ''' GetPosSep("a1 a2 a3"," ",2) -> a2
        ''' GetPosSep("a1;a2;a3",";",3) -> a3
        ''' GetPosSep("a1;;a2;;a3",";;",2) -> a2
        ''' GetPosSep("1 2 ""3 4 5"" 6", " ", 3, True) -> "3 4 5"
        ''' 
        ''' Used like: Get Substring from String
        ''' <param name="source">The normal string</param>
        ''' <param name="separator">The separator/string to find</param>
        ''' <param name="position">The Nth separator from the left, to start from</param>
        ''' <param name="keepTogether">Treat "ab cd de" as one unbreakable unit</param>
        ''' <returns>The string between the Nth separator position, and the Nth+1</returns>
        Public Shared Function GetPosSep(source As String, separator As String, position As Integer, keepTogether As Boolean) As String
            Dim separatorLength = Len(separator)
            If separatorLength = 0 Then
                separatorLength = 1
            End If

            If position = 1 Then
                'First
                Dim endPosition = InStrKeep(1, source, separator, keepTogether)

                If endPosition > 1 Then
                    Return Left(source, endPosition - 1)
                Else
                    If StartsWith(source, separator) Then
                        'The first character is a separator
                        Return String.Empty
                    Else
                        Return source
                    End If
                End If
            Else
                Dim startPosition = InStrKeep(1, source, separator, keepTogether)
                For vLoop = 1 To position - 2
                    startPosition = InStrKeep(startPosition + separatorLength, source, separator, keepTogether)
                    If startPosition = 0 Then
                        Return String.Empty
                    End If
                Next

                If startPosition <> 0 Then
                    Dim endPosition = InStrKeep(startPosition + separatorLength, source, separator, keepTogether)
                    If endPosition = 0 Then
                        Dim vSourceLength = Len(source)

                        Return Right(source, vSourceLength - startPosition - separatorLength + 1)
                    Else
                        Return Mid(source, startPosition + separatorLength, endPosition - startPosition - separatorLength)
                    End If
                Else
                    Return String.Empty
                End If
            End If
        End Function

        ''' <summary>
        ''' Instr but keeps "   "
        ''' </summary>
        ''' InStr but keeps "   " together
        ''' <param name="startPosition">Position in the string to start the search</param>
        ''' <param name="text">The text to be searched</param>
        ''' <param name="lookFor">The text to search for</param>
        Private Shared Function InStrKeep(startPosition As Integer, text As String, lookFor As String) As Integer
            Return InStrKeep(startPosition, text, lookFor, False)
        End Function

        ''' <summary>
        ''' Instr but keeps "   "
        ''' </summary>
        ''' InStr but keeps "   " together
        ''' <param name="startPosition">Position in the string to start the search</param>
        ''' <param name="text">The text to be searched</param>
        ''' <param name="lookFor">The text to search for</param>
        ''' <param name="keepTogether">Treat "ab cd de" as one unbreakable unit</param>
        Private Shared Function InStrKeep(startPosition As Integer, text As String, lookFor As String, keepTogether As Boolean) As Integer
            Dim result As Integer

            If keepTogether Then
                'Look out for "

                Dim pos = InStr(startPosition, text, lookFor)
                Dim keepPos = InStr(startPosition, text, """")

                If keepPos <> 0 And pos > keepPos Then
                    'Look for the next "
                    keepPos = InStr(keepPos + 1, text, """")

                    If keepPos <> 0 Then
                        'Found it
                        result = InStr(keepPos, text, lookFor)
                    Else
                        'Matching " not found!
                        result = pos
                    End If
                Else
                    '" not found!
                    result = pos
                End If
            Else
                'Normal
                result = InStr(startPosition, text, lookFor)
            End If

            Return result
        End Function

        ''' <summary>
        ''' Removes the specified text from both edges/ends of the string
        ''' </summary>
        ''' <param name="text">The original text</param>
        ''' <param name="textToRemove">The text to trim from the edges</param>
        Public Shared Function TrimText(text As String, textToRemove As String) As String
            Return TrimText(text, textToRemove, False)
        End Function

        ''' <summary>
        ''' Removes the specified text from both edges/ends of the string
        ''' </summary>
        ''' Removes any string from start and end of string
        ''' TrimText("xABCx","x")
        ''' <param name="text">The original text</param>
        ''' <param name="textToRemove">The text to trim from the edges</param>
        ''' <param name="ignoreCase">Should the text to remove be case insensitive</param>
        Public Shared Function TrimText(text As String, textToRemove As String, ignoreCase As Boolean) As String
            text = RTrimText(text, textToRemove, ignoreCase)
            text = LTrimText(text, textToRemove, ignoreCase)

            Return text
        End Function

        ''' <summary>
        ''' Removes the specified text from the right of the source text
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="textToRemove">Text to remove from the right side</param>
        Public Shared Function RTrimText(source As String, textToRemove As String) As String
            Return RTrimText(source, textToRemove, False)
        End Function

        ''' <summary>
        ''' Removes the specified text from the right of the source text
        ''' </summary>
        ''' Removes any string from end of string
        ''' RTrimText("xABCx","x")
        ''' <param name="source">The original text</param>
        ''' <param name="textToRemove">Text to remove from the right side</param>
        ''' <param name="ignoreCase">Should the search be case insensitive</param>
        Public Shared Function RTrimText(source As String, textToRemove As String, ignoreCase As Boolean) As String
            Dim origText As String = String.Empty

            If Len(textToRemove) = 0 Then
                Return source
            End If

            If ignoreCase Then
                origText = source
                source = LCase(source)
                textToRemove = LCase(textToRemove)
            End If

            Dim trimTextLen = Len(textToRemove)

            If Len(source) >= trimTextLen Then
                Do Until Mid(source, Math.Max(Len(source) - trimTextLen + 1, 1), Math.Min(trimTextLen, Len(source))) <> textToRemove
                    source = Left(source, Len(source) - trimTextLen)
                Loop
            End If

            If ignoreCase Then
                source = Left(origText, Len(source))
            End If

            Return source
        End Function

        ''' <summary>
        ''' Removes the specified text from the left of the source text
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="textToRemove">Text to remove from the left side</param>
        Public Shared Function LTrimText(source As String, textToRemove As String) As String
            Return LTrimText(source, textToRemove, False)
        End Function

        ''' <summary>
        ''' Removes the specified text from the left of the source text
        ''' </summary>
        ''' Removes any string from start of string
        ''' LTrimText("xABCx","x")
        ''' <param name="source">The original text</param>
        ''' <param name="textToRemove">Text to remove from the left side</param>
        ''' <param name="ignoreCase">Should the search be case insensitive</param>
        Public Shared Function LTrimText(source As String, textToRemove As String, ignoreCase As Boolean) As String
            Dim origText As String = String.Empty

            If ignoreCase Then
                origText = source
                source = LCase(source)
                textToRemove = LCase(textToRemove)
            End If

            If Len(textToRemove) = 0 Then
                Return source
            End If

            Dim trimTextLen = Len(textToRemove)

            If Len(source) >= trimTextLen Then
                Do Until Mid(source, 1, trimTextLen) <> textToRemove
                    source = Right(source, Len(source) - trimTextLen)
                Loop
            End If

            If ignoreCase Then
                source = Right(origText, Len(source))
            End If

            Return source
        End Function

        ''' <summary>
        ''' Removes ..\ and replaces with absolute path
        ''' </summary>
        ''' <param name="path">The actual path that should be corrected</param>
        ''' <param name="file">The filename</param>
        Public Shared Function ResolvePath(path As String, file As String) As String
            Dim vPath As String = path
            Dim vFile As String = file

            'File starts with ..\
            If GetPosSep(vFile, "\", 1) = ".." Then
                Do While GetPosSep(vFile, "\", 1) = ".."
                    vFile = LTrimText(vFile, "..")
                    vFile = LTrimText(vFile, "\")
                    vPath = RTrimText(vPath, "\")
                    vPath = RTrimText(vPath, GetPosSep(vPath, "\", GetPosNum(vPath, "\")))
                Loop

                Return CheckPathEnding(vPath) & vFile
            ElseIf file.IndexOf(":"c) > 0 Then
                'This is probably a complete path
                'Do nothing
                Return vFile
            Else
                Return CheckPathEnding(vPath) & vFile
            End If
        End Function

        ''' <summary>
        ''' Checks if the string ends with the specified string. (Case insensitive)
        ''' </summary>
        ''' Checks if a string starts with ...
        ''' <param name="text">The normal string</param>
        ''' <param name="test">The ending</param>
        Public Shared Function StartsWith(text As String, test As String) As Boolean
            If UCase(Left(text, Len(test))) = UCase(test) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Checks if the string ends with the specified string. (Case insensitive)
        ''' </summary>
        ''' Checks if a string ends with ...
        ''' <param name="text">The normal string</param>
        ''' <param name="test">The ending</param>
        Public Shared Function EndsWith(text As String, test As String) As Boolean
            If UCase(Right(text, Len(test))) = UCase(test) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Check if the filename matches the mask
        ''' </summary>
        ''' <param name="fileName">The filename, with or without the path</param>
        ''' <param name="mask">The actual filemask, including *, ? and other special characters</param>
        ''' <returns>True if the filename matches the filemask</returns>
        Public Shared Function MatchesFileMask(fileName As String, mask As String) As Boolean
            Return MatchesFileMask(fileName, mask, False)
        End Function

        ''' <summary>
        ''' Check if the filename matches the mask
        ''' </summary>
        ''' Wildcard check
        ''' <param name="fileName">The filename, with or without the path</param>
        ''' <param name="mask">The actual filemask, including *, ? and other special characters</param>
        ''' <param name="caseSensitive">Should the match be case sensitive</param>
        ''' <returns>True if the filename matches the filemask</returns>
        Public Shared Function MatchesFileMask(fileName As String, mask As String, caseSensitive As Boolean) As Boolean
            If Not caseSensitive Then
                Dim temp As New StringBuilder

                While Len(mask) > 0
                    Dim character = Left(mask, 1)
                    mask = Right(mask, Len(mask) - 1)
                    If (character Like "[a-ö]") OrElse (character Like "[A-Ö]") Then
                        temp.Append($"[{LCase(character)}{UCase(character)}]")
                    Else
                        temp.Append(character)
                    End If
                End While

                mask = temp.ToString
            End If

            'Testing for match
            mask = ReplaceString(mask, " ", "[ ]")
            mask = ReplaceString(mask, "-", "[-]")
            mask = ReplaceString(mask, "_", "[_]")
            mask = ReplaceString(mask, "!", "[!]")
            mask = ReplaceString(mask, ".", "[.]")
            mask = ReplaceString(mask, ",", "[,]")

            If fileName Like mask Then
                Return True
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Get the text between 2 separators, where the second have to be after the first
        ''' </summary>
        ''' <param name="source">The normal string</param>
        ''' <param name="firstSeparator">The first string to find</param>
        ''' <param name="endSeparator">The second string to find</param>
        Public Shared Function GetTextBetweenSeparators(source As String, firstSeparator As String, endSeparator As String) As String
            If firstSeparator = String.Empty And endSeparator = String.Empty Then
                Return source
            End If

            If firstSeparator = String.Empty Then
                If InStr(source, endSeparator) > 0 Then
                    Return Left(source, InStr(source, endSeparator) - 1)
                End If
                Return String.Empty 'Todo: Jonas la till denna för att få bort en varning. Tidigare stod det Exit Function
            End If

            If endSeparator = String.Empty Then
                If InStr(source, firstSeparator) > 0 Then
                    Return Right(source, Len(source) - InStr(source, firstSeparator) + 1 - Len(firstSeparator))
                End If
                Return String.Empty 'Todo: Jonas la till denna för att få bort en varning. Tidigare stod det Exit Function
            End If

            Dim startPos As Integer

            If InStr(source, firstSeparator) > 0 Then
                startPos = GetSepPos(source, firstSeparator, 1)
            End If

            If startPos <> 0 Then
                startPos += Len(firstSeparator)

                Dim vEndPos As Integer

                If InStr(source, endSeparator) > 0 Then
                    vEndPos = InStr(startPos, source, endSeparator)
                End If

                If vEndPos = 0 Then
                    Return Mid(source, startPos, Len(source) - startPos + 1)
                Else
                    If vEndPos = 1 Then
                        Return Mid(source, startPos, Len(source) - startPos + vEndPos)
                    Else
                        Return Mid(source, startPos, vEndPos - startPos)
                    End If
                End If
            Else
                Return String.Empty
            End If
        End Function

        ''' <summary>
        ''' Remove \\ or *:\ from the start of the path
        ''' </summary>
        ''' Extract the path (and filename) without driveletter:\
        ''' <param name="path">The path to change</param>
        ''' <returns>A path without the leading \\ or :\</returns>
        Public Shared Function RemoveDriveFromPath(path As String) As String
            If Len(path) <= 3 Then
                Return String.Empty
            Else
                Dim vLength = Len(path)
                Dim vLoop = 2

                Dim vChar = Mid(path, vLoop, 1)

                While (vChar = "\") Or (vChar = ":")
                    vLoop += 1
                    vChar = Mid(path, vLoop, 1)
                End While

                Return Mid(path, vLoop, vLength + 1 - vLoop)
            End If
        End Function

        ''' <summary>
        ''' Instead of replacing with two (on N) keywords, you can use this. See the example for usage and result.
        ''' </summary>
        ''' Example
        ''' ReplacePartOfKeywordString("Hello Who Ever You Are", "Klopp", "Hello Klopp You")
        ''' Will result in "Hello Who Ever You"
        ''' <param name="longOriginalString">The long original text</param>
        ''' <param name="keyword">The string that separates the N keywords in StringWithKeyword</param>
        ''' <param name="stringWithKeyword">How the string should look like, with the keyword. The keyword will then be replaced with the real text.</param>
        ''' <returns>A string looking like StingWithKeyword, but with the keyword replaced with the actual text.</returns>
        Public Shared Function ReplacePartOfKeywordString(longOriginalString As String, keyword As String, stringWithKeyword As String) As String
            Try
                Dim newString As String = String.Empty
                Dim separatorPosition = GetSepPos(stringWithKeyword, keyword, 1)

                'Var finns första delen av strStringWithKeyword i strLongOriginalString?
                Dim firstPosInOriginalString = InStr(longOriginalString, Left(stringWithKeyword, separatorPosition - 1))

                'Ta bort från longOriginalString, det som in
                longOriginalString = Right(longOriginalString, longOriginalString.Length - (firstPosInOriginalString - 1))

                While InStr(stringWithKeyword, keyword) > 0
                    'Var finns strKeyword?
                    separatorPosition = GetSepPos(stringWithKeyword, keyword, 1)

                    'Början av strStringWithKeyword fram till strKeyword
                    Dim vStart = Mid(stringWithKeyword, 1, separatorPosition - 1)
                    Dim vEnd As String

                    'strEnd skall innehålla det som finns efter strKeyword... men kanske bara fram till nästa strKeyword
                    If GetPosNum(stringWithKeyword, keyword, True) > 2 Then
                        'Wildcard finns på mer än 2 ställe
                        vEnd = Mid(stringWithKeyword, separatorPosition + Len(keyword), GetSepPos(stringWithKeyword, keyword, 2) - separatorPosition - Len(keyword))
                        stringWithKeyword = Right(stringWithKeyword, Len(stringWithKeyword) - GetSepPos(stringWithKeyword, keyword, 2) + 1)
                    Else
                        vEnd = Right(stringWithKeyword, Len(stringWithKeyword) - (separatorPosition + Len(keyword) - 1))
                        stringWithKeyword = vEnd
                    End If

                    Dim vTemp = vStart & GetTextBetweenSeparators(longOriginalString, vStart, vEnd) & vEnd
                    newString = vTemp
                End While

                Return newString
            Catch
                Return String.Empty
            End Try
        End Function

        ''' <summary>
        ''' Replaces the Nth occurance of a specified string, in the original string
        ''' </summary>
        ''' <param name="source">The normal string</param>
        ''' <param name="toBeReplaced">The string to find the Nth occurance of</param>
        ''' <param name="toReplace">The string to change the found string to</param>
        ''' <param name="occurance">The Nth occurance to replace</param>
        ''' <returns>A string that contains the new substring instead of the old, on the correct location.</returns>
        Public Shared Function ReplaceNthOccuranceOfString(source As String, toBeReplaced As String, toReplace As String, occurance As Integer) As String
            Dim startPos = GetSepPos(source, toBeReplaced, occurance)
            Return Left(source, startPos - 1) & toReplace & Right(source, Len(source) - startPos + 1 - Len(toBeReplaced))
        End Function

        ' Example ReplaceStringBetween("ABCdefgHI","BC","H","new value") will return "Anew valueI"
        ' Example ReplaceStringBetween("<H1><title>Här är en titel</title></H1>", "<title>", "</title>", "<title>En Ny Titel</title>") will return "<H1><title>En Ny Titel</title></H1>"
        ''' <summary>
        ''' Replace the string, using two strings as delimiters
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="firstString">The first delimiter</param>
        ''' <param name="secondString">The second delimiter</param>
        ''' <param name="newString">The new text between the two delimiters</param>
        ''' <returns>The original string but the text between the two delimiters is replaced</returns>
        Public Shared Function ReplaceStringBetween(source As String, firstString As String, secondString As String, newString As String) As String
            Return ReplaceStringBetween(source, firstString, secondString, newString, False)
        End Function

        ''' <summary>
        ''' Replace the string, using two strings as delimiters
        ''' </summary>
        ''' <param name="source">The original text</param>
        ''' <param name="firstString">The first delimiter</param>
        ''' <param name="secondString">The second delimiter</param>
        ''' <param name="newString">The new text between the two delimiters</param>
        ''' <param name="isCaseSensitive">Is the search case sensitive?</param>
        ''' <returns>The original string but the text between the two delimiters is replaced</returns>
        Public Shared Function ReplaceStringBetween(source As String, firstString As String, secondString As String, newString As String, isCaseSensitive As Boolean) As String
            Dim compareMethod = CType(IIf(isCaseSensitive, Microsoft.VisualBasic.CompareMethod.Binary, Microsoft.VisualBasic.CompareMethod.Text), CompareMethod)
            Dim stringBuilder = New StringBuilder

            Dim start = 1
            Dim previousEnd = 1

            While InStr(start, source, firstString, compareMethod) > 0
                start = InStr(start, source, firstString, compareMethod)
                Dim vEnd = InStr(start, source, secondString, compareMethod)

                If vEnd > 0 Then
                    stringBuilder.Append(Mid(source, previousEnd, start - previousEnd))
                    stringBuilder.Append(newString)
                    start = vEnd + Len(secondString)
                    previousEnd = start
                Else
                    start = previousEnd
                    Exit While
                End If
            End While

            stringBuilder.Append(Right(source, Len(source) - start + 1))

            Return stringBuilder.ToString
        End Function

        ''' <summary>
        ''' Converts unicode/utf8 text to a proper string, if the unicode/utf8 control characters are intact
        ''' </summary>
        ''' Use this if your string starts with "255, 254". This will return the true string.
        ''' <param name="badAsciiString">The string with the text and the control characters</param>
        ''' <returns>A correct string, the way it was supposed to look</returns>
        Public Shared Function ConvertNonAsciiTextCorrectly(badAsciiString As String) As String
            Dim buffer As Byte() = Encoding.Default.GetBytes(badAsciiString)

            If Len(badAsciiString) >= 2 Then
                'Guess the type of the file
                If Len(badAsciiString) >= 3 AndAlso ((buffer(0) = 239) And (buffer(1) = 187) And (buffer(2) = 191)) Then
                    'It's an UTF8 file
                    Dim vTmp(buffer.Length - 5) As Byte
                    Array.Copy(buffer, 3, vTmp, 0, buffer.Length - 4) 'The UTF8 file aways stats with "239,187,191" bytes, and ends with a "0" byte, and we don't want these.
                    Return Encoding.UTF8.GetString(vTmp)
                ElseIf (buffer(0) = 255) And (buffer(1) = 254) And (buffer(buffer.Length - 1) = 0) Then
                    'It's a Unicode file
                    Dim vTmp(buffer.Length - 3) As Byte
                    Array.Copy(buffer, 2, vTmp, 0, buffer.Length - 2) 'The Unicode file aways stats with "255,254" bytes, and we don't want these.
                    Return Encoding.Unicode.GetString(vTmp)
                Else
                    'Nope this was not any Unicode file, so return the previous string
                    Return badAsciiString
                End If
            Else
                Return badAsciiString
            End If
        End Function

        ''' <summary>
        ''' Uses the first bytes in the string to figure out if it is a unicode file or not.
        ''' </summary>
        ''' <param name="text">The string to be analysed. It needs to be at least 2 bytes long</param>
        ''' <returns></returns>
        ''' <remarks>http://en.wikipedia.org/wiki/Byte_order_mark</remarks>
        Public Shared Function GetTextEncodingFromString(text As String) As Encoding
            If Len(text) >= 4 Then
                If Asc(text(0)) = 255 And Asc(text(1)) = 254 Then
                    'It is utf16 (le) or utf32 (le)
                    If Asc(text(2)) = 0 And Asc(text(3)) = 0 Then
                        Return Encoding.UTF32 ' utf 32 (LE)
                    Else
                        Return Encoding.Unicode ' utf 16 (LE)
                    End If
                End If
                If Asc(text(0)) = 43 And Asc(text(1)) = 47 And Asc(text(2)) = 118 Then
                    If Asc(text(3)) = 56 Or Asc(text(3)) = 57 Or Asc(text(3)) = 43 Or Asc(text(3)) = 47 Then
                        Return Encoding.UTF7    ' utf 7
                    End If
                End If
            End If

            If Len(text) >= 2 Then
                If Asc(text(0)) = 254 And Asc(text(1)) = 255 Then
                    Return Encoding.BigEndianUnicode    ' utf 16 (BE)
                End If
                If Asc(text(0)) = 239 And Asc(text(1)) = 187 And Asc(text(2)) = 191 Then
                    Return Encoding.UTF8    ' utf 8
                End If
            End If

            Return Nothing
        End Function

    End Class
End Namespace
