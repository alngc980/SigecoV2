Imports System.Data.SqlClient
Public Class frmbuscaGarante
    Private oDataSet As DataSet
    Private Sub frmbuscaGarante_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daGarantes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM garantes", Connection)
            daGarantes.Fill(oDataSet, "garantes")

            Me.dgvGarantes.DataSource = oDataSet
            Me.dgvGarantes.DataMember = "garantes"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaGarante.KeyUp
        oDataSet = New DataSet()

        Try
            Dim daGarantes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM garantes where nombreGarante Like '" & Me.txtBuscaGarante.Text & "%" & "'", Connection)
            daGarantes.Fill(oDataSet, "garantes")

            Me.dgvGarantes.DataSource = oDataSet
            Me.dgvGarantes.DataMember = "garantes"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvGarantes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGarantes.CellDoubleClick
        Try
            With Me.dgvGarantes
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
                arrayDatos(3) = .Item(3, e.RowIndex).Value
                arrayDatos(4) = .Item(4, e.RowIndex).Value
                arrayDatos(5) = .Item(5, e.RowIndex).Value
                arrayDatos(6) = .Item(6, e.RowIndex).Value
                arrayDatos(7) = .Item(12, e.RowIndex).Value
            End With
            oDataSet.Tables.Clear()
            Me.txtBuscaGarante.Clear()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class