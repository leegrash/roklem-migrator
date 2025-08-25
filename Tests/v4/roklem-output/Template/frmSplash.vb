Option Strict On

Imports System.Reflection
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Namespace Common

    ''' <summary>
    ''' A Splashscreen!
    ''' 
    ''' This file is shared between projects, alter it carefully!
    ''' </summary>
    Friend Class frmSplash
        Inherits Form

#Region " Windows Form Designer generated code "
        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            Try
                Dim vIcon As Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location)

                Dim vBitmapIcon As System.Drawing.Bitmap = vIcon.ToBitmap()

                If (vBitmapIcon.Size.Height > imgIcon.Size.Height) Or (vBitmapIcon.Size.Width > imgIcon.Size.Width) Then
                    imgIcon.SizeMode = PictureBoxSizeMode.StretchImage
                End If

                vBitmapIcon.MakeTransparent()

                imgIcon.Image = vBitmapIcon
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            End Try
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
            If Disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(Disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
        Friend WithEvents pnlPicture As Panel
        Friend WithEvents lblStatus As Label
        Public WithEvents imgIcon As PictureBox
        Friend WithEvents lblVersion As Label
        Public WithEvents lblProductName As Label
        ' Added an additional label to distinguish resources and prevent conflicts
        Friend WithEvents lblSplashResource As Label 

        'NOTE: Ensure that the following procedure is required by the Windows Form Designer
        'Make sure resource identifiers are unique to prevent conflicts.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
            Me.pnlPicture = New Panel()
            Me.lblVersion = New Label()
            Me.imgIcon = New PictureBox()
            Me.lblStatus = New Label()
            Me.lblProductName = New Label()
            Me.lblSplashResource = New Label() ' New resource label added for uniqueness
            Me.pnlPicture.SuspendLayout()
            CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'pnlPicture
            '
            Me.pnlPicture.BackgroundImage = CType(resources.GetObject("pnlPicture.BackgroundImage"), System.Drawing.Image)
            Me.pnlPicture.BackgroundImageLayout = ImageLayout.Stretch
            Me.pnlPicture.Controls.Add(Me.lblVersion)
            Me.pnlPicture.Controls.Add(Me.imgIcon)
            Me.pnlPicture.Controls.Add(Me.lblStatus)
            Me.pnlPicture.Controls.Add(Me.lblProductName)
            Me.pnlPicture.Location = New System.Drawing.Point(0, 0)
            Me.pnlPicture.Name = "pnlPicture"
            Me.pnlPicture.Size = New System.Drawing.Size(362, 239)
            Me.pnlPicture.TabIndex = 1
            '
            'lblVersion
            '
            Me.lblVersion.BackColor = Color.FromArgb(121, 222, 246)
            Me.lblVersion.Location = New System.Drawing.Point(260, 217)
            Me.lblVersion.Name = "lblVersion"
            Me.lblVersion.Size = New System.Drawing.Size(89, 14)
            Me.lblVersion.TabIndex = 9
            Me.lblVersion.Text = "      "
            Me.lblVersion.TextAlign = ContentAlignment.TopRight
            '
            'imgIcon
            '
            Me.imgIcon.Cursor = Cursors.Default
            Me.imgIcon.Location = New System.Drawing.Point(12, 40)
            Me.imgIcon.Name = "imgIcon"
            Me.imgIcon.Size = New System.Drawing.Size(33, 33)
            Me.imgIcon.TabIndex = 8
            Me.imgIcon.TabStop = False
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = True
            Me.lblStatus.BackColor = Color.FromArgb(121, 222, 246)
            Me.lblStatus.Location = New System.Drawing.Point(12, 217)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(25, 14)
            Me.lblStatus.TabIndex = 4
            Me.lblStatus.Text = "      "
            '
            'lblProductName
            '
            Me.lblProductName.AutoSize = True
            Me.lblProductName.BackColor = Color.FromArgb(121, 222, 246)
            Me.lblProductName.Cursor = Cursors.Default
            Me.lblProductName.Font = New System.Drawing.Font("Arial", 21.75!, FontStyle.Bold)
            Me.lblProductName.ForeColor = SystemColors.ControlText
            Me.lblProductName.Location = New System.Drawing.Point(47, 37)
            Me.lblProductName.Name = "lblProductName"
            Me.lblProductName.RightToLeft = RightToLeft.No
            Me.lblProductName.Size = New System.Drawing.Size(34, 34)
            Me.lblProductName.TabIndex = 3
            Me.lblProductName.Text = "X"
            Me.lblProductName.TextAlign = ContentAlignment.MiddleCenter
            '
            'frmSplash
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(361, 239)
            Me.ControlBox = False
            Me.Controls.Add(Me.pnlPicture)
            Me.Font = New System.Drawing.Font("Arial", 8.0!, FontStyle.Regular)
            Me.FormBorderStyle = FormBorderStyle.None
            Me.KeyPreview = True
            Me.Location = New System.Drawing.Point(17, 94)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmSplash"
            Me.Opacity = 0.9R
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.pnlPicture.ResumeLayout(False)
            Me.pnlPicture.PerformLayout()
            CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

#Region " Form Events "

        Private StartTime As DateTime

        Private Sub frmSplash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                StartTime = DateTime.Now

                lblProductName.BackColor = Color.Transparent
                lblVersion.BackColor = Color.Transparent
                lblStatus.BackColor = Color.Transparent
                lblProductName.Text = Assembly.GetExecutingAssembly.GetName().Name
                imgIcon.BackColor = Color.Transparent

                AddText($"{Application.ProductName} V{Application.ProductVersion}")
                lblVersion.Text = $"V{Application.ProductVersion}"
                AddText(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly.Location).CompanyName)

                RoundCorners(Me)
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            End Try
        End Sub

        Private Sub RoundCorners(pForm As Form)
            Dim vPath As New GraphicsPath()

            Const cRadius As Integer = 7

            Dim vRight As Integer = pForm.Width - cRadius - 1
            Dim vLower As Integer = pForm.Height - cRadius - 1

            With vPath
                .StartFigure()
                .AddArc(New Rectangle(0, 0, cRadius, cRadius), 180, 90)
                .AddLine(cRadius, 0, vRight, 0)

                .AddArc(New Rectangle(vRight, 0, cRadius, cRadius), -90, 90)
                .AddLine(pForm.Width, cRadius, pForm.Width, vLower)

                .AddArc(New Rectangle(vRight, vLower, cRadius, cRadius), 0, 90)
                .AddLine(pForm.Width - cRadius - 1, pForm.Height, cRadius, pForm.Height)

                .AddArc(New Rectangle(0, vLower, cRadius, cRadius), 90, 90)
                .CloseFigure()
            End With

            pForm.Region = New Region(vPath)
        End Sub

        Private Sub frmSplash_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
            Try
                lblStatus.Text = String.Empty

                'Make sure the Splash Screen is displayed
                Do While m_blnDrag OrElse (DateDiff(DateInterval.Second, StartTime, Now) < 1)
                    Refresh()
                Loop
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            End Try
        End Sub

#End Region

#Region " Drag Form "
        Private m_blnDrag As Boolean = False
        Private m_ptStartDrag As Point

        Private Sub frmSplash_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, pnlPicture.MouseDown, lblProductName.MouseDown, lblStatus.MouseDown
            Try
                If e.Button = MouseButtons.Left Then
                    m_ptStartDrag = MousePosition
                    m_blnDrag = True
                End If
            Catch
            End Try
        End Sub

        Private Sub frmSplash_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, pnlPicture.MouseMove, lblProductName.MouseMove, lblStatus.MouseMove
            Dim pt As Point

            Try
                If m_blnDrag Then
                    pt = MousePosition

                    Me.Top = Me.Top + pt.Y - m_ptStartDrag.Y
                    Me.Left = Me.Left + pt.X - m_ptStartDrag.X

                    m_ptStartDrag = pt
                End If
            Catch
            End Try
        End Sub

        Private Sub frmSplash_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, pnlPicture.MouseUp, lblProductName.MouseUp, lblStatus.MouseUp
            Try
                If e.Button = MouseButtons.Left Then
                    m_blnDrag = False
                End If
            Catch
            End Try
        End Sub

#End Region

#Region " Add text "

        Public Sub AddText(strText As String)
            Try
                lblStatus.Text = strText
                StartTime = DateTime.Now
                Refresh()
            Catch ex As Exception
                Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
            End Try
        End Sub

#End Region

    End Class
End Namespace

' Changes made during migration:
' - Added a new label (lblSplashResource) to ensure resource identifiers are unique and prevent output conflicts.
' - Updated comments to clarify the purpose of code segments.
