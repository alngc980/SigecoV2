Imports Microsoft
Imports System.Data.SqlClient
Public Class frmcomisionVendedor
    Dim texto As New RichTextBox
    Private oDataSet As DataSet
    Dim miFecha As Date
    Dim anno As String
    Dim arrayMonedas() As String = {"S/", "$", "€"}
    Dim arrayConceptos() As String = {"VCo", "VCr", "VT"}
    Dim totalSoles, totalDolares, totalEuros As Decimal
    Dim totalCI, totalSI As Decimal
    Private flagProcesa As Boolean
    Dim NroPaginasImpresas As Integer = 0
    Private Sub frmcomisionVendedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
        Me.cbxAnno.SelectedIndex = 0
        Me.cbxMes.SelectedIndex = 0
        Me.txtVendedor.Text = 1
        miFecha = Now() : anno = DatePart(DateInterval.Year, miFecha)
        Me.cbxAnno.Text = anno
    End Sub
    Private Sub txtVendedor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVendedor.DoubleClick
        arrayDatos(0) = ""
        frmbuscaVendedor.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtVendedor.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click

        flagProcesa = True
        Dim Max As Long = 1000
        Dim z As Long

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        Try
            oDataSet = New DataSet()
            Connection.Open()

            Dim daVendedores As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM vendedores where idVendedor=" & CInt(Me.txtVendedor.Text) & "", Connection)
            daVendedores.Fill(oDataSet, "vendedores")

            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM vtaCabecera where month(fecOperacion)=" & Me.cbxMes.SelectedIndex + 1 & _
            " and year(fecOperacion)=" & CInt(Me.cbxAnno.Text) & " and idVendedor=" & CInt(Me.txtVendedor.Text) & " and comVendedor>0", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            If Me.oDataSet.Tables(1).Rows.Count <= 0 Then
                MsgBox("No registra movimientos en este periodo.", MsgBoxStyle.Information)
                flagProcesa = False
                Exit Sub
            End If

            'Dim daVtaDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT tipDocumento,serDocumento,numDocumento,cantidad,fecOperacion FROM vtaDetalle where month(fecOperacion)=" & Me.cbxMes.SelectedIndex + 1 & " and year(fecOperacion)=" & CInt(Me.cbxAnno.Text) & "", Connection)
            'daVtaDetalle.Fill(oDataSet, "vtaDetalle")
            Connection.Close()

            'Dim oDataRow1 As DataRow
            'For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
            '    For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1

            '        If Me.oDataSet.Tables(2).Rows(x).Item(1) = "SA" And _
            '        Me.oDataSet.Tables(2).Rows(x).Item(2).ToString = Me.oDataSet.Tables(3).Rows(i).Item(3).ToString Then
            '            oDataRow1 = Me.oDataSet.Tables(2).Rows(x)
            '            oDataRow1(6) = Me.oDataSet.Tables(3).Rows(i).Item(2)
            '        End If

            '    Next x
            'Next i

            For z = 1 To Max
                ProgressBar1.PerformStep()
                Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If flagProcesa <> True Then
            MsgBox("Procese información para imprimir.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        NroPaginasImpresas = 0
        totalSoles = 0 : totalDolares = 0 : totalEuros = 0 : totalCI = 0 : totalSI = 0

        Try
            Dim nomVendedor As String = Me.oDataSet.Tables(0).Rows(0).Item(3) + " " + Me.oDataSet.Tables(0).Rows(0).Item(2) + " " + Me.oDataSet.Tables(0).Rows(0).Item(1)
            Dim en, t As Keys
            Dim enter, tab As Char
            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)
            texto.Text = enter & _
            txtNombreEmpresa & enter & _
            txtRUCEmpresa & "                                                                 Fecha :" & DateTime.Today & enter & enter & enter
            texto.Text = texto.Text & "                                   REPORTE DE COMISION DE VENDEDOR" & enter
            texto.Text = texto.Text & "                                              " & Me.cbxMes.Text & "-" & Me.cbxAnno.Text & enter
            texto.Text = texto.Text & "Nombre Vendedor: " & nomVendedor & enter
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & "Fecha       Doc. Serie   Numero  Operación  Moneda   Monto Doc.    Comisión C/I        Comisión S/I   " & enter
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(1).Rows(i).Item(12).ToString, 10).PadLeft(10)
                texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(0).ToString.PadLeft(5)
                texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(1).ToString.PadLeft(5)
                texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(2).ToString.PadLeft(10)
                texto.Text = texto.Text & arrayConceptos(Me.oDataSet.Tables(1).Rows(i).Item(4)).ToString.PadLeft(10)
                texto.Text = texto.Text & arrayMonedas(Me.oDataSet.Tables(1).Rows(i).Item(15) - 1).ToString.PadLeft(6)
                texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(8).ToString.PadLeft(15)
                If Me.oDataSet.Tables(1).Rows(i).Item(14) > 0 Then
                    texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(13).ToString.PadLeft(12)
                    totalCI = totalCI + Me.oDataSet.Tables(1).Rows(i).Item(13)
                Else
                    texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(13).ToString.PadLeft(32)
                    totalSI = totalSI + Me.oDataSet.Tables(1).Rows(i).Item(13)
                End If
                texto.Text = texto.Text & enter
                If Me.oDataSet.Tables(1).Rows(i).Item(15) = 1 Then
                    totalSoles += Me.oDataSet.Tables(1).Rows(i).Item(13)
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(15) = 2 Then
                        totalDolares += Me.oDataSet.Tables(1).Rows(i).Item(13)
                    Else
                        totalEuros += Me.oDataSet.Tables(1).Rows(i).Item(13)
                    End If
                End If
            Next i
            texto.Text = texto.Text & "------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & "Resumen:"
            texto.Text = texto.Text & Format(totalCI, "###,##0.00").PadLeft(53)
            texto.Text = texto.Text & Format(totalSI, "###,##0.00").PadLeft(32) & enter
            texto.Text = texto.Text & "Total Soles:   " & CStr(Format(totalSoles, "##,##0.00")).ToString.PadLeft(12) & enter
            texto.Text = texto.Text & "Total Dolares: " & CStr(Format(totalDolares, "##,##0.00")).ToString.PadLeft(12) & enter
            texto.Text = texto.Text & "Total Euros:   " & CStr(Format(totalEuros, "##,##0.00")).ToString.PadLeft(12) & enter

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                'PrintDocument1.DefaultPageSettings.Landscape = True
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
                configurarImpresion()
                'PrintDocument1.DefaultPageSettings.Landscape = True
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
        VistaPrevia("Courier New", 9, texto.Text, e)

        If NroPaginasImpresas > 1 Then
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 20, 5)
            e.Graphics.DrawString("Fecha       Doc. Serie   Numero  Operación  Moneda   Monto Doc.    Comisión C/I        Comisión S/I   ", Fuente1, Brushes.Black, 20, 20)
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 20, 35)
        End If
        If e.HasMorePages <> False Then
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 20, 985)
            e.Graphics.DrawString("Resumen:", Fuente1, Brushes.Black, 20, 1000)
            e.Graphics.DrawString(Format(totalCI, "###,##0.00"), Fuente1, Brushes.Black, 520, 1000)
            e.Graphics.DrawString(Format(totalSI, "###,##0.00"), Fuente1, Brushes.Black, 695, 1000)
            e.Graphics.DrawString("Total Soles  : " & Format(totalSoles, "###,##0.00"), Fuente1, Brushes.Black, 20, 1015)
            e.Graphics.DrawString("Total Dolares: " & Format(totalDolares, "###,##0.00"), Fuente1, Brushes.Black, 20, 1030)
            e.Graphics.DrawString("Total Euros  : " & Format(totalEuros, "###,##0.00"), Fuente1, Brushes.Black, 20, 1045)
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Ventas"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 894
        Dim Alto As Short = 1090

        Dim left As Short = 20
        Dim top As Short = 50
        Dim bottom As Short = 100
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
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class