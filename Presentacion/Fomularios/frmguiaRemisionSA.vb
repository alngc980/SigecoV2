Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmguiaRemisionSA
    Dim te As New RichTextBox
    Dim txtCodigoCliente, fechaCierre As String
    Dim arrayGrupo(10) As String
    Dim arrayNS(10) As String
    Dim arrayNM(10) As String
    Dim arrayNCH(10) As String
    Dim txtGrupo As Integer
    Dim ultimaCantidad As Integer
    Dim txtStatus As String = "-"
    Dim i, item, ultimoNumero As Integer
    Private Sub frmguiaRemisionSA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRUC.Text = txtRUCEmpresa
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMovimiento.SelectedIndex = 0
        Dim strUltimoNumero As String = ("SELECT * FROM ultimosNumeros where tipDocumento='GR' and tipMovimiento='SA'")
        Me.txtSerie.Text = "01"
        Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1
        Me.fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")
        Me.txtNombre.Text = txtNombreEmpresa
        Me.txtDireccion.Text = txtDireccionEmpresa
        Me.txtDNIRUC.Text = Mid(txtRUCEmpresa, 6, 12)
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True
    End Sub
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente = arrayDatos(0)
            Me.txtNomCliente.Text = arrayDatos(1)
            Me.txtDirCliente.Text = arrayDatos(2)
            Me.txtDocCliente.Text = arrayDatos(3)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
        End If
    End Sub
    Private Sub btnNuevoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
        arrayDatos(0) = ""
        frmNuevoCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente = arrayDatos(0)
            Me.txtNomCliente.Text = arrayDatos(1)
            Me.txtDirCliente.Text = arrayDatos(2)
            Me.txtDocCliente.Text = arrayDatos(3)
            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
        End If
    End Sub
    Private Sub frmguiaRemisionSA_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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
            If Me.txtCodigoCliente <> "" And Me.txtDNIRUC.Text <> "" Then
                arrayDatos(0) = ""
                frmbuscaProducto.ShowDialog()
                If arrayDatos(0) <> "" Then
                    item += 1
                    Me.dgvProductos.Rows.Add()
                    Me.i = Me.dgvProductos.RowCount - 1
                    Me.dgvProductos.Rows(i).Cells(0).Value = item
                    Me.dgvProductos.Rows(i).Cells(1).Value = arrayDatos(0)
                    Me.dgvProductos.Rows(i).Cells(11).Value = arrayDatos(1)
                    Me.dgvProductos.Rows(i).Cells(2).Value = arrayDatos(2)
                    Me.dgvProductos.Rows(i).Cells(3).Value = arrayDatos(3)
                    Me.dgvProductos.Rows(i).Cells(4).Value = arrayDatos(4)

                    arraySeries(1) = ""
                    arraySeries(2) = ""
                    arraySeries(3) = ""
                    codigoProducto = arrayDatos(0)
                    frmbuscarSeries.ShowDialog()
                    If arraySeries(0) <> "" Then
                        Dim yaEsta As Boolean = False
                        For x As Integer = 0 To Me.dgvProductos.Rows.Count - 1

                            If Me.dgvProductos.Rows(x).Cells(11).Value = 6 Then
                                If Me.dgvProductos.Rows(x).Cells(1).Value = arrayDatos(0) And _
                                Me.dgvProductos.Rows(x).Cells(6).Value = arraySeries(2) And _
                                Me.dgvProductos.Rows(x).Cells(7).Value = arraySeries(3) Then
                                    yaEsta = True
                                    Exit For
                                End If
                            Else
                                If Me.dgvProductos.Rows(x).Cells(1).Value = arrayDatos(0) And _
                                Me.dgvProductos.Rows(x).Cells(5).Value = arraySeries(1) Then
                                    yaEsta = True
                                    Exit For
                                End If
                            End If
                        Next
                        If yaEsta = True Then
                            MsgBox("Este item ya está registrado.", MsgBoxStyle.Exclamation)
                            'flagString = ""
                            Exit Sub
                        End If

                        If Me.dgvProductos.Rows(i).Cells(11).Value = 6 Then
                            Me.dgvProductos.Rows(i).Cells(5).Value = ""
                            Me.dgvProductos.Rows(i).Cells(6).Value = arraySeries(2)
                            Me.dgvProductos.Rows(i).Cells(7).Value = arraySeries(3)
                            Me.dgvProductos.Rows(i).Cells(10).Value = arraySeries(4)
                        Else
                            Me.dgvProductos.Rows(i).Cells(5).Value = arraySeries(1)
                            Me.dgvProductos.Rows(i).Cells(7).Value = ""
                            Me.dgvProductos.Rows(i).Cells(8).Value = ""
                            Me.dgvProductos.Rows(i).Cells(10).Value = arraySeries(4)
                        End If
                    Else
                        MsgBox("No se puede procesar " + Me.dgvProductos.Rows(i).Cells(2).Value + ", no tiene datos completos.", MsgBoxStyle.Information)
                        flagString = ""
                        Exit Sub
                    End If
                    Me.dgvProductos.Rows(i).Cells(8).Value = 1
                    'If flag <> 0 Then
                    '    flag = 0
                    '    Exit Sub
                    'End If

                    arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
                    arrayDatos(4) = "" : arrayDatos(5) = "" : arrayDatos(6) = "" : arrayDatos(7) = ""
                    arraySeries(0) = "" : arraySeries(1) = "" : arraySeries(2) = "" : arraySeries(3) = ""
                    arraySeries(4) = ""
                    flagString = ""
                End If
            Else
                MsgBox("Faltan datos del Cliente.", MsgBoxStyle.Information)
                Me.btnBuscarCliente.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellValueChanged
        Dim sqlCadena As String
        Try
            If (dgvProductos.Columns(e.ColumnIndex).Name = "cantidad") Then
                sqlCadena = "SELECT * FROM saldosAlmacenes where idProducto='" & Me.dgvProductos.Rows(e.RowIndex).Cells(1).Value & "' and fechaSaldo='" & CDate(fechaCierre) & "'"
                If devuelveStock(sqlCadena) < Val(Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value) Then
                    MsgBox("Valor excede la cantidad de stock existente de " & Me.dgvProductos.Rows(e.RowIndex).Cells(2).Value, MsgBoxStyle.Information & ".")
                    Me.dgvProductos.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                    'flag = 1
                    'flagString = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            If Me.dgvProductos.RowCount <= 0 Then
                MsgBox("Faltan datos del producto.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            For i As Integer = 0 To Me.dgvProductos.Rows.Count - 1
                If Me.dgvProductos.Rows(i).Cells(8).Value = 0 Then
                    MsgBox("Uno de productos no indica cantidad, Eliminelo.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            Next

            Dim oFrmAcceso As New frmaccesoAdministrador()
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            Dim oProducto As Producto = New Producto()
            Dim stockActual As Integer
            Dim SqlStringAlm As String = ""
            Dim SqlStringAlm1 As String = ""
            Dim SqlStringAlm2 As String = ""
            Dim SqlStringAlm3 As String = ""
            Dim SqlStringAlm4 As String = ""

            Dim ListSqlStringsAlm1 As New ArrayList
            Dim ListSqlStringsAlm2 As New ArrayList
            Dim ListSqlStringsAlm3 As New ArrayList

            SqlStringAlm = "INSERT INTO almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor," & _
            "fecOrigen,nomOrigen,dirOrigen,rucDNI_1,fecLlegada,idCliente,transLlegada,status) VALUES ('" & _
            Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",1,'" & _
            Me.dtpFecOrigen.Text & "' ,'" & txtNombreEmpresa & "','" & txtDireccionEmpresa & "','" & _
            Mid(txtRUCEmpresa, 6, 11) & "','" & Me.dtpFecLlegada.Text & "'," & Me.txtCodigoCliente & ",'','" & Me.txtStatus & "' )"

            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                SqlStringAlm1 = "INSERT INTO almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad,status) VALUES ('" & _
                Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",'" & _
                dgvProductos.Rows(i).Cells(1).Value & "' ," & dgvProductos.Rows(i).Cells(8).Value & ",'0')"

                Dim sqlSaldo As String
                sqlSaldo = "SELECT * FROM saldosAlmacenes where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"
                stockActual = devuelveStock(sqlSaldo)
                stockActual = stockActual - Me.dgvProductos.Rows(i).Cells(8).Value

                SqlStringAlm2 = "UPDATE saldosAlmacenes Set stock='" & stockActual & "' where idProducto=" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & " and fechaSaldo='" & CDate(fechaCierre) & "'"

                If grabarSqlString(SqlStringAlm2) Then 'Para actualice inmediatamente el stock
                    'MsgBox("Saldo actualizado correctamente.", MsgBoxStyle.Information)
                Else
                    MsgBox("Error en la actualización de saldos.", MsgBoxStyle.Critical)
                End If
                SqlStringAlm2 = "Select * from saldosAlmacenes" 'Para completar la configuración de la función 'grabacionFacturacion()

                If Me.dgvProductos.Rows(i).Cells(11).Value = 6 Then
                    SqlStringAlm4 = "UPDATE numerosSerie Set numDoc='" & Me.txtNumDocumento.Text & "' where numMotor='" & Me.dgvProductos.Rows(i).Cells(6).Value & "' and numChasis='" & Me.dgvProductos.Rows(i).Cells(7).Value & "' and idProducto='" & Me.dgvProductos.Rows(i).Cells(1).Value & "' "
                Else
                    SqlStringAlm4 = "UPDATE numerosSerie Set numDoc='" & Me.txtNumDocumento.Text & "' where numSerie='" & Me.dgvProductos.Rows(i).Cells(5).Value & "'and idProducto='" & Me.dgvProductos.Rows(i).Cells(1).Value & "' "
                End If

                ListSqlStringsAlm3.Add(SqlStringAlm4)
                ListSqlStringsAlm1.Add(SqlStringAlm1)
                ListSqlStringsAlm2.Add(SqlStringAlm2)
            Next

            SqlStringAlm3 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumDocumento.Text & " where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and tipMovimiento='" & Me.cbxTipoMovimiento.Text & "'"

            If transaccionAlmacen(SqlStringAlm, SqlStringAlm3, ListSqlStringsAlm1, ListSqlStringsAlm2, ListSqlStringsAlm3) Then
                MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                Me.btnLimpiar_Click(sender, e)
            Else
                MsgBox("La Información no se guardó.", MsgBoxStyle.Critical)
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
            te.Text = enter & enter & enter & enter & enter & _
            "   " & Me.txtDocCliente.Text & "                                            " & Me.dtpFecOrigen.Text & enter & enter & enter & _
            "                     " & Me.txtNomCliente.Text & enter & enter & _
            "                     " & Me.txtDirCliente.Text & enter & enter & enter & enter & _
            "                     " & enter & enter & enter & enter

            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                te.Text = te.Text & enter & _
                Me.dgvProductos.Rows(i).Cells(8).Value.ToString & " " & Space(2 - Len(Me.dgvProductos.Rows(i).Cells(8).Value.ToString)) & _
                VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(2).Value.ToString, 30) & " " & Space(30 - Len(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(2).Value.ToString, 30))) & _
                VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 10) & " " & Space(10 - Len(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(3).Value.ToString, 10))) & _
                VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 15) & " " & Space(15 - Len(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 15))) & _
                VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 20) & " " & Space(20 - Len(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 20))) & _
                VisualBasic.Left(CStr(Me.dgvProductos.Rows(i).Cells(6).Value), 20) & " " & Space(20 - Len(VisualBasic.Left(CStr(Me.dgvProductos.Rows(i).Cells(6).Value), 20))) & _
                VisualBasic.Left(CStr(Me.dgvProductos.Rows(i).Cells(7).Value), 20) & " " & Space(20 - Len(VisualBasic.Left(CStr(Me.dgvProductos.Rows(i).Cells(7).Value), 20))) & _
                VisualBasic.Left(CStr(Me.dgvProductos.Rows(i).Cells(9).Value), 10)
            Next

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
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
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        VistaPrevia("Courier New", 8, te.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 1024
        Dim Alto As Short = 787

        Dim left As Short = 0
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
                SqlString = "INSERT INTO almCabecera (nomDocumento,tipDocumento,numDocumento,idProveedor," & _
                "fecOrigen,nomOrigen,dirOrigen,rucDNI_1,fecLlegada,idCliente,transLlegada,status) VALUES ('" & _
                Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",1,'" & _
                Me.dtpFecOrigen.Text & "',' ',' ',' ',' ',1,' ','A' )"

                SqlString1 = "INSERT INTO almDetalle (nomDocumento,tipDocumento,numDocumento,idProducto,cantidad) VALUES ('" & _
                Me.cbxTipoDocumento.Text & "' ,'" & Me.cbxTipoMovimiento.Text & "' ," & Me.txtNumDocumento.Text & ",1,0)"

                SqlString2 = "UPDATE ultimosNumeros Set numero=" & Me.txtNumDocumento.Text & " where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and tipMovimiento='" & Me.cbxTipoMovimiento.Text & "'"

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
    Private Sub cbxTipoDocumento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTipoDocumento.SelectedIndexChanged
        If Me.cbxTipoDocumento.SelectedIndex = 0 Then
            Dim strUltimoNumero As String = ("SELECT *FROM ultimosNumeros where tipDocumento='GR' and tipMovimiento='SA'")
            Me.txtSerie.Text = "01"
            Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1
            Me.txtNumDocumento.Enabled = True
        Else
            Dim strUltimoNumero As String = ("SELECT *FROM ultimosNumeros where tipDocumento='PD' and tipMovimiento='SA'")
            Me.txtSerie.Text = "01"
            Me.txtNumDocumento.Text = devuelveUltimoNumero(strUltimoNumero) + 1
            Me.txtNumDocumento.Enabled = False
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtNomCliente.Text = ""
        Me.txtDirCliente.Text = ""
        Me.txtDocCliente.Text = ""
        Me.txtTransLlegada.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.dgvProductos.Rows.Clear()
        Me.btnBuscarCliente.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class