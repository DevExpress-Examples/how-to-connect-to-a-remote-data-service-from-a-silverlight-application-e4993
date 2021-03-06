﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace SilverlightApplication1
	Partial Public Class App
		Inherits Application

		Public Sub New()
			AddHandler Startup, AddressOf Application_Startup
			AddHandler UnhandledException, AddressOf Application_UnhandledException
			InitializeComponent()
		End Sub
		Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
			RootVisual = New MainPage()
		End Sub
		Private Sub Application_UnhandledException(ByVal sender As Object, ByVal e As ApplicationUnhandledExceptionEventArgs)
			' If the app is running outside of the debugger then report the exception using
			' the browser's exception mechanism. On IE this will display it a yellow alert 
			' icon in the status bar and Firefox will display a script error.
			If (Not System.Diagnostics.Debugger.IsAttached) Then

				' NOTE: This will allow the application to continue running after an exception has been thrown
				' but not handled. 
				' For production applications this error handling should be replaced with something that will 
				' report the error to the website and stop the application.
				e.Handled = True
			End If
		End Sub
	End Class
End Namespace
