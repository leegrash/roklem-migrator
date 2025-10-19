Option Strict On 'Of course

Imports System.Reflection
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Diagnostics
Imports System.IO

Namespace Common
    ''' <summary>
    ''' An Aboutbox!
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class frmAbout
        Inherits Form

#Region " Windows Form Designer generated code "
        Public Sub New(pMainForm As Form)
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            Try
                Dim vOurLogo As Bitmap

                vOurLogo = DirectCast(Me.imgLogo.Image, Bitmap)
                'Remove the white background
                vOurLogo.MakeTransparent()

                Link = "http://www.nodesoft.com"

                lblLink.Cursor = Cursors.Hand
                lblUpdate.Cursor = Cursors.Hand

                ShowLink = True
                ShowLogo = True

                If pMainForm IsNot Nothing Then
                    Dim vIcon As Bitmap = pMainForm.Icon.ToBitmap()

                    If (vIcon.Size.Height > imgIcon.Size.Height) Or (vIcon.Size.Width > imgIcon.Size.Width) Then
                        imgIcon.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    imgIcon.Image = vIcon
                    Me.Icon = pMainForm.Icon
                End If

                Me.Text = String.Format("About {0}", Assembly.GetExecutingAssembly().GetName().Name)

                AddText(String.Format("{0} V{1}", Application.ProductName, Application.ProductVersion))
                AddText(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName)

                'Does not work for Windows 8.1
                'AddText(SysUtilities.GetOSVersionString)

                AddText(String.Format(".Net Core Version: {0}", Environment.Version.ToString()))
                AddText(String.Format("System UpTime: {0}", SysUtilities.UpTime))

                AddText(" ")

                AddText(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).Comments)

                AddText(" ")

                AddText(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright)

                AddText(" ")
            Catch ex As Exception
                Dim vError As String = String.Format("{0}.{1}: {2}", Me.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message)
                MessageBox.Show(vError)
            End Try
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(pDisposing As Boolean)
            If pDisposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(pDisposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        Public ToolTipText As ToolTip
        Public WithEvents cmdOK As Button
        Public WithEvents cmdSysInfo As Button
        Public WithEvents imgLogo As PictureBox
        Public WithEvents lblLink As Label
        Public WithEvents imgIcon As PictureBox
        Friend WithEvents txtText As TextBox
        Public WithEvents lblUpdate As Label

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
            Me.ToolTipText = New ToolTip(Me.components)
            Me.lblLink = New Label()
            Me.lblUpdate = New Label()
            Me.cmdOK = New Button()
            Me.cmdSysInfo = New Button()
            Me.imgLogo = New PictureBox()
            Me.imgIcon = New PictureBox()
            Me.txtText = New TextBox()
            CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblLink
            '
            Me.lblLink.AutoSize = True
            Me.lblLink.BackColor = SystemColors.Control
            Me.lblLink.Cursor = Cursors.Default
            Me.lblLink.Font = New Font("Arial", 8.25!, FontStyle.Underline, GraphicsUnit.Point, CType(0, Byte))
            Me.lblLink.ForeColor = Color.Blue
            Me.lblLink.Location = New Point(12, 49)
            Me.lblLink.Name = "lblLink"
            Me.lblLink.RightToLeft = RightToLeft.No
            Me.lblLink.Size = New Size(26, 14)
            Me.lblLink.TabIndex = 0
            Me.lblLink.Text = "Link"
            Me.ToolTipText.SetToolTip(Me.lblLink, "Click Me")
            '
            'lblUpdate
            '
            Me.lblUpdate.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
            Me.lblUpdate.AutoSize = True
            Me.lblUpdate.BackColor = SystemColors.Control
            Me.lblUpdate.Cursor = Cursors.Default
            Me.lblUpdate.Font = New Font("Arial", 8.25!, FontStyle.Underline, GraphicsUnit.Point, CType(0, Byte))
            Me.lblUpdate.ForeColor = Color.Blue
            Me.lblUpdate.Location = New Point(12, 407)
            Me.lblUpdate.Name = "lblUpdate"
            Me.lblUpdate.RightToLeft = RightToLeft.No
            Me.lblUpdate.Size = New Size(90, 14)
            Me.lblUpdate.TabIndex = 2
            Me.lblUpdate.Text = "Check for update"
            Me.ToolTipText.SetToolTip(Me.lblUpdate, "Click Me")
            '
            'cmdOK
            '
            Me.cmdOK.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Right), AnchorStyles)
            Me.cmdOK.BackColor = SystemColors.Control
            Me.cmdOK.Cursor = Cursors.Default
            Me.cmdOK.DialogResult = DialogResult.Cancel
            Me.cmdOK.FlatStyle = FlatStyle.System
            Me.cmdOK.Font = New Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.cmdOK.ForeColor = SystemColors.ControlText
            Me.cmdOK.Location = New Point(347, 403)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.RightToLeft = RightToLeft.No
            Me.cmdOK.Size = New Size(89, 23)
            Me.cmdOK.TabIndex = 4
            Me.cmdOK.Text = "OK"
            Me.cmdOK.UseVisualStyleBackColor = False
            '
            'cmdSysInfo
            '
            Me.cmdSysInfo.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Right), AnchorStyles)
            Me.cmdSysInfo.BackColor = SystemColors.Control
            Me.cmdSysInfo.Cursor = Cursors.Default
            Me.cmdSysInfo.FlatStyle = FlatStyle.System
            Me.cmdSysInfo.Font = New Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.cmdSysInfo.ForeColor = SystemColors.ControlText
            Me.cmdSysInfo.Location = New Point(252, 403)
            Me.cmdSysInfo.Name = "cmdSysInfo"
            Me.cmdSysInfo.RightToLeft = RightToLeft.No
            Me.cmdSysInfo.Size = New Size(89, 23)
            Me.cmdSysInfo.TabIndex = 3
            Me.cmdSysInfo.Text = "&System Info..."
            Me.cmdSysInfo.UseVisualStyleBackColor = False
            '
            'imgLogo
            '
            Me.imgLogo.BackColor = Color.Transparent
            Me.imgLogo.Cursor = Cursors.Default
            Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), Image)
            Me.imgLogo.Location = New Point(12, 12)
            Me.imgLogo.Name = "imgLogo"
            Me.imgLogo.Size = New Size(193, 30)
            Me.imgLogo.TabIndex = 6
            Me.imgLogo.TabStop = False
            '
            'imgIcon
            '
            Me.imgIcon.Cursor = Cursors.Default
            Me.imgIcon.Location = New Point(12, 80)
            Me.imgIcon.Name = "imgIcon"
            Me.imgIcon.Size = New Size(33, 33)
            Me.imgIcon.TabIndex = 7
            Me.imgIcon.TabStop = False
            '
            'txtText
            '
            Me.txtText.Anchor = CType((((AnchorStyles.Top Or AnchorStyles.Bottom) _
                    Or AnchorStyles.Left) _
                    Or AnchorStyles.Right), AnchorStyles)
            Me.txtText.BackColor = Color.White
            Me.txtText.Location = New Point(12, 131)
            Me.txtText.Multiline = True
            Me.txtText.Name = "txtText"
            Me.txtText.ReadOnly = True
            Me.txtText.ScrollBars = ScrollBars.Vertical
            Me.txtText.Size = New Size(424, 266)
            Me.txtText.TabIndex = 1
            '
            'frmAbout
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleBaseSize = New Size(5, 13)
            Me.CancelButton = Me.cmdOK
            Me.ClientSize = New Size(448, 438)
            Me.Controls.Add(Me.lblUpdate)
            Me.Controls.Add(Me.txtText)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.cmdSysInfo)
            Me.Controls.Add(Me.imgLogo)
            Me.Controls.Add(Me.lblLink)
            Me.Controls.Add(Me.imgIcon)
            Me.Font = New Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.Location = New Point(157, 130)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New Size(456, 426)
            Me.Name = "frmAbout"
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "About MyApp"
            CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

