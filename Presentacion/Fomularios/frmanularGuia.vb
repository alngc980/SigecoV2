Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmanularGuia
    Dim te As New RichTextBox
    Dim codigo, tipoDocumento, moneda, numLetra, fecVencimiento As String
    Dim numeDocumento, item As Integer
    Dim numComprobante, tipoComprobante, numRecibo As String
    Dim totalComprobante, valorCuota, inicial As Decimal
    Dim tipoVenta, tipoMoneda, numCuotas As Byte
    Dim arraySerie(10, 2) As String
    Dim arraySimbolo() As String = {"S/.", "$", "€"}
    Private oDataSet As DataSet
    Private Sub frmanularGuia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMovimiento.SelectedIndex = 1
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.txtSerieDocumento.Text = "01"
        Me.KeyPreview = True
        Me.btnGrabar.Enabled = False
        Me.btnImprimir.Enabled = False
        If flag = 1 And fecDocumento = Date.Today Then
            Me.btnBuscar.Enabled = False
            Me.btnAnular.Enabled = False
            Me.btnImprimir.Enabled = True
            Me.btnLimpiar.Enabled = False
            Me.txtNumDocumento.Text = numDocumento
            Me.cbxTipoDocumento.Text = tipDocumento
            Me.cbxTipoMovimiento.Text = tipMovimiento
            Me.btnBuscar_Click(sender, e)
        Else
            If flag = 1 And fecDocumento <> Date.Today Then
                Me.btnBuscar.Enabled = False
                Me.btnAnular.Enabled = False
                Me.btnImprimir.Enabled = True
                Me.btnLimpiar.Enabled = False
                Me.txtNumDocumento.Text = numDocumento
                Me.cbxTipoDocumento.Text = tipDocumento
                Me.cbxTipoMovimiento.Text = tipMovimiento
                Me.btnBuscar_Click(sender, e)
            End If
        End If
    End Sub
    Private Sub frmboletaVenta_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim oProducto As Producto = New Producto()
        Me.dgvProductos.Rows.Clear()

        Try
            If Me.txtNumDocumento.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()

            Dim daAlmCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT  *from almCabecera where nomDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and tipDocumento='" & Me.cbxTipoMovimiento.Text & "' and numDocumento='" & Me.txtNumDocumento.Text & "' and status<>'A' ", Connection)
            daAlmCabecera.Fill(oDataSet, "almCabecera")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Me.dtpFecOrigen.Text = Me.oDataSet.Tables(0).Rows(0).Item(4)
            If Me.dtpFecOrigen.Text <> Date.Today Then
                Me.btnAnular.Enabled = False
            Else
                Me.btnAnular.Enabled = True
            End If

            If Me.cbxTipoMovimiento.SelectedIndex = 0 Then
                Dim daProveedor As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM proveedores where idProveedor='" & Me.oDataSet.Tables(0).Rows(0).Item(3) & "' ", Connection)
                daProveedor.Fill(oDataSet, "proveedores")

                Me.codigo = Me.oDataSet.Tables(1).Rows(0).Item(0)
                Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
                Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
                Me.txtDNIRUC.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            Else
                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(9) & "' ", Connection)
                daCliente.Fill(oDataSet, "clientes")

                Me.codigo = Me.oDataSet.Tables(1).Rows(0).Item(0)
                Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
                Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
                Me.txtDNIRUC.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            End If

            Me.txtGarantia.Text = Me.oDataSet.Tables(0).Rows(0).Item(10)

            Dim daAlmDetalle = New SqlDataAdapter("SELECT *from almDetalle where nomDocumento='" & Me.cbxTipoDocumento.Text & "' and tipDocumento='" & Me.cbxTipoMovimiento.Text & "' and numDocumento='" & Me.txtNumDocumento.Text & "' order by idProducto asc", Connection)
            daAlmDetalle.Fill(oDataSet, "almDetalle")

            Dim colNombreProducto As DataColumn = New DataColumn()
            colNombreProducto.Caption = "Descripción Producto"
            colNombreProducto.ColumnName = "descripcionProducto"
            Me.oDataSet.Tables(2).Columns.Add(colNombreProducto)

            Dim colMarcaProducto As DataColumn = New DataColumn()
            colMarcaProducto.Caption = "Marca"
            colMarcaProducto.ColumnName = "marca"
            Me.oDataSet.Tables(2).Columns.Add(colMarcaProducto)

            Dim colModeloProducto As DataColumn = New DataColumn()
            colModeloProducto.Caption = "Modelo"
            colModeloProducto.ColumnName = "modelo"
            Me.oDataSet.Tables(2).Columns.Add(colModeloProducto)

            Dim colNumSerie As DataColumn = New DataColumn()
            colNumSerie.Caption = "Número Serie"
            colNumSerie.ColumnName = "numeroSerie"
            Me.oDataSet.Tables(2).Columns.Add(colNumSerie)

            Dim colNumMotor As DataColumn = New DataColumn()
            colNumMotor.Caption = "Número Motor"
            colNumMotor.ColumnName = "numeroMotor"
            Me.oDataSet.Tables(2).Columns.Add(colNumMotor)

            Dim colNumChasis As DataColumn = New DataColumn()
            colNumChasis.Caption = "Número Chásis"
            colNumChasis.ColumnName = "numeroChasis"
            Me.oDataSet.Tables(2).Columns.Add(colNumChasis)

            Dim colColor As DataColumn = New DataColumn()
            colColor.Caption = "Color"
            colColor.ColumnName = "color"
            Me.oDataSet.Tables(2).Columns.Add(colColor)

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT *from productos where idProducto='" & Me.oDataSet.Tables(2).Rows(i).Item(3) & "' order by idProducto", Connection)
                daProductos.Fill(oDataSet, "productos")
            Next

            Dim daNumerosSerie As SqlDataAdapter
            If Me.oDataSet.Tables(2).Rows(0).Item(1).ToString() = "SA" Then
                daNumerosSerie = New SqlDataAdapter("SELECT *from numerosSerie where numDoc='" & Me.txtNumDocumento.Text & "' order by idProducto", Connection)
            Else
                daNumerosSerie = New SqlDataAdapter("SELECT *from numerosSerie where numDoc1='" & Me.txtNumDocumento.Text & "' order by idProducto", Connection)
            End If
            daNumerosSerie.Fill(oDataSet, "numerosSerie")

            If Me.cbxTipoMovimiento.Text = "SA" Then
                Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM vtaCabecera where idCliente='" & Me.codigo & "' and numGuia='" & Me.txtNumDocumento.Text & "' ", Connection)
                daVtaCabecera.Fill(oDataSet, "vtaCabecera")
                If Me.oDataSet.Tables("vtaCabecera").Rows.Count >= 1 Then
                    tipoComprobante = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(0)
                    numComprobante = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(2)
                    tipoVenta = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(4)
                    numLetra = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(5)
                    totalComprobante = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(8)
                    inicial = Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(14)
                    moneda = arraySimbolo(Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(15) - 1)

                    If tipoVenta = 0 Or tipoVenta = 2 Then
                        Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM recibosClientes where idCliente='" & Me.codigo & "' and numDocGenACI='" & Me.tipoComprobante & Me.numComprobante & "' ", Connection)
                        daRecibos.Fill(oDataSet, "recibos")
                        numRecibo = Me.oDataSet.Tables("recibos").Rows(0).Item(0)
                    Else
                        Dim daLetras As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM letrasClientes where idCliente='" & Me.codigo & "' and numLetra='" & Me.numLetra & "' ", Connection)
                        daLetras.Fill(oDataSet, "letras")
                        numCuotas = Me.oDataSet.Tables("letras").Rows.Count
                        If Me.oDataSet.Tables("vtaCabecera").Rows(0).Item(15) > 1 Then
                            valorCuota = Me.oDataSet.Tables("letras").Rows(0).Item(5)
                        Else
                            valorCuota = Me.oDataSet.Tables("letras").Rows(0).Item(4)
                        End If
                        fecVencimiento = Me.oDataSet.Tables("letras").Rows(0).Item(7)
                    End If
                End If
            End If

            Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM glosasFacturas where nomDocumento='" & Me.cbxTipoDocumento.Text & "' and tipDocumento='" & Me.cbxTipoMovimiento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & "", Connection)
            daGlosas.Fill(oDataSet, "glosas")
            Connection.Close()

            If Me.oDataSet.Tables("glosas").Rows.Count >= 1 Then Me.txtGlosa.Text = Me.oDataSet.Tables("glosas").Rows(0).Item(2)

            ' Create a new DataTable.
            Dim table As DataTable = New DataTable("almDetalle1")
            Dim column As DataColumn
            Dim oDataRow As DataRow

            ' Create news DataColumns, set DataType and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "idProducto"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "cantidad"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "desProducto"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "marca"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "modelo"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "numSerie"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "numMotor"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "numChasis"
            table.Columns.Add(column)

            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "color"
            table.Columns.Add(column)

            'Add the table to dataset
            oDataSet.Tables.Add(table)

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                oDataRow = table.NewRow()
                For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                    If Me.oDataSet.Tables(2).Rows(i).Item(3) = Me.oDataSet.Tables(3).Rows(x).Item(0) Then
                        oDataRow(0) = Me.oDataSet.Tables(3).Rows(x).Item(0) 'idProducto
                        oDataRow(1) = Me.oDataSet.Tables(2).Rows(i).Item(4) 'cantidad
                        oDataRow(2) = Me.oDataSet.Tables(3).Rows(x).Item(2) 'desProducto
                        oDataRow(3) = Me.oDataSet.Tables(3).Rows(x).Item(4) 'marca
                        oDataRow(4) = Me.oDataSet.Tables(3).Rows(x).Item(5) 'modelo
                    End If
                Next x

                Dim otraVez As Boolean = False
                For y As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If Me.oDataSet.Tables(2).Rows(i).Item(3) = Me.oDataSet.Tables(4).Rows(y).Item(0) Then
                        If otraVez = True Then
                            oDataRow = table.NewRow()
                        End If
                        oDataRow(5) = Me.oDataSet.Tables(4).Rows(y).Item(1) 'numSerie
                        oDataRow(6) = Me.oDataSet.Tables(4).Rows(y).Item(2) 'numMotor
                        oDataRow(7) = Me.oDataSet.Tables(4).Rows(y).Item(3) 'numChasis
                        oDataRow(8) = Me.oDataSet.Tables(4).Rows(y).Item(6) 'color
                        If otraVez = False Then table.Rows.Add(oDataRow)
                        If otraVez = True Then table.Rows.Add(oDataRow)
                        otraVez = True
                    End If
                Next y
            Next i

            For x As Integer = 0 To oDataSet.Tables("almDetalle1").Rows.Count() - 1
                item = item + 1
                Me.dgvProductos.Rows.Add()
                Me.dgvProductos.Rows(x).Cells(0).Value = Me.item
                Me.dgvProductos.Rows(x).Cells(1).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(0)
                Me.dgvProductos.Rows(x).Cells(2).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(2)
                Me.dgvProductos.Rows(x).Cells(3).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(3)
                Me.dgvProductos.Rows(x).Cells(4).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(4)
                Me.dgvProductos.Rows(x).Cells(5).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(5)
                Me.dgvProductos.Rows(x).Cells(6).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(6)
                Me.dgvProductos.Rows(x).Cells(7).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(7)
                Me.dgvProductos.Rows(x).Cells(8).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(8)
                Me.dgvProductos.Rows(x).Cells(9).Value = Me.oDataSet.Tables("almDetalle1").Rows(x).Item(1)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
            numDocumento = 0
            tipMovimiento = ""
            flag = 0
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'Try
        '    Dim en, t As Keys
        '    Dim enter, tab As Char
        '    en = Keys.Enter
        '    t = Keys.Tab
        '    enter = Convert.ToChar(en)
        '    tab = Convert.ToChar(t)
        '    te.Clear()

        '    If cbxTipoMovimiento.Text = "EN" Then
        '        te.Text = te.Text & (Me.txtSerieDocumento.Text & "-" & Me.txtNumDocumento.Text).ToString.PadLeft(100) & enter
        '        te.Text = te.Text & "                                         " & Me.dtpFecOrigen.Text & "                       " & Me.txtNombre.Text & enter & enter & enter & enter & enter
        '        'te.Text = te.Text & "CANT. DESCRIPCION          MARCA         MODELO          NUMERO SERIE   NUMERO MOTOR      NUMERO CHASIS      COLOR      ".PadLeft(103) & enter
        '        For i As Integer = 0 To Me.dgvProductos.RowCount - 1
        '            te.Text = te.Text & dgvProductos.Rows(i).Cells(9).Value.ToString.PadRight(6)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(2).Value.ToString, 20).PadRight(21)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 21).PadRight(23)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 25).PadRight(26)
        '            'te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(6).Value.ToString, 25).PadRight(26)
        '            'te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(7).Value.ToString, 18).PadRight(19)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(8).Value.ToString, 6).PadRight(7)
        '            te.Text = te.Text & enter
        '        Next
        '    Else 'If cbxTipoMovimiento.Text = "SA" Then
        '        te.Text = te.Text & enter & enter & enter & enter & enter & enter
        '        te.Text = te.Text & "                                                      " & Me.dtpFecOrigen.Text & enter & enter & enter & enter
        '        te.Text = te.Text & "                     " & Me.txtNombre.Text & enter
        '        te.Text = te.Text & "                " & Me.txtDNIRUC.Text & enter
        '        te.Text = te.Text & "Dirección:" & Me.txtDireccion.Text & enter & enter & enter & enter & enter & enter & enter & enter & enter & enter
        '        'te.Text = te.Text & Me.txtGarantia.Text & enter & enter & enter & enter & enter

        '        'te.Text = te.Text & CANT. DESCRIPCION          MARCA           MODELO          NUMERO SERIE  NUMERO MOTOR      NUMERO CHASIS      COLOR      ".PadLeft(103) & enter
        '        For i As Integer = 0 To Me.dgvProductos.RowCount - 1
        '            te.Text = te.Text & dgvProductos.Rows(i).Cells(9).Value.ToString.PadRight(6)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(2).Value.ToString, 20).PadRight(21)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 21).PadRight(23)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 15).PadRight(16)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 25).PadRight(26)
        '            'te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(6).Value.ToString, 25).PadRight(26)
        '            'te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(7).Value.ToString, 18).PadRight(19)
        '            te.Text = te.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(8).Value.ToString, 6).PadRight(7)
        '            te.Text = te.Text & enter
        '        Next
        '    End If
        '    te.Text = te.Text & enter & enter
        '    te.Text = te.Text & Me.txtGlosa.Text & enter
        '    te.Text = te.Text & "Número Recibo:" & numRecibo

        '    If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        configurarImpresion()
        '        PrintPreviewDialog1.Document = PrintDocument1
        '        PrintPreviewDialog1.ShowDialog()
        '    End If

        '    PrintDialog1.Document = PrintDocument1
        '    If PrintDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
        '        configurarImpresion()
        '        PrintDocument1.Print()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        Dim dsGuia As New dsGuiaRemision
        Dim dt As New DataTable

        dt = RetornaDataTable("EXEC rptGuia 'GX','" & txtSerieDocumento.Text & "','" & txtNumDocumento.Text & "'")

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                dsGuia.DataTable1.Rows.Add(
                       dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString(),
                       dt.Rows(0)(2).ToString(), dt.Rows(0)(3).ToString(),
                       dt.Rows(0)(4).ToString(), dt.Rows(0)(5).ToString(),
                       dt.Rows(0)(6).ToString(), dt.Rows(0)(7).ToString(),
                       dt.Rows(0)(8).ToString(), dt.Rows(0)(9).ToString(),
                       dt.Rows(0)(10).ToString(), dt.Rows(0)(11).ToString(),
                       dt.Rows(0)(12).ToString(), dt.Rows(0)(13).ToString(),
                       dt.Rows(0)(14).ToString(), dt.Rows(0)(15).ToString(),
                       dt.Rows(0)(16).ToString(), dt.Rows(0)(17).ToString(),
                       dt.Rows(0)(18).ToString(), dt.Rows(0)(19).ToString(),
                       dt.Rows(0)(20).ToString(), dt.Rows(0)(21).ToString(),
                       dt.Rows(0)(22).ToString(), dt.Rows(0)(23).ToString(),
                       dt.Rows(0)(24).ToString())
            Next
        End If

        Dim rpt As New rptGuia
        rpt.SetDataSource(dsGuia.Tables("DataTable1"))

        Dim frm As New frmReporte
        frm.CrystalReportViewer1.ReportSource = rpt
        frm.ShowDialog()

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
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim Fuente As New Font("Courier New", 9)

        VistaPrevia("Courier New", 9, te.Text, e)
        If Me.cbxTipoMovimiento.Text = "SA" Then
            If tipoVenta = 0 Or tipoVenta = 2 Then
                'e.Graphics.DrawString(tipoComprobante & " " & numComprobante & " Contado  " & moneda & " " & Format(totalComprobante, "###,##0.00") & " Recibo N° " & numRecibo, Fuente, Brushes.Black, 0, 500)
                e.Graphics.DrawString(tipoComprobante & " " & numComprobante & " Contado  " & moneda & " " & Format(totalComprobante, "###,##0.00"), Fuente, Brushes.Black, 0, 500)
            Else
                e.Graphics.DrawString(tipoComprobante & " " & numComprobante & " Credito  " & moneda & " " & Format(totalComprobante, "###,##0.00") & "   " & inicial & "       " & numCuotas & "    " & valorCuota & "  1er. Vcto: " & fecVencimiento, Fuente, Brushes.Black, 0, 500)
            End If
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 1024
        Dim Alto As Short = 827

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
        oDataSet = New DataSet()
        Try
            If Me.txtNumDocumento.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.codigo = "" Then
                MsgBox("Primero busque los datos del documento, luego anule.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.cbxTipoMovimiento.Text <> "EN" Then
                MsgBox("Solicitud invalida. No se puede anular 'Salidas Almacén' por este módulo, utilice módulo 'Anular Documento Venta'.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim stockActual As Integer
            Dim fechaCierre As String
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim SqlString2 As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim ListSqlStrings1 As New ArrayList
            Dim ListSqlStrings2 As New ArrayList

            fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'SqlString = "UPDATE almCabecera set nomOrigen='',dirOrigen='',rucDNI_1='',idCliente=" & CInt(Me.codigo) & _
                '             ",transLlegada=''," & "status='A' where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & ""

                SqlString = "DELETE from almDetalle where nomDocumento='" & Me.cbxTipoDocumento.Text & "' and tipDocumento='" & _
                Me.cbxTipoMovimiento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & ""
                ListSqlStrings.Add(SqlString)

                SqlString = "DELETE from almCabecera where nomDocumento='" & Me.cbxTipoDocumento.Text & "' and tipDocumento='" & _
                Me.cbxTipoMovimiento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & ""
                ListSqlStrings.Add(SqlString)

                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    Dim sqlSaldo As String

                    sqlSaldo = "SELECT * FROM saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                    stockActual = devuelveStock(sqlSaldo)

                    stockActual = stockActual - dgvProductos.Rows(i).Cells(8).Value

                    SqlString1 = "DELETE from numerosSerie where numDoc1='" & Trim(Me.txtNumDocumento.Text) & "'"
                    SqlString2 = "UPDATE saldosAlmacenes Set stock=" & stockActual & " where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"

                    ListSqlStrings1.Add(SqlString1)
                    ListSqlStrings2.Add(SqlString2)
                Next

                If transaccionAnulacionGuias(ListSqlStrings, ListSqlStrings1, ListSqlStrings2) Then
                    MsgBox("Documento anulado correctamente.", MsgBoxStyle.Information)
                    actualizaNumItem()
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se anuló documento.", MsgBoxStyle.Critical)
                End If
            End If
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
        Me.codigo = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNIRUC.Text = ""
        Me.txtNumDocumento.Text = ""
        Me.cbxTipoMovimiento.SelectedIndex = 1
        Me.dgvProductos.Rows.Clear()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class