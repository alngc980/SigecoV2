Imports System.Data.SqlClient
Public Class frmmostrarSeries
    Private oDataSet As DataSet
    Private Sub frmmostrarSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daSeries As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM numerosSerie where idProducto='" & CInt(codigoProducto) & "' ", Connection)
            daSeries.Fill(oDataSet, "numerosSerie")

            Me.dgvSeries.DataSource = oDataSet
            Me.dgvSeries.DataMember = "numerosSerie"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
End Class