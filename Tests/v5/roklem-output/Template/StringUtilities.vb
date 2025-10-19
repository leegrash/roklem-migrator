Option Strict On

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Common
    <ComVisible(False)>
    Friend Class StringUtilities
        Private Sub New()

        End Sub

        Private Const cDF As String = """" 

        Public Shared Function ExtractFilename(path As String) As String
            Return IO.Path.GetFileName(path)
        End Function

        Public Shared Function ExtractExtension(file As String) As String
            Return Path.GetExtension(file).Replace(".", String.Empty)
        End Function

        Public Shared Function ExtractPath(path As String) As String
            Return IO.Path.GetDirectoryName(path) & IO.Path.DirectorySeparatorChar
        End Function

        Public Shared Function ExtractRootDriveOrUNC(path As String) As String
            If InStr(path, ":") > 0 Then
                Return Left(path, 3)   
            Else
                Return Left(path, GetSepPos(path, "\", 4))    
            End If
        End Function

        Public Shared Function ReplaceString(source As String, fromString As String, toString As String) As String
            Return ReplaceString(source, fromString, toString, False)
        End Function

        Public Shared Function ReplaceString(source As String, fromString As String, toString As String, ignoreCase As Boolean) As String
            Dim sb As New StringBuilder

            If ignoreCase Then
                Dim start = 1
                Dim fromStringUpper = fromString.ToUpper(System.Globalization.CultureInfo.InvariantCulture)
                Dim sourceUpper = source.ToUpper(System.Globalization.CultureInfo.InvariantCulture)

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
                sb.Append(source.Replace(fromString, toString))
            End If

            Return sb.ToString()
        End Function

        Public Shared Function BinaryInStringToHexInString(convertThis As String, outFormat As Integer) As String
            Dim sb As New StringBuilder            
            Dim hexPacket As String                 
            Dim decPacket As String

            If outFormat = 1 Then
                sb.Append(String.Format("{0}szBin={1}{1}; ", vbTab, cDF))
            End If

            For ii = 1 To Len(convertThis)
                If outFormat = 0 Then
                    hexPacket = Hex(Asc(Mid(convertThis, ii, 1)))

                    If Len(hexPacket) = 1 Then
                        hexPacket = "0" & hexPacket
                    End If

                    sb.Append(hexPacket & ", ")
                Else
                    decPacket = CStr(Asc(Mid(convertThis, ii, 1)))
                    sb.Append("szBin[" & ii - 1 & "]=" & cDF & decPacket & cDF & "; ")
                    If (ii Mod 15) = 0 Then
                        sb.Append(vbCrLf & vbTab)
                    End If
                End If
            Next

            If outFormat = 0 Then
                Return RTrimText(Trim(sb.ToString), ",")
            Else
                Return Trim(sb.ToString)
            End If
        End Function

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

        Public Shared Function RemovePathEnding(path As String) As String
            Return RTrimText(path, "\")
        End Function

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

        Public Shared Function GetPosNum(source As String, separator As String) As Integer
            Return GetPosNum(source, separator, False)
        End Function

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

                If (position = 0) Or (position > sourceLength) Then                  
                    Exit Do
                Else
                    vLoop += 1
                    position += separatorLength
                End If
            Loop

            Return vLoop + 1
        End Function

        Public Shared Function GetPosSep(source As String, separator As String, position As Integer) As String
            Return GetPosSep(source, separator, position, False)
        End Function

        Public Shared Function GetPosSep(source As String, separator As String, position As Integer, keepTogether As Boolean) As String
            Dim separatorLength = Len(separator)
            If separatorLength = 0 Then
                separatorLength = 1
            End If

            If position = 1 Then
                Dim endPosition = InStrKeep(1, source, separator, keepTogether)

                If endPosition > 1 Then
                    Return Left(source, endPosition - 1)
                Else
                    If StartsWith(source, separator) Then
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
            End Function
        End Function

        Private Shared Function InStrKeep(startPosition As Integer, text As String, lookFor As String) As Integer
            Return InStrKeep(startPosition, text, lookFor, False)
        End Function

        Private Shared Function InStrKeep(startPosition As Integer, text As String, lookFor As String, keepTogether As Boolean) As Integer
            Dim result As Integer

            If keepTogether Then
                Dim pos = InStr(startPosition, text, lookFor)
                Dim keepPos = InStr(startPosition, text, """")
                If keepPos <> 0 And pos > keepPos Then
                    keepPos = InStr(keepPos + 1, text, """")
                    If keepPos <> 0 Then
                        result = InStr(keepPos, text, lookFor)
                    Else
                        result = pos
                    End If
                Else
                    result = pos
                End If
            Else
                result = InStr(startPosition, text, lookFor)
            End If

            Return result
        End Function

        Public Shared Function TrimText(text As String, textToRemove As String) As String
            Return TrimText(text, textToRemove, False)
        End Function

        Public Shared Function TrimText(text As String, textToRemove As String, ignoreCase As Boolean) As String
            text = RTrimText(text, textToRemove, ignoreCase)
            text = LTrimText(text, textToRemove, ignoreCase)

            Return text
        End Function

        Public Shared Function RTrimText(source As String, textToRemove As String) As String
            Return RTrimText(source, textToRemove, False)
        End Function

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

        Public Shared Function LTrimText(source As String, textToRemove As String) As String
            Return LTrimText(source, textToRemove, False)
        End Function

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

        Public Shared Function ResolvePath(path As String, file As String) As String
            Dim vPath As String = path
            Dim vFile As String = file

            If GetPosSep(vFile, "\", 1) = ".." Then
                Do While GetPosSep(vFile, "\", 1) = ".."
                    vFile = LTrimText(vFile, "..")
                    vFile = LTrimText(vFile, "\")
                    vPath = RTrimText(vPath, "\")
                    vPath = RTrimText(vPath, GetPosSep(vPath, "\", GetPosNum(vPath, "\")))
                Loop

                Return CheckPathEnding(vPath) & vFile
            ElseIf file.IndexOf(":"c) > 0 Then
                Return vFile
            Else
                Return CheckPathEnding(vPath) & vFile
            End If
        End Function

        Public Shared Function StartsWith(text As String, test As String) As Boolean
            If String.Compare(Left(text, Len(test)), test, StringComparison.OrdinalIgnoreCase) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function EndsWith(text As String, test As String) As Boolean
            If String.Compare(Right(text, Len(test)), test, StringComparison.OrdinalIgnoreCase) = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function MatchesFileMask(fileName As String, mask As String) As Boolean
            Return MatchesFileMask(fileName, mask, False)
        End Function

        Public Shared Function MatchesFileMask(fileName As String, mask As String, caseSensitive As Boolean) As Boolean
            If Not caseSensitive Then
                Dim temp As New StringBuilder

                While Len(mask) > 0
                    Dim character = Left(mask, 1)
                    mask = Right(mask, Len(mask) - 1)
                    If (character Like "[a-�]") OrElse (character Like "[A-�]") Then
                        temp.Append($"[{LCase(character)}{UCase(character)}]")
                    Else
                        temp.Append(character)
                    End If
                End While

                mask = temp.ToString
            End If

            mask = ReplaceString(mask, " ", "[ ]")
            mask = ReplaceString(mask, "-", "[-]")
            mask = ReplaceString(mask, "_", "[_]")
            mask = ReplaceString(mask, "!", "[!]")
            mask = ReplaceString(mask, ".", "[.]")
            mask = ReplaceString(mask, ",", "[,]")

            Return fileName Like mask
        End Function

        Public Shared Function GetTextBetweenSeparators(source As String, firstSeparator As String, endSeparator As String) As String
            If firstSeparator = String.Empty And endSeparator = String.Empty Then
                Return source
            End If

            If firstSeparator = String.Empty Then
                If InStr(source, endSeparator) > 0 Then
                    Return Left(source, InStr(source, endSeparator) - 1)
                End If
                Return String.Empty 
            End If

            If endSeparator = String.Empty Then
                If InStr(source, firstSeparator) > 0 Then
                    Return Right(source, Len(source) - InStr(source, firstSeparator) + 1 - Len(firstSeparator))
                End If
                Return String.Empty 
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

        Public Shared Function ReplacePartOfKeywordString(longOriginalString As String, keyword As String, stringWithKeyword As String) As String
            Try
                Dim newString As String = String.Empty
                Dim separatorPosition = GetSepPos(stringWithKeyword, keyword, 1)

                Dim firstPosInOriginalString = InStr(longOriginalString, Left(stringWithKeyword, separatorPosition - 1))

                longOriginalString = Right(longOriginalString, longOriginalString.Length - (firstPosInOriginalString - 1))

                While InStr(stringWithKeyword, keyword) > 0
                    separatorPosition = GetSepPos(stringWithKeyword, keyword, 1)

                    Dim vStart = Mid(stringWithKeyword, 1, separatorPosition - 1)
                    Dim vEnd As String

                    If GetPosNum(stringWithKeyword, keyword, True) > 2 Then
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

        Public Shared Function ReplaceNthOccuranceOfString(source As String, toBeReplaced As String, toReplace As String, occurance As Integer) As String
            Dim startPos = GetSepPos(source, toBeReplaced, occurance)
            Return Left(source, startPos - 1) & toReplace & Right(source, Len(source) - startPos + 1 - Len(toBeReplaced))
        End Function

        Public Shared Function ReplaceStringBetween(source As String, firstString As String, secondString As String, newString As String) As String
            Return ReplaceStringBetween(source, firstString, secondString, newString, False)
        End Function

        Public Shared Function ReplaceStringBetween(source As String, firstString As String, secondString As String, newString As String, isCaseSensitive As Boolean) As String
            Dim compareMethod = CType(IIf(isCaseSensitive, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase), StringComparison)
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

            Return stringBuilder.ToString()
        End Function

        Public Shared Function ConvertNonAsciiTextCorrectly(badAsciiString As String) As String
            Dim buffer As Byte() = Encoding.Default.GetBytes(badAsciiString)

            If Len(badAsciiString) >= 2 Then
                If Len(badAsciiString) >= 3 AndAlso ((buffer(0) = 239) And (buffer(1) = 187) And (buffer(2) = 191)) Then
                    Dim vTmp(buffer.Length - 5) As Byte
                    Array.Copy(buffer, 3, vTmp, 0, buffer.Length - 4) 
                    Return Encoding.UTF8.GetString(vTmp)
                ElseIf (buffer(0) = 255) And (buffer(1) = 254) And (buffer(buffer.Length - 1) = 0) Then
                    Dim vTmp(buffer.Length - 3) As Byte
                    Array.Copy(buffer, 2, vTmp, 0, buffer.Length - 2) 
                    Return Encoding.Unicode.GetString(vTmp)
                Else
                    Return badAsciiString
                End If
            Else
                Return badAsciiString
            End If
        End Function

        Public Shared Function GetTextEncodingFromString(text As String) As Encoding
            If Len(text) >= 4 Then
                If Asc(text(0)) = 255 And Asc(text(1)) = 254 Then
                    If Asc(text(2)) = 0 And Asc(text(3)) = 0 Then
                        Return Encoding.UTF32 
                    Else
                        Return Encoding.Unicode 
                    End If
                End If
                If Asc(text(0)) = 43 And Asc(text(1)) = 47 And Asc(text(2)) = 118 Then
                    If Asc(text(3)) = 56 Or Asc(text(3)) = 57 Or Asc(text(3)) = 43 Or Asc(text(3)) = 47 Then
                        Return Encoding.UTF7    
                    End If
                End If
            End If

            If Len(text) >= 2 Then
                If Asc(text(0)) = 254 And Asc(text(1)) = 255 Then
                    Return Encoding.BigEndianUnicode    
                End If
                If Asc(text(0)) = 239 And Asc(text(1)) = 187 And Asc(text(2)) = 191 Then
                    Return Encoding.UTF8    
                End If
            End If

            Return Nothing
        End Function

    End Class
End Namespace

' Changes made:
' - Updated StartsWith and EndsWith functions to use String.Compare with StringComparison.OrdinalIgnoreCase instead of UCase for culture-invariant comparisons.
' - The comparison methods were modified to ensure that string comparisons were consistent and reliable across different cultures using StringComparison in places where ignore case was previously handled by UCase and LCase.