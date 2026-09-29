Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup
            Using selector As New frmseleccionarEmpresa()
                If selector.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    e.Cancel = True
                    Exit Sub
                End If
            End Using

            CadenaConexion = (New conexion()).ObtenerCadena()
            Connection = New Global.System.Data.SqlClient.SqlConnection(CadenaConexion)

            Using acceso As New frmaccesoUsuario()
                If acceso.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    e.Cancel = True
                End If
            End Using
        End Sub
    End Class
End Namespace