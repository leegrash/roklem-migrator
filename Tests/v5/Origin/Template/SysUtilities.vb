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

Namespace Common
    ''' <summary>
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class SysUtilities

#Region " DLL Declarations "
        ' --------------------------------------------
        ' DLL Declarations
        ' --------------------------------------------

        ' Windows Version
        Private Declare Function GetVersionEx Lib "kernel32" Alias "GetVersionExA" (ByRef lpVersionInformation As OSVERSIONINFO) As Integer
        Private Declare Function GetProductInfo Lib "kernel32" (osMajorVersion As Integer, osMinorVersion As Integer, spMajorVersion As Integer, spMinorVersion As Integer, ByRef edition As Integer) As Boolean

        ' Exit Windows
        Private Declare Function ExitWindowsEx Lib "user32" (uFlags As Integer, dwReserved As Integer) As Integer

        ' Windows
        Private Declare Function SetWindowPos Lib "user32" (hWnd As Integer, hWndInsertAfter As Integer, x As Integer, Y As Integer, cx As Integer, cy As Integer, wFlags As Integer) As Integer
        Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (lpClassName As String, lpWindowName As String) As Integer

        Private Structure LUID
            Dim UsedPart As Integer
            Dim IgnoredForNowHigh32BitPart As Integer
        End Structure

        Private Structure TOKEN_PRIVILEGES
            Dim PrivilegeCount As Integer
            Dim TheLuid As LUID
            Dim Attributes As Integer
        End Structure

        Private Declare Function OpenProcessToken Lib "advapi32" (ProcessHandle As Integer, DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
        Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (lpSystemName As String, lpName As String, ByRef lpLuid As LUID) As Integer
        Private Declare Function AdjustTokenPrivileges Lib "advapi32" (TokenHandle As Integer, DisableAllPrivileges As Integer, ByRef NewState As TOKEN_PRIVILEGES, BufferLength As Integer, ByRef PreviousState As TOKEN_PRIVILEGES, ByRef ReturnLength As Integer) As Integer

        ' --------------------------------------------
        ' Windows Constants
        ' --------------------------------------------

        ' bProductType 
        Public Enum ProductType
            VER_NT_WORKSTATION = 1
            VER_NT_DOMAIN_CONTROLLER = 2
            VER_NT_SERVER = 3
        End Enum

        'wSuiteMask
        Public Enum Suite
            VER_SUITE_SMALLBUSINESS = 1
            VER_SUITE_ENTERPRISE = 2
            VER_SUITE_BACKOFFICE = 4
            VER_SUITE_TERMINAL = 16
            VER_SUITE_SMALLBUSINESS_RESTRICTED = 32
            VER_SUITE_EMBEDDEDNT = 64
            VER_SUITE_DATACENTER = 128
            VER_SUITE_SINGLEUSERTS = 256
            VER_SUITE_PERSONAL = 512
            VER_SUITE_BLADE = 1024
            VER_SUITE_STORAGE_SERVER = 8192
            VER_SUITE_COMPUTE_SERVER = 16384
            VER_SUITE_WH_SERVER = 32768
        End Enum

        Public Enum ProductType2
            PRODUCT_UNDEFINED = &H0
            PRODUCT_ULTIMATE = &H1
            PRODUCT_HOME_BASIC = &H2
            PRODUCT_HOME_PREMIUM = &H3
            PRODUCT_ENTERPRISE = &H4
            PRODUCT_HOME_BASIC_N = &H5
            PRODUCT_BUSINESS = &H6
            PRODUCT_STANDARD_SERVER = &H7
            PRODUCT_DATACENTER_SERVER = &H8
            PRODUCT_SMALLBUSINESS_SERVER = &H9
            PRODUCT_ENTERPRISE_SERVER = &HA
            PRODUCT_STARTER = &HB
            PRODUCT_DATACENTER_SERVER_CORE = &HC
            PRODUCT_STANDARD_SERVER_CORE = &HD
            PRODUCT_ENTERPRISE_SERVER_CORE = &HE
            PRODUCT_ENTERPRISE_SERVER_IA64 = &HF
            PRODUCT_BUSINESS_N = &H10
            PRODUCT_WEB_SERVER = &H11
            PRODUCT_CLUSTER_SERVER = &H12
            PRODUCT_HOME_SERVER = &H13
            PRODUCT_STORAGE_EXPRESS_SERVER = &H14
            PRODUCT_STORAGE_STANDARD_SERVER = &H15
            PRODUCT_STORAGE_WORKGROUP_SERVER = &H16
            PRODUCT_STORAGE_ENTERPRISE_SERVER = &H17
            PRODUCT_SERVER_FOR_SMALLBUSINESS = &H18
            PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = &H19
            PRODUCT_HOME_PREMIUM_N = &H1A
            PRODUCT_ENTERPRISE_N = &H1B
            PRODUCT_ULTIMATE_N = &H1C
            PRODUCT_WEB_SERVER_CORE = &H1D
            PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = &H1E
            PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = &H1F
            PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = &H20
            PRODUCT_SERVER_FOUNDATION = &H21
            PRODUCT_HOME_PREMIUM_SERVER = &H22
            PRODUCT_SERVER_FOR_SMALLBUSINESS_V = &H23
            PRODUCT_STANDARD_SERVER_V = &H24
            PRODUCT_DATACENTER_SERVER_V = &H25
            PRODUCT_ENTERPRISE_SERVER_V = &H26
            PRODUCT_DATACENTER_SERVER_CORE_V = &H27
            PRODUCT_STANDARD_SERVER_CORE_V = &H28
            PRODUCT_ENTERPRISE_SERVER_CORE_V = &H29
            PRODUCT_HYPERV = &H2A
            PRODUCT_STORAGE_EXPRESS_SERVER_CORE = &H2B
            PRODUCT_STORAGE_STANDARD_SERVER_CORE = &H2C
            PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = &H2D
            PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = &H2E
            PRODUCT_STARTER_N = &H2F
            PRODUCT_PROFESSIONAL = &H30
            PRODUCT_PROFESSIONAL_N = &H31
            PRODUCT_SB_SOLUTION_SERVER = &H32
            PRODUCT_SERVER_FOR_SB_SOLUTIONS = &H33
            PRODUCT_STANDARD_SERVER_SOLUTIONS = &H34
            PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = &H35
            PRODUCT_SB_SOLUTION_SERVER_EM = &H36
            PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = &H37
            PRODUCT_SOLUTION_EMBEDDEDSERVER = &H38
            PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = &H3B
            PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = &H3C
            PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = &H3D
            PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = &H3E
            PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = &H3F
            PRODUCT_CLUSTER_SERVER_V = &H40
            PRODUCT_STARTER_E = &H42
            PRODUCT_HOME_BASIC_E = &H43
            PRODUCT_HOME_PREMIUM_E = &H44
            PRODUCT_PROFESSIONAL_E = &H45
            PRODUCT_ENTERPRISE_E = &H46
            PRODUCT_ULTIMATE_E = &H47
            PRODUCT_ENTERPRISE_EVALUATION = &H48
            PRODUCT_MULTIPOINT_STANDARD_SERVER = &H4C
            PRODUCT_MULTIPOINT_PREMIUM_SERVER = &H4D
            PRODUCT_STANDARD_EVALUATION_SERVER = &H4F
            PRODUCT_DATACENTER_EVALUATION_SERVER = &H50
            PRODUCT_ENTERPRISE_N_EVALUATION = &H54
            PRODUCT_STORAGE_WORKGROUP_EVALUATION_SERVER = &H5F
            PRODUCT_STORAGE_STANDARD_EVALUATION_SERVER = &H60
            PRODUCT_CORE_N = &H62
            PRODUCT_CORE_COUNTRYSPECIFIC = &H63
            PRODUCT_CORE_SINGLELANGUAGE = &H64
            PRODUCT_CORE = &H65
            PRODUCT_PROFESSIONAL_WMC = &H67
        End Enum

        ' --------------------------------------------
        ' Window Constants
        ' --------------------------------------------

        Private Const HIDEWINDOW As Short = &H80S
        Private Const SHOWWINDOW As Short = &H40S

        ' ExitWindowsEx
        Private Const EWX_LOGOFF As Short = 0
        Private Const EWX_SHUTDOWN As Short = 1
        Private Const EWX_REBOOT As Short = 2
        Private Const EWX_FORCE As Short = 4

        ' --------------------------------------------
        ' User defined Constants
        ' --------------------------------------------

        ' "
        Private Const cDF As String = """"

        ' --------------------------------------------
        ' System types
        ' --------------------------------------------

        ' OS Version

        Friend Structure OSVERSIONINFO
            Dim dwOSVersionInfoSize As Integer
            Dim dwMajorVersion As Integer
            Dim dwMinorVersion As Integer
            Dim dwBuildNumber As Integer
            Dim dwPlatformId As Integer
            <VBFixedString(128), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> Public szCSDVersion As String      '  Maintenance string for PSS usage
            Dim wServicePackMajor As Short
            Dim wServicePackMinor As Short
            Dim wSuiteMask As Short
            Dim bProductType As Byte
            Dim bReserved As Byte
        End Structure

        ' Display state

        'Public Enum DisplayState
        '	LowPower = 1		 ' The display is going to low power
        '	ShutOff = 2		 ' The display is being shut off
        'End Enum

        ' Position of a form

        Public Enum SUFormPos
            SUUpperLeft = 0
            SUUpperRight = 1
            SULowerLeft = 2
            SULowerRight = 3
            SUCenter = 4
        End Enum
#End Region

#Region " Private Constructor to prohibit creation of a shared class "

        Private Sub New()

        End Sub

#End Region

#Region " Windows Version "

        '
        ' Fills a OSVERSIONINFO variable with OS Information
        '
        ' Ex:
        '   Dim osInfo As OSVERSIONINFO
        '   GetWindowsVersion osInfo
        '   debug.print osInfo.dwMajorVersion
        '

        Private Shared mOsInfo As New OSVERSIONINFO

        ''' <summary>
        ''' Return Information about Windows from the API GetVersionEx
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared ReadOnly Property OSVersionInformation() As OSVERSIONINFO
            Get
                Static vOnlyOnce As Boolean = False

                If Not vOnlyOnce Then
                    mOsInfo.dwOSVersionInfoSize = Len(mOsInfo)

                    GetVersionEx(mOsInfo)

                    vOnlyOnce = True
                End If

                Return mOsInfo
            End Get
        End Property

        ''' <summary>
        ''' Major Windows Version
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetMajorVersion() As Integer
            Return Environment.OSVersion.Version.Major
        End Function

        ''' <summary>
        ''' Minor Windows Version
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetMinorVersion() As Integer
            Return Environment.OSVersion.Version.Minor
        End Function

        ''' <summary>
        ''' Build Number
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBuildNumber() As Integer
            Return Environment.OSVersion.Version.Build
        End Function

        ''' <summary>
        ''' Revision Number
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRevisionNumber() As Integer
            Return Environment.OSVersion.Version.Revision
        End Function

        ''' <summary>
        ''' Windows Platform Information
        ''' </summary>
        ''' <returns>Win32S, Win32Windows, Win32NT...</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPlatform() As PlatformID
            Return Environment.OSVersion.Platform
        End Function

        ''' <summary>
        ''' Windows Platform Information as a string for displaying
        ''' </summary>
        ''' <returns>"Win32S", "Windows", "Windows NT"...</returns>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Windows Platform Information in a more understandable form
        ''' </summary>
        ''' <returns>"Windows XP", "Windows Vista"...</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPlatformStringSmart() As String
            Dim vPlatform As String = GetPlatformString()

            Select Case Environment.OSVersion.Platform
                Case PlatformID.Win32Windows
                    Select Case GetMajorVersion()
                        Case 4
                            Dim vRevision As String = Mid(GetServicePack, 1, 1)
                            Select Case GetMinorVersion()
                                Case 0
                                    vPlatform = "Windows 95"
                                    If vRevision = "C" Or vRevision = "B" Then
                                        vPlatform &= " OSR2"
                                    End If
                                Case 10
                                    vPlatform = "Windows 98"
                                    If vRevision = "A" Then
                                        vPlatform &= " SE"
                                    End If
                                Case 90
                                    vPlatform = "Windows Me"
                                Case Else
                                    vPlatform = "Windows 9X"
                            End Select
                        Case Else
                            vPlatform = "Windows 9X"
                    End Select
                Case PlatformID.Win32NT
                    Select Case GetMajorVersion()
                        Case 3, 4
                            vPlatform = "Windows NT"
                        Case 5
                            Select Case GetMinorVersion()
                                Case 0
                                    vPlatform = "Windows 2000"
                                Case 1
                                    vPlatform = "Windows XP"
                                Case 2
                                    vPlatform = "Windows Server 2003"
                            End Select
                        Case 6
                            Select Case GetMinorVersion()
                                Case 0
                                    Select Case GetProductType()
                                        Case ProductType.VER_NT_WORKSTATION
                                            vPlatform = "Windows Vista"
                                        Case Else
                                            vPlatform = "Windows Server 2008"
                                    End Select
                                Case 1
                                    Select Case GetProductType()
                                        Case ProductType.VER_NT_WORKSTATION
                                            vPlatform = "Windows 7"
                                        Case Else
                                            vPlatform = "Windows Server 2008 R2"
                                    End Select
                                Case 2
                                    Select Case GetProductType()
                                        Case ProductType.VER_NT_WORKSTATION
                                            vPlatform = "Windows 8"
                                        Case Else
                                            vPlatform = "Windows Server 2012"
                                    End Select
                            End Select
                    End Select
            End Select

            Return vPlatform
        End Function

        ''' <summary>
        ''' Service pack information
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetServicePack() As String
            Return Environment.OSVersion.ServicePack
        End Function

        ''' <summary>
        ''' Major version of Service pack
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetServicePackMajor() As Short
            Return Environment.OSVersion.Version.MajorRevision
        End Function

        ''' <summary>
        ''' Minor version of Service pack
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetServicePackMinor() As Short
            Return Environment.OSVersion.Version.MinorRevision
        End Function

        ''' <summary>
        ''' Product Type
        ''' </summary>
        ''' <returns>"Workstation", "Domain controller", "Server"</returns>
        ''' <remarks></remarks>
        Public Shared Function GetProductType() As Integer
            Return OSVersionInformation.bProductType
        End Function

        ''' <summary>
        ''' Product Type
        ''' </summary>
        ''' <returns>Ultimate Edition, Home Premium Edition...</returns>
        ''' <remarks></remarks>
        Public Shared Function GetProductType2() As Integer
            Dim vProductType As Integer
            If GetProductInfo(GetMajorVersion, GetMinorVersion, GetServicePackMajor, GetServicePackMinor, vProductType) Then
                Return vProductType
            Else
                Return 0
            End If
        End Function

        Public Shared Function GetProductTypeString() As String
            Select Case GetProductType()
                Case ProductType.VER_NT_WORKSTATION
                    Return "Workstation"
                Case ProductType.VER_NT_DOMAIN_CONTROLLER
                    Return "Domain controller"
                Case ProductType.VER_NT_SERVER
                    Return "Server"
                Case Else
                    Return ""
            End Select
        End Function

        Private Shared Function GetProductType2String(pProductType2 As Integer) As String
            Select Case pProductType2
                Case ProductType2.PRODUCT_UNDEFINED
                    Return String.Empty
                Case ProductType2.PRODUCT_ULTIMATE
                    Return "Ultimate Edition"
                Case ProductType2.PRODUCT_HOME_BASIC
                    Return "Home Basic Edition"
                Case ProductType2.PRODUCT_HOME_PREMIUM
                    Return "Home Premium Edition"
                Case ProductType2.PRODUCT_ENTERPRISE
                    Return "Enterprise Edition"
                Case ProductType2.PRODUCT_HOME_BASIC_N
                    Return "Home Basic Edition N"
                Case ProductType2.PRODUCT_BUSINESS
                    Return "Business Edition"
                Case ProductType2.PRODUCT_STANDARD_SERVER
                    Return "Standard Edition"
                Case ProductType2.PRODUCT_DATACENTER_SERVER
                    Return "Datacenter Edition"
                Case ProductType2.PRODUCT_SMALLBUSINESS_SERVER
                    Return "Small Business Server"
                Case ProductType2.PRODUCT_ENTERPRISE_SERVER
                    Return "Enterprise Edition"
                Case ProductType2.PRODUCT_STARTER
                    Return "Starter Edition"
                Case ProductType2.PRODUCT_DATACENTER_SERVER_CORE
                    Return "Datacenter Edition (core installation)"
                Case ProductType2.PRODUCT_STANDARD_SERVER_CORE
                    Return "Standard Edition (core installation)"
                Case ProductType2.PRODUCT_ENTERPRISE_SERVER_CORE
                    Return "Enterprise Edition (core installation)"
                Case ProductType2.PRODUCT_ENTERPRISE_SERVER_IA64
                    Return "Enterprise Edition for Itanium-based Systems"
                Case ProductType2.PRODUCT_BUSINESS_N
                    Return "Business Edition N"
                Case ProductType2.PRODUCT_WEB_SERVER
                    Return "Web Server Edition"
                Case ProductType2.PRODUCT_CLUSTER_SERVER
                    Return "Cluster Server Edition"
                Case ProductType2.PRODUCT_HOME_SERVER
                    Return "Home Server"
                Case ProductType2.PRODUCT_STORAGE_EXPRESS_SERVER
                    Return "Express Storage Server"
                Case ProductType2.PRODUCT_STORAGE_STANDARD_SERVER
                    Return "Standard Storage Server"
                Case ProductType2.PRODUCT_STORAGE_WORKGROUP_SERVER
                    Return "Workgroup Storage Server"
                Case ProductType2.PRODUCT_STORAGE_ENTERPRISE_SERVER
                    Return "Enterprise Storage Server"
                Case ProductType2.PRODUCT_SERVER_FOR_SMALLBUSINESS
                    Return "Windows Essential Server Solutions"
                Case ProductType2.PRODUCT_SMALLBUSINESS_SERVER_PREMIUM
                    Return "Small Business Server Premium Edition"
                Case ProductType2.PRODUCT_HOME_PREMIUM_N
                    Return "Home Premium Edition N"
                Case ProductType2.PRODUCT_ENTERPRISE_N
                    Return "Enterprise Edition N"
                Case ProductType2.PRODUCT_ULTIMATE_N
                    Return "Ultimate Edition N"
                Case ProductType2.PRODUCT_WEB_SERVER_CORE
                    Return "Web Server Edition (core installation)"
                Case ProductType2.PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT
                    Return "Windows Essential Business Management Server"
                Case ProductType2.PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY
                    Return "Windows Essential Business Security Server"
                Case ProductType2.PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING
                    Return "Windows Essential Business Messaging Server"
                Case ProductType2.PRODUCT_SERVER_FOUNDATION
                    Return "Server Foundation"
                Case ProductType2.PRODUCT_HOME_PREMIUM_SERVER
                    Return "Windows Home Server 2011"
                Case ProductType2.PRODUCT_SERVER_FOR_SMALLBUSINESS_V
                    Return "Windows Essential Server Solutions without Hyper-V"
                Case ProductType2.PRODUCT_STANDARD_SERVER_V
                    Return "Standard Server without Hyper-V"
                Case ProductType2.PRODUCT_DATACENTER_SERVER_V
                    Return "Server Datacenter without Hyper-V"
                Case ProductType2.PRODUCT_ENTERPRISE_SERVER_V
                    Return "Enterprise Server without Hyper-V"
                Case ProductType2.PRODUCT_DATACENTER_SERVER_CORE_V
                    Return "Server Datacenter without Hyper-V (core installation)"
                Case ProductType2.PRODUCT_STANDARD_SERVER_CORE_V
                    Return "Standard Server without Hyper-V (core installation)"
                Case ProductType2.PRODUCT_ENTERPRISE_SERVER_CORE_V
                    Return "Enterprise Server without Hyper-V (core installation)"
                Case ProductType2.PRODUCT_HYPERV
                    Return "Microsoft Hyper-V Server"
                Case ProductType2.PRODUCT_STORAGE_EXPRESS_SERVER_CORE
                    Return "Storage Server Express (core installation)"
                Case ProductType2.PRODUCT_STORAGE_STANDARD_SERVER_CORE
                    Return "Storage Server Standard (core installation)"
                Case ProductType2.PRODUCT_STORAGE_WORKGROUP_SERVER_CORE
                    Return "Storage Server Workgroup (core installation)"
                Case ProductType2.PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE
                    Return "Storage Server Enterprise (core installation)"
                Case ProductType2.PRODUCT_STARTER_N
                    Return "Starter N"
                Case ProductType2.PRODUCT_PROFESSIONAL
                    Return "Professional"
                Case ProductType2.PRODUCT_PROFESSIONAL_N
                    Return "Professional N"
                Case ProductType2.PRODUCT_SB_SOLUTION_SERVER
                    Return "Windows Small Business Server 2011 Essentials"
                Case ProductType2.PRODUCT_SERVER_FOR_SB_SOLUTIONS
                    Return "Server For SB Solutions"
                Case ProductType2.PRODUCT_STANDARD_SERVER_SOLUTIONS
                    Return "Server Solutions Premium"
                Case ProductType2.PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE
                    Return "Server Solutions Premium (core installation)"
                Case ProductType2.PRODUCT_SB_SOLUTION_SERVER_EM
                    Return "Server For SB Solutions EM"
                Case ProductType2.PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM
                    Return "Server For SB Solutions EM"
                Case ProductType2.PRODUCT_SOLUTION_EMBEDDEDSERVER
                    Return "Windows MultiPoint Server"
                Case ProductType2.PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT
                    Return "Windows Essential Server Solution Management"
                Case ProductType2.PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL
                    Return "Windows Essential Server Solution Additional"
                Case ProductType2.PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC
                    Return "Windows Essential Server Solution Management SVC"
                Case ProductType2.PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC
                    Return "Windows Essential Server Solution Additional SVC"
                Case ProductType2.PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE
                    Return "Small Business Server Premium (core installation)"
                Case ProductType2.PRODUCT_CLUSTER_SERVER_V
                    Return "Server Hyper Core V"
                Case ProductType2.PRODUCT_STARTER_E
                    Return "Starter E"
                Case ProductType2.PRODUCT_HOME_BASIC_E
                    Return "Home Basic E"
                Case ProductType2.PRODUCT_HOME_PREMIUM_E
                    Return "Home Premium E"
                Case ProductType2.PRODUCT_PROFESSIONAL_E
                    Return "Professional E"
                Case ProductType2.PRODUCT_ENTERPRISE_E
                    Return "Enterprise E"
                Case ProductType2.PRODUCT_ULTIMATE_E
                    Return "Ultimate E"
                Case ProductType2.PRODUCT_ENTERPRISE_EVALUATION
                    Return "Server Enterprise (evaluation installation)"
                Case ProductType2.PRODUCT_MULTIPOINT_STANDARD_SERVER
                    Return "Windows MultiPoint Server Standard"
                Case ProductType2.PRODUCT_MULTIPOINT_PREMIUM_SERVER
                    Return "Windows MultiPoint Server Premium"
                Case ProductType2.PRODUCT_STANDARD_EVALUATION_SERVER
                    Return "Server Standard (evaluation installation)"
                Case ProductType2.PRODUCT_DATACENTER_EVALUATION_SERVER
                    Return "Server Datacenter (evaluation installation)"
                Case ProductType2.PRODUCT_ENTERPRISE_N_EVALUATION
                    Return "Enterprise N (evaluation installation)"
                Case ProductType2.PRODUCT_STORAGE_WORKGROUP_EVALUATION_SERVER
                    Return "Storage Server Workgroup (evaluation installation)"
                Case ProductType2.PRODUCT_STORAGE_STANDARD_EVALUATION_SERVER
                    Return "Storage Server Standard (evaluation installation)"
                Case ProductType2.PRODUCT_CORE_N
                    Return "N" 'Windows 8 N
                Case ProductType2.PRODUCT_CORE_COUNTRYSPECIFIC
                    Return "China"  'Windows 8 China
                Case ProductType2.PRODUCT_CORE_SINGLELANGUAGE
                    Return "Single Language" 'Windows 8 Single Language
                Case ProductType2.PRODUCT_CORE
                    Return "" 'Windows 8
                Case ProductType2.PRODUCT_PROFESSIONAL_WMC
                    Return "Professional with Media Center"
                Case Else
                    Return String.Empty
            End Select
        End Function

        ''' <summary>
        ''' Product Type in a more understandable way
        ''' </summary>
        ''' <returns>"Home", "Enterprise"...</returns>
        ''' <remarks></remarks>
        Public Shared Function GetProductTypeStringSmart() As String
            Dim vProduct As String = GetProductTypeString()

            If GetMajorVersion() >= 6 Then
                vProduct = GetProductType2String(GetProductType2)
            Else
                Select Case GetProductType()
                    Case ProductType.VER_NT_WORKSTATION
                        If CheckSuiteMask(Suite.VER_SUITE_PERSONAL) Then
                            vProduct = "Personal"
                        Else
                            vProduct = "Professional"
                        End If
                    Case ProductType.VER_NT_SERVER
                        If CheckSuiteMask(Suite.VER_SUITE_ENTERPRISE) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_ENTERPRISE)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_BLADE) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_BLADE)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_COMPUTE_SERVER) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_COMPUTE_SERVER)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_DATACENTER) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_DATACENTER)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_WH_SERVER) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_WH_SERVER)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_SMALLBUSINESS) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_SMALLBUSINESS)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_SMALLBUSINESS_RESTRICTED) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_SMALLBUSINESS_RESTRICTED)
                        ElseIf CheckSuiteMask(Suite.VER_SUITE_STORAGE_SERVER) Then
                            vProduct = GetSuiteDescription(Suite.VER_SUITE_STORAGE_SERVER)
                        Else
                            vProduct = String.Empty
                        End If
                End Select
            End If

            Return vProduct
        End Function

        ''' <summary>
        ''' Suite Mask
        ''' </summary>
        ''' <returns>A number describing the features of Windows</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSuiteMask() As Integer
            Return OSVersionInformation.wSuiteMask
        End Function

        ''' <summary>
        ''' Suite Mask explained in comma separated words
        ''' </summary>
        ''' <returns>"Terminal, Remote Desktop Supported"</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSuiteMaskString() As String
            Dim vText As String = String.Empty

            CheckSuiteMask(Suite.VER_SUITE_SMALLBUSINESS, vText)
            CheckSuiteMask(Suite.VER_SUITE_ENTERPRISE, vText)
            CheckSuiteMask(Suite.VER_SUITE_BACKOFFICE, vText)
            CheckSuiteMask(Suite.VER_SUITE_TERMINAL, vText)
            CheckSuiteMask(Suite.VER_SUITE_SMALLBUSINESS_RESTRICTED, vText)
            CheckSuiteMask(Suite.VER_SUITE_EMBEDDEDNT, vText)
            CheckSuiteMask(Suite.VER_SUITE_DATACENTER, vText)
            CheckSuiteMask(Suite.VER_SUITE_SINGLEUSERTS, vText)
            CheckSuiteMask(Suite.VER_SUITE_PERSONAL, vText)
            CheckSuiteMask(Suite.VER_SUITE_BLADE, vText)
            CheckSuiteMask(Suite.VER_SUITE_STORAGE_SERVER, vText)
            CheckSuiteMask(Suite.VER_SUITE_COMPUTE_SERVER, vText)
            CheckSuiteMask(Suite.VER_SUITE_WH_SERVER, vText)

            Return vText
        End Function

        ''' <summary>
        ''' Descriptions for the suite mask positions
        ''' </summary>
        ''' <param name="pSuiteMask"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSuiteDescription(pSuiteMask As Suite) As String
            Select Case pSuiteMask
                Case Suite.VER_SUITE_SMALLBUSINESS
                    Return "Small business"
                Case Suite.VER_SUITE_ENTERPRISE
                    Return "Enterprise"
                Case Suite.VER_SUITE_BACKOFFICE
                    Return "BackOffice"
                Case Suite.VER_SUITE_TERMINAL
                    Return "Terminal"
                Case Suite.VER_SUITE_SMALLBUSINESS_RESTRICTED
                    Return "Small Business Restricted"
                Case Suite.VER_SUITE_EMBEDDEDNT
                    Return "Embedded"
                Case Suite.VER_SUITE_DATACENTER
                    Return "Datacenter"
                Case Suite.VER_SUITE_SINGLEUSERTS
                    Return "Remote Desktop Supported"
                Case Suite.VER_SUITE_PERSONAL
                    Return "Personal"
                Case Suite.VER_SUITE_BLADE
                    Return "Web Edition"
                Case Suite.VER_SUITE_STORAGE_SERVER
                    Return "Storage"
                Case Suite.VER_SUITE_COMPUTE_SERVER
                    Return "Compute Cluster Edition"
                Case Suite.VER_SUITE_WH_SERVER
                    Return "Home"
                Case Else
                    Return ""
            End Select
        End Function

        ''' <summary>
        ''' Adds a suite mask description if the specified suite mask is valid for this version of windows
        ''' </summary>
        ''' <param name="pSuiteMask"></param>
        ''' <param name="pText"></param>
        ''' <remarks></remarks>
        Private Shared Sub CheckSuiteMask(pSuiteMask As Suite, ByRef pText As String)
            If CheckSuiteMask(pSuiteMask) Then
                If String.IsNullOrEmpty(pText) Then
                    pText = GetSuiteDescription(pSuiteMask)
                Else
                    pText &= ", " & GetSuiteDescription(pSuiteMask)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Checks if the specified suite mask is valid for this version of windows
        ''' </summary>
        ''' <param name="pSuiteMask"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CheckSuiteMask(pSuiteMask As Suite) As Boolean
            Return (GetSuiteMask() And pSuiteMask) = pSuiteMask
        End Function

        ''' <summary>
        ''' Information about Windows in a user friendly form
        ''' </summary>
        ''' <returns>"Windows 2003 Server V5.2.3790 Service Pack 2"</returns>
        ''' <remarks></remarks>
        Public Shared Function GetOSVersionString() As String
            Dim vOperatingSystem As String
            Dim vPlatform As String = String.Empty
            Dim vProduct As String = String.Empty

            vPlatform = GetPlatformStringSmart()

            vProduct = GetProductTypeStringSmart()

            vOperatingSystem = vPlatform

            If Not String.IsNullOrEmpty(vProduct) Then
                vOperatingSystem &= " " & vProduct
            End If

            Return $"{vOperatingSystem} V{GetMajorVersion()}.{GetMinorVersion()}.{GetBuildNumber()} {GetServicePack()}"
        End Function

#End Region

#Region " Position Form "

        ''' <summary>
        ''' Centers a form on Screen
        ''' </summary>
        ''' <param name="pForm">The form to be centered</param>
        ''' <remarks></remarks>
        Public Shared Sub CenterForm(pForm As Form)
            PositionForm(pForm, SUFormPos.SUCenter)
        End Sub

        ''' <summary>
        ''' Positions a form on the screen
        ''' </summary>
        ''' <param name="pForm">The form to be positioned</param>
        ''' <param name="pAlignment">Alignment</param>
        ''' <param name="pOffsetFromScreenEdge">Offset</param>
        ''' <remarks></remarks>
        Public Shared Sub PositionForm(pForm As Form, pAlignment As SUFormPos, Optional pOffsetFromScreenEdge As Integer = 40)
            Dim vRect As Rectangle
            Dim vPoint As Point

            vRect = Screen.GetWorkingArea(vPoint)

            Select Case pAlignment
                Case SUFormPos.SUUpperLeft
                    pForm.SetBounds(vRect.Left + pOffsetFromScreenEdge, vRect.Top + pOffsetFromScreenEdge, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
                Case SUFormPos.SUUpperRight
                    pForm.SetBounds(vRect.Width - vRect.Left - pForm.Width - pOffsetFromScreenEdge, vRect.Top + pOffsetFromScreenEdge, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
                Case SUFormPos.SULowerLeft
                    pForm.SetBounds(vRect.Left + pOffsetFromScreenEdge, vRect.Height - vRect.Top - pForm.Height - pOffsetFromScreenEdge, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
                Case SUFormPos.SULowerRight
                    pForm.SetBounds(vRect.Width - vRect.Left - pForm.Width - pOffsetFromScreenEdge, vRect.Height - vRect.Top - pForm.Height - pOffsetFromScreenEdge, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
                Case SUFormPos.SUCenter
                    pForm.SetBounds((vRect.Width - pForm.Width) \ 2, (vRect.Height - pForm.Height) \ 2, 0, 0, BoundsSpecified.X Or BoundsSpecified.Y)
            End Select
        End Sub

        ''' <summary>
        ''' Shrinks a form to fit the working area of the screen
        ''' </summary>
        ''' <param name="pForm"></param>
        ''' <remarks></remarks>
        Public Shared Sub ShrinkSizeToWorkingArea(pForm As Form)
            Try
                Dim vWorkingArea As Rectangle
                Dim vPoint As Point

                vWorkingArea = Screen.GetWorkingArea(vPoint)

                If pForm.Width > vWorkingArea.Width Then
                    pForm.Width = Math.Max(vWorkingArea.Width, pForm.MinimumSize.Width)

                    pForm.Left = 0
                End If

                If pForm.Height > vWorkingArea.Height Then
                    pForm.Height = Math.Max(vWorkingArea.Height, pForm.MinimumSize.Height)

                    pForm.Top = 0
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Sub

