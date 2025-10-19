' ------------------------------------------------------------------------
' DESCRIPTION
' ------------------------------------------------------------------------
'
' Common.vb
'
' Contains the Sub Main for the application.
' Holds the Application and the Splash objects.
'
' ------------------------------------------------------------------------
' Version
' ------------------------------------------------------------------------
'
' V 1.01.00
'
' ------------------------------------------------------------------------
' Created by: Anders S
' Date      : 2002-05-10
' ------------------------------------------------------------------------
#Region "History"
' ------------------------------------------------------------------------
' Update by : Anders S
' Date      : 2010-04-30
' Version   : 1.01.00
' Comment   : Added ExceptionHandling
' ------------------------------------------------------------------------
#End Region

Imports NodeSoft.Common

Namespace Template
	Module Common

		Public SP As frmSplash

		Public Sub Main()
			Dim prg As Application

			System.Windows.Forms.Application.EnableVisualStyles()

			SP = New frmSplash()
			SP.Show()

			System.Windows.Forms.Application.DoEvents()

			SetUpExceptionHandling()

			prg = New Application()
			prg.ShowMain()
		End Sub

#Region " OnException "

		Private Sub SetUpExceptionHandling()
#If CONFIG = "Release" Then
			'Catch unhandled exceptions from main thread
			AddHandler System.Windows.Forms.Application.ThreadException, AddressOf OnThreadException

			'Catch unhandled exceptions from other threads
			AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
#End If
		End Sub

		Public Sub OnUnhandledException(ByVal sender As Object, ByVal e As System.UnhandledExceptionEventArgs)
			If e.ExceptionObject IsNot Nothing _
			  AndAlso TypeOf e.ExceptionObject Is Exception Then
				OnException(sender, DirectCast(e.ExceptionObject, Exception))
			End If
		End Sub

		Public Sub OnThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
			OnException(sender, e.Exception)
		End Sub

		Private Sub OnException(ByVal sender As Object, ByVal e As Exception)
			Try
				MsgBox(e.Message)
			Catch ex As Exception
				MsgBox(String.Format("Error displaying error! ({0})", ex.Message))
			End Try
		End Sub

#End Region

	End Module
End Namespace

