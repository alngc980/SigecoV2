Imports System.Data.SqlClient
Public Class frmnuevoUsuario
    Dim SqlString1 As String = "SELECT *FROM usuariosSistema"

    Private Sub frmnuevoCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoCliente.Text = devuelveCodigo(SqlString1) + 1
        Me.cbxUsuario.Items.Clear()
        Me.cbxUsuario.Items.Add("administrador")
        Me.cbxUsuario.Items.Add("vendedor")
        Me.cbxUsuario.SelectedIndex = 1
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            If Not UsuarioActualEsAdministrador() Then
                flag = 0
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then Exit Sub
            End If

            If (Trim(Me.txtNombreUsuario.Text) <> "" And Trim(Me.txtClave.Text) <> "" And Trim(Me.txtClave1.Text) <> "" And Me.cbxUsuario.SelectedItem IsNot Nothing) Then
                If Trim(Me.txtClave.Text) <> Trim(Me.txtClave1.Text) Then
                    MsgBox("Claves no coinciden, vuelva a ingresar", MsgBoxStyle.Critical)
                    Me.txtClave.Text = "" : Me.txtClave1.Text = ""
                    Me.txtClave.Focus()
                    Exit Sub
                End If

                Dim tipoUsuario As String = NormalizarTipoUsuario(Me.cbxUsuario.Text)
                If tipoUsuario <> "administrador" AndAlso tipoUsuario <> "vendedor" Then
                    MsgBox("Solo se permiten usuarios de tipo administrador o vendedor.", MsgBoxStyle.Critical)
                    Me.cbxUsuario.Focus()
                    Exit Sub
                End If

                Using cn As New SqlConnection(CadenaConexion)
                    cn.Open()
                    Using cmd As New SqlCommand("INSERT INTO usuariosSistema (nombreUsuario,usuario,clave,status,fecha) VALUES (@nombreUsuario,@usuario,@clave,1,@fecha)", cn)
                        cmd.Parameters.AddWithValue("@nombreUsuario", Me.txtNombreUsuario.Text.Trim())
                        cmd.Parameters.AddWithValue("@usuario", tipoUsuario)
                        cmd.Parameters.AddWithValue("@clave", Me.txtClave.Text)
                        cmd.Parameters.AddWithValue("@fecha", Me.dtpFecha.Value.Date)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("Faltan Datos del Usuario.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class