Imports System.Data.SqlClient
Public Class frmaccesoUsuario
    Private oDataSet As DataSet
    Private Sub frmAccesoUsuario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim daUsuarios As New SqlDataAdapter("SELECT *FROM usuariosSistema", Connection)
            oDataSet = New DataSet()
            Connection.Open()
            daUsuarios.Fill(oDataSet, "usuariosSistema")
            Connection.Close()

            Me.txtUsuario.Text = "User"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Static cuenta As Integer
            Dim objDataView As New DataView()
            objDataView.Table = oDataSet.Tables(0)
            objDataView.RowFilter = "usuario='" & Trim(Me.txtUsuario.Text) & "' and clave='" & Me.txtPassword.Text & "'"

            If objDataView.Count > 0 Then
                flag = 1
                Connection.Close()
                Me.Close()
            Else
                cuenta = cuenta + 1
                If cuenta = 3 Then
                    MsgBox("Intentelo en otro momento con una clave existente.", MsgBoxStyle.Critical)
                    Me.Close()
                End If
                MsgBox("Clave incorrecta!!! Te queda: " + Str(3 - cuenta) + " oportunidad(es).", MsgBoxStyle.Critical)
                Me.txtPassword.Clear()
                Me.txtPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(e.ToString, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub grbDatosUsuario_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grbDatosUsuario.MouseEnter
        Me.lblMensaje.Text = "Ingrese clave de Usuario para grabar las modificaciones."
    End Sub
    Private Sub grbDatosUsuario_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grbDatosUsuario.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        flag = 0
        Me.Close()
    End Sub
End Class