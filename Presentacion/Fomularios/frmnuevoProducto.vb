Imports System.Data.SqlClient
Public Class frmnuevoProducto
    Private txtIdGrupo As Integer
    Private Sub btnGrupo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrupo.Click
        arrayDatos(0) = ""
        frmBuscaGrupo.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtIdGrupo = arrayDatos(0)
            Me.txtGrupo.Text = arrayDatos(1)
        End If
        Me.txtDescripcion.Focus()
    End Sub
    Private Sub frmnuevoProducto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SqlString As String = "SELECT *FROM productos"
        Dim SqlString1 As String = "SELECT *FROM gruposProductos where idGrupo=1"
        Dim SqlString2 As String = "SELECT *FROM gruposProductos where idGrupo=1"

        Me.txtCodigoProducto.Text = devuelveCodigo(SqlString) + 1
        Me.txtIdGrupo = devuelveCodigo(SqlString2)
        Me.txtGrupo.Text = devuelveGrupo(SqlString1)
        Me.cbxPresentacion.SelectedIndex = 0
        Me.cbxAfectoIGV.SelectedIndex = 1
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String

        Try
            Me.txtDescripcion.Text = EliminarSaltosLinea(Me.txtDescripcion.Text, " ")

            If (Me.txtGrupo.Text <> "" And Me.txtDescripcion.Text <> "" And Me.cbxPresentacion.Text <> "" And Me.txtMarca.Text <> "" And Me.txtModelo.Text <> "" _
                And Me.txtPrecioContado.Text <> "" And Me.txtPrecioCredito.Text <> "" And Me.txtPrecioTarjeta.Text <> "" And Me.cbxAfectoIGV.Text <> "") Then

                ' Mantengo el campo numSerie en esta tabla para evitar conflictos con la posición de los campos en algunos módulos
                SqlString = "INSERT INTO productos (idProducto,idGrupo,desProducto,presentacion,marca,modelo,numSerie,preContado,preCredito,preTarjeta,preTarjetaOferta," &
                "preTarjetaRemate,preOferta,preRemate,stoInicial,afeIGV,cCodBarra) VALUES (" & Me.txtCodigoProducto.Text & "," & Me.txtIdGrupo & ",'" & Me.txtDescripcion.Text & "','" &
                Me.cbxPresentacion.Text & "','" & Me.txtMarca.Text & "','" & Me.txtModelo.Text & "','S'," & Me.txtPrecioContado.Text & "," & Me.txtPrecioCredito.Text & "," &
                Me.txtPrecioTarjeta.Text & "," & Me.txtPrecioTarjeta.Text & "," & Me.txtPrecioTarjeta.Text & "," & Me.txtPrecioTarjeta.Text & "," &
                Me.txtPrecioTarjeta.Text & ",0,'" & Me.cbxAfectoIGV.Text & "', ' " & txtCodBarra.Text & "' )"

                If grabarSqlString(SqlString) Then
                    MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                    limpiar()
                Else
                    MsgBox("Error en la operación.", MsgBoxStyle.Critical)
                End If
            Else
                MsgBox("Faltan Datos del Producto.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtPrecioContado_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecioContado.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtPrecioCredito_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecioCredito.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtPrecioOferta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecioOferta.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtPrecioRemate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecioRemate.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtPrecioContado_LostFocus(sender As Object, e As System.EventArgs) Handles txtPrecioContado.Leave
        If CDec(txtPrecioContado.Text) > 0 Then
            txtPrecioTarjeta.Text = Format(CDec(txtPrecioContado.Text) / 0.95, "####0.00")
        End If
    End Sub
    Private Sub txtPrecioOferta_LostFocus(sender As Object, e As System.EventArgs) Handles txtPrecioOferta.Leave
        If CDec(txtPrecioOferta.Text) > 0 Then
            txtPrecioTarjetaOferta.Text = Format(CDec(txtPrecioOferta.Text) / 0.95, "####0.00")
        End If
    End Sub
    Private Sub txtPrecioRemate_LostFocus(sender As Object, e As System.EventArgs) Handles txtPrecioRemate.Leave
        If CDec(txtPrecioRemate.Text) > 0 Then
            txtPrecioTarjetaRemate.Text = Format(CDec(txtPrecioRemate.Text) / 0.95, "####0.00")
        End If
    End Sub
    Private Sub limpiar()
        Dim SqlString As String = "SELECT *FROM productos"
        Dim SqlString1 As String = "SELECT *FROM gruposProductos where idGrupo=1"
        Dim SqlString2 As String = "SELECT *FROM gruposProductos where idGrupo=1"

        Me.txtCodigoProducto.Text = devuelveCodigo(SqlString) + 1
        Me.txtIdGrupo = devuelveCodigo(SqlString2)
        Me.txtGrupo.Text = devuelveGrupo(SqlString1)

        Me.txtDescripcion.Text = ""
        Me.cbxPresentacion.SelectedIndex = 0
        Me.txtMarca.Text = "XXXXX"
        Me.txtModelo.Text = "XXXXX"
        Me.txtPrecioContado.Text = 0
        Me.txtPrecioCredito.Text = 0
        Me.txtPrecioTarjeta.Text = 0
        Me.txtPrecioTarjetaOferta.Text = 0
        Me.txtPrecioTarjetaRemate.Text = 0
        Me.txtPrecioOferta.Text = 0
        Me.txtPrecioRemate.Text = 0
        Me.cbxAfectoIGV.SelectedIndex = 0
        Me.txtDescripcion.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class