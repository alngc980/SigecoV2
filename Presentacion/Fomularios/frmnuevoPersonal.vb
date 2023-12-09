Public Class frmnuevoPersonal
    Dim SqlString1 As String = "SELECT *FROM personal"
    Private Sub frmnuevoPersonal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoTrabajador.Text = devuelveCodigo(SqlString1) + 1
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String
            If (Me.txtPaterno.Text <> "" And Me.txtMaterno.Text <> "" And Me.txtNombre.Text <> "" And Me.txtDNI.Text <> "" And Me.txtCelular.Text <> "") Then

                SqlString = "INSERT INTO personal (apePaterno,apeMaterno,nombres,direccion,dni,telCelular,telFijo,cargo,fecAlta) VALUES ('" & _
                Me.txtPaterno.Text & "' ,'" & Me.txtMaterno.Text & "' ,'" & Me.txtNombre.Text & "','" & Me.txtDireccion.Text & "','" & _
                Me.txtDNI.Text & "' ,'" & Me.txtCelular.Text & "' ,'" & Me.txtFijo.Text & "','" & Me.txtCargo.Text & "','" & Me.dtpFecha.Text & "' )"

                If grabarSqlString(SqlString) Then
                    MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    Me.txtCodigoTrabajador.Text = devuelveCodigo(SqlString1)
                    arrayDatos(0) = Me.txtCodigoTrabajador.Text
                    arrayDatos(1) = Me.txtPaterno.Text
                    arrayDatos(2) = Me.txtMaterno.Text
                    arrayDatos(3) = Me.txtNombre.Text
                    arrayDatos(4) = Me.txtDireccion.Text
                    arrayDatos(5) = Me.txtDNI.Text
                    Me.limpiar()
                Else
                    MsgBox("La Información no se guardó.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Faltan datos del cobrador.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtDNI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDNI.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub limpiar()
        Me.txtCodigoTrabajador.Text = ""
        Me.txtPaterno.Text = ""
        Me.txtMaterno.Text = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtCelular.Text = ""
        Me.txtFijo.Text = ""
        Me.txtCargo.Text = ""

        Me.txtCodigoTrabajador.Text = devuelveCodigo(SqlString1) + 1
        Me.txtPaterno.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class