Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmanularDocumentoVta
    Dim te As New RichTextBox
    Dim codigoCliente, numLetra, numGuia As String
    Dim item As Integer
    Dim sumaSubTotales, intFinanciero, igv, cuotaInicial, tipoCambio As Decimal
    Dim totVentaMN, totVentaME, txtImpLetraME, txtImpLetra As Decimal
    Dim txtCanCuotas As Integer
    Private oDataSet As DataSet
    Private Sub frmanularDocumentoVta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False

        '-------13-08-15
        For i As Integer = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
            matrizRecibos(i, 0) = " "
            matrizRecibos(i, 1) = " "
        Next i

        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Me.txtSerieDocumento.Text = "01"
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0

        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.txtCuotas.Text = ""
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 0
        Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.txtCuotas.Enabled = False
        Me.KeyPreview = True
        Me.btnImprimir.Enabled = False
        Me.btnGrabar.Enabled = False

        If flag = 1 And fecDocumento = Date.Today Then
            Me.btnBuscar.Enabled = False
            Me.btnAnular.Enabled = False
            Me.btnImprimir.Enabled = True
            Me.btnLimpiar.Enabled = False
            Me.cbxTipoDocumento.Text = tipMovimiento
            Me.txtNumDocumento.Text = numDocumento
            Me.btnBuscar_Click(sender, e)
        Else
            If flag = 1 And fecDocumento <> Date.Today Then
                Me.btnBuscar.Enabled = False
                Me.btnAnular.Enabled = False

                Me.btnImprimir.Enabled = True

                Me.btnLimpiar.Enabled = False
                Me.cbxTipoDocumento.Text = tipMovimiento
                Me.txtNumDocumento.Text = numDocumento
                Me.btnBuscar_Click(sender, e)
            End If
        End If

        If flag = 2 Then
            Me.btnBuscar.Enabled = False
            Me.btnAnular.Enabled = False
            Me.btnLimpiar.Enabled = False
            Me.cbxTipoDocumento.Text = tipMovimiento
            Me.txtNumDocumento.Text = numDocumento
            Me.btnBuscar_Click(sender, e)
        End If
    End Sub
    Private Sub frmanularDocumentoVta_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
                            'btnLimpiar_Click(sender, e)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub cbxTipoDocumento_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbxTipoDocumento.SelectedIndexChanged
        If cbxTipoDocumento.SelectedIndex = 0 Then
            txtSerieDocumento.Text = "B001"
        Else
            txtSerieDocumento.Text = "F001"
        End If
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim oProducto As Producto = New Producto()
        Me.dgvProductos.Rows.Clear()
        sumaSubTotales = 0

        Try
            If Me.txtNumDocumento.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()

            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("select * from vtaCabecera where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & Me.txtNumDocumento.Text & " and status<>'A' ", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Me.numGuia = Me.oDataSet.Tables(0).Rows(0).Item(3)
            Me.cbxTipoVenta.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(4)
            Me.numLetra = Me.oDataSet.Tables(0).Rows(0).Item(5)
            Me.totVentaMN = Me.oDataSet.Tables(0).Rows(0).Item(8)
            Me.totVentaME = Me.oDataSet.Tables(0).Rows(0).Item(9)
            Me.intFinanciero = Me.oDataSet.Tables(0).Rows(0).Item(10)
            Me.igv = Me.oDataSet.Tables(0).Rows(0).Item(11)
            Me.dtpFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(12)
            Me.cuotaInicial = Me.oDataSet.Tables(0).Rows(0).Item(14)
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(15) - 1
            Me.tipoCambio = Me.oDataSet.Tables(0).Rows(0).Item(16)
            Me.txtTipoCambio.Text = Me.tipoCambio

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where idCliente=" & Me.oDataSet.Tables(0).Rows(0).Item(6) & " ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daVtaDetalle = New SqlDataAdapter("select * from vtaDetalle where numDocumento=" & Me.txtNumDocumento.Text & " and tipDocumento='" & Me.cbxTipoDocumento.Text & "' order by idProducto asc", Connection)
            daVtaDetalle.Fill(oDataSet, "vtaDetalle")

            Me.codigoCliente = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            If Me.oDataSet.Tables(0).Rows(0).Item(3).ToString.Trim = "FV" Then
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            Else
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(4)
            End If

            Dim colNumGuia As DataColumn = New DataColumn()
            colNumGuia.Caption = "numGuia"
            colNumGuia.ColumnName = "numGuia"
            Me.oDataSet.Tables(2).Columns.Add(colNumGuia)

            Dim colDescripcion As DataColumn = New DataColumn()
            colDescripcion.Caption = "Descripción"
            colDescripcion.ColumnName = "descripcion"
            Me.oDataSet.Tables(2).Columns.Add(colDescripcion)

            Dim colMarca As DataColumn = New DataColumn()
            colMarca.Caption = "Marca"
            colMarca.ColumnName = "marca"
            Me.oDataSet.Tables(2).Columns.Add(colMarca)

            Dim colModelo As DataColumn = New DataColumn()
            colModelo.Caption = "Modelo"
            colModelo.ColumnName = "modelo"
            Me.oDataSet.Tables(2).Columns.Add(colModelo)

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

            Dim colColor As DataColumn = New DataColumn()
            colColor.Caption = "Color"
            colColor.ColumnName = "Color"
            Me.oDataSet.Tables(2).Columns.Add(colColor)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                oDataRow = Me.oDataSet.Tables(2).Rows(i)
                oDataRow(10) = Me.numGuia
            Next i

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                Dim daProductos As SqlDataAdapter = New SqlDataAdapter("select * from productos where idProducto=" & Me.oDataSet.Tables(2).Rows(i).Item(3) & " order by idProducto asc", Connection)
                daProductos.Fill(oDataSet, "productos")
            Next

            Dim daSeries As SqlDataAdapter = New SqlDataAdapter("select * from numerosSerie where numDoc='" & Me.oDataSet.Tables(2).Rows(0).Item(10) & "' order by idProducto asc", Connection)
            daSeries.Fill(oDataSet, "series")

            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select *from letrasClientes where numLetra='" & Me.numLetra & "' and idCliente=" & Me.codigoCliente & "", Connection)
            daLetras.Fill(oDataSet, "letras")

            If Me.cbxTipoVenta.SelectedIndex = 3 Or Me.cbxTipoVenta.SelectedIndex = 4 Then
                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("select * from recibosClientes where NumDocGenCI='" & Me.cbxTipoDocumento.Text & Me.txtNumDocumento.Text & "' and idCliente=" & Me.codigoCliente & "", Connection)
                daRecibos.Fill(oDataSet, "recibos")
            Else
                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("select * from recibosClientes where NumDocGenACI='" & Me.cbxTipoDocumento.Text & Me.txtNumDocumento.Text & "' and idCliente=" & Me.codigoCliente & "", Connection)
                daRecibos.Fill(oDataSet, "recibos")
            End If

            If Me.cbxTipoVenta.SelectedIndex = 10 Then
                Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("select * from glosasFacturas where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & "", Connection)
                daGlosas.Fill(oDataSet, "glosasFacturas")
                Me.txtGlosa.Text = Me.oDataSet.Tables(7).Rows(0).Item(2)
            End If
            Connection.Close()

            Dim oDataRow1 As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                    If Me.oDataSet.Tables(3).Rows.Item(x).Item(0) = Me.oDataSet.Tables(2).Rows.Item(i).Item(3) Then
                        oDataRow1 = Me.oDataSet.Tables(2).Rows(i)
                        If Me.cbxTipoVenta.SelectedIndex <> 10 Then
                            oDataRow1(11) = Me.oDataSet.Tables(3).Rows(x).Item(2) 'Descripción
                            oDataRow1(12) = Me.oDataSet.Tables(3).Rows(x).Item(4) 'Marca
                            oDataRow1(13) = Me.oDataSet.Tables(3).Rows(x).Item(5) 'Modelo
                        Else
                            oDataRow1(11) = "FACTURACION OTROS CONCEPTOS"
                            oDataRow1(12) = "OTROS"
                            oDataRow1(13) = "OTROS"
                        End If
                    End If
                Next x
            Next i

            If Me.cbxTipoVenta.SelectedIndex <> 3 And Me.cbxTipoVenta.SelectedIndex <> 4 Then
                Dim oDataRow2 As DataRow
                For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    oDataRow2 = Me.oDataSet.Tables(2).Rows(i)
                    oDataRow2(14) = Me.oDataSet.Tables(4).Rows(i).Item(1) 'numSerie
                    oDataRow2(15) = Me.oDataSet.Tables(4).Rows(i).Item(2) 'numMotor
                    oDataRow2(16) = Me.oDataSet.Tables(4).Rows(i).Item(3) 'numChasis
                    oDataRow2(17) = Me.oDataSet.Tables(4).Rows(i).Item(6) 'Color
                Next i
            End If

            If Me.cbxTipoVenta.SelectedIndex = 1 Then
                If oDataSet.Tables(6).Rows.Count >= 1 Then
                    Me.txtNumRecibo.Text = Me.oDataSet.Tables(6).Rows(0).Item(0)
                    Me.txtMontoRecibo.Text = Format(Me.oDataSet.Tables(6).Rows(0).Item(3), "###,###0.00")
                End If
                Me.txtCuotas.Text = Me.oDataSet.Tables(5).Rows.Count
                Me.txtImpLetra = Me.oDataSet.Tables(5).Rows(CInt(Me.txtCuotas.Text) - 1).Item(4)
                Me.txtImpLetraME = Me.oDataSet.Tables(5).Rows(CInt(Me.txtCuotas.Text) - 1).Item(5)
            Else
                Me.txtNumRecibo.Text = Me.oDataSet.Tables(6).Rows(0).Item(0)
                Me.txtMontoRecibo.Text = Format(Me.oDataSet.Tables(6).Rows(0).Item(3), "###,###0.00")
                For i As Integer = 0 To Me.oDataSet.Tables(6).Rows.Count - 1
                    matrizRecibos(i, 0) = Me.oDataSet.Tables(6).Rows(i).Item(0)
                    If Me.oDataSet.Tables(6).Rows(0).Item(14) > 1 Then
                        matrizRecibos(i, 1) = Me.oDataSet.Tables(6).Rows(i).Item(4)
                    Else
                        matrizRecibos(i, 1) = Me.oDataSet.Tables(6).Rows(i).Item(3)
                    End If
                Next i
            End If
            Me.txtNumGuia.Text = Me.numGuia

            For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                item = item + 1
                Me.dgvProductos.Rows.Add()
                Me.dgvProductos.Rows(x).Cells(0).Value = Me.item
                Me.dgvProductos.Rows(x).Cells(1).Value = Me.oDataSet.Tables(2).Rows(x).Item(3)
                Me.dgvProductos.Rows(x).Cells(2).Value = Me.oDataSet.Tables(2).Rows(x).Item(11)
                Me.dgvProductos.Rows(x).Cells(3).Value = Me.oDataSet.Tables(2).Rows(x).Item(12)
                Me.dgvProductos.Rows(x).Cells(4).Value = Me.oDataSet.Tables(2).Rows(x).Item(13)
                Me.dgvProductos.Rows(x).Cells(5).Value = Me.oDataSet.Tables(2).Rows(x).Item(4)
                Me.dgvProductos.Rows(x).Cells(6).Value = Me.oDataSet.Tables(2).Rows(x).Item(14)
                Me.dgvProductos.Rows(x).Cells(7).Value = Me.oDataSet.Tables(2).Rows(x).Item(15)
                Me.dgvProductos.Rows(x).Cells(8).Value = Me.oDataSet.Tables(2).Rows(x).Item(16)
                Me.dgvProductos.Rows(x).Cells(9).Value = Me.oDataSet.Tables(2).Rows(x).Item(17)
                Me.dgvProductos.Rows(x).Cells(10).Value = Me.oDataSet.Tables(2).Rows(x).Item(5)
                Me.dgvProductos.Rows(x).Cells(11).Value = Me.oDataSet.Tables(2).Rows(x).Item(6)
                sumaSubTotales += Val(Me.dgvProductos.Rows(x).Cells(5).Value)
            Next

            Me.txtSubTotal.Text = Format(Me.sumaSubTotales - cuotaInicial, "###,##0.00")
            Me.txtInteres.Text = Format(Me.intFinanciero, "###,##0.00")
            Me.txtIGV.Text = Format(Me.igv, "###,##0.00")
            If Me.cbxTipoVenta.SelectedIndex <> 3 And Me.cbxTipoVenta.SelectedIndex <> 4 Then
                Me.txtAnticipos.Text = txtMontoRecibo.Text
            Else
                Me.txtAnticipos.Text = "0.00"
            End If
            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                Me.txtTotalPagar.Text = Format(Me.totVentaME, "###,##0.00")
            Else
                Me.txtTotalPagar.Text = Format(Me.totVentaMN, "###,##0.00")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            'Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "select * from tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        ''Dim tipo As String

        ''If cbxTipoDocumento.SelectedIndex = 0 Then
        ''    tipo = "03"
        ''Else
        ''    tipo = "01"
        ''End If

        ''If System.IO.File.Exists("D:\SFS_v1.4\sunat_archivos\sfs\REPO\" & ruc_archivoPlano & "-" & tipo & "-" & txtSerieDocumento.Text & "-" & txtNumDocumento.Text & ".pdf") Then
        ''    System.Diagnostics.Process.Start("D:\SFS_v1.4\sunat_archivos\sfs\REPO\" & ruc_archivoPlano & "-" & tipo & "-" & txtSerieDocumento.Text & "-" & txtNumDocumento.Text & ".pdf")
        ''Else
        ''    MsgBox("Lo siento, no existe documento o no se ha generado  !  !  !", MsgBoxStyle.Exclamation)
        ''End If

        'Try

        '    If Me.cbxTipoDocumento.Text = "BV" Then
        '        Dim en, t As Keys
        '        Dim enter, tab As Char
        '        en = Keys.Enter
        '        t = Keys.Tab
        '        enter = Convert.ToChar(en)
        '        tab = Convert.ToChar(t)
        '        te.Text = enter & enter & enter & enter & enter &
        '        "   " & Me.txtDNI.Text & "                                             " & Me.numGuia & enter & enter & enter &
        '        "   " & Me.txtNombre.Text & enter & enter & enter &
        '        "   " & Me.txtDireccion.Text & "                            " & Me.dtpFecha.Text & enter & enter & enter & enter

        '        'te.Text = te.Text & _
        '        '"CANT. DESCRIPCION          MARCA           MODELO          NUMERO SERIE" & enter
        '        'te.Text = te.Text & "NUMERO MOTOR      NUMERO CHASIS      COLOR      PREC.UNIT.        TOTAL".PadLeft(103) & enter
        '        For i As Integer = 0 To Me.dgvProductos.RowCount - 1
        '            te.Text = te.Text & enter
        '            te.Text = te.Text & dgvProductos.Rows(i).Cells(10).Value.ToString().PadRight(6)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(2).Value.ToString(), 20).PadRight(21)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(3).Value.ToString(), 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(4).Value.ToString(), 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(6).Value.ToString(), 18).PadRight(19) & enter
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(7).Value.ToString(), 18).PadLeft(49) & " "
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(8).Value.ToString(), 18).PadRight(19)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(9).Value.ToString(), 6).PadRight(7)
        '            te.Text = te.Text & Format(Decimal.Parse(dgvProductos.Rows(i).Cells(5).Value.ToString()), "##,##0.00").PadLeft(14)
        '            te.Text = te.Text & Format(Decimal.Parse(dgvProductos.Rows(i).Cells(11).Value.ToString()), "##,##0.00").PadLeft(13)
        '        Next

        '        te.Text = te.Text & enter & enter
        '        te.Text = te.Text & Me.txtGlosa.Text

        '        te.Text = te.Text & enter & enter & enter & enter & enter
        '        te.Text = te.Text & enter & "          " &
        '        numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00")) - 3)) &
        '                                    " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00")) & " /100 " & Me.cbxTipoMoneda.Text & enter
        '        te.Text = te.Text & "                          S.E.ú.O." & enter & enter

        '        If Me.cbxTipoVenta.SelectedIndex = 1 Then te.Text = te.Text & enter & "                Venta al credito en " & Me.txtCanCuotas & " cuotas de " & Format(Me.txtImpLetra, "##,##0.0") & Format(Me.txtImpLetraME, "##,##0") & " " & Me.cbxTipoMoneda.Text & " " & "1er. Vcto: " & Me.oDataSet.Tables(5).Rows(0).Item(7) & enter & enter

        '        'If Me.cbxTipoVenta.SelectedIndex = 0 Or Me.cbxTipoVenta.SelectedIndex = 2 Or Me.cbxTipoVenta.SelectedIndex = 5 Then te.Text = te.Text & enter & "                Venta al contado, recibo N° " & Me.txtNumRecibo.Text & enter & enter

        '        '-------- 13-08-15
        '        If Me.cbxTipoVenta.SelectedIndex = 0 Or Me.cbxTipoVenta.SelectedIndex = 2 Or Me.cbxTipoVenta.SelectedIndex = 3 Or Me.cbxTipoVenta.SelectedIndex = 4 Or Me.cbxTipoVenta.SelectedIndex = 5 Then
        '            Dim pase As Boolean
        '            te.Text = te.Text & enter & "                Venta al contado, recibo(s) N° S/. "
        '            For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
        '                If matrizRecibos(y, 0) <> "" Then
        '                    If pase <> True Then
        '                        te.Text = te.Text & matrizRecibos(y, 0).PadLeft(12) & " " & matrizRecibos(y, 1).PadLeft(10) & enter
        '                    Else
        '                        te.Text = te.Text & matrizRecibos(y, 0).PadLeft(63) & " " & matrizRecibos(y, 1).PadLeft(10) & enter
        '                    End If
        '                    pase = True
        '                End If
        '            Next y
        '            te.Text = te.Text & enter
        '        End If

        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(103, " ") & enter
        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(103, " ") & enter
        '        ''te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ") & enter
        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(103, " ")

        '    Else 'If Me.cbxTipoDocumento.Text = "FV" Then

        '        Dim en, t As Keys
        '        Dim enter, tab As Char
        '        en = Keys.Enter
        '        t = Keys.Tab
        '        enter = Convert.ToChar(en)
        '        tab = Convert.ToChar(t)
        '        te.Text = enter & enter & enter & enter &
        '        "                    " & Me.txtDNI.Text & "                      " & Me.numGuia & enter & enter &
        '        "   " & Me.txtNombre.Text & enter & enter & enter & enter &
        '        "   " & Me.txtDireccion.Text & "                                         " & Me.dtpFecha.Text & enter & enter & enter & enter

        '        'te.Text = te.Text & _
        '        '"CANT. DESCRIPCION          MARCA           MODELO          NUMERO SERIE" & enter
        '        'te.Text = te.Text & "NUMERO MOTOR      NUMERO CHASIS      COLOR      PREC.UNIT.        TOTAL".PadLeft(103) & enter
        '        For i As Integer = 0 To Me.dgvProductos.RowCount - 1
        '            te.Text = te.Text & enter
        '            te.Text = te.Text & dgvProductos.Rows(i).Cells(10).Value.ToString().PadRight(6)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(2).Value.ToString(), 20).PadRight(21)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(3).Value.ToString(), 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(4).Value.ToString(), 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(6).Value.ToString(), 18).PadRight(19) & enter
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(7).Value.ToString(), 18).PadLeft(49) & " "
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(8).Value.ToString(), 18).PadRight(19)
        '            te.Text = te.Text & VisualBasic.Left(dgvProductos.Rows(i).Cells(9).Value.ToString(), 6).PadRight(7)
        '            te.Text = te.Text & Format(Decimal.Parse(dgvProductos.Rows(i).Cells(5).Value.ToString()), "##,##0.00").PadLeft(14)
        '            te.Text = te.Text & Format(Decimal.Parse(dgvProductos.Rows(i).Cells(11).Value.ToString()), "##,##0.00").PadLeft(13)
        '        Next

        '        te.Text = te.Text & enter & enter
        '        te.Text = te.Text & Me.txtGlosa.Text

        '        te.Text = te.Text & enter & enter & enter & enter & enter
        '        te.Text = te.Text & enter & "          " &
        '        numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00")) - 3)) &
        '                                    " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00")) & " /100 " & Me.cbxTipoMoneda.Text & enter
        '        te.Text = te.Text & "                          S.E.ú.O." & enter & enter

        '        If Me.cbxTipoVenta.SelectedIndex = 1 Then te.Text = te.Text & enter & "                Venta al credito en " & Me.txtCanCuotas & " cuotas de " & Format(Me.txtImpLetra, "##,##0.0") & Format(Me.txtImpLetraME, "##,##0") & " " & Me.cbxTipoMoneda.Text & " " & "1er. Vcto: " & Me.oDataSet.Tables(5).Rows(0).Item(7) & enter & enter & enter

        '        'If Me.cbxTipoVenta.SelectedIndex = 0 Or Me.cbxTipoVenta.SelectedIndex = 2 Or Me.cbxTipoVenta.SelectedIndex = 5 Then te.Text = te.Text & enter & "                Venta al contado, recibo N° " & Me.txtNumRecibo.Text & enter & enter

        '        '-------- 13-08-15
        '        If Me.cbxTipoVenta.SelectedIndex = 0 Or Me.cbxTipoVenta.SelectedIndex = 2 Or Me.cbxTipoVenta.SelectedIndex = 3 Or Me.cbxTipoVenta.SelectedIndex = 4 Or Me.cbxTipoVenta.SelectedIndex = 5 Then
        '            Dim pase As Boolean
        '            te.Text = te.Text & enter & "                Venta al contado, recibo(s) N° S/. "
        '            For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
        '                If matrizRecibos(y, 0) <> "" Then
        '                    If pase <> True Then
        '                        te.Text = te.Text & matrizRecibos(y, 0).PadLeft(12) & " " & matrizRecibos(y, 1).PadLeft(10) & enter
        '                    Else
        '                        te.Text = te.Text & matrizRecibos(y, 0).PadLeft(63) & " " & matrizRecibos(y, 1).PadLeft(10) & enter
        '                    End If
        '                    pase = True
        '                End If
        '            Next y
        '            te.Text = te.Text & enter
        '        End If

        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(103, " ") & enter
        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(103, " ") & enter
        '        ''te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ") & enter
        '        'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(103, " ")
        '    End If

        '    If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        configurarImpresion()
        '        PrintPreviewDialog1.Document = PrintDocument1
        '        PrintPreviewDialog1.ShowDialog()
        '    End If

        '    PrintDialog1.Document = PrintDocument1
        '    If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        configurarImpresion()
        '        PrintDocument1.Print()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
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
                Dim query As String
                query = "exec rpt_Comprobante '" & cbxTipoDocumento.Text & "','" & txtSerieDocumento.Text & "','" & txtNumDocumento.Text & "'"
                Dt = RetornaDataTable(query)
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
        If Me.cbxTipoDocumento.Text = "BV" Then
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 550)
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 580)
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 610)
        Else
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 540)
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 565)
            e.Graphics.DrawString(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 585)
        End If
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
        'prdDocumento.PrinterSettings = ImpresoraActual
        ' Asignamos el tamaño personalizado de papel
        Me.PrintDocument1.DefaultPageSettings.PaperSize = TamañoPersonal
        ' Asignamos los márgenes al documento
        Me.PrintDocument1.DefaultPageSettings.Margins = margenes
    End Sub
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Dim stockActual As Integer
        Dim fechaCierre As String
        Dim sqlString As String
        Dim listaSqlStrings As New ArrayList

        If Me.cbxTipoDocumento.SelectedIndex <> 0 Then
            tipoDocumento = "01"
        Else
            tipoDocumento = "03"
        End If

        If Me.dgvProductos.RowCount <= 0 Then
            MsgBox("Ingrese el número de documento que desea anular   !  !  !", MsgBoxStyle.Exclamation)
            Me.txtNumDocumento.Focus()
            Exit Sub
        End If

        If Me.cbxTipoVenta.SelectedIndex <> 0 And Me.cbxTipoVenta.SelectedIndex <> 1 Then
            MsgBox("Concepto no puede ser procesado en este módulo, utilice anular recibo.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & "\RPTA\R" & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ZIP") = True Then
            MsgBox("Por favor, documento fue enviado y aceptado por la SUNAT, no se puede anular, utilice NOTA CREDITO, para este procedimiento  !  !  !", MsgBoxStyle.Exclamation)
            Me.txtNumDocumento.Focus()
            Exit Sub
        End If

        Try
            fechaCierre = devuelveFecha("select * from cierreDiario")
            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                sqlString = "update vtaCabecera set numGuia='',numLetra='',totVentaMN=0,totVentaME=0,intFinanciero=0,comVendedor=0,cuoInicial=0,status='A' " &
                            "where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & Me.txtNumDocumento.Text & ""
                listaSqlStrings.Add(sqlString)

                sqlString = "update vtaDetalle set precio=0,subTotal=0,status='A' where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & Me.txtNumDocumento.Text & ""
                listaSqlStrings.Add(sqlString)

                If Me.cbxTipoVenta.SelectedIndex = 1 Then
                    sqlString = "update letrasClientes set status='A' where numLetra='" & numLetra & "'"
                    listaSqlStrings.Add(sqlString)
                End If

                sqlString = "update recibosClientes set numDocGenACI='' where numDocGenACI='" & Me.cbxTipoDocumento.Text & "' + '" & Me.txtNumDocumento.Text & "'"
                listaSqlStrings.Add(sqlString)

                sqlString = "update almCabecera set nomOrigen='',dirOrigen='',rucDNI_1='',transLlegada='',status='A' where tipDocumento='SA' and numDocumento=" & CInt(Me.numGuia) & ""
                listaSqlStrings.Add(sqlString)

                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    Dim sqlSaldo As String

                    sqlSaldo = "select * from saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                    stockActual = devuelveStock(sqlSaldo)

                    stockActual = stockActual + dgvProductos.Rows(i).Cells(10).Value

                    sqlString = "update numerosSerie set numDoc='' where  numDoc='" & Trim(Me.numGuia) & "'"
                    listaSqlStrings.Add(sqlString)

                    sqlString = "update saldosAlmacenes set stock=" & stockActual & " where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                    listaSqlStrings.Add(sqlString)
                Next

                If transaccionLetras(listaSqlStrings) Then
                    MsgBox("Documento venta y documentos relacionados, anulados correctamente  !  !  !", MsgBoxStyle.Information)
                    eliminarDocumentoPlano()
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en proceso anulación documento venta   !  !  !", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".CAB")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".DET")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".LEY")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".TRI")
            My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathData & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ACA")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathEnvio & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ZIP") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathEnvio & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".ZIP")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathFirma & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathFirma & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathOridat & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathOridat & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathParse & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathParse & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".PDF") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathRepo & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".PDF")

            If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathTemp & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML") = True Then _
               My.Computer.FileSystem.DeleteFile("\\" & devuelveNameComputer_sfs & pathTemp & ruc_archivoPlano & "-" & tipoDocumento & "-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text & ".XML")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtNumDocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumento.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtNumDocumento.Text = ""
        Me.txtSerieDocumento.Text = "01"
        Me.codigoCliente = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtNumRecibo.Text = ""
        Me.txtGlosa.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.txtCuotas.Text = ""
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.cbxTipoCredito.SelectedIndex = 0
        Me.cbxTipoVenta.SelectedIndex = 0
        Me.lbltotalMN.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.txtCuotas.Enabled = False
        Me.KeyPreview = True
        Me.btnImprimir.Enabled = False
        Me.btnGrabar.Enabled = False
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.dgvProductos.Rows.Clear()
        Me.txtNumDocumento.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class