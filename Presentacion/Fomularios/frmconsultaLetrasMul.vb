Imports Microsoft
Imports System.Data.SqlClient
Public Class frmconsultaLetrasMul
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim vfecVencimiento As Date
    Dim sumaAmortizacionesMN, sumaAmortizacionesME As Decimal
    Dim vLetra, vInteres, interes, totalColumna As Decimal
    Dim vFlag, dVencidos As Integer
    Dim valorDecimal As Byte
    Dim dirCliente, dniCliente As String
    Private Sub frmconsultaLetrasNom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        sumaAmortizacionesMN = 0
        sumaAmortizacionesME = 0
        Me.txtCliente.Text = ""

        If flag <> 0 Then
            Me.txtNumCuenta.Text = numeroLetra
        End If

        If Me.txtNumCuenta.Text = "" Or (Len(Me.txtNumCuenta.Text) < 6 Or Len(Me.txtNumCuenta.Text) > 14) Then
            MsgBox("Ingrese número de cta. cte. válido.", MsgBoxStyle.Information)
            Me.txtNumCuenta.Text = ""
            Me.txtNumCuenta.Focus()
            Exit Sub
        End If

        Try
            vFlag = 0
            oDataSet = New DataSet()
            Connection.Open()

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,idMoneda,idCliente " & _
            "FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and  (numLetra='" & Me.txtNumCuenta.Text & "') order by numCorrelativo", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("Número cta. cte. inexistente.", MsgBoxStyle.Information)
                Me.txtNumCuenta.Text = ""
                Me.txtNumCuenta.Focus()
                Connection.Close()
                Exit Sub
            End If

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where idCliente Like '" & Me.oDataSet.Tables(0).Rows(0).Item(7) & "' ", Connection)
            daCliente.Fill(oDataSet, "cliente")

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
            Me.oDataSet.Tables(0).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(0).Rows(i)
                If Me.oDataSet.Tables(0).Rows(i).Item(6) = 1 Then
                    oDataRowMoneda(8) = "S/."
                Else
                    If Me.oDataSet.Tables(0).Rows(i).Item(6) = 2 Then
                        oDataRowMoneda(8) = "$"
                    Else
                        oDataRowMoneda(8) = "€"
                    End If
                End If
            Next i

            Dim colAmortizacion As DataColumn = New DataColumn()
            colAmortizacion.AllowDBNull = True
            colAmortizacion.DataType = System.Type.GetType("System.Decimal")
            colAmortizacion.Caption = "Adelantos"
            colAmortizacion.ColumnName = "adelantos"
            colAmortizacion.DefaultValue = 0.0
            Me.oDataSet.Tables(0).Columns.Add(colAmortizacion)

            Dim colSaldo As DataColumn = New DataColumn()
            colSaldo.AllowDBNull = True
            colSaldo.DataType = System.Type.GetType("System.Decimal")
            colSaldo.Caption = "saldoPagar"
            colSaldo.ColumnName = "saldoPagar"
            Me.oDataSet.Tables(0).Columns.Add(colSaldo)

            Dim colInteres As DataColumn = New DataColumn()
            colInteres.AllowDBNull = True
            colInteres.DataType = System.Type.GetType("System.Decimal")
            colInteres.Caption = "interés"
            colInteres.ColumnName = "interes"
            colInteres.DefaultValue = 0.0
            Me.oDataSet.Tables(0).Columns.Add(colInteres)

            Dim colTotPagar As DataColumn = New DataColumn()
            colTotPagar.AllowDBNull = True
            colTotPagar.DataType = System.Type.GetType("System.Decimal")
            colTotPagar.Caption = "totPagar"
            colTotPagar.ColumnName = "totPagar"
            Me.oDataSet.Tables(0).Columns.Add(colTotPagar)

            Dim oDataRowAmortiza As DataRow
            Dim oDataRowSaldos As DataRow
            Dim oDataRowInteres As DataRow
            Dim oDataRowTotPagar As DataRow

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowAmortiza = Me.oDataSet.Tables(0).Rows(i)
                oDataRowSaldos = Me.oDataSet.Tables(0).Rows(i)
                oDataRowInteres = Me.oDataSet.Tables(0).Rows(i)
                oDataRowTotPagar = Me.oDataSet.Tables(0).Rows(i)
                vfecVencimiento = Me.oDataSet.Tables(0).Rows(i).Item(5)

                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(0) = Me.oDataSet.Tables(2).Rows(x).Item(2) And _
                       Me.oDataSet.Tables(0).Rows(i).Item(1) = Me.oDataSet.Tables(2).Rows(x).Item(5) And _
                       Me.oDataSet.Tables(2).Rows(x).Item(1) = "1" Then

                        If Me.oDataSet.Tables(2).Rows(x).Item(14) > 1 Then
                            sumaAmortizacionesME += Me.oDataSet.Tables(2).Rows(x).Item(4)
                            oDataRowAmortiza(9) = sumaAmortizacionesME
                        Else
                            sumaAmortizacionesMN += Me.oDataSet.Tables(2).Rows(x).Item(3)
                            oDataRowAmortiza(9) = sumaAmortizacionesMN
                        End If

                        If Me.oDataSet.Tables(2).Rows(x).Item(10) > Me.oDataSet.Tables(2).Rows(x).Item(11) Then
                            vfecVencimiento = Me.oDataSet.Tables(2).Rows(x).Item(10)
                        Else
                            vfecVencimiento = Me.oDataSet.Tables(2).Rows(x).Item(11)
                        End If
                    End If

                    If Me.oDataSet.Tables(0).Rows(i).Item(6) > 1 Then
                        If Me.oDataSet.Tables(0).Rows(i).Item(2) > 0 Then
                            oDataRowSaldos(10) = Me.oDataSet.Tables(0).Rows(i).Item(2) - sumaAmortizacionesMN
                        Else
                            oDataRowSaldos(10) = Me.oDataSet.Tables(0).Rows(i).Item(3) - sumaAmortizacionesME
                        End If
                    Else
                        oDataRowSaldos(10) = Me.oDataSet.Tables(0).Rows(i).Item(2) - sumaAmortizacionesMN
                    End If
                Next x

                Me.vLetra = oDataRowSaldos(10)
                Me.dVencidos = DateDiff(DateInterval.Day, vfecVencimiento, CDate(Me.dtpFecha.Text))

                If dVencidos >= 9 Then
                    If vLetra <= Me.oDataSet.Tables(3).Rows(0).Item(2) Then
                        vInteres = Me.oDataSet.Tables(3).Rows(0).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(4).Rows(0).Item(1))
                    Else
                        If vLetra <= Me.oDataSet.Tables(3).Rows(1).Item(2) Then
                            vInteres = Me.oDataSet.Tables(3).Rows(1).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        Else
                            vInteres = Me.oDataSet.Tables(3).Rows(2).Item(3) + (vLetra * dVencidos * Me.oDataSet.Tables(4).Rows(0).Item(1))
                        End If
                    End If
                    oDataRowInteres(11) = Format(vInteres, "#####0.00")
                    valorDecimal = CByte(VisualBasic.Right(oDataRowInteres(11), 2))
                    If valorDecimal >= 1 Then
                        oDataRowInteres(11) = Format(Math.Floor(CDec(oDataRowInteres(11))) + 1, "#####0.00")
                    End If
                End If

                oDataRowTotPagar(12) = oDataRowSaldos(10) + oDataRowInteres(11)
                sumaAmortizacionesME = 0
                sumaAmortizacionesMN = 0
            Next i

            '------------- Creamos columna check del 'datagridview' en tiempo de ejecucion
            Dim colCheck As New DataGridViewCheckBoxColumn
            colCheck.Name = "status"
            colCheck.HeaderText = "status"
            colCheck.Width = 40

            Me.lblNombre.Text = oDataSet.Tables(1).Rows(0).Item(1)
            Me.dirCliente = oDataSet.Tables(1).Rows(0).Item(2)
            If Me.oDataSet.Tables(1).Rows(0).Item(3).ToString.Trim = "" Then
                Me.dniCliente = Me.oDataSet.Tables(1).Rows(0).Item(4)
            Else
                Me.dniCliente = Me.oDataSet.Tables(1).Rows(0).Item(3)
            End If

            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(0).DisplayIndex = 0
                .Columns(0).ReadOnly = True
                .Columns(1).DisplayIndex = 1
                .Columns(1).ReadOnly = True
                .Columns(1).Width = 50
                .Columns(2).DisplayIndex = 3
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).ReadOnly = True
                .Columns(2).Width = 70
                .Columns(3).DisplayIndex = 4
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).ReadOnly = True
                .Columns(3).Width = 70
                .Columns(4).DisplayIndex = 9
                .Columns(4).ReadOnly = True
                .Columns(4).Width = 80
                .Columns(5).DisplayIndex = 10
                .Columns(5).ReadOnly = True
                .Columns(5).Width = 80
                .Columns(6).DisplayIndex = 11
                .Columns(6).ReadOnly = True
                .Columns(6).Visible = False
                .Columns(7).DisplayIndex = 12
                .Columns(7).ReadOnly = True
                .Columns(7).Visible = False
                .Columns(8).DisplayIndex = 2
                .Columns(8).ReadOnly = True
                .Columns(8).Width = 50
                .Columns(9).DisplayIndex = 5
                .Columns(9).ReadOnly = True
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "####0.00"
                .Columns(9).Width = 70
                .Columns(10).DisplayIndex = 6
                .Columns(10).ReadOnly = True
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).Width = 80
                .Columns(11).DisplayIndex = 7
                .Columns(11).ReadOnly = True
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).Width = 70
                .Columns(12).DisplayIndex = 8
                .Columns(12).ReadOnly = True
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).Width = 80
                .Columns.Add(colCheck)
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyUp
        Try

            If Me.dgvLetras.Rows.Count > 1 And vFlag <> 1 Then
                With Me.dgvLetras
                    .Columns.Remove("status")
                End With
            End If
            vFlag = 1

            sumaAmortizacionesMN = 0
            sumaAmortizacionesME = 0
            Me.txtNumCuenta.Text = ""

            oDataSet = New DataSet()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where nombres Like '" & Me.txtCliente.Text & "%" & "' ", Connection)
            Connection.Open()
            daCliente.Fill(oDataSet, "cliente")

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,idMoneda,idCliente " & _
            "FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT * from recibosClientes", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            Connection.Close()

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(6) = 1 Then
                    oDataRowMoneda(8) = "S/."
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(6) = 2 Then
                        oDataRowMoneda(8) = "$"
                    Else
                        oDataRowMoneda(8) = "€"
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
                       Me.oDataSet.Tables(2).Rows(x).Item(1) = "1" Then

                        If Me.oDataSet.Tables(2).Rows(x).Item(14) > 1 Then
                            sumaAmortizacionesME += Me.oDataSet.Tables(2).Rows(x).Item(4)
                            oDataRowAmortiza(9) = sumaAmortizacionesME
                        Else
                            sumaAmortizacionesMN += Me.oDataSet.Tables(2).Rows(x).Item(3)
                            oDataRowAmortiza(9) = sumaAmortizacionesMN
                        End If
                    End If

                    If Me.oDataSet.Tables(1).Rows(i).Item(6) > 1 Then
                        oDataRowSaldos(10) = Me.oDataSet.Tables(1).Rows(i).Item(3) - sumaAmortizacionesME
                    Else
                        oDataRowSaldos(10) = Me.oDataSet.Tables(1).Rows(i).Item(2) - sumaAmortizacionesMN
                    End If
                Next x
                sumaAmortizacionesME = 0
                sumaAmortizacionesMN = 0
            Next i

            Me.lblNombre.Text = oDataSet.Tables(0).Rows(0).Item(1)
            Me.dirCliente = oDataSet.Tables(0).Rows(0).Item(2)
            If Me.oDataSet.Tables(1).Rows(0).Item(3).ToString.Trim = "" Then
                Me.dniCliente = Me.oDataSet.Tables(1).Rows(0).Item(4)
            Else
                Me.dniCliente = Me.oDataSet.Tables(1).Rows(0).Item(3)
            End If
            Me.dgvLetras.DataSource = oDataSet
            Me.dgvLetras.DataMember = "ctaCorriente"
            With Me.dgvLetras
                .Columns(0).DisplayIndex = 0
                .Columns(0).ReadOnly = True
                .Columns(1).DisplayIndex = 1
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True
                .Columns(2).DisplayIndex = 3
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).ReadOnly = False
                .Columns(3).DisplayIndex = 4
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).ReadOnly = False
                .Columns(4).DisplayIndex = 7
                .Columns(4).ReadOnly = False
                .Columns(5).DisplayIndex = 8
                .Columns(5).ReadOnly = False
                .Columns(6).DisplayIndex = 9
                .Columns(6).ReadOnly = True
                .Columns(7).DisplayIndex = 10
                .Columns(7).ReadOnly = True
                .Columns(8).DisplayIndex = 2
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).ReadOnly = True
                .Columns(9).DisplayIndex = 5
                .Columns(9).ReadOnly = True
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "####0.00"
                .Columns(10).DisplayIndex = 6
                .Columns(10).ReadOnly = True
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub dgvLetras_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLetras.DoubleClick
        Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex

        Try
            If columna = 0 Then
                flag = 1
                numeroLetra = Me.dgvLetras.Rows(Me.dgvLetras.CurrentCell.RowIndex).Cells(0).Value
                Me.btnAceptar_Click(sender, e)
                numeroLetra = ""
                flag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvLetras_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellValueChanged
        Try
            If (Me.dgvLetras.Columns(e.ColumnIndex).Name = "status") Then
                sumaColumnas()
                lblTotal.Text = Format(totalColumna, "##,##0.00")
            End If
            totalColumna = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sumaColumnas()
        Try
            For i As Integer = 0 To Me.dgvLetras.Rows.Count - 1
                If Me.dgvLetras.Rows(i).Cells(13).Value = True Then
                    totalColumna += Me.dgvLetras.Rows(i).Cells(12).Value
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Ingrese número cta. cte. o inicial de apellido del cliente para empezar la búsqueda."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvLetras_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseEnter
        Me.lblMensaje.Text = "Haz doble click en el número de cuenta para hacer la consulta individual."
    End Sub
    Private Sub dgvLetras_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class