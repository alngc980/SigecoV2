Imports System.Data.SqlClient
Public Class frmbuscaCliente
    Private oDataSet As DataSet
    Private oDataTable As DataTable
    Private oDataAdapter As SqlDataAdapter
    Private oDataColumn As DataColumn
    Private oDataRow As DataRow
    Private oDataRowArray() As DataRow
    Private Sub frmbuscaCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtBuscaCliente.Clear()
        Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where zona <> -1", Connection)
        oDataSet = New DataSet()

        Try
            Connection.Open()
            daClientes.Fill(oDataSet, "clientes")
            Connection.Close()

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "clientes"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaCliente.KeyUp
        oDataTable = New DataTable()

        Try
            oDataRowArray = oDataSet.Tables(0).Select("nombres like '" & "%" & Me.txtBuscaCliente.Text & "%" & "'")

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "idCliente"
            oDataColumn.ColumnName = "idCliente"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "nombres"
            oDataColumn.ColumnName = "nombres"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "direccion"
            oDataColumn.ColumnName = "direccion"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "ruc"
            oDataColumn.ColumnName = "ruc"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "dni"
            oDataColumn.ColumnName = "dni"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "telCelular"
            oDataColumn.ColumnName = "telCelular"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "telFijo"
            oDataColumn.ColumnName = "telFijo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "dirTrabajo"
            oDataColumn.ColumnName = "dirTrabajo"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "nomPareja"
            oDataColumn.ColumnName = "nomPareja"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "dirPareja"
            oDataColumn.ColumnName = "dirPareja"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "dniPareja"
            oDataColumn.ColumnName = "dniPareja"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "celPareja"
            oDataColumn.ColumnName = "celPareja"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "dirTraPareja"
            oDataColumn.ColumnName = "dirTraPareja"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "zona"
            oDataColumn.ColumnName = "zona"
            oDataTable.Columns.Add(oDataColumn)

            oDataColumn = New DataColumn()
            oDataColumn.AllowDBNull = True
            oDataColumn.Caption = "fecAlta"
            oDataColumn.ColumnName = "fecAlta"
            oDataTable.Columns.Add(oDataColumn)

            For Each row As DataRow In oDataRowArray
                oDataRow = oDataTable.NewRow
                oDataRow("idCliente") = row("idCliente")
                oDataRow("nombres") = row("nombres")
                oDataRow("direccion") = row("direccion")
                oDataRow("ruc") = row("ruc")
                oDataRow("dni") = row("dni")
                oDataRow("telCelular") = row("telCelular")
                oDataRow("telFijo") = row("telFijo")
                oDataRow("dirTrabajo") = row("dirTrabajo")
                oDataRow("nomPareja") = row("nomPareja")
                oDataRow("dirPareja") = row("dirPareja")
                oDataRow("dniPareja") = row("dniPareja")
                oDataRow("celPareja") = row("celPareja")
                oDataRow("dirTraPareja") = row("dirTraPareja")
                oDataRow("zona") = row("zona")
                oDataRow("fecAlta") = row("fecAlta")
                oDataTable.Rows.Add(oDataRow)
            Next

            Me.dgvClientes.DataSource = oDataTable
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvClientes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellDoubleClick
        Try
            With Me.dgvClientes
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
                arrayDatos(3) = .Item(3, e.RowIndex).Value
                arrayDatos(4) = .Item(4, e.RowIndex).Value
                arrayDatos(5) = .Item(13, e.RowIndex).Value
            End With
            oDataSet.Tables.Clear()
            Me.txtBuscaCliente.Clear()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class