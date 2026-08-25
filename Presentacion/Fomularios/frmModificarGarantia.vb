Public Class frmModificarGarantia
    Public cTipDoc, cSerDoc, cNumDoc As String

    Private Sub frmModificarGarantia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvProductos.DataSource = funciones.RetornaDataTable("EXEC STP_VerDetalleVenta '" & cTipDoc & "', '" & cSerDoc & "', " & cNumDoc)

        dgvProductos.ReadOnly = False

        For Each col As DataGridViewColumn In dgvProductos.Columns
            col.ReadOnly = True
        Next

        dgvProductos.Columns("Garantia").ReadOnly = False

    End Sub

    Private Sub dgvProductos_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProductos.CellEndEdit
        Dim header As String = dgvProductos.Columns(e.ColumnIndex).HeaderText

        If header = "Garantia" Then   ' ← texto exacto de la cabecera
            Dim TipDoc As String = dgvProductos.Rows(e.RowIndex).Cells("TipDoc").Value
            Dim SerDoc As String = dgvProductos.Rows(e.RowIndex).Cells("SerDoc").Value
            Dim NumDoc As String = dgvProductos.Rows(e.RowIndex).Cells("NumDoc").Value
            Dim IdProd As Integer = dgvProductos.Rows(e.RowIndex).Cells("IdProd").Value
            Dim Garantia As String = dgvProductos.Rows(e.RowIndex).Cells("Garantia").Value

            grabarSqlString("update vtaDetalle set nMesGarantia = " & Garantia & " where tipDocumento = '" & TipDoc & "' and serDocumento = '" & SerDoc & "' and numDocumento = " & NumDoc & " and idProducto = " & IdProd)
        End If
    End Sub
End Class