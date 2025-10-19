Imports System.Windows.Forms
Imports System.Threading ' Added import for threading
Imports System.Drawing ' Required for System.Drawing.Point

Namespace Template
    Friend Class frmMain
        Inherits Form

#Region "Windows Form Designer generated code "
        Public Sub New(ByVal p_Appl As Application)
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            m_Appl = p_Appl

            _onUpdateStatusBar = New UpdateTheStatusBarHandler(AddressOf UpdateTheStatusBar)
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal Disposing As Boolean)
            If Disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(Disposing)
        End Sub
        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        Public ToolTip1 As ToolTip
        Friend WithEvents mnuMain As MenuStrip
        Friend WithEvents mnuFile As ToolStripMenuItem
        Friend WithEvents mnuTools As ToolStripMenuItem
        Friend WithEvents mnuFileExit As ToolStripMenuItem
        Friend WithEvents mnuHelp As ToolStripMenuItem
        Friend WithEvents mnuHelpAbout As ToolStripMenuItem
        Friend WithEvents mnuToolsCheckForUpdatesNow As ToolStripMenuItem
        Friend WithEvents sbInfo As StatusStrip
        Friend WithEvents sbInfoText As ToolStripStatusLabel
        Friend WithEvents pnlLine As Panel
        Friend WithEvents mnuToolsCheckForUpdatesEveryMonth As ToolStripMenuItem

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.ToolTip1 = New ToolTip(Me.components)
            Me.mnuMain = New MenuStrip()
            Me.mnuFile = New ToolStripMenuItem()
            Me.mnuFileExit = New ToolStripMenuItem()
            Me.mnuTools = New ToolStripMenuItem()
            Me.mnuToolsCheckForUpdatesNow = New ToolStripMenuItem()
            Me.mnuToolsCheckForUpdatesEveryMonth = New ToolStripMenuItem()
            Me.mnuHelp = New ToolStripMenuItem()
            Me.mnuHelpAbout = New ToolStripMenuItem()
            Me.sbInfo = New StatusStrip()
            Me.sbInfoText = New ToolStripStatusLabel()
            Me.pnlLine = New Panel()
            Me.mnuMain.SuspendLayout()
            Me.sbInfo.SuspendLayout()
            Me.SuspendLayout()

            'mnuMain
            Me.mnuMain.Items.AddRange(New ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuHelp})
            Me.mnuMain.Location = New System.Drawing.Point(0, 0)
            Me.mnuMain.Name = "mnuMain"
            Me.mnuMain.Size = New System.Drawing.Size(441, 24)
            Me.mnuMain.TabIndex = 1
            Me.mnuMain.Text = "MenuStrip1"

            'mnuFile
            Me.mnuFile.DropDownItems.AddRange(New ToolStripItem() {Me.mnuFileExit})
            Me.mnuFile.Name = "mnuFile"
            Me.mnuFile.Size = New System.Drawing.Size(37, 20)
            Me.mnuFile.Text = "&File"

            'mnuFileExit
            Me.mnuFileExit.Name = "mnuFileExit"
            Me.mnuFileExit.Size = New System.Drawing.Size(152, 22)
            Me.mnuFileExit.Text = "&Exit"

            'mnuTools
            Me.mnuTools.DropDownItems.AddRange(New ToolStripItem() {Me.mnuToolsCheckForUpdatesNow, Me.mnuToolsCheckForUpdatesEveryMonth})
            Me.mnuTools.Name = "mnuTools"
            Me.mnuTools.Size = New System.Drawing.Size(48, 20)
            Me.mnuTools.Text = "&Tools"

            'mnuToolsCheckForUpdatesNow
            Me.mnuToolsCheckForUpdatesNow.Name = "mnuToolsCheckForUpdatesNow"
            Me.mnuToolsCheckForUpdatesNow.Size = New System.Drawing.Size(240, 22)
            Me.mnuToolsCheckForUpdatesNow.Text = "&Check for updates now"

            'mnuToolsCheckForUpdatesEveryMonth
            Me.mnuToolsCheckForUpdatesEveryMonth.Name = "mnuToolsCheckForUpdatesEveryMonth"
            Me.mnuToolsCheckForUpdatesEveryMonth.Size = New System.Drawing.Size(240, 22)
            Me.mnuToolsCheckForUpdatesEveryMonth.Text = "Check for updates every &month"

            'mnuHelp
            Me.mnuHelp.DropDownItems.AddRange(New ToolStripItem() {Me.mnuHelpAbout})
            Me.mnuHelp.Name = "mnuHelp"
            Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
            Me.mnuHelp.Text = "&Help"

            'mnuHelpAbout
            Me.mnuHelpAbout.Name = "mnuHelpAbout"
            Me.mnuHelpAbout.Size = New System.Drawing.Size(152, 22)
            Me.mnuHelpAbout.Text = "&About"

            'sbInfo
            Me.sbInfo.Items.AddRange(New ToolStripItem() {Me.sbInfoText})
            Me.sbInfo.Location = New System.Drawing.Point(0, 309)
            Me.sbInfo.Name = "sbInfo"
            Me.sbInfo.Size = New System.Drawing.Size(441, 22)
            Me.sbInfo.TabIndex = 2
            Me.sbInfo.Text = "StatusStrip1"

            'sbInfoText
            Me.sbInfoText.Name = "sbInfoText"
            Me.sbInfoText.Size = New System.Drawing.Size(0, 17)

            'pnlLine
            Me.pnlLine.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) Or AnchorStyles.Right), AnchorStyles)
            Me.pnlLine.BackColor = System.Drawing.Color.Black
            Me.pnlLine.Location = New System.Drawing.Point(0, 24)
            Me.pnlLine.Name = "pnlLine"
            Me.pnlLine.Size = New System.Drawing.Size(441, 1)
            Me.pnlLine.TabIndex = 3

            'frmMain
            Me.ClientSize = New System.Drawing.Size(441, 331)
            Me.Controls.Add(Me.pnlLine)
            Me.Controls.Add(Me.sbInfo)
            Me.Controls.Add(Me.mnuMain)
            Me.MainMenuStrip = Me.mnuMain
            Me.Name = "frmMain"
            Me.Text = "Template"
            Me.mnuMain.ResumeLayout(False)
            Me.mnuMain.PerformLayout()
            Me.sbInfo.ResumeLayout(False)
            Me.sbInfo.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
