Imports Microsoft
Imports System.Data.SqlClient
Public Class frmcierreCajaRangos
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim ctaTablas, ctaTablas1 As Integer
    Dim NroPaginasImpresas As Integer = 0

    Dim ventaContadoSoles, ventaContadoDolares, ventaContadoEuros As Decimal
    Dim amortizarLetraSoles, amortizarLetraDolares, amortizarLetraEuros As Decimal
    Dim cancelarLetraSoles, cancelarLetraDolares, cancelarLetraEuros As Decimal
    Dim cuotaInicialSoles, cuotaInicialDolares, cuotaInicialEuros As Decimal
    Dim anticipoCuotaSoles, anticipoCuotaDolares, anticipoCuotaEuros As Decimal
    Dim ventaTarjetaSoles, ventaTarjetaDolares As Decimal
    Dim ventaTarjetaOfertaSoles, ventaTarjetaOfertaDolares As Decimal
    Dim ventaTarjetaRemateSoles, ventaTarjetaRemateDolares As Decimal
    Dim ventaOfertaSoles, ventaOfertaDolares As Decimal
    Dim ventaRemateSoles, ventaRemateDolares As Decimal
    Dim otrosPagosSoles, otrosPagosDolares, otrosPagosEuros As Decimal
    Dim cobroInteresSoles, cobroInteresDolares As Decimal
    Dim cargoOperacionSoles, cargoOperacionDolares As Decimal

    Dim totalSoles, totalDolares, totalEuros As Decimal
    Dim totalSalidasSoles, totalSalidasDolares, totalSalidasEuros As Decimal

    Dim totalChequesMN, totalChequesME As Decimal
    Dim prestamoPersonalSoles, prestamoPersonalDolares, prestamoPersonalEuros As Decimal
    Dim prestamoClientesSoles, prestamoClientesDolares, prestamoClientesEuros As Decimal
    Dim pagosDiversosSoles, pagosDiversosDolares, pagosDiversosEuros As Decimal

    Dim arrayConceptos() As String = {"Venta Contado", "Amortización Letra", "Cancelación Letra", "Cuota Inicial", "A.Cuota Inicial", "Venta Tarjeta", "V.Tarjeta Oferta", "V.Tarjeta Remate", "Venta Oferta", "Venta Remate", "Otros Pagos", "Cobro Interés", "Cargo Operación"}
    Dim arrayConceptos1() As String = {"Préstamo a Personal", "Préstamo a Clientes", "Pagos Diversos"}

    Dim arrayTiposPago() As String = {"Cheque MN", "Cheque ME", "Efectivo MN", "Efectivo ME", "Pago Tarjeta", "Transf/Abono Cta"}
    Dim arrayMonedas() As String = {"S/", "$", "€"}

    Dim arrayRecibos(250, 17) As Object
    Dim arrayRecibos1(250, 17) As Object

    Dim totalCaja, totalCajaDia, totalCajaMes As Single
    Dim dia, mes, ctaFilas, cantidadDias, cantidadMeses As Integer
    Private Sub frmLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim Max As Long = 1000
        Dim z As Long

        oDataSet = New DataSet()
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1
        iniciarMatrices()
        iniciarTotales()

        If CDate(Me.dtpFechaLiquidacionF.Text) < CDate(Me.dtpFechaLiquidacionI.Text) Then
            MsgBox("Rango no admitido, fecha límite tiene que ser mayor o igual a fecha inicio  !  !  !", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If CDate(Me.dtpFechaLiquidacionF.Text).Year <> CDate(Me.dtpFechaLiquidacionI.Text).Year Then
            MsgBox("Lo sentimos, solo se permite reporte hasta de un año  !  !  !", MsgBoxStyle.Critical)
            Exit Sub
        End If

        cantidadDias = DateDiff(DateInterval.Day, CDate(Me.dtpFechaLiquidacionI.Text), CDate(Me.dtpFechaLiquidacionF.Text)) + 1
        cantidadMeses = DateDiff(DateInterval.Month, CDate(Me.dtpFechaLiquidacionI.Text), CDate(Me.dtpFechaLiquidacionF.Text)) + 1
        totalCaja = 0 : totalCajaDia = 0 : totalCajaMes = 0 : mes = 0 : dia = 0

        Try
            Dim daRecibosClientes As New SqlDataAdapter("select * from recibosClientes where fecEmision>='" & CDate(dtpFechaLiquidacionI.Text) & "' and fecEmision<='" & CDate(dtpFechaLiquidacionF.Text) & "'", Connection)
            daRecibosClientes.Fill(oDataSet, "recibosClientes")

            Dim daRecibosSalidas As New SqlDataAdapter("select * from recibosSalidas where fecEmision>='" & CDate(dtpFechaLiquidacionI.Text) & "' and fecEmision<='" & CDate(dtpFechaLiquidacionF.Text) & "'", Connection)
            daRecibosSalidas.Fill(oDataSet, "recibosSalidas")

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("select * from clientes", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daCheques As New SqlDataAdapter("select * from datosCheques", Connection)
            daCheques.Fill(oDataSet, "datosCheques")

            If Me.oDataSet.Tables(0).Rows.Count <= 0 And Me.oDataSet.Tables(1).Rows.Count <= 0 Then
                MsgBox("No existe información del día para procesar.", MsgBoxStyle.Information)
                Me.oDataSet.Tables.Clear()
                Exit Sub
            End If

            ctaTablas = oDataSet.Tables(0).Rows.Count()
            ctaTablas1 = oDataSet.Tables(1).Rows.Count()

            If ctaTablas > 0 Then
                Dim colNombre As DataColumn = New DataColumn()
                colNombre.AllowDBNull = True
                colNombre.Caption = "Nombre Cliente"
                colNombre.ColumnName = "nombreCliente"
                Me.oDataSet.Tables(0).Columns.Add(colNombre)

                Dim colMontoChequeMN As DataColumn = New DataColumn()
                colMontoChequeMN.AllowDBNull = True
                colMontoChequeMN.Caption = "Monto ChequeMN"
                colMontoChequeMN.ColumnName = "montoChequeMN"
                Me.oDataSet.Tables(0).Columns.Add(colMontoChequeMN)

                Dim colMontoChequeME As DataColumn = New DataColumn()
                colMontoChequeME.AllowDBNull = True
                colMontoChequeME.Caption = "Monto ChequeME"
                colMontoChequeME.ColumnName = "montoChequeME"
                Me.oDataSet.Tables(0).Columns.Add(colMontoChequeME)

                Dim colTipoPago As DataColumn = New DataColumn()
                colTipoPago.AllowDBNull = True
                colTipoPago.Caption = "Tipo Pago"
                colTipoPago.ColumnName = "tipoPago"
                Me.oDataSet.Tables(0).Columns.Add(colTipoPago)

                Dim oDataRow As DataRow
                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                        If Me.oDataSet.Tables(2).Rows.Item(x).Item(0) = Me.oDataSet.Tables(0).Rows.Item(i).Item(8) Then
                            oDataRow = Me.oDataSet.Tables(0).Rows(i)
                            oDataRow(17) = Me.oDataSet.Tables(2).Rows.Item(x).Item(1)
                        End If
                    Next x
                Next i

                Dim oDataRow1 As DataRow
                Dim oDataRow2 As DataRow
                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                        If Me.oDataSet.Tables(3).Rows.Item(x).Item(1) = Me.oDataSet.Tables(0).Rows.Item(i).Item(0) And _
                        (Me.oDataSet.Tables(3).Rows.Item(x).Item(2) = 0 Or Me.oDataSet.Tables(3).Rows.Item(x).Item(2) = 1) Then
                            oDataRow1 = Me.oDataSet.Tables(0).Rows(i)
                            oDataRow2 = Me.oDataSet.Tables(0).Rows(i)
                            If Me.oDataSet.Tables(3).Rows.Item(x).Item(2) = 0 Then
                                'oDataRow1(18) = Me.oDataSet.Tables(3).Rows.Item(x).Item(6)
                                oDataRow1(18) = ""
                                totalChequesMN += Me.oDataSet.Tables(3).Rows.Item(x).Item(6)
                                'Me.oDataSet.Tables(0).Rows(i).Item(3) = 0.0
                            Else
                                'oDataRow2(19) = Me.oDataSet.Tables(3).Rows.Item(x).Item(6)
                                oDataRow1(19) = ""
                                totalChequesME += Me.oDataSet.Tables(3).Rows.Item(x).Item(6)
                                'Me.oDataSet.Tables(0).Rows(i).Item(4) = 0.0
                            End If
                        End If
                    Next x
                Next i

                Dim oDataRow3 As DataRow
                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                        oDataRow3 = Me.oDataSet.Tables(0).Rows(i)
                        If Me.oDataSet.Tables(3).Rows.Item(x).Item(1) = Me.oDataSet.Tables(0).Rows.Item(i).Item(0) Then
                            oDataRow3(20) = Me.oDataSet.Tables(3).Rows.Item(x).Item(2)
                        End If
                    Next x
                Next i

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    arrayRecibos(i, 0) = Me.oDataSet.Tables(0).Rows(i).Item(0)
                    arrayRecibos(i, 1) = VisualBasic.Left(Me.oDataSet.Tables(0).Rows(i).Item(17), 20)
                    arrayRecibos(i, 2) = arrayConceptos(Me.oDataSet.Tables(0).Rows(i).Item(1))
                    Select Case Me.oDataSet.Tables(0).Rows(i).Item(1)
                        Case 0
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaContadoSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    ventaContadoDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    ventaContadoEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 1
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                amortizarLetraSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    amortizarLetraDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    amortizarLetraEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 2
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                cancelarLetraSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    cancelarLetraDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    cancelarLetraEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 3
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                cuotaInicialSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    cuotaInicialDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    cuotaInicialEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 4
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                anticipoCuotaSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    anticipoCuotaDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    anticipoCuotaEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 5
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaTarjetaSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                ventaTarjetaDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 6
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaTarjetaOfertaSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                ventaTarjetaOfertaDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 7
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaTarjetaRemateSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                ventaTarjetaRemateDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 8
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaOfertaSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                ventaOfertaDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 9
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                ventaRemateSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                ventaRemateDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 10
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                otrosPagosSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(14) = 2 Then
                                    otrosPagosDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                Else
                                    otrosPagosEuros += Me.oDataSet.Tables(0).Rows(i).Item(4)
                                End If
                            End If
                        Case 11
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                cobroInteresSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                cobroInteresDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                        Case 12
                            If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                                cargoOperacionSoles += Me.oDataSet.Tables(0).Rows(i).Item(3)
                            Else
                                cargoOperacionDolares += Me.oDataSet.Tables(0).Rows(i).Item(4)
                            End If
                    End Select
                    arrayRecibos(i, 3) = Me.oDataSet.Tables(0).Rows(i).Item(2)
                    If Me.oDataSet.Tables(0).Rows(i).Item(5) > 0 Then
                        arrayRecibos(i, 4) = Me.oDataSet.Tables(0).Rows(i).Item(5)
                    Else
                        arrayRecibos(i, 4) = ""
                    End If
                    arrayRecibos(i, 5) = Me.oDataSet.Tables(0).Rows(i).Item(10)
                    arrayRecibos(i, 6) = Me.oDataSet.Tables(0).Rows(i).Item(11)
                    arrayRecibos(i, 7) = Me.oDataSet.Tables(0).Rows(i).Item(14) - 1
                    'arrayRecibos(i, 7) = arrayMonedas(Me.oDataSet.Tables(0).Rows(i).Item(14) - 1)
                    If Me.oDataSet.Tables(0).Rows(i).Item(14) = 1 Then
                        arrayRecibos(i, 8) = Me.oDataSet.Tables(0).Rows(i).Item(3)
                        arrayRecibos(i, 9) = 0
                    Else
                        arrayRecibos(i, 9) = Me.oDataSet.Tables(0).Rows(i).Item(4)
                        arrayRecibos(i, 8) = 0
                    End If
                    arrayRecibos(i, 10) = Me.oDataSet.Tables(0).Rows(i).Item(13) 'Descuento
                    arrayRecibos(i, 11) = 0
                    arrayRecibos(i, 12) = Me.oDataSet.Tables(0).Rows(i).Item(15) 'Tipo Cambio
                    arrayRecibos(i, 13) = Me.oDataSet.Tables(0).Rows(i).Item(18) 'Monto CH MN
                    arrayRecibos(i, 14) = Me.oDataSet.Tables(0).Rows(i).Item(19) 'Monto CH ME
                    arrayRecibos(i, 15) = Me.oDataSet.Tables(0).Rows(i).Item(20) 'Tipo Pago
                Next
                totalSoles += ventaContadoSoles + amortizarLetraSoles + cancelarLetraSoles + cuotaInicialSoles + anticipoCuotaSoles + ventaTarjetaSoles + ventaTarjetaOfertaSoles + ventaTarjetaRemateSoles + ventaOfertaSoles + ventaRemateSoles + otrosPagosSoles + cobroInteresSoles + cargoOperacionSoles
                totalDolares += ventaContadoDolares + amortizarLetraDolares + cancelarLetraDolares + cuotaInicialDolares + anticipoCuotaDolares + ventaTarjetaDolares + ventaTarjetaOfertaDolares + ventaTarjetaRemateDolares + ventaOfertaDolares + ventaTarjetaDolares + otrosPagosDolares + cobroInteresDolares + cargoOperacionDolares
                totalEuros += ventaContadoEuros + amortizarLetraEuros + cancelarLetraEuros + cuotaInicialEuros + anticipoCuotaEuros + otrosPagosEuros
            Else
                MsgBox("No hay información 'recibos de entrada' para procesar en esta fecha.", MsgBoxStyle.Critical)
            End If

            If ctaTablas1 > 0 Then
                Dim colNombre1 As DataColumn = New DataColumn()
                colNombre1.AllowDBNull = True
                colNombre1.Caption = "Nombre Cliente"
                colNombre1.ColumnName = "nombreCliente"
                Me.oDataSet.Tables(1).Columns.Add(colNombre1)

                Dim oDataRow1 As DataRow
                For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                        If Me.oDataSet.Tables(2).Rows.Item(x).Item(0) = Me.oDataSet.Tables(1).Rows.Item(i).Item(8) Then
                            oDataRow1 = Me.oDataSet.Tables(1).Rows(i)
                            oDataRow1(17) = Me.oDataSet.Tables(2).Rows.Item(x).Item(1)
                        End If
                    Next x
                Next i

                For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    arrayRecibos1(i, 0) = Me.oDataSet.Tables(1).Rows(i).Item(0)
                    arrayRecibos1(i, 1) = VisualBasic.Left(Me.oDataSet.Tables(1).Rows(i).Item(17), 20)
                    arrayRecibos1(i, 2) = arrayConceptos1(Me.oDataSet.Tables(1).Rows(i).Item(1))
                    Select Case Me.oDataSet.Tables(1).Rows(i).Item(1)
                        Case 0
                            If Me.oDataSet.Tables(1).Rows(i).Item(14) = 1 Then
                                prestamoPersonalSoles += Me.oDataSet.Tables(1).Rows(i).Item(3)
                            Else
                                prestamoPersonalDolares += Me.oDataSet.Tables(1).Rows(i).Item(4)
                            End If
                        Case 1
                            If Me.oDataSet.Tables(1).Rows(i).Item(14) = 1 Then
                                prestamoClientesSoles += Me.oDataSet.Tables(1).Rows(i).Item(3)
                            Else
                                prestamoClientesDolares += Me.oDataSet.Tables(1).Rows(i).Item(4)
                            End If
                        Case 2
                            If Me.oDataSet.Tables(1).Rows(i).Item(14) = 1 Then
                                pagosDiversosSoles += Me.oDataSet.Tables(1).Rows(i).Item(3)
                            Else
                                pagosDiversosDolares += Me.oDataSet.Tables(1).Rows(i).Item(4)
                            End If
                    End Select
                    arrayRecibos1(i, 3) = Me.oDataSet.Tables(1).Rows(i).Item(2)
                    If Me.oDataSet.Tables(1).Rows(i).Item(5) > 0 Then
                        arrayRecibos1(i, 4) = Me.oDataSet.Tables(1).Rows(i).Item(5)
                    Else
                        arrayRecibos1(i, 4) = ""
                    End If
                    arrayRecibos1(i, 5) = Me.oDataSet.Tables(1).Rows(i).Item(10)
                    arrayRecibos1(i, 6) = Me.oDataSet.Tables(1).Rows(i).Item(11)
                    'arrayRecibos1(i, 7) = Me.oDataSet.Tables(1).Rows(i).Item(14) - 1
                    arrayRecibos1(i, 7) = arrayMonedas(Me.oDataSet.Tables(1).Rows(i).Item(14) - 1)
                    If Me.oDataSet.Tables(1).Rows(i).Item(14) = 1 Then
                        arrayRecibos1(i, 8) = Me.oDataSet.Tables(1).Rows(i).Item(3)
                    Else
                        arrayRecibos1(i, 9) = Me.oDataSet.Tables(1).Rows(i).Item(4)
                    End If
                    arrayRecibos1(i, 10) = 0
                    arrayRecibos1(i, 11) = 0
                    arrayRecibos1(i, 12) = Me.oDataSet.Tables(1).Rows(i).Item(15)
                    arrayRecibos1(i, 13) = Me.oDataSet.Tables(1).Rows(i).Item(6)
                Next
                totalSalidasSoles += prestamoPersonalSoles + prestamoClientesSoles + pagosDiversosSoles
                totalSalidasDolares += prestamoPersonalDolares + prestamoClientesDolares + pagosDiversosDolares
                totalSalidasEuros += prestamoPersonalEuros + prestamoClientesEuros + pagosDiversosEuros
            Else
                MsgBox("No hay información de 'recibos de salida' para procesar en esta fecha.", MsgBoxStyle.Critical)
            End If

            For z = 1 To Max
                ProgressBar1.PerformStep()
                Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            If ctaTablas > 0 Or ctaTablas1 > 0 Then
                Dim en, t As Keys
                Dim enter, tab As Char
                en = Keys.Enter
                t = Keys.Tab

                NroPaginasImpresas = 0
                enter = Convert.ToChar(en)
                tab = Convert.ToChar(t)
                te.Text = "                                    " & txtNombreEmpresa & enter & _
                txtRUCEmpresa & "                                                                                 Fecha :" & DateTime.Today & enter & enter
                te.Text = te.Text & "                            REPORTE DIARIO DE CAJA DEL " & Me.dtpFechaLiquidacionI.Text & " AL " & Me.dtpFechaLiquidacionF.Text & enter & enter
                te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------" & enter
                te.Text = te.Text & "N°Rec.  Cliente         Concep.       T.Pago          F.Emisión   EFE. MN.   EFE. ME.  CH.MN. CH.ME.  N° C u o t a" & enter
                te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------" & enter
                If ctaTablas > 0 Then te.Text = te.Text & "Entradas" & enter
                Dim entra As Boolean
                For Meses As Int16 = 1 To cantidadMeses
                    For Dias As Int16 = 1 To cantidadDias
                        entra = False
                        For i As Integer = arrayRecibos.GetLowerBound(0) To arrayRecibos.GetUpperBound(0)
                            If CStr(arrayRecibos(i, 0)) <> "" Then
                                If CDate(Me.dtpFechaLiquidacionI.Text).Day + dia = CDate(VisualBasic.Left(arrayRecibos(i, 5), 10)).Day And _
                                   CDate(Me.dtpFechaLiquidacionI.Text).Month = CDate(VisualBasic.Left(arrayRecibos(i, 5), 10)).Month Then
                                    te.Text = te.Text & arrayRecibos(i, 0).ToString.PadRight(8)
                                    te.Text = te.Text & VisualBasic.Left(CStr(arrayRecibos(i, 1)), 15).PadRight(16)
                                    te.Text = te.Text & VisualBasic.Left(arrayRecibos(i, 2), 12).ToString.PadRight(13)
                                    te.Text = te.Text & VisualBasic.Left(arrayTiposPago(IIf(IsDBNull(arrayRecibos(i, 15)), 2, arrayRecibos(i, 15))), 15).ToString.PadRight(17)
                                    te.Text = te.Text & VisualBasic.Left(CStr(arrayRecibos(i, 5)), 6) & VisualBasic.Right(CStr(arrayRecibos(i, 5)), 2).PadRight(3)
                                    te.Text = te.Text & Format(arrayRecibos(i, 8), "#####0.00").PadLeft(10) 'efectivo MN
                                    te.Text = te.Text & Format(arrayRecibos(i, 9), "#####0.00").PadLeft(10) 'efectivo ME
                                    'te.Text = te.Text & Format(arrayRecibos(i, 10), "#####0.00").PadLeft(10) 'Descuento
                                    'te.Text = te.Text & Format(arrayRecibos(i, 12), "##0.00").PadLeft(7) 'Tipo Cambio
                                    te.Text = te.Text & arrayRecibos(i, 13).ToString.PadLeft(10) 'CH MN
                                    te.Text = te.Text & arrayRecibos(i, 14).ToString.PadLeft(10) 'CH ME
                                    If CStr(Trim(arrayRecibos(i, 2))) = "Amortización Letra" Or CStr(Trim(arrayRecibos(i, 2))) = "Cancelación Letra" Then
                                        te.Text = te.Text & arrayRecibos(i, 4).ToString.PadRight(2)
                                        te.Text = te.Text & VisualBasic.Left(arrayRecibos(i, 3), 12).ToString.Trim 'N° Cuota
                                    End If
                                    te.Text = te.Text & enter
                                    totalCajaDia += CSng(arrayRecibos(i, 8))
                                    entra = True
                                End If
                            End If
                        Next i
                        If entra = True Then
                            te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------" & enter
                            te.Text = te.Text & ("Total Caja del Dia:S/. " & Format(totalCajaDia, "###,##0.00").ToString.PadLeft(50))
                            te.Text = te.Text & enter & enter
                        End If
                        dia += 1
                        totalCajaDia = 0
                    Next Dias
                    Me.dtpFechaLiquidacionI.Text = "01" & "-" & CDate(Me.dtpFechaLiquidacionF.Text).Month & "-" & CDate(Me.dtpFechaLiquidacionF.Text).Year
                    dia = 0
                Next Meses

                If ctaTablas1 > 0 Then te.Text = te.Text & "Salidas" & enter
                For i As Integer = arrayRecibos1.GetLowerBound(0) To arrayRecibos1.GetUpperBound(0)
                    If CStr(arrayRecibos1(i, 0)) <> "" Then
                        te.Text = te.Text & arrayRecibos1(i, 0).ToString.PadRight(8)
                        te.Text = te.Text & VisualBasic.Left(CStr(arrayRecibos1(i, 1)), 15).PadRight(16)
                        te.Text = te.Text & arrayRecibos1(i, 2).ToString.PadRight(20)
                        'te.Text = te.Text & arrayRecibos1(i, 3).ToString.PadRight(20)
                        te.Text = te.Text & arrayRecibos1(i, 4).ToString.PadRight(3)
                        te.Text = te.Text & (VisualBasic.Left(CStr(arrayRecibos1(i, 5)), 6) & VisualBasic.Right(CStr(arrayRecibos1(i, 5)), 2)).PadLeft(15)
                        'te.Text = te.Text & VisualBasic.Left(CStr(arrayRecibos1(i, 6)), 6) & VisualBasic.Right(CStr(arrayRecibos1(i, 6)), 2).PadRight(3)
                        'te.Text = te.Text & arrayRecibos1(i, 7).ToString.PadRight(3)
                        te.Text = te.Text & Format(arrayRecibos1(i, 8), "#####0.00").PadLeft(11)
                        'te.Text = te.Text & Format(arrayRecibos1(i, 9), "#####0.00").PadLeft(10)
                        'te.Text = te.Text & Format(arrayRecibos1(i, 10), "#####0.00").PadLeft(10)
                        'te.Text = te.Text & Format(arrayRecibos1(i, 11), "#####0.00").PadLeft(10)
                        'te.Text = te.Text & Format(arrayRecibos1(i, 12), "#####0.00").PadLeft(6)
                        'te.Text = te.Text & arrayRecibos(i, 13).ToString.PadLeft(10)
                        te.Text = te.Text & enter
                    End If
                Next i
                te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------" & enter
                te.Text = te.Text & "Resumen Total Entradas" & enter
                te.Text = te.Text & Space(33) & "Soles        Dolares          " & enter
                te.Text = te.Text & Space(20) & "-------------------------------------------" & enter
                te.Text = te.Text & "Venta Contado         :" & Format(ventaContadoSoles, "###,##0.00").PadLeft(15) & Format(ventaContadoDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Amortización Letras   :" & Format(amortizarLetraSoles, "###,##0.00").PadLeft(15) & Format(amortizarLetraDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Cancelación Letras    :" & Format(cancelarLetraSoles, "###,##0.00").PadLeft(15) & Format(cancelarLetraDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Cuota Inicial         :" & Format(cuotaInicialSoles, "###,##0.00").PadLeft(15) & Format(cuotaInicialDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Anticipo Cuota Inicial:" & Format(anticipoCuotaSoles, "###,##0.00").PadLeft(15) & Format(anticipoCuotaDolares, "###,##0.00").PadLeft(15) & enter

                te.Text = te.Text & "Venta Tarjeta         :" & Format(ventaTarjetaSoles, "###,##0.00").PadLeft(15) & Format(ventaTarjetaDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Venta Tarjeta Oferta  :" & Format(ventaTarjetaOfertaSoles, "###,##0.00").PadLeft(15) & Format(ventaTarjetaOfertaDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Venta Tarjeta Remate  :" & Format(ventaTarjetaRemateSoles, "###,##0.00").PadLeft(15) & Format(ventaTarjetaRemateDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Venta Oferta          :" & Format(ventaOfertaSoles, "###,##0.00").PadLeft(15) & Format(ventaOfertaDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Venta Remate          :" & Format(ventaRemateSoles, "###,##0.00").PadLeft(15) & Format(ventaRemateDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Cobro Interés         :" & Format(cobroInteresSoles, "###,##0.00").PadLeft(15) & Format(cobroInteresDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Cargo Operación       :" & Format(cargoOperacionSoles, "###,##0.00").PadLeft(15) & Format(cargoOperacionDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Otros Pagos           :" & Format(otrosPagosSoles, "###,##0.00").PadLeft(15) & Format(otrosPagosDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & Space(20) & "-------------------------------------------" & enter
                te.Text = te.Text & "Total Caja            :" & Format(totalSoles, "###,##0.00").PadLeft(15) & Format(totalDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Total Cheques         :" & Format(totalChequesMN, "###,##0.00").PadLeft(15) & Format(totalChequesME, "###,##0.00").PadLeft(15) & enter & enter
                te.Text = te.Text & "Resumen Total Salidas" & enter
                te.Text = te.Text & "Préstamo a Personal   :" & Format(prestamoPersonalSoles, "###,##0.00").PadLeft(15) & Format(prestamoPersonalDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Préstamo a Clientes   :" & Format(prestamoClientesSoles, "###,##0.00").PadLeft(15) & Format(prestamoClientesDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & "Pagos Diversos        :" & Format(pagosDiversosSoles, "###,##0.00").PadLeft(15) & Format(pagosDiversosDolares, "###,##0.00").PadLeft(15) & enter
                te.Text = te.Text & Space(20) & "-------------------------------------------" & enter
                te.Text = te.Text & "Total Salida Caja     :" & Format(totalSalidasSoles, "###,##0.00").PadLeft(15) & Format(totalSalidasDolares, "###,##0.00").PadLeft(15)

                If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    configurarImpresion()
                    PrintPreviewDialog1.Document = PrintDocument1
                    'PrintDocument1.DefaultPageSettings.Landscape = True
                    PrintPreviewDialog1.ShowDialog()
                End If

                PrintDialog1.Document = PrintDocument1
                If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    configurarImpresion()
                    'PrintDocument1.DefaultPageSettings.Landscape = True
                    PrintDocument1.Print()
                End If
                Me.oDataSet.Tables.Clear()
            Else
                MsgBox("No se puede imprimir si no hay información previamente procesada.", MsgBoxStyle.Critical)
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

            ' Comenzamos a contar las paginas impresas
            NroPaginasImpresas += 1

            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente, New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, NroLineasRelleno)
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
        VistaPrevia("Courier New", 9, te.Text, e)
        Dim Fuente1 As New Font("Courier New", 9)
        If NroPaginasImpresas > 1 Then
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 5)
            e.Graphics.DrawString("N°Rec.  Cliente         Concep.    N°  F.Emis.   EFE.MN. EFE.ME.  Dscto. T.Pagado   T.C.     CH.MN.    CH.ME.     ", Fuente1, Brushes.Black, 0, 20)
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 35)
        End If
        If e.HasMorePages <> False Then
            e.Graphics.DrawString("------------------------------------------------------------------------------------------------------------------", Fuente1, Brushes.Black, 0, 1025)
        End If
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Caja"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 894
        Dim Alto As Short = 1090

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
    Private Sub iniciarMatrices()
        For x As Integer = arrayRecibos.GetLowerBound(0) To arrayRecibos.GetUpperBound(0)
            For y As Integer = arrayRecibos.GetLowerBound(1) To arrayRecibos.GetUpperBound(1)
                arrayRecibos(x, y) = ""
            Next
        Next

        For x As Integer = arrayRecibos1.GetLowerBound(0) To arrayRecibos1.GetUpperBound(0)
            For y As Integer = arrayRecibos1.GetLowerBound(1) To arrayRecibos1.GetUpperBound(1)
                arrayRecibos1(x, y) = ""
            Next
        Next
    End Sub
    Private Sub iniciarTotales()
        ctaTablas = 0 : ctaTablas1 = 0
        ventaContadoSoles = 0 : ventaContadoDolares = 0 : ventaContadoEuros = 0
        amortizarLetraSoles = 0 : amortizarLetraDolares = 0 : amortizarLetraEuros = 0
        cancelarLetraSoles = 0 : cancelarLetraDolares = 0 : cancelarLetraEuros = 0
        cuotaInicialSoles = 0 : cuotaInicialDolares = 0 : cuotaInicialEuros = 0
        anticipoCuotaSoles = 0 : anticipoCuotaDolares = 0 : anticipoCuotaEuros = 0
        ventaTarjetaSoles = 0 : ventaTarjetaDolares = 0
        ventaTarjetaOfertaSoles = 0 : ventaTarjetaOfertaDolares = 0
        ventaTarjetaRemateSoles = 0 : ventaTarjetaRemateDolares = 0
        ventaOfertaSoles = 0 : ventaOfertaDolares = 0
        ventaRemateSoles = 0 : ventaRemateDolares = 0
        otrosPagosSoles = 0 : otrosPagosDolares = 0 : otrosPagosEuros = 0
        cobroInteresSoles = 0 : cobroInteresDolares = 0
        cargoOperacionSoles = 0 : cargoOperacionDolares = 0

        totalSoles = 0 : totalDolares = 0 : totalEuros = 0
        totalChequesMN = 0 : totalChequesME = 0
        totalSalidasSoles = 0 : totalSalidasDolares = 0 : totalSalidasEuros = 0

        prestamoPersonalSoles = 0 : prestamoPersonalDolares = 0 : prestamoPersonalEuros = 0
        prestamoClientesSoles = 0 : prestamoClientesDolares = 0 : prestamoClientesEuros = 0
        pagosDiversosSoles = 0 : pagosDiversosDolares = 0 : pagosDiversosEuros = 0
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class