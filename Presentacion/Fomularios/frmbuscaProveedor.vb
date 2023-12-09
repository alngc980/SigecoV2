Imports System.Data.SqlClient
Public Class frmbuscaProveedor
    Private oDataSet As DataSet
    Private Sub frmbuscaProveedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM proveedores where idProveedor<>1", Connection)
            daClientes.Fill(oDataSet, "provedores")

            Me.dgvProveedores.DataSource = oDataSet
            Me.dgvProveedores.DataMember = "provedores"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaProveedor.KeyUp
        oDataSet = New DataSet()

        Try
            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM proveedores where idProveedor<>1 and nomProveedor Like '" & Me.txtBuscaProveedor.Text & "%" & "'  ", Connection)
            daClientes.Fill(oDataSet, "provedores")

            Me.dgvProveedores.DataSource = oDataSet
            Me.dgvProveedores.DataMember = "provedores"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProveedores_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProveedores.CellDoubleClick
        Try
            With Me.dgvProveedores
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
                arrayDatos(3) = .Item(3, e.RowIndex).Value
            End With
            oDataSet.Tables.Clear()
            Me.Close()
            Me.txtBuscaProveedor.Clear()
            Me.txtBuscaProveedor.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class