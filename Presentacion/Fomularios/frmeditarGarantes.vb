Imports System.Data.SqlClient
Public Class frmeditarGarantes
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Private Sub frmEditarGarantes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Controls.Add(te)
            te.Multiline = True
            te.Visible = False

            oDataSet = New DataSet()
            Connection.Open()
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT  * from garantes", Connection)
            daCTaCte.Fill(oDataSet, "garantes")

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes", Connection)
            daCliente.Fill(oDataSet, "clientes")
            Connection.Close()

            Dim colNombreCliente As DataColumn = New DataColumn()
            colNombreCliente.AllowDBNull = True
            colNombreCliente.Caption = "nombreCliente"
            colNombreCliente.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(0).Columns.Add(colNombreCliente)

            Dim oDataRowNombreCliente As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowNombreCliente = Me.oDataSet.Tables(0).Rows(i)
                For x As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(0) = Me.oDataSet.Tables(1).Rows(x).Item(0) Then
                        oDataRowNombreCliente(14) = Me.oDataSet.Tables(1).Rows(x).Item(1)
                        Exit For
                    End If
                Next x
            Next i

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "garantes"
            With Me.dgvClientes
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).ReadOnly = True
                .Columns(14).ReadOnly = True
                .Columns(14).DisplayIndex = 1
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub txtGarante_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGarante.KeyUp
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT  * from garantes where nombreGarante Like '" & Me.txtGarante.Text & "%" & "'", Connection)
            daCTaCte.Fill(oDataSet, "garantes")

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes", Connection)
            daCliente.Fill(oDataSet, "clientes")
            Connection.Close()

            Dim colNombreCliente As DataColumn = New DataColumn()
            colNombreCliente.AllowDBNull = True
            colNombreCliente.Caption = "nombreCliente"
            colNombreCliente.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(0).Columns.Add(colNombreCliente)

            Dim oDataRowNombreCliente As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowNombreCliente = Me.oDataSet.Tables(0).Rows(i)
                For x As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(0) = Me.oDataSet.Tables(1).Rows(x).Item(0) Then
                        oDataRowNombreCliente(14) = Me.oDataSet.Tables(1).Rows(x).Item(1)
                        Exit For
                    End If
                Next x
            Next i

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "garantes"
            With Me.dgvClientes
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).ReadOnly = True
                .Columns(14).ReadOnly = True
                .Columns(14).DisplayIndex = 1
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            If MsgBox("Está seguro de modificar datos de los garantes?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                For i As Integer = 0 To dgvClientes.Rows.Count - 1
                    SqlString = "UPDATE garantes Set nombreGarante='" & dgvClientes.Rows(i).Cells(1).Value & "',direccion ='" & _
                    dgvClientes.Rows(i).Cells(2).Value & "',dni ='" & dgvClientes.Rows(i).Cells(3).Value & "',telCelular ='" & _
                    dgvClientes.Rows(i).Cells(4).Value & "',telFijo ='" & dgvClientes.Rows(i).Cells(5).Value & "',dirTrabajo ='" & _
                    dgvClientes.Rows(i).Cells(6).Value & "',nomPareja ='" & dgvClientes.Rows(i).Cells(7).Value & "',dirPareja ='" & _
                    dgvClientes.Rows(i).Cells(8).Value & "',dniPareja ='" & dgvClientes.Rows(i).Cells(9).Value & "',celPareja ='" & _
                    dgvClientes.Rows(i).Cells(10).Value & "',dirTraPareja ='" & dgvClientes.Rows(i).Cells(11).Value & "',zona=" & _
                    dgvClientes.Rows(i).Cells(12).Value & " where idCliente= '" & dgvClientes.Rows(i).Cells(0).Value & "'"
                    ListSqlStrings.Add(SqlString)
                Next
                If transaccionProducto(ListSqlStrings) Then
                    MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    Me.Close()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                    Me.Close()
                End If
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
            te.Text = _
            "                    Comercial Oriente SRL " & enter & enter & _
            "Dirección: Jirón Próspero N° 823" & enter & _
            "R.U.C.   : 10053952629                         " & "Fecha : " & DateTime.Today() & enter & enter & _
            "                    |Listado de Garantes|" & enter
            te.Text = te.Text & enter & "Cod      Nombres        Direccion        RUC/DNI       Tel.Celular" & enter

            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                te.Text = te.Text & enter & _
                Trim(Me.dgvClientes.Rows(i).Cells(0).Value.ToString) & "   " & _
                Trim(Me.dgvClientes.Rows(i).Cells(1).Value.ToString) & "   " & _
                Trim(Me.dgvClientes.Rows(i).Cells(2).Value.ToString) & "   " & _
                Trim(Me.dgvClientes.Rows(i).Cells(3).Value.ToString) & "   " & _
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
        VistaPrevia("Courier New", 10, te.Text, e)
    End Sub
    Private Sub dgvClientes_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        If columna = 3 Or columna = 9 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub dgvClientes_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        If columna = 3 Or columna = 9 Then
            DirectCast(e.Control, TextBox).MaxLength = 11
        End If
    End Sub
    Private Sub dgvClientes1_EditingControlShowing2(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvClientes.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 6 Or columna = 7 Or columna = 8 Or columna = 11 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class