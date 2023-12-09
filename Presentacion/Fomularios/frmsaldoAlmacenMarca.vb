Imports Microsoft
Imports System.Data.SqlClient
Public Class frmsaldoAlmacenMarca
    Private oDataSet As DataSet
    Private oDataTable As DataTable
    Private oDataRow As DataRow
    Private oDataRowArray() As DataRow
    Private oDataColumn As DataColumn

    Dim texto As New RichTextBox
    Dim item As Integer
    Dim marca As String
    Dim NroPaginasImpresas As Integer = 0
    Private Sub frmsaldoAlmacenMarca_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
        marca = marcaProducto
        btnProcesar_Click(sender, e)
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        oDataSet = New DataSet()

        Try
            Dim daSaldos As SqlDataAdapter = New SqlDataAdapter("SELECT  * from saldosAlmacenes order by idProducto", Connection)
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

            ' Create a new DataTable.
            Dim table As DataTable = New DataTable("saldos1")
            Dim column As DataColumn
            Dim row As DataRow

            ' Create new DataColumn, set DataType and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "idProducto"
            table.Columns.Add(column)

            ' Create new DataColumn, set DataType and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "stock"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "fechaSaldo"
            table.Columns.Add(column)

            ' Create new DataColumn and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "desProducto"
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

            'Add the table to dataset
            oDataSet.Tables.Add(table)

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                If Me.oDataSet.Tables(0).Rows(i).Item(4).ToString.Trim = Me.marca Then
                    row = table.NewRow()
                    row(0) = Me.oDataSet.Tables(0).Rows(i).Item(0) 'idProducto
                    row(1) = Me.oDataSet.Tables(0).Rows(i).Item(1) 'stock
                    row(2) = Microsoft.VisualBasic.Left(Me.oDataSet.Tables(0).Rows(i).Item(2).ToString, 10) 'fechaSaldo
                    row(3) = Me.oDataSet.Tables(0).Rows(i).Item(3) 'descripcionProducto
                    row(4) = Me.oDataSet.Tables(0).Rows(i).Item(4) 'marca
                    row(5) = Me.oDataSet.Tables(0).Rows(i).Item(5) 'modelo
                    table.Rows.Add(row)
                End If
            Next i

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "saldos1"
            With Me.dgvProductos
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DisplayIndex = 4
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).DisplayIndex = 5
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
            oDataRowArray = oDataSet.Tables(2).Select("desProducto Like '" & Me.txtBuscaProducto.Text & "%" & "'")

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
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DisplayIndex = 4
                .Columns(1).DefaultCellStyle.BackColor = Color.GreenYellow
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).DisplayIndex = 5
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

        Dim en, t As Keys
        Dim enter, tab As Char
        en = Keys.Enter
        t = Keys.Tab
        enter = Convert.ToChar(en)
        tab = Convert.ToChar(t)
        texto.Text = "          Comercial Oriente Hnos.SAC" & enter & enter & _
        "Fecha: " & DateTime.Today & enter & _
        "|Núm|Cód|Descripción Productos  | Marca        | Modelo        |Saldo| Fec Saldo |"
        texto.Text = texto.Text & enter

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        ProgressBar1.Maximum = dgvProductos.Rows.Count - 1
        For i As Integer = 0 To Me.dgvProductos.Rows.Count - 1
            texto.Text = texto.Text & CStr(i + 1).ToString.PadLeft(4)
            texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(0).Value.ToString.PadLeft(4) & " "
            texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 20).PadRight(25)
            texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 10).PadRight(15)
            texto.Text = texto.Text & VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 10).PadRight(15)
            texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(1).Value.ToString.PadLeft(6) & " "
            texto.Text = texto.Text & Me.dgvProductos.Rows(i).Cells(2).Value
            texto.Text = texto.Text & enter
            ProgressBar1.PerformStep()
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
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior, _
            AreaImpresion_Ancho, AreaImpresion_Alto)
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
        vistaPrevia("Courier New", 9, texto.Text, e)
        If NroPaginasImpresas > 1 Then
            e.Graphics.DrawString("|Núm|Cód|Descripción Productos  | Marca        | Modelo        |Saldo| Fec Saldo |", Fuente1, Brushes.Black, 50, 50)
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "MyPaper"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 894
        Dim Alto As Short = 1090

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
        Dim ofrmmostrarSeries As New frmmostrarSeries()

        If Me.dgvProductos.Columns(dgvProductos.CurrentCell.ColumnIndex).Index = 0 Then
            flag = 1
            codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
            ofrmkardexProducto.ShowDialog()
            flag = 0
        Else
            If Me.dgvProductos.Columns(dgvProductos.CurrentCell.ColumnIndex).Index = 3 Then
                codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
                ofrmmostrarSeries.ShowDialog()
            End If
        End If
    End Sub
    Private Sub dgvProductos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseEnter
        Me.lblMensaje.Text = "Doble click en columna 'idProducto' para generar el Kardex Gerencial"
        Me.lblMensaje1.Text = "o doble click en columna 'desProducto' para visualizar series o números motor."
    End Sub
    Private Sub dgvProductos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseLeave
        Me.lblMensaje.Text = ""
        Me.lblMensaje1.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class