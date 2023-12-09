Imports Microsoft
Imports Libreria
Imports System.Data.SqlClient
Imports System.IO
Public Class frmreciboPago
    Private oDataSet As DataSet
    Dim strUltimoNumeroDoc As String
    Dim txtTipoDocumento As String = "RC"
    Dim txtNumDocumento, txtStringNumDoc As String
    Dim codCliente, vnumCorrelativo, txtNumCorrelativo As String
    Dim vfecEmision, vfecVencimiento, vfecPago, vNvoVcto As Date
    Dim tipoCambioLetra, vMonto, vMontoME, vMontoOriginal, vMontoOriginalME As Decimal
    Dim vMontoAmortizar, vMontoAmortizarME, txtMontoCancelar, txtMontoCancelarME As Decimal
    Dim arrayDatosCheques(10, 6) As Object
    Dim dVencidos As Integer
    Dim tInteres, vLetra As Decimal
    Dim interesMN, interesME As Decimal
    Dim cOPeracionMN, cOPeracionME As Decimal
    Dim pase1 As Integer = 0
    Dim pase As Integer = 0
    Dim tPagoAnterior, valorDecimal As Byte
    Dim strSuma As String
    Dim flagAmortiza As Integer
    Dim paseAmortizacion As Integer = 0
    Dim flagGraba As Boolean
    Dim status As String
    Dim flagAdelantoVtaCash As Boolean
    Dim te As New RichTextBox
    Private Sub frmreciboPago_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.cbxTipoDocumento.SelectedIndex = 0

        Dim strCodigoVendedor As String = ("SELECT * FROM Vendedores where idVendedor=1")
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='RC'")
        Me.strUltimoNumeroDoc = ("SELECT * FROM ultimosNumeros where tipMovimiento='" & Me.cbxTipoDocumento.Text & "'")

        Me.vnumCorrelativo = 0
        Me.txtMonto.Text = 0
        Me.txtMontoME.Text = 0
        Me.txtDiasVencidos.Text = 0
        Me.txtInteres.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtPMultiple.Text = 0
        Me.txtDiferencia.Text = 0
        Me.txtInteresPagoCuota.Text = 0
        Me.cOPeracionMN = 0
        Me.cOPeracionME = 0

        Me.txtNumDocumento = devuelveUltimoNumero(strUltimoNumeroDoc) + 1
        Me.txtNumRecibo.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)

        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoPago.SelectedIndex = 2
        Me.cbxConcepto.SelectedIndex = 2

        Me.tPagoAnterior = Me.cbxTipoPago.SelectedIndex
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True
    End Sub
    Private Sub frmreciboPago_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            If cbxTipoDocumento.SelectedIndex = 1 Then
                Me.txtDNI.Text = arrayDatos(3)
            Else
                Me.txtDNI.Text = arrayDatos(4)
            End If
            igualaVacio()
        End If
    End Sub
    Private Sub btnNuevoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
        arrayDatos(0) = ""
        frmnuevoCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            If cbxTipoDocumento.SelectedIndex = 1 Then
                Me.txtDNI.Text = arrayDatos(3)
            Else
                Me.txtDNI.Text = arrayDatos(4)
            End If
            igualaVacio()
        End If
    End Sub
    Private Sub cbxTipoDocumento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipoDocumento.SelectedIndexChanged
        If Me.cbxTipoDocumento.SelectedIndex = 0 Then
            txtSerie.Text = "B001"
        Else
            txtSerie.Text = "F001"
        End If
        strUltimoNumeroDoc = ("select * from ultimosNumeros where tipMovimiento='" & Me.cbxTipoDocumento.Text & "'")
        Me.txtNumDocumento = devuelveUltimoNumero(strUltimoNumeroDoc) + 1
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim sqlStringNumero As String = "select *from recibosClientes where idRecibo=" & CInt(Me.txtNumRecibo.Text) & ""
        Dim oProducto As Producto = New Producto()

        Dim txtStatusC As String = "C"
        Dim txtStatusA As String = "A"
        Dim sqlString As String = ""
        Dim sqlString1 As String = ""
        Dim sqlString2 As String = ""
        Dim sqlString3 As String = ""
        Dim sqlString4 As String = ""

        Dim sqlStringCO As String = ""
        Dim sqlStringCO1 As String = ""
        Dim sqlStringArray As String = ""

        Dim ListSqlStrings As New ArrayList
        Dim ListSqlStringsCO As New ArrayList
        Dim ListSqlStringsCO1 As New ArrayList
        Dim ListSqlStringsArray As New ArrayList

        Try
            oDataSet = New DataSet()
            If Me.txtCodigoCliente.Text <> "" And (Me.txtMonto.Text > 0 Or Me.txtMontoME.Text > 0) Then
                If verificaNumeroDocumento(sqlStringNumero) = True Then
                    MsgBox("Por favor, documento ya fue grabado o ya existe número.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            Dim ctaDatosCheques As Integer = 0
            For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                If arrayDatosCheques(x, 0) <> "" Then
                    ctaDatosCheques = +1
                End If
            Next

            If ctaDatosCheques <= 0 Then
                Me.arrayDatosCheques(0, 0) = "Pago Efectivo"
                Me.arrayDatosCheques(0, 1) = Me.cbxTipoPago.Text

                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    Me.arrayDatosCheques(0, 2) = Me.txtMontoME.Text
                    Me.arrayDatosCheques(0, 4) = Me.txtMontoME.Text
                    'Me.arrayDatosCheques(0, 5) = Me.cbxTipoPago.SelectedIndex
                Else
                    Me.arrayDatosCheques(0, 2) = Me.txtMonto.Text
                    Me.arrayDatosCheques(0, 4) = Me.txtMonto.Text
                    'Me.arrayDatosCheques(0, 5) = Me.cbxTipoPago.SelectedIndex
                End If
                Me.arrayDatosCheques(0, 3) = Me.txtTipoCambio.Text
                Me.arrayDatosCheques(0, 5) = Me.cbxTipoPago.SelectedIndex
                Me.arrayDatosCheques(0, 6) = Me.cbxTipoMoneda.SelectedIndex + 1
            End If

            If Me.cbxConcepto.SelectedIndex <> 5 And Me.cbxConcepto.SelectedIndex <> 6 And Me.cbxConcepto.SelectedIndex <> 7 Then
                If Me.cbxTipoMoneda.SelectedIndex = 0 Then
                    cOPeracionMN = Me.txtMonto.Text * 0.055
                Else
                    cOPeracionME = Me.txtMontoME.Text * 0.055
                End If
            End If

            If Me.cbxConcepto.SelectedIndex = 0 Then '---------- Tipo Concepto: Venta Contado
                If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                    MsgBox("Faltan datos para la operación de Venta al Contado.", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                'If flagAdelantoVtaCash = True Then status = "A"

                sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                             Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                             "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                             ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                    If arrayDatosCheques(x, 0) <> "" Then
                        sqlStringArray = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                     Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                     arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                     Val(arrayDatosCheques(x, 4)) & ")"
                        ListSqlStringsArray.Add(sqlStringArray)
                    End If
                Next

                sqlString1 = "update ultimosNumeros set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                ListSqlStrings.Add(sqlString)
                ListSqlStrings.Add(sqlString1)

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Información procesada correctamente  !  !  !", MsgBoxStyle.Information)
                    flagGraba = True

                    If transaccionLetras(ListSqlStringsArray) Then
                    Else
                        MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                    End If

                    If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                        Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                        Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                        Dim monto As Decimal

                        sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                    "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                     numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                     Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                        ListSqlStringsCO.Add(sqlString)

                        If cbxTipoMoneda.SelectedIndex = 0 Then
                            monto = cOPeracionMN
                        Else
                            monto = cOPeracionME
                        End If

                        sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                     Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                        ListSqlStringsCO.Add(sqlString)

                        sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                        ListSqlStringsCO.Add(sqlString)

                        If transaccionLetras(ListSqlStringsCO) Then
                            MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                        Else
                            MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                        End If
                    End If
                Else
                    MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                End If
            Else
                If Me.cbxConcepto.SelectedIndex = 1 Then '---------- Tipo Concepto: Amortizar Letra
                    If Me.txtCodigoCliente.Text = "" Or Me.txtNumLetra.Text = "" Then
                        MsgBox("Falta datos para la operación de Amortizar Letras.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    If (txtMonto.Text >= vMontoAmortizar) And (txtMontoME.Text >= vMontoAmortizarME) Then
                        MsgBox("Operacion indebida, monto no puede ser mayor o igual a la deuda.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                    vMontoAmortizar = 0
                    vMontoAmortizarME = 0

                    txtStringNumDoc = oProducto.stringLetra(Me.txtTipoDocumento, Me.txtNumRecibo.Text, " ", " ")
                    sqlString = "UPDATE letrasClientes Set numRecibo='" & Me.txtNumRecibo.Text & "', status='" & txtStatusA & "' where numLetra='" & _
                    txtNumLetra.Text & "' and numCorrelativo=" & CInt(txtNumCorrelativo) & ""

                    sqlString1 = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                   "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                    Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & _
                                    Me.txtMonto.Text & "," & Me.txtMontoME.Text & "," & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & _
                                    "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "','" & Me.vfecVencimiento & "',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & _
                                    "," & Me.txtTipoCambio.Text & ",'" & txtStatusA & "')"

                    For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                        If arrayDatosCheques(x, 0) <> "" Then
                            sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                         Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                         arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                         Val(arrayDatosCheques(x, 4)) & ")"
                            ListSqlStringsArray.Add(sqlStringArray)
                        End If
                    Next

                    sqlString2 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & Me.txtTipoDocumento & "'"

                    ListSqlStrings.Add(sqlString)
                    ListSqlStrings.Add(sqlString1)
                    ListSqlStrings.Add(sqlString2)

                    If transaccionLetras(ListSqlStrings) Then
                        MsgBox("Documentos procesados correctamente.", MsgBoxStyle.Information)
                        flagGraba = True

                        If transaccionLetras(ListSqlStringsArray) Then
                        Else
                            MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                        End If

                        If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                            Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                            Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                            Dim monto As Decimal

                            sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                        "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                         numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                         Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                            ListSqlStringsCO.Add(sqlString)

                            If cbxTipoMoneda.SelectedIndex = 0 Then
                                monto = cOPeracionMN
                            Else
                                monto = cOPeracionME
                            End If

                            sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                         Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                            ListSqlStringsCO.Add(sqlString)

                            sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                            ListSqlStringsCO.Add(sqlString)

                            If transaccionLetras(ListSqlStringsCO) Then
                                MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                            Else
                                MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                            End If
                        End If

                        If Me.dVencidos >= 9 Then
                            Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                            Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                            Dim interes As Decimal

                            sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                           "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                            numRecibo & ",11,'" & Me.txtNumLetra.Text & "'," & interesMN & "," & interesME & "," & vnumCorrelativo & _
                                            ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "','" & _
                                            Me.dtpFechaVcmto.Text & "',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'I')"
                            ListSqlStringsCO1.Add(sqlString)

                            If cbxTipoMoneda.SelectedIndex = 0 Then
                                interes = interesMN
                            Else
                                interes = interesME
                            End If

                            sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                         Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & interes & ",1.0," & interes & ")"
                            ListSqlStringsCO1.Add(sqlString)

                            sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                            ListSqlStringsCO1.Add(sqlString)

                            If transaccionLetras(ListSqlStringsCO1) Then
                                MsgBox("Proceso a generado interés, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes' ! ! !", MsgBoxStyle.Information)
                            Else
                                MsgBox("El Proceso de generación de cobro interés a fallado.", MsgBoxStyle.Critical)
                            End If
                        End If
                    Else
                        MsgBox("El Proceso de Amortización de Letras a fallado.", MsgBoxStyle.Critical)
                    End If
                Else
                    If Me.cbxConcepto.SelectedIndex = 2 Then '---------- Tipo Concepto: Cancelar Letras
                        If CDec(Me.txtDiferencia.Text) > 0 Then
                            Me.arrayDatosCheques(ctaDatosCheques + 1, 0) = "Pago Efectivo"
                            Me.arrayDatosCheques(ctaDatosCheques + 1, 1) = Me.cbxTipoPago.Text
                            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                                Me.arrayDatosCheques(ctaDatosCheques + 1, 2) = Val(Me.txtDiferencia.Text)
                                Me.arrayDatosCheques(ctaDatosCheques + 1, 4) = Val(Me.txtDiferencia.Text)
                            Else
                                Me.arrayDatosCheques(ctaDatosCheques + 1, 2) = Val(Me.txtDiferencia.Text)
                                Me.arrayDatosCheques(ctaDatosCheques + 1, 4) = Val(Me.txtDiferencia.Text)
                            End If
                            Me.arrayDatosCheques(ctaDatosCheques + 1, 3) = Me.txtTipoCambio.Text
                            Me.arrayDatosCheques(ctaDatosCheques + 1, 5) = 5
                            Me.arrayDatosCheques(ctaDatosCheques + 1, 6) = Me.cbxTipoMoneda.SelectedIndex + 1
                        End If

                        Me.txtPMultiple.Text = Format(Val(Me.txtPMultiple.Text) + Val(Me.txtDiferencia.Text), "#####0.00")
                        Me.txtDiferencia.Text = 0

                        If Me.txtCodigoCliente.Text = "" Or Me.txtNumLetra.Text = "" Then
                            MsgBox("Falta datos para la operación de Cancelar Letras.", MsgBoxStyle.Critical)
                            Exit Sub
                        End If

                        txtStringNumDoc = oProducto.stringLetra(Me.txtTipoDocumento, Me.txtNumRecibo.Text, " ", " ")
                        sqlString = "UPDATE letrasClientes Set fecPago='" & Me.dtpFecha.Text & "', numRecibo='" & Me.txtNumRecibo.Text & _
                                    "', status='" & txtStatusC & "' where numLetra='" & txtNumLetra.Text & "' and numCorrelativo=" & CInt(txtNumCorrelativo) & ""

                        sqlString1 = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                       "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                        Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & _
                                        Me.txtMonto.Text & "," & Me.txtMontoME.Text & "," & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & _
                                        "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "','" & Me.vfecVencimiento & "',' ',0," & _
                                        Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & txtStatusC & "')"

                        For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                            If arrayDatosCheques(x, 0) <> "" Then
                                sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                             Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                             arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                             Val(arrayDatosCheques(x, 4)) & ")"
                                ListSqlStringsArray.Add(sqlStringArray)
                            End If
                        Next

                        sqlString2 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & Me.txtTipoDocumento & "'"

                        ListSqlStrings.Add(sqlString)
                        ListSqlStrings.Add(sqlString1)
                        ListSqlStrings.Add(sqlString2)

                        If transaccionLetras(ListSqlStrings) Then
                            MsgBox("Documentos procesados correctamente.", MsgBoxStyle.Information)
                            flagGraba = True

                            If transaccionLetras(ListSqlStringsArray) Then
                            Else
                                MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                            End If

                            If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                                Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                                Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                                Dim monto As Decimal

                                sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                             numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                             Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                                ListSqlStringsCO.Add(sqlString)

                                If cbxTipoMoneda.SelectedIndex = 0 Then
                                    monto = cOPeracionMN
                                Else
                                    monto = cOPeracionME
                                End If

                                sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                             Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                                ListSqlStringsCO.Add(sqlString)

                                sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                                ListSqlStringsCO.Add(sqlString)

                                If transaccionLetras(ListSqlStringsCO) Then
                                    MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                                Else
                                    MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                                End If
                            End If

                            If Me.dVencidos >= 9 Then
                                Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                                Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                                Dim interes As Decimal

                                sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                               "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                numRecibo & ",11,'" & Me.txtNumLetra.Text & "'," & interesMN & "," & interesME & "," & vnumCorrelativo & _
                                                ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "','" & _
                                                Me.dtpFechaVcmto.Text & "',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'I')"
                                ListSqlStringsCO1.Add(sqlString)

                                If cbxTipoMoneda.SelectedIndex = 0 Then
                                    interes = interesMN
                                Else
                                    interes = interesME
                                End If

                                sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                             Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & interes & ",1.0," & interes & ")"
                                ListSqlStringsCO1.Add(sqlString)

                                sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                                ListSqlStringsCO1.Add(sqlString)

                                If transaccionLetras(ListSqlStringsCO1) Then
                                    MsgBox("Proceso a generado interés, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes' ! ! !", MsgBoxStyle.Information)
                                Else
                                    MsgBox("El Proceso de generación de cobro interés a fallado.", MsgBoxStyle.Critical)
                                End If
                            End If
                        Else
                            MsgBox("El Proceso de cancelación de letra a fallado.", MsgBoxStyle.Critical)
                        End If
                    Else
                        If Me.cbxConcepto.SelectedIndex = 3 Then ' ---------- Tipo Concepto: Cuota Inicial

                            If Me.txtCodigoCliente.Text = "" Then
                                MsgBox("Falta datos del cliente para la operación de 'Cuota Inicial' ! ! !", MsgBoxStyle.Critical)
                                Exit Sub
                            End If

                            If Me.cbxTipoDocumento.SelectedIndex = 1 And (Me.txtDNI.Text = "" Or Len(Me.txtDNI.Text) < 11) Then
                                MsgBox("Operación con este documento exige N° RUC.", MsgBoxStyle.Critical)
                                Exit Sub
                            End If

                            txtStringNumDoc = oProducto.stringLetra(Me.cbxTipoDocumento.Text, Me.txtNumDocumento, " ", " ")
                            sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                        "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                         Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & _
                                         Me.txtMonto.Text & "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",'" & Me.txtStringNumDoc.Trim & _
                                         "',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & _
                                         "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"

                            For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                If arrayDatosCheques(x, 0) <> "" Then
                                    sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                 Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                 arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                 Val(arrayDatosCheques(x, 4)) & ")"
                                    ListSqlStringsArray.Add(sqlStringArray)
                                End If
                            Next

                            sqlString1 = "insert into vtaCabecera (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,numLetra,idCliente,idVendedor,totVentaMN," & _
                                         "totVentaME,intFinanciero,IGV,fecOperacion,comVendedor,cuoInicial,idMoneda,tipCambio,tasInteres,statusNC,statusNA,staEnvio,status) VALUES ('" & _
                                         Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",' ','" & Me.cbxConcepto.SelectedIndex & _
                                         "',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & "," & Me.txtMonto.Text & "," & _
                                         Me.txtMontoME.Text & ",0,0,'" & Me.dtpFecha.Text & "',0,0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",0,'','','','')"

                            sqlString2 = "insert into vtaDetalle (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV,fecOperacion,status) VALUES ('" & _
                                         Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",4," & Me.txtMonto.Text & ",1," & Me.txtMonto.Text & ",0,'" & Me.dtpFecha.Text & "','')"

                            sqlString3 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"
                            sqlString4 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumDocumento) & " where tipMovimiento= '" & Me.cbxTipoDocumento.Text & "'"

                            ListSqlStrings.Add(sqlString)
                            ListSqlStrings.Add(sqlString1)
                            ListSqlStrings.Add(sqlString2)
                            ListSqlStrings.Add(sqlString3)
                            ListSqlStrings.Add(sqlString4)

                            If transaccionLetras(ListSqlStrings) Then
                                MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                generarDocumentoPlano()
                                flagGraba = True

                                If transaccionLetras(ListSqlStringsArray) Then
                                Else
                                    MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                End If

                                If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                                    Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                                    Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                                    Dim monto As Decimal

                                    sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                 numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                                 Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                                    ListSqlStringsCO.Add(sqlString)

                                    If cbxTipoMoneda.SelectedIndex = 0 Then
                                        monto = cOPeracionMN
                                    Else
                                        monto = cOPeracionME
                                    End If

                                    sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                 Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                                    ListSqlStringsCO.Add(sqlString)

                                    sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                                    ListSqlStringsCO.Add(sqlString)

                                    If transaccionLetras(ListSqlStringsCO) Then
                                        MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                                    Else
                                        MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                                    End If
                                End If
                            Else
                                MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                            End If
                        Else
                            If Me.cbxConcepto.SelectedIndex = 4 Then ' ---------- Tipo Concepto: Anticipo Cuota Inicial
                                If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                    MsgBox("Falta datos para la operación de 'Anticipo de Cuota'  !  !  !", MsgBoxStyle.Critical)
                                    Exit Sub
                                End If

                                If Me.cbxTipoDocumento.SelectedIndex = 1 And (Me.txtDNI.Text = "" Or Len(Me.txtDNI.Text) < 11) Then
                                    MsgBox("Operación con este documento exige N° RUC.", MsgBoxStyle.Critical)
                                    Exit Sub
                                End If

                                txtStringNumDoc = oProducto.stringLetra(Me.cbxTipoDocumento.Text, Me.txtNumDocumento, " ", " ")
                                sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                             Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & _
                                             Me.txtMonto.Text & "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",'" & Me.txtStringNumDoc.Trim & _
                                             "',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & _
                                             "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"

                                For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                    If arrayDatosCheques(x, 0) <> "" Then
                                        sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                     Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                     arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                     Val(arrayDatosCheques(x, 4)) & ")"
                                        ListSqlStringsArray.Add(sqlStringArray)
                                    End If
                                Next

                                sqlString1 = "INSERT INTO vtaCabecera (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,numLetra,idCliente,idVendedor,totVentaMN," & _
                                             "totVentaME,intFinanciero,IGV,fecOperacion,comVendedor,cuoInicial,idMoneda,tipCambio,tasInteres,statusNC,statusNA,staEnvio,status) VALUES ('" & _
                                             Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",' ','" & Me.cbxConcepto.SelectedIndex & _
                                             "',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & "," & Me.txtMonto.Text & "," & _
                                            Me.txtMontoME.Text & ",0,0,'" & Me.dtpFecha.Text & "',0,0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",0,'','','','')"

                                sqlString2 = "INSERT INTO vtaDetalle (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV,fecOperacion,status) VALUES ('" & _
                                             Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",5," & Me.txtMonto.Text & ",1," & Me.txtMonto.Text & ",0,'" & Me.dtpFecha.Text & "','')"

                                sqlString3 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"
                                sqlString4 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumDocumento) & " where tipMovimiento= '" & Me.cbxTipoDocumento.Text & "'"

                                ListSqlStrings.Add(sqlString)
                                ListSqlStrings.Add(sqlString1)
                                ListSqlStrings.Add(sqlString2)
                                ListSqlStrings.Add(sqlString3)
                                ListSqlStrings.Add(sqlString4)

                                If transaccionLetras(ListSqlStrings) Then
                                    MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                    generarDocumentoPlano()
                                    flagGraba = True

                                    If transaccionLetras(ListSqlStringsArray) Then
                                    Else
                                        MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                    End If

                                    If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                                        Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                                        Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                                        Dim monto As Decimal

                                        sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                    "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                     numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                                     Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                                        ListSqlStringsCO.Add(sqlString)

                                        If cbxTipoMoneda.SelectedIndex = 0 Then
                                            monto = cOPeracionMN
                                        Else
                                            monto = cOPeracionME
                                        End If

                                        sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                     Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                                        ListSqlStringsCO.Add(sqlString)

                                        sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                                        ListSqlStringsCO.Add(sqlString)

                                        If transaccionLetras(ListSqlStringsCO) Then
                                            MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                                        Else
                                            MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                                        End If
                                    End If
                                Else
                                    MsgBox("El Proceso ha fallado.", MsgBoxStyle.Critical)
                                End If
                            Else
                                If Me.cbxConcepto.SelectedIndex = 5 Then '------------- Tipo Concepto: Venta Tarjeta

                                    If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                        MsgBox("Faltan datos para la operación de 'Venta Tarjeta'  !  !  !", MsgBoxStyle.Critical)
                                        Exit Sub
                                    End If

                                    sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                 Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                                                 "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                                                 ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                                    For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                        If arrayDatosCheques(x, 0) <> "" Then
                                            sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                         Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                         arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                         Val(arrayDatosCheques(x, 4)) & ")"
                                            ListSqlStringsArray.Add(sqlStringArray)
                                        End If
                                    Next

                                    sqlString1 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                                    ListSqlStrings.Add(sqlString)
                                    ListSqlStrings.Add(sqlString1)

                                    If transaccionLetras(ListSqlStrings) Then
                                        MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                        flagGraba = True

                                        If transaccionLetras(ListSqlStringsArray) Then
                                        Else
                                            MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                        End If
                                    Else
                                        MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                    End If
                                Else
                                    If Me.cbxConcepto.SelectedIndex = 6 Then '------------- Tipo Concepto: Venta Tarjeta Oferta

                                        If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                            MsgBox("Faltan datos para la operación de 'Venta Tarjeta Oferta'  !  !  !", MsgBoxStyle.Critical)
                                            Exit Sub
                                        End If

                                        sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                    "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                     Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                                                     "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                                                     ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                                        For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                            If arrayDatosCheques(x, 0) <> "" Then
                                                sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                             Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                             arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                             Val(arrayDatosCheques(x, 4)) & ")"
                                                ListSqlStringsArray.Add(sqlStringArray)
                                            End If
                                        Next

                                        sqlString1 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                                        ListSqlStrings.Add(sqlString)
                                        ListSqlStrings.Add(sqlString1)

                                        If transaccionLetras(ListSqlStrings) Then
                                            MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                            flagGraba = True

                                            If transaccionLetras(ListSqlStringsArray) Then
                                            Else
                                                MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                            End If
                                        Else
                                            MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                        End If
                                    Else
                                        If Me.cbxConcepto.SelectedIndex = 7 Then '------------- Tipo Concepto: Venta Tarjeta Remate

                                            If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                                MsgBox("Faltan datos para la operación de 'Venta Tarjeta Remate' !  !  !", MsgBoxStyle.Critical)
                                                Exit Sub
                                            End If

                                            sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                        "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                         Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                                                         "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                                                         ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                                            For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                                If arrayDatosCheques(x, 0) <> "" Then
                                                    sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                                 Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                                 arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                                 Val(arrayDatosCheques(x, 4)) & ")"
                                                    ListSqlStringsArray.Add(sqlStringArray)
                                                End If
                                            Next

                                            sqlString1 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                                            ListSqlStrings.Add(sqlString)
                                            ListSqlStrings.Add(sqlString1)

                                            If transaccionLetras(ListSqlStrings) Then
                                                MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                                flagGraba = True

                                                If transaccionLetras(ListSqlStringsArray) Then
                                                Else
                                                    MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                                End If
                                            Else
                                                MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                            End If
                                        Else
                                            If Me.cbxConcepto.SelectedIndex = 8 Then '------------- Tipo Concepto: Venta Oferta

                                                If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                                    MsgBox("Faltan datos para la operación de 'Venta Oferta'  !  !  !", MsgBoxStyle.Critical)
                                                    Exit Sub
                                                End If

                                                sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                             Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                                                             "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                                                             ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                                                For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                                    If arrayDatosCheques(x, 0) <> "" Then
                                                        sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                                     Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                                     arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                                     Val(arrayDatosCheques(x, 4)) & ")"
                                                        ListSqlStringsArray.Add(sqlStringArray)
                                                    End If
                                                Next

                                                sqlString1 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                                                ListSqlStrings.Add(sqlString)
                                                ListSqlStrings.Add(sqlString1)

                                                If transaccionLetras(ListSqlStrings) Then
                                                    MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                                    flagGraba = True

                                                    If transaccionLetras(ListSqlStringsArray) Then
                                                    Else
                                                        MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                                    End If
                                                Else
                                                    MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                                End If
                                            Else
                                                If Me.cbxConcepto.SelectedIndex = 9 Then '------------- Tipo Concepto: Venta Remate

                                                    If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                                        MsgBox("Faltan datos para la operación de 'Venta Remate'  !  !  !", MsgBoxStyle.Critical)
                                                        Exit Sub
                                                    End If

                                                    sqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                                "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                                 Me.txtNumRecibo.Text & ", " & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & Me.txtMonto.Text & _
                                                                 "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & _
                                                                 ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",'" & status & "')"

                                                    For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                                        If arrayDatosCheques(x, 0) <> "" Then
                                                            sqlStringArray = "INSERT INTO datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                                         Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                                         arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                                         Val(arrayDatosCheques(x, 4)) & ")"
                                                            ListSqlStringsArray.Add(sqlStringArray)
                                                        End If
                                                    Next

                                                    sqlString1 = "UPDATE ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"

                                                    ListSqlStrings.Add(sqlString)
                                                    ListSqlStrings.Add(sqlString1)

                                                    If transaccionLetras(ListSqlStrings) Then
                                                        MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                                                        flagGraba = True

                                                        If transaccionLetras(ListSqlStringsArray) Then
                                                        Else
                                                            MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                                        End If
                                                    Else
                                                        MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                                    End If
                                                Else
                                                    If Me.cbxConcepto.SelectedIndex = 10 Then '------------- Tipo Concepto: Otros Pagos
                                                        If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                                                            MsgBox("Falta datos para la operación 'Otros Pagos'.", MsgBoxStyle.Critical)
                                                            Exit Sub
                                                        End If

                                                        If Trim(Me.txtOtrosPagos.Text = "") Then
                                                            MsgBox("Ingrese glosa de 'Otros Pagos' para continuar.", MsgBoxStyle.Critical)
                                                            Exit Sub
                                                        End If

                                                        Dim oFrmAcceso As New frmaccesoAdministrador()
                                                        oFrmAcceso.ShowDialog()
                                                        If flag <> 1 Then
                                                            Exit Sub
                                                        End If

                                                        sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                                    "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                                     Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtNumLetra.Text & "'," & _
                                                                     Me.txtMonto.Text & "," & Me.txtMontoME.Text & " , " & vnumCorrelativo & ",' ',' '," & _
                                                                     Me.txtCodigoCliente.Text & "," & Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & _
                                                                     "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                                                        ListSqlStrings.Add(sqlString)

                                                        If Me.txtOtrosPagos.Text <> "" Then
                                                            sqlString = "insert into glosasFacturas (tipDocumento,numDocumento,glosa) VALUES ('" & _
                                                                        Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & txtOtrosPagos.Text & "')"
                                                            ListSqlStrings.Add(sqlString)
                                                        End If

                                                        For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
                                                            If arrayDatosCheques(x, 0) <> "" Then
                                                                sqlStringArray = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                                             Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & arrayDatosCheques(x, 5) & "','" & arrayDatosCheques(x, 0) & "','" & _
                                                                             arrayDatosCheques(x, 1) & "'," & Val(arrayDatosCheques(x, 6)) & "," & Val(arrayDatosCheques(x, 2)) & "," & arrayDatosCheques(x, 3) & "," & _
                                                                             Val(arrayDatosCheques(x, 4)) & ")"
                                                                ListSqlStringsArray.Add(sqlStringArray)
                                                            End If
                                                        Next

                                                        sqlString = "update ultimosNumeros set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"
                                                        ListSqlStrings.Add(sqlString)

                                                        'If MsgBox("Crear documento de venta para esta operación?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                                        '    sqlString = "insert into vtaCabecera (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,numLetra,idCliente,idVendedor,totVentaMN," & _
                                                        '                "totVentaME,intFinanciero,IGV,fecOperacion,comVendedor,cuoInicial,idMoneda,tipCambio,tasInteres,statusNC,statusNA,staEnvio,status) VALUES ('" & _
                                                        '                Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",' ',1,' '," & Me.txtCodigoCliente.Text & "," & _
                                                        '                Me.txtCodigoVendedor.Text & "," & Me.txtMonto.Text & "," & Me.txtMontoME.Text & ",0,0,'" & Me.dtpFecha.Text & "',0,0," & _
                                                        '                Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",0,'','','','')"
                                                        '    ListSqlStrings.Add(sqlString)

                                                        '    sqlString = "insert into vtaDetalle (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV,fecOperacion,status) VALUES ('" & _
                                                        '                 Me.cbxTipoDocumento.Text & "','" & Me.txtSerie.Text & "'," & Me.txtNumDocumento & ",1," & Me.txtMonto.Text & ",1," & Me.txtMonto.Text & ",0,'" & Me.dtpFecha.Text & "','')"
                                                        '    ListSqlStrings.Add(sqlString)

                                                        '    sqlString = "update ultimosNumeros set numero=" & CInt(Me.txtNumDocumento) & " where tipMovimiento= '" & Me.cbxTipoDocumento.Text & "'"
                                                        '    ListSqlStrings.Add(sqlString)
                                                        'End If

                                                        If transaccionLetras(ListSqlStrings) Then
                                                            MsgBox("Información procesada correctamente  !  !  !", MsgBoxStyle.Information)
                                                            flagGraba = True
                                                            If transaccionLetras(ListSqlStringsArray) Then
                                                            Else
                                                                MsgBox("El Proceso de inserción en la tabla 'datosCheques' ha fallado.", MsgBoxStyle.Critical)
                                                            End If

                                                            If Me.cbxTipoPago.SelectedIndex = 4 Then ' Cuando tipo de pago es con tarjeta
                                                                Dim ultimoNumero As String = ("select * from ultimosNumeros where tipDocumento='RC'")
                                                                Dim numRecibo As Integer = devuelveUltimoNumero(ultimoNumero) + 1
                                                                Dim monto As Decimal

                                                                sqlString = "insert into recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                                                                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                                                                             numRecibo & ",12,' '," & cOPeracionMN & "," & cOPeracionME & ",0,' ',' '," & Me.txtCodigoCliente.Text & "," & _
                                                                             Me.txtCodigoVendedor.Text & ",'" & Me.dtpFecha.Text & "',' ',' ',0," & Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
                                                                ListSqlStringsCO.Add(sqlString)

                                                                If cbxTipoMoneda.SelectedIndex = 0 Then
                                                                    monto = cOPeracionMN
                                                                Else
                                                                    monto = cOPeracionME
                                                                End If

                                                                sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                                                             Me.txtTipoDocumento & "'," & numRecibo & ",'" & cbxTipoPago.SelectedIndex & "','',''," & cbxTipoMoneda.SelectedIndex + 1 & "," & monto & ",1.0," & monto & ")"
                                                                ListSqlStringsCO.Add(sqlString)

                                                                sqlString = "update ultimosNumeros set numero=" & numRecibo & " where tipDocumento= 'RC'"
                                                                ListSqlStringsCO.Add(sqlString)

                                                                If transaccionLetras(ListSqlStringsCO) Then
                                                                    MsgBox("Proceso a generado cargo del 5.5%, imprima combrobante en módulo 'Documentos Emitidos' del menú 'Reportes'  ! ! !", MsgBoxStyle.Information)
                                                                Else
                                                                    MsgBox("El Proceso de generación de cargo del 5.5% a fallado.", MsgBoxStyle.Critical)
                                                                End If
                                                            End If
                                                        Else
                                                            MsgBox("El Proceso ha Fallado.", MsgBoxStyle.Critical)
                                                        End If
                                                    Else
                                                        If Me.cbxConcepto.SelectedIndex = 11 Then '------------- Tipo Concepto: Cobro Interés
                                                            MsgBox("Procedimiento inválido, concepto 'Cobro Interés' no se maneja directamente  !  !  !", MsgBoxStyle.Critical)
                                                            Exit Sub
                                                        Else
                                                            'If Me.cbxConcepto.SelectedIndex = 12 Then '------------- Tipo Concepto: Cargo Operación
                                                            MsgBox("Procedimiento inválido, concepto 'Cargo Operación' no se maneja directamente  !  !  !", MsgBoxStyle.Critical)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"

        Try
            Me.txtTipoCambio.Text = devuelveTipoCambio(cadenaString, cbxTipoMoneda.Text)
            arrayDatos(0) = Me.cbxTipoMoneda.SelectedItem
            arrayDatos(1) = Me.txtTipoCambio.Text

            If Me.cbxTipoMoneda.SelectedIndex = 0 Then
                txtMonto.Enabled = True
                txtMontoME.Enabled = False
                lblMontoMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
                lblMontoME.Text = ""
            Else
                txtMontoME.Enabled = True
                txtMonto.Enabled = False
                lblMontoME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
                lblMontoMN.Text = ""
            End If
            lblMoneda.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda1.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda2.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda3.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda4.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxTipoPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoPago.SelectedIndexChanged
        arrayDatos(0) = Me.cbxTipoMoneda.SelectedItem
        arrayDatos(1) = Me.txtTipoCambio.Text
        arrayDatos(2) = Me.cbxTipoPago.SelectedIndex
        arrayDatos(3) = Me.cbxTipoPago.Text

        If (cbxTipoPago.SelectedIndex = 0 Or cbxTipoPago.SelectedIndex = 1 Or cbxTipoPago.SelectedIndex = 4 Or cbxTipoPago.SelectedIndex = 5) Then
            If cbxTipoPago.SelectedIndex = 1 Then
                cbxTipoMoneda.SelectedIndex = 1
            Else
                cbxTipoMoneda.SelectedIndex = 0
            End If

            frmdatosCheques.ShowDialog()
            If flag <> 1 Then
                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    Me.txtMontoME.Text = Val(arrayDatos(4))
                    If Me.cbxTipoPago.SelectedIndex = 4 Then
                        Me.txtInteresPagoCuota.Text = Format(CDec(arrayDatos(4)) * 0.055, "####0.00")
                        Me.txtTotalPagar.Text = Format(CDec(Me.txtMontoME.Text) + CDec(Me.txtInteresPagoCuota.Text), "######0.00")
                    End If
                Else
                    Me.txtMonto.Text = Val(arrayDatos(4))
                    If Me.cbxTipoPago.SelectedIndex = 4 Then
                        Me.txtInteresPagoCuota.Text = Format(CDec(arrayDatos(4)) * 0.055, "####0.00")
                        Me.txtTotalPagar.Text = Format(CDec(Me.txtMonto.Text) + CDec(Me.txtInteresPagoCuota.Text), "######0.00")
                    End If
                End If
                igualaDatos()
                z += 1
                igualaVacio()
                z = 0
            Else
                Me.cbxTipoMoneda.SelectedIndex = 0
                Me.cbxTipoPago.SelectedIndex = tPagoAnterior
            End If
        Else
            cbxConcepto.SelectedIndex = 2
            cbxTipoPago.SelectedIndex = 2
        End If
    End Sub
    Private Sub cbxConcepto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxConcepto.SelectedIndexChanged
        txtOtrosPagos.Enabled = False
        txtNumLetra.Enabled = False
        btnBuscarCliente.Enabled = True
        btnNuevoCliente.Enabled = True

        Select Case cbxConcepto.SelectedIndex
            Case 0
                'If MsgBox("Procesará recibo x adelanto de Venta Contado?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '    Me.flagAdelantoVtaCash = True
                'Else
                '    Me.flagAdelantoVtaCash = False
                'End If
            Case 1
                flagAmortiza = 0
                btnBuscarCliente.Enabled = False
                btnNuevoCliente.Enabled = False
                txtNumLetra.Enabled = True
            Case 2
                btnBuscarCliente.Enabled = False
                btnNuevoCliente.Enabled = False
                txtNumLetra.Enabled = True
            Case 3

            Case 4

            Case 5
                cbxTipoPago.SelectedIndex = 4
            Case 6
                cbxTipoPago.SelectedIndex = 4
            Case 7
                cbxTipoPago.SelectedIndex = 4
            Case 8

            Case 9

            Case 10
                Me.txtOtrosPagos.Enabled = True
        End Select
    End Sub
    Private Sub txtNumLetra_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumLetra.DoubleClick
        arrayDatos(0) = ""
        numModulo = Me.cbxConcepto.SelectedIndex
        frmbuscaLetra.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtNumLetra.Text = arrayDatos(0)
            Me.txtNumCorrelativo = arrayDatos(1)
            Me.txtCorrelativo.Text = arrayDatos(1)
            Me.vMonto = arrayDatos(2)
            Me.vMontoME = arrayDatos(3)
            Me.txtNumLetra.Focus()
            igualaVacio()
        End If
    End Sub
    Private Sub btnBuscaLetra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscaLetra.Click
        Try
            If Me.txtNumLetra.Text <> "" Then
                oDataSet = New DataSet()
                Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select * from letrasClientes where((numRecibo=' ' and status=' ') or (numRecibo<>' ' and status='A'))" & _
                                                                    "and numLetra like'" & Me.txtNumLetra.Text & "' and numCorrelativo='" & Me.txtNumCorrelativo & "'", Connection)
                daLetras.Fill(oDataSet, "letrasClientes")
                If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                    MsgBox("No existe número letra o ya fue cancelado  !  !  !", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where idCliente Like '" & Me.oDataSet.Tables(0).Rows(0).Item(1) & "' ", Connection)
                daCliente.Fill(oDataSet, "clientes")

                Dim daCostosFijos As SqlDataAdapter = New SqlDataAdapter("select * from costosFijos", Connection)
                daCostosFijos.Fill(oDataSet, "costosFijos")

                Dim daFactorInteres As SqlDataAdapter = New SqlDataAdapter("select * from factorInteres", Connection)
                daFactorInteres.Fill(oDataSet, "factorInteres")

                buscarAmortizaciones()
                '--------------- Datos del Cliente --------------------
                Me.txtCodigoCliente.Text = Me.oDataSet.Tables(1).Rows(0).Item(0)
                Me.txtNombres.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
                Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
                If Me.oDataSet.Tables(1).Rows(0).Item(3).ToString.Trim = "" Then
                    Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(4)
                Else
                    Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
                End If
                '--------------- Datos de la Letra ---------------------
                Me.vnumCorrelativo = Me.oDataSet.Tables(0).Rows(0).Item(3)
                Me.vfecVencimiento = Me.oDataSet.Tables(0).Rows(0).Item(7)
                Me.dtpFechaVcmto.Text = Me.oDataSet.Tables(0).Rows(0).Item(7)
                Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(10) - 1

                If Me.oDataSet.Tables(0).Rows(0).Item(10) > 1 Then
                    Me.vMontoOriginalME = Me.oDataSet.Tables(0).Rows(0).Item(5)
                Else
                    Me.vMontoOriginal = Me.oDataSet.Tables(0).Rows(0).Item(4)
                End If

                If paseAmortizacion <> 0 Then
                    Me.txtMonto.Text = Format(txtMontoCancelar, "######0.00")
                    Me.txtMontoME.Text = Format(txtMontoCancelarME, "######0.00")
                Else
                    If Me.oDataSet.Tables(0).Rows(0).Item(10) > 1 Then
                        Me.txtMontoME.Text = Format(Me.oDataSet.Tables(0).Rows(0).Item(5), "######0.00")
                    Else
                        Me.txtMonto.Text = Format(Me.oDataSet.Tables(0).Rows(0).Item(4), "######0.00")
                    End If
                End If

                If Me.cbxConcepto.SelectedIndex = 1 Or Me.cbxConcepto.SelectedIndex = 2 Then
                    If paseAmortizacion <> 0 Then
                        Me.dVencidos = DateDiff(DateInterval.Day, Me.vNvoVcto, CDate(Me.dtpFecha.Text))
                        Me.dtpFechaVcmto.Text = Me.vNvoVcto
                        paseAmortizacion = 0
                    Else
                        Me.dVencidos = DateDiff(DateInterval.Day, Me.oDataSet.Tables(0).Rows(0).Item(7), CDate(Me.dtpFecha.Text))
                    End If
                    If oDataSet.Tables(0).Rows(0).Item(10) > 1 Then
                        vLetra = txtMontoME.Text
                    Else
                        vLetra = txtMonto.Text
                    End If
                    If dVencidos >= 9 Then
                        If vLetra <= Me.oDataSet.Tables(2).Rows(0).Item(2) Then
                            Me.tInteres = Me.oDataSet.Tables(2).Rows(0).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(3).Rows(0).Item(1))
                        Else
                            If vLetra <= Me.oDataSet.Tables(2).Rows(1).Item(2) Then
                                Me.tInteres = Me.oDataSet.Tables(2).Rows(1).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(3).Rows(0).Item(1))
                            Else
                                Me.tInteres = Me.oDataSet.Tables(2).Rows(2).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(3).Rows(0).Item(1))
                            End If
                        End If
                        Me.txtDiasVencidos.Text = dVencidos
                        Me.txtInteres.Text = Format(tInteres, "#####0.00")
                        valorDecimal = CByte(VisualBasic.Right(Me.txtInteres.Text, 2))
                        If valorDecimal >= 1 Then
                            Me.txtInteres.Text = Format(Math.Floor(CDec(Me.txtInteres.Text)) + 1, "#####0.00")
                        End If
                    End If

                    If oDataSet.Tables(0).Rows(0).Item(10) > 1 Then
                        interesME = Me.txtInteres.Text
                        Me.txtTotalPagar.Text = Format(Me.txtMontoME.Text + interesME, "######0.00")
                    Else
                        interesMN = Me.txtInteres.Text
                        Me.txtTotalPagar.Text = Format(Me.txtMonto.Text + interesMN, "######0.00")
                    End If
                End If

                vMontoAmortizar = Me.txtMonto.Text
                vMontoAmortizarME = Me.txtMontoME.Text
                'Me.txtTipoCambio.Text = Me.oDataSet.Tables(0).Rows(0).Item(11)
                Me.tipoCambioLetra = Me.txtTipoCambio.Text

                If Me.cbxConcepto.SelectedIndex = 1 Then
                    If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                        Me.txtMontoME.Enabled = True
                    Else
                        Me.txtMonto.Enabled = True
                    End If
                End If
                pase1 = 1
                Me.oDataSet.Tables.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub buscarAmortizaciones()
        Try
            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select * from recibosClientes where numLetra='" & Me.txtNumLetra.Text & "' " & _
                                                                "and numCorrelativo='" & Me.txtNumCorrelativo & "' and status='A'", Connection)
            daLetras.Fill(oDataSet, "recibosClientes")

            If Me.oDataSet.Tables(4).Rows.Count() > 0 Then
                If Me.oDataSet.Tables(4).Rows(Me.oDataSet.Tables(4).Rows.Count() - 1).Item(10) > Me.oDataSet.Tables(4).Rows(Me.oDataSet.Tables(4).Rows.Count() - 1).Item(11) Then
                    Me.vNvoVcto = Me.oDataSet.Tables(4).Rows(Me.oDataSet.Tables(4).Rows.Count() - 1).Item(10)
                Else
                    Me.vNvoVcto = Me.oDataSet.Tables(4).Rows(Me.oDataSet.Tables(4).Rows.Count() - 1).Item(11)
                End If
                If Me.vMonto > 0 Then
                    strSuma = Me.oDataSet.Tables(4).Compute("Sum(impDocumento)", "impDocumento > 0")
                    MsgBox(Format("Monto original es: " & Me.vMonto, "Currency"))
                    MsgBox(Format("suma amortizaciones es: " & CDec(strSuma), "Currency"))
                    Me.txtMontoCancelar = Me.vMonto - CDec(strSuma)
                    MsgBox(Format("Monto por cancelar es: " & Me.txtMontoCancelar, "Currency"))
                Else
                    strSuma = Me.oDataSet.Tables(4).Compute("Sum(impDocumentoME)", "impDocumentoME > 0")
                    MsgBox(Format("Monto original es: " & Me.vMontoME, "Currency"))
                    MsgBox(Format("suma amortizaciones es: " & CDec(strSuma), "Currency"))
                    Me.txtMontoCancelarME = Me.vMontoME - CDec(strSuma)
                    MsgBox(Format("Monto por cancelar es: " & Me.txtMontoCancelarME, "Currency"))
                End If
                paseAmortizacion = 1
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtCodigoVendedor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoVendedor.DoubleClick
        arrayDatos(0) = ""
        frmbuscaVendedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoVendedor.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim arrayMoneda() As String = {"S/.", "$  ", "€  "}
        Dim arrayTipoPago() As String = {"Cheque MN", "Cheque ME", "Efect. MN", "Efect. ME", "P.Tarjeta", "Transf/Abono Cta"}
        Dim stringConcepto As String
        Dim stringPago As String

        oDataSet = New DataSet()

        Try
            Dim daCambio As SqlDataAdapter = New SqlDataAdapter("SELECT * from tiposMonedas where idMoneda=" & Me.cbxTipoMoneda.SelectedIndex + 1 & "", Connection)
            daCambio.Fill(oDataSet, "tipoCambio")

            Dim daDatosCheques As SqlDataAdapter = New SqlDataAdapter("SELECT * from datosCheques where numRecibo=" & CInt(Me.txtNumRecibo.Text) & "", Connection)
            daDatosCheques.Fill(oDataSet, "datosCheques")

            Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("SELECT * from glosasFacturas where numDocumento=" & CInt(Me.txtNumRecibo.Text) & "", Connection)
            daGlosas.Fill(oDataSet, "glosasFacturas")

            If flagGraba <> True Then
                MsgBox("Tiene que procesar la operación para poder imprimir.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.cbxConcepto.SelectedIndex = 10 Then
                stringConcepto = VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40) & Space(43 - Len(VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40))) & "Concepto : " & VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40)
            Else
                'If Me.flagAdelantoVtaCash = True Then Me.cbxConcepto.Text = "Adelanto Vta. Contado"
                stringConcepto = Me.cbxConcepto.Text & Space(43 - Len(Me.cbxConcepto.Text)) & "Concepto : " & Me.cbxConcepto.Text
            End If

            If Me.oDataSet.Tables(1).Rows.Count <= 1 Then
                stringPago = Me.cbxTipoPago.Text & Space(43 - Len(Me.cbxTipoPago.Text)) & "T. Pago  : " & Me.cbxTipoPago.Text
            Else
                stringPago = arrayTipoPago(6) & Space(43 - Len(arrayTipoPago(6))) & "T. Pago  : " & arrayTipoPago(6)
            End If

            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)

            te.Text = "      " & _
            Me.lblNombre.Text & "                          " & Me.lblNombre.Text & enter & enter & _
            Me.lblDireccion.Text & "                             " & Me.lblDireccion.Text & enter & _
            Me.lblRUC.Text & "                                      " & Me.lblRUC.Text & " " & enter & _
            Me.lblTelefono.Text & "                                  " & Me.lblTelefono.Text & " " & enter & enter & _
            "R E C I B O   D E  C A J A  N° " & Me.txtNumRecibo.Text & Space(53 - Len("R E C I B O   D E  C A J A  N° " & Me.txtNumRecibo.Text)) & " " & "R E C I B O   D E  C A J A  N° " & Me.txtNumRecibo.Text & enter & enter & _
            "Fecha    : " & Me.dtpFecha.Text & Space(53 - Len("Fecha    : " & Me.dtpFecha.Text)) & " " & "Fecha    : " & Me.dtpFecha.Text & enter & _
            "TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2) & Space(53 - Len("TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2))) & " " & "TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2) & enter & _
            "TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3) & Space(53 - Len("TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3))) & " " & "TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3) & enter & _
            "Nombres  : " & VisualBasic.Left(Me.txtNombres.Text, 35) & Space(43 - Len(VisualBasic.Left(Me.txtNombres.Text, 35))) & "Nombres  : " & VisualBasic.Left(Me.txtNombres.Text, 35) & enter & _
            "           " & VisualBasic.Mid(Me.txtNombres.Text, 36, 35) & Space(43 - Len(VisualBasic.Left(Me.txtNombres.Text, 35))) & "           " & VisualBasic.Mid(Me.txtNombres.Text, 36, 35) & enter & _
            "Documento: " & Me.txtDNI.Text & Space(43 - Len(Me.txtDNI.Text)) & "Documento: " & Me.txtDNI.Text & enter & _
            "Concepto : " & stringConcepto & enter & _
            "T. Pago  : " & stringPago & enter

            If Me.oDataSet.Tables(1).Rows.Count > 1 Then
                For i As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
                    te.Text = te.Text & "Pago " & i + 1 & " : " & arrayTipoPago(Me.oDataSet.Tables(1).Rows(i).Item(2)) & " " & arrayMoneda(Me.oDataSet.Tables(1).Rows(i).Item(5) - 1) & " " & Me.oDataSet.Tables(1).Rows(i).Item(6) & " " & Me.oDataSet.Tables(1).Rows(i).Item(7) & " " & Me.oDataSet.Tables(1).Rows(i).Item(8) & Space(50 - Len(CStr("Pago " & i + 1 & " : " & arrayTipoPago(Me.oDataSet.Tables(1).Rows(i).Item(2)) & arrayMoneda(Me.oDataSet.Tables(1).Rows(i).Item(5) - 1) & Me.oDataSet.Tables(1).Rows(i).Item(6) & Me.oDataSet.Tables(1).Rows(i).Item(7) & Me.oDataSet.Tables(1).Rows(i).Item(8)))) & _
                                        "Pago " & i + 1 & " : " & arrayTipoPago(Me.oDataSet.Tables(1).Rows(i).Item(2)) & " " & arrayMoneda(Me.oDataSet.Tables(1).Rows(i).Item(5) - 1) & " " & Me.oDataSet.Tables(1).Rows(i).Item(6) & " " & Me.oDataSet.Tables(1).Rows(i).Item(7) & " " & Me.oDataSet.Tables(1).Rows(i).Item(8) & enter
                Next
            End If

            If Me.cbxConcepto.SelectedIndex = 1 Or Me.cbxConcepto.SelectedIndex = 2 Then
                te.Text = te.Text & "--------------------------------------------------" & "    " & "--------------------------------------------------" & enter
                te.Text = te.Text & "Letra      Num. F. Vcto. I.Original I.Pagar  Saldo" & "    " & "Letra      Num. F. Vcto. I.Original I.Pagar  Saldo" & enter
                te.Text = te.Text & "--------------------------------------------------" & "    " & "--------------------------------------------------" & enter & enter
                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.vnumCorrelativo & " " & Me.vfecVencimiento & " " & Me.vMontoOriginalME & Space(9 - Len(Str(Me.vMontoOriginalME))) & " " & Me.txtMontoME.Text & Space(8 - Len(Me.txtMontoME.Text)) & " " & Me.vMontoOriginalME - (Me.txtMontoME.Text + CDec(strSuma)) & Space(8 - Len(Str(Me.vMontoOriginalME - (Me.txtMontoME.Text + Val(strSuma))))) & "   " & _
                                        Me.txtNumLetra.Text & " " & Me.vnumCorrelativo & "  " & Me.vfecVencimiento & "  " & Me.vMontoOriginalME & "    " & Me.txtMontoME.Text & "   " & Me.vMontoOriginalME - (Me.txtMontoME.Text + CDec(strSuma)) & enter & enter
                Else
                    te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.vnumCorrelativo & " " & Me.vfecVencimiento & " " & Me.vMontoOriginal & Space(9 - Len(Str(Me.vMontoOriginal))) & " " & Me.txtMonto.Text & Space(8 - Len(Me.txtMonto.Text)) & " " & Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma)) & Space(8 - Len(Str(Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma))))) & "   " & _
                                        Me.txtNumLetra.Text & "     " & Me.vnumCorrelativo & "  " & Me.vfecVencimiento & " " & Me.vMontoOriginal & "   " & Me.txtMonto.Text & "   " & Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma)) & enter & enter
                End If

                te.Text = te.Text & "--------------------------------------------------" & "    " & "--------------------------------------------------" & enter
            Else
                te.Text = te.Text & "--------------------------------------------------" & "    " & "--------------------------------------------------" & enter & enter

                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    te.Text = te.Text & "Total a Pagar............." & arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & " " & Format(Decimal.Parse(Me.txtMontoME.Text), "##,##0.00") & Space(54 - (Len(arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & Str(Format(Decimal.Parse(Me.txtMontoME.Text), "##,##0.00"))) + Len("Total a Pagar............."))) & " " & _
                                        "Total a Pagar............." & arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & " " & Format(Decimal.Parse(Me.txtMontoME.Text), "##,##0.00") & enter & enter
                Else
                    te.Text = te.Text & "Total a Pagar............." & arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & " " & Format(Decimal.Parse(Me.txtMonto.Text), "##,##0.00") & Space(54 - (Len(arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & Str(Format(Decimal.Parse(Me.txtMonto.Text), "##,##0.00"))) + Len("Total a Pagar............."))) & " " & _
                                        "Total a Pagar............." & arrayMoneda(Me.cbxTipoMoneda.SelectedIndex) & " " & Format(Decimal.Parse(Me.txtMonto.Text), "##,##0.00") & enter & enter
                End If

                te.Text = te.Text & "--------------------------------------------------" & "    " & "--------------------------------------------------" & enter
            End If

            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                te.Text = te.Text & "son: " & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00")) - 3)) & " " & Space(48 - Len(numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00")) - 3)))) & _
                "son: " & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMontoME.Text), "#,###,##0.00")) - 3)) & enter & _
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMontoME.Text), "###,###0.00")) & " /100 " & Me.cbxTipoMoneda.Text & "                                     " & _
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMontoME.Text), "###,###0.00")) & " /100 " & Me.cbxTipoMoneda.Text & enter
            Else
                te.Text = te.Text & "son: " & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00")) - 3)) & " " & Space(48 - Len(numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00")) - 3)))) & _
                "son: " & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtMonto.Text), "#,###,##0.00")) - 3)) & enter & _
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMonto.Text), "###,###0.00")) & "/100 SOLES" & "                                        " & _
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMonto.Text), "###,###0.00")) & "/100 SOLES" & enter
            End If

            If Me.cbxConcepto.SelectedIndex = 3 Or Me.cbxConcepto.SelectedIndex = 4 Then
                te.Text = te.Text & enter & "Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento & Space(53 - Len("Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento)) & "   " & _
                                            "Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento & enter
            End If

            te.Text = te.Text & enter & "CARECE DE VALOR SIN EL SELLO DE CAJA." & "                 " & "CARECE DE VALOR SIN EL SELLO DE CAJA."
            If Me.cbxConcepto.SelectedIndex = 0 Or Me.cbxConcepto.SelectedIndex = 3 Then
                te.Text = te.Text & enter & "El precio del artículo es el que rige al momento " & "     " & "El precio del artículo es el que rige al momento"
                te.Text = te.Text & enter & "de entrega del mismo. Las devoluciones por resci-" & "     " & "de entrega del mismo. Las devoluciones por resci-"
                te.Text = te.Text & enter & "sión de contrato, se hará efectivo después de 48 " & "     " & "sión de contrato, se hará efectivo después de 48"
                te.Text = te.Text & enter & "horas, previo trámite reglamentario.             " & "     " & "horas, previo trámite reglamentario."
            End If

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
                configurarImpresion()
                PrintDocument1.Print()
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
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior, AreaImpresion_Ancho, AreaImpresion_Alto)
            Dim NroLineasImpresion As Integer = CInt(AreaImpresion_Alto / Fuente.Height)
            Dim NroLineasRelleno, NroLetrasLinea As Integer
            Static CaracterActual As Integer

            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente, _
            New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, NroLineasRelleno)
            e.Graphics.DrawString(Mid(TextoImpresion, CaracterActual + 1), Fuente, Brushes.Black, Rectangulo, Formato)
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
        VistaPrevia("Courier New", 9, te.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        'tamaño original
        'Dim Ancho As Short = 453
        'Dim Alto As Short = 551

        Dim Ancho As Short = 890
        Dim Alto As Short = 551

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
    Private Sub generarDocumentoPlano()
        Dim swEscritor As StreamWriter
        Dim sqlString As String
        Dim listaSqlString As New ArrayList
        Dim tipDocumento, numDocumento As String
        Dim idConcepto As Char
        Dim numTipoDocumento As Byte
        Dim pathData, pathRepo As String

        If Me.cbxTipoDocumento.SelectedIndex = 0 Then
            tipDocumento = "03"
            numTipoDocumento = 1
        Else
            tipDocumento = "01"
            numTipoDocumento = 6
        End If
        numDocumento = Me.txtDNI.Text

        If generaDocumentoTicket = True Then
            pathData = "\data\"
            pathRepo = "\repo\"
        Else
            pathData = "\SFS_v1.4_A4\sunat_archivos\sfs\data\"
            pathRepo = "\SFS_v1.4_A4\sunat_archivos\sfs\repo\"
        End If

        If cbxConcepto.SelectedIndex = 3 Then
            idConcepto = "8"
        Else
            idConcepto = "9"
        End If

        Try
            nomArchivo = "\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".PDF"
            swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".CAB", True)
            swEscritor.Write("0101|" & CDate(dtpFecha.Text).ToString("yyyy-MM-dd") & "|" & VisualBasic.Mid(Date.Now, 12, 8) & "|-|0000|" & numTipoDocumento & "|" & numDocumento & "|" & Me.txtNombres.Text & "|PEN|" & "0.0" & "|" & Format(CDec(txtMonto.Text), "#####0.00") & "|" & Format(CDec(Me.txtMonto.Text), "#####0.00") & "|0.00|0.00|0.00|" & Format(CDec(Me.txtMonto.Text), "#####0.00") & "|2.1|2.0|")
            swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".DET", True)
                swEscritor.WriteLine("NIU|1|" & idConcepto & "|-|" & cbxConcepto.Text & "|" & Format(CDec(txtMonto.Text), "#####0.00") & "|0.00|9997|0.00|" & Format(CDec(txtMonto.Text), "#####0.00") & "|EXO|VAT|20|18.00|-|0.00|0.00||||0.00|-|0.00|0.00|||0.00|-|0.00|0|||0.00|" & Format(CDec(txtMonto.Text), "#####0.00") & "|" & Format(CDec(txtMonto.Text), "#####0.00") & "|0.00|")
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".LEY", True)
                swEscritor.Write("1000|" & numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtMonto.Text), "#####0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#####0.00")) - 3)) & " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#####0.00")) & "/100 Soles|")
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".TRI", True)
                swEscritor.Write("9997" & "|EXO|VAT|" & Format(CDec(Me.txtMonto.Text), "#####0.00") & "|0.00|")
                'If CDec(Me.txtICBPeru.Text) > 0 Then swEscritor.Write("7152" & "|ICBPER|OTH|" & Format(CDec(Me.txtICBPeru.Text), "#####0.00") & "|" & Format(CDec(Me.txtICBPeru.Text), "#####0.00") & "|")
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".ACA", True)
                swEscritor.Write("| | | | |PE|160101|" & Me.txtDireccion.Text & "|-| | |")
                swEscritor.Close()

                If cbxTipoDocumento.SelectedIndex = 1 Then
                    swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipDocumento & "-" & Me.txtSerie.Text & "-" & Me.txtNumDocumento & ".PAG", True)

                    swEscritor.Write("Contado" & "|-|-|")
                    swEscritor.Close()
                End If

            sqlString = "update vtaCabecera set staEnvio='@' where tipDocumento='" & cbxTipoDocumento.Text & "' and numDocumento=" & Me.txtNumDocumento & ""
            listaSqlString.Add(sqlString)

            If transaccionLetras(listaSqlString) = True Then
                tipoDocumento = cbxTipoDocumento.Text
                numeDocumento = Me.txtNumDocumento
                frmMsjNumeroDocumento.ShowDialog()
                iniciarTimer()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Try
            Dim statusAnulado As String = "X"
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim ListSqlStrings As New ArrayList

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                SqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                            Me.txtNumRecibo.Text & ",' ',' ',0,0,0,' ',' ',1,1,'" & Me.dtpFecha.Text & "',' ',' ',0,1,0,'" & statusAnulado & "')"

                SqlString1 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumRecibo.Text & " where tipDocumento='" & txtTipoDocumento & "'"

                ListSqlStrings.Add(SqlString)
                ListSqlStrings.Add(SqlString1)

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
    Private Sub igualaDatos()
        arrayDatosCheques(z, 0) = arrayDatos(0)
        arrayDatosCheques(z, 1) = arrayDatos(1)
        arrayDatosCheques(z, 2) = arrayDatos(2)
        arrayDatosCheques(z, 3) = arrayDatos(3)
        arrayDatosCheques(z, 4) = arrayDatos(4)
        arrayDatosCheques(z, 5) = arrayDatos(5)
        arrayDatosCheques(z, 6) = arrayDatos(6)
    End Sub
    Private Sub igualaVacio()
        arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = ""
        arrayDatos(3) = "" : arrayDatos(4) = "" : arrayDatos(5) = ""
        arrayDatos(6) = "" : arrayDatos(7) = "" : arrayDatos(8) = ""
        arrayDatos(9) = "" : arrayDatos(10) = ""
    End Sub
    Private Sub igualaVacioArrayCheques()
        For x As Integer = arrayDatosCheques.GetLowerBound(0) To arrayDatosCheques.GetUpperBound(0)
            arrayDatosCheques(x, 0) = "" : arrayDatosCheques(x, 1) = ""
            arrayDatosCheques(x, 2) = "" : arrayDatosCheques(x, 3) = ""
            arrayDatosCheques(x, 4) = "" : arrayDatosCheques(x, 5) = ""
            arrayDatosCheques(x, 6) = ""
        Next
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
    Private Sub txtDNI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDNI.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        txtNumLetra_DoubleClick(sender, e)
    End Sub
    Private Sub txtTotalPagar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalPagar.DoubleClick
        If Me.cbxTipoMoneda.SelectedIndex > 0 Then
            Me.txtTotalPagar.Text = Format(CDec(Me.txtMontoME.Text) + CDec(Me.txtInteres.Text), "######0.00")
        Else
            Me.txtTotalPagar.Text = Format(CDec(Me.txtMonto.Text) + CDec(Me.txtInteres.Text), "######0.00")
        End If
        Me.txtTotalPagar.Enabled = False
    End Sub
    Private Sub txtMonto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMonto.TextChanged
        If Me.cbxConcepto.SelectedIndex = 1 Then Me.txtTotalPagar.Enabled = True
    End Sub
    Private Sub txtMontoME_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMontoME.TextChanged
        If Me.cbxConcepto.SelectedIndex = 1 Then Me.txtTotalPagar.Enabled = True
    End Sub
    Private Sub txtMontoME_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMontoME.MouseEnter
        Me.lblMsj.Text = "Haz doble clic en la cajita 'T.Pagar' para actualizar total."
    End Sub
    Private Sub txtMontoME_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMontoME.MouseLeave
        Me.lblMsj.Text = ""
    End Sub
    Private Sub txtMonto_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonto.MouseEnter
        Me.lblMsj.Text = "Haz doble clic en la cajita 'T.Pagar' para actualizar total."
    End Sub
    Private Sub txtMonto_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonto.MouseLeave
        Me.lblMsj.Text = ""
    End Sub
    Private Sub dtmFecha_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFecha.CloseUp
        Dim oFrmAcceso As New frmaccesoAdministrador()
        oFrmAcceso.ShowDialog()

        If flag <> 1 Then
            Me.dtpFecha.Value = Today()
            Exit Sub
        End If
        flag = 0
    End Sub
    Private Sub dtmFecha_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyUp
        Me.dtpFecha.Value = Today()
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.cbxTipoDocumento.SelectedIndex = 0
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='RC'")
        Dim strUltimoNumeroDoc As String = ("SELECT * FROM ultimosNumeros where tipMovimiento='" & Me.cbxTipoDocumento.Text & "'")

        Me.vnumCorrelativo = 0
        Me.txtNumLetra.Text = ""
        Me.txtMonto.Text = 0 : Me.txtMontoME.Text = 0
        Me.dVencidos = 0 : Me.tInteres = 0 : Me.vLetra = 0
        Me.interesMN = 0 : Me.interesME = 0
        Me.txtDiasVencidos.Text = 0 : Me.txtInteres.Text = 0
        Me.dtpFechaVcmto.Text = Date.Today
        Me.txtTotalPagar.Text = 0
        Me.txtTotalPagar.Enabled = False
        Me.txtPMultiple.Text = 0
        Me.txtDiferencia.Text = 0
        Me.txtInteresPagoCuota.Text = 0
        Me.cOPeracionMN = 0
        Me.cOPeracionME = 0
        Me.txtOtrosPagos.Text = ""
        Me.txtNumDocumento = devuelveUltimoNumero(strUltimoNumeroDoc) + 1
        Me.txtNumRecibo.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.pase1 = 0
        Me.strSuma = 0
        z = 0
        Me.txtMontoCancelar = 0
        Me.txtMontoCancelarME = 0
        Me.vMontoOriginal = 0
        Me.vMontoOriginalME = 0
     
        Me.txtOtrosPagos.Enabled = False
        Me.flagGraba = False

        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoPago.SelectedIndex = 2
        Me.cbxConcepto.SelectedIndex = 2

        Me.tPagoAnterior = Me.cbxTipoPago.SelectedIndex
        Me.txtCorrelativo.Text = ""
        Me.lblMontoME.Text = ""
        Me.lblMontoMN.Text = ""
        lblMontoMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        status = " "
        igualaVacioArrayCheques()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        status = " "
        Me.Close()
    End Sub

    Private Sub btnGrabar_ClientSizeChanged(sender As Object, e As EventArgs) Handles btnGrabar.ClientSizeChanged

    End Sub
End Class