#Region " Members "

        Private mLink As String
        Private mShowLink As Boolean
        Private mShowLogo As Boolean

#End Region

#Region " Control events "

        Private Sub frmAbout_Load(eventSender As Object, eventArgs As EventArgs) Handles MyBase.Load
            Try
                SysUtilities.CenterForm(Me)
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            End Try
        End Sub

        Private Sub cmdSysInfo_Click(eventSender As Object, eventArgs As EventArgs) Handles cmdSysInfo.Click
            Try
                Me.Cursor = Cursors.WaitCursor
                StartSysInfo()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub cmdOK_Click(eventSender As Object, eventArgs As EventArgs) Handles cmdOK.Click
            Me.Close()
        End Sub

        Private Sub lblLink_Click(eventSender As Object, eventArgs As EventArgs) Handles lblLink.Click
            Try
                Me.Cursor = Cursors.WaitCursor

                ProcessUtilities.ShellDoc(mLink)
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub lblUpdate_Click(sender As Object, e As EventArgs) Handles lblUpdate.Click
            Dim vVersionCheck As VersionCheck

            Try
                Me.Cursor = Cursors.WaitCursor

                vVersionCheck = New VersionCheck()

                vVersionCheck.CheckNewVersion()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

#End Region

#Region " AddText "

        Public Sub AddText(pText As String)
            Try
                txtText.AppendText(pText & vbNewLine)
                Refresh()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            End Try
        End Sub

