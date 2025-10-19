Option Strict On 'Of course

Imports System.Reflection

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
                Dim vOurLogo As System.Drawing.Bitmap

                vOurLogo = DirectCast(Me.imgLogo.Image, System.Drawing.Bitmap)
                'Remove the white background
                vOurLogo.MakeTransparent()

                Link = "http://www.nodesoft.com"

                lblLink.Cursor = System.Windows.Forms.Cursors.Hand
                lblUpdate.Cursor = System.Windows.Forms.Cursors.Hand

                ShowLink = True
                ShowLogo = True

                If Not pMainForm Is Nothing Then
                    Dim vIcon As System.Drawing.Bitmap = pMainForm.Icon.ToBitmap

                    If (vIcon.Size.Height > imgIcon.Size.Height) Or (vIcon.Size.Width > imgIcon.Size.Width) Then
                        imgIcon.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    imgIcon.Image = vIcon
                    Me.Icon = pMainForm.Icon
                End If

                Me.Text = String.Format("About {0}", System.Reflection.Assembly.GetExecutingAssembly.GetName.Name)

                AddText(String.Format("{0} V{1}", System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.ProductVersion))
                AddText(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).CompanyName)

                'Does not work for Windows 8.1
                'AddText(SysUtilities.GetOSVersionString)

                AddText(String.Format(".Net Framework Version: {0}", System.Environment.Version.ToString))
                AddText(String.Format("System UpTime: {0}", SysUtilities.UpTime))

                AddText(" ")

                AddText(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).Comments)

                AddText(" ")

                AddText(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).LegalCopyright)

                AddText(" ")
            Catch ex As Exception
                Dim vError As String = String.Format("{0}.{1}: {2}", Me.GetType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
                MsgBox(vError)
            End Try
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(pDisposing As Boolean)
            If pDisposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(pDisposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        Public ToolTipText As System.Windows.Forms.ToolTip
        Public WithEvents cmdOK As System.Windows.Forms.Button
        Public WithEvents cmdSysInfo As System.Windows.Forms.Button
        Public WithEvents imgLogo As System.Windows.Forms.PictureBox
        Public WithEvents lblLink As System.Windows.Forms.Label
        Public WithEvents imgIcon As System.Windows.Forms.PictureBox
        Friend WithEvents txtText As System.Windows.Forms.TextBox
        Public WithEvents lblUpdate As System.Windows.Forms.Label

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
            Me.ToolTipText = New System.Windows.Forms.ToolTip(Me.components)
            Me.lblLink = New System.Windows.Forms.Label()
            Me.lblUpdate = New System.Windows.Forms.Label()
            Me.cmdOK = New System.Windows.Forms.Button()
            Me.cmdSysInfo = New System.Windows.Forms.Button()
            Me.imgLogo = New System.Windows.Forms.PictureBox()
            Me.imgIcon = New System.Windows.Forms.PictureBox()
            Me.txtText = New System.Windows.Forms.TextBox()
            CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblLink
            '
            Me.lblLink.AutoSize = True
            Me.lblLink.BackColor = System.Drawing.SystemColors.Control
            Me.lblLink.Cursor = System.Windows.Forms.Cursors.Default
            Me.lblLink.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblLink.ForeColor = System.Drawing.Color.Blue
            Me.lblLink.Location = New System.Drawing.Point(12, 49)
            Me.lblLink.Name = "lblLink"
            Me.lblLink.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.lblLink.Size = New System.Drawing.Size(26, 14)
            Me.lblLink.TabIndex = 0
            Me.lblLink.Text = "Link"
            Me.ToolTipText.SetToolTip(Me.lblLink, "Click Me")
            '
            'lblUpdate
            '
            Me.lblUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblUpdate.AutoSize = True
            Me.lblUpdate.BackColor = System.Drawing.SystemColors.Control
            Me.lblUpdate.Cursor = System.Windows.Forms.Cursors.Default
            Me.lblUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblUpdate.ForeColor = System.Drawing.Color.Blue
            Me.lblUpdate.Location = New System.Drawing.Point(12, 407)
            Me.lblUpdate.Name = "lblUpdate"
            Me.lblUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.lblUpdate.Size = New System.Drawing.Size(90, 14)
            Me.lblUpdate.TabIndex = 2
            Me.lblUpdate.Text = "Check for update"
            Me.ToolTipText.SetToolTip(Me.lblUpdate, "Click Me")
            '
            'cmdOK
            '
            Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
            Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdOK.Location = New System.Drawing.Point(347, 403)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdOK.Size = New System.Drawing.Size(89, 23)
            Me.cmdOK.TabIndex = 4
            Me.cmdOK.Text = "OK"
            Me.cmdOK.UseVisualStyleBackColor = False
            '
            'cmdSysInfo
            '
            Me.cmdSysInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdSysInfo.BackColor = System.Drawing.SystemColors.Control
            Me.cmdSysInfo.Cursor = System.Windows.Forms.Cursors.Default
            Me.cmdSysInfo.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.cmdSysInfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdSysInfo.ForeColor = System.Drawing.SystemColors.ControlText
            Me.cmdSysInfo.Location = New System.Drawing.Point(252, 403)
            Me.cmdSysInfo.Name = "cmdSysInfo"
            Me.cmdSysInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.cmdSysInfo.Size = New System.Drawing.Size(89, 23)
            Me.cmdSysInfo.TabIndex = 3
            Me.cmdSysInfo.Text = "&System Info..."
            Me.cmdSysInfo.UseVisualStyleBackColor = False
            '
            'imgLogo
            '
            Me.imgLogo.BackColor = System.Drawing.Color.Transparent
            Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
            Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), System.Drawing.Image)
            Me.imgLogo.Location = New System.Drawing.Point(12, 12)
            Me.imgLogo.Name = "imgLogo"
            Me.imgLogo.Size = New System.Drawing.Size(193, 30)
            Me.imgLogo.TabIndex = 6
            Me.imgLogo.TabStop = False
            '
            'imgIcon
            '
            Me.imgIcon.Cursor = System.Windows.Forms.Cursors.Default
            Me.imgIcon.Location = New System.Drawing.Point(12, 80)
            Me.imgIcon.Name = "imgIcon"
            Me.imgIcon.Size = New System.Drawing.Size(33, 33)
            Me.imgIcon.TabIndex = 7
            Me.imgIcon.TabStop = False
            '
            'txtText
            '
            Me.txtText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtText.BackColor = System.Drawing.Color.White
            Me.txtText.Location = New System.Drawing.Point(12, 131)
            Me.txtText.Multiline = True
            Me.txtText.Name = "txtText"
            Me.txtText.ReadOnly = True
            Me.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtText.Size = New System.Drawing.Size(424, 266)
            Me.txtText.TabIndex = 1
            '
            'frmAbout
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.CancelButton = Me.cmdOK
            Me.ClientSize = New System.Drawing.Size(448, 438)
            Me.Controls.Add(Me.lblUpdate)
            Me.Controls.Add(Me.txtText)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.cmdSysInfo)
            Me.Controls.Add(Me.imgLogo)
            Me.Controls.Add(Me.lblLink)
            Me.Controls.Add(Me.imgIcon)
            Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Location = New System.Drawing.Point(157, 130)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(456, 426)
            Me.Name = "frmAbout"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
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
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MsgBox(vError)
            End Try
        End Sub

        Private Sub cmdSysInfo_Click(eventSender As Object, eventArgs As EventArgs) Handles cmdSysInfo.Click
            Try
                Me.Cursor = Cursors.WaitCursor
                StartSysInfo()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MsgBox(vError)
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
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MsgBox(vError)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub lblUpdate_Click(sender As Object, e As EventArgs) Handles lblUpdate.Click
            Dim vVersionCheck As VersionCheck

            Try
                Me.Cursor = Cursors.WaitCursor

                vVersionCheck = New VersionCheck

                vVersionCheck.CheckNewVersion()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MsgBox(vError)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End Sub

