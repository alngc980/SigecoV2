Public Class frmsaldoXMovimiento

    Private Sub frmsaldoXMovimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = funciones.RetornaDataTable("rptKardex_Tarjeta")
        DataGridView1.Columns(0).Width = 50
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 150
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 50
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count > 0 Then
            Dim fila As DataGridViewRow = DataGridView1.CurrentRow
            Dim valor As String = fila.Cells("IdProducto").Value.ToString()
            Dim frm = New frmkardexProductoSimple
            frm.AbrirFormulario(valor)
            frm.ShowDialog()
        End If

    End Sub
End Class