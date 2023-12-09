Imports System.Data.SqlClient
Public Class frmbuscaMoneda
    Private oDataSet As DataSet
    Private Sub frmbuscaMoneda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daMonedas As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM tiposMonedas ", Connection)
            daMonedas.Fill(oDataSet, "monedas")

            Me.dgvMonedas.DataSource = oDataSet
            Me.dgvMonedas.DataMember = "monedas"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvClientes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMonedas.CellDoubleClick
        Try
            With Me.dgvMonedas
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
            End With
            oDataSet.Tables.Clear()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class