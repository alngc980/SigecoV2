Imports System.Data.SqlClient
Public Class frmajustarStock
    Private oDataSet As DataSet
    Private oDataTable As DataTable
    Private oDataAdapter As SqlDataAdapter
    Private oDataColumn As DataColumn
    Private oDataRow As DataRow
    Private oDataRowArray() As DataRow

    Dim x, y, z As Integer
    Dim fechaCierre As String
    Dim c As Cursor = Me.Cursor
    Private Sub frmAjustarStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnMostrar_Click(sender, e)
    End Sub
    Private Sub btnMostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMostrar.Click
        oDataSet = New DataSet()

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim daSeries As SqlDataAdapter = New SqlDataAdapter("SELECT  * from numerosSerie", Connection)
            daSeries.Fill(oDataSet, "series")

            Dim daCierre As SqlDataAdapter = New SqlDataAdapter("SELECT  * from cierreDiario", Connection)
            daCierre.Fill(oDataSet, "cierreDiario")

            fechaCierre = Me.oDataSet.Tables(1).Rows(0).Item(0).ToString
            ReDim matrizSeries(oDataSet.Tables(0).Rows.Count(), 7)

            For x As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count() - 1
                For y As Integer = 0 To 7
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                    matrizSeries(x, y) = Me.oDataSet.Tables(0).Rows(x).Item(y).ToString
                Next y
                z = x
            Next x

            Dim daSaldos As SqlDataAdapter = New SqlDataAdapter("SELECT  * from saldosAlmacenes where fechaSaldo='" & CDate(fechaCierre) & "' order by idProducto", Connection)
            daSaldos.Fill(oDataSet, "saldos")

            Dim daProducto As SqlDataAdapter = New SqlDataAdapter("SELECT  * from Productos where stoInicial>=0", Connection)
            daProducto.Fill(oDataSet, "productos")

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idGrupo"
            oDataColumn.ColumnName = "idGrupo"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "desProducto"
            oDataColumn.ColumnName = "desProducto"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "marca"
            oDataColumn.ColumnName = "marca"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "modelo"
            oDataColumn.ColumnName = "modelo"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "stockFisico"
            oDataColumn.ColumnName = "stockFisico"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "diferencia"
            oDataColumn.ColumnName = "diferencia"
            Me.oDataSet.Tables(2).Columns.Add(oDataColumn)

            Dim oDataRow As DataRow
            For i As Integer = 0 To Me.oDataSet.Tables(2).Rows.Count() - 1
                For x As Integer = 0 To Me.oDataSet.Tables(3).Rows.Count() - 1
                    If Me.oDataSet.Tables(3).Rows.Item(x).Item(0) = Me.oDataSet.Tables(2).Rows.Item(i).Item(0) Then
                        oDataRow = Me.oDataSet.Tables(2).Rows(i)
                        oDataRow(3) = Me.oDataSet.Tables(3).Rows(x).Item(1)
                        oDataRow(4) = Me.oDataSet.Tables(3).Rows(x).Item(2)
                        oDataRow(5) = Me.oDataSet.Tables(3).Rows(x).Item(4)
                        oDataRow(6) = Me.oDataSet.Tables(3).Rows(x).Item(5)
                    End If
                Next x
            Next i

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "saldos"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True 'stock
                .Columns(1).DisplayIndex = 5
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(1).DefaultCellStyle.BackColor = Color.Orange
                .Columns(2).ReadOnly = True 'fechaSaldo
                .Columns(2).DisplayIndex = 8
                .Columns(3).ReadOnly = True 'idGrupo
                .Columns(3).DisplayIndex = 1
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).ReadOnly = True 'desProducto
                .Columns(4).DisplayIndex = 2
                .Columns(5).ReadOnly = True 'marca
                .Columns(5).DisplayIndex = 3
                .Columns(6).ReadOnly = True 'modelo
                .Columns(6).DisplayIndex = 4
                .Columns(7).DisplayIndex = 6 'stockFisico
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.BackColor = Color.Pink
                .Columns(8).ReadOnly = True 'Diferencia
                .Columns(8).DisplayIndex = 7
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ' Restauramos el cursor
            Me.Cursor = c
        End Try
    End Sub
    Private Sub txtBuscaProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaProducto.KeyUp
        oDataTable = New DataTable()

        Try
            oDataRowArray = oDataSet.Tables(2).Select("desProducto Like '" & "%" & txtBuscaProducto.Text & "%'")

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idProducto"
            oDataColumn.ColumnName = "idProducto"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "stock"
            oDataColumn.ColumnName = "stock"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "fechaSaldo"
            oDataColumn.ColumnName = "fechaSaldo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idGrupo"
            oDataColumn.ColumnName = "idGrupo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "desProducto"
            oDataColumn.ColumnName = "desProducto"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "marca"
            oDataColumn.ColumnName = "marca"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "modelo"
            oDataColumn.ColumnName = "modelo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "stockFisico"
            oDataColumn.ColumnName = "stockFisico"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "diferencia"
            oDataColumn.ColumnName = "diferencia"
            oDataTable.Columns.Add(oDataColumn)

            For Each row As DataRow In oDataRowArray
                oDataRow = oDataTable.NewRow
                oDataRow("idProducto") = row("idProducto")
                oDataRow("stock") = row("stock")
                oDataRow("fechaSaldo") = CDate(row("fechaSaldo"))
                oDataRow("idGrupo") = row("idGrupo")
                oDataRow("desProducto") = row("desProducto")
                oDataRow("marca") = row("marca")
                oDataRow("modelo") = row("modelo")
                oDataRow("stockFisico") = row("stockFisico")
                oDataRow("diferencia") = row("diferencia")
                oDataTable.Rows.Add(oDataRow)
            Next

            Me.dgvProductos.DataSource = oDataTable
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True 'stock
                .Columns(1).DisplayIndex = 5
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(1).DefaultCellStyle.BackColor = Color.Orange
                .Columns(2).ReadOnly = True 'fechaSaldo
                .Columns(2).DisplayIndex = 8
                .Columns(3).ReadOnly = True 'idGrupo
                .Columns(3).DisplayIndex = 1
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).ReadOnly = True 'desProducto
                .Columns(4).DisplayIndex = 2
                .Columns(5).ReadOnly = True 'marca
                .Columns(5).DisplayIndex = 3
                .Columns(6).ReadOnly = True 'modelo
                .Columns(6).DisplayIndex = 4
                .Columns(7).DisplayIndex = 6 'stockFisico
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.BackColor = Color.Pink
                .Columns(8).ReadOnly = True 'Diferencia
                .Columns(8).DisplayIndex = 7
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellValueChanged
        Dim ofrmnumerosSerie As New frmingresarSeries()
        Try
            If (dgvProductos.Columns(e.ColumnIndex).Name = "stockFisico") Then
                Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value = Me.dgvProductos.Rows(e.RowIndex).Cells(1).Value - Me.dgvProductos.Rows(e.RowIndex).Cells(7).Value

                If Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value < 0 Then
                    flagString = "ASI"
                    codigoProducto = Me.dgvProductos.Rows(e.RowIndex).Cells(0).Value
                    codigoGrupo = Me.dgvProductos.Rows(e.RowIndex).Cells(3).Value
                    canNumSeries = Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value * -1
                    ofrmnumerosSerie.ShowDialog()
                    If flag = 0 Then Me.dgvProductos.Rows(e.RowIndex).Cells(7).Value = Me.dgvProductos.Rows(e.RowIndex).Cells(1).Value
                    flagString = ""
                    codigoProducto = 0
                    codigoGrupo = 0
                    canNumSeries = 0
                Else
                    If Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value > 0 Then
                        flagString = "AS"
                        codigoProducto = Me.dgvProductos.Rows(e.RowIndex).Cells(0).Value
                        codigoGrupo = Me.dgvProductos.Rows(e.RowIndex).Cells(3).Value
                        canNumSeries = Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value
                        nSeriesBorrar = Me.dgvProductos.Rows(e.RowIndex).Cells(8).Value
                        ofrmnumerosSerie.ShowDialog()
                        If flag = 0 Then Me.dgvProductos.Rows(e.RowIndex).Cells(7).Value = Me.dgvProductos.Rows(e.RowIndex).Cells(1).Value
                        flagString = ""
                        codigoProducto = 0
                        codigoGrupo = 0
                        canNumSeries = 0
                        nSeriesBorrar = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String = ""
        Dim SqlString1 As String = ""
        Dim SqlString2 As String = ""
        Dim SqlString3 As String = "" 'agregado el 29-12-15

        Dim ListSqlStrings As New ArrayList
        Dim ListSqlStrings1 As New ArrayList
        Dim ListSqlStrings2 As New ArrayList 'agregado el 29-12-15

        If dgvProductos.Rows.Count <= 0 Then
            MsgBox("No hay información procesada para grabar.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Me.btnSalir.Enabled = False

        Try
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = dgvProductos.Rows.Count - 1
            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                If dgvProductos.Rows(i).Cells(7).Value.ToString = "" Then
                    dgvProductos.Rows(i).Cells(7).Value = dgvProductos.Rows(i).Cells(1).Value

                    ProgressBar1.PerformStep()
                    Application.DoEvents()
                End If
            Next i

            SqlString = "TRUNCATE TABLE numerosSerie"

            ProgressBar1.Value = 1
            ProgressBar1.Maximum = matrizSeries.GetUpperBound(0) - 1
            For x As Integer = matrizSeries.GetLowerBound(0) To matrizSeries.GetUpperBound(0) - 1
                If matrizSeries(x, 0) <> 0 Then
                    SqlString1 = "INSERT INTO numerosSerie (idProducto,numSerie,numMotor,numChasis,numDoc,numDoc1,color,anoFab) VALUES ( '" & _
                    matrizSeries(x, 0) & "','" & matrizSeries(x, 1) & "','" & matrizSeries(x, 2) & "','" & matrizSeries(x, 3) & _
                    "','" & matrizSeries(x, 4) & "','" & matrizSeries(x, 5) & "','" & matrizSeries(x, 6) & "','" & matrizSeries(x, 7) & "')"
                    ListSqlStrings.Add(SqlString1)

                    ProgressBar1.PerformStep()
                    Application.DoEvents()
                End If
            Next

            ProgressBar1.Value = 1
            ProgressBar1.Maximum = dgvProductos.Rows.Count - 1
            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                SqlString2 = "UPDATE saldosAlmacenes Set stock='" & CInt(dgvProductos.Rows(i).Cells(7).Value) & "' where fechaSaldo='" & CDate(dgvProductos.Rows(i).Cells(2).Value) & "' and idProducto='" & CInt(dgvProductos.Rows(i).Cells(0).Value.ToString()) & "'"
                ListSqlStrings1.Add(SqlString2)

                ProgressBar1.PerformStep()
                Application.DoEvents()
            Next

            '-----agregado el dia 29-12-15
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = dgvProductos.Rows.Count
            Dim vDiferenciaMas, vDiferenciaMenos As Int16
            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                If dgvProductos.Rows(i).Cells(8).Value > 0 Then
                    vDiferenciaMas = dgvProductos.Rows(i).Cells(8).Value
                Else
                    vDiferenciaMenos = dgvProductos.Rows(i).Cells(8).Value
                End If
                SqlString3 = "insert into resumenAjustes(idProducto,stockSistema,stockFisico,mas,menos,fecha) VALUES ( '" & _
                CInt(dgvProductos.Rows(i).Cells(0).Value.ToString()) & "','" & CInt(dgvProductos.Rows(i).Cells(1).Value.ToString()) & _
                "','" & CInt(dgvProductos.Rows(i).Cells(7).Value.ToString()) & "'," & vDiferenciaMas & "," & vDiferenciaMenos & ",'" & Now.Date & "')"
                ListSqlStrings2.Add(SqlString3)

                vDiferenciaMas = 0
                vDiferenciaMenos = 0
                ProgressBar1.PerformStep()
                Application.DoEvents()
            Next
            '------------

            If transaccionAjuste(SqlString, ListSqlStrings, ListSqlStrings1, ListSqlStrings2) Then
                MsgBox("Información procesada correctamente.", MsgBoxStyle.Information)
                Me.Cursor = Cursors.WaitCursor
                actualizaNumItem()
                Me.Cursor = c
                Me.Close()
            Else
                MsgBox("Error en el procesamiento de la información.", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)

        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex

        If columna = 7 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        If Me.dgvProductos.RowCount > 0 Then
            Me.lblMensaje.Text = "Haz un click en el botón 'Grabar' para procesar las modificaciones."
        Else
            Me.lblMensaje.Text = "Presione [Enter] o click en el botón 'Mostrar' para visualizar los registros del stock."
        End If
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvProductos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseEnter
        Me.lblMensaje.Text = "Ingrese stock real en columna 'Stock Físico' y presione [enter]. Al hacer esto la columna 'Diferencia' será positiva o negativa."
        Me.lblMensaje1.Text = "Si es positiva; en la ventana  'Números Serie y Motor' tendrá que eliminar registros, caso contrario tendrá que agregar."
    End Sub
    Private Sub dgvProductos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseLeave
        Me.lblMensaje.Text = ""
        Me.lblMensaje1.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class