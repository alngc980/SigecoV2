Imports Microsoft
Imports Libreria
Imports System.data.SqlClient
Public Class frmreciboSalida
    Private oDataSet As DataSet
    Dim sqlStringCliente As String = "SELECT *FROM clientes"
    Dim txtTipoDocumento As String = "RP"
    Dim numLetra As String
    Dim txtStringLetra As String
    Dim montoPrestamo, txtImpLetra As Decimal
    Dim valorDecimal As Byte
    Dim vSemana, vQuincena As Integer
    Dim fecVcmto As DateTime
    Dim te As New RichTextBox
    Private Sub btnBuscarVendedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarVendedor.Click
        arrayDatos(0) = ""
        If Me.cbxConcepto.SelectedIndex = 0 Then
            frmbuscaPersonal.ShowDialog()
            If arrayDatos(0) <> "" Then
                Me.txtCodigoCliente.Text = arrayDatos(0)
                Me.txtNombres.Text = arrayDatos(1) & " " & arrayDatos(2) & " " & arrayDatos(3)
                Me.txtDireccion.Text = arrayDatos(4)
                Me.txtDNI.Text = arrayDatos(5)
                igualaVacio()
            End If
        Else
            frmbuscaCliente.ShowDialog()
            If arrayDatos(0) <> "" Then
                Me.txtCodigoCliente.Text = arrayDatos(0)
                Me.txtNombres.Text = arrayDatos(1)
                Me.txtDireccion.Text = arrayDatos(2)
                Me.txtDNI.Text = arrayDatos(3)
                igualaVacio()
            End If
        End If
    End Sub
    Private Sub btnNuevoVendedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoVendedor.Click
        arrayDatos(0) = ""
        If Me.cbxConcepto.SelectedIndex = 0 Then
            frmnuevoPersonal.ShowDialog()
            If arrayDatos(0) <> "" Then
                Me.txtCodigoCliente.Text = arrayDatos(0)
                Me.txtNombres.Text = arrayDatos(1) & " " & arrayDatos(2) & " " & arrayDatos(3)
                Me.txtDireccion.Text = arrayDatos(4)
                Me.txtDNI.Text = arrayDatos(5)
                igualaVacio()
            End If
        Else
            frmNuevoCliente.ShowDialog()
            If arrayDatos(0) <> "" Then
                Me.txtCodigoCliente.Text = arrayDatos(0)
                Me.txtNombres.Text = arrayDatos(1)
                Me.txtDireccion.Text = arrayDatos(2)
                Me.txtDNI.Text = arrayDatos(3)
                igualaVacio()
            End If
        End If
    End Sub
    Private Sub frmreciboSalida_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='RP'")

        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.txtNumRecibo.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtMonto.Text = 0 : Me.txtMontoME.Text = 0
        Me.txtDiasVencidos.Text = 0 : Me.txtInteres.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtPMultiple.Text = 0
        Me.txtDiferencia.Text = 0

        Me.cbxTipoCredito.SelectedIndex = 0
        Me.txtCanCuotas.Text = 1
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoPago.SelectedIndex = 0
        Me.cbxConcepto.SelectedIndex = 0

        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True
    End Sub
    Private Sub frmreciboPago_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            'Me.btnProducto_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                btnGrabar_Click(sender, e)
            Else
                If e.KeyCode = Keys.F8 Then
                    'btnAnular_Click(sender, e)
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
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim sqlStringNumero As String = "select * from recibosSalidas where idRecibo='" & CInt(Me.txtNumRecibo.Text) & "'"
        txtStringLetra = ""
        Dim oProducto As Producto = New Producto()
        Dim sqlString As String = ""
        Dim listaSqlStrings As New ArrayList
       

        Try
            If Me.txtCodigoCliente.Text <> "" And (Me.txtMonto.Text > 0 Or Me.txtMontoME.Text > 0) Then
                If verificaNumeroDocumento(sqlStringNumero) = True Then
                    MsgBox("Por favor, documento ya fue grabado o ya existe número.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If Me.txtCodigoCliente.Text = "" Or (Me.txtMonto.Text <= 0 And Me.txtMontoME.Text <= 0) Then
                MsgBox("Faltan datos para la operación de 'Préstamo a Personal' o 'Préstamo a Clientes'.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.txtOtrosPagos.Text = "" Then
                MsgBox("Por favor, tiene que indicar la glosa del documento  !  !  !", MsgBoxStyle.Information)
                Exit Sub
            End If

            'Si concepto es "Préstamo a Personal" gestionarlo como cliente.
            If cbxConcepto.SelectedIndex = 0 Then
                If Me.buscarDocumento(Trim(Me.txtDNI.Text)) >= 1 Then
                    Me.txtCodigoCliente.Text = asignarCodigo(Trim(Me.txtDNI.Text))
                Else
                    sqlString = "insert into clientes (nombres,direccion,dni,telCelular,telFijo,dirTrabajo,nomPareja,dirPareja,dniPareja," & _
                                 "celPareja,dirTraPareja,fecAlta) values ('" & Me.txtNombres.Text & "','" & Me.txtDireccion.Text & "','" & _
                                 Me.txtDNI.Text & "',' ',' ',' ',' ',' ',' ',' ','','" & Me.dtmFecha.Text & "' )"
                    Me.txtCodigoCliente.Text = devuelveCodigo(sqlStringCliente) + 1

                    If grabarSqlString(sqlString) Then
                        'MsgBox("Cadena sqlString sobre Cliente procesada correctamente.", MsgBoxStyle.Information)
                    Else
                        MsgBox("El proceso de gestión del personal como cliente ha fallado.", MsgBoxStyle.Critical)
                    End If
                End If
            End If
            'Fin de gestión de trabajadores como clientes
            If cbxConcepto.SelectedIndex <= 1 Then Me.txtStringLetra = oProducto.stringLetra(Me.txtTipoDocumento, Me.txtNumRecibo.Text, Me.dtmFecha.Text, Me.txtCanCuotas.Text)

            sqlString = "insert into recibosSalidas (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                        "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                         Me.txtNumRecibo.Text & "," & Me.cbxConcepto.SelectedIndex & ",'" & Me.txtStringLetra & "'," & Me.txtMonto.Text & "," & Me.txtMontoME.Text & _
                         ",0,' ',' '," & Me.txtCodigoCliente.Text & ",1,'" & Me.dtmFecha.Text & "',' ',' ',0," & _
                         Me.cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtTipoCambio.Text & ",' ')"
            listaSqlStrings.Add(sqlString)

            sqlString = "insert into datosCheques (tipDoc,numRecibo,tipoPago,nomBanco,numCheque,idMoneda,monCheque,tipCambio,monCamCheque) VALUES ('" & _
                                  Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & cbxTipoPago.SelectedIndex & "','Pago Efectivo','" & cbxTipoPago.Text & "'," & cbxTipoMoneda.SelectedIndex + 1 & "," & Me.txtMonto.Text & ",1.0," & Me.txtMontoME.Text & ")"
            listaSqlStrings.Add(sqlString)

            sqlString = "insert into glosasFacturas (tipDocumento,numDocumento,glosa) VALUES ('" & _
                        Me.txtTipoDocumento & "'," & Me.txtNumRecibo.Text & ",'" & txtOtrosPagos.Text & "')"
            listaSqlStrings.Add(sqlString)

            sqlString = "update ultimosNumeros Set numero=" & CInt(Me.txtNumRecibo.Text) & " where tipDocumento= '" & txtTipoDocumento & "'"
            listaSqlStrings.Add(sqlString)

            'Procesamiento de las cuotas del crédito del cliente
            If cbxConcepto.SelectedIndex <= 1 Then
                If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                    montoPrestamo = txtMontoME.Text
                Else
                    montoPrestamo = txtMonto.Text
                End If

                Me.txtImpLetra = Format(montoPrestamo / CInt(Me.txtCanCuotas.Text), "#####0.00")
                valorDecimal = CByte(VisualBasic.Right(Me.txtImpLetra, 2))

                If valorDecimal >= 1 Then
                    Me.txtImpLetra = Math.Floor(Me.txtImpLetra) + 1
                End If

                For i As Integer = 0 To CInt(Me.txtCanCuotas.Text) - 1
                    If Me.cbxTipoCredito.SelectedIndex = 0 Then
                        fecVcmto = Me.dtpFechaVcmto.Value.AddMonths(i).ToShortDateString
                    Else
                        If Me.cbxTipoCredito.SelectedIndex = 1 Then
                            fecVcmto = Me.dtpFechaVcmto.Value.AddDays(vQuincena).ToShortDateString
                            vQuincena += 15
                        Else
                            fecVcmto = Me.dtpFechaVcmto.Value.AddDays(vSemana).ToShortDateString
                            vSemana += 7
                        End If
                    End If

                    sqlString = "insert into letrasClientes (numLetra,idCliente,idVendedor,numCorrelativo,impLetra,impLetraME," & _
                                 "fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,tipCambio,statusNC,statusNA,zona,status) VALUES ('" & _
                                 Me.txtStringLetra & "'," & Me.txtCodigoCliente.Text & ",1," & i + 1 & "," & txtImpLetra & ",0,'" & _
                                 Me.dtmFecha.Text & "','" & fecVcmto & "','',''," & Me.cbxTipoMoneda.SelectedIndex + 1 & " ," & _
                                 Me.txtTipoCambio.Text & ",'','',1,'')"
                    listaSqlStrings.Add(sqlString)
                Next
            End If

            If ejecutarTransaccion(listaSqlStrings) Then
                MsgBox("Recibo salida procesada correctamente  !  !  !", MsgBoxStyle.Information)
            Else
                MsgBox("El Proceso ha fallado  !  !  !", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Try
            Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
            Me.txtTipoCambio.Text = devuelveTipoCambio(cadenaString, cbxTipoMoneda.Text)

            lblMoneda.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda1.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda2.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMoneda3.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"

            txtMonto.Text = 0
            txtMontoME.Text = 0

            If Me.cbxTipoMoneda.SelectedIndex = 0 Then
                lblMontoMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
                lblMontoME.Text = ""
                txtMonto.Enabled = True
                txtMontoME.Enabled = False
            Else
                lblMontoME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
                lblMontoMN.Text = ""
                txtMonto.Enabled = False
                txtMontoME.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxConcepto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxConcepto.SelectedIndexChanged

        'If Me.cbxConcepto.SelectedIndex = 0 Or Me.cbxConcepto.SelectedIndex = 1 Then
        '    Me.txtMontoME.Enabled = False
        '    Me.txtMonto.Enabled = False
        '    Me.btnBuscarVendedor.Enabled = False
        '    Me.btnNuevoVendedor.Enabled = False
        '    Me.btnProducto.Enabled = True
        '    Me.txtOtrosPagos.Enabled = False
        'Else
        '    Me.btnBuscarVendedor.Enabled = True
        '    Me.btnNuevoVendedor.Enabled = True
        '    Me.btnProducto.Enabled = False
        '    Me.txtOtrosPagos.Enabled = False
        '    Me.txtMontoME.Text = 0
        '    Me.txtMonto.Text = 0
        'End If

    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim oProducto As Producto = New Producto()
        Dim lCadenaNumLetra As Byte

        Me.numLetra = txtTipoDocumento.Trim + Me.txtNumRecibo.Text.Trim
        lCadenaNumLetra = Me.numLetra.Length

        Try
            If Me.txtNumRecibo.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()

            Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("select * from recibosSalidas where idRecibo='" & Me.txtNumRecibo.Text & _
            "' and status<>'A'", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(8) & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daDatosCheques As SqlDataAdapter = New SqlDataAdapter("select * from datosCheques where numRecibo='" & Me.txtNumRecibo.Text & "' ", Connection)
            daDatosCheques.Fill(oDataSet, "datosCheques")

            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select * from letrasClientes where left(numLetra," & lCadenaNumLetra & ")='" & numLetra & "' and idCliente=" & Me.oDataSet.Tables(0).Rows(0).Item(8) & "", Connection)
            daLetras.Fill(oDataSet, "letras")
            Connection.Close()

            '----------------- Datos del Cliente --------------------------
            Me.txtCodigoCliente.Text = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombres.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            If Me.oDataSet.Tables(1).Rows(0).Item(3).ToString.Trim = "" Then
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(4)
            Else
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            End If

            '----------------- Datos del Recibo --------------------------
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(14) - 1
            Me.cbxTipoDocumento.Text = VisualBasic.Left(Me.oDataSet.Tables(0).Rows(0).Item(6), 2)
            'Me.cbxTipoPago.SelectedIndex = Me.oDataSet.Tables(2).Rows(0).Item(2)  'Desactivamos esta línea x una situación excepciónal.
            Me.cbxConcepto.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(1)
            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(10)
            Me.dtpFechaVcmto.Text = Me.oDataSet.Tables(0).Rows(0).Item(11)
            If Me.dtmFecha.Text <> Date.Today Then
                Me.btnAnular.Enabled = False
            Else
                Me.btnAnular.Enabled = True
            End If
            Me.txtTipoCambio.Text = Me.oDataSet.Tables(0).Rows(0).Item(15)
            Me.txtMonto.Text = Me.oDataSet.Tables(0).Rows(0).Item(3)
            Me.txtMontoME.Text = Me.oDataSet.Tables(0).Rows(0).Item(4)
            Me.numLetra = Me.oDataSet.Tables(3).Rows(0).Item(0)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub btnBuscaRecibo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscaRecibo.Click
        Me.btnBuscar.Enabled = False
        Me.btnGrabar.Enabled = False
        btnBuscar_Click(sender, e)
    End Sub
    Private Function buscarDocumento(ByVal Documento As String) As Byte
        Dim daClientes As SqlDataAdapter = New SqlDataAdapter("select * from clientes where dni Like '" & Documento & "'", Connection)
        oDataSet = New DataSet()

        Try
            daClientes.Fill(oDataSet, "clientes")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try

        Return Me.oDataSet.Tables(0).Rows.Count()
    End Function
    Private Function asignarCodigo(ByVal Documento As String) As Integer
        Dim daClientes As SqlDataAdapter = New SqlDataAdapter("select * from clientes where dni Like '" & Documento & "'", Connection)
        oDataSet = New DataSet()

        Try
            daClientes.Fill(oDataSet, "clientes")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try

        Return Me.oDataSet.Tables(0).Rows(0).Item(0)
    End Function
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Try
            Dim statusAnulado As String = "A"
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'SqlString = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
                '            "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
                '            Me.txtNumRecibo.Text & ",' ',' ',0,0,0,' ',' ',1,1,'" & Me.dtmFecha.Text & "',' ',' ',0,1,0,'" & statusAnulado & "')"
                'ListSqlStrings.Add(SqlString)

                SqlString = "DELETE from recibosSalidas where idRecibo=" & CInt(Me.txtNumRecibo.Text) & ""
                ListSqlStrings.Add(SqlString)

                SqlString = "UPDATE ultimosNumeros Set numero=" & Me.txtNumRecibo.Text & " where tipDocumento='" & txtTipoDocumento & "'"
                ListSqlStrings.Add(SqlString)

                'SqlString1 = "UPDATE letrasClientes set status='A' where numLetra='" & numLetra & "'"
                SqlString = "DELETE from letrasClientes where numLetra='" & numLetra & "'"
                ListSqlStrings.Add(SqlString)

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
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim arrayMoneda() As String = {"S/.", "$  ", "€  "}
        Dim arrayTipoPago() As String = {"Cheque MN", "Cheque ME", "Efect. MN", "Efect. ME", "P.Tarjeta", "Efectivo ", "Pago Multiple"}
        Dim stringConcepto As String
        Dim stringPago As String
        oDataSet = New DataSet()

        Connection.Open()
        Dim daCambio As SqlDataAdapter = New SqlDataAdapter("SELECT  *from tiposMonedas where idMoneda=" & Me.cbxTipoMoneda.SelectedIndex + 1 & " ", Connection)
        daCambio.Fill(oDataSet, "tipoCambio")
        Dim daDatosCheques As SqlDataAdapter = New SqlDataAdapter("SELECT  *from datosCheques where numRecibo=" & CInt(Me.txtNumRecibo.Text) & " ", Connection)
        daDatosCheques.Fill(oDataSet, "datosCheques")
        Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("SELECT  *from glosasFacturas where numDocumento=" & CInt(Me.txtNumRecibo.Text) & " ", Connection)
        daGlosas.Fill(oDataSet, "glosasFacturas")
        Connection.Close()

        If Me.oDataSet.Tables(2).Rows.Count <= 0 And Me.cbxConcepto.SelectedIndex = 5 Then
            MsgBox("Tiene que grabar la operación para poder imprimir.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            If Me.cbxConcepto.SelectedIndex = 5 Then
                stringConcepto = VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40) & Space(43 - Len(VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40))) & "Concepto : " & VisualBasic.Left(Me.oDataSet.Tables(2).Rows(0).Item(2).ToString, 40)
            Else
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
            "Fecha    : " & Me.dtmFecha.Text & Space(53 - Len("Fecha    : " & Me.dtmFecha.Text)) & " " & "Fecha    : " & Me.dtmFecha.Text & enter & _
            "TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2) & Space(53 - Len("TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2))) & " " & "TCC      : " & Me.oDataSet.Tables(0).Rows(0).Item(2) & enter & _
            "TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3) & Space(53 - Len("TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3))) & " " & "TCV      : " & Me.oDataSet.Tables(0).Rows(0).Item(3) & enter & _
            "Nombres  : " & VisualBasic.Left(Me.txtNombres.Text, 35) & Space(43 - Len(VisualBasic.Left(Me.txtNombres.Text, 35))) & "Nombres  : " & VisualBasic.Left(Me.txtNombres.Text, 35) & enter & _
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
                'If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                '    te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.vnumCorrelativo & " " & Me.vfecVencimiento & " " & Me.vMontoOriginalME & Space(9 - Len(Str(Me.vMontoOriginalME))) & " " & Me.txtMontoME.Text & Space(8 - Len(Me.txtMontoME.Text)) & " " & Me.vMontoOriginalME - (Me.txtMontoME.Text + CDec(strSuma)) & Space(8 - Len(Str(Me.vMontoOriginalME - (Me.txtMontoME.Text + Val(strSuma))))) & "   " & _
                '                        Me.txtNumLetra.Text & " " & Me.vnumCorrelativo & "  " & Me.vfecVencimiento & "  " & Me.vMontoOriginalME & "    " & Me.txtMontoME.Text & "   " & Me.vMontoOriginalME - (Me.txtMontoME.Text + CDec(strSuma)) & enter & enter
                'Else
                '    te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.vnumCorrelativo & " " & Me.vfecVencimiento & " " & Me.vMontoOriginal & Space(9 - Len(Str(Me.vMontoOriginal))) & " " & Me.txtMonto.Text & Space(8 - Len(Me.txtMonto.Text)) & " " & Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma)) & Space(8 - Len(Str(Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma))))) & "   " & _
                '                        Me.txtNumLetra.Text & "     " & Me.vnumCorrelativo & "  " & Me.vfecVencimiento & " " & Me.vMontoOriginal & "   " & Me.txtMonto.Text & "   " & Me.vMontoOriginal - (Me.txtMonto.Text + CDec(strSuma)) & enter & enter
                'End If

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
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMonto.Text), "###,###0.00")) & "/100 NUEVOS SOLES" & "                                 " & _
                "Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtMonto.Text), "###,###0.00")) & "/100 NUEVOS SOLES" & enter
            End If

            'If Me.cbxConcepto.SelectedIndex = 3 Or Me.cbxConcepto.SelectedIndex = 4 Then
            '    te.Text = te.Text & enter & "Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento & Space(53 - Len("Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento)) & "   " & _
            '                                "Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocumento.Text & " " & Me.txtNumDocumento & enter
            'End If

            te.Text = te.Text & enter & "CARECE DE VALOR SIN EL SELLO DE CAJA." & "                 " & "CARECE DE VALOR SIN EL SELLO DE CAJA." & enter
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
    Private Sub vistaPrevia(ByVal TipoFuente As String, ByVal TamañoFuente As Byte, ByVal TextoImpresion As String, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
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
        vistaPrevia("Courier New", 9, te.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

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
    Private Sub igualaVacio()
        arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = ""
        arrayDatos(3) = "" : arrayDatos(4) = "" : arrayDatos(5) = ""
        arrayDatos(6) = "" : arrayDatos(7) = "" : arrayDatos(8) = ""
        arrayDatos(9) = "" : arrayDatos(10) = ""
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
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtSerie.Text = "01"
        Dim strCodigoVendedor As String = ("SELECT * FROM Vendedores where idVendedor=1")
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='RP'")

        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""

        Me.txtMonto.Text = 0 : Me.txtMontoME.Text = 0
        Me.txtDiasVencidos.Text = 0 : Me.txtInteres.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtPMultiple.Text = 0
        Me.txtDiferencia.Text = 0

        Me.txtNumRecibo.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.txtCanCuotas.Text = 1
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoPago.SelectedIndex = 0
        Me.cbxConcepto.SelectedIndex = 0
        Me.btnBuscar.Enabled = True
        Me.btnGrabar.Enabled = True
        Me.Controls.Add(te)
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class
