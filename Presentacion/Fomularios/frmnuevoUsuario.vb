Public Class frmnuevoUsuario
    Dim SqlString1 As String = "SELECT *FROM usuariosSistema"
    Private Sub frmnuevoCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoCliente.Text = devuelveCodigo(SqlString1) + 1
        Me.cbxUsuario.SelectedIndex = 0
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String
            Dim miClave As String

            If (Trim(Me.txtNombreUsuario.Text) <> "" And Trim(Me.txtClave.Text) <> "" And Trim(Me.txtClave1.Text) <> "") Then

                If Trim(Me.txtClave.Text) <> Trim(Me.txtClave1.Text) Then
                    MsgBox("Claves no coinciden, vuelva a ingresar", MsgBoxStyle.Critical)
                    Me.txtClave.Text = "" : Me.txtClave1.Text = ""
                    Me.txtClave.Focus()
                    Exit Sub
                End If

                miClave = InputBox("Ingrese clave de acceso de creación de usuarios:", , , 700, 300)
                If miClave <> "13579aceg" Then
                    MsgBox("I'm sorry, your password is incorrect.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                SqlString = "INSERT INTO usuariosSistema (nombreUsuario,usuario,clave,status,fecha) VALUES ('" & _
                Me.txtNombreUsuario.Text & "','" & Me.cbxUsuario.Text & "','" & Me.txtClave.Text & "',1 ,'" & Me.dtpFecha.Text & "' )"

                If grabarSqlString(SqlString) Then
                    MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    Me.Close()
                Else
                    MsgBox("La Información no se guardó.", MsgBoxStyle.Information)
                End If
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