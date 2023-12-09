Imports System.Data.SqlClient
Public Class frmeditarClientes
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Private Sub frmeditarClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
        oDataSet = New DataSet()

        Try
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT  * from clientes where zona <> -1", Connection)
            daCTaCte.Fill(oDataSet, "clientes")

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "clientes"
            With Me.dgvClientes
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyUp
        oDataSet = New DataSet()

        Try
            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where zona <> -1 and nombres Like '" & "%" & Me.txtCliente.Text & "%" & "'", Connection)
            daClientes.Fill(oDataSet, "clientes")

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "clientes"
            With Me.dgvClientes
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).ReadOnly = True
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
        Dim oFrmaccesoUsuario As New frmaccesoUsuario()

        Try
            oFrmaccesoUsuario.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            For i As Integer = 0 To dgvClientes.Rows.Count - 1
                SqlString = "UPDATE clientes Set nombres='" & dgvClientes.Rows(i).Cells(1).Value & "',direccion ='" &
                dgvClientes.Rows(i).Cells(2).Value & "',ruc ='" & dgvClientes.Rows(i).Cells(3).Value & "',dni ='" &
                dgvClientes.Rows(i).Cells(4).Value & "',telCelular ='" & dgvClientes.Rows(i).Cells(5).Value & "',telFijo ='" &
                dgvClientes.Rows(i).Cells(6).Value & "',dirTrabajo ='" & dgvClientes.Rows(i).Cells(7).Value & "',nomPareja ='" &
                dgvClientes.Rows(i).Cells(8).Value & "',dirPareja ='" & dgvClientes.Rows(i).Cells(9).Value & "',dniPareja ='" &
                dgvClientes.Rows(i).Cells(10).Value & "',celPareja ='" & dgvClientes.Rows(i).Cells(11).Value & "',dirTraPareja ='" &
                dgvClientes.Rows(i).Cells(12).Value & "',zona='" & dgvClientes.Rows(i).Cells(13).Value & "' where idCliente= '" & dgvClientes.Rows(i).Cells(0).Value & "'"

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
            te.Text =
            "                    Comercial Oriente SRL " & enter & enter &
            "Dirección: Jirón Próspero N° 823" & enter &
            "R.U.C.   : 10053952629                         " & "Fecha : " & DateTime.Today() & enter & enter &
            "                    |Listado de Clientes|" & enter
            te.Text = te.Text & enter & "Cod      Nombres        Direccion        RUC/DNI       Tel.Celular" & enter

            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                te.Text = te.Text & enter &
                Trim(Me.dgvClientes.Rows(i).Cells(0).Value.ToString) & "   " &
                Trim(Me.dgvClientes.Rows(i).Cells(1).Value.ToString) & "   " &
                Trim(Me.dgvClientes.Rows(i).Cells(2).Value.ToString) & "   " &
                Trim(Me.dgvClientes.Rows(i).Cells(3).Value.ToString) & "   " &
                Trim(Me.dgvClientes.Rows(i).Cells(4).Value.ToString)
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
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 3 Or columna = 4 Or columna = 10 Or columna = 13 Then
            letra = CShort(Validar_SoloNumeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvClientes_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 7 Or columna = 8 Or columna = 9 Or columna = 12 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvClientes_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        If columna = 1 Then
            DirectCast(e.Control, TextBox).MaxLength = 250
        Else
            If columna = 2 Then
                DirectCast(e.Control, TextBox).MaxLength = 100
            Else
                If columna = 3 Then
                    DirectCast(e.Control, TextBox).MaxLength = 11
                Else
                    If columna = 4 Or columna = 10 Then
                        DirectCast(e.Control, TextBox).MaxLength = 8
                    Else
                        If columna = 5 Or columna = 6 Or columna = 11 Then
                            DirectCast(e.Control, TextBox).MaxLength = 15
                        Else
                            DirectCast(e.Control, TextBox).MaxLength = 80
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Si desea escriba la inicial del apellido para hacer una búsqueda particular."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList
        Dim oFrmaccesoUsuario As New frmaccesoUsuario()

        Try
            If (dgvClientes.SelectedRows.Count <> 0) Then
                MsgBox("Por favor seleccione un cliente!", MsgBoxStyle.Information, "Mensaje")
                Exit Sub
            End If

            oFrmaccesoUsuario.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            SqlString = "UPDATE clientes Set zona=-1 where idCliente= '" & dgvClientes.Rows(dgvClientes.CurrentRow.Index).Cells(0).Value & "'"
            ListSqlStrings.Add(SqlString)

            If transaccionProducto(ListSqlStrings) Then
                MsgBox("Cliente Eliminado correctamente.", MsgBoxStyle.Information)
                flag = 0
                Me.Close()
            Else
                MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class