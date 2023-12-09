Imports System.data.SqlClient
Public Class frmbuscaVendedor
    Private oDataSet As DataSet
    Private oDataAdapter As SqlDataAdapter
    Private Sub frmbuscaVendedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM vendedores", Connection)
            oDataAdapter.Fill(oDataSet, "Vendedores")

            Me.dgvVendedores.DataSource = oDataSet
            Me.dgvVendedores.DataMember = "Vendedores"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaVendedor_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaVendedor.KeyUp
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM vendedores where apePaterno Like '" & Me.txtBuscaVendedor.Text & "%" & "'", Connection)
            oDataAdapter.Fill(oDataSet, "Vendedores")

            Me.dgvVendedores.DataSource = oDataSet
            Me.dgvVendedores.DataMember = "Vendedores"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvVendedores_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvVendedores.CellDoubleClick
        Try
            With Me.dgvVendedores
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
                arrayDatos(3) = .Item(3, e.RowIndex).Value
                arrayDatos(4) = .Item(4, e.RowIndex).Value
                arrayDatos(5) = .Item(5, e.RowIndex).Value
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.txtBuscaVendedor.Clear()
            Me.txtBuscaVendedor.Focus()
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
End Class