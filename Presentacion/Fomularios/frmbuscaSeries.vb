Imports System.Data.SqlClient
Public Class frmbuscaSeries
    Private oDataSet As DataSet
    Private oDataAdapter As SqlDataAdapter
    Private Sub frmbuscaSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM numerosSerie where idProducto Like '" & codigoProducto & "' and numDoc=''", Connection)
            oDataAdapter.Fill(oDataSet, "Series")

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "Series"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellDoubleClick
        Try
            With Me.dgvProductos
                arraySeries(0) = .Item(0, e.RowIndex).Value
                arraySeries(1) = .Item(1, e.RowIndex).Value
                arraySeries(2) = .Item(2, e.RowIndex).Value
                arraySeries(3) = .Item(3, e.RowIndex).Value
                arraySeries(4) = .Item(6, e.RowIndex).Value
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
End Class