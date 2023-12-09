Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmanularRecibo
    Dim te As New RichTextBox
    Dim txtTipoDocumento As String = "RC"
    Dim vfecEmision, vfecVencimiento, vfecPago As Date
    Dim txtNumDocumento, txtStringNumDoc, statusNC As String
    Private oDataSet As DataSet
    Private serDocVenta, numDocGenCI, numDocGenACI, numCorrelativo As String
    Dim importeLetraMN, importeLetraME, totalAmortizacion As Decimal
    Private Sub frmanularRecibo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.txtSerie.Text = "01"
        Me.KeyPreview = True
        Me.btnGrabar.Enabled = False
        Me.btnImprimir.Enabled = False

        If flag = 1 And fecDocumento = Date.Today Then
            Me.btnBuscar.Enabled = False
            Me.btnAnular.Enabled = False
            Me.btnImprimir.Enabled = True
            Me.btnLimpiar.Enabled = False
            Me.txtNumRecibo.Text = numDocumento
            Me.btnBuscar_Click(sender, e)
            If Me.cbxConcepto.SelectedIndex = 1 Or Me.cbxConcepto.SelectedIndex = 2 Then Me.btnBuscaLetra_Click(sender, e)
        Else
            If flag = 1 And fecDocumento <> Date.Today Then
                Me.btnBuscar.Enabled = False
                Me.btnAnular.Enabled = False

                Me.btnImprimir.Enabled = True 'Se activa la impresión de documentos anteriores x una situación excepcional

                Me.btnLimpiar.Enabled = False
                Me.txtNumRecibo.Text = numDocumento
                Me.btnBuscar_Click(sender, e)
                If Me.cbxConcepto.SelectedIndex = 1 Or Me.cbxConcepto.SelectedIndex = 2 Then Me.btnBuscaLetra_Click(sender, e)
            End If
        End If
    End Sub
    Private Sub frmanularRecibo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            btnBuscar_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                'btnGrabar_Click(sender, e)
            Else
                If e.KeyCode = Keys.F8 Then
                    btnAnular_Click(sender, e)
                Else
                    If e.KeyCode = Keys.F10 Then
                        'btnImprimir_Click(sender, e)
                    Else
                        If e.KeyCode = Keys.F12 Then
                            btnLimpiar_Click(sender, e)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim oProducto As Producto = New Producto()

        Try
            If Me.txtNumRecibo.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("select  * from recibosClientes where idRecibo=" & Me.txtNumRecibo.Text & " and status<>'X'", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where idCliente=" & Me.oDataSet.Tables(0).Rows(0).Item(8) & "", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daDatosCheques As SqlDataAdapter = New SqlDataAdapter("select * from datosCheques where numRecibo=" & Me.txtNumRecibo.Text & "", Connection)
            daDatosCheques.Fill(oDataSet, "datosCheques")

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
            '-----12-08-15
            If Me.oDataSet.Tables(0).Rows(0).Item(1) = 0 And Me.oDataSet.Tables(0).Rows(0).Item(16) = "A" Then
                Me.cbxConcepto.SelectedText = "Adelanto Vta Contado"
            Else
                Me.cbxConcepto.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(1)
            End If
            Me.cbxTipoDocVenta.Text = VisualBasic.Left(Me.oDataSet.Tables(0).Rows(0).Item(6), 2)
            If cbxTipoDocVenta.SelectedIndex = 0 Then
                serDocVenta = "B001"
            Else
                serDocVenta = "F001"
            End If
            Me.txtNumDocumento = Me.oDataSet.Tables(0).Rows(0).Item(6)
            If (Me.cbxConcepto.SelectedIndex = 3 Or Me.cbxConcepto.SelectedIndex = 4 Or Me.cbxConcepto.SelectedIndex = 10) And Me.oDataSet.Tables(0).Rows(0).Item(6).ToString <> " " Then
                Me.txtNumDocVenta.Text = VisualBasic.Mid(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString, 3, Len(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString) - 2)

                Dim vtaCabecera As SqlDataAdapter = New SqlDataAdapter("select * from vtaCabecera where tipDocumento='" & Me.cbxTipoDocVenta.Text & "' and numDocumento=" & txtNumDocVenta.Text & "", Connection)
                vtaCabecera.Fill(oDataSet, "vtaCabecera")
                statusNC = Me.oDataSet.Tables(3).Rows(0).Item(18).ToString.Trim
            End If

            'Desactivamos esta línea x una situación excepciónal.
            'Me.cbxTipoPago.SelectedIndex = Me.oDataSet.Tables(2).Rows(0).Item(2)
            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(10)
            Me.dtpFechaVcmto.Text = Me.oDataSet.Tables(0).Rows(0).Item(11)
            If Me.dtmFecha.Text <> Date.Today Then
                Me.btnAnular.Enabled = True ' Hacemos true x una situación excepcional
            Else
                Me.btnAnular.Enabled = True
            End If
            Me.txtTipoCambio.Text = Me.oDataSet.Tables(0).Rows(0).Item(15)
            Me.txtNumLetra.Text = Me.oDataSet.Tables(0).Rows(0).Item(2)

            If (Me.cbxConcepto.SelectedIndex = 3 Or Me.cbxConcepto.SelectedIndex = 4 Or Me.cbxConcepto.SelectedIndex = 10) And Me.oDataSet.Tables(0).Rows(0).Item(6).ToString <> " " Then
                Me.numDocGenCI = VisualBasic.Mid(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString, 3, Len(Me.oDataSet.Tables(0).Rows(0).Item(6).ToString) - 2)
            End If

            Me.numDocGenACI = Trim(Me.oDataSet.Tables(0).Rows(0).Item(7).ToString)
            Me.numCorrelativo = Me.oDataSet.Tables(0).Rows(0).Item(5)
            Me.txtMonto.Text = Format(Me.oDataSet.Tables(0).Rows(0).Item(3), "###,##0.00")
            Me.txtMontoME.Text = Format(Me.oDataSet.Tables(0).Rows(0).Item(4), "###,##0.00")
            Me.txtTipoPago.Text = Me.oDataSet.Tables(2).Rows(0).Item(3)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
            numDocumento = 0
            flag = 0
        End Try
    End Sub
    Private Sub btnBuscaLetra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscaLetra.Click
        Try
            If Me.txtNumLetra.Text <> "" Then
                oDataSet = New DataSet()
                Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select  * from letrasClientes where numLetra='" & Me.txtNumLetra.Text & "'" & _
                                                                    " and idCliente=" & Me.txtCodigoCliente.Text & " and numCorrelativo='" & Me.numCorrelativo & "'", Connection)
                daLetras.Fill(oDataSet, "letrasClientes")

                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("select  * from recibosClientes where numLetra='" & Me.txtNumLetra.Text & "'" & _
                                                                     " and numCorrelativo='" & Me.numCorrelativo & "' and status<>'X'", Connection)
                daRecibos.Fill(oDataSet, "recibosClientes")
                Connection.Close()

                '--------------- Datos de la Letra ---------------------
                Me.vfecVencimiento = Me.oDataSet.Tables(0).Rows(0).Item(7)
                If Me.oDataSet.Tables(0).Rows(0).Item(10) > 1 Then
                    If Me.oDataSet.Tables(0).Rows(0).Item(4) > 0 Then
                        Me.importeLetraME = Format(CDec(Me.oDataSet.Tables(0).Rows(Me.oDataSet.Tables(0).Rows.Count() - 1).Item(4).ToString), "#####0.00")
                    Else
                        Me.importeLetraME = Format(CDec(Me.oDataSet.Tables(0).Rows(Me.oDataSet.Tables(0).Rows.Count() - 1).Item(5).ToString), "#####0.00")
                    End If
                Else
                    Me.importeLetraMN = Format(CDec(Me.oDataSet.Tables(0).Rows(Me.oDataSet.Tables(0).Rows.Count() - 1).Item(4).ToString), "#####0.00")
                End If

                '--------------- Datos del Recibo ---------------------
                If Me.oDataSet.Tables(1).Rows.Count() > 0 Then
                    If Me.oDataSet.Tables(1).Rows(0).Item(14) > 1 Then
                        totalAmortizacion = Format(CDec(Me.oDataSet.Tables(1).Compute("Sum(impDocumentoME)", "impDocumentoME > 0").ToString), "#####0.00")
                    Else
                        totalAmortizacion = Format(CDec(Me.oDataSet.Tables(1).Compute("Sum(impDocumento)", "impDocumento > 0").ToString), "#####0.00")
                    End If
                End If
            End If
            Me.oDataSet.Tables.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Dim statusAnulado As String = "X"
        Dim SqlString As String = ""
        Dim listaSqlStrings As New ArrayList

        Try
            If Me.cbxTipoDocVenta.SelectedIndex <> 0 Then
                tipoDocumento = "01"
            Else
                tipoDocumento = "03"
            End If

            If Me.txtNumRecibo.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.txtCodigoCliente.Text = "" Then
                MsgBox("Primero busque los datos del documento, luego anule documento  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.numDocGenACI <> "" Then
                MsgBox("No se puede anular documento, tiene relación con una facturación. Primero anule documento de venta: " & Me.numDocGenACI & "  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & "\RPTA\R" & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & Me.txtNumDocVenta.Text & ".ZIP") = True And statusNC = "" Then
                MsgBox("Por favor, documento venta asociado a este recibo fue enviado y aceptado por SUNAT, utilice NOTA CREDITO para anular documento venta y luego anule recibo  !  !  !", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                SqlString = "update recibosClientes set numLetra='',impDocumento=0,impDocumentoME=0,numCorrelativo=0,numDocGenCI=''," & _
                            "numDocGenACI='',idCliente=" & CInt(Me.txtCodigoCliente.Text) & ",idVendedor=1,descuento=0,idMoneda=1,tipCambio=0,status='X'" & _
                            "where idRecibo=" & CInt(Me.txtNumRecibo.Text) & ""
                listaSqlStrings.Add(SqlString)

                If Me.cbxConcepto.SelectedIndex = 3 Or Me.cbxConcepto.SelectedIndex = 4 Then
                    SqlString = "update vtaCabecera set numGuia='',numLetra='',totVentaMN=0,totVentaME=0,intFinanciero=0,comVendedor=0,cuoInicial=0,status='A' " & _
                                 "where tipDocumento='" & Me.cbxTipoDocVenta.Text & "' and numDocumento=" & CInt(Me.numDocGenCI) & ""
                    listaSqlStrings.Add(SqlString)

                    SqlString = "update vtaDetalle set precio=0,subTotal=0,status='A' where tipDocumento='" & Me.cbxTipoDocVenta.Text & "' and numDocumento=" & CInt(Me.numDocGenCI) & ""
                    listaSqlStrings.Add(SqlString)
                End If

                If Me.cbxConcepto.SelectedIndex = 1 Or Me.cbxConcepto.SelectedIndex = 2 Then
                    SqlString = "update letrasClientes set fecPago='01/01/1900',numRecibo='',status='' where numletra='" & Me.txtNumLetra.Text & _
                                "' and  numCorrelativo=" & CInt(Me.numCorrelativo) & ""
                    listaSqlStrings.Add(SqlString)
                End If

                SqlString = "update datosCheques set monCheque=0,monCamCheque=0 where numRecibo=" & CInt(Me.txtNumRecibo.Text) & ""
                listaSqlStrings.Add(SqlString)

                SqlString = "delete from glosasFacturas where numDocumento=" & CInt(Me.txtNumRecibo.Text) & ""
                listaSqlStrings.Add(SqlString)

                If transaccionLetras(listaSqlStrings) Then
                    MsgBox("Documento anulado correctamente  !  !  !", MsgBoxStyle.Information)
                    If statusNC = "" And (cbxConcepto.SelectedIndex = 3 Or cbxConcepto.SelectedIndex = 4) Then eliminarDocumentoPlano()
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se anuló documento  !  !  !", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxConcepto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxConcepto.SelectedIndexChanged
        If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
            lblMontoME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMontoMN.Text = ""
        Else
            lblMontoMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
            lblMontoME.Text = ""
        End If
    End Sub
    Private Sub eliminarDocumentoPlano()
        Dim pathData, pathEnvio, pathFirma, pathOridat, pathParse, pathRepo, pathRpta, pathTemp As String

        If generaDocumentoTicket = True Then
            pathData = "\data\" : pathEnvio = "\envio\" : pathFirma = "\firma\" : pathOridat = "\oridat\"
            pathParse = "\parse\" : pathRepo = "\repo\" : pathRpta = "\rpta\R" : pathTemp = "\temp\"
        Else
            pathData = "\SFS_v1.4_A4\sunat_archivos\sfs\data\" : pathEnvio = "\SFS_v1.4_A4\sunat_archivos\sfs\envio\"
            pathFirma = "\SFS_v1.4_A4\sunat_archivos\sfs\firma\" : pathOridat = "\SFS_v1.4_A4\sunat_archivos\sfs\oridat\"
            pathParse = "\SFS_v1.4_A4\sunat_archivos\sfs\parse\" : pathRepo = "\SFS_v1.4_A4\sunat_archivos\sfs\repo\"
            pathRpta = "\SFS_v1.4_A4\sunat_archivos\sfs\rpta\R" : pathTemp = "\SFS_v1.4_A4\sunat_archivos\sfs\temp\"
        End If

        Try
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".CAB")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".DET")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".LEY")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".TRI")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".ACA")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathEnvio & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".ZIP") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathEnvio & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".ZIP")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathFirma & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathFirma & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathOridat & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathOridat & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathParse & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathParse & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".PDF") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".PDF")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathTemp & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathTemp & ruc_archivoPlano & "-" & tipoDocumento & "-" & serDocVenta & "-" & txtNumDocVenta.Text & ".XML")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim arrayMoneda() As String = {"S/.", "$  ", "€  "}
        Dim arrayTipoPago() As String = {"Cheque MN", "Cheque ME", "Efect. MN", "Efect. ME", "P.Tarjeta", "Transf/Abono Cta"}
        Dim stringConcepto As String
        Dim stringPago As String

        oDataSet = New DataSet()
        Dim daCambio As SqlDataAdapter = New SqlDataAdapter("SELECT  *from tiposMonedas where idMoneda=" & Me.cbxTipoMoneda.SelectedIndex + 1 & "", Connection)
        daCambio.Fill(oDataSet, "tipoCambio")

        Dim daDatosCheques As SqlDataAdapter = New SqlDataAdapter("SELECT  *from datosCheques where numRecibo=" & CInt(Me.txtNumRecibo.Text) & "", Connection)
        daDatosCheques.Fill(oDataSet, "datosCheques")

        Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("SELECT  *from glosasFacturas where numDocumento=" & CInt(Me.txtNumRecibo.Text) & "", Connection)
        daGlosas.Fill(oDataSet, "glosasFacturas")

        'Try
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
                te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.numCorrelativo & " " & Me.dtpFechaVcmto.Text & " " & Me.importeLetraME & Space(9 - Len(Str(Me.importeLetraME))) & " " & Me.txtMontoME.Text & Space(8 - Len(Me.txtMontoME.Text)) & " " & Me.importeLetraME - Me.totalAmortizacion & Space(8 - Len(Str(Me.importeLetraME - Me.totalAmortizacion))) & "   " & _
                                    Me.txtNumLetra.Text & " " & Me.numCorrelativo & "  " & Me.dtpFechaVcmto.Text & "  " & Me.importeLetraME & "    " & Me.txtMontoME.Text & "   " & Me.importeLetraME - Me.totalAmortizacion & enter & enter
            Else
                te.Text = te.Text & Me.txtNumLetra.Text & Space(12 - Len(Me.txtNumLetra.Text)) & " " & Me.numCorrelativo & " " & Me.dtpFechaVcmto.Text & " " & Me.importeLetraMN & Space(9 - Len(Str(Me.importeLetraMN))) & " " & Me.txtMonto.Text & Space(8 - Len(Me.txtMonto.Text)) & " " & Me.importeLetraMN - Me.totalAmortizacion & Space(8 - Len(Str(Me.importeLetraMN - Me.totalAmortizacion))) & "   " & _
                                    Me.txtNumLetra.Text & " " & Me.numCorrelativo & "  " & Me.dtpFechaVcmto.Text & "  " & Me.importeLetraMN & "    " & Me.txtMonto.Text & "   " & Me.importeLetraMN - Me.totalAmortizacion & enter & enter
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
            te.Text = te.Text & enter & "Nota:Proceso generó comprobante pago N°: " & Me.txtNumDocumento & Space(53 - Len("Nota:Proceso generó comprobante pago N°:" & Me.cbxTipoDocVenta.Text & " " & Me.txtNumDocumento)) & "   " & _
                                        "Nota:Proceso generó comprobante pago N°: " & Me.txtNumDocumento & enter
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
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            configurarImpresion()
            PrintDocument1.Print()
        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        '    Connection.Close()
        'End Try
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

        Dim Ancho As Short = 906
        Dim Alto As Short = 551

        'Dim Ancho As Short = 1006
        'Dim Alto As Short = 551

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
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.numDocGenCI = ""
        Me.numDocGenACI = ""
        Me.numCorrelativo = ""
        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtTipoCambio.Text = ""
        Me.txtNumLetra.Text = ""
        Me.txtMonto.Text = ""
        Me.txtMontoME.Text = ""
        Me.txtNumRecibo.Text = ""
        Me.txtNumRecibo.Focus()
    End Sub
    Private Sub txtNumRecibo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumRecibo.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class