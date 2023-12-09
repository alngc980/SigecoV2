Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Imports System.IO
Imports ZXing

Public Class frmboletaVenta
    Dim te As New RichTextBox
    Dim txtCodigoCliente, txtZonaCliente, txtFechaCobro, fechaCierre As String
    Dim txtStringNumDocumento, txtStringLetra As String
    Dim tipDocumento As String = "GX"   ' "GR"
    Dim tipMovimiento As String = "SA"
    Dim txtTipoMovimiento As String = "BV"
    Dim txtStatus As String = "-"
    Dim numDocumentoGR As String
    Dim tipDocGenCI As String
    Dim numDocGenCI As String
    Dim codDocGenCI As String
    Dim serDocGenCI As String

    Dim txtComVendedor, txtTasa, txtTipoCambio1, txtPrecioVenta, txtPrecioUnitario As Decimal
    Dim txtImpLetra, txtImpLetraME, txtImpLetrav, txtTotalPagarv, txtInteresv As Decimal
    Dim txtMonto, txtMontoME, txtCuotaInicial, vCuotaInicial As Decimal
    Dim vCantidadCuotas, vCantidadMeses As Byte
    Dim valorDecimal As Integer
    Dim vSemana, vQuincena As Integer
    Dim txtGrupoProducto As Integer
    Dim chrConcepto As Char
    Dim pase As Byte = 0
    Dim sinRecibo As Byte = 0
    Dim flagGraba As Byte = 0
    Dim dsctoLetra As Decimal
    Dim i, item, resto As Integer
    Private Sub frmboletaVenta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipMovimiento='BV'")
        Dim strCodigoVendedor As String = ("SELECT * FROM vendedores where idVendedor=1")

        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)
        Me.fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalAnticipos.Text = 0
        Me.txtGrupoProducto = 0
        Me.txtMonto = 0
        Me.txtMontoME = 0
        Me.txtImpLetra = 0
        Me.txtImpLetraME = 0
        Me.vSemana = 0
        Me.vQuincena = 0
        Me.txtComVendedor = 0
        Me.txtCuotaInicial = 0
        Me.txtTotalPagar.Text = 0
        Me.txtTipoCambio1 = 0
        Me.txtTasa = 0
        Me.vCantidadCuotas = 0
        Me.txtCanCuotas.Text = 1
        Me.dsctoLetra = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 0
        Me.cbxGarantia.SelectedIndex = 3
        Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.txtCanCuotas.Enabled = False
        Me.KeyPreview = True
    End Sub
    Private Sub cbxGarantia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxGarantia.SelectedIndexChanged
        Me.txtGlosa.Text = "Condiciones de garantía: a)Plazo " & Me.cbxGarantia.Text & " meses. Incluye certificado de garantía sólo por fallos de fabricación."
    End Sub
    Private Sub frmboletaVenta_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            btnProducto_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                btnGrabar_Click(sender, e)
            Else
                If e.KeyCode = Keys.F8 Then
                    btnAnular_Click(sender, e)
                Else
                    If e.KeyCode = Keys.F10 Then
                        btnImprimir_Click(sender, e)
                    Else
                        If e.KeyCode = Keys.F12 Then
                            btnLimpiar_Click(sender, e)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub txtCodigoVendedor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoVendedor.DoubleClick
        arrayDatos(0) = ""
        frmbuscaVendedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoVendedor.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        If MsgBox("Está operación será sin cuota inicial?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        Me.cbxTipoVenta.SelectedIndex = 1
        chrConcepto = "1"
        Me.cbxTipoMoneda.Enabled = True
        Me.txtCanCuotas.Enabled = True
        Me.txtCuotaInicial = 0
        Me.sinRecibo = 1

        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente = arrayDatos(0)
            Me.txtNombre.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(4)
            Me.txtZonaCliente = arrayDatos(5)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = "" : arrayDatos(4) = "" : arrayDatos(5) = ""
        End If
    End Sub
    Private Sub btnNuevoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
        If MsgBox("Está operación será sin cuota inicial?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        Me.cbxTipoVenta.SelectedIndex = 1
        chrConcepto = "1"
        Me.cbxTipoMoneda.Enabled = True
        Me.txtCanCuotas.Enabled = True
        Me.txtCuotaInicial = 0
        Me.sinRecibo = 1

        arrayDatos(0) = ""
        frmNuevoCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente = arrayDatos(0)
            Me.txtNombre.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(4)
            Me.txtZonaCliente = arrayDatos(5)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = "" : arrayDatos(4) = "" : arrayDatos(5) = ""
        End If
    End Sub
    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        Try
            flag = 0
            If Me.dgvProductos.RowCount <= 0 Then Me.txtCuotaInicial = Me.vCuotaInicial
            If Me.cbxTipoVenta.SelectedIndex = 1 Then
                If pase = 0 Then
                    If MsgBox("Está seguro que fecha de vencimiento y N° cuotas son las correctas?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    If Me.cbxTipoCredito.SelectedIndex = 0 Then
                        vCantidadMeses = CInt(Me.txtCanCuotas.Text)
                    Else
                        If Me.cbxTipoCredito.SelectedIndex = 1 Then
                            vCantidadMeses = CInt(Me.txtCanCuotas.Text) / 2
                            resto = CInt(Me.txtCanCuotas.Text) Mod 2
                        Else
                            vCantidadMeses = CInt(Me.txtCanCuotas.Text) / 4
                            resto = CInt(Me.txtCanCuotas.Text) Mod 4
                        End If
                    End If

                    vCantidadCuotas = CInt(Me.txtCanCuotas.Text)

                    If (vCantidadCuotas > 24 And Me.cbxTipoCredito.SelectedIndex = 0) Or (vCantidadCuotas > 48 And Me.cbxTipoCredito.SelectedIndex = 1) Or (vCantidadCuotas > 96 And Me.cbxTipoCredito.SelectedIndex = 2) Then
                        MsgBox("No procede operación porque cuotas mensuales no debe ser mayor a 24, quincenales mayor a 48 o semanales mayor a 96.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    If resto > 0 Then
                        MsgBox("No procede operación porque cuotas indicadas no son meses exactos.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
                pase = 1
            End If

            Me.cbxTipoVenta.Enabled = False
            Me.cbxTipoMoneda.Enabled = False
            Me.cbxTipoCredito.Enabled = False
            Me.txtCanCuotas.Enabled = False
            Me.cbxGarantia.Enabled = False

            Dim oProducto As Producto = New Producto()
            Dim percentPrecio As Decimal
            Dim sqlString As String = "select * from tasasCreditos where numMes=" & vCantidadMeses & ""

            If Me.txtCodigoCliente <> "" And Me.txtDNI.Text <> "" Then
                arrayDatos(0) = ""
                frmbuscaProducto.ShowDialog()
                If arrayDatos(0) <> "" Then
                    Me.txtGrupoProducto = arrayDatos(1)
                    Dim sqlString1 As String = "select * from tasasVendedores where idGrupo='" & Me.txtGrupoProducto & "'"
                    item += 1
                    Me.dgvProductos.Rows.Add()
                    Me.i = Me.dgvProductos.RowCount - 1
                    Me.dgvProductos.Rows(i).Cells(0).Value = item
                    Me.dgvProductos.Rows(i).Cells(1).Value = arrayDatos(0)
                    Me.dgvProductos.Rows(i).Cells(13).Value = arrayDatos(1)
                    Me.dgvProductos.Rows(i).Cells(2).Value = arrayDatos(2)
                    Me.dgvProductos.Rows(i).Cells(3).Value = arrayDatos(3)
                    Me.dgvProductos.Rows(i).Cells(4).Value = arrayDatos(4)

                    If chrConcepto = "0" Then
                        Me.txtPrecioUnitario = arrayDatos(5)
                    Else
                        If chrConcepto = "3" Or chrConcepto = "4" Then
                            Me.txtPrecioUnitario = arrayDatos(6)
                        Else
                            If chrConcepto = "5" Then
                                Me.txtPrecioUnitario = arrayDatos(7)
                            Else
                                If chrConcepto = "6" Then
                                    Me.txtPrecioUnitario = arrayDatos(8)
                                Else
                                    If chrConcepto = "7" Then
                                        Me.txtPrecioUnitario = arrayDatos(9)
                                    Else
                                        If chrConcepto = "8" Then
                                            Me.txtPrecioUnitario = arrayDatos(10)
                                        Else
                                            If chrConcepto = "9" Then
                                                Me.txtPrecioUnitario = arrayDatos(11)
                                            Else
                                                Me.txtPrecioUnitario = arrayDatos(6)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If Me.cbxTipoMoneda.SelectedIndex >= 1 Then Me.txtPrecioUnitario = Format(CDec(Me.txtPrecioUnitario) / CDec(Me.txtTipoCambio.Text), "####0.00")

                    valorDecimal = CInt(VisualBasic.Right(Me.txtPrecioUnitario, 4))
                    If valorDecimal >= 1 Then
                        Me.txtPrecioUnitario = Math.Floor(Me.txtPrecioUnitario) + 1
                    End If

                    Me.dgvProductos.Rows(i).Cells(5).Value = Me.txtPrecioUnitario

                    arraySeries(0) = "" : arraySeries(1) = "" : arraySeries(2) = "" : arraySeries(3) = ""

                    codigoProducto = CInt(arrayDatos(0))
                    frmbuscarSeries.ShowDialog()

                    If arraySeries(0) <> "" Then
                        Dim yaEsta As Boolean = False
                        For x As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                            If Me.dgvProductos.Rows(x).Cells(13).Value = 6 Then
                                If Me.dgvProductos.Rows(x).Cells(1).Value = arrayDatos(0) And
                                   Me.dgvProductos.Rows(x).Cells(7).Value = arraySeries(2) And
                                   Me.dgvProductos.Rows(x).Cells(8).Value = arraySeries(3) Then
                                    yaEsta = True
                                    Exit For
                                End If
                            Else
                                If Me.dgvProductos.Rows(x).Cells(1).Value = arrayDatos(0) And
                                   Me.dgvProductos.Rows(x).Cells(6).Value = arraySeries(1) Then
                                    yaEsta = True
                                    Exit For
                                End If
                            End If
                        Next

                        If yaEsta = True Then
                            MsgBox("Producto ya está registrado en el detalle   !  !  !", MsgBoxStyle.Exclamation)
                            flagString = ""
                            Exit Sub
                        End If

                        If Me.dgvProductos.Rows(i).Cells(13).Value = 6 Then
                            Me.dgvProductos.Rows(i).Cells(6).Value = ""
                            Me.dgvProductos.Rows(i).Cells(7).Value = arraySeries(2)
                            Me.dgvProductos.Rows(i).Cells(8).Value = arraySeries(3)
                            Me.dgvProductos.Rows(i).Cells(12).Value = arraySeries(4)
                        Else
                            Me.dgvProductos.Rows(i).Cells(6).Value = arraySeries(1)
                            Me.dgvProductos.Rows(i).Cells(7).Value = ""
                            Me.dgvProductos.Rows(i).Cells(8).Value = ""
                            Me.dgvProductos.Rows(i).Cells(12).Value = arraySeries(4)
                        End If
                    Else
                        MsgBox("No se puede procesar " + Me.dgvProductos.Rows(i).Cells(2).Value + ", no tiene datos completos.", MsgBoxStyle.Information)
                        flagString = ""
                        Exit Sub
                    End If
                    Me.dgvProductos.Rows(i).Cells(9).Value = 1
                    If flag <> 0 Then
                        flag = 0
                        Exit Sub
                    End If

                    Me.dgvProductos.Rows(i).Cells(10).Value = Me.dgvProductos.Rows(i).Cells(5).Value * Me.dgvProductos.Rows(i).Cells(9).Value
                    Me.dgvProductos.Rows(i).Cells(14).Value = devuelveTasaVendedor(sqlString1).Rows(0).Item(Me.cbxTipoVenta.SelectedIndex + 1)
                    Me.txtComVendedor = Me.txtComVendedor + Me.dgvProductos.Rows(i).Cells(14).Value * Me.dgvProductos.Rows(i).Cells(5).Value

                    If chrConcepto = "10" And Me.cbxTipoVenta.SelectedIndex = 1 Then
                        dsctoLetra = txtCuotaInicial \ Me.txtCanCuotas.Text
                        txtCuotaInicial = 0
                    End If

                    Me.txtSubTotal.Text = Format(CDec(Me.txtSubTotal.Text) + CDec(Me.dgvProductos.Rows(i).Cells(10).Value), "#####0.00")

                    If Me.cbxTipoVenta.SelectedIndex = 1 Then
                        If Me.dgvProductos.Rows(i).Cells(5).Value > 0 Then
                            percentPrecio = Format(Me.txtCuotaInicial / CDec(Me.dgvProductos.Rows(i).Cells(5).Value), "0.00")
                            txtTasa = devuelveTasa(sqlString, percentPrecio)
                        End If

                        Me.txtImpLetrav = Format(oProducto.importeLetra(Val(Me.dgvProductos.Rows(i).Cells(5).Value), txtCuotaInicial, txtTasa, Me.cbxTipoCredito.SelectedIndex), "####0.00")

                        valorDecimal = CByte(VisualBasic.Right(Me.txtImpLetrav, 2))
                        If valorDecimal >= 1 Then
                            Me.txtImpLetrav = Math.Floor(Me.txtImpLetrav) + 1
                        End If

                        Me.txtImpLetra = (Me.txtImpLetra + Me.txtImpLetrav) - dsctoLetra
                        Me.txtTotalPagarv = Me.txtImpLetrav * Me.vCantidadCuotas
                        Me.txtPrecioVenta = CDec(Me.dgvProductos.Rows(i).Cells(5).Value) - txtCuotaInicial
                        Me.txtInteresv = Me.txtTotalPagarv - Me.txtPrecioVenta
                        Me.txtInteres.Text = Format(CDec(Me.txtInteres.Text.ToString.Trim) + Me.txtInteresv, "#####0.00")
                        Me.txtTotalAnticipos.Text = Format(CDec(txtTotalRecibos.Text), "#####0.00")
                        Me.txtTotalPagar.Text = Format(CDec(Me.txtTotalPagar.Text) + Me.txtTotalPagarv, "#####0.00")

                        Me.txtImpLetrav = 0
                        Me.txtInteresv = 0
                        Me.txtTotalPagarv = 0
                        Me.txtPrecioVenta = 0
                        Me.txtCuotaInicial = 0
                        Me.txtNumGuia.Text = Me.txtImpLetra
                    Else
                        Me.txtTotalPagar.Text = Format(CDec(Me.txtSubTotal.Text), "#####0.00")
                    End If

                    If Me.cbxTipoVenta.SelectedIndex = 1 Then Me.txtStringLetra = oProducto.stringLetra(Me.txtTipoMovimiento, Me.txtNumDocumento.Text, Me.dtpFecha.Text, vCantidadCuotas)

                    arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = "" : arrayDatos(4) = "" : arrayDatos(5) = "" : arrayDatos(6) = ""
                    arrayDatos(7) = "" : arrayDatos(8) = "" : arrayDatos(9) = "" : arrayDatos(10) = "" : arrayDatos(11) = ""
                    arraySeries(0) = "" : arraySeries(1) = "" : arraySeries(2) = "" : arraySeries(3) = ""
                    arraySeries(4) = ""
                    flagString = ""
                End If
            Else
                MsgBox("Falta información del cliente  !  !  !", MsgBoxStyle.Information)
                Me.btnBuscarCliente.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellValueChanged
        Dim sqlCadena As String

        Try
            If (dgvProductos.Columns(e.ColumnIndex).Name = "cantidad") Then
                sqlCadena = "select * from saldosAlmacenes where idProducto='" & Me.dgvProductos.Rows(e.RowIndex).Cells(1).Value & "' and fechaSaldo='" & CDate(fechaCierre) & "'"
                If devuelveStock(sqlCadena) < Val(Me.dgvProductos.Rows(e.RowIndex).Cells(9).Value) Then
                    MsgBox("No se puede procesar '" + Me.dgvProductos.Rows(e.RowIndex).Cells(2).Value + "', no tiene stock.", MsgBoxStyle.Information)
                    Me.dgvProductos.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                    flag = 1
                    flagString = ""
                    Exit Sub
                End If
            End If

            If (dgvProductos.Columns(e.ColumnIndex).Name = "precioUnitario") And flagString <> "1" Then
                procesaRegistros()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim oProducto As Producto = New Producto()
        Dim fecVcto As DateTime
        Dim stockActual As Integer
        Dim sqlString As String
        Dim listaSqlStringsVenta As New ArrayList
        Dim listaSqlStringsAlmacen As New ArrayList
        Dim sqlStringNumero As String = "select * from vtaCabecera where tipDocumento='" & Me.txtTipoMovimiento & "' and numDocumento='" & CInt(Me.txtNumDocumento.Text) & "'"

        If (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) >= 1 And (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) <= 199 Then
            cbxGarantia.SelectedIndex = 0
        Else
            If (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) >= 200 And (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) <= 599 Then
                cbxGarantia.SelectedIndex = 1
            Else
                If (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) >= 600 And (CDec(txtSubTotal.Text) + CDec(txtInteres.Text)) <= 999 Then
                    cbxGarantia.SelectedIndex = 2
                Else
                    cbxGarantia.SelectedIndex = 3
                End If
            End If
        End If

        If chrConcepto <> "10" Then
            'Dim strUltimoNumeroGR As String = ("select * from ultimosNumeros where tipDocumento='GR'and tipMovimiento='SA'")
            Dim strUltimoNumeroGR As String = ("select * from ultimosNumeros where tipDocumento='GX'and tipMovimiento='SA'")
            Me.numDocumentoGR = devuelveUltimoNumero(strUltimoNumeroGR) + 1
        Else
            Me.numDocumentoGR = ""
        End If

        If Me.dgvProductos.RowCount <= 0 Then
            MsgBox("Faltan datos del producto  !  !  !", MsgBoxStyle.Critical)
            Exit Sub
        End If

        For i As Integer = 0 To Me.dgvProductos.Rows.Count - 1
            If Me.dgvProductos.Rows(i).Cells(10).Value = 0 Then
                MsgBox("Por favor, algun(os) item(s) totaliza(n) cero  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Next

        If Me.cbxTipoVenta.SelectedIndex = 0 Or Me.cbxTipoVenta.SelectedIndex = 2 Then
            If (Me.txtTotalPagar.Text <> Me.vCuotaInicial) Then
                MsgBox("No coinciden monto de la boleta con el recibo  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        If CDate(dtpFecha.Text) >= CDate(dtpFechaVcmto.Text) And cbxTipoVenta.SelectedIndex = 1 Then
            MsgBox("Por favor, fecha de cobro no puede ser igual o menor a fecha de documento  !  !  !", MsgBoxStyle.Information)
            Exit Sub
        End If

        If verificaNumeroDocumento(sqlStringNumero) = True Then
            MsgBox("Por favor, documento ya fue grabado  ! ! !", MsgBoxStyle.Information)
            Exit Sub
        End If

        If (dgvProductos.Rows.Count <= 0) And (sumaColumnas(dgvProductos, 10) <= 0) Then
            MsgBox("No hay información procesada para grabar  !  !  !", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cbxTipoVenta.SelectedIndex = 1 Then
            Dim oFrmAcceso As New frmaccesoAdministrador()
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If
        End If

        If MsgBoxResult.No = MsgBox("Esta conforme el importe y fecha de vencimiento de la venta?" + vbCrLf + "Continuar Grabando?", vbYesNo, Title:="Estas seguro de guardar?") Then
            Exit Sub
        End If

        Try
            Me.txtStringNumDocumento = oProducto.stringLetra(Me.txtTipoMovimiento, Me.txtNumDocumento.Text, "", "")
            sqlString = "insert into vtaCabecera (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,numLetra,idCliente,idVendedor,totVentaMN," &
                        "totVentaME,intFinanciero,IGV,fecOperacion,comVendedor,cuoInicial,idMoneda,tipCambio,tasInteres,statusNC,statusNA,staEnvio,status) values ('" &
                        Me.txtTipoMovimiento & "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumDocumento.Text & ",'" & Me.numDocumentoGR & "','" &
                        chrConcepto & "','" & Me.txtStringLetra & "'," & Me.txtCodigoCliente & "," & Me.txtCodigoVendedor.Text & "," &
                        Me.txtTotalPagar.Text & "," & Me.txtTotalPagar.Text & "," & Me.txtInteres.Text & "," & Me.txtIGV.Text & ",'" & Me.dtpFecha.Text & "'," &
                        Me.txtComVendedor & "," & Me.vCuotaInicial & "," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & "," & txtTasa & ",'','','','')"
            listaSqlStringsVenta.Add(sqlString)

            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                sqlString = "insert into vtaDetalle (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV,fecOperacion,status) values ('" &
                             Me.txtTipoMovimiento & "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumDocumento.Text & "," & dgvProductos.Rows(i).Cells(1).Value &
                             "," & dgvProductos.Rows(i).Cells(5).Value & "," & dgvProductos.Rows(i).Cells(9).Value & "," & dgvProductos.Rows(i).Cells(10).Value &
                             ",'','" & Me.dtpFecha.Text & "','')"
                listaSqlStringsVenta.Add(sqlString)
            Next

            If Me.txtGlosa.Text <> "" Then
                sqlString = "insert into glosasFacturas (tipDocumento,numDocumento,glosa,nomDocumento) values ('" &
                             Me.tipMovimiento & "'," & Me.numDocumentoGR & ",'" & Me.txtGlosa.Text & "','" & Me.tipDocumento & "')"
                listaSqlStringsVenta.Add(sqlString)
            End If

            For i As Integer = 0 To vCantidadCuotas - 1
                If Me.cbxTipoCredito.SelectedIndex = 0 Then
                    fecVcto = Me.dtpFechaVcmto.Value.AddMonths(i).ToShortDateString
                Else
                    If Me.cbxTipoCredito.SelectedIndex = 1 Then
                        fecVcto = Me.dtpFechaVcmto.Value.AddDays(vQuincena).ToShortDateString
                        vQuincena += 15
                    Else
                        fecVcto = Me.dtpFechaVcmto.Value.AddDays(vSemana).ToShortDateString
                        vSemana += 7
                    End If
                End If

                sqlString = "insert into letrasClientes (numLetra,idCliente,idVendedor,numCorrelativo,impLetra,impLetraME," &
                             "fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,tipCambio,statusNC,statusNA,zona,status) VALUES ('" &
                            Me.txtStringLetra & "'," & Me.txtCodigoCliente & "," & Me.txtCodigoVendedor.Text & "," & i + 1 & "," &
                            Me.txtImpLetra & "," & Me.txtImpLetraME & ",'" & Me.dtpFecha.Text & "','" & fecVcto & "','',''," &
                            CInt(Me.cbxTipoMoneda.SelectedIndex.ToString.Trim) + 1 & " ," & Me.txtTipoCambio.Text & ",'',''," & Me.txtZonaCliente & ",'')"
                listaSqlStringsVenta.Add(sqlString)
            Next
            'AQUI ME QUEDÉ
            If sinRecibo <> 1 Then
                Dim y As Byte
                For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                    If matrizRecibos(y, 0) <> Nothing Then
                        If matrizRecibos(y, 0).ToString().Trim() <> "" Then
                            sqlString = "update recibosClientes set numDocGenACI='" & Me.txtStringNumDocumento & "' where idRecibo= " & matrizRecibos(y, 0) & ""
                            listaSqlStringsVenta.Add(sqlString)
                        End If
                    End If
                Next y
            End If

            sqlString = "update ultimosNumeros set numero=" & Me.txtNumDocumento.Text & " where tipMovimiento= '" & txtTipoMovimiento & "'"
            listaSqlStringsVenta.Add(sqlString)

            '-------- Grabación de la Guía Salida ------------
            If chrConcepto <> "10" Then
                sqlString = "insert into almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor," &
                               "fecOrigen,nomOrigen,dirOrigen,rucDNI_1,fecLlegada,idCliente,transLlegada,status) VALUES ('" &
                               Me.tipDocumento & "' ,'" & Me.tipMovimiento & "' ," & Me.numDocumentoGR & ",1,'" & Me.dtpFecha.Text & "' ,'" &
                               txtNombreEmpresa & "','" & txtDireccionEmpresa & "','" & Mid(txtRUCEmpresa, 6, 11) & "','" & Me.dtpFecha.Text & "'," &
                               Me.txtCodigoCliente & ",'" & Me.cbxGarantia.Text & "','" & Me.txtStatus & "' )"
                listaSqlStringsAlmacen.Add(sqlString)

                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    sqlString = "insert into almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad,status) VALUES ('" &
                                    Me.tipDocumento & "' ,'" & Me.tipMovimiento & "' ," & Me.numDocumentoGR & ",'" &
                                    dgvProductos.Rows(i).Cells(1).Value & "' ," & dgvProductos.Rows(i).Cells(9).Value & ",'0')"
                    listaSqlStringsAlmacen.Add(sqlString)

                    Dim sqlSaldo As String
                    sqlSaldo = "select * from saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                    stockActual = devuelveStock(sqlSaldo)
                    stockActual = stockActual - Me.dgvProductos.Rows(i).Cells(9).Value
                    sqlString = "update saldosAlmacenes set stock='" & stockActual & "' where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                    listaSqlStringsAlmacen.Add(sqlString)

                    If Me.dgvProductos.Rows(i).Cells(13).Value = 6 Then
                        sqlString = "update numerosSerie set numDoc='" & Me.numDocumentoGR & "' where numMotor='" & Me.dgvProductos.Rows(i).Cells(7).Value & "' and numChasis='" & Me.dgvProductos.Rows(i).Cells(8).Value & "' and idProducto='" & Me.dgvProductos.Rows(i).Cells(1).Value & "'"
                    Else
                        sqlString = "update numerosSerie set numDoc='" & Me.numDocumentoGR & "' where numSerie='" & Me.dgvProductos.Rows(i).Cells(6).Value & "' and idProducto='" & Me.dgvProductos.Rows(i).Cells(1).Value & "'"
                    End If
                    listaSqlStringsAlmacen.Add(sqlString)
                Next
                sqlString = "update ultimosNumeros set numero=" & Me.numDocumentoGR & " where tipDocumento='" & tipDocumento & "' and tipMovimiento='" & tipMovimiento & "'"
                listaSqlStringsAlmacen.Add(sqlString)
            End If

            If Me.cbxTipoVenta.SelectedIndex = 1 Then
                If MsgBox("Crear garantes para esta operación?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    flag = 1
                    codigoCliente = Me.txtCodigoCliente
                    frmcrearGarantes.ShowDialog()
                    flag = 0
                End If
            End If

            If transaccionLetras(listaSqlStringsVenta) Then
                flagGraba = 1
                'generarDocumentoPlano()

                Try
                    Dim ms1 As New System.IO.MemoryStream
                    Dim rep As New ReportesBD
                    AbrirAppQr()
                    Dim imagePath As String = Application.StartupPath + "\QR\" & txtSerieDocumento.Text & "-" & txtNumDocumento.Text & ".jpg"

                    ' Verifica si el archivo de imagen existe antes de intentar cargarlo
                    If System.IO.File.Exists(imagePath) Then
                        PictureBox1.Image = Image.FromFile(imagePath)
                        PictureBox1.Image.Save(ms1, PictureBox1.Image.RawFormat)
                        Dim byt() As Byte = ms1.ToArray

                        Dim ds As New DataSet1
                        Dim Dt As New DataTable
                        Dt = RetornaDataTable("rpt_Comprobante 'BV','" & txtSerieDocumento.Text & "','" & txtNumDocumento.Text & "'")
                        If Dt.Rows.Count > 0 Then
                            For i = 0 To Dt.Rows.Count - 1
                                ds.DataTable1.Rows.Add(byt,
                                               Dt.Rows(0)(0).ToString(), Dt.Rows(0)(1).ToString(),
                                               Dt.Rows(0)(2).ToString(), Dt.Rows(0)(3).ToString(),
                                               Dt.Rows(0)(4).ToString(), Dt.Rows(0)(5).ToString(),
                                               Dt.Rows(0)(6).ToString(), Dt.Rows(0)(7).ToString(),
                                               Dt.Rows(0)(8).ToString(), Dt.Rows(0)(9).ToString(),
                                               Dt.Rows(i)(10).ToString(), Dt.Rows(i)(11).ToString(),
                                               Dt.Rows(i)(12).ToString(), Dt.Rows(i)(13).ToString(),
                                               Dt.Rows(i)(14).ToString(), Dt.Rows(0)(15).ToString(), "")
                            Next
                        End If

                        Dim rpt As New rptComprobante
                        rpt.SetDataSource(ds.Tables("DataTable1"))

                        Dim frm As New frmReporte
                        frm.CrystalReportViewer1.ReportSource = rpt
                        frm.ShowDialog()
                    Else
                        MessageBox.Show("No se encontró el archivo de imagen: " & imagePath)
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try



                'btnDatosSFS_Click(sender, e)
                'MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
            Else
                MsgBox("Procesamiento de ventas no se realizó correctamente  !  !  !", MsgBoxStyle.Critical)
            End If

            If flagGraba <> 0 Then
                If transaccionLetras(listaSqlStringsAlmacen) Then
                    'MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    If chrConcepto <> "10" Then MsgBox("El proceso a generado la guía salida N° " & Me.numDocumentoGR & ".", MsgBoxStyle.Information)

                    Dim dsGuia As New dsGuiaRemision
                    Dim dt As New DataTable
                    Dim cSql As String
                    cSql = "EXEC rptGuia 'GX','" & txtSerieDocumento.Text & "','" & txtNumDocumento.Text & "'"
                    dt = RetornaDataTable(cSql)

                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dsGuia.DataTable1.Rows.Add(
                                   dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString(),
                                   dt.Rows(0)(2).ToString(), dt.Rows(0)(3).ToString(),
                                   dt.Rows(0)(4).ToString(), dt.Rows(0)(5).ToString(),
                                   dt.Rows(0)(6).ToString(), dt.Rows(0)(7).ToString(),
                                   dt.Rows(0)(8).ToString(), dt.Rows(i)(9).ToString(),
                                   dt.Rows(i)(10).ToString(), dt.Rows(i)(11).ToString(),
                                   dt.Rows(i)(12).ToString(), dt.Rows(i)(13).ToString(),
                                   dt.Rows(i)(14).ToString(), dt.Rows(0)(15).ToString(),
                                   dt.Rows(0)(16).ToString(), dt.Rows(0)(17).ToString(),
                                   dt.Rows(0)(18).ToString(), dt.Rows(0)(19).ToString(),
                                   dt.Rows(0)(20).ToString(), dt.Rows(0)(21).ToString(),
                                   dt.Rows(0)(22).ToString(), dt.Rows(0)(23).ToString(),
                                   dt.Rows(0)(24).ToString(), dt.Rows(0)(25).ToString())
                        Next
                    End If

                    Dim rpt As New rptGuia
                    rpt.SetDataSource(dsGuia.Tables("DataTable1"))

                    Dim frm As New frmReporte
                    frm.CrystalReportViewer1.ReportSource = rpt
                    frm.ShowDialog()


                Else
                    MsgBox("Procesamiento de almacén no se realizó correctamente  !  !  !", MsgBoxStyle.Critical)
                End If
            End If

            If Me.cbxTipoVenta.SelectedIndex = 1 And flagGraba <> 0 Then
                If MsgBox("Cuotas del crédito generadas correctamente. Desea editarlas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim ofrmconsultaLetrasNom As New frmconsultaLetrasNom()
                    flag = 1
                    flagGraba = 0
                    numeroLetra = txtStringLetra
                    ofrmconsultaLetrasNom.ShowDialog()
                    numeroLetra = ""
                    flag = 0
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub generarDocumentoPlano()
        Dim swEscritor As StreamWriter
        Dim sqlString As String
        Dim listaSqlString As New ArrayList
        Dim tipDocumento, numDocumento As String
        Dim numTipoDocumento As Byte
        Dim pathData, pathRepo As String

        tipDocumento = "03"
        numTipoDocumento = 1
        numDocumento = Me.txtDNI.Text

        If generaDocumentoTicket = True Then
            pathData = "\data\"
            pathRepo = "\repo\"
        Else
            pathData = "\SFS_v1.4_A4\sunat_archivos\sfs\data\"
            pathRepo = "\SFS_v1.4_A4\sunat_archivos\sfs\repo\"
        End If

        Try
            nomArchivo = "\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".PDF"
            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".CAB", True)
            swEscritor.Write("0101|" & CDate(dtpFecha.Text).ToString("yyyy-MM-dd") & "|" & VisualBasic.Mid(Date.Now, 12, 8) & "|-|0000|" & numTipoDocumento & "|" & numDocumento & "|" & Me.txtNombre.Text & "|PEN|" & "0.0" & "|" & Format(CDec(Me.txtSubTotal.Text) + CDec(Me.txtInteres.Text), "#####0.00") & "|" & Format(CDec(Me.txtSubTotal.Text) + CDec(Me.txtInteres.Text), "#####0.00") & "|0.00|0.00|" & Format(CDec(Me.txtTotalAnticipos.Text), "#####0.00") & "|" & Format(CDec(Me.txtTotalPagar.Text), "#####0.00") & "|2.1|2.0|")
            swEscritor.Close()

            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".DET", True)
            For x As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                If Me.dgvProductos.Rows(x).Cells(13).Value = 6 Then
                    swEscritor.WriteLine("NIU|" & CInt(Me.dgvProductos.Rows(x).Cells(9).Value) & "|" & Me.dgvProductos.Rows(x).Cells(1).Value & "|-|" & Me.dgvProductos.Rows(x).Cells(2).Value & " N/M " & Me.dgvProductos.Rows(x).Cells(7).Value & " N/CH " & Me.dgvProductos.Rows(x).Cells(8).Value & "|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(5).Value), "#####0.00") & "|0.00|9997|0.00|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(10).Value), "#####0.00") & "|EXO|VAT|20|18.00|-|0.00|0.00||||0.00|-|0.00|0.00|||0.00|-|0.00|0|||0.00|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(5).Value), "#####0.00") & "|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(10).Value), "#####0.00") & "|0.00|")
                Else
                    swEscritor.WriteLine("NIU|" & CInt(Me.dgvProductos.Rows(x).Cells(9).Value) & "|" & Me.dgvProductos.Rows(x).Cells(1).Value & "|-|" & Me.dgvProductos.Rows(x).Cells(2).Value & " S/N " & Me.dgvProductos.Rows(x).Cells(6).Value & "|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(5).Value), "#####0.00") & "|0.00|9997|0.00|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(10).Value), "#####0.00") & "|EXO|VAT|20|18.00|-|0.00|0.00||||0.00|-|0.00|0.00|||0.00|-|0.00|0|||0.00|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(5).Value), "#####0.00") & "|" & Format(CDec(Me.dgvProductos.Rows(x).Cells(10).Value), "#####0.00") & "|0.00|")
                End If
            Next x
            If txtInteres.Text > 0 Then
                swEscritor.Write("NIU|1|INT|-|INTERES GENERADO POR OPERACION|" & Format(CDec(txtInteres.Text), "#####0.00") & "|0.00|9997|0.00|" & Format(CDec(txtInteres.Text), "#####0.00") & "|EXO|VAT|20|18.00|-|0.00|0.00||||0.00|-|0.00|0.00|||0.00|-|0.00|0|||0.00|" & Format(CDec(txtInteres.Text), "#####0.00") & "|" & Format(CDec(txtInteres.Text), "#####0.00") & "|0.00|")
            End If
            swEscritor.Close()

            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".LEY", True)
            swEscritor.Write("1000|" & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#####0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#####0.00")) - 3)) & " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#####0.00")) & "/100 Soles|")
            swEscritor.Close()

            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".TRI", True)
            swEscritor.Write("9997" & "|EXO|VAT|" & Format(CDec(Me.txtTotalPagar.Text), "#####0.00") & "|0.00|")
            'If CDec(Me.txtICBPeru.Text) > 0 Then swEscritor.Write("7152" & "|ICBPER|OTH|" & Format(CDec(Me.txtICBPeru.Text), "#####0.00") & "|" & Format(CDec(Me.txtICBPeru.Text), "#####0.00") & "|")
            swEscritor.Close()

            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ACA", True)
            swEscritor.Write("| | | | |PE|160101|" & Me.txtDireccion.Text & "|-| | |")
            swEscritor.Close()

            If (CDec(txtTotalRecibos.Text) > 0 And Me.cbxTipoVenta.SelectedIndex = 1) Then
                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".REL", True)
                Dim y As Byte
                For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                    If matrizRecibos(y, 0) <> "" Then
                        swEscritor.WriteLine("2|" & y + 1 & "|" & Me.codDocGenCI & "|" & Me.serDocGenCI & "-" & matrizRecibos(y, 0) & "|6|" & ruc_archivoPlano & "|" & Format(Decimal.Parse(matrizRecibos(y, 1)), "#####0.00") & "|")
                    End If
                Next y
                swEscritor.Close()
                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ACV", True)
                swEscritor.Write("false|05|1.00|PEN|" & Format(Decimal.Parse(Me.txtTotalAnticipos.Text), "#####0.00") & "|PEN|" & Format(Decimal.Parse(Me.txtTotalAnticipos.Text), "#####0.00") & "|")
                swEscritor.Close()
            End If
            sqlString = "update vtaCabecera set staEnvio='@' where tipDocumento='" & txtTipoMovimiento & "' and numDocumento=" & Me.txtNumDocumento.Text & ""
            listaSqlString.Add(sqlString)

            If transaccionLetras(listaSqlString) = True Then
                tipoDocumento = Me.txtTipoMovimiento
                numeDocumento = Me.txtNumDocumento.Text
                frmMsjNumeroDocumento.ShowDialog()
                iniciarTimer()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnDatosSFS_Click(sender As System.Object, e As System.EventArgs) Handles btnDatosSFS.Click
        Dim tipo As String = "03"

        wbrSFS.Document.GetElementById("btnRefrescar").InvokeMember("Click")

        wbrSFS.Document.GetElementById("hddNumRuc").SetAttribute("Value", ruc_archivoPlano)
        wbrSFS.Document.GetElementById("hddTipDoc").SetAttribute("Value", tipo)
        wbrSFS.Document.GetElementById("hddNumDoc").SetAttribute("Value", Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text)
        wbrSFS.Document.GetElementById("hddNomArc").SetAttribute("Value", ruc_archivoPlano & "-" & tipo & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text)

        wbrSFS.Document.GetElementById("btnGenerar").InvokeMember("Click")
        wbrSFS.Document.GetElementById("btnVisorCdp").InvokeMember("Click")
    End Sub
    Private Sub iniciarTimer()
        Timer1.Start()
    End Sub
    Private Sub cerrarTimer()
        Timer1.Stop()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If System.IO.File.Exists(nomArchivo) = True Then
            cerrarTimer()
            printArchivo(nomArchivo)
            Me.btnLimpiar_Click(sender, e)
        End If
    End Sub
    Sub printArchivo(ByVal archivo As String)
        Try
            Dim printerName As String
            Dim oPS As New System.Drawing.Printing.PrinterSettings
            printerName = oPS.PrinterName

            Dim Proceso As New System.Diagnostics.Process
            Proceso.EnableRaisingEvents = True
            Proceso.StartInfo.FileName = archivo
            Proceso.StartInfo.Arguments = Chr(34) + printerName + Chr(34)
            Proceso.StartInfo.Verb = "Print"
            Proceso.StartInfo.CreateNoWindow = True
            Proceso.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            Proceso.Start()
            Proceso.CloseMainWindow()
            Proceso.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'If Me.dgvProductos.RowCount <= 0 Then
        '    MsgBox("No hay información procesada para imprimir.", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        Try
            Dim ms1 As New System.IO.MemoryStream
            Dim rep As New ReportesBD
            AbrirAppQr()
            Dim imagePath As String = Application.StartupPath + "\QR\" & txtSerieDocumento.Text & "-" & txtNumDocumento.Text & ".jpg"

            ' Verifica si el archivo de imagen existe antes de intentar cargarlo
            If System.IO.File.Exists(imagePath) Then
                PictureBox1.Image = Image.FromFile(imagePath)
                PictureBox1.Image.Save(ms1, PictureBox1.Image.RawFormat)
                Dim byt() As Byte = ms1.ToArray

                Dim ds As New DataSet1
                Dim Dt As New DataTable
                Dt = RetornaDataTable("rpt_Comprobante 'BV','" & txtSerieDocumento.Text & "','" & txtNumDocumento.Text & "'")
                If Dt.Rows.Count > 0 Then
                    For i = 0 To Dt.Rows.Count - 1
                        ds.DataTable1.Rows.Add(byt,
                                               Dt.Rows(0)(0).ToString(), Dt.Rows(0)(1).ToString(),
                                               Dt.Rows(0)(2).ToString(), Dt.Rows(0)(3).ToString(),
                                               Dt.Rows(0)(4).ToString(), Dt.Rows(0)(5).ToString(),
                                               Dt.Rows(0)(6).ToString(), Dt.Rows(0)(7).ToString(),
                                               Dt.Rows(0)(8).ToString(), Dt.Rows(0)(9).ToString(),
                                               Dt.Rows(i)(10).ToString(), Dt.Rows(i)(11).ToString(),
                                               Dt.Rows(i)(12).ToString(), Dt.Rows(i)(13).ToString(),
                                               Dt.Rows(i)(14).ToString(), Dt.Rows(0)(15).ToString(), "")
                    Next
                End If

                Dim rpt As New rptComprobante
                rpt.SetDataSource(ds.Tables("DataTable1"))

                Dim frm As New frmReporte
                frm.CrystalReportViewer1.ReportSource = rpt
                frm.ShowDialog()
            Else
                MessageBox.Show("No se encontró el archivo de imagen: " & imagePath)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VistaPrevia(ByVal TipoFuente As String, ByVal TamañoFuente As Byte, ByVal TextoImpresion As String, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim Fuente As New Font(TipoFuente, TamañoFuente)
        Dim AreaImpresion_Alto, AreaImpresion_Ancho, MargenIzquierdo, MargenSuperior As Integer

        With Me.PrintDocument1.DefaultPageSettings
            'Area Neta de Impresion (se descuenta los margenes)
            AreaImpresion_Alto = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            AreaImpresion_Ancho = .PaperSize.Width - .Margins.Left - .Margins.Right
            MargenIzquierdo = .Margins.Left
            MargenSuperior = .Margins.Top

            'Verificar si se ha elegido el modo horizontal
            If .Landscape Then
                Dim NroTemp As Integer
                NroTemp = AreaImpresion_Alto
                AreaImpresion_Alto = AreaImpresion_Ancho
                AreaImpresion_Ancho = NroTemp
            End If
            Dim Formato As New StringFormat(StringFormatFlags.LineLimit)
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior,
            AreaImpresion_Ancho, AreaImpresion_Alto)
            Dim NroLineasImpresion As Integer = CInt(AreaImpresion_Alto / Fuente.Height)
            Dim NroLineasRelleno, NroLetrasLinea As Integer
            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente,
            New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea,
            NroLineasRelleno)
            e.Graphics.DrawString(Mid(TextoImpresion, CaracterActual + 1), Fuente,
            Brushes.Black, Rectangulo, Formato)
            CaracterActual += NroLetrasLinea
            If CaracterActual < TextoImpresion.Length Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
                CaracterActual = 0
            End If
        End With
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
    Handles PrintDocument1.PrintPage
        Dim Fuente As New Font("Courier New", 9)

        VistaPrevia("Courier New", 9, te.Text, e)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 540)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 575)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 605)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 996
        Dim Alto As Short = 709

        Dim left As Short = 0
        Dim top As Short = 50
        Dim bottom As Short = 50
        Dim right As Short = 0

        TamañoPersonal = New Printing.PaperSize(nombrePapel, Ancho, Alto)
        margenes = New Printing.Margins(left, right, top, bottom)

        ' Asignamos la impresora seleccionada
        Me.PrintDocument1.PrinterSettings = ImpresoraActual
        ' Asignamos el tamaño personalizado de papel
        Me.PrintDocument1.DefaultPageSettings.PaperSize = TamañoPersonal
        ' Asignamos los márgenes al documento
        Me.PrintDocument1.DefaultPageSettings.Margins = margenes
    End Sub
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Try
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim SqlString2 As String = ""
            Dim ListSqlStrings As New ArrayList

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                SqlString = "INSERT INTO vtaCabecera (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,numLetra,idCliente,idVendedor," &
                "totVentaMN,totVentaME,intFinanciero,IGV,fecOperacion,comVendedor,cuoInicial,idMoneda,tipCambio,tasInteres,status,tienda) VALUES ('" &
                Me.txtTipoMovimiento & "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumDocumento.Text & ",' ',0,' ',1,1,0,0,0,0,'" &
                Me.dtpFecha.Text & "',0,0,1,0,0,'A',' ')"

                SqlString1 = "INSERT INTO vtaDetalle (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV," &
                             "fecOperacion,fe,sa) VALUES ('" & Me.txtTipoMovimiento & "' , '" & Me.txtSerieDocumento.Text & "' , " &
                             Me.txtNumDocumento.Text & ",1,0,0,0,' ','" & Me.dtpFecha.Text & "',' ',' ')"

                SqlString2 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumDocumento.Text & " where tipMovimiento= '" & txtTipoMovimiento & "'"

                ListSqlStrings.Add(SqlString)
                ListSqlStrings.Add(SqlString1)
                ListSqlStrings.Add(SqlString2)

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Documento anulado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se anuló documento.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxTipoVenta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipoVenta.SelectedIndexChanged
        If Me.cbxTipoVenta.SelectedIndex = 1 Then
            Me.cbxTipoCredito.Enabled = True
            Me.txtCanCuotas.Enabled = True
        Else
            Me.cbxTipoCredito.Enabled = False
            Me.txtCanCuotas.Enabled = False
        End If
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"

        Me.txtTipoCambio.Text = devuelveTipoCambio(cadenaString, cbxTipoMoneda.Text)

        If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
            Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Else
            Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        End If
    End Sub
    Private Sub txtNumRecibo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumRecibo.DoubleClick
        matrizRecibos(0, 0) = ""
        frmbuscaRecibo.ShowDialog()
        If matrizRecibos(0, 0) <> "" Then
            Me.txtNumRecibo.Text = matrizRecibos(0, 0)
        End If
    End Sub
    Private oDataSet As DataSet
    Private Sub btnBuscaRecibo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscaRecibo.Click
        Try
            If Me.txtNumRecibo.Text = "" Then
                MsgBox("POr favor, indique el número de recibo  !  !  !", MsgBoxStyle.Information)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("select * from recibosClientes where (concepto='0'or concepto='3'or concepto='4'or concepto='5' or " &
                                                                "concepto='6'or concepto='7' or concepto='8'or concepto='9') and numDocGenACI='' and idRecibo=" & CInt(Me.txtNumRecibo.Text) & " and status<>'X'", Connection)
            Connection.Open()
            daCTaCte.Fill(oDataSet, "recibosClientes")
            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("Por favor, no existe recibo,está anulado, ya fue procesado o concepto no valido  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where zona <> -1 and idCliente Like '" & Me.oDataSet.Tables(0).Rows(0).Item(8) & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")
            Connection.Close()

            '--------------- Datos del Cliente --------------------
            Me.txtCodigoCliente = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(4)
            Me.txtZonaCliente = Me.oDataSet.Tables(1).Rows(0).Item(13)
            '--------------- Datos del Recibo ---------------------
            Me.chrConcepto = Me.oDataSet.Tables(0).Rows(0).Item(1).ToString.Trim

            If chrConcepto = "3" Or chrConcepto = "4" Then
                Me.numDocGenCI = Mid(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString.Trim, 3, Len(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString.Trim) - 2)
                Me.tipDocGenCI = Microsoft.VisualBasic.Left(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString.Trim, 2)
                If Me.tipDocGenCI = "FV" Then
                    Me.codDocGenCI = "02"
                Else
                    Me.codDocGenCI = "03"
                End If
            End If
            Me.serDocGenCI = Microsoft.VisualBasic.Left(Me.tipDocGenCI, 1) + "001"

            If numModulo = 1 Then
                Me.txtMonto = totalRecibosMN
                Me.txtMontoME = totalRecibosME
            Else
                If numModulo = 2 Then 'Cuota Inicial
                    Me.txtMonto = totalRecibosMN
                    Me.txtMontoME = totalRecibosME
                Else
                    If numModulo = 3 Then 'Anticipo Cuota
                        Me.txtMonto = totalRecibosMN
                        Me.txtMontoME = totalRecibosME
                    Else
                        If numModulo = 4 Then
                            Me.txtMonto = totalRecibosMN
                            Me.txtMontoME = totalRecibosME
                        Else
                            If numModulo = 5 Then
                                Me.txtMonto = totalRecibosMN
                                Me.txtMontoME = totalRecibosME
                            Else
                                If numModulo = 6 Then
                                    Me.txtMonto = totalRecibosMN
                                    Me.txtMontoME = totalRecibosME
                                Else
                                    If numModulo = 7 Then
                                        Me.txtMonto = totalRecibosMN
                                        Me.txtMontoME = totalRecibosME
                                    Else
                                        If numModulo = 8 Then
                                            Me.txtMonto = totalRecibosMN
                                            Me.txtMontoME = totalRecibosME
                                        Else
                                            If numModulo = 9 Then
                                                Me.txtMonto = totalRecibosMN
                                                Me.txtMontoME = totalRecibosME
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If Me.oDataSet.Tables(0).Rows(0).Item(14) > 1 Then
                Me.txtCuotaInicial = Me.txtMontoME
                txtTotalRecibos.Text = Format(Me.txtMontoME, "###,##0.00")
            Else
                Me.txtCuotaInicial = Me.txtMonto
                txtTotalRecibos.Text = Format(Me.txtMonto, "###,##0.00")
            End If

            Me.vCuotaInicial = Me.txtCuotaInicial
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(14) - 1
            Me.txtTipoCambio.Text = Me.oDataSet.Tables(0).Rows(0).Item(15)
            Me.txtTipoCambio1 = Me.txtTipoCambio.Text

            If Me.chrConcepto = "0" Or Me.chrConcepto = "5" Or Me.chrConcepto = "6" Or Me.chrConcepto = "7" Or Me.chrConcepto = "8" Or Me.chrConcepto = "9" Then
                Me.cbxTipoVenta.SelectedIndex = 0
            Else
                If Me.chrConcepto = "3" Or Me.chrConcepto = "4" Then
                    Me.chrConcepto = "1"
                    Me.cbxTipoVenta.SelectedIndex = 1
                End If
            End If

            Me.oDataSet.Tables.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub procesaRegistros()
        Try

            Dim oProducto As Producto = New Producto()
            Dim percentPrecio As Decimal
            Dim sqlString As String = "select * from tasasCreditos where numMes=" & vCantidadMeses & ""

            Me.txtSubTotal.Text = 0
            Me.txtImpLetrav = 0
            Me.txtImpLetra = 0
            Me.txtInteresv = 0
            Me.txtInteres.Text = 0
            Me.txtTotalPagarv = 0
            Me.txtTotalPagar.Text = 0
            Me.txtPrecioVenta = 0
            Me.txtCuotaInicial = vCuotaInicial
            Me.dsctoLetra = 0
            Me.txtComVendedor = 0

            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                Me.dgvProductos.Rows(i).Cells(10).Value = Me.dgvProductos.Rows(i).Cells(5).Value * Me.dgvProductos.Rows(i).Cells(9).Value
                Me.txtComVendedor = Me.txtComVendedor + Me.dgvProductos.Rows(i).Cells(14).Value * Me.dgvProductos.Rows(i).Cells(5).Value

                If chrConcepto = "10" And Me.cbxTipoVenta.SelectedIndex = 1 Then
                    dsctoLetra = txtCuotaInicial \ Me.txtCanCuotas.Text
                    txtCuotaInicial = 0
                End If

                If Me.cbxTipoVenta.SelectedIndex = 1 Then
                    Me.txtSubTotal.Text = Format(Val(Me.txtSubTotal.Text) + Val(Me.dgvProductos.Rows(i).Cells(10).Value) - txtCuotaInicial, "#####0.00")
                Else
                    Me.txtSubTotal.Text = Format(Val(Me.txtSubTotal.Text) + Val(Me.dgvProductos.Rows(i).Cells(10).Value), "#####0.00")
                End If

                If Me.cbxTipoVenta.SelectedIndex = 1 Then
                    If Me.dgvProductos.Rows(i).Cells(5).Value > 0 Then
                        percentPrecio = Format(Me.txtCuotaInicial / Val(Me.dgvProductos.Rows(i).Cells(5).Value), "0.00")
                    End If
                    txtTasa = devuelveTasa(sqlString, percentPrecio)

                    Me.txtImpLetrav = Format(oProducto.importeLetra(Val(Me.dgvProductos.Rows(i).Cells(5).Value), txtCuotaInicial, txtTasa, Me.cbxTipoCredito.SelectedIndex), "####0.00")

                    valorDecimal = CByte(VisualBasic.Right(Me.txtImpLetrav, 2))
                    If valorDecimal >= 1 Then
                        Me.txtImpLetrav = Math.Floor(Me.txtImpLetrav) + 1
                    End If

                    Me.txtImpLetra = Me.txtImpLetra + Me.txtImpLetrav - dsctoLetra

                    Me.txtTotalPagarv = Me.txtImpLetrav * Me.vCantidadCuotas
                    Me.txtPrecioVenta = Val(Me.dgvProductos.Rows(i).Cells(5).Value) - txtCuotaInicial
                    Me.txtInteresv = Me.txtTotalPagarv - Me.txtPrecioVenta
                    Me.txtInteres.Text = Format(Val(Me.txtInteres.Text) + Me.txtInteresv, "#####0.00")
                    Me.txtTotalPagar.Text = Format(Val(Me.txtTotalPagar.Text) + Me.txtTotalPagarv, "#####0.00")

                    Me.txtImpLetrav = 0
                    Me.txtInteresv = 0
                    Me.txtTotalPagarv = 0
                    Me.txtPrecioVenta = 0
                    Me.txtCuotaInicial = 0
                    Me.txtNumGuia.Text = Me.txtImpLetra
                Else
                    Me.txtTotalPagar.Text = Format(Val(Me.txtSubTotal.Text), "#####0.00")
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellEndEdit
        Dim oFrmAcceso As New frmaccesoAdministrador()
        Dim temporal As Decimal

        If e.ColumnIndex = 5 Then
            Dim celda = dgvProductos(e.ColumnIndex, e.RowIndex)
            If String.IsNullOrEmpty(CStr(celda.Value)) Then
                celda.Value = 0
            Else
                temporal = CDec(dgvProductos(e.ColumnIndex, e.RowIndex).Value)
                If CDec(dgvProductos(e.ColumnIndex, e.RowIndex).Value) < txtPrecioUnitario Then
                    oFrmAcceso.ShowDialog()
                    If flag <> 1 Then
                        dgvProductos.Rows(e.RowIndex).Cells(5).Value = txtPrecioUnitario
                    Else
                        dgvProductos.Rows(e.RowIndex).Cells(5).Value = temporal
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub txtTotalPagar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalPagar.DoubleClick
        Try
            Me.txtTotalPagar.Text = Format(Val(Me.txtSubTotal.Text) + Val(Me.txtInteres.Text), "#####0.00")
            Me.txtImpLetra = Me.txtTotalPagar.Text / Me.vCantidadCuotas

            valorDecimal = CByte(VisualBasic.Right(Me.txtImpLetra, 2))
            If valorDecimal >= 1 Then
                Me.txtImpLetra = Math.Floor(Me.txtImpLetra) + 1
            End If
            Me.txtNumGuia.Text = Me.txtImpLetra
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtcanCuotas_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCanCuotas.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNumRecibo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumRecibo.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 5 Then
            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvProductos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellContentClick
        Me.item = 0
        If dgvProductos.Columns(e.ColumnIndex).Name = "eliminar" Then
            Try
                dgvProductos.Rows.RemoveAt(e.RowIndex)
                Me.txtSubTotal.Text = 0
                Me.txtInteres.Text = 0
                Me.txtIGV.Text = 0
                Me.txtTotalPagar.Text = 0
                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    item += 1
                    dgvProductos.Rows(i).Cells(0).Value = item
                    procesaRegistros()
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipMovimiento='BV'")
        Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1

        Me.txtStringNumDocumento = ""
        Me.txtStringLetra = ""
        Me.txtCodigoCliente = ""
        Me.txtZonaCliente = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtNumGuia.Text = ""
        Me.txtNumRecibo.Text = ""
        Me.txtTotalRecibos.Text = "0"
        Me.txtGlosa.Text = ""
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtGrupoProducto = 0
        Me.txtMonto = 0
        Me.txtMontoME = 0
        Me.txtImpLetra = 0
        Me.txtImpLetraME = 0
        Me.vSemana = 0
        Me.vQuincena = 0
        Me.pase = 0
        Me.sinRecibo = 0
        Me.txtComVendedor = 0
        Me.txtCuotaInicial = 0
        Me.txtTotalAnticipos.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtTipoCambio1 = 0
        Me.txtTasa = 0
        Me.resto = 0
        Me.vCantidadCuotas = 0
        Me.txtCanCuotas.Text = 1
        Me.dsctoLetra = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 0
        numModulo = 0
        For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
            matrizRecibos(y, 0) = ""
            matrizRecibos(y, 1) = ""
        Next y
        Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        'Me.cbxTipoMoneda.Enabled = True
        Me.cbxTipoCredito.Enabled = False
        Me.txtCanCuotas.Enabled = False
        Me.cbxGarantia.Enabled = True
        Me.dgvProductos.Rows.Clear()
        Me.btnBuscarCliente.Focus()
    End Sub
    Private Sub limpiarDataGridView()
        Me.dgvProductos.Rows.Clear()
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalAnticipos.Text = 0
        Me.txtTotalPagar.Text = 0
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    'Sub GENERAR_QR_CODE(Ruc As String, TipoDoc As String, cSerie As String, cCorrelativo As String, nTotal As String, DniRuc As String, Picture As PictureBox)
    '    Dim GENERADOR As BarcodeWriter = New BarcodeWriter 'INICIALIZA EL GENERADOR       
    '    GENERADOR.Format = BarcodeFormat.QR_CODE

    '    Dim textQR As String = ""
    '    Dim fecha = Today.Year.ToString & "-" & Today.Month.ToString & "-" & Today.Day.ToString

    '    textQR = Ruc & "|" & TipoDoc & "|" & cSerie & "|" & cCorrelativo & "|0|" & nTotal & "|" & fecha & "|6|" & DniRuc & "|"

    '    Try 'GENERA UN BITMAP Y LO PRESENTA EN EL PICTUREBOX
    '        Dim IMAGEN As Bitmap = New Bitmap(GENERADOR.Write(textQR), 1000, 1000)
    '        Picture.Image = IMAGEN
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub



End Class