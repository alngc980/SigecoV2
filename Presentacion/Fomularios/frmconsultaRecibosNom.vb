Imports System.Data.SqlClient
Public Class frmconsultaRecibosNom
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim arrayConceptos() As String = {"V.Contado", "A.Letra", "C.Letra", "C.Inicial", "A.Cuota Inicial", "V.Tarjeta", "V.Tarjeta Oferta", "V.Tarjeta Remate", "V.Oferta", "V.Remate", "O.Pagos", "Cobro Interés", "Cargo Operación"}
    Dim arrayMonedas() As String = {"S/.", "$", "€"}
    Private Sub frmconsultaLetrasNom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaCliente.KeyUp
        Try
            oDataSet = New DataSet()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes where nombres Like '" & Me.txtBuscaCliente.Text & "%" & "' ", Connection)
            Connection.Open()
            daCliente.Fill(oDataSet, "cliente")

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("select idRecibo,impDocumento,impDocumentoME,fecEmision,idMoneda,numDocGenCI,numDocGenACI,concepto,idCliente" & _
                                                               " from recibosClientes where (concepto='0' or concepto='3'or concepto='4' or concepto='5' or concepto='6'" & _
                                                               " or concepto='7' or concepto='8' or concepto='9'or concepto='10') and numDocGenACI='' and idCliente like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' and status<>'X'", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")
            Connection.Close()

            Dim colConcepto As DataColumn = New DataColumn()
            colConcepto.AllowDBNull = True
            colConcepto.Caption = "Conceptos"
            colConcepto.ColumnName = "conceptos"
            Me.oDataSet.Tables(1).Columns.Add(colConcepto)

            Dim oDataRowConcepto As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowConcepto = Me.oDataSet.Tables(1).Rows(i)
                oDataRowConcepto(9) = arrayConceptos(Me.oDataSet.Tables(1).Rows(i).Item(7))
            Next i

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                oDataRowMoneda(10) = arrayMonedas(Me.oDataSet.Tables(1).Rows(i).Item(4) - 1)
            Next i

            Me.lblNombre.Text = oDataSet.Tables(0).Rows(0).Item(1)
            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(9).DisplayIndex = 1
                .Columns(10).DisplayIndex = 2
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
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
            "Nombre Cliente: " & Me.lblNombre.Text & enter & _
            "Fecha         : " & Me.dtpFecha.Text & enter & enter & _
           "Núm.Rec. Moneda Importe MN  Importe ME  Fec.Emisión"
            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                te.Text = te.Text & enter & "   " & _
                Me.dgvLetras.Rows(i).Cells(1).Value & "        " & _
                Me.dgvLetras.Rows(i).Cells(7).Value & "      " & _
                Me.dgvLetras.Rows(i).Cells(2).Value & "      " & _
                Me.dgvLetras.Rows(i).Cells(3).Value & "      " & _
                Me.dgvLetras.Rows(i).Cells(4).Value
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
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    End Sub
    Private Sub dgvletras_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvLetras.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex
        If columna = 6 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class