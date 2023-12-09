Imports Microsoft
Imports System.Data.SqlClient
Public Class frmeditarProductos
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Private Sub frmeditarProductos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
        oDataSet = New DataSet()

        Try
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT  * from Productos where stoInicial>=0", Connection)
            daProductos.Fill(oDataSet, "productos")

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "productos"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Visible = False
                .Columns(3).ReadOnly = True
                .Columns(6).Visible = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).Visible = False
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM productos where stoInicial>=0 and desProducto Like '" & "%" & Me.txtProducto.Text & "%" & "'", Connection)
            daProductos.Fill(oDataSet, "productos")
            Connection.Close()

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "productos"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Visible = False
                .Columns(3).ReadOnly = True
                .Columns(6).Visible = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).Visible = False
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList
        Dim oFrmAcceso As New frmaccesoAdministrador()

        Try
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                SqlString = "UPDATE productos Set idGrupo='" & dgvProductos.Rows(i).Cells(1).Value & "',desProducto ='" &
                dgvProductos.Rows(i).Cells(2).Value & "', marca ='" & dgvProductos.Rows(i).Cells(4).Value & "',modelo ='" &
                dgvProductos.Rows(i).Cells(5).Value & "', preContado ='" & dgvProductos.Rows(i).Cells(7).Value & "',preCredito ='" &
                dgvProductos.Rows(i).Cells(8).Value & "',preTarjeta ='" & dgvProductos.Rows(i).Cells(9).Value & "',afeIGV ='" &
                dgvProductos.Rows(i).Cells(15).Value & "', cCodBarra = '" & dgvProductos.Rows(i).Cells(17).Value.ToString.Trim & "'" &
                " where idProducto= '" & dgvProductos.Rows(i).Cells(0).Value & "'"
                ListSqlStrings.Add(SqlString)
            Next
            If transaccionProducto(ListSqlStrings) Then
                MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                flag = 0
                Me.Close()
            Else
                MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim en, t As Keys
        Dim enter, tab As Char

        Exit Sub

        Try
            en = Keys.Enter
            t = Keys.Tab
            Enter = Convert.ToChar(en)
            TAB = Convert.ToChar(t)
            te.Text = _
            "                    Comercial Oriente Hnos. SAC" & Enter & Enter & _
            "Dirección: Próspero N° 663 - Iquitos" & Enter & _
            "R.U.C.   : 20103855391                       " & "Fecha : " & DateTime.Today() & Enter & Enter & _
            "                    |Listado de Productos|" & Enter
            te.Text = te.Text & Enter & "Cod Descripción           Marca       PreContado  PreCrédito  PreTarjeta" & Enter

            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                te.Text = te.Text & Enter & _
                dgvProductos.Rows(i).Cells(0).Value.ToString.PadRight(3, " ") & _
                dgvProductos.Rows(i).Cells(2).Value.ToString.PadRight(25, " ") & _
                dgvProductos.Rows(i).Cells(4).Value.ToString.PadRight(10, " ") & _
                dgvProductos.Rows(i).Cells(7).Value.ToString.PadLeft(11, " ") & _
                dgvProductos.Rows(i).Cells(8).Value.ToString.PadLeft(11, " ") & _
                dgvProductos.Rows(i).Cells(9).Value.ToString.PadLeft(11, " ")
            Next

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
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
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior, AreaImpresion_Ancho, AreaImpresion_Alto)
            Dim NroLineasImpresion As Integer = CInt(AreaImpresion_Alto / Fuente.Height)
            Dim NroLineasRelleno, NroLetrasLinea As Integer
            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente, New SizeF(AreaImpresion_Ancho, _
            AreaImpresion_Alto), Formato, NroLetrasLinea, NroLineasRelleno)
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
        VistaPrevia("Courier New", 10, te.Text, e)
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 7 Or columna = 8 Or columna = 9 Then
            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvProductos1_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 2 Or columna = 4 Or columna = 5 Or columna = 11 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvProductos_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 11 Then
            DirectCast(e.Control, TextBox).MaxLength = 1
        End If
    End Sub
    Private Sub dgvProductos_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvProductos.MouseDoubleClick
        Dim ofrmmostrarSeries As New frmmostrarSeries()
        codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
        ofrmmostrarSeries.ShowDialog()
    End Sub
    Private Sub txtProducto_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.MouseEnter
        Me.lblMensaje.Text = "Si desea escriba la inicial del producto para hacer una búsqueda incremental."
    End Sub
    Private Sub txtProducto_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Si desea escriba la inicial del producto para hacer una búsqueda incremental."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvProductos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseEnter
        Me.lblMensaje.Text = "Haz doble click en cualquier registro para visualizar series o números de motor correspondiente."
    End Sub
    Private Sub dgvProductos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class