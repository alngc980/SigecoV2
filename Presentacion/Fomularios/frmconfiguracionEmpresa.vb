Imports System.Data.SqlClient

Public Class frmconfiguracionEmpresa
    Private Sub frmconfiguracionEmpresa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Try
            CargarConfiguracionEmpresa()
            Me.txtRazonSocial.Text = txtNombreEmpresa
            Me.txtDireccion.Text = txtDireccionEmpresa
            Me.txtTelefono.Text = LimpiarPrefijo(txtTelefonoEmpresa, "Telefono:")
            Me.txtRuc.Text = ruc_archivoPlano
            Me.txtRazonSocial.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LimpiarPrefijo(ByVal valor As String, ByVal prefijo As String) As String
        If valor.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase) Then Return valor.Substring(prefijo.Length).Trim()
        Dim posicion As Integer = valor.IndexOf(":"c)
        If posicion >= 0 Then Return valor.Substring(posicion + 1).Trim()
        Return valor
    End Function

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim razonSocial As String = Me.txtRazonSocial.Text.Trim()
        Dim direccion As String = Me.txtDireccion.Text.Trim()
        Dim telefono As String = Me.txtTelefono.Text.Trim()
        Dim ruc As String = Me.txtRuc.Text.Trim()

        If razonSocial = "" Then
            MessageBox.Show("Ingrese la razon social.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtRazonSocial.Focus()
            Exit Sub
        End If
        If direccion = "" Then
            MessageBox.Show("Ingrese la direccion.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtDireccion.Focus()
            Exit Sub
        End If
        If telefono = "" Then
            MessageBox.Show("Ingrese el telefono.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtTelefono.Focus()
            Exit Sub
        End If
        If ruc.Length <> 11 OrElse Not IsNumeric(ruc) Then
            MessageBox.Show("El RUC debe contener 11 numeros.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtRuc.Focus()
            Exit Sub
        End If

        Try
            Const sql As String = "IF EXISTS (SELECT 1 FROM configuracionEmpresa WHERE idConfiguracion = 1) " & _
                "UPDATE configuracionEmpresa SET razonSocial=@razonSocial, direccion=@direccion, telefono=@telefono, ruc=@ruc, fechaModificacion=GETDATE() WHERE idConfiguracion=1 " & _
                "ELSE INSERT INTO configuracionEmpresa(idConfiguracion,razonSocial,direccion,telefono,ruc) VALUES(1,@razonSocial,@direccion,@telefono,@ruc)"
            Using cn As New SqlConnection(CadenaConexion)
                cn.Open()
                Using cmd As New SqlCommand(sql, cn)
                    cmd.Parameters.Add("@razonSocial", SqlDbType.VarChar, 200).Value = razonSocial
                    cmd.Parameters.Add("@direccion", SqlDbType.VarChar, 250).Value = direccion
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = telefono
                    cmd.Parameters.Add("@ruc", SqlDbType.VarChar, 11).Value = ruc
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            CargarConfiguracionEmpresa()
            MessageBox.Show("Datos de la empresa guardados correctamente.", "Empresa", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class
