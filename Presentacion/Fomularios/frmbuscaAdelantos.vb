Imports System.Data.SqlClient
Public Class frmbuscaAdelantos
    Private oDataSet As DataSet
    Private Sub frmbuscaAdelantos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daSeries As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM recibosClientes where concepto=1 and numLetra='" & CStr(arrayDatos(0)) & "' and numCorrelativo=" & CInt(arrayDatos(1)) & "", Connection)
            daSeries.Fill(oDataSet, "recibos")
            Connection.Close()

            Me.dgvRecibos.DataSource = oDataSet
            Me.dgvRecibos.DataMember = "recibos"
            arrayDatos(0) = ""
            arrayDatos(1) = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
End Class