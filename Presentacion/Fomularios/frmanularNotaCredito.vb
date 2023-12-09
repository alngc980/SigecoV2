Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmanularNotaCredito
    Dim te As New RichTextBox
    Dim codigoCliente, tipoVenta, tipDocumento, numLetra, numRecibo, numGuia, numGuiaOriginal, numReciboAdelanto As String
    Dim item, nDocumento As Integer
    Dim arrayConceptos() As String = {"V.Contado", "V.Crédito", "V.Tarjeta", "Cuota Inicial", "Anticipo Cuota", "Otros"}
    Private oDataSet As DataSet
    Private Sub frmanularNotaCredito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Me.txtSerieDocumento.Text = "01"
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtTotalPagarME.Text = 0
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.cbxCanCuotas.Enabled = False
        Me.KeyPreview = True
        If flag = 1 And fecDocumento = Date.Today Then
            Me.btnBuscar.Enabled = False
            Me.btnImprimir.Enabled = True 'No debe permitir imprimir documentos que no sean del día
            Me.cbxTipoDocumento.Text = tipMovimiento
            Me.txtNumNotaCredito.Text = numDocumento
            Me.btnBuscar_Click(sender, e)
        Else
            If flag = 1 And fecDocumento <> Date.Today Then
                Me.btnBuscar.Enabled = False
                Me.cbxTipoDocumento.Text = tipMovimiento
                Me.txtNumNotaCredito.Text = numDocumento
                Me.btnBuscar_Click(sender, e)
            End If
        End If
    End Sub
    Private Sub frmanularNotaCredito_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
                        btnImprimir_Click(sender, e)
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
        Dim sumaSubTotales, totVentaMN, totVentaME As Decimal
        Dim oProducto As Producto = New Producto()
        Me.dgvProductos.Rows.Clear()

        Try
            If Me.txtNumNotaCredito.Text = "" Then
                MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()
            Dim daNotaCreditoCa As SqlDataAdapter = New SqlDataAdapter("SELECT  *from notaCreditoCa where tipDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and numDocumento='" & Me.txtNumNotaCredito.Text & "' and status<>'A' ", Connection)
            daNotaCreditoCa.Fill(oDataSet, "notaCreditoCa")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado.", MsgBoxStyle.Critical)
                Me.txtNumNotaCredito.Text = ""
                Exit Sub
            End If

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(6) & "'", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daGlosasFacturas As SqlDataAdapter = New SqlDataAdapter("SELECT  *from glosasFacturas where tipDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and numDocumento='" & Me.txtNumNotaCredito.Text & "'", Connection)
            daGlosasFacturas.Fill(oDataSet, "glosasFacturas")

            Dim daNotaCreditoDe As SqlDataAdapter = New SqlDataAdapter("SELECT  *from notaCreditoDe where tipDocumento='" & Me.cbxTipoDocumento.Text & _
           "' and numDocumento='" & Me.txtNumNotaCredito.Text & "'", Connection)
            daNotaCreditoDe.Fill(oDataSet, "notaCreditoDe")

            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT *from productos where idProducto='" & Me.oDataSet.Tables(3).Rows(i).Item(3) & "'", Connection)
                daProductos.Fill(oDataSet, "productos")
            Next

            If Me.oDataSet.Tables(0).Rows(0).Item(4) <> 3 And Me.oDataSet.Tables(0).Rows(0).Item(4) <> 4 Then
                Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT  *from vtaCabecera where statusNC='NC" & Me.txtNumNotaCredito.Text & "'", Connection)
                daVtaCabecera.Fill(oDataSet, "vtaCabecera")

                Dim daLetrasClientes As SqlDataAdapter = New SqlDataAdapter("SELECT  *from letrasClientes where numLetra='" & Me.oDataSet.Tables(5).Rows(0).Item(5).ToString & "' and statusNC<>''", Connection)
                daLetrasClientes.Fill(oDataSet, "letrasClientes")
            End If
            Connection.Close()

            Me.cbxTipoVenta.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(4).ToString
            Me.txtNumDocumento.Text = Me.oDataSet.Tables(0).Rows(0).Item(5).ToString
            Me.tipDocumento = Mid(Me.oDataSet.Tables(0).Rows(0).Item(5).ToString, 1, 2)
            Me.nDocumento = Mid(Me.oDataSet.Tables(0).Rows(0).Item(5).ToString, 3, Me.oDataSet.Tables(0).Rows(0).Item(5).ToString.Length - 2)

            Dim colDescripcion As DataColumn = New DataColumn()
            colDescripcion.Caption = "Descripción"
            colDescripcion.ColumnName = "descripcion"
            Me.oDataSet.Tables(3).Columns.Add(colDescripcion)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If Me.oDataSet.Tables(4).Rows(x).Item(0) = Me.oDataSet.Tables(3).Rows(i).Item(3) Then
                        oDataRow = Me.oDataSet.Tables(3).Rows(i)
                        If Me.cbxTipoVenta.SelectedIndex <> 5 Then
                            oDataRow(10) = Me.oDataSet.Tables(4).Rows(x).Item(2)
                        Else
                            oDataRow(10) = "FACTURACION OTROS CONCEPTOS"
                        End If
                    End If
                Next x
            Next i

            totVentaMN = Me.oDataSet.Tables(0).Rows(0).Item(7)
            totVentaME = Me.oDataSet.Tables(0).Rows(0).Item(8)

            Me.codigoCliente = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombre.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            If Me.oDataSet.Tables(2).Rows.Count >= 1 Then Me.txtGlosa.Text = Me.oDataSet.Tables(2).Rows(0).Item(2)
            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(11)
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(12) - 1

            If Me.oDataSet.Tables(0).Rows(0).Item(4) = "3" Or Me.oDataSet.Tables(0).Rows(0).Item(4) = "4" Then
                item = item + 1
                Me.dgvProductos.Rows.Add()
                Me.dgvProductos.Rows(0).Cells(0).Value = Me.item
                Me.dgvProductos.Rows(0).Cells(1).Value = ""
                Me.dgvProductos.Rows(0).Cells(2).Value = arrayConceptos(Me.oDataSet.Tables(0).Rows(0).Item(4))
                Me.dgvProductos.Rows(0).Cells(3).Value = ""
                Me.dgvProductos.Rows(0).Cells(4).Value = ""
                Me.dgvProductos.Rows(0).Cells(5).Value = Me.oDataSet.Tables(0).Rows(0).Item(7)
            Else
                For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                    item = item + 1
                    Me.dgvProductos.Rows.Add()
                    Me.dgvProductos.Rows(x).Cells(0).Value = Me.item
                    Me.dgvProductos.Rows(x).Cells(1).Value = Me.oDataSet.Tables(3).Rows(x).Item(3)
                    Me.dgvProductos.Rows(x).Cells(2).Value = Me.oDataSet.Tables(3).Rows(x).Item(10)
                    Me.dgvProductos.Rows(x).Cells(3).Value = Me.oDataSet.Tables(3).Rows(x).Item(4)
                    Me.dgvProductos.Rows(x).Cells(4).Value = Me.oDataSet.Tables(3).Rows(x).Item(5)
                    Me.dgvProductos.Rows(x).Cells(5).Value = Me.oDataSet.Tables(3).Rows(x).Item(6)
                    sumaSubTotales += Val(Me.dgvProductos.Rows(x).Cells(5).Value)
                Next
            End If

            numGuia = Me.oDataSet.Tables(0).Rows(0).Item(3).ToString
            numRecibo = "NC" & Trim(Me.txtNumNotaCredito.Text)
            If Me.oDataSet.Tables(0).Rows(0).Item(4) <> 3 And Me.oDataSet.Tables(0).Rows(0).Item(4) <> 4 Then
                numLetra = Me.oDataSet.Tables(5).Rows(0).Item(5).ToString
                numGuiaOriginal = Me.oDataSet.Tables(5).Rows(0).Item(3).ToString
                If Me.oDataSet.Tables(6).Rows.Count >= 1 Then numReciboAdelanto = VisualBasic.Mid(Me.oDataSet.Tables(6).Rows(0).Item(13).ToString, 2, Me.oDataSet.Tables(6).Rows(0).Item(13).ToString.Length - 1)
            End If

            Me.txtSubTotal.Text = Format(sumaSubTotales, "###,##0.00")
            Me.txtInteres.Text = Me.oDataSet.Tables(0).Rows(0).Item(9)
            Me.txtIGV.Text = Me.oDataSet.Tables(0).Rows(0).Item(10)

            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                Me.txtTotalPagar.Text = Format(totVentaME, "###,##0.00")
            Else
                Me.txtTotalPagar.Text = Format(totVentaMN, "###,##0.00")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            'Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        'Try
        '    Dim en, t As Keys
        '    Dim enter, tab As Char
        '    en = Keys.Enter
        '    t = Keys.Tab
        '    enter = Convert.ToChar(en)
        '    tab = Convert.ToChar(t)
        '    te.Text = enter & enter & enter & enter & enter & enter & enter & _
        '    "   " & Me.txtNombre.Text & enter & enter & enter & _
        '    "   " & Me.txtDireccion.Text & "                 " & Me.txtDNI.Text & "               " & Me.dtmFecha.Text & enter & enter & enter & enter

        '    For i As Integer = 0 To Me.dgvProductos.RowCount - 1
        '        te.Text = te.Text & enter & _
        '        Me.dgvProductos.Rows(i).Cells(4).Value.ToString.PadRight(5, " ") & _
        '        Me.dgvProductos.Rows(i).Cells(1).Value.ToString.PadRight(5, " ") & _
        '        Me.dgvProductos.Rows(i).Cells(2).Value.ToString.PadRight(50, " ") & _
        '        Me.dgvProductos.Rows(i).Cells(3).Value.ToString.PadLeft(20, " ") & _
        '        Me.dgvProductos.Rows(i).Cells(5).Value.ToString.PadLeft(10, " ")
        '    Next

        '    te.Text = te.Text & enter & enter & enter & enter & enter & enter & enter & enter
        '    te.Text = te.Text & enter & "          " & _
        '   numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00")) - 3)) & _
        '                                " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,###0.00")) & "/100 NUEVOS SOLES "

        '    te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "#####0.00").PadLeft(90, " ")
        '    te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "#####0.00").PadLeft(90, " ")
        '    'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ")
        '    te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagar.Text), "######0.00").PadLeft(90, " ")
        '    'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagarME.Text), "######0.00").PadLeft(40, " ")

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

            Dim Serie, Correl As String

            Dim data As DataTable
            data = RetornaDataTable("select serDocumento, numDocumento from notaCreditoCa where docReferencia = '" & txtNumDocumento.Text & "' and serDocumento in ('BN01','FN01')")
            If data.Rows.Count > 0 Then
                Serie = data.Rows(0)(0).ToString
                Correl = data.Rows(0)(1).ToString
            Else
                MsgBox("No se encontró la venta relacionada a la nota de credito")
                Exit Sub
            End If

            Dim imagePath As String = Application.StartupPath + "\QR\" & Serie & "-" & Correl & ".jpg"

            ' Verifica si el archivo de imagen existe antes de intentar cargarlo
            If System.IO.File.Exists(imagePath) Then
                PictureBox1.Image = Image.FromFile(imagePath)
                PictureBox1.Image.Save(ms1, PictureBox1.Image.RawFormat)
                Dim byt() As Byte = ms1.ToArray

                Dim ds As New DataSet1
                Dim Dt As New DataTable


                Dt = RetornaDataTable("rpt_Comprobante 'NC','" & Serie & "','" & Correl & "'")
                If Dt.Rows.Count > 0 Then
                    For i = 0 To Dt.Rows.Count - 1
                        ds.DataTable1.Rows.Add(byt,
                                   Dt.Rows(0)(0).ToString(), Dt.Rows(0)(1).ToString(),
                                   Dt.Rows(0)(2).ToString(), Dt.Rows(0)(3).ToString(),
                                   Dt.Rows(0)(4).ToString(), Dt.Rows(0)(5).ToString(),
                                   Dt.Rows(0)(6).ToString(), Dt.Rows(0)(7).ToString(),
                                   Dt.Rows(0)(8).ToString(), Dt.Rows(0)(9).ToString(),
                                   Dt.Rows(0)(10).ToString(), Dt.Rows(0)(11).ToString(),
                                   Dt.Rows(0)(12).ToString(), Dt.Rows(0)(13).ToString(),
                                   Dt.Rows(0)(14).ToString(), Dt.Rows(0)(15).ToString(),
                                   Dt.Rows(0)(16).ToString())
                    Next
                End If

                Dim rpt As New rptNotaCred
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
        Dim Fuente As New Font("Courier New", 9)

        VistaPrevia("Courier New", 9, te.Text, e)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 540)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 565)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 585)
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
        Try

            Dim stockActual As Integer
            Dim fechaCierre As String
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim ListSqlStrings1 As New ArrayList
            Dim ListSqlStrings2 As New ArrayList

            fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")

            If Me.txtNumNotaCredito.Text = "" Then
                MsgBox("Ingrese número documento para eliminar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'SqlString = "UPDATE notaCreditoCa SET docReferencia=' ',totVentaMN=0,totVentaME=0,intFinanciero=0,status='A' where numDocumento=" & CInt(Me.txtNumNotaCredito.Text) & ""
                'ListSqlStrings.Add(SqlString)

                SqlString = "DELETE from notaCreditoDe where numDocumento=" & CInt(Me.txtNumNotaCredito.Text) & ""
                ListSqlStrings.Add(SqlString)

                SqlString = "DELETE from notaCreditoCa where numDocumento=" & CInt(Me.txtNumNotaCredito.Text) & ""
                ListSqlStrings.Add(SqlString)

                If Me.cbxTipoVenta.SelectedIndex <> 3 And Me.cbxTipoVenta.SelectedIndex <> 4 Then

                    If Me.cbxTipoVenta.SelectedIndex = 1 Then
                        SqlString = "UPDATE letrasClientes set fecPago='01/01/1900',numRecibo=' ',status=' ' where (numLetra='" & numLetra & "' and rtrim(numRecibo)='" & numRecibo & "')"
                        ListSqlStrings.Add(SqlString)

                        SqlString = "UPDATE letrasClientes set numRecibo='" & numReciboAdelanto & "',status='A',statusNC=' ' where (numLetra='" & numLetra & "' and statusNC='A" & numReciboAdelanto & "')"
                        ListSqlStrings.Add(SqlString)

                    Else
                        SqlString = "Select * from letrasClientes "
                        ListSqlStrings.Add(SqlString)
                    End If

                    'SqlString = "UPDATE almCabecera set nomOrigen='',dirOrigen='',rucDNI_1='',idCliente=" & CInt(Me.codigo) & _
                    '             ",transLlegada=''," & "status='A' where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & CInt(Me.txtNumDocumento.Text) & ""
                    'ListSqlStrings.Add(SqlString)

                    SqlString = "DELETE from almDetalle where nomDocumento='PD' and tipDocumento='EN' and numDocumento=" & numGuia & ""
                    ListSqlStrings.Add(SqlString)

                    SqlString = "DELETE from almCabecera where nomDocumento='PD' and tipDocumento='EN' and numDocumento=" & numGuia & ""
                    ListSqlStrings.Add(SqlString)

                    For i As Integer = 0 To dgvProductos.Rows.Count - 1
                        Dim sqlSaldo As String

                        sqlSaldo = "SELECT * FROM saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                        stockActual = devuelveStock(sqlSaldo)

                        stockActual = stockActual - dgvProductos.Rows(i).Cells(4).Value

                        SqlString = "UPDATE numerosSerie set numDoc='" & numGuiaOriginal & "',sExtorno=' ' where sExtorno='NC" & Trim(Me.txtNumNotaCredito.Text) & "'"
                        ListSqlStrings1.Add(SqlString)

                        SqlString = "UPDATE saldosAlmacenes Set stock=" & stockActual & " where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                        ListSqlStrings1.Add(SqlString)
                    Next
                Else
                    SqlString = "Select * from letrasClientes "
                    ListSqlStrings1.Add(SqlString)
                End If

                SqlString = "UPDATE vtaCabecera SET statusNC=' ' where tipDocumento='" & Me.tipDocumento & "' and numDocumento=" & Me.nDocumento & ""
                ListSqlStrings2.Add(SqlString)

                SqlString = "DELETE from glosasFacturas where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & CInt(Me.txtNumNotaCredito.Text) & ""
                ListSqlStrings2.Add(SqlString)

                If transaccionAnulacionGuias(ListSqlStrings, ListSqlStrings1, ListSqlStrings2) Then
                    MsgBox("Documento anulado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                    Me.Close()
                Else
                    MsgBox("Error en el proceso, no se anuló documento.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtNumNotaCredito_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumNotaCredito.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtSerieDocumento.Text = "01"
        Me.txtNumNotaCredito.Text = ""
        Me.codigoCliente = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtGlosa.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.cbxCanCuotas.Enabled = False
        Me.KeyPreview = True
        Me.btnImprimir.Enabled = False
        Me.btnProcesar.Enabled = False
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.dgvProductos.Rows.Clear()
        Me.txtNumNotaCredito.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class