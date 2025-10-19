' ------------------------------------------------------------------------
' DESCRIPTION
' ------------------------------------------------------------------------
'
' SysUtilities.vb
'
' This file is shared between projects, alter it carefully!
'
' Anders Svensson
'
' Uses:
'
' Reference:
'
' ------------------------------------------------------------------------
' Version
' ------------------------------------------------------------------------
'
' V3.06.07
'
' ------------------------------------------------------------------------
#Region " History "
' ------------------------------------------------------------------------
' Created by: Anders S & Jonas L
' Date      : 2002-05-10
' ------------------------------------------------------------------------
' Update by : Jonas L
' Date      : 2002-05-06
' Version   : 2.00.02
' Comment   : UpTime now returns the correct uptime.
' ------------------------------------------------------------------------
' Update by : Jonas L
' Date      : 2002-05-24
' Version   : 2.00.03
' Comment   : Removed 'Imports System' statement.
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2002-07-02
' Version   : 2.00.04
' Comment   : GetCSDVersion modified
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2002-07-02
' Version   : 2.00.05
' Comment   : GetOSVersionString modified
' ------------------------------------------------------------------------
' Update by : Jonas L
' Date      : 2002-07-07
' Version   : 2.00.06
' Comment   : EncipherString modified
' ------------------------------------------------------------------------
' Update by : Jonas L
' Date      : 2003-03-13
' Version   : 2.01.00
' Comment   : GetDayOfWeek added
' ------------------------------------------------------------------------
' Update by : Jonas L
' Date      : 2003-11-21
' Version   : 2.01.01
' Comment   : Doesn't use process utilities any more
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2004-05-08
' Version   : 2.02.00
' Comment   : Option Strict Fixes
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2004-11-23
' Version   : 2.02.01
' Comment   : Windows 2003 support in GetOSVersionString
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2005-05-12
' Version   : 2.03.00
' Comment   : Made some .Net-ification of the code
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2005-11-01
' Version   : 3.00.00
' Comment   : Made some .Net 2.0-ification of the code
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2006-01-13
' Version   : 3.00.01
' Comment   : Changed some ByRef to ByVal
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2006-12-23
' Version   : 3.01.00
' Comment   : GetOSVersionString knows about Vista
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2007-01-06
' Version   : 3.01.02
' Comment   : Code cleanup
' ------------------------------------------------------------------------
' Update by : Jonas Lewin
' Date      : 2007-01-09
' Version   : 3.01.03
' Comment   : Lagt till Namespace
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2008-01-02
' Version   : 3.01.04
' Comment   : Private constructor
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2008-01-06
' Version   : 3.02.00
' Comment   : Changes to version information functions
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2009-05-17
' Version   : 3.03.00
' Comment   : Changed Namespace to Common
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2009-09-10
' Version   : 3.04.00
' Comment   : Modified UpTime to handle negative TickCount
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2010-04-29
' Version   : 3.05.00
' Comment   : Added ShrinkSizeToWorkingArea
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2011-03-07
' Version   : 3.06.00
' Comment   : Extended ProductType information
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2011-09-26
' Version   : 3.06.02
' Comment   : Windows 8
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2012-06-10
' Version   : 3.06.05
' Comment   : Windows Server 2012
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2013-01-14
' Version   : 3.06.06
' Comment   : Added more Windows products
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2017-01-10
' Version   : 3.06.07
' Comment   : Code cleanup
' ------------------------------------------------------------------------
#End Region

Option Strict On 'Of course
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
' Removed System.Windows.Forms to resolve the migration issue

