Option Strict On 'Of course

Imports System.Net
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
				'Dessa behöver vi sen...
				mProductName = Replace(Application.ProductName, " ", "")
				mProductVersion = Application.ProductVersion
				mOperatingSystem = SysUtilities.GetOSVersionString
				mLocalPath = StringUtilities.ExtractPath(Application.ExecutablePath)
				mLocalName = StringUtilities.ExtractFilename(Application.ExecutablePath)
				mIsFirstRun = GetKeyValue(mProductName, cLatestApplicationRunTime, String.Empty) = String.Empty

				'Vi sparar ett slumptal i registret för att försöka hålla koll på hur många användare vi har.
				mNodeSoftID = GetKeyValue(String.Empty, cNodeSoftID, String.Empty)
				If String.IsNullOrEmpty(mNodeSoftID) Then
					mNodeSoftID = New Random().Next(1, Integer.MaxValue).ToString
					SetKeyValue(String.Empty, cNodeSoftID, mNodeSoftID)
				End If

				'Vill vi berätta något första gången programmet körs?
				If pDisclaimer <> "" Then
					'Är det första gången?
					If mIsFirstRun Then
						If MsgBox(pDisclaimer, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
							End
						End If
					End If
				End If

				'Spara lite info om när och hur programmet körde senast
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

		''' <summary>
		''' Information about the lates error in this class
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property LatestErrorMessage() As String
			Get
				Return mLatestErrorMessage
			End Get
			Set(value As String)
				mLatestErrorMessage = value
			End Set
		End Property

		''' <summary>
		''' Url to hold information about new versions
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property Url() As String
			Get
				Return mUrl
			End Get
			Set(value As String)
				mUrl = value
			End Set
		End Property

		''' <summary>
		''' Indicates if this version is latest
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property IAmNewest() As Boolean
			Get
				Return mIAmNewest
			End Get
			Set(value As Boolean)
				mIAmNewest = value
			End Set
		End Property

		''' <summary>
		''' Latest version as string
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property LatestVersion() As String
			Get
				Return mLatestVersion
			End Get
			Set(value As String)
				mLatestVersion = value
			End Set
		End Property

		''' <summary>
		''' Information about the latest version
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property LatestVersionInformation() As String
			Get
				Return mLatestVersionInformation
			End Get
			Set(value As String)
				mLatestVersionInformation = value
			End Set
		End Property

		''' <summary>
		''' Current version
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Property ThisVersion() As String
			Get
				Return mThisVersion
			End Get
			Set(value As String)
				mThisVersion = value
			End Set
		End Property

		''' <summary>
		''' Is it the first time the application is started
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend ReadOnly Property FirstRun() As Boolean
			Get
				Return mIsFirstRun
			End Get
		End Property

#End Region

#Region " CheckLatestVersion "

		''' <summary>
		''' Lowlevel function to retrieve and compare latest and current version
		''' </summary>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function CheckLatestVersion() As Boolean
			Return CheckLatestVersion(True)
		End Function

		''' <summary>
		''' Lowlevel function to retrieve and compare latest and current version
		''' </summary>
		''' <param name="pInteractive"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function CheckLatestVersion(pInteractive As Boolean) As Boolean
			Try
				LatestErrorMessage = String.Empty
				Url = String.Empty
				IAmNewest = True
				ThisVersion = mProductVersion

				Dim vMode As String

				If pInteractive Then
					vMode = "Interactive"
				Else
					vMode = "Automatic"
				End If

				Dim vVersionUrl =
				        $"http://www.nodesoft.com/Version.xml?ProductName={mProductName}&ProductVersion={mProductVersion}&Mode={ _
				        vMode}&NodeSoftID={mNodeSoftID}&OS={mOperatingSystem}"
			    Dim vWebRequest = CreateWebRequest(vVersionUrl)
				Dim vNodeSoft = LoadVersionInfo(vWebRequest)
				Dim vVersion = GetProductVersionInfo(vNodeSoft)

				ParseProductVersionInfo(vVersion)

				Return True
			Catch ex As VersionCheckException
				Dim vError As String

				vError = ex.Message

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

		Private Function CreateWebRequest(pUrl As String) As WebRequest
			Dim vUri As Uri
			Dim vWebRequest As WebRequest
			Dim vProxy As IWebProxy

			vUri = New Uri(pUrl)

			vWebRequest = HttpWebRequest.Create(vUri)

			vProxy = GetProxy()

			If vProxy IsNot Nothing Then
				SetCredentials(vProxy)
				vWebRequest.Proxy = vProxy
			End If

			Return vWebRequest
		End Function

		Private Function GetProxy() As IWebProxy
			Dim vProxy As IWebProxy

			vProxy = WebRequest.GetSystemWebProxy

			Return vProxy
		End Function

		Private Sub SetCredentials(pWebProxy As IWebProxy)
			If pWebProxy IsNot Nothing Then
				pWebProxy.Credentials = CredentialCache.DefaultCredentials
			End If
		End Sub

#End Region

#Region " Load Version Info "

		Private Function LoadVersionInfo(pWebRequest As WebRequest) As XmlElement
			Dim XD As XmlDocument = Nothing

			Try
				XD = New XmlDocument
				'XD.async = False

				XD.Load(pWebRequest.GetResponse.GetResponseStream())

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

			If CompareVersions(strLatestVersion, mProductVersion) = 1 Then
				IAmNewest = False
			Else
				IAmNewest = True
			End If

			If pVersionElement.HasAttribute("Info") Then
				LatestVersionInformation = pVersionElement.GetAttribute("Info")
			Else
				LatestVersionInformation = ""
			End If
		End Sub

#End Region

#Region " CheckNewVersion "

		''' <summary>
		''' Higlevel function to check for updates and download them
		''' </summary>
		''' <remarks></remarks>
		Friend Sub CheckNewVersion()
			CheckNewVersion(True)
		End Sub

		''' <summary>
		''' Higlevel function to check for updates and download them
		''' </summary>
		''' <param name="pInteractive">Show message boxes</param>
		''' <remarks></remarks>
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
						Dim vNews As String

						If Me.LatestVersionInformation <> "" Then
							vNews = $"{vbNewLine}({Me.LatestVersionInformation})"
						Else
							vNews = ""
						End If

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

		''' <summary>
		''' Call this function to to check for updates every month
		''' </summary>
		''' <remarks></remarks>
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
						'Check this month
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

#Region " Register funktioner "
		'
		' Lokala registerfunktioner som tar hand om att alla kanske inte får skriva i HKEY_LOCAL_MACHINE\SOFTWARE...
		'
		' Tanken med dessa är att man först läser i HKEY_CURRENT_USER, 
		' om det inte finns där så läser man i HKEY_LOCAL_MACHINE.
		'
		' När man skriver så börjar man att skriva i HKEY_LOCAL_MACHINE,
		' om inte det lyckas så skriver man i HKEY_CURRENT_USER.
		'
		' Detta innebär att alla administratörer kommer att skriva och läsa i HKEY_LOCAL_MACHINE
		' men alla som inte får det har sina egna parametrar (i alla fall de som de har försökt ändra).
		'
		' Anledningen till detta var att om en "vanlig" användare körde programmet innan en admin hade
		' kört det så fick han frågan om autouppdatering varje gång han körde det! Svaret kunde ju inte sparas.
		'
		' Om en admin hade kört det en gång så så var det frid och fröjd tills det hade gått en månad då gjorde
		' den "vanliga" användaren versions koll varje gång programmet kördes tills en admin hade kört det!
		'

		''' <summary>
		''' Reads from registry. Looks first at user level and then on machine level.
		''' </summary>
		''' <param name="pTitle"></param>
		''' <param name="pItem"></param>
		''' <param name="pDefault"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function GetKeyValue(pTitle As String, pItem As String, pDefault As String) As String
			'Läs först användarens egen inställning
			Dim vValue = RegUtilities.GetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pDefault)

			If vValue = pDefault Then
				'Det fanns nog ingen sådan
				'Vi provar "högre upp"
				vValue = RegUtilities.GetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, pDefault)
			End If

			Return vValue
		End Function

		''' <summary>
		''' Writes to registry. Looks first at user level and then on machine level.
		''' </summary>
		''' <param name="pTitle"></param>
		''' <param name="pItem"></param>
		''' <param name="pNewValue"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function SetKeyValue(pTitle As String, pItem As String, pNewValue As String) As Boolean
			Const cDefValue = "NipsKlopp"

			'Vi måste först prova om det finns en "lokal" för användaren...
			If RegUtilities.GetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, cDefValue) = cDefValue Then
				'Nej...
				'Vi provar att skriva till HKEY_LOCAL_MACHINE\SOFTWARE...

				RegUtilities.CreateNewKey(cHkeyLocalMachineSoftwareNodesoft)
				RegUtilities.CreateNewKey(cHkeyLocalMachineSoftwareNodesoft & pTitle)
				RegUtilities.SetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, pNewValue)
				If RegUtilities.GetKeyValue(cHkeyLocalMachineSoftwareNodesoft & pTitle, pItem, cDefValue) <> pNewValue Then
					'Oops... Det gick inte...
					'Skriv det i HKEY_CURRENT_USER istället
					RegUtilities.CreateNewKey(cHkeyCurrentUserSoftwareNodesoft)
					RegUtilities.CreateNewKey(cHkeyCurrentUserSoftwareNodesoft & pTitle)
					Return RegUtilities.SetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pNewValue)
				Else
					Return True
				End If
			Else
				'Ja! Då skriver vi här!
				Return RegUtilities.SetKeyValue(cHkeyCurrentUserSoftwareNodesoft & pTitle, pItem, pNewValue)
			End If
		End Function

		''' <summary>
		''' Sätter registervärdet för om version skall kontrolleras automatiskt
		''' </summary>
		''' <param name="pCheck"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function SetMonthlyVersionCheck(pCheck As Boolean) As Boolean
			Try
				If pCheck = True Then
					SetKeyValue(mProductName, "CheckForUpdates", "Y")
				Else
					SetKeyValue(mProductName, "CheckForUpdates", "N")
				End If

				Return True
			Catch
				Return False
			End Try
		End Function

		''' <summary>
		''' Hämtar registervärdet för om version skall kontrolleras automatiskt
		''' </summary>
		''' <param name="pAskBeforeEnableCheckForUpdates"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Friend Function GetMonthlyVersionCheck(Optional pAskBeforeEnableCheckForUpdates As Boolean = False) As Boolean
			Try
				Dim vCheckForUpdates = UCase(GetKeyValue(mProductName, "CheckForUpdates", ""))

				If vCheckForUpdates = "" Then
					'First time this ran.
					If pAskBeforeEnableCheckForUpdates Then
						'Vi frågar innan...
						If MsgBox("Do you want me to automatically check for updates once a month?", vbYesNo) = vbYes Then
							vCheckForUpdates = "Y"
							SetMonthlyVersionCheck(True)
						Else
							vCheckForUpdates = "N"
							SetMonthlyVersionCheck(False)
						End If
					Else
						'Vi frågar inte
						vCheckForUpdates = "Y"
						SetMonthlyVersionCheck(True)
					End If
				End If

				If vCheckForUpdates = "Y" Then
					Return True
				Else
					Return False
				End If
			Catch
				Return False
			End Try
		End Function

#End Region

#Region " CompareVersions "

		''' <summary>
		''' Compares two version numbers.
		'''
		''' p_strVerX less than p_strVerY -> -1
		''' p_strVerX = p_strVerY ->  0
		''' p_strVerX greater than p_strVerY ->  1 
		'''
		''' Ex:
		'''   3.10 > 3.9
		'''   3.0.1 > 3.0
		''' </summary>
		''' <param name="pVerX"></param>
		''' <param name="pVerY"></param>
		''' <returns></returns>
		''' <remarks></remarks>
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

		''' <summary>
		''' Sends information to anyone interested
		''' </summary>
		''' <param name="pText"></param>
		''' <remarks></remarks>
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