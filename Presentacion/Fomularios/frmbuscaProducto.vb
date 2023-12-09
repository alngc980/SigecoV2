Imports System.Data.SqlClient
Public Class frmbuscaProducto
    Private oDataSet As DataSet
    Private oDataTable As DataTable
    Private oDataAdapter As SqlDataAdapter
    Private oDataColumn As DataColumn
    Private oDataRow As DataRow
    Private oDataRowArray() As DataRow
    Private Sub frmbuscaProducto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            If iniciarSaldos = True Then
                oDataAdapter = New SqlDataAdapter("SELECT * FROM productos where stoInicial=0", Connection)
            Else
                oDataAdapter = New SqlDataAdapter("SELECT * FROM productos where stoInicial=1", Connection)
            End If
            oDataAdapter.Fill(oDataSet, "Productos")

            oDataAdapter = New SqlDataAdapter("SELECT  * from saldosAlmacenes", Connection)
            oDataAdapter.Fill(oDataSet, "saldos")

            'Generando columna en tiempo de ejecución
            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "saldos"
            oDataColumn.ColumnName = "saldos"
            oDataSet.Tables(0).Columns.Add(oDataColumn)

            For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                For y As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    oDataRow = oDataSet.Tables(0).Rows(x)
                    If oDataSet.Tables(0).Rows(x).Item(0) = oDataSet.Tables(1).Rows(y).Item(0) Then
                        'oDataRow(17) = oDataSet.Tables(1).Rows(y).Item(1) 'Stock
                        oDataRow(18) = oDataSet.Tables(1).Rows(y).Item(1) 'Stock
                        Exit For
                    Else
                        'oDataRow(17) = 0 'Stock
                        oDataRow(18) = 0 'Stock
                    End If
                Next y
            Next x

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "Productos"
            With Me.dgvProductos
                .Columns(0).Width = 60
                .Columns(1).Visible = False
                .Columns(2).Width = 300
                .Columns(3).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Width = 80
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).Width = 80
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).Width = 80
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).Width = 80
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).Width = 80
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).Width = 80
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).Width = 80
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).Visible = False
                '.Columns(17).Width = 60
                '.Columns(17).DisplayIndex = 3
                '.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns(17).DefaultCellStyle.BackColor = Color.GreenYellow
                .Columns(18).Width = 60
                .Columns(18).DisplayIndex = 3
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(18).DefaultCellStyle.BackColor = Color.GreenYellow
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaProducto.KeyUp
        oDataTable = New DataTable()

        Try
            If iniciarSaldos = True Then
                oDataRowArray = oDataSet.Tables(0).Select("desProducto Like '" & "%" & txtBuscaProducto.Text & "%' and stoInicial=0")
            Else
                If ckBarra.Checked Then
                    oDataRowArray = oDataSet.Tables(0).Select("cCodBarra Like '" & "%" & txtBuscaProducto.Text & "%' and stoInicial=1")
                Else
                    oDataRowArray = oDataSet.Tables(0).Select("desProducto Like '" & "%" & txtBuscaProducto.Text & "%' and stoInicial=1")
                End If
            End If

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idProducto"
            oDataColumn.ColumnName = "idProducto"
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
            oDataColumn.Caption = "presentacion"
            oDataColumn.ColumnName = "presentacion"
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
            oDataColumn.Caption = "numSerie"
            oDataColumn.ColumnName = "numSerie"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preContado"
            oDataColumn.ColumnName = "preContado"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preCredito"
            oDataColumn.ColumnName = "preCredito"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preTarjeta"
            oDataColumn.ColumnName = "preTarjeta"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preTarjetaOferta"
            oDataColumn.ColumnName = "preTarjetaOferta"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preTarjetaRemate"
            oDataColumn.ColumnName = "preTarjetaRemate"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preOferta"
            oDataColumn.ColumnName = "preOferta"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "preRemate"
            oDataColumn.ColumnName = "preRemate"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "stoInicial"
            oDataColumn.ColumnName = "stoInicial"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "afeIGV"
            oDataColumn.ColumnName = "afeIGV"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "status"
            oDataColumn.ColumnName = "status"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "saldos"
            oDataColumn.ColumnName = "saldos"
            oDataTable.Columns.Add(oDataColumn)

            For Each row As DataRow In oDataRowArray
                oDataRow = oDataTable.NewRow
                oDataRow("idProducto") = row("idProducto")
                oDataRow("idGrupo") = row("idGrupo")
                oDataRow("desProducto") = row("desProducto")
                oDataRow("presentacion") = row("presentacion")
                oDataRow("marca") = row("marca")
                oDataRow("modelo") = row("modelo")
                oDataRow("numSerie") = row("numSerie")
                oDataRow("preContado") = row("preContado")
                oDataRow("preCredito") = row("preCredito")
                oDataRow("preTarjeta") = row("preTarjeta")
                oDataRow("preTarjetaOferta") = row("preTarjetaOferta")
                oDataRow("preTarjetaRemate") = row("preTarjetaRemate")
                oDataRow("preOferta") = row("preOferta")
                oDataRow("preRemate") = row("preRemate")
                oDataRow("stoInicial") = row("stoInicial")
                oDataRow("afeIGV") = row("afeIGV")
                oDataRow("status") = row("status")
                oDataRow("saldos") = row("saldos")
                oDataTable.Rows.Add(oDataRow)
            Next

            Me.dgvProductos.DataSource = oDataTable
            With Me.dgvProductos
                .Columns(0).Width = 60
                .Columns(1).Visible = False
                .Columns(2).Width = 300
                .Columns(3).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Width = 80
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).Width = 80
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).Width = 80
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).Width = 80
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).Width = 80
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).Width = 80
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).Width = 80
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).Visible = False
                .Columns(17).Width = 60
                .Columns(17).DisplayIndex = 3
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(17).DefaultCellStyle.BackColor = Color.GreenYellow
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvProductos_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellDoubleClick
        Try
            With Me.dgvProductos
                arrayDatos(0) = .Item(0, e.RowIndex).Value 'idProducto
                arrayDatos(1) = .Item(1, e.RowIndex).Value 'idGrupo
                arrayDatos(2) = .Item(2, e.RowIndex).Value 'desProducto
                arrayDatos(3) = .Item(4, e.RowIndex).Value 'marca
                arrayDatos(4) = .Item(5, e.RowIndex).Value 'modelo
                arrayDatos(5) = .Item(7, e.RowIndex).Value 'preContado
                arrayDatos(6) = .Item(8, e.RowIndex).Value 'preCredito
                arrayDatos(7) = .Item(9, e.RowIndex).Value 'preTarjeta
                arrayDatos(8) = .Item(10, e.RowIndex).Value 'preTarjetaOferta
                arrayDatos(9) = .Item(11, e.RowIndex).Value 'preTarjetaRemate
                arrayDatos(10) = .Item(12, e.RowIndex).Value 'preOferta
                arrayDatos(11) = .Item(13, e.RowIndex).Value 'preRemate
                flagString = "1"
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.txtBuscaProducto.Clear()
            Me.txtBuscaProducto.Focus()
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub

    Private Sub ckBarra_CheckedChanged(sender As Object, e As EventArgs) Handles ckBarra.CheckedChanged
        txtBuscaProducto.Focus()
        txtBuscaProducto.SelectAll()

    End Sub

    Private Sub txtBuscaProducto_TextChanged(sender As Object, e As EventArgs) Handles txtBuscaProducto.TextChanged

    End Sub
End Class