#End Region

#Region " SysInfo "

        Public Shared Sub StartSysInfo()
            Dim vSysInfoPath As String

            Try
                ' Try To Get System Info Program Path\Name From Registry...
                vSysInfoPath = RegUtilities.GetKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools\MSINFO", "PATH", "")

                If vSysInfoPath = "" Then
                    ' Try To Get System Info Program Path Only From Registry...
                    vSysInfoPath = RegUtilities.GetKeyValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools Location", "MSINFO", "")

                    If vSysInfoPath = "" Then
                        Throw New Exception("Registry Entry Can Not Be Found")
                    Else
                        If (File.Exists(vSysInfoPath & "\MSINFO32.EXE")) Then
                            vSysInfoPath = vSysInfoPath & "\MSINFO32.EXE"
                        Else
                            Throw New Exception("Unable to locate MsInfo")
                        End If
                    End If
                End If

                Process.Start(vSysInfoPath)
            Catch ex As Exception
                MessageBox.Show("System Information Is Unavailable At This Time", MessageBoxButtons.OK)
            End Try
        End Sub

#End Region

#Region " Properties "

        Public Property Link() As String
            Get
                Return mLink
            End Get
            Set(Value As String)
                mLink = Value
                lblLink.Text = mLink
            End Set
        End Property

        Public Property ShowLink() As Boolean
            Get
                Return mShowLink
            End Get
            Set(Value As Boolean)
                mShowLink = Value
                lblLink.Visible = mShowLink
                lblUpdate.Visible = mShowLink
            End Set
        End Property

        Public Property ShowLogo() As Boolean
            Get
                Return mShowLogo
            End Get
            Set(Value As Boolean)
                mShowLogo = Value
                imgLogo.Visible = mShowLogo
            End Set
        End Property

#End Region

#Region " Drag Form "

        Private mDrag As Boolean = False
        Private mStartDrag As Point

        Private Sub frmAbout_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, imgLogo.MouseDown, imgIcon.MouseDown
            Try
                If e.Button = MouseButtons.Left Then
                    mStartDrag = MousePosition
                    mDrag = True
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub frmAbout_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, imgLogo.MouseMove, imgIcon.MouseMove
            Dim pt As Point

            Try
                If mDrag Then
                    pt = MousePosition

                    Me.Top = Me.Top + pt.Y - mStartDrag.Y
                    Me.Left = Me.Left + pt.X - mStartDrag.X

                    mStartDrag = pt
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub frmAbout_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, imgLogo.MouseUp, imgIcon.MouseUp
            Try
                If e.Button = MouseButtons.Left Then
                    mDrag = False
                End If
            Catch ex As Exception
            End Try
        End Sub

#End Region

    End Class
End Namespace

' Changes made:
' 1. Updated error message handling to use MessageBox.Show instead of MsgBox for .NET Core compatibility.
' 2. Confirmed file existence checks use IO.File.Exists, which is compatible in .NET Core.
' 3. Ensured Process.Start is used instead of Shell for starting new processes, which aligns with .NET Core standards.
' 4. Ensured that Environment.Version is fetched appropriately as a string; no changes needed as it was already correct.
' 5. Ensured that properties are using the "Return" keyword for consistency with .NET Core standards.
' 6. Added necessary Imports for System.IO and System.Diagnostics at the beginning of the file.