Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmnotaCreditoFallas
    Dim te As New RichTextBox
    Dim txtStringNumDocumento, txtStringNumDocumentoPD, fechaCierre As String
    Dim codigoCliente, tipoVenta, numLetra, numGuia, numReciboAdelanto As String
    Dim txtnomDocumento As String = "PD"
    Dim txtTipoDocumentoEN As String = "EN"
    Dim txtTipoDocumento As String = "NC"
    Dim item, stockActual As Integer
    Dim sumaSubTotales, intFinanciero, igv As Decimal
    Dim totVentaMN, totVentaME As Decimal
    Dim chrConcepto As Char
    Private oDataSet As DataSet
    Private Sub frmnotaCreditoFallas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='NC'")
        Dim strCodigoVendedor As String = ("SELECT * FROM vendedores where idVendedor=1")
        Me.fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.txtSerieDocumento.Text = "01"
        Me.txtNumNotaCredito.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.KeyPreview = True
        Me.txtNumDocumento.Focus()
    End Sub
    Private Sub frmnotaCreditoFallas_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            btnBuscar_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                btnProcesar_Click(sender, e)
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
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        Dim oProducto As Producto = New Producto()
        oDataSet = New DataSet()
        sumaSubTotales = 0
        Me.dgvProductos.Rows.Clear()

        Try
            If Trim(Me.txtNumDocumento.Text) = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Me.txtNumDocumento.Focus()
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()

            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT  *from vtaCabecera where tipDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and numDocumento='" & Me.txtNumDocumento.Text & "' and rtrim(status)='' and rtrim(statusNC)=''", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento, fue aplicado a una 'Nota Crédito' o está anulado.", MsgBoxStyle.Critical)
                Me.txtNumDocumento.Text = ""
                Me.txtNumDocumento.Focus()
                Exit Sub
            End If

            Me.chrConcepto = Me.oDataSet.Tables(0).Rows(0).Item(4)
            If chrConcepto = "3" Or chrConcepto = "4" Then
                MsgBox("Error, Concepto no válido para esta Nota Crédito.", MsgBoxStyle.Critical)
                Me.txtNumDocumento.Text = ""
                Me.txtNumDocumento.Focus()
                Exit Sub
            End If

            Me.numGuia = Me.oDataSet.Tables(0).Rows(0).Item(3)
            Me.cbxTipoVenta.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(4)
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(15) - 1
            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(12)
            Me.txtNumGuia.Text = Me.numGuia
            Me.numLetra = Me.oDataSet.Tables(0).Rows(0).Item(5)
            Me.totVentaMN = Me.oDataSet.Tables(0).Rows(0).Item(8)
            Me.totVentaME = Me.oDataSet.Tables(0).Rows(0).Item(9)
            Me.intFinanciero = Me.oDataSet.Tables(0).Rows(0).Item(10)
            Me.igv = Me.oDataSet.Tables(0).Rows(0).Item(11)

            Me.btnAnular.Enabled = False

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(6) & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daVtaDetalle = New SqlDataAdapter("SELECT *from vtaDetalle where numDocumento='" & Me.txtNumDocumento.Text & _
            "' and tipDocumento='" & Me.cbxTipoDocumento.Text & "'", Connection)
            daVtaDetalle.Fill(oDataSet, "vtaDetalle")

            Me.codigoCliente = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)

            Dim colNombreProducto As DataColumn = New DataColumn()
            colNombreProducto.Caption = "Descripción Producto"
            colNombreProducto.ColumnName = "descripcionProducto"
            Me.oDataSet.Tables(2).Columns.Add(colNombreProducto)

            Dim colGrupoProducto As DataColumn = New DataColumn()
            colGrupoProducto.Caption = "Grupo Producto"
            colGrupoProducto.ColumnName = "grupoProducto"
            Me.oDataSet.Tables(2).Columns.Add(colGrupoProducto)

            Dim colNumSerie As DataColumn = New DataColumn()
            colNumSerie.Caption = "numSerie"
            colNumSerie.ColumnName = "numSerie"
            Me.oDataSet.Tables(2).Columns.Add(colNumSerie)

            Dim colNumMotor As DataColumn = New DataColumn()
            colNumMotor.Caption = "numMotor"
            colNumMotor.ColumnName = "numMotor"
            Me.oDataSet.Tables(2).Columns.Add(colNumMotor)

            Dim colNumChasis As DataColumn = New DataColumn()
            colNumChasis.Caption = "numChasis"
            colNumChasis.ColumnName = "numChasis"
            Me.oDataSet.Tables(2).Columns.Add(colNumChasis)

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT *from productos where idProducto='" & Me.oDataSet.Tables(2).Rows(i).Item(3) & "' ", Connection)
                daProductos.Fill(oDataSet, "productos")
            Next

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                Dim daSeries As SqlDataAdapter = New SqlDataAdapter("SELECT *from numerosSerie where numDoc='" & Me.numGuia & "' ", Connection)
                daSeries.Fill(oDataSet, "series")
            Next

            Dim daLetrasClientes = New SqlDataAdapter("SELECT *from letrasClientes where numLetra='" & Me.numLetra & "' and status='A'", Connection)
            daLetrasClientes.Fill(oDataSet, "letrasClientes")
            If Me.oDataSet.Tables(5).Rows.Count >= 1 Then Me.numReciboAdelanto = Me.oDataSet.Tables(5).Rows(0).Item(12) & Me.oDataSet.Tables(5).Rows(0).Item(9)
            Connection.Close()

            Dim oDataRow As DataRow
            Dim oDataRow1 As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                    If Me.oDataSet.Tables(3).Rows(x).Item(0) = Me.oDataSet.Tables(2).Rows(i).Item(3) Then
                        oDataRow = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow1 = Me.oDataSet.Tables(2).Rows(i)
                        If Me.cbxTipoVenta.SelectedIndex <> 5 Then
                            oDataRow(11) = Me.oDataSet.Tables(3).Rows(x).Item(2)
                        Else
                            oDataRow(11) = "FACTURACION OTROS CONCEPTOS"
                        End If
                        oDataRow1(12) = Me.oDataSet.Tables(3).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim oDataRow2 As DataRow
            Dim oDataRow3 As DataRow
            Dim oDataRow4 As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If Me.oDataSet.Tables(4).Rows(x).Item(0) = Me.oDataSet.Tables(2).Rows(i).Item(3) And _
                       Me.oDataSet.Tables(4).Rows(x).Item(4) = Me.numGuia Then
                        oDataRow2 = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow3 = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow4 = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow2(13) = Me.oDataSet.Tables(4).Rows(x).Item(1)
                        oDataRow3(14) = Me.oDataSet.Tables(4).Rows(x).Item(2)
                        oDataRow4(15) = Me.oDataSet.Tables(4).Rows(x).Item(3)
                    End If
                Next x
            Next i

            item = 0
            For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                item = item + 1
                Me.dgvProductos.Rows.Add()
                Me.dgvProductos.Rows(x).Cells(0).Value = Me.item
                Me.dgvProductos.Rows(x).Cells(1).Value = Me.oDataSet.Tables(2).Rows(x).Item(3)
                Me.dgvProductos.Rows(x).Cells(2).Value = Me.oDataSet.Tables(2).Rows(x).Item(11)
                Me.dgvProductos.Rows(x).Cells(3).Value = Me.oDataSet.Tables(2).Rows(x).Item(4)
                Me.dgvProductos.Rows(x).Cells(4).Value = Me.oDataSet.Tables(2).Rows(x).Item(5)
                Me.dgvProductos.Rows(x).Cells(5).Value = Me.oDataSet.Tables(2).Rows(x).Item(6)
                Me.dgvProductos.Rows(x).Cells(7).Value = Me.oDataSet.Tables(2).Rows(x).Item(12)
                Me.dgvProductos.Rows(x).Cells(8).Value = Me.oDataSet.Tables(2).Rows(x).Item(13)
                Me.dgvProductos.Rows(x).Cells(9).Value = Me.oDataSet.Tables(2).Rows(x).Item(14)
                Me.dgvProductos.Rows(x).Cells(10).Value = Me.oDataSet.Tables(2).Rows(x).Item(15)
                sumaSubTotales += Val(Me.dgvProductos.Rows(x).Cells(5).Value)
            Next

            Me.txtSubTotal.Text = Me.sumaSubTotales
            Me.txtInteres.Text = Me.intFinanciero
            Me.txtIGV.Text = Me.igv
            Me.txtTotalPagar.Text = Me.totVentaMN
            Me.txtTotalPagarME.Text = Me.totVentaME
            Me.txtTotalNotaCredito.Text = 0
            Me.txtTotalNotaCredito.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        lbltotalNotaCredito.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim oProducto As Producto = New Producto()
        'Variables PD
        Dim txtNumDocumentoPD As String
        Dim strUltimoNumeroPD As String = ("SELECT * FROM ultimosNumeros where tipDocumento='PD' and tipMovimiento='EN'")
        txtNumDocumentoPD = devuelveUltimoNumero(strUltimoNumeroPD) + 1

        Dim ctaTrue As Byte
        Try
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim SqlString2 As String = ""
            Dim SqlString3 As String = ""
            Dim SqlString4 As String = ""
            Dim SqlString5 As String = ""
            Dim SqlString6 As String = ""
            Dim SqlString7 As String = ""
            Dim SqlString8 As String = ""
            Dim SqlString9 As String = ""
            Dim SqlString10 As String = ""
            Dim SqlString11 As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim ListSqlStrings1 As New ArrayList
            Dim ListSqlStrings2 As New ArrayList
            Dim ListSqlStrings3 As New ArrayList

            If Trim(Me.txtNumDocumento.Text) = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Me.txtNumDocumento.Focus()
                Exit Sub
            End If

            If CInt(Me.txtTotalNotaCredito.Text) <= 0 Then
                MsgBox("Por favor, indique monto de 'la Nota Crédito' para continuar.", MsgBoxStyle.Information)
                Me.txtTotalNotaCredito.Focus()
                Exit Sub
            End If

            If CDec(Me.txtTotalNotaCredito.Text) > CDec(Me.txtTotalPagar.Text) Then
                MsgBox("Por favor, monto de la 'Nota Crédito' no puede ser mayor al documento a procesar.", MsgBoxStyle.Critical)
                Me.txtTotalNotaCredito.Focus()
                Exit Sub
            End If

            For i As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                If Me.dgvProductos.Rows(i).Cells(6).Value = "True" Then
                    ctaTrue += 1
                End If
            Next

            If MsgBox("Está seguro de aplicar nota de crédito a este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                If ctaTrue <= 0 Then
                    MsgBox("Por favor, haz un check en el producto o los productos a extornar.", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Me.txtStringNumDocumento = oProducto.stringLetra(Me.cbxTipoDocumento.Text, Me.txtNumDocumento.Text, "", "")
                SqlString = "INSERT INTO notaCreditoCa (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,docReferencia," & _
               "idCliente,totVentaMN,totVentaME,intFinanciero,IGV,fecOperacion,idMoneda,tipCambio,tasInteres,status) VALUES ('" & _
               Me.txtTipoDocumento & "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumNotaCredito.Text & ",'" & txtNumDocumentoPD & "','" & _
               Me.cbxTipoVenta.SelectedIndex & "','" & Me.txtStringNumDocumento & "'," & Me.codigoCliente & "," & Me.txtTotalNotaCredito.Text & "," & _
               Me.txtTotalNotaCredito.Text & "," & Me.txtInteres.Text & "," & Me.txtIGV.Text & ",'" & Me.dtpFechaVcmto.Text & "'," & _
               Me.cbxTipoMoneda.SelectedIndex + 1 & ",0,0,' ')"

                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    If Me.dgvProductos.Rows(i).Cells(6).Value = "True" Then
                        SqlString1 = "INSERT INTO notaCreditoDe (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal," & _
                        "afeIGV,fecOperacion,status) VALUES ('" & txtTipoDocumento & "','" & txtSerieDocumento.Text & "'," & txtNumNotaCredito.Text & _
                        " , " & dgvProductos.Rows(i).Cells(1).Value.ToString() & _
                        " , " & dgvProductos.Rows(i).Cells(3).Value.ToString() & _
                        " , " & dgvProductos.Rows(i).Cells(4).Value.ToString() & _
                        " , " & dgvProductos.Rows(i).Cells(5).Value.ToString() & _
                        " , ' ' , '" & Me.dtpFechaVcmto.Text & "' ,' ')"
                        ListSqlStrings.Add(SqlString1)
                    End If
                Next

                If Me.cbxTipoVenta.SelectedIndex = 1 Then
                    SqlString11 = "UPDATE letrasClientes set statusNC='" & Me.numReciboAdelanto & "' where (numLetra='" & numLetra & "' and status='A')"

                    SqlString2 = "UPDATE letrasClientes set fecPago='" & dtpFechaVcmto.Text & "',numRecibo='NC" & txtNumNotaCredito.Text & _
                    "',status='C' where (numLetra='" & numLetra & "' and rtrim(numRecibo)='') or (numLetra='" & numLetra & "' and status='A')"
                Else
                    SqlString2 = "Select *from letrasClientes"
                    SqlString11 = "Select *from letrasClientes"
                End If

                If Me.cbxTipoVenta.SelectedIndex <> 5 Then
                    'Retornando Producto(s) a almacén con Parte Diario (PD)
                    SqlString3 = "INSERT INTO almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor,fecOrigen,nomOrigen,dirOrigen," & _
                    "rucDNI_1,fecLlegada,idCliente,transLlegada,status) VALUES ('" & Me.txtnomDocumento & "','" & Me.txtTipoDocumentoEN & "' ," & _
                    txtNumDocumentoPD & ",1,'" & Me.dtpFechaVcmto.Text & "','" & txtNombreEmpresa & "','" & txtDireccionEmpresa & "','20103855391','" & _
                    Me.dtpFechaVcmto.Text & "',1,'','+')"

                    For i As Integer = 0 To dgvProductos.Rows.Count - 1
                        Dim sqlSaldo As String
                        If Me.dgvProductos.Rows(i).Cells(6).Value = "True" Then
                            SqlString4 = "INSERT INTO almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad,status) VALUES ('" & _
                            Me.txtnomDocumento & "','" & Me.txtTipoDocumentoEN & "'," & txtNumDocumentoPD & ",'" & dgvProductos.Rows(i).Cells(1).Value & _
                            "'," & dgvProductos.Rows(i).Cells(4).Value & ",'0')"

                            sqlSaldo = "SELECT * FROM saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                            stockActual = devuelveStock(sqlSaldo)

                            stockActual = stockActual + Me.dgvProductos.Rows(i).Cells(4).Value
                            SqlString5 = "UPDATE saldosAlmacenes Set stock='" & stockActual & "' where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"

                            ListSqlStrings1.Add(SqlString4)
                            ListSqlStrings2.Add(SqlString5)
                        End If
                    Next

                    For i As Integer = 0 To dgvProductos.Rows.Count - 1
                        SqlString6 = "UPDATE numerosSerie Set numDoc='',sExtorno='NC" & Me.txtNumNotaCredito.Text & "' where  numDoc='" & Trim(Me.numGuia) & "'"
                        ListSqlStrings3.Add(SqlString6)
                    Next
                Else
                    SqlString3 = "SELECT * FROM ultimosNumeros"
                    SqlString4 = "SELECT * FROM ultimosNumeros"
                    SqlString5 = "SELECT * FROM ultimosNumeros"
                    ListSqlStrings1.Add(SqlString4)
                    ListSqlStrings2.Add(SqlString5)
                End If

                SqlString7 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumNotaCredito.Text & " where tipDocumento= '" & txtTipoDocumento & "'"
                SqlString8 = "UPDATE ultimosNumeros Set numero=" & txtNumDocumentoPD & " where tipDocumento= '" & txtnomDocumento & "' and tipMovimiento='EN'"

                If Me.txtGlosa.Text <> "" Then
                    SqlString9 = "INSERT INTO glosasFacturas (tipDocumento,numDocumento,glosa) VALUES ('" & _
                                Me.txtTipoDocumento & "'," & Me.txtNumNotaCredito.Text & ",'" & Me.txtGlosa.Text & "')"
                Else
                    SqlString9 = "Select *from glosasFacturas"
                End If

                SqlString10 = "UPDATE vtaCabecera Set statusNC='NC" & Me.txtNumNotaCredito.Text & "' where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento='" & Me.txtNumDocumento.Text & "'"

                If grabacionNotaCredito(SqlString, SqlString11, SqlString2, SqlString3, SqlString7, SqlString8, SqlString9, SqlString10, _
                                        ListSqlStrings, ListSqlStrings1, ListSqlStrings2, ListSqlStrings3) Then
                    MsgBox("'Nota Crédito' ha sido procesado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                    Me.Close()
                Else
                    MsgBox("Error, no se procesó 'Nota Crédito'.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)
            te.Text = enter & enter & enter & enter & enter & enter & enter & _
            "   " & Me.txtNombre.Text & enter & enter & enter & _
            "   " & Me.txtDireccion.Text & "                 " & Me.txtDNI.Text & "               " & Me.dtmFecha.Text & enter & enter & enter & enter

            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                te.Text = te.Text & enter & _
                Me.dgvProductos.Rows(i).Cells(4).Value.ToString.PadRight(5, " ") & _
                Me.dgvProductos.Rows(i).Cells(1).Value.ToString.PadRight(5, " ") & _
                Me.dgvProductos.Rows(i).Cells(2).Value.ToString.PadRight(50, " ") & _
                Me.dgvProductos.Rows(i).Cells(3).Value.ToString.PadLeft(20, " ") & _
                Me.dgvProductos.Rows(i).Cells(5).Value.ToString.PadLeft(10, " ")
            Next

            te.Text = te.Text & enter & enter & enter & enter & enter & enter & enter & enter
            te.Text = te.Text & enter & "          " & _
           numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00")) - 3)) & _
                                        " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,###0.00")) & "/100 SOLES "

            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "#####0.00").PadLeft(90, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "#####0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagar.Text), "######0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagarME.Text), "######0.00").PadLeft(40, " ")

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
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior, _
            AreaImpresion_Ancho, AreaImpresion_Alto)
            Dim NroLineasImpresion As Integer = CInt(AreaImpresion_Alto / Fuente.Height)
            Dim NroLineasRelleno, NroLetrasLinea As Integer
            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente, _
            New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, _
            NroLineasRelleno)
            e.Graphics.DrawString(Mid(TextoImpresion, CaracterActual + 1), Fuente, _
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
        VistaPrevia("Courier New", 10, te.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 937
        Dim Alto As Short = 748

        Dim left As Short = 50
        Dim top As Short = 50
        Dim bottom As Short = 50
        Dim right As Short = 50

        TamañoPersonal = New Printing.PaperSize(nombrePapel, Ancho, Alto)
        margenes = New Printing.Margins(left, right, top, bottom)

        ' Asignamos la impresora seleccionada
        'prdDocumento.PrinterSettings = ImpresoraActual
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
                SqlString = "INSERT INTO notaCreditoCa (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,docReferencia,idCliente," & _
                "totVentaMN,totVentaME,intFinanciero,IGV,fecOperacion,idMoneda,tipCambio,tasInteres,status) VALUES ('" & Me.txtTipoDocumento & _
                "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumNotaCredito.Text & ",' ',0,' ',1,0,0,0,0,'" & Me.dtpFechaVcmto.Text & "',1,0,0,'A')"

                SqlString1 = "INSERT INTO notaCreditoDe (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV," & _
                             "fecOperacion,status) VALUES ('" & Me.txtTipoDocumento & "' , '" & Me.txtSerieDocumento.Text & "' , " & _
                             Me.txtNumNotaCredito.Text & ",1,0,0,0,0,'" & Me.dtpFechaVcmto.Text & "','A')"

                SqlString2 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumNotaCredito.Text & " where tipDocumento= '" & Me.txtTipoDocumento & "'"

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
    Private Sub txtNumDocumento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumento.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtTotalNotaCredito_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalNotaCredito.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtNumDocumento.Text = ""
        Me.txtNumRecibo.Text = ""
        Me.txtNumGuia.Text = ""
        Me.dgvProductos.Rows.Clear()
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtTotalPagarME.Text = 0
        Me.txtTotalNotaCredito.Text = 0
        Me.btnAnular.Enabled = True
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='NC'")
        Me.txtNumNotaCredito.Text = devuelveUltimoNumero(strUltimoNumero) + 1
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class