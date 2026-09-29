Imports System.Data.SqlClient
Public Class frmaccesoAdministrador
    Private intentos As Integer = 0

    Private Sub frmAccesoAdministrador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If UsuarioActualEsAdministrador() Then
                flag = 1
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
                Exit Sub
            End If

            flag = 0
            Me.txtUsuario.Clear()
            Me.txtPassword.Clear()
            Me.txtUsuario.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            If Trim(Me.txtUsuario.Text) = "" Then
                MessageBox.Show("Ingrese el usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.txtUsuario.Focus()
                Exit Sub
            End If

            If Trim(Me.txtPassword.Text) = "" Then
                MessageBox.Show("Ingrese la clave.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.txtPassword.Focus()
                Exit Sub
            End If

            If ValidarCredencialesAdministrador(Trim(Me.txtUsuario.Text), Me.txtPassword.Text) Then
                flag = 1
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                intentos = intentos + 1
                If intentos >= 3 Then
                    MsgBox("Inténtelo en otro momento con una clave existente.", MsgBoxStyle.Critical)
                    Me.Close()
                    Exit Sub
                End If
                MsgBox("Credenciales incorrectas. Te queda: " + Str(3 - intentos) + " oportunidad(es).", MsgBoxStyle.Critical)
                Me.txtPassword.Clear()
                Me.txtPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grbDatosUsuario_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grbDatosUsuario.MouseEnter
        Me.lblMensaje.Text = "Ingrese credenciales de Administrador para continuar."
    End Sub

    Private Sub grbDatosUsuario_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grbDatosUsuario.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub

    Private Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        flag = 0
        Me.Close()
    End Sub
End Class