Imports System.Data.SqlClient
Public Class frmingresarUsuario
    Private oDataSet As DataSet
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Static contador As Integer
            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM usuariosSistema where usuario Like '" & Trim(Me.txtUsuario.Text) & "' and clave like '" & Trim(Me.txtClave.Text) & "' ", Connection)
            oDataSet = New DataSet()
            Connection.Open()
            daClientes.Fill(oDataSet, "usuarios")
            Connection.Close()

            If Me.txtUsuario.Text <> "" And Me.txtClave.Text <> "" Then

                If Me.oDataSet.Tables(0).Rows.Count > 0 Then
                    Me.Close()
                Else
                    contador = contador + 1
                    If contador = 3 Then
                        MsgBox("Para otra vez será", MsgBoxStyle.Critical)
                        End
                    End If
                    MsgBox("Te queda: " + Str(3 - contador) + " oportunidad(es)", 16, "Clave incorrecta.")
                    Me.txtUsuario.Clear()
                    Me.txtClave.Clear()
                    Me.txtUsuario.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        End
    End Sub
End Class