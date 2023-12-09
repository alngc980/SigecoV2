Imports System.Data.SqlClient
Imports Libreria
Public Class frmhistoricoLetrasCod
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim arrayMoneda() As String = {"S/.", "$", "€"}
    Private Sub frmconsultaLetrasCod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        oDataSet = New DataSet()
        Me.dgvLetras.Rows.Clear()
        Try

            If Me.txtcodCliente.Text = "" Or (Len(Me.txtcodCliente.Text) < 8 Or Len(Me.txtcodCliente.Text) > 11) Then
                MsgBox("Ingrese número DNI o RUC válido.", MsgBoxStyle.Information)
                Me.txtcodCliente.Text = ""
                Me.txtcodCliente.Focus()
                Exit Sub
            End If

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where dni Like '" & Me.txtcodCliente.Text & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("Documento no registrado.", MsgBoxStyle.Critical)
                Me.txtcodCliente.Text = ""
                Me.txtcodCliente.Focus()
                Connection.Close()
                Exit Sub
            End If

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento," & _
            "fecPago,status,idMoneda,idCliente FROM letrasClientes where idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")
            Connection.Close()

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                oDataRowMoneda(10) = arrayMoneda(Me.oDataSet.Tables(1).Rows(i).Item(8) - 1)
            Next i

            Me.txtCliente.Text = oDataSet.Tables(0).Rows(0).Item(1)
            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(0).DisplayIndex = 0
                .Columns(1).DisplayIndex = 1
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DisplayIndex = 3
                .Columns(3).DisplayIndex = 4
                .Columns(4).DisplayIndex = 5
                .Columns(5).DisplayIndex = 6
                .Columns(6).DisplayIndex = 7
                .Columns(7).DisplayIndex = 8
                .Columns(8).DisplayIndex = 9
                .Columns(9).DisplayIndex = 10
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
            "Nombre Cliente: " & Me.txtCliente.Text & enter & _
            "Fecha         : " & Me.dtpFecha.Text & enter & enter & _
            "Núm.Letra Corr. Moneda  Importe MN  Importe ME  Fec.Emisión  Fec.Venc."
            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                te.Text = te.Text & enter & _
                Me.dgvLetras.Rows(i).Cells(0).Value & "      " & _
                Me.dgvLetras.Rows(i).Cells(1).Value & "      " & _
                Me.dgvLetras.Rows(i).Cells(9).Value & "    " & _
                Me.dgvLetras.Rows(i).Cells(2).Value & "       " & _
                Me.dgvLetras.Rows(i).Cells(3).Value & "    " & _
                Me.dgvLetras.Rows(i).Cells(4).Value & "    " & _
                Me.dgvLetras.Rows(i).Cells(5).Value
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
    Private Sub txtcodCliente_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcodCliente.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class