Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmanularNotaDebito
    Dim te As New RichTextBox
    Dim codigoCliente, tipDocumentoVta As String
    Dim numDocumentoVta As Integer
    Private oDataSet As DataSet
    Private Sub frmanularNotaDebito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.lblNombre.Text = txtNombreEmpresa
        Me.lblDireccion.Text = txtDireccionEmpresa
        Me.lblTelefono.Text = txtTelefonoEmpresa
        Me.lblRuc.Text = txtRUCEmpresa
        Me.txtSerieDocumento.Text = "01"
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.txtTotalPagarME.Text = 0
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.cbxCanCuotas.Enabled = False
        Me.KeyPreview = True
        If flag = 1 And fecDocumento = Date.Today Then
            Me.btnBuscar.Enabled = False
            Me.btnImprimir.Enabled = True 'No debe permitir imprimir documentos que no sean del día
            Me.cbxTipoDocumento.Text = tipMovimiento
            Me.txtNumNotaDebito.Text = numDocumento
            Me.btnBuscar_Click(sender, e)
        Else
            If flag = 1 And fecDocumento <> Date.Today Then
                Me.btnBuscar.Enabled = False
                Me.cbxTipoDocumento.Text = tipMovimiento
                Me.txtNumNotaDebito.Text = numDocumento
                Me.btnBuscar_Click(sender, e)
            End If
        End If
    End Sub
    Private Sub frmanularNotaDebito_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F2 Then
            btnBuscar_Click(sender, e)
        Else
            If e.KeyCode = Keys.F4 Then
                'btnGrabar_Click(sender, e)
            Else
                If e.KeyCode = Keys.F8 Then
                    btnAnular_Click(sender, e)
                Else
                    If e.KeyCode = Keys.F10 Then
                        btnImprimir_Click(sender, e)
                    Else
                        If e.KeyCode = Keys.F12 Then
                            'btnLimpiar_Click(sender, e)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim subTotal As Decimal
        Dim oProducto As Producto = New Producto()
        oDataSet = New DataSet()
        Me.dgvProductos.Rows.Clear()

        If Me.txtNumNotaDebito.Text = "" Then
            MsgBox("Ingrese número documento para continuar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            Dim daNotaCreditoCa As SqlDataAdapter = New SqlDataAdapter("select * from notaDebitoCa where tipDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and numDocumento='" & Me.txtNumNotaDebito.Text & "' and status<>'A' ", Connection)
            daNotaCreditoCa.Fill(oDataSet, "notaCreditoCa")

            If Me.oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existe número de documento o ya está anulado.", MsgBoxStyle.Critical)
                Me.txtNumNotaDebito.Text = ""
                Exit Sub
            End If

            Dim daGlosasFacturas As SqlDataAdapter = New SqlDataAdapter("select * from glosasFacturas where tipDocumento='" & Me.cbxTipoDocumento.Text & _
            "' and numDocumento='" & Me.txtNumNotaDebito.Text & "'", Connection)
            daGlosasFacturas.Fill(oDataSet, "glosasFacturas")

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(6) & "'", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Me.cbxTipoVenta.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(4).ToString
            Me.txtNumDocumento.Text = Me.oDataSet.Tables(0).Rows(0).Item(5).ToString
            Me.tipDocumentoVta = Mid(Me.oDataSet.Tables(0).Rows(0).Item(5).ToString, 1, 2)
            Me.numDocumentoVta = Mid(Me.oDataSet.Tables(0).Rows(0).Item(5).ToString, 3, Me.oDataSet.Tables(0).Rows(0).Item(5).ToString.Length - 2)

            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                subTotal = Me.oDataSet.Tables(0).Rows(0).Item(8)
            Else
                subTotal = Me.oDataSet.Tables(0).Rows(0).Item(7)
            End If

            Me.dtmFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(11)
            Me.cbxTipoMoneda.SelectedIndex = Me.oDataSet.Tables(0).Rows(0).Item(12) - 1

            Me.txtGlosa.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)

            Me.codigoCliente = Me.oDataSet.Tables(2).Rows(0).Item(0)
            Me.txtNombre.Text = Me.oDataSet.Tables(2).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(2).Rows(0).Item(2)
            Me.txtDNI.Text = Me.oDataSet.Tables(2).Rows(0).Item(3)

            Me.dgvProductos.Rows.Add()
            Me.dgvProductos.Rows(0).Cells(0).Value = "1"
            Me.dgvProductos.Rows(0).Cells(1).Value = "1"
            Me.dgvProductos.Rows(0).Cells(2).Value = Me.txtGlosa.Text
            Me.dgvProductos.Rows(0).Cells(3).Value = subTotal
            Me.dgvProductos.Rows(0).Cells(4).Value = "1"
            Me.dgvProductos.Rows(0).Cells(5).Value = subTotal

            Me.txtSubTotal.Text = Format(subTotal, "###,##0.00")
            Me.txtInteres.Text = 0
            Me.txtIGV.Text = 0

            If Me.cbxTipoMoneda.SelectedIndex >= 1 Then
                Me.txtTotalPagar.Text = Format(subTotal, "###,##0.00")
            Else
                Me.txtTotalPagar.Text = Format(subTotal, "###,##0.00")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            'Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub cbxTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxTipoMoneda.SelectedIndexChanged
        Dim cadenaString As String = "SELECT * FROM tiposMonedas where idMoneda='" & cbxTipoMoneda.SelectedIndex + 1 & "'"
        lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)
            te.Text = enter & enter & enter & enter & enter & enter & enter & _
            "   " & Me.txtNombre.Text & enter & enter & enter & _
            "   " & Me.txtDireccion.Text & "                 " & Me.txtDNI.Text & "               " & Me.dtmFecha.Text & enter & enter & enter & enter

            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                te.Text = te.Text & enter & _
                Me.dgvProductos.Rows(i).Cells(4).Value.ToString.PadRight(5, " ") & _
                Me.dgvProductos.Rows(i).Cells(1).Value.ToString.PadRight(5, " ") & _
                Me.dgvProductos.Rows(i).Cells(2).Value.ToString.PadRight(50, " ") & _
                Me.dgvProductos.Rows(i).Cells(3).Value.ToString.PadLeft(20, " ") & _
                Me.dgvProductos.Rows(i).Cells(5).Value.ToString.PadLeft(10, " ")
            Next

            te.Text = te.Text & enter & enter & enter & enter & enter & enter & enter & enter
            te.Text = te.Text & enter & "          " & _
           numeroLetras(VisualBasic.Left(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00"), Len(Format(Decimal.Parse(Me.txtTotalPagar.Text), "#,###,##0.00")) - 3)) & _
                                        " Y " & obtieneDecimales(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,###0.00")) & "/100 NUEVOS SOLES "

            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtSubTotal.Text), "#####0.00").PadLeft(90, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtInteres.Text), "#####0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtIGV.Text), "#####0.00").PadLeft(40, " ")
            te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagar.Text), "######0.00").PadLeft(90, " ")
            'te.Text = te.Text & enter & Format(Decimal.Parse(Me.txtTotalPagarME.Text), "######0.00").PadLeft(40, " ")

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
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
    Handles PrintDocument1.PrintPage
        Dim Fuente As New Font("Courier New", 9)

        VistaPrevia("Courier New", 9, te.Text, e)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtSubTotal.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 540)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtInteres.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 565)
        e.Graphics.DrawString(Format(Decimal.Parse(Me.txtTotalPagar.Text), "###,##0.00").PadLeft(10), Fuente, Brushes.Black, 720, 585)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Recibo"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 996
        Dim Alto As Short = 709

        Dim left As Short = 0
        Dim top As Short = 50
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
    End Sub
    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList

        If Me.txtNumNotaDebito.Text = "" Then
            MsgBox("Ingrese número documento para eliminar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            If MsgBox("Está seguro de anular este documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                SqlString = "update notaDebitoCa set docReferencia=' ',totVentaMN=0,totVentaME=0,intFinanciero=0,status='A' where numDocumento=" & CInt(Me.txtNumNotaDebito.Text) & ""
                ListSqlStrings.Add(SqlString)

                SqlString = "update vtaCabecera set statusNC=' ' where tipDocumento='" & Me.tipDocumentoVta & "' and numDocumento=" & Me.numDocumentoVta & ""
                ListSqlStrings.Add(SqlString)

                SqlString = "delete from glosasFacturas where tipDocumento='" & Me.cbxTipoDocumento.Text & "' and numDocumento=" & CInt(Me.txtNumNotaDebito.Text) & ""
                ListSqlStrings.Add(SqlString)

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Documento anulado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                    Me.Close()
                Else
                    MsgBox("Error en el proceso, no se anuló documento.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtNumNotaDebito_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumNotaDebito.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Me.txtSerieDocumento.Text = "01"
        Me.txtNumNotaDebito.Text = ""
        Me.codigoCliente = ""
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.txtGlosa.Text = ""
        Me.cbxTipoDocumento.SelectedIndex = 0
        Me.cbxTipoMoneda.SelectedIndex = 0
        Me.lbltotalME.Text = Trim(Me.cbxTipoMoneda.SelectedItem) + ":"
        Me.cbxTipoVenta.Enabled = False
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoCredito.Enabled = False
        Me.cbxCanCuotas.Enabled = False
        Me.KeyPreview = True
        Me.btnImprimir.Enabled = False
        Me.btnProcesar.Enabled = False
        Me.txtSubTotal.Text = 0
        Me.txtInteres.Text = 0
        Me.txtIGV.Text = 0
        Me.txtTotalPagar.Text = 0
        Me.dgvProductos.Rows.Clear()
        Me.txtNumNotaDebito.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class