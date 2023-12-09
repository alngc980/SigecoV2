Imports Microsoft
Imports System.Data.SqlClient
Public Class frmkardexProductoSimple
    Dim texto As New RichTextBox
    Private oDataSet As DataSet
    Private txtSaldo As Integer
    Private flagProcesa As Boolean
    Dim NroPaginasImpresas As Integer = 0
    Dim cantidadDias As Integer
    Private Sub frmkardexProductoSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
    End Sub
    Private Sub txtProducto_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.DoubleClick
        arrayDatos(0) = ""
        frmbuscaProducto.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtProducto.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        If Trim(Me.txtProducto.Text) = "" Then
            MsgBox("No se puede procesar si no indica el producto.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim ds As New DataSet2
        Dim Dt As New DataTable
        Dim Query = "rptKardex " & Trim(Me.txtProducto.Text) & ",'" & dtpFechaInicio.Text & "','" & dtpFechaLimite.Text & "'"
        Dt = RetornaDataTable(Query)
        If Dt.Rows.Count > 0 Then
            For i = 0 To Dt.Rows.Count - 1
                ds.dsKardex.Rows.Add(
                               Dt.Rows(i)(1).ToString(),
                               Dt.Rows(i)(2).ToString(), Dt.Rows(i)(3).ToString(),
                               Dt.Rows(i)(4).ToString(), Dt.Rows(i)(5).ToString(),
                               Dt.Rows(i)(6).ToString(), Dt.Rows(i)(7).ToString(),
                               Dt.Rows(i)(8).ToString(), Dt.Rows(i)(9).ToString(),
                               Dt.Rows(i)(10).ToString(), Dt.Rows(i)(11).ToString(),
                               Dt.Rows(i)(12).ToString())
            Next
        End If

        Dim rpt As New rptKardex
        rpt.SetDataSource(ds.Tables("dsKardex"))

        Dim frm As New frmReporte
        frm.CrystalReportViewer1.ReportSource = rpt
        frm.ShowDialog()


        'flagProcesa = True
        'NroPaginasImpresas = 0

        'Dim Max As Long = 1000
        'Dim z As Long

        'ProgressBar1.Visible = True
        'ProgressBar1.Minimum = 1
        'ProgressBar1.Maximum = Max
        'ProgressBar1.Value = 1
        'ProgressBar1.Step = 1

        'Try
        '    oDataSet = New DataSet()
        '    Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT idProducto,desProducto,preCredito,idGrupo FROM productos where idProducto=" & CInt(txtProducto.Text) & "", Connection)
        '    daProductos.Fill(oDataSet, "productos")

        '    Dim daAlmCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT nomDocumento,tipDocumento,numDocumento,fecOrigen FROM almCabecera where (fecOrigen>='" & Me.dtpFechaInicio.Text & "' and fecOrigen<='" & Me.dtpFechaLimite.Text & "') and status<>'A'", Connection)
        '    daAlmCabecera.Fill(oDataSet, "almCabecera")

        '    If Me.oDataSet.Tables(1).Rows.Count >= 1 Then
        '        For i As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
        '            Dim daAlmDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT nomDocumento,tipDocumento,numDocumento,cantidad FROM almDetalle where nomDocumento='" & Me.oDataSet.Tables(1).Rows(i).Item(0).ToString & "' and tipDocumento='" & Me.oDataSet.Tables(1).Rows(i).Item(1).ToString & "' and numDocumento=" & CInt(Me.oDataSet.Tables(1).Rows(i).Item(2).ToString) & " and idProducto=" & CInt(txtProducto.Text) & "", Connection)
        '            daAlmDetalle.Fill(oDataSet, "almDetalle")
        '        Next
        '    Else
        '        Dim daAlmDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM almDetalle where idProducto=0", Connection)
        '        daAlmDetalle.Fill(oDataSet, "almDetalle")
        '    End If

        '    Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT tipDocumento,serDocumento,numDocumento,numGuia,fecOperacion FROM vtaCabecera where fecOperacion>='" & Me.dtpFechaInicio.Text & "' and fecOperacion<='" & Me.dtpFechaLimite.Text & "'", Connection)
        '    daVtaCabecera.Fill(oDataSet, "vtaCabecera")

        '    Dim daVtaDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT tipDocumento,serDocumento,numDocumento,cantidad,fecOperacion,idProducto FROM vtaDetalle where fecOperacion>='" & Me.dtpFechaInicio.Text & "' and fecOperacion<='" & Me.dtpFechaLimite.Text & "' and idProducto=" & CInt(txtProducto.Text) & "", Connection)
        '    daVtaDetalle.Fill(oDataSet, "vtaDetalle")

        '    Dim daNumerosSerie As SqlDataAdapter = New SqlDataAdapter("SELECT idProducto,numSerie,numMotor,numDoc FROM numerosSerie where idProducto=" & CInt(txtProducto.Text) & "", Connection)
        '    daNumerosSerie.Fill(oDataSet, "numerosSerie")

        '    'Agregamos columnas necesarias a la tabla "almDetalle"
        '    Dim colFechaDoc As DataColumn = New DataColumn()
        '    colFechaDoc.Caption = "fechadoc"
        '    colFechaDoc.ColumnName = "fechadoc"
        '    Me.oDataSet.Tables(2).Columns.Add(colFechaDoc)

        '    Dim colSerieDoc As DataColumn = New DataColumn()
        '    colSerieDoc.Caption = "seriedoc"
        '    colSerieDoc.ColumnName = "seriedoc"
        '    Me.oDataSet.Tables(2).Columns.Add(colSerieDoc)

        '    Dim colDocVenta As DataColumn = New DataColumn()
        '    colDocVenta.Caption = "docVenta"
        '    colDocVenta.ColumnName = "docVenta"
        '    Me.oDataSet.Tables(2).Columns.Add(colDocVenta)

        '    'Agregamos columnas necesarias a la tabla "vtaDetalle"
        '    Dim colNumGuia As DataColumn = New DataColumn()
        '    colNumGuia.Caption = "numGuia"
        '    colNumGuia.ColumnName = "numGuia"
        '    Me.oDataSet.Tables(4).Columns.Add(colNumGuia)

        '    Dim colCodigoGrupo As DataColumn = New DataColumn()
        '    colCodigoGrupo.Caption = "codigoGrupo"
        '    colCodigoGrupo.ColumnName = "codigoGrupo"
        '    Me.oDataSet.Tables(4).Columns.Add(colCodigoGrupo)

        '    Dim colNumSerie As DataColumn = New DataColumn()
        '    colNumSerie.Caption = "numSerie"
        '    colNumSerie.ColumnName = "numSerie"
        '    Me.oDataSet.Tables(4).Columns.Add(colNumSerie)

        '    'Asignando datos a las columnas adicionadas de la tabla "almCabecera" a la tabla "almDetalle"
        '    Dim oDataRow As DataRow
        '    For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
        '        For x As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
        '            If Me.oDataSet.Tables(2).Rows(i).Item(0) = Me.oDataSet.Tables(1).Rows(x).Item(0) And _
        '            Me.oDataSet.Tables(2).Rows(i).Item(1) = Me.oDataSet.Tables(1).Rows(x).Item(1) And _
        '            Me.oDataSet.Tables(2).Rows(i).Item(2) = Me.oDataSet.Tables(1).Rows(x).Item(2) Then
        '                oDataRow = Me.oDataSet.Tables(2).Rows(i)
        '                oDataRow(4) = Me.oDataSet.Tables(1).Rows(x).Item(3)
        '                oDataRow(5) = "01"
        '                Exit For
        '            End If
        '        Next x
        '    Next i

        '    'Asignando datos a las columnas adicionadas de la tabla "vtaCabecera" a la tabla "vtaDetalle"
        '    Dim oDataRow1 As DataRow
        '    For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
        '        For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
        '            If Me.oDataSet.Tables(4).Rows(i).Item(0) = Me.oDataSet.Tables(3).Rows(x).Item(0) And _
        '            Me.oDataSet.Tables(4).Rows(i).Item(2) = Me.oDataSet.Tables(3).Rows(x).Item(2) Then
        '                oDataRow1 = Me.oDataSet.Tables(4).Rows(i)
        '                oDataRow1(6) = Me.oDataSet.Tables(3).Rows(x).Item(3)
        '                Exit For
        '            End If
        '        Next x
        '    Next i

        '    'Asignando datos a las columnas adicionadas de la tabla "productos" a la tabla "vtaDetalle"
        '    Dim oDataRow2 As DataRow
        '    For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
        '        For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
        '            If Me.oDataSet.Tables(4).Rows(i).Item(5) = Me.oDataSet.Tables(0).Rows(x).Item(0) Then
        '                oDataRow2 = Me.oDataSet.Tables(4).Rows(i)
        '                oDataRow2(7) = Me.oDataSet.Tables(0).Rows(x).Item(3)
        '                Exit For
        '            End If
        '        Next x
        '    Next i

        '    'Asignando datos a las columnas adicionadas de la tabla "numerosSerie" a la tabla "vtaDetalle"
        '    Dim oDataRow3 As DataRow
        '    For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
        '        For x As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
        '            If Me.oDataSet.Tables(5).Rows(x).Item(0) = CInt(Me.txtProducto.Text) And _
        '            Me.oDataSet.Tables(5).Rows(x).Item(3) = Me.oDataSet.Tables(4).Rows(i).Item(6) Then
        '                oDataRow3 = Me.oDataSet.Tables(4).Rows(i)
        '                If Me.oDataSet.Tables(4).Rows(i).Item(7) = 6 Then
        '                    oDataRow3(8) = Me.oDataSet.Tables(5).Rows(x).Item(2)
        '                Else
        '                    oDataRow3(8) = Me.oDataSet.Tables(5).Rows(x).Item(1)
        '                End If
        '                Exit For
        '            End If
        '        Next x
        '    Next i

        '    For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
        '        For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
        '            If Me.oDataSet.Tables(2).Rows(i).Item(1) = "SA" And Me.oDataSet.Tables(2).Rows(i).Item(2) = Me.oDataSet.Tables(4).Rows(x).Item(6) Then
        '                oDataSet.Tables(2).Rows(i).Delete()
        '                Exit For
        '            End If
        '        Next x
        '    Next i

        '    oDataSet.Tables(2).AcceptChanges()

        '    For z = 1 To Max
        '        ProgressBar1.PerformStep()
        '        Me.lblAvance.Text = Format(z / Max, "0%")
        '        Application.DoEvents()
        '    Next z

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If flagProcesa <> True Then
            MsgBox("Procese información para imprimir.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        cantidadDias = DateDiff(DateInterval.Day, CDate(Me.dtpFechaInicio.Text), CDate(Me.dtpFechaLimite.Text))

        Try
            Dim totalEntradas, totalSalidas, totalGeneralSalidas As Decimal
            'Dim totalCostoCompras As Decimal
            'Dim totalGeneralCostoCompras As Decimal
            Dim totalVentas As Decimal
            'Dim totalCostoVentas As Decimal
            'Dim totalGeneralCostoVentas As Decimal
            Dim totalSaldo As Decimal
            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)
            texto.Text = enter & _
            txtNombreEmpresa & enter & _
            txtRUCEmpresa & "                                                                 Fecha :" & DateTime.Today & enter & enter & enter
            texto.Text = texto.Text & "                                           KARDEX  SIMPLE" & enter
            texto.Text = texto.Text & "                                    DEL " & Me.dtpFechaInicio.Text & " AL " & Me.dtpFechaLimite.Text & enter
            texto.Text = texto.Text & "Producto: " & txtProducto.Text & " " & Trim(Me.oDataSet.Tables(0).Rows(0).Item(1)) & enter
            texto.Text = texto.Text & "-----------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & "                     Entradas                                Salidas                           Saldo Final " & enter
            texto.Text = texto.Text & "Fecha      Doc.Ser.  Numero  Cant    Fecha      Doc.Ser.  Numero   Guía  Serie Articulo   Cant.            " & enter
            texto.Text = texto.Text & "-----------------------------------------------------------------------------------------------------------" & enter
            totalSaldo += Me.txtSaldo
            For x As Integer = 0 To cantidadDias
                For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    If CDate(Me.oDataSet.Tables(2).Rows(i).Item(4).ToString) = CDate(Me.dtpFechaInicio.Text).AddDays(x) Then
                        If Me.oDataSet.Tables(2).Rows(i).Item(6).ToString = "" Then
                            texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(2).Rows(i).Item(4).ToString, 10).PadLeft(10)
                            texto.Text = texto.Text & Me.oDataSet.Tables(2).Rows(i).Item(1).ToString.PadLeft(4)
                            texto.Text = texto.Text & Me.oDataSet.Tables(2).Rows(i).Item(5).ToString.PadLeft(3)
                            texto.Text = texto.Text & Me.oDataSet.Tables(2).Rows(i).Item(2).ToString.PadLeft(9)
                            If oDataSet.Tables(2).Rows(i).Item(1) = "EN" Then
                                texto.Text = texto.Text & Me.oDataSet.Tables(2).Rows(i).Item(3).ToString.PadLeft(7)
                                totalEntradas += Me.oDataSet.Tables(2).Rows(i).Item(3)
                            Else
                                texto.Text = texto.Text & Me.oDataSet.Tables(2).Rows(i).Item(3).ToString.PadLeft(68)
                                totalSalidas += Me.oDataSet.Tables(2).Rows(i).Item(3)
                            End If
                            texto.Text = texto.Text & enter
                        End If
                    End If
                Next i

                For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If CDate(Me.oDataSet.Tables(4).Rows(i).Item(4).ToString) = CDate(Me.dtpFechaInicio.Text).AddDays(x) Then
                        totalVentas += Me.oDataSet.Tables(4).Rows(i).Item(3)
                        texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(4).Rows(i).Item(4).ToString, 10).PadLeft(47)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(0).ToString.PadLeft(4)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(1).ToString.PadLeft(3)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(2).ToString.PadLeft(9)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(6).ToString.PadLeft(8)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(8).ToString.PadLeft(17)
                        texto.Text = texto.Text & Me.oDataSet.Tables(4).Rows(i).Item(3).ToString.PadLeft(6)
                        texto.Text = texto.Text & enter
                    End If
                Next i
            Next x
            totalGeneralSalidas = totalVentas + totalSalidas
            totalSaldo = totalEntradas - totalGeneralSalidas
            texto.Text = texto.Text & "-----------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & CStr(totalEntradas).PadLeft(33)
            texto.Text = texto.Text & CStr(totalGeneralSalidas).PadLeft(61)
            texto.Text = texto.Text & CStr(totalSaldo).PadLeft(10)

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                'PrintDocument1.DefaultPageSettings.Landscape = True
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
                configurarImpresion()
                'PrintDocument1.DefaultPageSettings.Landscape = True
                PrintDocument1.Print()
            End If
            Me.txtProducto.Text = ""
            Me.flagProcesa = False

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

            ' Comenzamos a contar las paginas impresas
            NroPaginasImpresas += 1

            e.Graphics.MeasureString(Mid(texto.Text, +1), Fuente, _
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
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim Fuente1 As New Font("Courier New", 9)
        VistaPrevia("Courier New", 9, texto.Text, e)
        If NroPaginasImpresas > 1 Then
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 5)
            e.Graphics.DrawString("                     Entradas                                Salidas                           Saldo Final ", Fuente1, Brushes.Black, 0, 20)
            e.Graphics.DrawString("Fecha      Doc.Ser.  Numero  Cant    Fecha      Doc.Ser.  Numero   Guía  Serie Articulo   Cant.            ", Fuente1, Brushes.Black, 0, 35)
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 50)
        End If
        If e.HasMorePages <> False Then
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 1030)
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Ventas"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 894
        Dim Alto As Short = 1090

        Dim left As Short = 0
        Dim top As Short = 65
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
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class