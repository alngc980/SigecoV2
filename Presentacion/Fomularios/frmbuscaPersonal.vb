Imports System.data.SqlClient
Public Class frmbuscaPersonal
    Private oDataSet As DataSet
    Private oDataAdapter As SqlDataAdapter
    Private Sub frmbuscaPersonal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM personal", Connection)
            oDataAdapter.Fill(oDataSet, "personal")

            Me.dgvPersonal.DataSource = oDataSet
            Me.dgvPersonal.DataMember = "personal"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaTrabajador_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaTrabajador.KeyUp
        oDataSet = New DataSet()

        Try
            oDataAdapter = New SqlDataAdapter("SELECT * FROM personal where apePaterno Like '" & Me.txtBuscaTrabajador.Text & "%" & "'", Connection)
            oDataAdapter.Fill(oDataSet, "personal")

            Me.dgvPersonal.DataSource = oDataSet
            Me.dgvPersonal.DataMember = "personal"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvPersonal_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPersonal.CellDoubleClick
        Try
            With Me.dgvPersonal
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
            Me.txtBuscaTrabajador.Clear()
            Me.txtBuscaTrabajador.Focus()
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
End Class