Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Windows.Controls
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.Filtering

Namespace SilverlightApplication1
	Partial Public Class MainPage
		Inherits UserControl
		Implements IDisposable
		Private session As Session = Nothing
		Public Sub New()
			InitializeComponent()
				'It is necessary to call UpdataSchema method for all persistent classes.
			ThreadPool.QueueUserWorkItem(Function(o) AnonymousMethod1(o))
		End Sub
		
		Private Function AnonymousMethod1(ByVal o As Object) As Boolean
			XpoDefault.DataLayer = XpoDefault.GetDataLayer("http://localhost:64466/Service1.svc", AutoCreateOption.SchemaAlreadyExists)
			session = New Session()
			session.UpdateSchema(GetType(Customer))
			If session.FindObject(GetType(Customer), New BinaryOperator("ContactName", "Alex Smith", BinaryOperatorType.Equal)) Is Nothing Then
				Dim custAlex As New Customer(session) With {.ContactName = "Alex Smith", .CompanyName = "DevExpress"}
				custAlex.Save()
				Dim Tom As New Customer(session) With {.ContactName = "Tom Jensen", .CompanyName = "ExpressIT"}
				Tom.Save()
			End If
			session.TypesManager.EnsureIsTypedObjectValid()
			Dispatcher.BeginInvoke(AddressOf BeginInitializeDataSource)
			Return True
		End Function
		Private Sub BeginInitializeDataSource()
			Dim query = _
				From c In session.Query(Of Customer)() _
				Select c
			'Execute the query asynchronously.
			query.EnumerateAsync(AddressOf EndInitializeDataSource)

		End Sub
		Private Sub EndInitializeDataSource(ByVal result As IEnumerable(Of Customer), ByVal ex As Exception)
			'Assign the data source to the control.
			gridControl1.ItemsSource = result
		End Sub
		Public Sub Dispose() Implements IDisposable.Dispose
			If session IsNot Nothing Then
				session.Dispose()
				session = Nothing
			End If
		End Sub
	End Class
End Namespace