#End Region

#Region "Delegates"

        Delegate Sub UpdateTheStatusBarHandler(ByVal p_strText As String)
        Private _onUpdateStatusBar As UpdateTheStatusBarHandler

#End Region

#Region "Members"

        Private m_Appl As Application

#End Region

#Region "frmMain Events"

        Private Sub frmMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim VC As VersionCheck

            Try
                SysUtilities.CenterForm(Me)

                VC = New VersionCheck()
                mnuToolsCheckForUpdatesEveryMonth.Checked = VC.GetMonthlyVersionCheck

                Me.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
            Catch ex As Exception
                Dim strText As String = String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
                Debug.WriteLine(strText)
                MessageBox.Show(strText) ' Updated MsgBox to MessageBox.Show for .NET Core compatibility
            End Try
        End Sub

        Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
            Static blnOnce As Boolean
            Dim VC As VersionCheck
            Dim TR As Thread

            If Not blnOnce Then
                blnOnce = True

                Try
                    Cursor.Current = Cursors.WaitCursor
                    Application.DoEvents()
                    VC = New VersionCheck()

                    TR = New Thread(AddressOf VC.ScheduleCheckNewVersion)
                    TR.IsBackground = True
                    TR.Start()
                Finally
                    Cursor.Current = Cursors.Default
                End Try
            End If
        End Sub

        Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            If MessageBox.Show("Really quit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
            End If
        End Sub

#End Region

#Region "Menu"

        Public Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuFileExit.Click
            Me.Close()
        End Sub

        Public Sub mnuHelpAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuHelpAbout.Click
            m_Appl.ShowAboutBox()
        End Sub

        Private Sub mnuToolsCheckForUpdatesNow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuToolsCheckForUpdatesNow.Click
            Dim VC As VersionCheck

            Try
                Cursor.Current = Cursors.WaitCursor

                VC = New VersionCheck()
                VC.CheckNewVersion()
            Catch ex As Exception
                MessageBox.Show(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)) ' Updated MsgBox to MessageBox.Show
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

        Private Sub mnuToolsCheckForUpdatesEveryMonth_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuToolsCheckForUpdatesEveryMonth.Click
            Dim VC As VersionCheck

            Try
                Cursor.Current = Cursors.WaitCursor

                mnuToolsCheckForUpdatesEveryMonth.Checked = Not mnuToolsCheckForUpdatesEveryMonth.Checked

                VC = New VersionCheck()
                VC.SetMonthlyVersionCheck(mnuToolsCheckForUpdatesEveryMonth.Checked)
            Catch ex As Exception
                MessageBox.Show(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)) ' Updated MsgBox to MessageBox.Show
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

#End Region

#Region "Display information"

        Public Sub UpdateStatusBar(ByVal p_strText As String)
            Try
                If InvokeRequired Then
                    Invoke(_onUpdateStatusBar, New Object() {p_strText})
                Else
                    UpdateTheStatusBar(p_strText)
                End If
            Catch ex As Exception
                Debug.WriteLine(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message))
            End Try
        End Sub

        Private Sub UpdateTheStatusBar(ByVal p_strText As String)
            Try
                sbInfoText.Text = p_strText
            Catch ex As Exception
                Try
                    Debug.WriteLine(String.Format("{0}.{1}: {2}", Me.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message))
                Catch ex2 As Exception
                End Try
            End Try
        End Sub

#End Region

#Region "Drag Form"
        Private m_blnDrag As Boolean = False
        Private m_ptStartDrag As System.Drawing.Point

        Private Sub frmMain_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
            Try
                If e.Button = MouseButtons.Left Then
                    m_ptStartDrag = Control.MousePosition
                    m_blnDrag = True
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub frmMain_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
            Dim pt As System.Drawing.Point

            Try
                If m_blnDrag Then
                    pt = Control.MousePosition

                    Me.Top = Me.Top + pt.Y - m_ptStartDrag.Y
                    Me.Left = Me.Left + pt.X - m_ptStartDrag.X

                    m_ptStartDrag = pt
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub frmMain_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
            Try
                If e.Button = MouseButtons.Left Then
                    m_blnDrag = False
                End If
            Catch ex As Exception
            End Try
        End Sub
#End Region

    End Class
End Namespace

' Changes made:
' 1. Ensure all forms and components reference System.Windows.Forms for .NET Core compatibility.
' 2. Updated all instances of MsgBox to MessageBox.Show for compatibility with .NET Core.
' 3. Added import statement for System.Threading to use the Thread class.
' 4. Added import for System.Drawing for Point used in dragging functionality.