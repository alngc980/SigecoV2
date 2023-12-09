Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Imports System.IO
Imports System.Drawing.Printing
Public Class frmnotaCreditoInicial
    Dim te As New RichTextBox
    Dim txtStringNumDocumento As String
    Dim codigoCliente, tipoVenta As String
    Dim txtTipoDocumento As String = "NC"
    Dim stockActual As Integer
    Dim totVentaMN, totVentaME As Decimal
    Dim chrConcepto As Char
    Dim arrayMotivos() As String = {"Anulación de la operación", "Anulación por error en el RUC", "Corrección por error en la descripción", "Descuento global",
                                  "Descuento por ítem", "Devolución total", "Devolución por ítem", "Bonificación", "Disminución en el valor", "Otros Conceptos ",
                                  "Ajustes de operaciones de exportación", "Ajustes afectos al IVAP"}
    Dim arrayConceptos() As String = {"V.Contado", "V.Crédito", "V.Crédito", "Cuota Inicial", "Anticipo Cuota Inicial", "V.Tarjeta", "Venta Tarjeta Oferta", "Venta Tarjeta Remate", "Venta Oferta", "Venta Remate"}
    Private oDataSet As DataSet
    Private Sub frmnotaCreditoInicial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Dim strUltimoNumero As String = ("select * from ultimosNumeros where tipDocumento='NC'")
        Dim strCodigoVendedor As String = ("select * from vendedores where idVendedor=1")
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.txtSerieDocumento.Text = "01"
        Me.txtNumNotaCredito.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.txtCodigoVendedor.Text = devuelveCodigo(strCodigoVendedor)

        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoNotaCredito.SelectedIndex = 0

        Me.KeyPreview = True
        Me.txtNumDocumentoVenta.Focus()
    End Sub
    Private Sub frmnotaCreditoInicial_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
    Private Sub cbxTipoDocumento_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbxTipoDocumento.SelectedIndexChanged
        If cbxTipoDocumento.SelectedIndex = 0 Then
            txtSerieDocumento.Text = "BN01"
        Else
            txtSerieDocumento.Text = "FN01"
        End If
    End Sub
    Private Sub cbxTipoNotaCredito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoNotaCredito.SelectedIndexChanged
        Me.txtMotivoNotaCredito.Text = Me.arrayMotivos(Me.cbxTipoNotaCredito.SelectedIndex)
        If cbxTipoNotaCredito.SelectedIndex = 0 Or cbxTipoNotaCredito.SelectedIndex = 1 Or cbxTipoNotaCredito.SelectedIndex = 5 Then
            Me.txtTotalNotaCredito.Text = Me.txtTotalNotaCredito.Text
            Me.txtTotalNotaCredito.ReadOnly = True
        Else
            Me.txtTotalNotaCredito.Text = 0
            Me.txtTotalNotaCredito.ReadOnly = False
        End If
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim oProducto As Producto = New Producto()
        oDataSet = New DataSet()
        Me.dgvDetalles.Rows.Clear()
        Dim Query As String

        If Trim(Me.txtNumDocumentoVenta.Text) = "" Then
            MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
            Me.txtNumDocumentoVenta.Focus()
            Exit Sub
        End If

        Try
            oDataSet = New DataSet()
            Connection.Open()
            Query = "select * from vtaCabecera where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento='" & Me.txtNumDocumentoVenta.Text & "' and rtrim(status)='' and rtrim(statusNC)=''"
            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter(Query, Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento, fue aplicado a una 'Nota Crédito' o está anulado.", MsgBoxStyle.Critical)
                Me.txtNumDocumentoVenta.Text = ""
                Me.txtNumDocumentoVenta.Focus()
                Exit Sub
            End If

            Me.chrConcepto = Me.oDataSet.Tables(0).Rows(0).Item(4)
            'If chrConcepto <> "3" And chrConcepto <> "4" Then
            '    MsgBox("Error, Concepto no válido para esta 'Nota Crédito'.", MsgBoxStyle.Critical)
            '    Me.txtNumDocumentoVenta.Text = ""
            '    Me.txtNumDocumentoVenta.Focus()
            '    Exit Sub
            'End If
            Me.btnAnular.Enabled = False

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(6) & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Me.cbxTipoVenta.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(4)
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(15) - 1
            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(12)

            Me.codigoCliente = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombres.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            If cbxTipoDocumento.SelectedIndex = 1 Then
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)
            Else
                Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(4)
            End If
            Connection.Close()

            Me.dgvDetalles.Rows.Add()
            Me.dgvDetalles.Rows(0).Cells(0).Value = 1
            Me.dgvDetalles.Rows(0).Cells(2).Value = arrayConceptos(Me.oDataSet.Tables(0).Rows(0).Item(4))
            If Me.oDataSet.Tables(0).Rows(0).Item(15) > 1 Then
                Me.dgvDetalles.Rows(0).Cells(1).Value = chrConcepto
                Me.dgvDetalles.Rows(0).Cells(3).Value = Me.oDataSet.Tables(0).Rows(0).Item(9)
                Me.dgvDetalles.Rows(0).Cells(4).Value = 1
                Me.dgvDetalles.Rows(0).Cells(5).Value = Me.oDataSet.Tables(0).Rows(0).Item(9)
                Me.txtTotalNotaCredito.Text = Me.oDataSet.Tables(0).Rows(0).Item(9)
            Else
                Me.dgvDetalles.Rows(0).Cells(1).Value = chrConcepto
                Me.dgvDetalles.Rows(0).Cells(3).Value = Me.oDataSet.Tables(0).Rows(0).Item(8)
                Me.dgvDetalles.Rows(0).Cells(4).Value = 1
                Me.dgvDetalles.Rows(0).Cells(5).Value = Me.oDataSet.Tables(0).Rows(0).Item(8)
                Me.txtTotalNotaCredito.Text = Format(Me.oDataSet.Tables(0).Rows(0).Item(8), "#####0.00")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        'Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        'lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim oProducto As Producto = New Producto()
        Dim sqlString As String
        Dim listaSqlStrings As New ArrayList

        If Trim(Me.txtNumDocumentoVenta.Text) = "" Then
            MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
            Me.txtNumRecibo.Focus()
            Exit Sub
        End If

        Try
            If MsgBox("Está seguro de aplicar nota de crédito a  este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                Me.txtStringNumDocumento = oProducto.stringLetra(Me.cbxTipoDocumento.Text, Me.txtNumDocumentoVenta.Text, "", "")
                sqlString = "insert into notaCreditoCa (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,docReferencia," &
                            "idCliente,totVentaMN,totVentaME,intFinanciero,IGV,fecOperacion,idMoneda,tipCambio,tasInteres,status) VALUES ('" &
                            Me.txtTipoDocumento & "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumNotaCredito.Text & ",'', '" &
                            Me.cbxTipoVenta.SelectedIndex & "','" & Me.txtStringNumDocumento & "'," & Me.codigoCliente & "," &
                            Me.txtTotalNotaCredito.Text & "," & Me.txtTotalNotaCredito.Text & ",0,0,'" &
                            Me.dtpFechaVcmto.Text & "'," & Me.cbxTipoMoneda.SelectedIndex + 1 & ",0,0,' ')"
                listaSqlStrings.Add(sqlString)

                sqlString = "insert into notaCreditoDe (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal," &
                            "afeIGV,fecOperacion,status) VALUES ('" & Me.txtTipoDocumento & "','" & Me.txtSerieDocumento.Text & "'," &
                            Me.txtNumNotaCredito.Text & ",1," & Me.txtTotalNotaCredito.Text & ",1," & Me.txtTotalNotaCredito.Text & ",'', '" &
                            Me.dtpFechaVcmto.Text & "' ,'')"
                listaSqlStrings.Add(sqlString)

                If Me.txtGlosa.Text <> "" Then
                    sqlString = "insert into glosasFacturas (tipDocumento,numDocumento,glosa) VALUES ('" &
                                Me.txtTipoDocumento & "'," & Me.txtNumNotaCredito.Text & ",'" & Me.txtGlosa.Text & "')"
                    listaSqlStrings.Add(sqlString)
                End If

                sqlString = "update ultimosNumeros set numero=" & Me.txtNumNotaCredito.Text & " where tipDocumento= '" & txtTipoDocumento & "'"
                listaSqlStrings.Add(sqlString)

                sqlString = "update vtaCabecera set statusNC='NC" & Me.txtNumNotaCredito.Text & "' where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento='" & Me.txtNumDocumentoVenta.Text & "'"
                listaSqlStrings.Add(sqlString)

                If transaccionLetras(listaSqlStrings) Then
                    MsgBox("'Nota Crédito' ha sido procesado correctamente  !  !  !", MsgBoxStyle.Information)
                    'Me.generarDocumentoPlano()

                    Try
                        Dim ms1 As New System.IO.MemoryStream
                        Dim rep As New ReportesBD
                        AbrirAppQr()
                        Dim imagePath As String = Application.StartupPath + "\QR\" & txtSerieDocumento.Text & "-" & txtNumNotaCredito.Text & ".jpg"

                        ' Verifica si el archivo de imagen existe antes de intentar cargarlo
                        If System.IO.File.Exists(imagePath) Then
                            PictureBox1.Image = Image.FromFile(imagePath)
                            PictureBox1.Image.Save(ms1, PictureBox1.Image.RawFormat)
                            Dim byt() As Byte = ms1.ToArray

                            Dim ds As New DataSet1
                            Dim Dt As New DataTable
                            Dt = RetornaDataTable("rpt_Comprobante 'NC','" & txtSerieDocumento.Text & "','" & txtNumNotaCredito.Text & "'")
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


                    btnLimpiar_Click(sender, e)
                    Me.oDataSet.Tables.Clear()
                    Me.Close()
                Else
                    MsgBox("Error, no se procesó 'Nota Crédito'  !  !  !", MsgBoxStyle.Critical)
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
        Dim tipoDocumento, numDocumento As String
        Dim numTipoDocumento As Byte

        If Me.cbxTipoDocumento.SelectedIndex = 0 Then
            numTipoDocumento = 1
            tipoDocumento = "03"
        Else
            numTipoDocumento = 6
            tipoDocumento = "01"
        End If
        numDocumento = Me.txtDNI.Text

        Try
            If generaDocumentoPLano = True Then
                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & "\data\" & ruc_archivoPlano & "-07-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumNotaCredito.Text & ".NOT", True)
                swEscritor.Write("0101|" & Now.ToString("yyyy-MM-dd") & "|" & VisualBasic.Mid(Date.Now, 12, 8) & "|0000|" & numTipoDocumento & "|" & numDocumento & "|" & Me.txtNombres.Text & "|PEN|" & Me.cbxTipoNotaCredito.SelectedItem & "|" & Me.txtMotivoNotaCredito.Text & "|" & tipoDocumento & "|" & VisualBasic.Left(Me.cbxTipoDocumento.Text, 1) & "001-" & Me.txtNumDocumentoVenta.Text & "|0.00|" & Format(CSng(Me.txtTotalNotaCredito.Text), "#####0.00") & "|" & Format(CSng(Me.txtTotalNotaCredito.Text), "#####0.00") & "|0.00|0.00|0.00|" & Format(CSng(Me.txtTotalNotaCredito.Text), "#####0.00") & "|2.1|2.0|")
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & "\data\" & ruc_archivoPlano & "-07-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumNotaCredito.Text & ".DET", True)
                For x As Integer = 0 To Me.dgvDetalles.Rows.Count - 1
                    swEscritor.WriteLine("NIU|" & Me.dgvDetalles.Rows(x).Cells(4).Value & "|" & Me.dgvDetalles.Rows(x).Cells(1).Value & "|-|" & Me.dgvDetalles.Rows(x).Cells(2).Value & "|" & Format(CSng(Me.dgvDetalles.Rows(x).Cells(3).Value), "#####0.00") & "|0.00|9997|0.00|" & Format(CSng(Me.dgvDetalles.Rows(x).Cells(5).Value), "#####0.00") & "|EXO|VAT|20|0.00|-|0.00|0.00||||0.00|-|0.00|0.00|||0.00|-|0.00|0|||0.00|" & Format(CSng(Me.dgvDetalles.Rows(x).Cells(3).Value), "#####0.00") & "|" & Format(CSng(Me.dgvDetalles.Rows(x).Cells(5).Value), "#####0.00") & "|0.00|")
                Next x
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & "\data\" & ruc_archivoPlano & "-07-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumNotaCredito.Text & ".LEY", True)
                swEscritor.Write("1000|" & numeroLetras(VisualBasic.Left(Format(Single.Parse(Me.txtTotalNotaCredito.Text), "###,##0.00"), Len(Format(Single.Parse(Me.txtTotalNotaCredito.Text), "###,##0.00")) - 3)) & " Y " & obtieneDecimales(Format(Single.Parse(Me.txtTotalNotaCredito.Text), "###,##0.00")) & "/100 Soles|")
                swEscritor.Close()

                swEscritor = New StreamWriter("\\" & devuelveNameComputer_sfs & "\data\" & ruc_archivoPlano & "-07-" & Me.txtSerieDocumento.Text & "-" & Me.txtNumNotaCredito.Text & ".TRI", True)
                swEscritor.WriteLine("9997" & "|EXO|VAT|" & Format(Single.Parse(Me.txtTotalNotaCredito.Text), "#####0.00") & "|0.00|")
                swEscritor.Close()

                sqlString = "update notaCreditoCa set status='@' where numDocumento=" & Me.txtNumNotaCredito.Text & ""
                listaSqlString.Add(sqlString)

                If transaccionLetras(listaSqlString) = True Then
                    MsgBox("Nota de crédito ha generado archivo plano, debe ser enviado individualmente  !  !  !", MsgBoxStyle.Information)
                    Close()
                Else
                    MsgBox("Error en la generación del archivo plano del documento  !  !  !", MsgBoxStyle.Critical)
                End If
            Else
                MsgBox("Nota de crédito no ha generado archivo plano, debe usar resumen de venta para procesar documento  !  !  !", MsgBoxStyle.Information)
                Close()
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
            te.Text = enter & enter & enter & enter & enter & enter & enter &
            "   " & Me.txtNombres.Text & enter & enter & enter &
            "   " & Me.txtDireccion.Text & "                 " & Me.txtDNI.Text & "               " & Me.dtmFecha.Text & enter & enter & enter & enter

            For i As Integer = 0 To Me.dgvDetalles.RowCount - 1
                te.Text = te.Text & enter &
                Me.dgvDetalles.Rows(i).Cells(4).Value.ToString.PadRight(5, " ") &
                Me.dgvDetalles.Rows(i).Cells(1).Value.ToString.PadRight(5, " ") &
                Me.dgvDetalles.Rows(i).Cells(2).Value.ToString.PadRight(50, " ") &
                Me.dgvDetalles.Rows(i).Cells(3).Value.ToString.PadLeft(20, " ") &
                Me.dgvDetalles.Rows(i).Cells(5).Value.ToString.PadLeft(10, " ")
            Next

            te.Text = te.Text & enter & enter & enter & enter & enter & enter & enter & enter
            te.Text = te.Text & enter & "          " &
           numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtDescuento.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtDescuento.Text), "#,###,##0.00")) - 3)) &
                                        " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtDescuento.Text), "###,###0.00")) & "/100 SOLES "

            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "#####0.00").PadLeft(90, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "#####0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtDescuento.Text), "######0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagarME.Text), "######0.00").PadLeft(40, " ")

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
                SqlString = "INSERT INTO notaCreditoCa (tipDocumento,serDocumento,numDocumento,numGuia,tipOperacion,docReferencia,idCliente," &
                "totVentaMN,totVentaME,intFinanciero,IGV,fecOperacion,idMoneda,tipCambio,tasInteres,status) VALUES ('" & Me.txtTipoDocumento &
                "','" & Me.txtSerieDocumento.Text & "'," & Me.txtNumNotaCredito.Text & ",' ',0,' ',1,0,0,0,0,'" & Me.dtpFechaVcmto.Text & "',1,0,0,'A')"

                SqlString1 = "INSERT INTO notaCreditoDe (tipDocumento,serDocumento,numDocumento,idProducto,precio,cantidad,subTotal,afeIGV," &
                             "fecOperacion,status) VALUES ('" & Me.txtTipoDocumento & "' , '" & Me.txtSerieDocumento.Text & "' , " &
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
    Private Sub txtNumDocumento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumentoVenta.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoNotaCredito.SelectedIndex = 0
        Me.txtNumDocumentoVenta.Text = ""
        Me.txtNumRecibo.Text = ""
        Me.txtNumGuia.Text = ""
        Me.dgvDetalles.Rows.Clear()
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtDescuento.Text = 0
        Me.txtTotalNotaCredito.Text = 0
        Me.btnAnular.Enabled = True
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='NC'")
        Me.txtNumNotaCredito.Text = devuelveUltimoNumero(strUltimoNumero) + 1
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class