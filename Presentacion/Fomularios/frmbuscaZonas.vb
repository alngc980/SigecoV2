Imports System.Data.SqlClient
Public Class frmbuscaZonas
    Private oDataSet As DataSet
    Private Sub frmbuscaZonas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daZonas As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM zonas", Connection)
            daZonas.Fill(oDataSet, "zonas")

            Me.dgvZonas.DataSource = oDataSet
            Me.dgvZonas.DataMember = "zonas"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvZonas_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvZonas.CellDoubleClick
        Try
            With Me.dgvZonas
                arrayDatos(0) = .Item(0, e.RowIndex).Value
            End With
            oDataSet.Tables.Clear()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class