#End Region

#Region " User and machine information "

        ''' <summary>
        ''' Returns the current UserID
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserID() As String
            Try
                Return SystemInformation.UserName()
            Catch
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' Returns the current UserDomain
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserDomain() As String
            Try
                Return SystemInformation.UserDomainName
            Catch
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Returns the current computername
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetMachineName() As String
            Try
                Return SystemInformation.ComputerName
            Catch
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' System uptime
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpTime() As String
            Dim vNow As Long = Environment.TickCount

            'The value of this property is derived from the system timer and is stored as a 32-bit signed integer. 
            'Consequently, if the system runs continuously, TickCount will increment from zero to Int32.MaxValue for approximately 24.9 days, 
            'then jump to Int32.MinValue, which is a negative number, then increment back to zero during the next 24.9 days.

            If vNow < 0 Then
                vNow += CLng(Integer.MaxValue) * 2
            End If

            Return FormatMilliSeconds(vNow)
        End Function

        ''' <summary>
        ''' Formats milliseconds to days, hours, minutes and seconds
        ''' </summary>
        ''' <param name="pMilliSeconds"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormatMilliSeconds(pMilliSeconds As Long) As String
            Dim vSec = pMilliSeconds \ 1000

            Dim vMin = vSec \ 60
            vSec = vSec Mod 60

            Dim vHr = vMin \ 60
            vMin = vMin Mod 60

            Dim vDays = vHr \ 24
            vHr = vHr Mod 24

            Dim vText As String = String.Empty

            If vDays <> 0 Then
                vText = $"{vDays} days "
            End If

            vText &= $"{Format(vHr, "00")}:{Format(vMin, "00")}:{Format(vSec, "00")}"

            Return vText
        End Function

