Imports System.Data.SqlClient
Public Class frmmodificaPrecios
    Private oDataSet As DataSet
    Private Sub frmmodificaPrecios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT  * from productos where stoInicial>=0", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "ctaCorriente"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(1).ReadOnly = True
                .Columns(1).Visible = False
                .Columns(2).ReadOnly = True
                .Columns(3).ReadOnly = True
                .Columns(3).Visible = False
                .Columns(4).ReadOnly = True
                .Columns(5).ReadOnly = True
                .Columns(6).ReadOnly = True
                .Columns(7).ReadOnly = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).ReadOnly = False
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).ReadOnly = False
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).ReadOnly = True
                .Columns(11).ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtProducto_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        oDataSet = New DataSet()

        Try
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM productos where stoInicial>=0 and desProducto Like '" & "%" & Me.txtProducto.Text & "%" & "'", Connection)
            daProductos.Fill(oDataSet, "ctaCorriente")

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "ctaCorriente"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(1).ReadOnly = True
                .Columns(1).Visible = False
                .Columns(2).ReadOnly = True
                .Columns(3).ReadOnly = True
                .Columns(3).Visible = False
                .Columns(4).ReadOnly = True
                .Columns(5).ReadOnly = True
                .Columns(6).ReadOnly = True
                .Columns(7).ReadOnly = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).ReadOnly = False
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).ReadOnly = False
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).ReadOnly = True
                .Columns(11).ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList
        Dim oFrmAcceso As New frmaccesoAdministrador()

        Try
            If MsgBox("Está seguro de modificar precio de los productos?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                For i As Integer = 0 To dgvProductos.Rows.Count - 1
                    SqlString = "UPDATE productos Set preContado='" & dgvProductos.Rows(i).Cells(7).Value & "',preCredito ='" & _
                    dgvProductos.Rows(i).Cells(8).Value & "', preTarjeta ='" & dgvProductos.Rows(i).Cells(9).Value & "' where idProducto= '" & dgvProductos.Rows(i).Cells(0).Value & "'"
                    ListSqlStrings.Add(SqlString)
                Next
                If transaccionProducto(ListSqlStrings) Then
                    MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    Me.Close()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                    Me.Close()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 7 Or columna = 8 Or columna = 9 Then
            Dim letra As Short = CShort(Asc(e.KeyChar))

            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvProductos_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellEndEdit
        If e.ColumnIndex = 7 Or e.ColumnIndex = 8 Or e.ColumnIndex = 9 Then
            Dim celda = dgvProductos(e.ColumnIndex, e.RowIndex)

            If IsDBNull(celda.Value) Then
                celda.Value = 0
            End If
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class