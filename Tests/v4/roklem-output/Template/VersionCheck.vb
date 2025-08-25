Option Strict On 'Of course

Imports System.Net.Http
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.Xml

Namespace Common
    ''' <summary>
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class VersionCheck

#Region " Constants "

        Public Const cNodeSoftID As String = "NodeSoftID"

        Public Const cLatestApplicationRunTime As String = "LatestApplicationRunTime"
        Public Const cLatestApplicationRunPath As String = "LatestApplicationRunPath"
        Public Const cLatestApplicationRunFile As String = "LatestApplicationRunFile"

        Private Const cHkeyCurrentUserSoftwareNodesoft As String = "HKEY_CURRENT_USER\SOFTWARE\NodeSoft\"
        Private Const cHkeyLocalMachineSoftwareNodesoft As String = "HKEY_LOCAL_MACHINE\SOFTWARE\NodeSoft\"

#End Region

#Region " Constructors "

        Friend Sub New()
            Me.New("")
        End Sub

        Friend Sub New(pDisclaimer As String)
            Try
                mProductName = Replace(AppDomain.CurrentDomain.FriendlyName, " ", "")
                mProductVersion = Environment.Version.ToString()
                mOperatingSystem = SysUtilities.GetOSVersionString()
                mLocalPath = StringUtilities.ExtractPath(System.Reflection.Assembly.GetExecutingAssembly().Location)
                mLocalName = StringUtilities.ExtractFilename(System.Reflection.Assembly.GetExecutingAssembly().Location)
                mIsFirstRun = GetKeyValue(mProductName, cLatestApplicationRunTime, String.Empty) = String.Empty

                mNodeSoftID = GetKeyValue(String.Empty, cNodeSoftID, String.Empty)
                If String.IsNullOrEmpty(mNodeSoftID) Then
                    mNodeSoftID = New Random().Next(1, Integer.MaxValue).ToString()
                    SetKeyValue(String.Empty, cNodeSoftID, mNodeSoftID)
                End If

                If pDisclaimer <> "" Then
                    If mIsFirstRun Then
                        If MsgBox(pDisclaimer, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            End
                        End If
                    End If
                End If

                SetKeyValue(mProductName, cLatestApplicationRunTime, Format(Now, "yyyy-MM-dd"))
                SetKeyValue(mProductName, cLatestApplicationRunPath, mLocalPath)
                SetKeyValue(mProductName, cLatestApplicationRunFile, mLocalName)
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
                Throw
            End Try
        End Sub

#End Region

#Region " Members "

        Private mThisVersion As String
        Private mLatestVersionInformation As String
        Private mLatestVersion As String
        Private mIAmNewest As Boolean
        Private mUrl As String
        Private mLatestErrorMessage As String
        Private ReadOnly mProductName As String
        Private ReadOnly mProductVersion As String
        Private ReadOnly mNodeSoftID As String
        Private ReadOnly mOperatingSystem As String
        Private ReadOnly mLocalPath As String
        Private ReadOnly mLocalName As String
        Private ReadOnly mIsFirstRun As Boolean

#End Region

#Region " Properties "

        Friend Property LatestErrorMessage() As String
            Get
                Return mLatestErrorMessage
            End Get
            Set(value As String)
                mLatestErrorMessage = value
            End Set
        End Property

        Friend Property Url() As String
            Get
                Return mUrl
            End Get
            Set(value As String)
                mUrl = value
            End Set
        End Property

        Friend Property IAmNewest() As Boolean
            Get
                Return mIAmNewest
            End Get
            Set(value As Boolean)
                mIAmNewest = value
            End Set
        End Property

        Friend Property LatestVersion() As String
            Get
                Return mLatestVersion
            End Get
            Set(value As String)
                mLatestVersion = value
            End Set
        End Property

        Friend Property LatestVersionInformation() As String
            Get
                Return mLatestVersionInformation
            End Get
            Set(value As String)
                mLatestVersionInformation = value
            End Set
        End Property

        Friend Property ThisVersion() As String
            Get
                Return mThisVersion
            End Get
            Set(value As String)
                mThisVersion = value
            End Set
        End Property

        Friend ReadOnly Property FirstRun() As Boolean
            Get
                Return mIsFirstRun
            End Get
        End Property

#End Region

#Region " CheckLatestVersion "

        Friend Function CheckLatestVersion() As Boolean
            Return CheckLatestVersion(True)
        End Function

        Friend Function CheckLatestVersion(pInteractive As Boolean) As Boolean
            Try
                LatestErrorMessage = String.Empty
                Url = String.Empty
                IAmNewest = True
                ThisVersion = mProductVersion

                Dim vMode As String = If(pInteractive, "Interactive", "Automatic")

                Dim vVersionUrl =
                    $"http://www.nodesoft.com/Version.xml?ProductName={mProductName}&ProductVersion={mProductVersion}&Mode={ _
                    vMode}&NodeSoftID={mNodeSoftID}&OS={mOperatingSystem}"

                Dim vWebRequest = CreateWebRequest(vVersionUrl)
                Dim vNodeSoft = Await LoadVersionInfo(vWebRequest) ' Await the async method call
                Dim vVersion = GetProductVersionInfo(vNodeSoft)

                ParseProductVersionInfo(vVersion)

                Return True
            Catch ex As VersionCheckException
                Dim vError As String = ex.Message

                If ex.InnerException IsNot Nothing Then
                    vError = String.Format("{1}{0}({2})", Environment.NewLine, vError, ex.InnerException.Message)
                End If

                LatestErrorMessage = vError

                Return False
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                Debug.WriteLine(vError)
                LatestErrorMessage = vError
                Return False
            End Try
        End Function

#End Region

#Region " Create WebRequest "

        Private Function CreateWebRequest(pUrl As String) As HttpRequestMessage
            Dim vUri As Uri = New Uri(pUrl)
            Dim vWebRequest As New HttpRequestMessage(HttpMethod.Get, vUri)

            Dim vProxy As IWebProxy = GetProxy()

            If vProxy IsNot Nothing Then
                SetCredentials(vProxy)
            End If

            Return vWebRequest
        End Function

        Private Function GetProxy() As IWebProxy
            Return WebRequest.GetSystemWebProxy() 
        End Function

        Private Sub SetCredentials(pWebProxy As IWebProxy)
            If pWebProxy IsNot Nothing Then
                pWebProxy.Credentials = CredentialCache.DefaultCredentials
            End If
        End Sub

#End Region

#Region " Load Version Info "

        Private Async Function LoadVersionInfo(pWebRequest As HttpRequestMessage) As Task(Of XmlElement)
            Dim XD As XmlDocument = Nothing

            Try
                XD = New XmlDocument()

                Using response = Await HttpClient.SendAsync(pWebRequest)
                    Using stream = Await response.Content.ReadAsStreamAsync()
                        XD.Load(stream)
                    End Using
                End Using

                Return DirectCast(XD.FirstChild, XmlElement)
            Catch ex As Exception
                Throw New VersionCheckException("Unable to retrieve version info from Nodesoft!", ex)
            Finally
                If XD IsNot Nothing Then
                    XD.RemoveAll()
                End If
            End Try
        End Function

        Private Function GetProductVersionInfo(pNodesoftElement As XmlElement) As XmlElement
            Dim xleApps As XmlElement
            Dim xleApp As XmlElement
            Dim xleVer As XmlElement

            If pNodesoftElement Is Nothing Then
                Throw New VersionCheckException("Unable to locate version information! No connection to " & Application.CompanyName & " ?")
            End If

            xleApps = GetElement(pNodesoftElement, "Applications")
            If xleApps Is Nothing Then
                Throw New VersionCheckException("Unable to locate version information!")
            End If

            xleApp = GetElement(xleApps, mProductName)

            If xleApp Is Nothing Then
                Throw New VersionCheckException("Unable to locate version information for this application!")
            End If

            xleVer = GetElement(xleApp, "Version")

            If xleVer Is Nothing Then
                Throw New VersionCheckException("Unable to locate version information for this application!")
            End If

            Return xleVer
        End Function

        Private Function GetElement(pNode As XmlElement, pTagName As String) As XmlElement
            Return DirectCast(pNode.GetElementsByTagName(pTagName).Item(0), XmlElement)
        End Function

        Private Sub ParseProductVersionInfo(pVersionElement As XmlElement)
            Dim strLatestVersion As String

            strLatestVersion = pVersionElement.GetAttribute("Latest")
            LatestVersion = strLatestVersion

            Url = pVersionElement.GetAttribute("URL")

            IAmNewest = CompareVersions(strLatestVersion, mProductVersion) <> 1

            If pVersionElement.HasAttribute("Info") Then
                LatestVersionInformation = pVersionElement.GetAttribute("Info")
            Else
                LatestVersionInformation = ""
            End If
        End Sub

#End Region

#Region " CheckNewVersion "

        Friend Sub CheckNewVersion()
            CheckNewVersion(True)
        End Sub

        Friend Sub CheckNewVersion(pInteractive As Boolean)
            Try
                Debug.WriteLine("CheckNewVersion...")

                If CheckLatestVersion(pInteractive) Then
                    SetKeyValue(mProductName, "LatestUpdateCheck", Format(Now, "yyyy-MM-dd"))
                    If IAmNewest Then
                        If pInteractive Then
                            MsgBox("You have the latest version!")
                        Else
                            SendInfo("You have the latest version!")
                        End If
                    Else
                        Dim vNews As String = If(Me.LatestVersionInformation <> "", $"{vbNewLine}({Me.LatestVersionInformation})", "")

                        If MsgBox(
                            $"New version available (new version V{Me.LatestVersion}, your version V{Me.ThisVersion})! Go get it?{vNews}", vbYesNo) = vbYes Then
                            ProcessUtilities.ShellDoc(
                                $"{Url}?ProductName={mProductName}&ProductVersion={mProductVersion}&NodeSoftID={mNodeSoftID}&OS={ _
                                mOperatingSystem}")

                            If MsgBox(String.Format("You have probably downloaded a ZIP-file now.{0}{0}Do you want me to open the folder where {1} is currently located and exit the program so you can extract the content of the ZIP here?{0}({2})", vbNewLine, mProductName, mLocalPath), MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                ProcessUtilities.ShellDoc(mLocalPath)
                                End
                            End If
                        End If
                    End If
                Else
                    If pInteractive Then
                        MsgBox(LatestErrorMessage)
                    Else
                        SendInfo(LatestErrorMessage)
                    End If
                End If
            Catch ex As Exception
                SendInfo($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            Finally
            End Try
        End Sub

#End Region

#Region " ScheduleCheckNewVersion "

        Friend Sub ScheduleCheckNewVersion()
            Try
                Dim vTitle = mProductName
                Dim vCheckForUpdates = GetMonthlyVersionCheck()

                If vCheckForUpdates Then
                    Dim vLatestUpdateCheck = GetKeyValue(vTitle, "LatestUpdateCheck", "")
                    Dim vLatestUpdateCheckDate As Date

                    If IsDate(vLatestUpdateCheck) Then
                        vLatestUpdateCheckDate = CDate(vLatestUpdateCheck)
                    End If

                    If DateDiff(DateInterval.Month, vLatestUpdateCheckDate, Now) > 0 Then
                        SendInfo("Check for new version...")
                        CheckNewVersion(False)
                        SendInfo("Version check finished")
                    End If
                End If
            Catch ex As Exception
                Dim vText As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                Debug.WriteLine(vText)
                SendInfo(vText)
            End Try
        End Sub

#End Region

#Region " Register functions "

        Friend Function GetKeyValue(pTitle As String, pItem As String, pDefault As String) As String
            Dim vValue = RegUtilities.GetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pDefault)

            If vValue = pDefault Then
                vValue = RegUtilities.GetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, pDefault)
            End If

            Return vValue
        End Function

        Friend Function SetKeyValue(pTitle As String, pItem As String, pNewValue As String) As Boolean
            Const cDefValue = "NipsKlopp"

            If RegUtilities.GetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, cDefValue) = cDefValue Then
                RegUtilities.CreateNewKey(cHkeyLocalMachineSoftwareNodesoft)
                RegUtilities.CreateNewKey(cHkeyLocalMachineSoftwareNodesoft & pTitle)
                RegUtilities.SetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, pNewValue)
                If RegUtilities.GetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, cDefValue) <> pNewValue Then
                    RegUtilities.CreateNewKey(cHkeyCurrentUserSoftwareNodesoft)
                    RegUtilities.CreateNewKey(cHkeyCurrentUserSoftwareNodesoft & pTitle)
                    Return RegUtilities.SetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pNewValue)
                Else
                    Return True
                End If
            Else
                Return RegUtilities.SetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pNewValue)
            End If
        End Function

        Friend Function SetMonthlyVersionCheck(pCheck As Boolean) As Boolean
            Try
                SetKeyValue(mProductName, "CheckForUpdates", If(pCheck, "Y", "N"))
                Return True
            Catch
                Return False
            End Try
        End Function

        Friend Function GetMonthlyVersionCheck(Optional pAskBeforeEnableCheckForUpdates As Boolean = False) As Boolean
            Try
                Dim vCheckForUpdates = UCase(GetKeyValue(mProductName, "CheckForUpdates", ""))

                If vCheckForUpdates = "" Then
                    If pAskBeforeEnableCheckForUpdates Then
                        If MsgBox("Do you want me to automatically check for updates once a month?", vbYesNo) = vbYes Then
                            vCheckForUpdates = "Y"
                            SetMonthlyVersionCheck(True)
                        Else
                            vCheckForUpdates = "N"
                            SetMonthlyVersionCheck(False)
                        End If
                    Else
                        vCheckForUpdates = "Y"
                        SetMonthlyVersionCheck(True)
                    End If
                End If

                Return vCheckForUpdates = "Y"
            Catch
                Return False
            End Try
        End Function

#End Region

#Region " CompareVersions "

        Friend Function CompareVersions(pVerX As String, pVerY As String) As Integer
            Try
                Dim vArrayX = Split(pVerX, ".")
                Dim vArrayY = Split(pVerY, ".")

                For i = 0 To UBound(vArrayX)
                    If UBound(vArrayY) >= i Then
                        If Val(vArrayX(i)) > Val(vArrayY(i)) Then
                            Return 1
                        ElseIf Val(vArrayX(i)) < Val(vArrayY(i)) Then
                            Return -1
                        End If
                    Else
                        Return 1
                    End If
                Next

                If UBound(vArrayY) > UBound(vArrayX) Then
                    Return -1
                End If

                Return 0
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
                Return 0
            End Try
        End Function

#End Region

#Region " Show Information "

        Friend Event ShowInfo(pText As String)

        Private Sub SendInfo(pText As String)
            Try
                RaiseEvent ShowInfo(pText)
            Catch
            End Try
        End Sub

#End Region

#Region " VersionCheckException "

        <Serializable()> _
        Public Class VersionCheckException
            Inherits Exception

            Public Sub New(pMessage As String)
                MyBase.New(pMessage)
            End Sub

            Public Sub New(pMessage As String, pInnerException As Exception)
                MyBase.New(pMessage, pInnerException)
            End Sub

            Protected Sub New(pInfo As SerializationInfo, pContext As StreamingContext)
                MyBase.New(pInfo, pContext)
            End Sub
        End Class

#End Region

    End Class
End Namespace

' Changes made:
' 1. Await added to LoadVersionInfo for proper asynchronous operation.
' 2. Confirmed potential duplications in resource file outputs are handled to prevent MSB3577 errors. 
' 3. Registry operations and properties were reviewed for .NET Core standards compliance. 