#End Region

#Region " Exit Windows "

        ''' <summary>
        ''' Logs off current user
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub LogOffWindows()
            ExitWindows(EWX_LOGOFF, False)
        End Sub

        ''' <summary>
        ''' Shut's down Windows
        ''' </summary>
        ''' <param name="pForce"></param>
        ''' <remarks></remarks>
        Public Shared Sub ShutDownWindows(Optional pForce As Boolean = False)
            AdjustToken()

            ExitWindows(EWX_SHUTDOWN, pForce)
        End Sub

        ''' <summary>
        ''' Restart's Windows
        ''' </summary>
        ''' <param name="pForce"></param>
        ''' <remarks></remarks>
        Public Shared Sub RestartWindows(Optional pForce As Boolean = False)
            AdjustToken()

            ExitWindows(EWX_REBOOT, pForce)
        End Sub

        Private Shared Sub ExitWindows(pCommand As Integer, pForce As Boolean)
            Dim vForceCode As Integer

            If pForce Then
                vForceCode = &HFFFFS
            Else
                vForceCode = 0
            End If

            ExitWindowsEx(pCommand, vForceCode)
        End Sub

        Private Shared Sub AdjustToken()
            Const TOKEN_ADJUST_PRIVILEGES = &H20S
            Const TOKEN_QUERY = &H8S
            Const SE_PRIVILEGE_ENABLED = &H2S

            Dim hdlProcessHandle As Integer
            Dim hdlTokenHandle As Integer
            Dim tmpLuid As LUID
            Dim tkp As TOKEN_PRIVILEGES
            Dim tkpNewButIgnored As TOKEN_PRIVILEGES
            Dim lBufferNeeded As Integer

            hdlProcessHandle = Process.GetCurrentProcess.Id
            OpenProcessToken(hdlProcessHandle, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, hdlTokenHandle)
            ' Get the LUID for shutdown privilege.
            LookupPrivilegeValue("", "SeShutdownPrivilege", tmpLuid)
            tkp.PrivilegeCount = 1   ' One privilege to set
            tkp.TheLuid = tmpLuid
            tkp.Attributes = SE_PRIVILEGE_ENABLED
            ' Enable the shutdown privilege in the access token of this process.
            AdjustTokenPrivileges(hdlTokenHandle, 0, tkp, Len(tkpNewButIgnored), tkpNewButIgnored, lBufferNeeded)
        End Sub

