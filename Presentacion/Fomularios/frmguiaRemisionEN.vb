Imports System.Data.SqlClient
Imports Libreria
Public Class frmguiaRemisionEN
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim txtGrupo As Integer
    Dim ultimaCantidad As Integer
    Dim txtStatus As String = "+"
    Dim txtCodigoProveedor As String
    Dim txtCodigoProducto As Integer
    Dim i, item, ultimoNumero As Integer
    Private Sub frmguiaRemisionEN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.txtNomCliente.Text = txtNombreEmpresa
        Me.txtDirCliente.Text = txtDireccionEmpresa
        Me.txtDocCliente.Text = Mid(txtRUCEmpresa, 6, 12)
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMovimiento.SelectedIndex = 0
        For i As Integer = matrizSeries.GetLowerBound(0) To matrizSeries.GetUpperBound(0)
            matrizSeries(i, 0) = " " : matrizSeries(i, 1) = " " : matrizSeries(i, 2) = " "
            matrizSeries(i, 3) = " " : matrizSeries(i, 4) = " " : matrizSeries(i, 5) = " "
        Next
        y = 0
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True
    End Sub
    Private Sub btnBuscarProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarProveedor.Click

        If Me.txtNumDocumento.Text = "" Then
            MsgBox("Ingrese número y serie documento para continuar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Me.buscarCodigo(Trim(Me.txtNumDocumento.Text)) >= 1 Then
            MsgBox("Número documento ya existe.", MsgBoxStyle.Exclamation)
            Me.txtNumDocumento.Text = ""
            Exit Sub
        End If

        arrayDatos(0) = ""
        frmbuscaProveedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoProveedor = arrayDatos(0)
            Me.txtNombre.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNIRUC.Text = arrayDatos(3)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
        End If
    End Sub
    Private Sub btnNuevoProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoProveedor.Click

        If Me.txtNumDocumento.Text = "" Then
            MsgBox("Ingrese número documento y serie para continuar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Me.buscarCodigo(Trim(Me.txtNumDocumento.Text)) >= 1 Then
            MsgBox("Número documento ya está registrado en el sistema.", MsgBoxStyle.Exclamation)
            Me.txtNumDocumento.Text = ""
            Exit Sub
        End If

        arrayDatos(0) = ""
        frmnuevoProveedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoProveedor = arrayDatos(0)
            Me.txtNombre.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNIRUC.Text = arrayDatos(3)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
        End If
    End Sub
    Private Sub frmguiaRemisionEN_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            btnProducto_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                btnGrabar_Click(sender, e)
            Else
                If e.KeyCode = Keys.F8 Then
                    btnAnular_Click(sender, e)
                Else
                    If e.KeyCode = Keys.F10 Then
                        btnImprimir_Click(sender, e)
                    Else
                        If e.KeyCode = Keys.F12 Then
                            btnLimpiar_Click(sender, e)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        Try
            If Me.dgvProductos.Rows.Count > 0 Then
                If ultimaCantidad = 0 Then
                    MsgBox("Cantidad de producto no puede ser cero.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            flag = 0
            If Me.txtCodigoProveedor <> "" And Me.txtDNIRUC.Text <> "" Then
                arrayDatos(0) = ""
                frmbuscaProducto.ShowDialog()
                If arrayDatos(0) <> "" Then
                    Dim yaEsta As Boolean = False
                    For x As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                        If Me.dgvProductos.Rows(x).Cells(1).Value = arrayDatos(0) Then
                            yaEsta = True
                            Exit For
                        End If
                    Next
                    If yaEsta = True Then
                        MsgBox("Este item ya fue registrado.", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                    item += 1
                    Me.dgvProductos.Rows.Add()
                    Me.i = Me.dgvProductos.RowCount - 1
                    Me.dgvProductos.Rows(i).Cells(0).Value = item
                    Me.dgvProductos.Rows(i).Cells(1).Value = arrayDatos(0)
                    Me.txtGrupo = arrayDatos(1)
                    Me.dgvProductos.Rows(i).Cells(2).Value = arrayDatos(2)
                    Me.dgvProductos.Rows(i).Cells(3).Value = arrayDatos(3)
                    Me.dgvProductos.Rows(i).Cells(4).Value = arrayDatos(4)
                    Me.dgvProductos.Rows(i).Cells(8).Value = 0
                    Me.dgvProductos.Focus()
                    Me.dgvProductos.CurrentCell = dgvProductos.Rows(i).Cells(8)
                    arrayDatos(0) = "" : arrayDatos(2) = "" : arrayDatos(3) = "" : arrayDatos(4) = ""
                    Me.ultimaCantidad = Me.dgvProductos.Rows(i).Cells(8).Value
                    flag = 1
                End If
            Else
                MsgBox("Por favor, faltan datos del proveedor.", MsgBoxStyle.Information)
                Me.btnBuscarProveedor.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellValueChanged
        Dim ofrmnumerosSerie As New frmingresarSeries()
        Try
            If (dgvProductos.Columns(e.ColumnIndex).Name = "cantidad") And flag <> 0 Then
                If Val(Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value) > 0 Then

                    If MsgBox("Crear números de serie de este producto?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        flagString = "GR"
                        codigoProducto = Me.dgvProductos.Rows(i).Cells(1).Value
                        codigoGrupo = txtGrupo
                        canNumSeries = Me.dgvProductos.Rows(i).Cells(8).Value
                        ofrmnumerosSerie.ShowDialog()
                        flagString = ""
                        codigoProducto = 0
                        codigoGrupo = 0
                        canNumSeries = 0
                    End If

                End If
                Me.ultimaCantidad = Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            If dgvProductos.Rows.Count <= 0 Then
                MsgBox("No hay informacion para guardar.", MsgBoxStyle.Information)
                Exit Sub
            End If

            For i As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                If Me.dgvProductos.Rows(i).Cells(8).Value = 0 Then
                    MsgBox("No puede haber cantidad de producto igual a cero.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            Next

            Dim oProducto As Producto = New Producto()
            Dim stockActual As Integer
            Dim fechaCierre As String
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim SqlString2 As String = ""
            Dim SqlString3 As String = ""
            Dim SqlString4 As String = ""

            Dim ListSqlStrings1 As New ArrayList
            Dim ListSqlStrings2 As New ArrayList
            Dim ListSqlStrings3 As New ArrayList

            fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")

            SqlString = "INSERT INTO almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor,fecOrigen,nomOrigen,dirOrigen,rucDNI_1," & _
                        "fecLlegada,idCliente,transLlegada,status) VALUES ('" & _
                        Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & "," & _
                        Me.txtCodigoProveedor & ",'" & Me.dtpFecOrigen.Text & "' ,'" & Me.txtNombre.Text & "','" & Me.txtDireccion.Text & "','" & _
                        Me.txtDNIRUC.Text & "','" & Me.dtpFecLlegada.Text & "',1,'" & Me.txtTransportista.Text & "','" & Me.txtStatus & "' )"

            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                SqlString1 = "INSERT INTO almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad,status) VALUES ('" & _
                             Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",'" & _
                             dgvProductos.Rows(i).Cells(1).Value & "' ," & dgvProductos.Rows(i).Cells(8).Value & ",'0')"

                Dim sqlSaldo, sqlCodigo As String
                sqlSaldo = "select * from saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                stockActual = devuelveStock(sqlSaldo)

                sqlCodigo = "select * from saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"

                If verificarDocumento(sqlCodigo) > 0 Then
                    stockActual = stockActual + Me.dgvProductos.Rows(i).Cells(8).Value
                    SqlString2 = "UPDATE saldosAlmacenes Set stock='" & stockActual & "' where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                Else
                    SqlString2 = "INSERT INTO saldosAlmacenes (idProducto,stock,fechaSaldo) VALUES (" & _
                    dgvProductos.Rows(i).Cells(1).Value & " ,'" & Me.dgvProductos.Rows(i).Cells(8).Value + "' ,'" & CDate(fechaCierre) & "' )"
                End If
                ListSqlStrings1.Add(SqlString1)
                ListSqlStrings2.Add(SqlString2)
            Next

            Dim daSeries As SqlDataAdapter = New SqlDataAdapter("select * from numerosSerie where idProducto ='" & CInt(matrizSeries(0, 0)) & "'", Connection)
            oDataSet = New DataSet()
            Dim numero As Integer

            Connection.Open()
            daSeries.Fill(oDataSet, "numerosSerie")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count > 0 Then numero = Me.oDataSet.Tables(0).Rows(Me.oDataSet.Tables(0).Rows().Count() - 1).Item(8)

            For i As Integer = 0 To y - 1
                SqlString4 = "INSERT INTO numerosSerie (idProducto,numSerie,numMotor,numChasis,numDoc,numDoc1,color,anoFab,numItem) VALUES ( " & _
                             Val(matrizSeries(i, 0)) & ",'" & matrizSeries(i, 1) & "','" & matrizSeries(i, 2) & "','" & matrizSeries(i, 3) & "',' ','" & _
                             Me.txtNumDocumento.Text & "','" & matrizSeries(i, 4) & "','" & matrizSeries(i, 5) & "', " & numero + i + 1 & ")"
                ListSqlStrings3.Add(SqlString4)
            Next

            If Me.cbxTipoDocumento.Text = "PD" Then
                SqlString3 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumDocumento.Text & " where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and tipMovimiento='" & Me.cbxTipoMovimiento.Text & "'"
            Else
                SqlString3 = "select * from ultimosNumeros"
            End If

            If flag = 1 Then
                If transaccionAlmacen(SqlString, SqlString3, ListSqlStrings1, ListSqlStrings2, ListSqlStrings3) Then
                    MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    flag = 0
                    Me.btnLimpiar_Click(sender, e)
                Else
                    MsgBox("La Información no se guardó.", MsgBoxStyle.Critical)
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
            If Me.cbxTipoDocumento.SelectedIndex = 0 Then 'Guía Remisión
                te.Text = _
                Me.txtNomCliente.Text & enter & _
                Me.txtDirCliente.Text & enter & _
                Me.txtDocCliente.Text & enter & _
                "Tipo Documento: " & Me.cbxTipoMovimiento.Text & "                     " & Me.dtpFecOrigen.Text & enter & enter
                For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                    te.Text = te.Text & enter & _
                    Me.dgvProductos.Rows(i).Cells(1).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(3).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(4).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(5).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(6).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(7).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(8).Value & "   "
                Next
            Else ' Parte Diario

                te.Text = _
                Me.txtNomCliente.Text & enter & _
                Me.txtDirCliente.Text & enter & _
                Me.txtDocCliente.Text & enter & _
                "Tipo Documento: " & Me.cbxTipoMovimiento.Text & "                     " & Me.dtpFecOrigen.Text & enter & enter
                For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                    te.Text = te.Text & enter & _
                    Me.dgvProductos.Rows(i).Cells(1).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(3).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(4).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(5).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(6).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(7).Value & "   " & _
                    Me.dgvProductos.Rows(i).Cells(8).Value & "   "
                Next
            End If


            'Format(Me.dgvProductos.Rows(i).Cells(3).Value, "#,##0.00") & Space(10 - Len(Me.dgvProductos.Rows(i).Cells(3).Value)) & "   " & _
            'Format(Me.dgvProductos.Rows(i).Cells(7).Value, "##,##0.00") & Space(10 - Len(Me.dgvProductos.Rows(i).Cells(7).Value)) & "   " & _

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
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Try
            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim SqlString2 As String = ""
            Dim ListSqlStrings As New ArrayList

            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                SqlString = "INSERT INTO almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor," & _
                "fecOrigen,nomOrigen,dirOrigen,rucDNI_1,fecLlegada,idCliente,transLlegada,status) VALUES ('" & _
                Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",1,'" & _
                Me.dtpFecOrigen.Text & "',' ',' ',' ',' ',1,' ','A' )"

                SqlString1 = "INSERT INTO almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad) VALUES ('" & _
                Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",1,0)"

                SqlString2 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumDocumento.Text & " where tipoDocumento= '" & Me.cbxTipoDocumento.Text & "'"

                ListSqlStrings.Add(SqlString)
                ListSqlStrings.Add(SqlString1)
                ListSqlStrings.Add(SqlString2)

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Documento anulado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("No se anuló documento.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellContentClick
        Me.item = 0
        Dim oProducto As Producto = New Producto()

        If dgvProductos.Columns(e.ColumnIndex).Name = "eliminar" Then
            Try
                dgvProductos.Rows.RemoveAt(e.RowIndex)
                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    item += 1
                    dgvProductos.Rows(i).Cells(0).Value = item
                Next
                ultimaCantidad = 1
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 8 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub dgvProductos1_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 5 Or columna = 6 Or columna = 7 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub txtNumDocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumento.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Function buscarCodigo(ByVal codigo As String) As Byte
        Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM almCabecera where nomDocumento='" & Me.cbxTipoDocumento.Text & "' and tipDocumento='EN' and numDocumento Like '" & Me.txtNumDocumento.Text & "'", Connection)
        oDataSet = New DataSet()
        Try
            Connection.Open()
            daProductos.Fill(oDataSet, "productos")
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return Me.oDataSet.Tables(0).Rows.Count()
    End Function
    Private Sub cbxTipoDocumento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipoDocumento.SelectedIndexChanged
        If Me.cbxTipoDocumento.SelectedIndex = 1 Then
            Dim strUltimoNumero As String = ("SELECT *FROM ultimosNumeros where tipDocumento='PD' and tipMovimiento='EN'")
            Me.txtSerie.Text = "01"
            Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1
            Me.txtNumDocumento.Enabled = False
        Else
            Me.txtSerie.Text = ""
            Me.txtNumDocumento.Text = ""
            Me.txtNumDocumento.Enabled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNIRUC.Text = ""
        Me.txtTransportista.Text = ""
        Me.txtNumDocumento.Enabled = True
        Me.txtSerie.Text = ""
        Me.txtNumDocumento.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.dgvProductos.Rows.Clear()
        Me.btnBuscarProveedor.Focus()
        y = 0
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class