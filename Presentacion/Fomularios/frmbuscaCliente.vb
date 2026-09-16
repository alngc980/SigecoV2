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
        CargarClientes()
    End Sub

    Private Sub CargarClientes()
        Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where zona <> -1", Connection)
        oDataSet = New DataSet()

        Try
            If Connection.State <> ConnectionState.Closed Then
                Connection.Close()
            End If
            Connection.Open()
            daClientes.Fill(oDataSet, "clientes")
            Connection.Close()

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "clientes"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Connection.State <> ConnectionState.Closed Then
                Connection.Close()
            End If
        End Try
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaCliente.KeyUp
        Try
            If oDataSet Is Nothing OrElse oDataSet.Tables.Count = 0 Then Exit Sub

            Dim filtro As String = EscaparFiltro(Trim(Me.txtBuscaCliente.Text))
            Dim vistaClientes As New DataView(oDataSet.Tables(0))

            If filtro <> "" Then
                vistaClientes.RowFilter = "nombres LIKE '%" & filtro & "%' OR ruc LIKE '%" & filtro & "%' OR dni LIKE '%" & filtro & "%'"
            End If

            Me.dgvClientes.DataSource = vistaClientes
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function EscaparFiltro(ByVal valor As String) As String
        Return valor.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]")
    End Function

    Private Sub btnNuevoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
        Dim oFrmNuevoCliente As New frmNuevoCliente()
        oFrmNuevoCliente.ShowDialog()
        Me.txtBuscaCliente.Clear()
        CargarClientes()
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