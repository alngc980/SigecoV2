Imports Libreria
Public Class frmcreaLetras
    Dim te As New RichTextBox
    Dim txtStringLetra As String
    Dim montoLetra, montoLetraME As Decimal
    Dim vCantidadCuotas As Byte
    Dim vSemana, vQuincena As Integer
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(3)
        End If
    End Sub
    Private Sub btnNuevoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
        arrayDatos(0) = ""
        flagString = "1"
        frmNuevoCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(3)
        End If
    End Sub
    Private Sub txtCodigoVendedor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoVendedor.DoubleClick
        arrayDatos(0) = ""
        frmbuscaVendedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoVendedor.Text = arrayDatos(0)
        End If
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        Me.txtTipoCambio.Text = devuelveTipoCambio(cadenaString, Me.cbxTipoMoneda.Text)
        lblMonto.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"

        If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
            lblMontoME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMonto.Text = ""
            Me.txtMonto.Text = 0
            Me.txtMonto.Enabled = False
            Me.txtMontoME.Enabled = True
        Else
            lblMonto.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMontoME.Text = ""
            Me.txtMontoME.Text = 0
            Me.txtMonto.Enabled = True
            Me.txtMontoME.Enabled = False
        End If
    End Sub
    Private Sub frmcreaLetras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strCodigoVendedor As String = ("SELECT * FROM vendedores where idVendedor=1")
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)
        Me.txtMonto.Text = 0
        Me.txtMontoME.Text = 0
        Me.montoLetra = 0
        Me.montoLetraME = 0
        Me.cbxCantidadCuotas.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 1
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.txtNumDocumento.Text = ""
        lblMonto.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        If Me.txtNombres.Text = "" Then
            MsgBox("Faltan datos de cliente, no se puede continuar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Me.txtNumDocumento.Text = "" Then
            MsgBox("Indique número de documento, no se puede continuar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0 Then
            MsgBox("Indique total del documento menos la cuota inicial. Monto no puede ser cero.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            Dim oProducto As Producto = New Producto()
            Dim fecVcmto As DateTime
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim ListSqlStrings1 As New ArrayList

            If Me.cbxTipoCredito.SelectedIndex = 0 Then
                vCantidadCuotas = CInt(Me.cbxCantidadCuotas.Text)
            Else
                If Me.cbxTipoCredito.SelectedIndex = 1 Then
                    vCantidadCuotas = CInt(Me.cbxCantidadCuotas.Text) * 2
                Else
                    vCantidadCuotas = CInt(Me.cbxCantidadCuotas.Text) * 4
                End If
            End If

                Me.txtStringLetra = oProducto.stringLetra(Me.cbxTipoDocumento.Text, Me.txtNumDocumento.Text, Me.dtmFecha.Text, vCantidadCuotas)

                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    Me.montoLetraME = txtMontoME.Text / vCantidadCuotas
                Else
                    Me.montoLetra = txtMonto.Text / vCantidadCuotas
                End If

                For i As Integer = 0 To vCantidadCuotas - 1
                If Me.cbxTipoCredito.SelectedIndex = 0 Then
                    fecVcmto = Me.dtpFechaVcto.Value.AddMonths(i).ToShortDateString
                Else
                    If Me.cbxTipoCredito.SelectedIndex = 1 Then
                        fecVcmto = Me.dtpFechaVcto.Value.AddDays(vQuincena).ToShortDateString
                        vQuincena += 15
                    Else
                        fecVcmto = Me.dtpFechaVcto.Value.AddDays(vSemana).ToShortDateString
                        vSemana += 7
                    End If
                End If

                SqlString = "INSERT INTO letrasClientes (numLetra,idCliente,idVendedor,numCorrelativo,impLetra,impLetraME," & _
                             "fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,tipCambio,status,statusNC) VALUES ('" & _
                            Me.txtStringLetra & "'," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & "," & i + 1 & "," & _
                            Me.montoLetra & "," & Me.montoLetraME & ",'" & Me.dtmFecha.Text & "','" & fecVcmto & "',' ',' '," & _
                            Me.cbxTipoMoneda.SelectedIndex + 1 & " ," & Me.txtTipoCambio.Text & ",' ',' ')"

                ListSqlStrings.Add(SqlString)
            Next

            If Me.cbxTipoVenta.SelectedIndex = 1 Then

                '------agregado 05-09-13
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If
                flag = 0
                '-----------

                If transaccionLetras(ListSqlStrings) Then
                    If MsgBox("Cuotas del crédito generadas correctamente. Desea editarlas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Dim ofrmconsultaLetrasNom As New frmconsultaLetrasNom()
                        flag = 1
                        numeroLetra = Me.txtStringLetra
                        ofrmconsultaLetrasNom.ShowDialog()
                        numeroLetra = ""
                        flag = 0
                        btnLimpiar_Click(sender, e)
                    End If
                Else
                    MsgBox("No se generaron las letras del credito.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtMonto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonto.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtMontoME_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoME.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNumDocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumento.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Dim strCodigoVendedor As String = ("SELECT * FROM vendedores where idVendedor=1")
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)

        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""

        Me.txtMonto.Text = 0
        Me.txtMontoME.Text = 0
        Me.montoLetra = 0
        Me.montoLetraME = 0
        Me.cbxCantidadCuotas.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 1
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.txtNumDocumento.Text = ""
        Me.btnBuscarCliente.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class