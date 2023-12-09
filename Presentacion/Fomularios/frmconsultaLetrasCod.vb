Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Public Class frmconsultaLetrasCod
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim sumaSubTotales, sumaAmortizacionesMN, sumaAmortizacionesME As Decimal
    Dim tipoCambio, intFinanciero, totVentaMN, totVentaME As Decimal
    Dim nomCliente, dirCliente, dniCliente As String
    Dim nomGarante, dirGarante, dniGarante As String
    Dim arrayMonedas() As String = {"S", "$", "�"}
    Dim arrayConceptos() As String = {"V.Contado", "V.Cr�dito", "V.Tarjeta", "CI", "AC"}
    Dim codCliente As Short
    Private Sub frmconsultaLetrasCod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        sumaAmortizacionesMN = 0
        sumaAmortizacionesME = 0
        Me.txtCliente.Text = ""

        If flag <> 0 Then
            Me.txtcodCliente.Text = dniCliente
        End If

        If Me.txtcodCliente.Text = "" Or (Len(Me.txtcodCliente.Text) < 8 Or Len(Me.txtcodCliente.Text) > 11) Then
            MsgBox("Ingrese n�mero DNI o RUC v�lido.", MsgBoxStyle.Information)
            Me.txtcodCliente.Text = ""
            Me.txtcodCliente.Focus()
            Exit Sub
        End If

        Try
            oDataSet = New DataSet()
            Connection.Open()

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where dni Like '" & Me.txtcodCliente.Text & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("Documento no registrado.", MsgBoxStyle.Information)
                Me.txtcodCliente.Text = ""
                Me.txtcodCliente.Focus()
                Connection.Close()
                Exit Sub
            End If

            If flag <> 0 Then
                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,idCliente,status " & _
                "FROM letrasClientes where status<>'X' and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' and numLetra like '" & numeroLetra & "' order by numCorrelativo", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")
            Else
                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,idCliente,status " & _
                "FROM letrasClientes where status<>'X' and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")
            End If

            Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            Dim daCostosFijos As SqlDataAdapter = New SqlDataAdapter("SELECT *from costosFijos", Connection)
            daCostosFijos.Fill(oDataSet, "costosFijos")

            Dim daFactorInteres As SqlDataAdapter = New SqlDataAdapter("SELECT *from factorInteres", Connection)
            daFactorInteres.Fill(oDataSet, "factorInteres")

            Connection.Close()

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(8) = 1 Then
                    oDataRowMoneda(11) = "S/."
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(8) = 2 Then
                        oDataRowMoneda(11) = "$"
                    Else
                        oDataRowMoneda(11) = "�"
                    End If
                End If
            Next i

            Dim colAmortizacion As DataColumn = New DataColumn()
            colAmortizacion.AllowDBNull = True
            colAmortizacion.DataType = System.Type.GetType("System.Decimal")
            colAmortizacion.Caption = "Adelantos"
            colAmortizacion.ColumnName = "adelantos"
            colAmortizacion.DefaultValue = 0.0
            Me.oDataSet.Tables(1).Columns.Add(colAmortizacion)

            Dim colSaldo As DataColumn = New DataColumn()
            colSaldo.AllowDBNull = True
            colSaldo.Caption = "saldoPagar"
            colSaldo.ColumnName = "saldoPagar"
            Me.oDataSet.Tables(1).Columns.Add(colSaldo)

            Dim oDataRowAmortiza As DataRow
            Dim oDataRowSaldos As DataRow

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowAmortiza = Me.oDataSet.Tables(1).Rows(i)
                oDataRowSaldos = Me.oDataSet.Tables(1).Rows(i)
                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    If Me.oDataSet.Tables(1).Rows(i).Item(0) = Me.oDataSet.Tables(2).Rows(x).Item(2) And _
                       Me.oDataSet.Tables(1).Rows(i).Item(1) = Me.oDataSet.Tables(2).Rows(x).Item(5) And _
                       (Me.oDataSet.Tables(2).Rows(x).Item(1) = "1" Or Me.oDataSet.Tables(2).Rows(x).Item(1) = "2") Then

                        If Me.oDataSet.Tables(2).Rows(x).Item(14) > 1 Then
                            sumaAmortizacionesME += Me.oDataSet.Tables(2).Rows(x).Item(4)
                            oDataRowAmortiza(12) = sumaAmortizacionesME
                        Else
                            sumaAmortizacionesMN += Me.oDataSet.Tables(2).Rows(x).Item(3)
                            oDataRowAmortiza(12) = sumaAmortizacionesMN
                        End If
                    End If

                    If Me.oDataSet.Tables(1).Rows(i).Item(8) > 1 Then
                        oDataRowSaldos(13) = Me.oDataSet.Tables(1).Rows(i).Item(3) - sumaAmortizacionesME
                    Else
                        oDataRowSaldos(13) = Me.oDataSet.Tables(1).Rows(i).Item(2) - sumaAmortizacionesMN
                    End If
                Next x
                sumaAmortizacionesME = 0
                sumaAmortizacionesMN = 0
            Next i

            Dim colDiasVencidos As DataColumn = New DataColumn()
            colDiasVencidos.DataType = System.Type.GetType("System.Int32")
            colDiasVencidos.Caption = "dVencidos"
            colDiasVencidos.ColumnName = "dVencidos"
            colDiasVencidos.DefaultValue = 0
            Me.oDataSet.Tables(1).Columns.Add(colDiasVencidos)

            Dim oDataRowDVencidos As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowDVencidos = Me.oDataSet.Tables(1).Rows(i)
                If Trim(Me.oDataSet.Tables(1).Rows(i).Item(10)) = "C" Then
                    oDataRowDVencidos(14) = DateDiff(DateInterval.Day, Me.oDataSet.Tables(1).Rows(i).Item(5), Me.oDataSet.Tables(1).Rows(i).Item(6))
                End If
            Next i

            Dim colInteres As DataColumn = New DataColumn()
            colInteres.DataType = System.Type.GetType("System.Decimal")
            colInteres.Caption = "Interes"
            colInteres.ColumnName = "interes"
            colInteres.DefaultValue = 0.0
            Me.oDataSet.Tables(1).Columns.Add(colInteres)

            Dim oDataRowInteres As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowInteres = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(14) >= 9 Then

                    If Me.oDataSet.Tables(1).Rows(i).Item(2) <= Me.oDataSet.Tables(3).Rows(0).Item(2) Then

                        oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(0).Item(3) + _
                                              (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                    Else
                        If Me.oDataSet.Tables(1).Rows(i).Item(2) <= Me.oDataSet.Tables(3).Rows(1).Item(2) Then

                            oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(1).Item(3) + _
                                                  (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        Else
                            oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(2).Item(3) + _
                                                  (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        End If

                    End If

                End If

            Next i

            Me.lblNombre.Text = oDataSet.Tables(0).Rows(0).Item(1)
            Me.dirCliente = oDataSet.Tables(0).Rows(0).Item(2)
            Me.dniCliente = oDataSet.Tables(0).Rows(0).Item(3)
            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(0).DisplayIndex = 0
                .Columns(0).ReadOnly = True
                .Columns(1).DisplayIndex = 1
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True
                .Columns(1).Width = 50
                .Columns(2).DisplayIndex = 3
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).ReadOnly = False
                .Columns(2).Width = 80
                .Columns(3).DisplayIndex = 4
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).ReadOnly = False
                .Columns(3).Width = 80
                .Columns(4).DisplayIndex = 7
                .Columns(4).ReadOnly = False
                .Columns(4).Width = 70
                .Columns(5).DisplayIndex = 8
                .Columns(5).ReadOnly = False
                .Columns(5).Width = 70
                .Columns(6).DisplayIndex = 9
                .Columns(6).ReadOnly = True
                .Columns(6).Width = 70
                .Columns(7).DisplayIndex = 10
                .Columns(7).ReadOnly = True
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DisplayIndex = 11
                .Columns(8).ReadOnly = True
                .Columns(8).Visible = False
                .Columns(9).DisplayIndex = 12
                .Columns(9).ReadOnly = True
                .Columns(9).Visible = False
                .Columns(10).DisplayIndex = 13
                .Columns(10).ReadOnly = True
                .Columns(10).Width = 40
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).DisplayIndex = 2
                .Columns(11).ReadOnly = True
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).Width = 50
                .Columns(12).DisplayIndex = 5
                .Columns(12).ReadOnly = True
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "####0.00"
                .Columns(12).Width = 70
                .Columns(13).DisplayIndex = 6
                .Columns(13).ReadOnly = True
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).Width = 80
                .Columns(14).ReadOnly = True
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).Width = 50
                .Columns(15).ReadOnly = True
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(15).DefaultCellStyle.Format = "####0.00"
                .Columns(15).Width = 70
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub txtCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyUp
        Try
            sumaAmortizacionesMN = 0
            sumaAmortizacionesME = 0
            Me.txtcodCliente.Text = ""

            oDataSet = New DataSet()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where nombres Like '" & Me.txtCliente.Text & "%" & "' ", Connection)
            Connection.Open()
            daCliente.Fill(oDataSet, "cliente")

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,fecPago,numRecibo,idMoneda,idCliente,status " & _
            "FROM letrasClientes where status<>'X' and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            Dim daCostosFijos As SqlDataAdapter = New SqlDataAdapter("SELECT *from costosFijos", Connection)
            daCostosFijos.Fill(oDataSet, "costosFijos")

            Dim daFactorInteres As SqlDataAdapter = New SqlDataAdapter("SELECT *from factorInteres", Connection)
            daFactorInteres.Fill(oDataSet, "factorInteres")
            Connection.Close()

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(8) = 1 Then
                    oDataRowMoneda(11) = "S/."
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(8) = 2 Then
                        oDataRowMoneda(11) = "$"
                    Else
                        oDataRowMoneda(11) = "�"
                    End If
                End If
            Next i

            Dim colAmortizacion As DataColumn = New DataColumn()
            colAmortizacion.AllowDBNull = True
            colAmortizacion.DataType = System.Type.GetType("System.Decimal")
            colAmortizacion.Caption = "Adelantos"
            colAmortizacion.ColumnName = "adelantos"
            colAmortizacion.DefaultValue = 0.0
            Me.oDataSet.Tables(1).Columns.Add(colAmortizacion)

            Dim colSaldo As DataColumn = New DataColumn()
            colSaldo.AllowDBNull = True
            colSaldo.Caption = "saldoPagar"
            colSaldo.ColumnName = "saldoPagar"
            Me.oDataSet.Tables(1).Columns.Add(colSaldo)

            Dim oDataRowAmortiza As DataRow
            Dim oDataRowSaldos As DataRow

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowAmortiza = Me.oDataSet.Tables(1).Rows(i)
                oDataRowSaldos = Me.oDataSet.Tables(1).Rows(i)
                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    If Me.oDataSet.Tables(1).Rows(i).Item(0) = Me.oDataSet.Tables(2).Rows(x).Item(2) And _
                       Me.oDataSet.Tables(1).Rows(i).Item(1) = Me.oDataSet.Tables(2).Rows(x).Item(5) And _
                       (Me.oDataSet.Tables(2).Rows(x).Item(1) = "1" Or Me.oDataSet.Tables(2).Rows(x).Item(1) = "2") Then

                        If Me.oDataSet.Tables(2).Rows(x).Item(14) > 1 Then
                            sumaAmortizacionesME += Me.oDataSet.Tables(2).Rows(x).Item(4)
                            oDataRowAmortiza(12) = sumaAmortizacionesME
                        Else
                            sumaAmortizacionesMN += Me.oDataSet.Tables(2).Rows(x).Item(3)
                            oDataRowAmortiza(12) = sumaAmortizacionesMN
                        End If
                    End If

                    If Me.oDataSet.Tables(1).Rows(i).Item(8) > 1 Then
                        oDataRowSaldos(13) = Me.oDataSet.Tables(1).Rows(i).Item(3) - sumaAmortizacionesME
                    Else
                        oDataRowSaldos(13) = Me.oDataSet.Tables(1).Rows(i).Item(2) - sumaAmortizacionesMN
                    End If
                Next x
                sumaAmortizacionesME = 0
                sumaAmortizacionesMN = 0
            Next i

            Dim colDiasVencidos As DataColumn = New DataColumn()
            colDiasVencidos.DataType = System.Type.GetType("System.Int32")
            colDiasVencidos.Caption = "dVencidos"
            colDiasVencidos.ColumnName = "dVencidos"
            colDiasVencidos.DefaultValue = 0
            Me.oDataSet.Tables(1).Columns.Add(colDiasVencidos)

            Dim oDataRowDVencidos As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowDVencidos = Me.oDataSet.Tables(1).Rows(i)
                If Trim(Me.oDataSet.Tables(1).Rows(i).Item(10)) = "C" Then
                    oDataRowDVencidos(14) = DateDiff(DateInterval.Day, Me.oDataSet.Tables(1).Rows(i).Item(5), Me.oDataSet.Tables(1).Rows(i).Item(6))
                End If
            Next i

            Dim colInteres As DataColumn = New DataColumn()
            colInteres.DataType = System.Type.GetType("System.Decimal")
            colInteres.Caption = "Interes"
            colInteres.ColumnName = "interes"
            colInteres.DefaultValue = 0.0
            Me.oDataSet.Tables(1).Columns.Add(colInteres)

            Dim oDataRowInteres As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowInteres = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(14) >= 9 Then

                    If Me.oDataSet.Tables(1).Rows(i).Item(2) <= Me.oDataSet.Tables(3).Rows(0).Item(2) Then

                        oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(0).Item(3) + _
                                              (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                    Else
                        If Me.oDataSet.Tables(1).Rows(i).Item(2) <= Me.oDataSet.Tables(3).Rows(1).Item(2) Then

                            oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(1).Item(3) + _
                                                  (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        Else
                            oDataRowInteres(15) = Me.oDataSet.Tables(3).Rows(2).Item(3) + _
                                                  (Me.oDataSet.Tables(1).Rows(i).Item(2) * Me.oDataSet.Tables(1).Rows(i).Item(14) * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        End If

                    End If

                End If

            Next i

            Me.lblNombre.Text = oDataSet.Tables(0).Rows(0).Item(1)
            Me.dirCliente = oDataSet.Tables(0).Rows(0).Item(2)
            Me.dniCliente = oDataSet.Tables(0).Rows(0).Item(3)
            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(0).DisplayIndex = 0
                .Columns(0).ReadOnly = True
                .Columns(1).DisplayIndex = 1
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True
                .Columns(1).Width = 50
                .Columns(2).DisplayIndex = 3
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).ReadOnly = False
                .Columns(2).Width = 80
                .Columns(3).DisplayIndex = 4
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).ReadOnly = False
                .Columns(3).Width = 80
                .Columns(4).DisplayIndex = 7
                .Columns(4).ReadOnly = False
                .Columns(4).Width = 70
                .Columns(5).DisplayIndex = 8
                .Columns(5).ReadOnly = False
                .Columns(5).Width = 70
                .Columns(6).DisplayIndex = 9
                .Columns(6).ReadOnly = True
                .Columns(6).Width = 70
                .Columns(7).DisplayIndex = 10
                .Columns(7).ReadOnly = True
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DisplayIndex = 11
                .Columns(8).ReadOnly = True
                .Columns(8).Visible = False
                .Columns(9).DisplayIndex = 12
                .Columns(9).ReadOnly = True
                .Columns(9).Visible = False
                .Columns(10).DisplayIndex = 13
                .Columns(10).ReadOnly = True
                .Columns(10).Width = 40
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).DisplayIndex = 2
                .Columns(11).ReadOnly = True
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).Width = 50
                .Columns(12).DisplayIndex = 5
                .Columns(12).ReadOnly = True
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "####0.00"
                .Columns(12).Width = 70
                .Columns(13).DisplayIndex = 6
                .Columns(13).ReadOnly = True
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).Width = 80
                .Columns(14).ReadOnly = True
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).Width = 50
                .Columns(15).ReadOnly = True
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(15).DefaultCellStyle.Format = "####0.00"
                .Columns(15).Width = 70

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Dim en, t As Keys
        Dim numDocGenACI As String = VisualBasic.Trim(tipMovimiento & numDocumento)
        Dim enter, tab As Char
        en = Keys.Enter
        t = Keys.Tab
        enter = Convert.ToChar(en)
        tab = Convert.ToChar(t)

        If Me.dgvLetras.RowCount <= 0 Then
            MsgBox("No hay informaci�n procesada para imprimir.", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try
            Me.codCliente = Me.dgvLetras.Rows(0).Cells(9).Value

            oDataSet = New DataSet()
            Connection.Open()
            Dim daGarantes As SqlDataAdapter = New SqlDataAdapter("SELECT *from garantes where idCliente=" & Me.codCliente & "", Connection)
            daGarantes.Fill(oDataSet, "garantes")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count > 0 Then
                Me.nomGarante = oDataSet.Tables(0).Rows(0).Item(1)
                Me.dirGarante = oDataSet.Tables(0).Rows(0).Item(2)
                Me.dniGarante = oDataSet.Tables(0).Rows(0).Item(3)
            Else
                Me.nomGarante = ""
                Me.dirGarante = ""
                Me.dniGarante = ""
            End If

            te.Text = _
            "                                  Comercial Oriente Hnos. SAC" & enter & enter & _
            "Movimientos de Cuenta Corriente" & enter & enter & _
            "Datos del Cliente:" & enter & _
            "Nombre Cliente: " & Me.lblNombre.Text & enter & _
            "Dir.   Cliente: " & Me.dirCliente & enter & _
            "Doc.   Cliente: " & Me.dniCliente & enter & enter & _
            "Datos del Garante:" & enter & _
            "Nombre Garante: " & Me.nomGarante & enter & _
            "Dir.   Garante: " & Me.dirGarante & enter & _
            "Doc.   Garante: " & Me.dniGarante & enter & _
            "Fecha         : " & Me.dtpFecha.Text & enter
            te.Text = te.Text & "-------------------------------------------------------------------------------------------------" & enter
            te.Text = te.Text & "N�m.Letra     Num. Mon. Monto MN Monto ME Fec.Emis.  Fec.Venc.  Fec.Pago     Amortiz.    Saldos S" & enter
            te.Text = te.Text & "-------------------------------------------------------------------------------------------------"
            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(0).Value.ToString.PadRight(14) & " "
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(1).Value.ToString.PadRight(4)
                te.Text = te.Text & Me.arrayMonedas(Me.dgvLetras.Rows(i).Cells(8).Value - 1) & " "
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(2).Value.ToString.PadLeft(10)
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(3).Value.ToString.PadLeft(10) & " "
                te.Text = te.Text & VisualBasic.Left(Me.dgvLetras.Rows(i).Cells(4).Value.ToString, 10).PadLeft(10) & " "
                te.Text = te.Text & VisualBasic.Left(Me.dgvLetras.Rows(i).Cells(5).Value.ToString, 10).PadLeft(10) & " "
                te.Text = te.Text & VisualBasic.Left(Me.dgvLetras.Rows(i).Cells(6).Value.ToString, 10).PadLeft(10) & " "
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(12).Value.ToString.PadLeft(10)
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(13).Value.ToString.PadLeft(10) & " "
                te.Text = te.Text & Me.dgvLetras.Rows(i).Cells(10).Value & enter
            Next
            te.Text = te.Text & "-------------------------------------------------------------------------------------------------" & enter

            If flag <> 0 Then
                Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT  *from vtaCabecera where tipDocumento='" & tipMovimiento & _
                "' and numDocumento='" & numDocumento & "' and status<>'A' ", Connection)
                daVtaCabecera.Fill(oDataSet, "vtaCabecera")

                te.Text = te.Text & enter & "Datos del Producto" & enter
                te.Text = te.Text & "-------------------------------------------------------------------------------------------------"
                te.Text = te.Text & "C�d.  Descripci�n               Marca      Modelo             Precio      Cantidad     Total     " & enter
                te.Text = te.Text & "-------------------------------------------------------------------------------------------------"
                Me.totVentaMN = Me.oDataSet.Tables(1).Rows(0).Item(8)
                Me.totVentaME = Me.oDataSet.Tables(1).Rows(0).Item(9)
                Me.intFinanciero = Me.oDataSet.Tables(1).Rows(0).Item(10)
                'Me.dtpFecha.Text = Me.oDataSet.Tables(0).Rows(0).Item(12)  
                Me.tipoCambio = Me.oDataSet.Tables(1).Rows(0).Item(16)

                Dim daVtaDetalle = New SqlDataAdapter("SELECT *from vtaDetalle where numDocumento='" & numDocumento & _
                "' and tipDocumento='" & tipMovimiento & "'", Connection)
                daVtaDetalle.Fill(oDataSet, "vtaDetalle")

                Dim colNombreProducto As DataColumn = New DataColumn()
                colNombreProducto.Caption = "Descripci�n Producto"
                colNombreProducto.ColumnName = "descripcionProducto"
                Me.oDataSet.Tables(2).Columns.Add(colNombreProducto)

                Dim colMarcaProducto As DataColumn = New DataColumn()
                colMarcaProducto.Caption = "Marca"
                colMarcaProducto.ColumnName = "marca"
                Me.oDataSet.Tables(2).Columns.Add(colMarcaProducto)

                Dim colModeloProducto As DataColumn = New DataColumn()
                colModeloProducto.Caption = "Modelo"
                colModeloProducto.ColumnName = "modelo"
                Me.oDataSet.Tables(2).Columns.Add(colModeloProducto)

                For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT *from productos where idProducto='" & Me.oDataSet.Tables(2).Rows(i).Item(3) & "' ", Connection)
                    daProductos.Fill(oDataSet, "productos")
                Next

                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes where numDocGenACI='" & numDocGenACI & "'", Connection)
                daRecibos.Fill(oDataSet, "recibosClientes")
                Connection.Close()

                Dim oDataRow As DataRow
                For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    For x As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                        If Me.oDataSet.Tables(3).Rows.Item(x).Item(0) = Me.oDataSet.Tables(2).Rows.Item(i).Item(3) Then
                            oDataRow = Me.oDataSet.Tables(2).Rows(i)
                            oDataRow(11) = Me.oDataSet.Tables(3).Rows.Item(x).Item(2)
                            oDataRow(12) = Me.oDataSet.Tables(3).Rows.Item(x).Item(4)
                            oDataRow(13) = Me.oDataSet.Tables(3).Rows.Item(x).Item(5)
                        End If
                    Next x
                Next i

                For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    te.Text = te.Text & Me.oDataSet.Tables(2).Rows.Item(i).Item(3).ToString.PadRight(5) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(2).Rows.Item(i).Item(11).ToString, 25).PadRight(25) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(2).Rows.Item(i).Item(12).ToString, 10).PadRight(10) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(2).Rows.Item(i).Item(13).ToString, 15).PadRight(15) & " "
                    te.Text = te.Text & Me.oDataSet.Tables(2).Rows.Item(i).Item(4).ToString.PadLeft(10)
                    te.Text = te.Text & Me.oDataSet.Tables(2).Rows.Item(i).Item(5).ToString.PadLeft(10)
                    te.Text = te.Text & Me.oDataSet.Tables(2).Rows.Item(i).Item(6).ToString.PadLeft(16) & enter
                    Me.sumaSubTotales += Me.oDataSet.Tables(2).Rows.Item(i).Item(6)
                Next
                te.Text = te.Text & "-------------------------------------------------------------------------------------------------" & enter
                te.Text = te.Text & "SubTotal   :" & Me.sumaSubTotales.ToString.PadLeft(83) & enter
                te.Text = te.Text & "Inter�s    :" & Me.intFinanciero.ToString.PadLeft(83) & enter
                te.Text = te.Text & "Total MN   :" & Me.totVentaMN.ToString.PadLeft(83) & enter
                te.Text = te.Text & "Total ME   :" & Me.totVentaME.ToString.PadLeft(83) & enter
                te.Text = te.Text & "Tipo Cambio:" & Me.tipoCambio & enter
                If Me.oDataSet.Tables(4).Rows.Count >= 1 Then
                    For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                        te.Text = te.Text & "Recibo " & Me.arrayConceptos(Me.oDataSet.Tables(4).Rows(i).Item(1)) & " N� " & Me.oDataSet.Tables(4).Rows(i).Item(0).ToString & " " & Me.arrayMonedas(Me.oDataSet.Tables(4).Rows(i).Item(14) - 1) & "/. " & Me.oDataSet.Tables(4).Rows(i).Item(3).ToString & enter
                    Next
                Else
                    te.Text = te.Text & "No registra cuota inicial."
                End If
            End If

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            configurarImpresion()
            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.Print()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub VistaPrevia(ByVal TipoFuente As String, ByVal Tama�oFuente As Byte, ByVal TextoImpresion As String, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim Fuente As New Font(TipoFuente, Tama�oFuente)
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
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "A4"
        Dim Tama�oPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 890
        Dim Alto As Short = 1102

        Dim left As Short = 25
        Dim top As Short = 50
        Dim bottom As Short = 50
        Dim right As Short = 25

        Tama�oPersonal = New Printing.PaperSize(nombrePapel, Ancho, Alto)
        margenes = New Printing.Margins(left, right, top, bottom)

        ' Asignamos la impresora seleccionada
        'prdDocumento.PrinterSettings = ImpresoraActual
        ' Asignamos el tama�o personalizado de papel
        Me.PrintDocument1.DefaultPageSettings.PaperSize = Tama�oPersonal
        ' Asignamos los m�rgenes al documento
        Me.PrintDocument1.DefaultPageSettings.Margins = margenes
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            Dim oFrmAcceso As New frmaccesoAdministrador()
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            If dgvLetras.Rows.Count > 0 Then
                If MsgBox("Est� seguro de modificar datos de las letras?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    For i As Integer = 0 To dgvLetras.Rows.Count - 1
                        SqlString = "UPDATE letrasClientes Set impLetra='" & Me.dgvLetras.Rows(i).Cells(2).Value & "',impLetraME='" & _
                        dgvLetras.Rows(i).Cells(3).Value & "',fecVencimiento='" & Me.dgvLetras.Rows(i).Cells(5).Value & "' " & _
                        " where idCliente= '" & Me.dgvLetras.Rows(i).Cells(9).Value & "' and numLetra='" & dgvLetras.Rows(i).Cells(0).Value & _
                        "' and numCorrelativo='" & dgvLetras.Rows(i).Cells(1).Value & "'"
                        ListSqlStrings.Add(SqlString)
                    Next
                    If transaccionProducto(ListSqlStrings) Then
                        MsgBox("Informaci�n modificada correctamente.", MsgBoxStyle.Information)
                        flag = 0
                        Me.Close()
                    Else
                        MsgBox("La Informaci�n no se proces� correctamente.", MsgBoxStyle.Critical)
                        Me.Close()
                    End If
                End If
            Else
                MsgBox("No hay informaci�n procesada para grabar.", MsgBoxStyle.Critical)
                Me.txtcodCliente.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvLetras_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLetras.DoubleClick

        Try
            oDataSet = New DataSet()
            Dim daVtaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM vtaCabecera where numLetra='" & Me.dgvLetras.Rows(Me.dgvLetras.CurrentCell.RowIndex).Cells(0).Value & "' ", Connection)
            Connection.Open()
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count <= 0 Then
                MsgBox("No existe n�mero de cta. cte. registrada en ventas.", MsgBoxStyle.Information)
                Me.txtcodCliente.Text = ""
                Me.txtCliente.Text = ""
                Exit Sub
            End If

            flag = 1
            tipMovimiento = CStr(oDataSet.Tables(0).Rows(0).Item(0))
            numDocumento = CInt(Me.oDataSet.Tables(0).Rows(0).Item(2))
            numeroLetra = Me.dgvLetras.Rows(Me.dgvLetras.CurrentCell.RowIndex).Cells(0).Value
            Me.btnAceptar_Click(sender, e)
            Me.btnImprimir_Click(sender, e)
            tipMovimiento = ""
            numDocumento = 0
            numeroLetra = ""
            flag = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtcodCliente_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcodCliente.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub dgvLetras_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvLetras.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 2 Or columna = 3 Then
            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Ingrese DNI o RUC  o inicial del apellido para hacer la b�squeda de la cta cte. del cliente."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvLetras_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseEnter
        Me.lblMensaje.Text = "Haz doble click en cualquiera de los registros para ingresar a la cta. cte. y al documento de origen."
    End Sub
    Private Sub dgvLetras_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class