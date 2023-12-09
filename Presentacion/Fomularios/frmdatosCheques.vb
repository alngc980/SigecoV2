Imports System.Data.SqlClient
Public Class frmdatosCheques
    Dim txtTipoMoneda As String
    Dim txtTipoPago, txtTipoPagoCad As String
    Dim txtTipoCambio, txtTipoCambio1 As Decimal
    Private Sub frmdatosCheques_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cbxNombreBanco.Enabled = True
        Me.txtNumCheque.Enabled = True
        Me.cbxNombreBanco.Focus()

        If CInt(arrayDatos(2)) = 2 Or CInt(arrayDatos(2)) = 3 Then
            Me.cbxNombreBanco.Enabled = False
            Me.txtNumCheque.Enabled = False
            Me.txtMontoCheque.Focus()
        End If

        cbxNombreBanco.SelectedIndex = 0
        Me.txtNumCheque.Text = ""
        Me.txtMontoCheque.Text = 0
        Me.txtMontoCambioCheque.Text = 0

        Me.txtTipoMoneda = Trim(arrayDatos(0))
        Me.txtTipoCambio = arrayDatos(1)
        Me.txtTipoPago = arrayDatos(2)
        Me.txtTipoPagoCad = arrayDatos(3)

        Me.GroupBox2.Text = "Datos de " & Me.txtTipoPagoCad
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.lblTipoMoneda.Text = txtTipoMoneda + ":"

        arrayDatos(0) = "" : arrayDatos(1) = ""
        arrayDatos(2) = "" : arrayDatos(3) = ""
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"

        Try
            Me.txtTipoCambio1 = devuelveTipoCambio(cadenaString, txtTipoMoneda)
            txtMontoCheque_TextChanged(sender, e)
            Me.lblTipoMoneda1.Text = Me.cbxTipoMoneda.SelectedItem + ":"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtMontoCheque_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMontoCheque.TextChanged
        Try
            Me.txtMontoCambioCheque.Text = Format(Val(Me.txtMontoCheque.Text) * Val(Me.txtTipoCambio1) / Val(Me.txtTipoCambio), "#####0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If Me.txtTipoPago = 0 Or Me.txtTipoPago = 1 Or Me.txtTipoPago = 4 Or Me.txtTipoPago = 5 Then
            If Me.txtNumCheque.Text = "" Then
                MsgBox("Faltan datos de Cheque, Tarjeta o de Abono a Cta, para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If
            arrayDatos(0) = Me.cbxNombreBanco.Text
            arrayDatos(1) = Me.txtNumCheque.Text
        Else
            arrayDatos(0) = "Pago Efectivo"
            arrayDatos(1) = Me.txtTipoPagoCad
        End If
        arrayDatos(2) = Me.txtMontoCheque.Text
        arrayDatos(3) = Me.txtTipoCambio1
        arrayDatos(4) = Me.txtMontoCambioCheque.Text
        arrayDatos(5) = Me.txtTipoPago
        arrayDatos(6) = Me.cbxTipoMoneda.SelectedIndex + 1
        flag = 0
        Me.Close()
    End Sub
    Private Sub txtMontoCheque_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCheque.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        flag = 1
        Me.Close()
    End Sub
End Class