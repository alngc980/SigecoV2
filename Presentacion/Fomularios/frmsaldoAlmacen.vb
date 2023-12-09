Imports Microsoft
Imports System.Data.SqlClient
Public Class frmsaldoAlmacen
    Private oDataSet As DataSet
    Private oDataTable As DataTable
    Private oDataRow As DataRow
    Private oDataRowArray() As DataRow
    Private oDataColumn As DataColumn

    Dim texto As New RichTextBox
    Dim item As Integer
    Dim NroPaginasImpresas As Integer = 0
    Private Sub frmsaldoAlmacen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
        btnProcesar_Click(sender, e)
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        oDataSet = New DataSet()

        Try
            Dim daSaldos As SqlDataAdapter = New SqlDataAdapter("select * from saldosAlmacenes order by idProducto", Connection)
            daSaldos.Fill(oDataSet, "saldos")

            If Me.oDataSet.Tables(0).Rows.Count <= 0 Then
                MsgBox("No existen saldos al " & Me.dtpFechaCierre.Text & ".", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim daProducto As SqlDataAdapter = New SqlDataAdapter("SELECT  * from Productos where stoInicial>=0", Connection)
            daProducto.Fill(oDataSet, "productos")

            Dim colDescripcion As DataColumn = New DataColumn()
            colDescripcion.AllowDBNull = True
            colDescripcion.Caption = "desProducto"
            colDescripcion.ColumnName = "desProducto"
            Me.oDataSet.Tables(0).Columns.Add(colDescripcion)

            Dim colMarca As DataColumn = New DataColumn()
            colMarca.AllowDBNull = True
            colMarca.Caption = "marca"
            colMarca.ColumnName = "marca"
            Me.oDataSet.Tables(0).Columns.Add(colMarca)

            Dim colModelo As DataColumn = New DataColumn()
            colModelo.AllowDBNull = True
            colModelo.Caption = "modelo"
            colModelo.ColumnName = "modelo"
            Me.oDataSet.Tables(0).Columns.Add(colModelo)

            Dim oDataRow As DataRow
            For i As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count() - 1
                For x As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count() - 1
                    If Me.oDataSet.Tables(1).Rows(x).Item(0) = Me.oDataSet.Tables(0).Rows(i).Item(0) Then
                        oDataRow = Me.oDataSet.Tables(0).Rows(i)
                        oDataRow(3) = Me.oDataSet.Tables(1).Rows(x).Item(2)
                        oDataRow(4) = Me.oDataSet.Tables(1).Rows(x).Item(4)
                        oDataRow(5) = Me.oDataSet.Tables(1).Rows(x).Item(5)
                    End If
                Next x
            Next i

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "saldos"
            With Me.dgvProductos
                .Columns(0).Width = 60
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Width = 60
                .Columns(1).DisplayIndex = 4
                .Columns(1).DefaultCellStyle.BackColor = Color.GreenYellow
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).Width = 80
                .Columns(2).DisplayIndex = 5
                .Columns(3).Width = 350
                .Columns(3).DisplayIndex = 1
                .Columns(4).DisplayIndex = 2
                .Columns(5).DisplayIndex = 3
            End With
            Me.btnImprimir.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtBuscaProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaProducto.KeyUp
        oDataTable = New DataTable()

        Try
            oDataRowArray = oDataSet.Tables(0).Select("desProducto Like '" & "%" & Me.txtBuscaProducto.Text & "%'")

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idProducto"
            oDataColumn.ColumnName = "idProducto"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "stock"
            oDataColumn.ColumnName = "stock"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "fechaSaldo"
            oDataColumn.ColumnName = "fechaSaldo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "desProducto"
            oDataColumn.ColumnName = "desProducto"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "marca"
            oDataColumn.ColumnName = "marca"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "modelo"
            oDataColumn.ColumnName = "modelo"
            oDataTable.Columns.Add(oDataColumn)

            For Each row As DataRow In oDataRowArray
                oDataRow = oDataTable.NewRow
                oDataRow("idProducto") = row("idProducto")
                oDataRow("stock") = row("stock")
                oDataRow("fechaSaldo") = Microsoft.VisualBasic.Left(row("fechaSaldo"), 10)
                oDataRow("desProducto") = row("desProducto")
                oDataRow("marca") = row("marca")
                oDataRow("modelo") = row("modelo")
                oDataTable.Rows.Add(oDataRow)
            Next

            Me.dgvProductos.DataSource = oDataTable
            With Me.dgvProductos
                .Columns(0).Width = 60
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Width = 60
                .Columns(1).DisplayIndex = 4
                .Columns(1).DefaultCellStyle.BackColor = Color.GreenYellow
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).Width = 80
                .Columns(2).DisplayIndex = 5
                .Columns(3).Width = 350
                .Columns(3).DisplayIndex = 1
                .Columns(4).DisplayIndex = 2
                .Columns(5).DisplayIndex = 3
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If Me.dgvProductos.RowCount <= 0 Then
            MsgBox("No hay información procesada para imprimir.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        NroPaginasImpresas = 0
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        'ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        Dim en, t As Keys
        Dim enter, tab As Char
        en = Keys.Enter
        t = Keys.Tab

        enter = Convert.ToChar(en)
        tab = Convert.ToChar(t)
        ProgressBar1.Maximum = Me.dgvProductos.Rows.Count - 1

        Try
            texto.Text = "        Comercial Oriente Hnos.SAC" & enter & enter
            texto.Text = texto.Text & "Reporte al: " & DateTime.Today & enter
            texto.Text = texto.Text & "| Núm | Cód |Descripción        |  Marca   |  Modelo  | Saldo | Fec Saldo |"
            texto.Text = texto.Text & enter
            For i As Integer = 0 To dgvProductos.RowCount - 1
                texto.Text = texto.Text & CStr(i + 1).ToString.PadLeft(4)
                texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(0).Value.ToString.PadLeft(4) & " "
                texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 18).PadRight(20)
                texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 8).PadRight(10)
                texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 8).PadRight(10)
                texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(1).Value.ToString.PadLeft(4) & " "
                texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(2).Value
                texto.Text = texto.Text & enter
                ProgressBar1.PerformStep()
                'Me.lblAvance.Text = Format((i + 1) / (Me.oDataSet.Tables(0).Rows.Count() - 1), "0%")
                Application.DoEvents()
            Next i

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            configurarImpresion()
            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
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

            ' Comenzamos a contar las paginas impresas
            NroPaginasImpresas += 1

            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(texto.Text, +1), Fuente, New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, NroLineasRelleno)
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
        vistaPrevia("Courier New", 10, texto.Text, e)
        Dim Fuente1 As New Font("Courier New", 10)
        If NroPaginasImpresas > 1 Then
            e.Graphics.DrawString("|Núm|Cód|Descripción       | Marca   | Modelo  |Saldo| Fec Saldo |", Fuente1, Brushes.Black, 50, 50)
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "MyPaper"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 992
        Dim Alto As Short = 1094

        Dim left As Short = 50
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
        'PrintDocument1.DefaultPageSettings.Landscape = True
    End Sub
    Private Sub dgvProductos_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvProductos.MouseDoubleClick
        Dim ofrmkardexProducto As New frmkardexProducto()
        Dim ofrmmostrarSeries As New frmconsultarSeries()
        Dim ofrmsaldoAlmacenMarca As New frmsaldoAlmacenMarca()

        If Me.dgvProductos.Columns(dgvProductos.CurrentCell.ColumnIndex).Index = 0 Then
            flag = 1
            codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
            ofrmkardexProducto.ShowDialog()
            flag = 0
        Else
            If Me.dgvProductos.Columns(dgvProductos.CurrentCell.ColumnIndex).Index = 3 Then
                codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
                ofrmmostrarSeries.ShowDialog()
            Else
                If Me.dgvProductos.Columns(dgvProductos.CurrentCell.ColumnIndex).Index = 4 Then
                    marcaProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(4).Value.ToString.Trim
                    ofrmsaldoAlmacenMarca.ShowDialog()
                End If
            End If
        End If
    End Sub
    Private Sub dgvProductos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseEnter
        Me.lblMensaje.Text = "Doble click en columna 'idProducto' para generar el Kardex Gerencial, o doble click en columna 'desProducto'"
        Me.lblMensaje1.Text = "para visualizar series o números motor, o doble click en columna 'marca' para visualizar sólo registros de ésta."
    End Sub
    Private Sub dgvProductos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseLeave
        Me.lblMensaje.Text = ""
        Me.lblMensaje1.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class