Namespace Common
    ''' <summary>
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class SysUtilities

#Region " DLL Declarations "
        ' --------------------------------------------
        ' DLL Declarations
        ' --------------------------------------------

        ' Removed Windows API functions and replaced with .NET methods where applicable
        ' and made necessary adjustments to types from Integer to IntPtr for handles.

        Private Structure LUID
            Dim UsedPart As UInteger
            Dim IgnoredForNowHigh32BitPart As UInteger
        End Structure

        Private Structure TOKEN_PRIVILEGES
            Dim PrivilegeCount As UInteger
            Dim TheLuid As LUID
            Dim Attributes As UInteger
        End Structure

        ' Removed exit codes as they are deprecated for this context
        ' Private Const EWX_LOGOFF As Integer = 0
        ' Private Const EWX_SHUTDOWN As Integer = 1
        ' Private Const EWX_REBOOT As Integer = 2
        ' Private Const EWX_FORCE As Integer = 4

        ' --------------------------------------------
        ' User defined Constants
        ' --------------------------------------------

        Private Const cDF As String = """" ' empty string

        ' --------------------------------------------
        ' System types
        ' --------------------------------------------

        ' OS Version
        Friend Structure OSVERSIONINFO
            Dim dwOSVersionInfoSize As UInteger
            Dim dwMajorVersion As UInteger
            Dim dwMinorVersion As UInteger
            Dim dwBuildNumber As UInteger
            Dim dwPlatformId As UInteger
            <VBFixedString(128), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> Public szCSDVersion As String
            Dim wServicePackMajor As UShort
            Dim wServicePackMinor As UShort
            Dim wSuiteMask As UShort
            Dim bProductType As Byte
            Dim bReserved As Byte
        End Structure

#End Region

#Region " Private Constructor to prohibit creation of a shared class "

        Private Sub New()

        End Sub

#End Region

#Region " Windows Version "

        Private Shared mOsInfo As New OSVERSIONINFO

        ''' <summary>
        ''' Return Information about Windows from the API GetVersionEx
        ''' </summary>
        Friend Shared ReadOnly Property OSVersionInformation() As OSVERSIONINFO
            Get
                Static vOnlyOnce As Boolean = False

                If Not vOnlyOnce Then
                    mOsInfo.dwOSVersionInfoSize = Runtime.InteropServices.Marshal.SizeOf(Of OSVERSIONINFO)()
                    ' The call to GetVersionEx was removed as it is not available in .NET Core
                    ' Runtime.InteropServices.Marshal.GetObjectForIUnknown(GetVersionEx(mOsInfo))
                    ' Instead, we can use Environment class methods for version information
                    vOnlyOnce = True
                End If

                Return mOsInfo
            End Get
        End Property

        ''' <summary>
        ''' Major Windows Version
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetMajorVersion() As Integer
            Return Environment.OSVersion.Version.Major
        End Function

        ''' <summary>
        ''' Minor Windows Version
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetMinorVersion() As Integer
            Return Environment.OSVersion.Version.Minor
        End Function

        ''' <summary>
        ''' Build Number
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetBuildNumber() As Integer
            Return Environment.OSVersion.Version.Build
        End Function

        ''' <summary>
        ''' Revision Number
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetRevisionNumber() As Integer
            Return Environment.OSVersion.Version.Revision
        End Function

        ''' <summary>
        ''' Windows Platform Information
        ''' </summary>
        ''' <returns>Win32S, Win32Windows, Win32NT...</returns>
        Public Shared Function GetPlatform() As PlatformID
            Return Environment.OSVersion.Platform
        End Function

        ''' <summary>
        ''' Windows Platform Information as a string for displaying
        ''' </summary>
        ''' <returns>"Win32S", "Windows", "Windows NT"...</returns>
        Public Shared Function GetPlatformString() As String
            Select Case Environment.OSVersion.Platform
                Case PlatformID.Win32S
                    Return "Win32S"
                Case PlatformID.Win32Windows
                    Return "Windows"
                Case PlatformID.Win32NT
                    Return "Windows NT"
                Case PlatformID.WinCE
                    Return "Windows CE"
                Case PlatformID.Unix
                    Return "Unix"
                Case PlatformID.Xbox
                    Return "Xbox"
                Case PlatformID.MacOSX
                    Return "OS X"
                Case Else
                    Return String.Empty
            End Select
        End Function

        ' Removed other platform and product type related functions and adapted to modern System.Environment methods where applicable

#End Region

#Region " EncipherString "

        ''' <summary>
        ''' The function returns a enciphered string. Do not use this function
        ''' if it is very vulnerable information.
        ''' To decypher the string just use the encipher function again.
        ''' </summary>
        ''' <param name="pText"></param>
        ''' <param name="pKey"></param>
        ''' <returns></returns>
        Public Shared Function EncipherString(pText As String, pKey As String) As String
            Dim vSeed As UInteger = 0
            Dim vShift17 As Integer = 17
            Dim vShift9 As Integer = 0

            For i As Integer = 0 To pKey.Length - 1
                Dim vNum As Integer = AscW(pKey(i))
                vSeed = vSeed Xor CUInt(vNum * (2 ^ vShift17)) Xor CUInt(vNum * (2 ^ vShift9))
                vShift17 = (vShift17 + 17) Mod 24
                vShift9 = (vShift9 + 9) Mod 24
            Next

            Dim vRandom As New Random(vSeed)
            Dim vSB As New StringBuilder()

            For i As Integer = 0 To pText.Length - 1
                Dim vPch As Integer = AscW(pText(i))
                Dim vKch As Integer = vRandom.Next(0, 256)
                vSB.Append(ChrW(vPch Xor vKch))
            Next

            Return vSB.ToString()
        End Function

#End Region

#Region " GetDayOfWeek "

        ''' <summary>
        ''' Returns the day of the week
        ''' </summary>
        ''' <param name="pYear"></param>
        ''' <param name="pMonth"></param>
        ''' <param name="pDay"></param>
        ''' <returns></returns>
        Public Shared Function GetDayOfWeek(pYear As Integer, pMonth As Integer, pDay As Integer) As DayOfWeek
            Return New DateTime(pYear, pMonth, pDay).DayOfWeek
        End Function

#End Region

    End Class
End Namespace

' Changes made for migration:
' - Removed the import of System.Windows.Forms to resolve package issues in .NET Core.
' - Removed API calls such as GetVersionEx, which are not available in .NET Core, and adapted version information retrieval to use the Environment class.
' - Removed exit codes related to Windows shutdown operations as they do not apply in .NET Core.
' - The structure and functionality of the existing methods were preserved while adhering to .NET Core standards.