#End Region

#Region " AddText "

        Public Sub AddText(pText As String)
            Try
                txtText.AppendText(pText & vbNewLine)
                'txtText.Text &= strText & vbNewLine
                Refresh()
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MsgBox(vError)
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
                        If (Dir(vSysInfoPath & "\MSINFO32.EXE") <> "") Then
                            vSysInfoPath = vSysInfoPath & "\MSINFO32.EXE"
                        Else
                            Throw New Exception("Unable to locate MsInfo")
                        End If
                    End If
                End If

                Shell(vSysInfoPath, AppWinStyle.NormalFocus)
            Catch ex As Exception
                MsgBox("System Information Is Unavailable At This Time", MsgBoxStyle.OkOnly)
            End Try
        End Sub

#End Region

#Region " Properties "

        Public Property Link() As String
            Get
                Link = mLink
            End Get
            Set(Value As String)
                mLink = Value
                lblLink.Text = mLink
            End Set
        End Property

        Public Property ShowLink() As Boolean
            Get
                ShowLink = mShowLink
            End Get
            Set(Value As Boolean)
                mShowLink = Value
                If mShowLink = True Then
                    lblLink.Visible = True
                    lblUpdate.Visible = True
                Else
                    lblLink.Visible = False
                    lblUpdate.Visible = False
                End If
            End Set
        End Property

        Public Property ShowLogo() As Boolean
            Get
                ShowLogo = mShowLogo
            End Get
            Set(Value As Boolean)
                mShowLogo = Value
                If mShowLogo = True Then
                    imgLogo.Visible = True
                Else
                    imgLogo.Visible = False
                End If
            End Set
        End Property

#End Region

#Region " Drag Form "

        Private mDrag As Boolean = False
        Private mStartDrag As Point

        Private Sub frmSplash_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, imgLogo.MouseDown, imgIcon.MouseDown
            Try
                If e.Button = MouseButtons.Left Then
                    mStartDrag = MousePosition
                    mDrag = True
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub frmSplash_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, imgLogo.MouseMove, imgIcon.MouseMove
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

        Private Sub frmSplash_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, imgLogo.MouseUp, imgIcon.MouseUp
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