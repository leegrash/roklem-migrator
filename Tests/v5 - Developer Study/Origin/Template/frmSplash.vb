' ------------------------------------------------------------------------
' DESCRIPTION
' ------------------------------------------------------------------------
'
' frmSplash.vb
'
' A Splashscreen!
'
' ------------------------------------------------------------------------
' Version
' ------------------------------------------------------------------------
'
' V 3.02.00
'
' ------------------------------------------------------------------------
#Region "History"
' ------------------------------------------------------------------------
' Created by: Anders & Jonas
' Date      : 2002-05-09
' ------------------------------------------------------------------------
' Update by : Anders
' Date      : 2009-08-23
' Version   : 3.01.00
' Comment   : Transparent Icon
' ------------------------------------------------------------------------
' Update by : Anders
' Date      : 2010-07-05
' Version   : 3.02.00
' Comment   : Rounded corners
' ------------------------------------------------------------------------
#End Region

Option Strict On

Imports System.Reflection
Imports System.Drawing.Drawing2D

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

				Dim vBitmapIcon As System.Drawing.Bitmap = vIcon.ToBitmap

				If (vBitmapIcon.Size.Height > imgIcon.Size.Height) Or (vBitmapIcon.Size.Width > imgIcon.Size.Width) Then
					imgIcon.SizeMode = PictureBoxSizeMode.StretchImage
				End If

				vBitmapIcon.MakeTransparent()

				imgIcon.Image = vBitmapIcon
				'Me.Icon = vIcon
			Catch ex As Exception
				Debug.WriteLine(String.Format("{0}.{1}: {2}", Me.GetType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message))
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
		Friend WithEvents pnlPicture As System.Windows.Forms.Panel
		Friend WithEvents lblStatus As System.Windows.Forms.Label
		Public WithEvents imgIcon As System.Windows.Forms.PictureBox
		Friend WithEvents lblVersion As System.Windows.Forms.Label
		Public WithEvents lblProductName As System.Windows.Forms.Label
		'NOTE: The following procedure is required by the Windows Form Designer
		'It can be modified using the Windows Form Designer.
		'Do not modify it using the code editor.
		<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
			Me.pnlPicture = New System.Windows.Forms.Panel()
			Me.lblVersion = New System.Windows.Forms.Label()
			Me.imgIcon = New System.Windows.Forms.PictureBox()
			Me.lblStatus = New System.Windows.Forms.Label()
			Me.lblProductName = New System.Windows.Forms.Label()
			Me.pnlPicture.SuspendLayout()
			CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			'
			'pnlPicture
			'
			Me.pnlPicture.BackgroundImage = CType(resources.GetObject("pnlPicture.BackgroundImage"), System.Drawing.Image)
			Me.pnlPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
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
			Me.lblVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(246, Byte), Integer))
			Me.lblVersion.Location = New System.Drawing.Point(260, 217)
			Me.lblVersion.Name = "lblVersion"
			Me.lblVersion.Size = New System.Drawing.Size(89, 14)
			Me.lblVersion.TabIndex = 9
			Me.lblVersion.Text = "      "
			Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
			'
			'imgIcon
			'
			Me.imgIcon.Cursor = System.Windows.Forms.Cursors.Default
			Me.imgIcon.Location = New System.Drawing.Point(12, 40)
			Me.imgIcon.Name = "imgIcon"
			Me.imgIcon.Size = New System.Drawing.Size(33, 33)
			Me.imgIcon.TabIndex = 8
			Me.imgIcon.TabStop = False
			'
			'lblStatus
			'
			Me.lblStatus.AutoSize = True
			Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(246, Byte), Integer))
			Me.lblStatus.Location = New System.Drawing.Point(12, 217)
			Me.lblStatus.Name = "lblStatus"
			Me.lblStatus.Size = New System.Drawing.Size(25, 14)
			Me.lblStatus.TabIndex = 4
			Me.lblStatus.Text = "      "
			'
			'lblProductName
			'
			Me.lblProductName.AutoSize = True
			Me.lblProductName.BackColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(246, Byte), Integer))
			Me.lblProductName.Cursor = System.Windows.Forms.Cursors.Default
			Me.lblProductName.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.lblProductName.ForeColor = System.Drawing.SystemColors.ControlText
			Me.lblProductName.Location = New System.Drawing.Point(47, 37)
			Me.lblProductName.Name = "lblProductName"
			Me.lblProductName.RightToLeft = System.Windows.Forms.RightToLeft.No
			Me.lblProductName.Size = New System.Drawing.Size(34, 34)
			Me.lblProductName.TabIndex = 3
			Me.lblProductName.Text = "X"
			Me.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			'
			'frmSplash
			'
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(361, 239)
			Me.ControlBox = False
			Me.Controls.Add(Me.pnlPicture)
			Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
			Me.KeyPreview = True
			Me.Location = New System.Drawing.Point(17, 94)
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "frmSplash"
			Me.Opacity = 0.9R
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.pnlPicture.ResumeLayout(False)
			Me.pnlPicture.PerformLayout()
			CType(Me.imgIcon, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
#End Region

#Region " Form Events "

		Private StartTime As Date

		Private Sub frmSplash_Load(eventSender As Object, eventArgs As EventArgs) Handles MyBase.Load
			Try
				StartTime = Now

				lblProductName.BackColor = Color.Transparent
				lblVersion.BackColor = Color.Transparent
				lblStatus.BackColor = Color.Transparent
				lblProductName.Text = Assembly.GetExecutingAssembly.GetName.Name
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

			Const cRadius = 7

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

		Private Sub frmSplash_Closed(eventSender As Object, eventArgs As EventArgs) Handles MyBase.Closed
			Try
				lblStatus.Text = String.Empty

				'Make sure the Splash Screen is displayed
				Do While m_blnDrag OrElse DateDiff(DateInterval.Second, StartTime, Now) < 1
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
				StartTime = Now
				Refresh()
			Catch ex As Exception
				Debug.WriteLine($"{Me.GetType.FullName}.{MethodBase.GetCurrentMethod().Name}: {ex.Message}")
			End Try
		End Sub

#End Region

	End Class
End Namespace
