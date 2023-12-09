Imports System.Data.SqlClient
Public Class frmbuscaGrupo
    Private oDataAdapter As SqlDataAdapter
    Private oDataSet As DataSet
    Private Sub frmbuscaGrupo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM gruposProductos", Connection)
            oDataAdapter.Fill(oDataSet, "gruposProductos")

            Me.dgvGrupoProducto.DataSource = oDataSet
            Me.dgvGrupoProducto.DataMember = "gruposProductos"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvGrupoProducto_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGrupoProducto.CellDoubleClick
        Try
            With Me.dgvGrupoProducto
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
End Class