Public Class frmnuevoProveedor
    Dim SqlString1 As String = "SELECT *FROM proveedores"
    Private Sub frmNuevoCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoProveedor.Text = devuelveCodigo(SqlString1) + 1
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String
            If (Me.txtRazonSocial.Text <> "" And Me.txtRUC.Text <> "" And Me.txtCelular.Text <> "") Then

                SqlString = "INSERT INTO proveedores (nomProveedor,direccion,ruc,telCelular,telFijo,representante,fecAlta) VALUES ('" & _
                Me.txtRazonSocial.Text & "','" & Me.txtDireccion.Text & "','" & Me.txtRUC.Text & "','" & Me.txtCelular.Text & "','" & _
                Me.txtFijo.Text & "','" & Me.txtRepresentante.Text & "','" & Me.dtpFecha.Text & "' )"

                If grabarSqlString(SqlString) Then
                    MsgBox("Información guardada correctamente", MsgBoxStyle.Information)
                    Me.txtCodigoProveedor.Text = devuelveCodigo(SqlString1)
                    arrayDatos(0) = Me.txtCodigoProveedor.Text
                    arrayDatos(1) = Me.txtRazonSocial.Text
                    arrayDatos(2) = Me.txtDireccion.Text
                    arrayDatos(3) = Me.txtRUC.Text
                    Me.limpiar()
                Else
                    MsgBox("La Información no se guardó.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Faltan Datos del Proveedor.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtDNI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRUC.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub limpiar()
        Me.txtCodigoProveedor.Text = ""
        Me.txtRazonSocial.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtRUC.Text = ""
        Me.txtCelular.Text = ""
        Me.txtFijo.Text = ""
        Me.txtRepresentante.Text = ""

        Me.txtCodigoProveedor.Text = devuelveCodigo(SqlString1) + 1
        Me.txtRazonSocial.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class