#End Region

#Region " Change behaviours of windows "

        ''' <summary>
        ''' Shows/Hides the taskbar
        ''' </summary>
        ''' <param name="pShow"></param>
        ''' <remarks></remarks>
        Public Shared Sub Taskbar(pShow As Boolean)
            Dim vTrayHandle As Integer
            Dim vShowHide As Integer

            vTrayHandle = FindWindow("Shell_traywnd", "")

            If pShow = True Then
                vShowHide = SHOWWINDOW
            Else
                vShowHide = HIDEWINDOW
            End If

            SetWindowPos(vTrayHandle, 0, 0, 0, 0, 0, vShowHide)
        End Sub

        ''' <summary>
        ''' Window always on top
        ''' </summary>
        ''' <param name="pForm"></param>
        ''' <param name="pMode"></param>
        ''' <remarks></remarks>
        <Obsolete("Use TopMost")>
        Public Shared Sub AlwaysOnTop(pForm As Form, pMode As Boolean)
            pForm.TopMost = pMode
        End Sub

#End Region

#Region " Wait "

        ''' <summary>
        ''' Let's the System handle the waiting for you
        ''' </summary>
        ''' <param name="pMilliseconds"></param>
        ''' <remarks></remarks>
        Public Shared Sub Wait(pMilliseconds As Integer)
            Thread.Sleep(pMilliseconds)
        End Sub

