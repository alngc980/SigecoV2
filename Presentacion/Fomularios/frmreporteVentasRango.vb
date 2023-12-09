Imports Microsoft
Imports System.Data.SqlClient
Public Class frmreporteVentasRango
    Dim texto As New RichTextBox
    Private oDataSet As DataSet
    Dim miFecha As Date
    Dim anno As String
    Dim mes As String

    Dim totVenta, totInteres, totInicial, totNotaCredito, totLetrasPeriodo, totPrecioVenta As Decimal
    Dim totVentaContado, totVentaCredito, totCuotaInicial, totAnticipoCuota, totVentaTarjeta As Decimal
    Dim totVentaTarjetaOferta, totVentaTarjetaRemate, totVentaOferta, totVentaRemate As Decimal

    Dim arrayMonedas() As String = {"S", "$", "€"}
    Dim arrayConceptos() As String = {"VCo", "VCr", "VCr", "CIn", "ACI", "VTa", "VTO", "VTR", "VOf", "VRe"}

    Private Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        'Dim rep As New frmReporte
        'rep.cNombreReport = "ReporteVentas"
        'rep.par1 = "@dFecIni"
        'rep.var1 = dtpFechaInicio.Text
        'rep.par2 = "@dFecFin"
        'rep.var2 = dtpFechaLimite.Text
        'rep.ShowDialog()

        Dim ds As New dsRepVenta
        Dim Dt As New DataTable
        Dim Sql As String = "rpt_Ventas '" & dtpFechaInicio.Text & "','" & dtpFechaLimite.Text & "'"
        Dt = RetornaDataTable(Sql)
        If Dt.Rows.Count > 0 Then
            For i = 0 To Dt.Rows.Count - 1
                ds.DataTable1.Rows.Add(
                               Dt.Rows(i)(0).ToString(), Dt.Rows(i)(1).ToString(),
                               Dt.Rows(i)(2).ToString(), Dt.Rows(i)(3).ToString(),
                               Dt.Rows(i)(4).ToString(), Dt.Rows(i)(5).ToString(),
                               Dt.Rows(i)(6).ToString(), Dt.Rows(i)(7).ToString(),
                               Dt.Rows(i)(8).ToString(), Dt.Rows(i)(9).ToString(),
                               Dt.Rows(i)(10).ToString(), Dt.Rows(i)(11).ToString(),
                               Dt.Rows(i)(12).ToString())
            Next
        End If

        Dim dtx As DataTable
        Sql = "rpt_Ventas_Sum '" & dtpFechaInicio.Text & "','" & dtpFechaLimite.Text & "'"
        dtx = RetornaDataTable(Sql)
        If dtx.Rows.Count > 0 Then
            For i = 0 To dtx.Rows.Count - 1
                ds.DataTable2.Rows.Add(dtpFechaInicio.Text, dtpFechaLimite.Text,
                                        dtx.Rows(i)(0).ToString(),
                                        dtx.Rows(i)(1).ToString(),
                                        dtx.Rows(i)(2).ToString(),
                                        dtx.Rows(i)(3).ToString(),
                                        dtx.Rows(i)(4).ToString(),
                                        dtx.Rows(i)(5).ToString(),
                                        dtx.Rows(i)(6).ToString(),
                                        dtx.Rows(i)(7).ToString(),
                                        dtx.Rows(i)(8).ToString(),
                                        dtx.Rows(i)(9).ToString())
            Next
        End If


        Dim rpt As New ReporteVentas
        'rpt.SetDataSource(ds.Tables("DataTable1"))
        'rpt.SetDataSource(ds.Tables("DataTable2"))
        rpt.SetDataSource(ds) ' SetDataSource only once with the entire dataset


        Dim frm As New frmReporte
        frm.CrystalReportViewer1.ReportSource = rpt
        frm.ShowDialog()

    End Sub

    Dim NroPaginasImpresas As Integer = 0
    Dim ctaFilasVentas, ctaFilasNotas As Integer
    Private Sub frmreporteVentasRango_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(texto)
        Me.texto.Multiline = True
        Me.texto.Visible = False
        Me.cbxMoneda.SelectedIndex = 0
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList
        Dim Max As Long = 1000
        Dim z As Long
        texto.Clear()

        'ProgressBar1.Visible = True
        'ProgressBar1.Minimum = 1
        'ProgressBar1.Maximum = Max
        'ProgressBar1.Value = 1
        'ProgressBar1.Step = 1

        totVenta = 0 : totInteres = 0 : totInicial = 0 : totNotaCredito = 0 : totLetrasPeriodo = 0 : totPrecioVenta = 0
        totVentaContado = 0 : totVentaCredito = 0 : totCuotaInicial = 0 : totAnticipoCuota = 0 : totVentaTarjeta = 0
        totVentaTarjetaOferta = 0 : totVentaTarjetaRemate = 0 : totVentaOferta = 0 : totVentaRemate = 0
        oDataSet = New DataSet()

        Try
            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("select * from vtaCabecera where idMoneda=" & Me.cbxMoneda.SelectedIndex + 1 &
            " and fecOperacion>='" & CDate(Me.dtpFechaInicio.Text) & "' and fecOperacion<='" & CDate(Me.dtpFechaLimite.Text) & "'", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            Dim daNotaCreditoCa As SqlDataAdapter = New SqlDataAdapter("select * from notaCreditoCa where idMoneda=" & Me.cbxMoneda.SelectedIndex + 1 &
            " and fecOperacion>='" & CDate(Me.dtpFechaInicio.Text) & "' and fecOperacion<='" & CDate(Me.dtpFechaLimite.Text) & "'", Connection)
            daNotaCreditoCa.Fill(oDataSet, "notaCreditoCa")

            If Me.oDataSet.Tables(0).Rows.Count <= 0 And Me.oDataSet.Tables(1).Rows.Count <= 0 Then
                MsgBox("No existe información para procesar de este periodo o moneda.", MsgBoxStyle.Critical)
                Connection.Close()
                Exit Sub
            End If

            ctaFilasVentas = Me.oDataSet.Tables(0).Rows.Count
            ctaFilasNotas = Me.oDataSet.Tables(1).Rows.Count

            If ctaFilasVentas >= 1 Then
                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    Dim daVtaDetalle As SqlDataAdapter = New SqlDataAdapter("select * from vtaDetalle where tipDocumento='" & Me.oDataSet.Tables(0).Rows(i).Item(0) & "' and numDocumento=" & Me.oDataSet.Tables(0).Rows(i).Item(2) & " ", Connection)
                    daVtaDetalle.Fill(oDataSet, "vtaDetalle")
                Next
            Else
                Dim daVtaDetalle As SqlDataAdapter = New SqlDataAdapter("select * from vtaDetalle", Connection)
                daVtaDetalle.Fill(oDataSet, "vtaDetalle")
            End If

            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("select * from productos", Connection)
            daProductos.Fill(oDataSet, "productos")

            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("select * from clientes", Connection)
            daClientes.Fill(oDataSet, "clientes")

            Dim daGlosas As SqlDataAdapter = New SqlDataAdapter("select * from glosasFacturas", Connection)
            daGlosas.Fill(oDataSet, "glosasFacturas")

            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("select * from letrasClientes", Connection)
            daLetras.Fill(oDataSet, "letrasClientes")

            'Sumando totales vtaCabecera
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                If Me.oDataSet.Tables(0).Rows(i).Item(4).ToString <> "" Then
                    If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                        totVenta += Me.oDataSet.Tables(0).Rows(i).Item(8)
                        totInteres += Me.oDataSet.Tables(0).Rows(i).Item(10)
                        If Me.oDataSet.Tables(0).Rows(i).Item(4) = 1 Then totInicial += Me.oDataSet.Tables(0).Rows(i).Item(14)
                    Else
                        totVenta += Me.oDataSet.Tables(0).Rows(i).Item(9)
                        totInteres += Me.oDataSet.Tables(0).Rows(i).Item(10)
                        If Me.oDataSet.Tables(0).Rows(i).Item(4) = 1 Then totInicial += Me.oDataSet.Tables(0).Rows(i).Item(14)
                    End If
                End If
            Next

            'Totalizando items
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                If Me.oDataSet.Tables(0).Rows(i).Item(4).ToString <> "" Then
                    Select Case Me.oDataSet.Tables(0).Rows(i).Item(4)
                        Case 0
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaContado += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaContado += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 1
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaCredito += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaCredito += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 3
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totCuotaInicial += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totCuotaInicial += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 4
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totAnticipoCuota += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totAnticipoCuota += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 5
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaTarjeta += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaTarjeta += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 6
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaTarjetaOferta += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaTarjetaOferta += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 7
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaTarjetaRemate += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaTarjetaRemate += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 8
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaOferta += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaOferta += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                        Case 9
                            If Me.oDataSet.Tables(0).Rows(i).Item(15) = 1 Then
                                totVentaRemate += Me.oDataSet.Tables(0).Rows(i).Item(8)
                            Else
                                totVentaRemate += Me.oDataSet.Tables(0).Rows(i).Item(9)
                            End If
                    End Select
                End If
            Next

            Dim colNomCliente As DataColumn = New DataColumn()
            colNomCliente.Caption = "nomCliente"
            colNomCliente.ColumnName = "nomCliente"
            Me.oDataSet.Tables(0).Columns.Add(colNomCliente)

            Dim colTotalLetras As DataColumn = New DataColumn()
            colTotalLetras.Caption = "totLetras"
            colTotalLetras.ColumnName = "totLetras"
            Me.oDataSet.Tables(0).Columns.Add(colTotalLetras)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If Me.oDataSet.Tables(4).Rows.Item(x).Item(0) = Me.oDataSet.Tables(0).Rows.Item(i).Item(6) Then
                        oDataRow = Me.oDataSet.Tables(0).Rows(i)
                        oDataRow(22) = Me.oDataSet.Tables(4).Rows(x).Item(1)
                        Exit For
                    End If
                Next x
            Next i

            Dim oDataRow1 As DataRow
            Dim totalLetrasCliente As Decimal
            Dim tipoOperacion As Byte
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRow1 = Me.oDataSet.Tables(0).Rows(i)
                If Me.oDataSet.Tables(0).Rows(i).Item(21).ToString <> "A" Then
                    tipoOperacion = Me.oDataSet.Tables(0).Rows.Item(i).Item(4)
                    If tipoOperacion = 1 Then
                        For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                            If Me.oDataSet.Tables(6).Rows.Item(x).Item(0) = Me.oDataSet.Tables(0).Rows.Item(i).Item(5) Then
                                totalLetrasCliente += Me.oDataSet.Tables(6).Rows.Item(x).Item(4)
                                oDataRow1(23) = totalLetrasCliente
                            End If
                        Next x
                        totLetrasPeriodo += totalLetrasCliente
                        totalLetrasCliente = 0
                    Else
                        oDataRow1(23) = 0
                    End If
                Else
                    oDataRow1(23) = 0
                End If
            Next i

            Dim colNomProducto As DataColumn = New DataColumn()
            colNomProducto.Caption = "nomProducto"
            colNomProducto.ColumnName = "nomProducto"
            Me.oDataSet.Tables(2).Columns.Add(colNomProducto)

            Dim colPrecioLista As DataColumn = New DataColumn()
            colPrecioLista.Caption = "preLista"
            colPrecioLista.ColumnName = "preLista"
            Me.oDataSet.Tables(2).Columns.Add(colPrecioLista)

            If ctaFilasVentas >= 1 Then
                Dim oDataRow2 As DataRow
                For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    oDataRow2 = Me.oDataSet.Tables(2).Rows(i)
                    For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                        If Me.oDataSet.Tables(3).Rows(x).Item(0) = Me.oDataSet.Tables(2).Rows(i).Item(3) Then
                            oDataRow2(10) = Me.oDataSet.Tables(3).Rows(x).Item(2) 'nomProducto
                            oDataRow2(11) = Me.oDataSet.Tables(3).Rows(x).Item(8) 'preLista
                            Exit For
                        End If
                    Next x
                Next i
            End If

            'Reportando notas de credito
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                If Me.oDataSet.Tables(1).Rows(i).Item(12) = 1 Then
                    totNotaCredito += Me.oDataSet.Tables(1).Rows(i).Item(7)
                Else
                    totNotaCredito += Me.oDataSet.Tables(1).Rows(i).Item(8)
                End If
            Next

            Dim colNomCliente1 As DataColumn = New DataColumn()
            colNomCliente1.AllowDBNull = True
            colNomCliente1.Caption = "Nombre Cliente"
            colNomCliente1.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(1).Columns.Add(colNomCliente1)

            Dim colGlosas As DataColumn = New DataColumn()
            colGlosas.AllowDBNull = True
            colGlosas.Caption = "Glosas"
            colGlosas.ColumnName = "glosas"
            Me.oDataSet.Tables(1).Columns.Add(colGlosas)

            Dim oDataRow3 As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    If Me.oDataSet.Tables(4).Rows.Item(x).Item(0) = Me.oDataSet.Tables(1).Rows.Item(i).Item(6) Then
                        oDataRow3 = Me.oDataSet.Tables(1).Rows(i)
                        oDataRow3(16) = Me.oDataSet.Tables(4).Rows.Item(x).Item(1)
                    End If
                Next x
            Next i

            Dim oDataRow4 As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
                    If Me.oDataSet.Tables(5).Rows.Item(x).Item(0) = Me.oDataSet.Tables(1).Rows.Item(i).Item(0) And
                    Me.oDataSet.Tables(5).Rows.Item(x).Item(1) = Me.oDataSet.Tables(1).Rows.Item(i).Item(2) Then
                        oDataRow4 = Me.oDataSet.Tables(1).Rows(i)
                        oDataRow4(17) = Me.oDataSet.Tables(5).Rows.Item(x).Item(2)
                    End If
                Next x
            Next i

            For z = 1 To Max
                'ProgressBar1.PerformStep()
                'Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            NroPaginasImpresas = 0
            totPrecioVenta = 0

            Dim en, t As Keys
            Dim enter, tab As Char
            Dim totVentas, totParcial As Decimal

            en = Keys.Enter
            t = Keys.Tab
            enter = Convert.ToChar(en)
            tab = Convert.ToChar(t)

            'ProgressBar1.Visible = True
            'ProgressBar1.Minimum = 1
            ''ProgressBar1.Maximum = Max
            'ProgressBar1.Value = 1
            'ProgressBar1.Step = 1

            texto.Text = texto.Text & enter
            texto.Text = texto.Text & txtNombreEmpresa & enter & enter
            texto.Text = texto.Text & txtRUCEmpresa & "                                                                                                                                 Fecha :" & DateTime.Today & enter & enter
            texto.Text = texto.Text & "                                                              REPORTE VENTAS MENSUAL EN " & Me.cbxMoneda.Text & " DEL " & Me.dtpFechaInicio.Text & " AL " & Me.dtpFechaLimite.Text & enter
            texto.Text = texto.Text & "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & enter
            texto.Text = texto.Text & "Fecha   Guia    N°Doc. Cliente         Con    Interes     Total   C.Inicial Letras Cobrar T.Ventas  S Precio Lista Precio Venta   Dscto.      Articulo                            " & enter
            texto.Text = texto.Text & "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & enter
            If oDataSet.Tables(0).Rows.Count() >= 1 Then
                'ProgressBar1.Maximum = Me.oDataSet.Tables(0).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(12), 6) & VisualBasic.Right(Me.oDataSet.Tables(0).Rows(x).Item(12), 2).PadRight(3)
                    texto.Text = texto.Text & Me.oDataSet.Tables(0).Rows(x).Item(3).ToString.PadRight(7)
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(0), 1) & Me.oDataSet.Tables(0).Rows(x).Item(2).ToString.PadRight(6)
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(22).ToString, 15).PadRight(16)

                    If Me.oDataSet.Tables(0).Rows(x).Item(4).ToString <> "" Then
                        texto.Text = texto.Text & arrayConceptos(Me.oDataSet.Tables(0).Rows(x).Item(4)).PadRight(3)
                    Else
                        texto.Text = texto.Text & "S/C".PadRight(3)
                    End If

                    texto.Text = texto.Text & Format(Me.oDataSet.Tables(0).Rows(x).Item(10), "###,##0.00").ToString.PadLeft(10)
                    If Me.oDataSet.Tables(0).Rows(x).Item(15) > 1 Then
                        texto.Text = texto.Text & Format(Me.oDataSet.Tables(0).Rows(x).Item(9), "###,##0.00").ToString.PadLeft(13)
                        totParcial = Me.oDataSet.Tables(0).Rows(x).Item(9).ToString.PadLeft(15)
                    Else
                        texto.Text = texto.Text & Format(Me.oDataSet.Tables(0).Rows(x).Item(8), "###,##0.00").ToString.PadLeft(13)
                        totParcial = Me.oDataSet.Tables(0).Rows(x).Item(8).ToString.PadLeft(15)
                    End If

                    texto.Text = texto.Text & Format(Me.oDataSet.Tables(0).Rows(x).Item(14), "###,##0.00").ToString.PadLeft(12)
                    totVentas = totParcial + Me.oDataSet.Tables(0).Rows(x).Item(14)
                    texto.Text = texto.Text & Format(CDec(Me.oDataSet.Tables(0).Rows(x).Item(23)), "###,##0.00").ToString.PadLeft(12)

                    If Me.oDataSet.Tables(0).Rows(x).Item(4) = "1" Then
                        texto.Text = texto.Text & Format(totVentas, "###,##0.00").ToString.PadLeft(10)
                    Else
                        texto.Text = texto.Text & ("0.00").PadLeft(10)
                    End If

                    texto.Text = texto.Text & Me.oDataSet.Tables(0).Rows(x).Item(19).ToString.PadLeft(3)

                    Dim space As Integer = 10
                    For y As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                        If Me.oDataSet.Tables(2).Rows(y).Item(0) = Me.oDataSet.Tables(0).Rows(x).Item(0) And
                           Me.oDataSet.Tables(2).Rows(y).Item(2) = Me.oDataSet.Tables(0).Rows(x).Item(2) Then
                            Dim dscto As Decimal
                            dscto = Me.oDataSet.Tables(2).Rows(y).Item(11) - Me.oDataSet.Tables(2).Rows(y).Item(6)
                            totPrecioVenta = totPrecioVenta + Me.oDataSet.Tables(2).Rows(y).Item(6)
                            texto.Text = texto.Text & Format(CDec(Me.oDataSet.Tables(2).Rows(y).Item(11)), "##,##0.00").ToString.PadLeft(space)
                            texto.Text = texto.Text & Format(Me.oDataSet.Tables(2).Rows(y).Item(6), "##,##0.00").ToString.PadLeft(12)
                            texto.Text = texto.Text & Format(dscto, "##,##0.00").PadLeft(12) & "  "

                            If Me.oDataSet.Tables(0).Rows(x).Item(4) <> 10 Then
                                texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(2).Rows(y).Item(10).ToString, 30).PadRight(32)
                            Else
                                texto.Text = texto.Text & "FACTURACION OTROS CONCEPTOS".PadRight(30)
                            End If

                            texto.Text = texto.Text & enter
                            space = 112
                        End If
                    Next y
                    space = 0
                    'ProgressBar1.PerformStep()
                    'Me.lblAvance.Text = Format((i + 1) / (Me.oDataSet.Tables(0).Rows.Count() - 1), "0%")
                    Application.DoEvents()
                Next x
                texto.Text = texto.Text & "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & enter
                texto.Text = texto.Text & "Resumen Total Ventas: "
                texto.Text = texto.Text & Format(totInteres, "###,##0.00").PadLeft(30)
                texto.Text = texto.Text & Format(totVenta, "###,##0.00").PadLeft(13) & " "
                texto.Text = texto.Text & Format(totInicial, "###,##0.00").PadLeft(10) & " "
                texto.Text = texto.Text & Format(totLetrasPeriodo, "###,##0.00").PadLeft(11) & " "
                texto.Text = texto.Text & Format(totPrecioVenta, "###,##0.00").PadLeft(35) & enter
                texto.Text = texto.Text & "Resumen General Movimientos" & " " & Me.cbxMoneda.Text & enter
                texto.Text = texto.Text & "------------------------------------" & enter
                texto.Text = texto.Text & "Total Venta Contado       : " & Format(totVentaContado, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Credito       : " & Format(totVentaCredito, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Cuotas Iniciales    : " & Format(totCuotaInicial, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Anticipo Cuotas     : " & Format(totAnticipoCuota, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Tarjeta       : " & Format(totVentaTarjeta, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Tarjeta Oferta: " & Format(totVentaTarjetaOferta, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Tarjeta Remate: " & Format(totVentaTarjetaRemate, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Oferta        : " & Format(totVentaOferta, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta Remate        : " & Format(totVentaRemate, "###,##0.00").PadLeft(12) & enter
                texto.Text = texto.Text & "Total Venta               : " & Format(totPrecioVenta + totInteres, "###,##0.00").PadLeft(12) & enter & enter
            End If

            If oDataSet.Tables(1).Rows.Count() >= 1 Then
                texto.Text = texto.Text & "NOTAS DE CREDITO DEL MES: " & enter
                texto.Text = texto.Text & "-------------------------------------------------------------------------------------------------------------------------" & enter
                texto.Text = texto.Text & "Fecha           N°Doc.   Cliente                   Importe                                  Glosa                      S " & enter
                texto.Text = texto.Text & "-------------------------------------------------------------------------------------------------------------------------" & enter
                For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    'ProgressBar1.Maximum = Me.oDataSet.Tables(1).Rows.Count() - 1
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(1).Rows(i).Item(11), 6) & VisualBasic.Right(Me.oDataSet.Tables(1).Rows(i).Item(11), 2).PadRight(10)
                    texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(0).ToString
                    texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(2).ToString.PadRight(4) & "  "
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(1).Rows(i).Item(16).ToString, 20).PadRight(21)
                    If Me.oDataSet.Tables(1).Rows(i).Item(12) > 1 Then
                        texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(8).ToString.PadLeft(13) & "                              "
                    Else
                        texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(7).ToString.PadLeft(13) & "                              "
                    End If
                    texto.Text = texto.Text & VisualBasic.Left(Me.oDataSet.Tables(1).Rows(i).Item(17).ToString, 20).PadRight(22)
                    texto.Text = texto.Text & Me.oDataSet.Tables(1).Rows(i).Item(15).ToString.PadLeft(10)
                    texto.Text = texto.Text & enter
                    'ProgressBar1.PerformStep()
                    'Me.lblAvance.Text = Format((i + 1) / (Me.oDataSet.Tables(0).Rows.Count() - 1), "0%")
                    Application.DoEvents()
                Next i
                texto.Text = texto.Text & "-------------------------------------------------------------------------------------------------------------------------" & enter
                texto.Text = texto.Text & "Resumen Total Notas Crédito: "
                texto.Text = texto.Text & Format(totNotaCredito, "###,##0.00").PadLeft(29)
            End If

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

            e.Graphics.MeasureString(Mid(texto.Text, +1), Fuente, New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, NroLineasRelleno)
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
        'If NroPaginasImpresas > 1 Then
        '    e.Graphics.DrawString("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 5)
        '    e.Graphics.DrawString("Fecha   Guia    N°Doc. Cliente         Con    Interes     Total   C.Inicial Letras Cobrar T.Ventas  S Precio Lista Precio Venta   Dscto.      Articulo                            ", Fuente1, Brushes.Black, 0, 20)
        '    e.Graphics.DrawString("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 35)
        'End If
        'If e.HasMorePages <> False Then
        '    e.Graphics.DrawString("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 910)
        '    e.Graphics.DrawString("Resumen Total Ventas: ", Fuente1, Brushes.Black, 0, 925)
        '    e.Graphics.DrawString(Format(totalInteres, "###,##0.00"), Fuente1, Brushes.Black, 330, 925)
        '    e.Graphics.DrawString(Format(totalVenta, "###,##0.00"), Fuente1, Brushes.Black, 425, 925)
        '    e.Graphics.DrawString(Format(totalInicial, "###,##0.00"), Fuente1, Brushes.Black, 525, 925)
        '    e.Graphics.DrawString(Format(totalLetrasPeriodo, "###,##0.00"), Fuente1, Brushes.Black, 610, 925)
        '    e.Graphics.DrawString(Format(totalPrecioVenta, "###,##0.00"), Fuente1, Brushes.Black, 890, 925)
        '    e.Graphics.DrawString("Resumen General Movimientos" & " " & Me.cbxMoneda.Text, Fuente1, Brushes.Black, 0, 940)
        '    e.Graphics.DrawString("------------------------------------", Fuente1, Brushes.Black, 0, 955)
        '    e.Graphics.DrawString("Total Venta Contado   : " & Format(totalVentaContado, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 970)
        '    e.Graphics.DrawString("Total Venta Tarjeta   : " & Format(totalVentaTarjeta, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 985)
        '    e.Graphics.DrawString("Total Cuotas Iniciales: " & Format(totalCuotaInicial, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 1000)
        '    e.Graphics.DrawString("Total Anticipo Cuotas : " & Format(totalAnticipoCuota, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 1015)
        '    e.Graphics.DrawString("Total Otros Pagos     : " & Format(totalOtrosPagos, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 1030)
        '    e.Graphics.DrawString("Total Venta           : " & Format(totalPrecioVenta + totalInteres, "###,##0.00").PadLeft(12), Fuente1, Brushes.Black, 0, 1045)
        'End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Ventas"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 1394
        Dim Alto As Short = 1090

        Dim left As Short = 0
        Dim top As Short = 50
        Dim bottom As Short = 165
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
    Private Sub dtpFechaInicio_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaInicio.ValueChanged
        ctaFilasVentas = 0
        ctaFilasNotas = 0
    End Sub
    Private Sub dtpFechaLimite_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaLimite.ValueChanged
        ctaFilasVentas = 0
        ctaFilasNotas = 0
    End Sub
    Private Sub cbxMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxMoneda.SelectedIndexChanged
        ctaFilasVentas = 0
        ctaFilasNotas = 0
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class