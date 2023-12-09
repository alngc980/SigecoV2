Imports Microsoft
Imports System.Data.SqlClient
Public Class frmkardexMarca
    Dim texto As New RichTextBox
    Private oDataSet As DataSet
    Private txtSaldo As Integer
    Private flagProcesa, flagModelo As Boolean
    Private dia, mes, anno As Int16
    Private Sub frmkardexMarca_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
        Me.cbxAnno.SelectedIndex = 0
        Me.cbxMes.SelectedIndex = 0
        Me.cbxMarca.SelectedIndex = 0
    End Sub
    Private Sub cbxMarca_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxMarca.SelectedIndexChanged
        oDataSet = New DataSet()
        flagModelo = False

        Try
            Connection.Open()
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM productos where marca like '" & Me.cbxMarca.Text & "'", Connection)
            daProductos.Fill(oDataSet, "productos")
            Connection.Close()

            Me.cbxModelo.Items.Clear()
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                Me.cbxModelo.Items.Add(oDataSet.Tables(0).Rows(i).Item(5))
            Next i
            flagModelo = True
            Me.cbxModelo.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cbxModelo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxModelo.SelectedIndexChanged
        'Construyendo fecha de periodo del kardex
        anno = Me.cbxAnno.SelectedItem
        mes = Me.cbxMes.SelectedIndex + 1
        'dia = Date.DaysInMonth(anno, mes)
        dia = 1

        'Construyendo fecha del saldo a buscar en tabla
        Dim fechaSaldo As New DateTime(anno, mes, dia, 0, 0, 0, 0)
        fechaSaldo = fechaSaldo.AddDays(-1)
        anno = fechaSaldo.Year()
        mes = fechaSaldo.Month()
        dia = fechaSaldo.Day()

        oDataSet = New DataSet()
        Try
            If flagModelo <> False Then
                Connection.Open()
                Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM productos where marca like '" & Me.cbxMarca.Text & "' and modelo like '" & Me.cbxModelo.Text & "'", Connection)
                daProductos.Fill(oDataSet, "productos")

                Dim daSaldosHistoricos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM saldosHistoricos where year(fechaSaldo)=" & anno & _
                " and month(fechaSaldo)=" & mes & " and day(fechaSaldo)=" & dia & " and idProducto=" & CInt(Me.oDataSet.Tables(0).Rows(0).Item(0).ToString) & "", Connection)
                daSaldosHistoricos.Fill(oDataSet, "saldosHistoricos")
                Connection.Close()

                If Me.oDataSet.Tables(1).Rows.Count >= 1 Then
                    Me.txtSaldo = Me.oDataSet.Tables(1).Rows(0).Item(1)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        flagProcesa = True
        Dim Max As Long = 1000
        Dim z As Long

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        Try
            oDataSet = New DataSet()
            Connection.Open()

            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT idProducto,marca,modelo,preCredito FROM productos", Connection)
            daProductos.Fill(oDataSet, "productos")

            Dim daAlmCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT nomDocumento,tipDocumento,numDocumento,fecOrigen FROM almCabecera where month(fecOrigen)=" & Me.cbxMes.SelectedIndex + 1 & " and year(fecOrigen)=" & CInt(Me.cbxAnno.Text) & "", Connection)
            daAlmCabecera.Fill(oDataSet, "almCabecera")

            If Me.oDataSet.Tables(1).Rows.Count >= 1 Then
                For i As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
                    Dim daAlmDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT nomDocumento,tipDocumento,numDocumento,idProducto,cantidad FROM almDetalle where nomDocumento='" & Me.oDataSet.Tables(1).Rows(i).Item(0).ToString & "' and tipDocumento='" & Me.oDataSet.Tables(1).Rows(i).Item(1).ToString & "' and numDocumento=" & CInt(Me.oDataSet.Tables(1).Rows(i).Item(2).ToString) & "", Connection)
                    daAlmDetalle.Fill(oDataSet, "almDetalle")
                Next
            Else
                Dim daAlmDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM almDetalle where idProducto=0", Connection)
                daAlmDetalle.Fill(oDataSet, "almDetalle")
            End If

            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT tipDocumento,serDocumento,numDocumento,fecOperacion FROM vtaCabecera where month(fecOperacion)=" & Me.cbxMes.SelectedIndex + 1 & " and year(fecOperacion)=" & CInt(Me.cbxAnno.Text) & "", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            Dim daVtaDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT tipDocumento,serDocumento,numDocumento,idProducto,cantidad,fecOperacion FROM vtaDetalle where month(fecOperacion)=" & Me.cbxMes.SelectedIndex + 1 & " and year(fecOperacion)=" & CInt(Me.cbxAnno.Text) & "", Connection)
            daVtaDetalle.Fill(oDataSet, "vtaDetalle")
            Connection.Close()

            Dim colMarca As DataColumn = New DataColumn()
            colMarca.Caption = "Marca"
            colMarca.ColumnName = "marca"
            Me.oDataSet.Tables(2).Columns.Add(colMarca)

            Dim colModelo As DataColumn = New DataColumn()
            colModelo.Caption = "Modelo"
            colModelo.ColumnName = "modelo"
            Me.oDataSet.Tables(2).Columns.Add(colModelo)

            Dim colFechaDoc As DataColumn = New DataColumn()
            colFechaDoc.Caption = "fechadoc"
            colFechaDoc.ColumnName = "fechadoc"
            Me.oDataSet.Tables(2).Columns.Add(colFechaDoc)

            Dim colSerieDoc As DataColumn = New DataColumn()
            colSerieDoc.Caption = "seriedoc"
            colSerieDoc.ColumnName = "seriedoc"
            Me.oDataSet.Tables(2).Columns.Add(colSerieDoc)

            'Agregando el campo "Marca" a la tabla almDetalle
            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(x).Item(0) = Me.oDataSet.Tables(2).Rows(i).Item(3) Then
                        oDataRow = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow(5) = Me.oDataSet.Tables(0).Rows(x).Item(1)
                        oDataRow(6) = Me.oDataSet.Tables(0).Rows(x).Item(2)
                        Exit For
                    End If
                Next x
            Next i

            'Agregando los campos "fechadoc", "seriedoc" a la tabla almDetalle
            Dim oDataRow2 As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    If Me.oDataSet.Tables(2).Rows.Item(i).Item(0) = Me.oDataSet.Tables(1).Rows.Item(x).Item(0) And _
                    Me.oDataSet.Tables(2).Rows.Item(i).Item(1) = Me.oDataSet.Tables(1).Rows.Item(x).Item(1) And _
                    Me.oDataSet.Tables(2).Rows.Item(i).Item(2) = Me.oDataSet.Tables(1).Rows.Item(x).Item(2) Then
                        oDataRow2 = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow2(7) = Me.oDataSet.Tables(1).Rows.Item(x).Item(3)
                        oDataRow2(8) = "01"
                        Exit For
                    End If
                Next x
            Next i

            ' Create a new DataTable.
            Dim table As DataTable = New DataTable("almDetalle1")
            Dim column As DataColumn
            Dim row As DataRow

            ' Create new DataColumn, set DataType and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "nomDocumento"
            table.Columns.Add(column)

            ' Create new DataColumn, set DataType and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "tipDocumento"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "numDocumento"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "marca"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "modelo"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "cantidad"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "fechadoc"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "seriedoc"
            table.Columns.Add(column)

            'Add the table to dataset
            oDataSet.Tables.Add(table)

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                If Me.oDataSet.Tables(2).Rows.Item(i).Item(5).ToString.Trim = Me.cbxMarca.Text.Trim And _
                   Me.oDataSet.Tables(2).Rows.Item(i).Item(6).ToString.Trim = Me.cbxModelo.Text.Trim Then
                    row = table.NewRow()
                    row(0) = Me.oDataSet.Tables(2).Rows.Item(i).Item(0) 'nom
                    row(1) = Me.oDataSet.Tables(2).Rows.Item(i).Item(1) 'tip
                    row(2) = Me.oDataSet.Tables(2).Rows.Item(i).Item(2) 'num
                    row(3) = Me.oDataSet.Tables(2).Rows.Item(i).Item(5) 'marca
                    row(4) = Me.oDataSet.Tables(2).Rows.Item(i).Item(6) 'modelo
                    row(5) = Me.oDataSet.Tables(2).Rows.Item(i).Item(4) 'cant
                    row(6) = Me.oDataSet.Tables(2).Rows.Item(i).Item(7) 'fecha
                    row(7) = Me.oDataSet.Tables(2).Rows.Item(i).Item(8) 'serie
                    table.Rows.Add(row)
                End If
            Next i

            Dim colMarca1 As DataColumn = New DataColumn()
            colMarca1.Caption = "Marca"
            colMarca1.ColumnName = "marca"
            Me.oDataSet.Tables(4).Columns.Add(colMarca1)

            Dim colModelo1 As DataColumn = New DataColumn()
            colModelo1.Caption = "Modelo"
            colModelo1.ColumnName = "modelo"
            Me.oDataSet.Tables(4).Columns.Add(colModelo1)

            Dim oDataRow1 As DataRow
            For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(x).Item(0) = Me.oDataSet.Tables(4).Rows(i).Item(3) Then
                        oDataRow1 = Me.oDataSet.Tables(4).Rows(i)
                        oDataRow1(6) = Me.oDataSet.Tables(0).Rows(x).Item(1)
                        oDataRow1(7) = Me.oDataSet.Tables(0).Rows(x).Item(2)
                        Exit For
                    End If
                Next x
            Next i

            ' Create a new DataTable.
            Dim table1 As DataTable = New DataTable("vtaDetalle1")
            Dim column1 As DataColumn
            Dim row1 As DataRow

            ' Create new DataColumn, set DataType and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "tipDocumento"
            table1.Columns.Add(column1)

            ' Create new DataColumn, set DataType and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "serDocumento"
            table1.Columns.Add(column1)

            ' Create new DataColumn and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.Int32")
            column1.ColumnName = "numDocumento"
            table1.Columns.Add(column1)

            ' Create new DataColumn and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "marca"
            table1.Columns.Add(column1)

            ' Create new DataColumn and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "modelo"
            table1.Columns.Add(column1)

            ' Create new DataColumn and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.Int32")
            column1.ColumnName = "cantidad"
            table1.Columns.Add(column1)

            ' Create new DataColumn and add to DataTable.    
            column1 = New DataColumn()
            column1.DataType = System.Type.GetType("System.String")
            column1.ColumnName = "fecOperacion"
            table1.Columns.Add(column1)

            'Add the table to dataset
            oDataSet.Tables.Add(table1)

            For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                If Me.oDataSet.Tables(4).Rows.Item(i).Item(6).ToString.Trim = Me.cbxMarca.Text.Trim And _
                   Me.oDataSet.Tables(4).Rows.Item(i).Item(7).ToString.Trim = Me.cbxModelo.Text.Trim Then
                    row1 = table1.NewRow()
                    row1(0) = Me.oDataSet.Tables(4).Rows.Item(i).Item(0) 'tip
                    row1(1) = Me.oDataSet.Tables(4).Rows.Item(i).Item(1) 'ser
                    row1(2) = Me.oDataSet.Tables(4).Rows.Item(i).Item(2) 'num
                    row1(3) = Me.oDataSet.Tables(4).Rows.Item(i).Item(6) 'marca
                    row1(4) = Me.oDataSet.Tables(4).Rows.Item(i).Item(7) 'modelo
                    row1(5) = Me.oDataSet.Tables(4).Rows.Item(i).Item(4) 'cant
                    row1(6) = Me.oDataSet.Tables(4).Rows.Item(i).Item(5) 'fecha
                    table1.Rows.Add(row1)
                End If
            Next i

            For z = 1 To Max
                ProgressBar1.PerformStep()
                Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If flagProcesa <> True Then
            MsgBox("Procese información para imprimir.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            Dim totalEntradas, totalSalidas, totalGeneralSalidas As Decimal
            'Dim totalCostoCompras As Decimal
            'Dim totalGeneralCostoCompras As Decimal
            Dim totalVentas As Decimal
            'Dim totalCostoVentas As Decimal
            'Dim totalGeneralCostoVentas As Decimal
            Dim totalSaldo As Decimal
            Dim spacio As Byte
            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)
            texto.Text = enter & _
            txtNombreEmpresa & enter & _
            txtRUCEmpresa & "                                                                 Fecha :" & DateTime.Today & enter & enter & enter
            texto.Text = texto.Text & "                                         KARDEX  VALORIZADO" & enter
            texto.Text = texto.Text & "                                           " & Me.cbxMes.Text & "-" & Me.cbxAnno.Text & enter
            texto.Text = texto.Text & "Marca producto : " & Me.cbxMarca.Text & enter
            texto.Text = texto.Text & "Modelo producto: " & Me.cbxModelo.Text & enter
            texto.Text = texto.Text & "Saldo       al : " & dia & "/" & mes & "/" & anno & " es : " & Me.txtSaldo & enter
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & "Fecha        Doc. Serie   Numero          Entradas              Salidas            Saldo Final        " & enter
            texto.Text = texto.Text & "                                    Cant.  Costo   Total  Cant. Costo  Total  Cant. Costo    Total    " & enter
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            totalSaldo += Me.txtSaldo
            For i As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
                'totalCostoCompras = Me.oDataSet.Tables(2).Rows(i).Item(2) * Me.oDataSet.Tables(0).Rows(0).Item(2)
                'totalGeneralCostoCompras = totalSaldo * Me.oDataSet.Tables(0).Rows(0).Item(2)
                texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(5).Rows(i).Item(6).ToString, 10).PadLeft(10)
                texto.Text = texto.Text & Me.oDataSet.Tables(5).Rows(i).Item(1).ToString.PadLeft(5)
                texto.Text = texto.Text & Me.oDataSet.Tables(5).Rows(i).Item(7).ToString.PadLeft(6)
                texto.Text = texto.Text & Me.oDataSet.Tables(5).Rows(i).Item(2).ToString.PadLeft(9)
                If Me.oDataSet.Tables(5).Rows(i).Item(1).ToString = "EN" Then
                    texto.Text = texto.Text & Me.oDataSet.Tables(5).Rows(i).Item(5).ToString.PadLeft(10)
                    totalSaldo += Me.oDataSet.Tables(5).Rows(i).Item(5)
                    totalEntradas += Me.oDataSet.Tables(5).Rows(i).Item(5)
                    spacio = 42
                Else
                    texto.Text = texto.Text & Me.oDataSet.Tables(5).Rows(i).Item(5).ToString.PadLeft(32)
                    totalSaldo -= Me.oDataSet.Tables(5).Rows(i).Item(5)
                    totalSalidas += Me.oDataSet.Tables(5).Rows(i).Item(5)
                    spacio = 20
                End If
                'texto.Text = texto.Text & CStr(Format(Me.oDataSet.Tables(0).Rows(0).Item(2), "#0.00")).PadLeft(6)
                'texto.Text = texto.Text & CStr(Format(totalCostoCompras, "#####0.00")).PadLeft(9)
                texto.Text = texto.Text & CStr(Format(totalSaldo, "##,##0")).PadLeft(spacio)
                'texto.Text = texto.Text & CStr(Format(Me.oDataSet.Tables(0).Rows(0).Item(2), "#0.00")).PadLeft(5)
                'texto.Text = texto.Text & CStr(Format(totalGeneralCostoCompras, "#####0.00")).PadLeft(10)
                texto.Text = texto.Text & enter
            Next i

            'totalCompras = totalSaldo - Me.txtSaldo
            For i As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                totalSaldo -= Me.oDataSet.Tables(6).Rows(i).Item(5)
                totalVentas += Me.oDataSet.Tables(6).Rows(i).Item(5)
                'totalCostoVentas = Me.oDataSet.Tables(4).Rows(i).Item(2) * Me.oDataSet.Tables(0).Rows(0).Item(2)
                'totalGeneralCostoVentas = totalSaldo * Me.oDataSet.Tables(0).Rows(0).Item(2)
                texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(6).Rows(i).Item(6).ToString, 10).PadLeft(10)
                texto.Text = texto.Text & Me.oDataSet.Tables(6).Rows(i).Item(0).ToString.PadLeft(5)
                texto.Text = texto.Text & Me.oDataSet.Tables(6).Rows(i).Item(1).ToString.PadLeft(6)
                texto.Text = texto.Text & Me.oDataSet.Tables(6).Rows(i).Item(2).ToString.PadLeft(9)
                texto.Text = texto.Text & Me.oDataSet.Tables(6).Rows(i).Item(5).ToString.PadLeft(32)
                'texto.Text = texto.Text & CStr(Format(Me.oDataSet.Tables(0).Rows(0).Item(2), "#0.00")).PadLeft(6)
                'texto.Text = texto.Text & CStr(Format(totalCostoVentas, "####0.00")).PadLeft(8)
                texto.Text = texto.Text & CStr(Format(totalSaldo, "##,##0")).PadLeft(20)
                'texto.Text = texto.Text & CStr(Format(Me.oDataSet.Tables(0).Rows(0).Item(2), "#0.00")).PadLeft(5)
                'texto.Text = texto.Text & CStr(Format(totalGeneralCostoVentas, "#####0.00")).PadLeft(10)
                texto.Text = texto.Text & enter
            Next i
            totalGeneralSalidas = totalVentas + totalSalidas
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & CStr(totalEntradas).PadLeft(40)
            texto.Text = texto.Text & CStr(totalGeneralSalidas).PadLeft(22)

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
            'Me.txtProducto.Text = ""
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
        VistaPrevia("Courier New", 9, texto.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Ventas"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 894
        Dim Alto As Short = 1090

        Dim left As Short = 0
        Dim top As Short = 5
        Dim bottom As Short = 5
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