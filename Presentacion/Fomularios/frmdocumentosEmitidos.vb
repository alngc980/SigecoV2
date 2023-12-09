Imports System.Data.SqlClient
Public Class frmdocumentosEmitidos
    Private oDataSet As DataSet
    Private item, v, w, x, y, z As Integer
    Private concepto As String
    Private Sub frmdocumentosEmitidos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnMostrar_Click(sender, e)
    End Sub

    Private Sub dtpFechaDocumento_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDocumento.ValueChanged
        Me.btnMostrar_Click(sender, e)
    End Sub

    Private Sub btnMostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMostrar.Click
        Dim arrayConcepto() As String = {"V.Contado", "A.Letra", "C.Letra", "C.Inicial", "A.Cuota", "O.Pagos", "Préstamo"}
        Dim arrayMoneda() As String = {"S/.", "$", "€"}

        item = 0
        Me.dgvDocumentos.Rows.Clear()
        oDataSet = New DataSet()

        Try
            Dim daVtaCabecera As New SqlDataAdapter("SELECT  tipDocumento,numDocumento,totVentaMN,totVentaME,fecOperacion,idMoneda,status,idCliente FROM vtaCabecera where fecOperacion='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daVtaCabecera.Fill(oDataSet, "vtaCabecera")

            Dim daAlmCabecera As New SqlDataAdapter("SELECT  nomDocumento,tipDocumento,numDocumento,fecOrigen,status,idCliente FROM almCabecera where fecOrigen='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daAlmCabecera.Fill(oDataSet, "almCabecera")

            Dim daRecibos As New SqlDataAdapter("SELECT  concepto,idRecibo,impDocumento,impDocumentoME,fecEmision,idMoneda,status,idCliente FROM recibosClientes where fecEmision='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daRecibos.Fill(oDataSet, "recibos")

            Dim daNotaCreditoCa As New SqlDataAdapter("SELECT  tipDocumento,numDocumento,totVentaMN,totVentaME,fecOperacion,idMoneda,status,idCliente FROM notaCreditoCa where fecOperacion='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daNotaCreditoCa.Fill(oDataSet, "notaCreditoCa")

            Dim daSalidas As New SqlDataAdapter("SELECT  concepto,idRecibo,impDocumento,impDocumentoME,fecEmision,idMoneda,status,idCliente FROM recibosSalidas where fecEmision='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daSalidas.Fill(oDataSet, "salidas")

            Dim daNotaDebitoCa As New SqlDataAdapter("SELECT  tipDocumento,numDocumento,totVentaMN,totVentaME,fecOperacion,idMoneda,status,idCliente FROM notaDebitoCa where fecOperacion='" & Me.dtpFechaDocumento.Text & "' ", Connection)
            daNotaDebitoCa.Fill(oDataSet, "notaDebitoCa")

            Dim daClientes As New SqlDataAdapter("Select *from clientes", Connection)
            daClientes.Fill(oDataSet, "clientes")

            Dim colNombres As DataColumn = New DataColumn()
            colNombres.AllowDBNull = True
            colNombres.Caption = "Cliente"
            colNombres.ColumnName = "cliente"
            Me.oDataSet.Tables(0).Columns.Add(colNombres)

            Dim oDataRowNombres As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres = Me.oDataSet.Tables(0).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(0).Rows(i).Item(7) Then
                        oDataRowNombres(8) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim colNombres1 As DataColumn = New DataColumn()
            colNombres1.AllowDBNull = True
            colNombres1.Caption = "Cliente"
            colNombres1.ColumnName = "cliente"
            Me.oDataSet.Tables(1).Columns.Add(colNombres1)

            Dim oDataRowNombres1 As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres1 = Me.oDataSet.Tables(1).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(1).Rows(i).Item(5) Then
                        oDataRowNombres1(6) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim colNombres2 As DataColumn = New DataColumn()
            colNombres2.AllowDBNull = True
            colNombres2.Caption = "Cliente"
            colNombres2.ColumnName = "cliente"
            Me.oDataSet.Tables(2).Columns.Add(colNombres2)

            Dim oDataRowNombres2 As DataRow
            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres2 = Me.oDataSet.Tables(2).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(2).Rows(i).Item(7) Then
                        oDataRowNombres2(8) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim colNombres3 As DataColumn = New DataColumn()
            colNombres3.AllowDBNull = True
            colNombres3.Caption = "Cliente"
            colNombres3.ColumnName = "cliente"
            Me.oDataSet.Tables(3).Columns.Add(colNombres3)

            Dim oDataRowNombres3 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres3 = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(3).Rows(i).Item(7) Then
                        oDataRowNombres3(8) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim colNombres4 As DataColumn = New DataColumn()
            colNombres4.AllowDBNull = True
            colNombres4.Caption = "Cliente"
            colNombres4.ColumnName = "cliente"
            Me.oDataSet.Tables(4).Columns.Add(colNombres4)

            Dim oDataRowNombres4 As DataRow
            For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres4 = Me.oDataSet.Tables(4).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(4).Rows(i).Item(7) Then
                        oDataRowNombres4(8) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            Dim colNombres5 As DataColumn = New DataColumn()
            colNombres5.AllowDBNull = True
            colNombres5.Caption = "Cliente"
            colNombres5.ColumnName = "cliente"
            Me.oDataSet.Tables(5).Columns.Add(colNombres5)

            Dim oDataRowNombres5 As DataRow
            For i As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(6).Rows.Count() - 1
                    oDataRowNombres5 = Me.oDataSet.Tables(5).Rows(i)
                    If Me.oDataSet.Tables(6).Rows(x).Item(0) = Me.oDataSet.Tables(5).Rows(i).Item(7) Then
                        oDataRowNombres5(8) = Me.oDataSet.Tables(6).Rows(x).Item(1)
                    End If
                Next x
            Next i

            If oDataSet.Tables(0).Rows.Count <= 0 And oDataSet.Tables(1).Rows.Count <= 0 And oDataSet.Tables(2).Rows.Count <= 0 And oDataSet.Tables(3).Rows.Count <= 0 And oDataSet.Tables(4).Rows.Count <= 0 And oDataSet.Tables(5).Rows.Count <= 0 Then
                MsgBox("No existen documentos emitidos en la fecha.", MsgBoxStyle.Information)
                Exit Sub
            End If

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i).Cells(1).Value = Me.oDataSet.Tables(0).Rows(i).Item(8)
                Me.dgvDocumentos.Rows(i).Cells(2).Value = "DV"
                Me.dgvDocumentos.Rows(i).Cells(3).Value = Me.oDataSet.Tables(0).Rows(i).Item(0)
                Me.dgvDocumentos.Rows(i).Cells(4).Value = Me.oDataSet.Tables(0).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i).Cells(5).Value = arrayMoneda(Me.oDataSet.Tables(0).Rows(i).Item(5) - 1)
                If Me.oDataSet.Tables(0).Rows(i).Item(3) > 0 Then
                    Me.dgvDocumentos.Rows(i).Cells(6).Value = Me.oDataSet.Tables(0).Rows(i).Item(3)
                Else
                    Me.dgvDocumentos.Rows(i).Cells(6).Value = Me.oDataSet.Tables(0).Rows(i).Item(2)
                End If
                Me.dgvDocumentos.Rows(i).Cells(7).Value = Me.oDataSet.Tables(0).Rows(i).Item(4)
                Me.dgvDocumentos.Rows(i).Cells(8).Value = Me.oDataSet.Tables(0).Rows(i).Item(6)
                x = i
            Next

            If Me.oDataSet.Tables(0).Rows.Count <= 0 Then
                x = 0
            Else
                x += 1
            End If

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i + x).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i + x).Cells(1).Value = Me.oDataSet.Tables(1).Rows(i).Item(6)
                Me.dgvDocumentos.Rows(i + x).Cells(2).Value = Me.oDataSet.Tables(1).Rows(i).Item(0)
                Me.dgvDocumentos.Rows(i + x).Cells(3).Value = Me.oDataSet.Tables(1).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i + x).Cells(4).Value = Me.oDataSet.Tables(1).Rows(i).Item(2)
                Me.dgvDocumentos.Rows(i + x).Cells(5).Value = ""
                Me.dgvDocumentos.Rows(i + x).Cells(6).Value = 0
                Me.dgvDocumentos.Rows(i + x).Cells(7).Value = Me.oDataSet.Tables(1).Rows(i).Item(3)
                Me.dgvDocumentos.Rows(i + x).Cells(8).Value = Me.oDataSet.Tables(1).Rows(i).Item(4)
                y = i + x
            Next

            If Me.oDataSet.Tables(1).Rows.Count <= 0 Then
                y = x
            Else
                y += 1
            End If

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i + y).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i + y).Cells(1).Value = Me.oDataSet.Tables(2).Rows(i).Item(8)
                Me.dgvDocumentos.Rows(i + y).Cells(2).Value = "RC"
                Me.dgvDocumentos.Rows(i + y).Cells(3).Value = arrayConcepto(Me.oDataSet.Tables(2).Rows(i).Item(0))
                Me.dgvDocumentos.Rows(i + y).Cells(4).Value = Me.oDataSet.Tables(2).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i + y).Cells(5).Value = arrayMoneda(Me.oDataSet.Tables(2).Rows(i).Item(5) - 1)
                If Me.oDataSet.Tables(2).Rows(i).Item(3) > 0 Then
                    Me.dgvDocumentos.Rows(i + y).Cells(6).Value = Me.oDataSet.Tables(2).Rows(i).Item(3)
                Else
                    Me.dgvDocumentos.Rows(i + y).Cells(6).Value = Me.oDataSet.Tables(2).Rows(i).Item(2)
                End If
                Me.dgvDocumentos.Rows(i + y).Cells(7).Value = Me.oDataSet.Tables(2).Rows(i).Item(4)
                Me.dgvDocumentos.Rows(i + y).Cells(8).Value = Me.oDataSet.Tables(2).Rows(i).Item(6)
                z = i + y
            Next

            If Me.oDataSet.Tables(2).Rows.Count <= 0 Then
                z = y
            Else
                z += 1
            End If

            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i + z).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i + z).Cells(1).Value = Me.oDataSet.Tables(3).Rows(i).Item(8)
                Me.dgvDocumentos.Rows(i + z).Cells(2).Value = "NC"
                Me.dgvDocumentos.Rows(i + z).Cells(3).Value = Me.oDataSet.Tables(3).Rows(i).Item(0)
                Me.dgvDocumentos.Rows(i + z).Cells(4).Value = Me.oDataSet.Tables(3).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i + z).Cells(5).Value = arrayMoneda(Me.oDataSet.Tables(3).Rows(i).Item(5) - 1)
                If Me.oDataSet.Tables(3).Rows(i).Item(3) > 0 Then
                    Me.dgvDocumentos.Rows(i + z).Cells(6).Value = Me.oDataSet.Tables(3).Rows(i).Item(3)
                Else
                    Me.dgvDocumentos.Rows(i + z).Cells(6).Value = Me.oDataSet.Tables(3).Rows(i).Item(2)
                End If
                Me.dgvDocumentos.Rows(i + z).Cells(7).Value = Me.oDataSet.Tables(3).Rows(i).Item(4)
                Me.dgvDocumentos.Rows(i + z).Cells(8).Value = Me.oDataSet.Tables(3).Rows(i).Item(6)
                w = i + z
            Next

            If Me.oDataSet.Tables(3).Rows.Count <= 0 Then
                w = z
            Else
                w += 1
            End If

            For i As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i + w).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i + w).Cells(1).Value = Me.oDataSet.Tables(4).Rows(i).Item(8)
                Me.dgvDocumentos.Rows(i + w).Cells(2).Value = "RP"
                Me.dgvDocumentos.Rows(i + w).Cells(3).Value = arrayConcepto(7)
                Me.dgvDocumentos.Rows(i + w).Cells(4).Value = Me.oDataSet.Tables(4).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i + w).Cells(5).Value = arrayMoneda(Me.oDataSet.Tables(4).Rows(i).Item(5) - 1)
                If Me.oDataSet.Tables(4).Rows(i).Item(3) > 0 Then
                    Me.dgvDocumentos.Rows(i + w).Cells(6).Value = Me.oDataSet.Tables(4).Rows(i).Item(3)
                Else
                    Me.dgvDocumentos.Rows(i + w).Cells(6).Value = Me.oDataSet.Tables(4).Rows(i).Item(2)
                End If
                Me.dgvDocumentos.Rows(i + w).Cells(7).Value = Me.oDataSet.Tables(4).Rows(i).Item(4)
                Me.dgvDocumentos.Rows(i + w).Cells(8).Value = Me.oDataSet.Tables(4).Rows(i).Item(6)
                v = i + w
            Next

            If Me.oDataSet.Tables(4).Rows.Count <= 0 Then
                v = w
            Else
                v += 1
            End If

            For i As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
                item = item + 1
                Me.dgvDocumentos.Rows.Add()
                Me.dgvDocumentos.Rows(i + v).Cells(0).Value = Me.item
                Me.dgvDocumentos.Rows(i + v).Cells(1).Value = Me.oDataSet.Tables(5).Rows(i).Item(8)
                Me.dgvDocumentos.Rows(i + v).Cells(2).Value = "ND"
                Me.dgvDocumentos.Rows(i + v).Cells(3).Value = Me.oDataSet.Tables(5).Rows(i).Item(0)
                Me.dgvDocumentos.Rows(i + v).Cells(4).Value = Me.oDataSet.Tables(5).Rows(i).Item(1)
                Me.dgvDocumentos.Rows(i + v).Cells(5).Value = arrayMoneda(Me.oDataSet.Tables(5).Rows(i).Item(5) - 1)
                If Me.oDataSet.Tables(5).Rows(i).Item(3) > 0 Then
                    Me.dgvDocumentos.Rows(i + v).Cells(6).Value = Me.oDataSet.Tables(5).Rows(i).Item(3)
                Else
                    Me.dgvDocumentos.Rows(i + v).Cells(6).Value = Me.oDataSet.Tables(5).Rows(i).Item(2)
                End If
                Me.dgvDocumentos.Rows(i + v).Cells(7).Value = Me.oDataSet.Tables(5).Rows(i).Item(4)
                Me.dgvDocumentos.Rows(i + v).Cells(8).Value = Me.oDataSet.Tables(5).Rows(i).Item(6)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvDocumentos_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvDocumentos.MouseDoubleClick
        flag = 1
        Dim ofrmanularRecibo As New frmanularRecibo()
        Dim ofrmanularGuia As New frmanularGuia()
        Dim ofrmanularDocumentoVta As New frmanularDocumentoVta()
        Dim ofrmanularNotaCredito As New frmanularNotaCredito()
        Dim ofrmanularNotaDebito As New frmanularNotaDebito()

        Select Case Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(2).Value.ToString
            Case "DV"
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularDocumentoVta.ShowDialog()
            Case "RC"
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularRecibo.ShowDialog()
            Case "GR"
                tipDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(2).Value
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularGuia.ShowDialog()
            Case "GX"
                tipDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(2).Value
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularGuia.ShowDialog()
            Case "PD"
                tipDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(2).Value
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularGuia.ShowDialog()
            Case "NC"
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularNotaCredito.ShowDialog()
            Case "ND"
                tipMovimiento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(3).Value
                numDocumento = Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(4).Value
                fecDocumento = CDate(Me.dgvDocumentos.Rows(dgvDocumentos.CurrentCell.RowIndex).Cells(7).Value.ToString)
                ofrmanularNotaDebito.ShowDialog()
        End Select
        numDocumento = 0
        tipDocumento = ""
        tipMovimiento = ""
        'fecDocumento = 
        flag = 0
    End Sub
    Private Sub dgvDocumentos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocumentos.CellMouseEnter
        Me.lblMensaje.Text = "Doble click sobre un registro para visualizarlo o imprimir, siempre que sea del día."
    End Sub
    Private Sub dgvDocumentos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDocumentos.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class