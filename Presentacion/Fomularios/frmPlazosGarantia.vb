Public Class frmPlazosGarantia

    Private Sub frmPlazosGarantia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim fnn As funciones
        DataGridView1.DataSource = RetornaDataTable("select * from PlazosGarantia")
    End Sub
End Class