#End Region

#Region " EncipherString "

        ''' <summary>
        ''' The function returns a enciphered string. Do not use this function
        ''' if it is very vulnerable information.
        ''' To decypher the string just use the encipher function again.
        ''' The function is borrowed from Visual Basic Developer, Dec 1998.
        ''' Example of use:
        ''' strCipherText = objEncipher.EncipherString(strPlainTest, strKeyText)
        ''' </summary>
        ''' <param name="pText"></param>
        ''' <param name="pKey"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function EncipherString(pText As String, pKey As String) As String
            ' Compute the random lngNumber generator's
            ' initiator for this key
            Dim vSeed = 0
            Dim vShift17 = 17
            Dim vShift9 = 0

            For i = 1 To Len(pKey)
                Dim vNum = Asc(Mid(pKey, i, 1))
                vSeed = vSeed Xor CInt(vNum * (2 ^ vShift17)) Xor CInt(vNum * (2 ^ vShift9))
                vShift17 = (vShift17 + 17) Mod 24
                vShift9 = (vShift9 + 9) Mod 24
            Next i

            ' Initialize the random generator
            Dim vRandom = CInt(Rnd(-1))
            Randomize(vSeed)

            Dim vSB As New StringBuilder

            For i = 1 To Len(pText)
                Dim vPch = Asc(Mid(pText, i, 1))
                Dim vKch = CInt(Int((255 + 1) * Rnd()))
                vSB.Append(Chr(vPch Xor vKch))
            Next i

            ' Send back the string
            Return vSB.ToString
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
        ''' <remarks></remarks>
        Public Shared Function GetDayOfWeek(pYear As Integer, pMonth As Integer, pDay As Integer) As DayOfWeek
            Return New DateTime(pYear, pMonth, pDay).DayOfWeek
        End Function

#End Region

    End Class
End Namespace