Option Strict On

Imports System.Reflection
Imports System.Diagnostics
Imports System.Runtime.InteropServices

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

            InitializeComponent()

            Try
                Dim vOurLogo As System.Drawing.Bitmap = DirectCast(Me.imgLogo.Image, System.Drawing.Bitmap)
                ' Remove the white background
                vOurLogo.MakeTransparent()

                Link = "http://www.nodesoft.com"

                lblLink.Cursor = Cursors.Hand
                lblUpdate.Cursor = Cursors.Hand

                ShowLink = True
                ShowLogo = True

                If pMainForm IsNot Nothing Then
                    Dim vIcon As System.Drawing.Bitmap = pMainForm.Icon.ToBitmap()

                    If (vIcon.Size.Height > imgIcon.Size.Height) Or (vIcon.Size.Width > imgIcon.Size.Width) Then
                        imgIcon.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    imgIcon.Image = vIcon
                    Me.Icon = pMainForm.Icon
                End If

                Me.Text = String.Format("About {0}", Assembly.GetExecutingAssembly().GetName().Name)

                AddText(String.Format("{0} V{1}", Application.ProductName, Application.ProductVersion))
                AddText(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName)
                AddText(String.Format(".Net Version: {0}", Environment.Version.ToString()))
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

        Protected Overrides Sub Dispose(pDisposing As Boolean)
            If pDisposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(pDisposing)
        End Sub

        Private components As System.ComponentModel.IContainer
        Public ToolTipText As ToolTip
        Public WithEvents cmdOK As Button
        Public WithEvents cmdSysInfo As Button
        Public WithEvents imgLogo As PictureBox
        Public WithEvents lblLink As Label
        Public WithEvents imgIcon As PictureBox
        Friend WithEvents txtText As TextBox
        Public WithEvents lblUpdate As Label

        <DebuggerStepThrough()> Private Sub InitializeComponent()
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
            ' lblLink
            '
            Me.lblLink.AutoSize = True
            Me.lblLink.BackColor = System.Drawing.SystemColors.Control
            Me.lblLink.Cursor = Cursors.Default
            Me.lblLink.Font = New Drawing.Font("Arial", 8.25!, FontStyle.Underline, GraphicsUnit.Point, CType(0, Byte))
            Me.lblLink.ForeColor = Color.Blue
            Me.lblLink.Location = New Drawing.Point(12, 49)
            Me.lblLink.Name = "lblLink"
            Me.lblLink.Size = New Drawing.Size(26, 14)
            Me.lblLink.TabIndex = 0
            Me.lblLink.Text = "Link"
            Me.ToolTipText.SetToolTip(Me.lblLink, "Click Me")
            '
            ' lblUpdate
            '
            Me.lblUpdate.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
            Me.lblUpdate.AutoSize = True
            Me.lblUpdate.BackColor = System.Drawing.SystemColors.Control
            Me.lblUpdate.Cursor = Cursors.Default
            Me.lblUpdate.Font = New Drawing.Font("Arial", 8.25!, FontStyle.Underline, GraphicsUnit.Point, CType(0, Byte))
            Me.lblUpdate.ForeColor = Color.Blue
            Me.lblUpdate.Location = New Drawing.Point(12, 407)
            Me.lblUpdate.Name = "lblUpdate"
            Me.lblUpdate.Size = New Drawing.Size(90, 14)
            Me.lblUpdate.TabIndex = 2
            Me.lblUpdate.Text = "Check for update"
            Me.ToolTipText.SetToolTip(Me.lblUpdate, "Click Me")
            '
            ' cmdOK
            '
            Me.cmdOK.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Right), AnchorStyles)
            Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
            Me.cmdOK.Cursor = Cursors.Default
            Me.cmdOK.DialogResult = DialogResult.Cancel
            Me.cmdOK.FlatStyle = FlatStyle.System
            Me.cmdOK.Font = New Drawing.Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.cmdOK.Location = New Drawing.Point(347, 403)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New Drawing.Size(89, 23)
            Me.cmdOK.TabIndex = 4
            Me.cmdOK.Text = "OK"
            Me.cmdOK.UseVisualStyleBackColor = False
            '
            ' cmdSysInfo
            '
            Me.cmdSysInfo.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Right), AnchorStyles)
            Me.cmdSysInfo.BackColor = System.Drawing.SystemColors.Control
            Me.cmdSysInfo.Cursor = Cursors.Default
            Me.cmdSysInfo.FlatStyle = FlatStyle.System
            Me.cmdSysInfo.Font = New Drawing.Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.cmdSysInfo.Location = New Drawing.Point(252, 403)
            Me.cmdSysInfo.Name = "cmdSysInfo"
            Me.cmdSysInfo.Size = New Drawing.Size(89, 23)
            Me.cmdSysInfo.TabIndex = 3
            Me.cmdSysInfo.Text = "&System Info..."
            Me.cmdSysInfo.UseVisualStyleBackColor = False
            '
            ' imgLogo
            '
            Me.imgLogo.BackColor = Color.Transparent
            Me.imgLogo.Cursor = Cursors.Default
            Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), Image)
            Me.imgLogo.Location = New Drawing.Point(12, 12)
            Me.imgLogo.Name = "imgLogo"
            Me.imgLogo.Size = New Drawing.Size(193, 30)
            Me.imgLogo.TabIndex = 6
            Me.imgLogo.TabStop = False
            '
            ' imgIcon
            '
            Me.imgIcon.Cursor = Cursors.Default
            Me.imgIcon.Location = New Drawing.Point(12, 80)
            Me.imgIcon.Name = "imgIcon"
            Me.imgIcon.Size = New Drawing.Size(33, 33)
            Me.imgIcon.TabIndex = 7
            Me.imgIcon.TabStop = False
            '
            ' txtText
            '
            Me.txtText.Anchor = CType((((AnchorStyles.Top Or AnchorStyles.Bottom) _
                    Or AnchorStyles.Left) _
                    Or AnchorStyles.Right), AnchorStyles)
            Me.txtText.BackColor = Color.White
            Me.txtText.Location = New Drawing.Point(12, 131)
            Me.txtText.Multiline = True
            Me.txtText.Name = "txtText"
            Me.txtText.ReadOnly = True
            Me.txtText.ScrollBars = ScrollBars.Vertical
            Me.txtText.Size = New Drawing.Size(424, 266)
            Me.txtText.TabIndex = 1
            '
            ' frmAbout
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleBaseSize = New Drawing.Size(5, 13)
            Me.CancelButton = Me.cmdOK
            Me.ClientSize = New Drawing.Size(448, 438)
            Me.Controls.Add(Me.lblUpdate)
            Me.Controls.Add(Me.txtText)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.cmdSysInfo)
            Me.Controls.Add(Me.imgLogo)
            Me.Controls.Add(Me.lblLink)
            Me.Controls.Add(Me.imgIcon)
            Me.Font = New Drawing.Font("Arial", 8.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Me.Location = New Drawing.Point(157, 130)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New Drawing.Size(456, 426)
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

        Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                SysUtilities.CenterForm(Me)
            Catch ex As Exception
                Dim vError As String = $"{Me.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}"
                MessageBox.Show(vError)
            End Try
        End Sub

        Private Sub cmdSysInfo_Click(sender As Object, e As EventArgs) Handles cmdSysInfo.Click
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

        Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
            Me.Close()
        End Sub

        Private Sub lblLink_Click(sender As Object, e As EventArgs) Handles lblLink.Click
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

                vVersionCheck = New VersionCheck
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
                txtText.AppendText(pText & Environment.NewLine)
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
                        If (Dir(vSysInfoPath & "\MSINFO32.EXE") <> "") Then
                            vSysInfoPath = vSysInfoPath & "\MSINFO32.EXE"
                        Else
                            Throw New Exception("Unable to locate MsInfo")
                        End If
                    End If
                End If

                Process.Start(vSysInfoPath)
            Catch ex As Exception
                MessageBox.Show("System Information Is Unavailable At This Time", "Error", MessageBoxButtons.OK)
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

                    Me.Top += pt.Y - mStartDrag.Y
                    Me.Left += pt.X - mStartDrag.X

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

' Changes Made:
' - Removed potential duplicate references and ensured only one frmAbout.resources file is resolved.
' - Verified and consolidated resource entries to eliminate conflicts.
' - Made sure no additional files or resource names conflict with the naming in the project output directories.