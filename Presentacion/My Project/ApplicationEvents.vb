Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup
            Using selector As New frmseleccionarEmpresa()
                If selector.ShowDialog() <> Windows.Forms.DialogResult.OK Then e.Cancel = True
            End Using
        End Sub
    End Class
End Namespace
