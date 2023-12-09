Imports System.Data.SqlClient
Public Class frmeditarPersonal
    Private oDataSet As DataSet
    Dim texto As New RichTextBox
    Private Sub frmeditarPersonal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Controls.Add(texto)
            texto.Multiline = True
            texto.Visible = False

            oDataSet = New DataSet()
            Connection.Open()
            Dim daVendedores As SqlDataAdapter = New SqlDataAdapter("SELECT  * from personal", Connection)
            daVendedores.Fill(oDataSet, "personal")
            Connection.Close()

            Me.dgvVendedores.DataSource = oDataSet
            Me.dgvVendedores.DataMember = "personal"
            With Me.dgvVendedores
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).ReadOnly = True
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtVendedor_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVendedor.KeyUp
        Dim daVendedores As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM personal where apePaterno Like '" & Me.txtVendedor.Text & "%" & "'  ", Connection)

        Try
            oDataSet = New DataSet()
            Connection.Open()
            daVendedores.Fill(oDataSet, "personal")
            Connection.Close()

            Me.dgvVendedores.DataSource = oDataSet
            Me.dgvVendedores.DataMember = "personal"

            With Me.dgvVendedores
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).ReadOnly = True
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            Dim oFrmaccesoUsuario As New frmaccesoUsuario()
            oFrmaccesoUsuario.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            For i As Integer = 0 To dgvVendedores.Rows.Count - 1
                SqlString = "UPDATE personal Set apePaterno='" & dgvVendedores.Rows(i).Cells(1).Value & "',apeMaterno='" & _
                dgvVendedores.Rows(i).Cells(2).Value & "',nombres='" & dgvVendedores.Rows(i).Cells(3).Value & "',direccion='" & _
                dgvVendedores.Rows(i).Cells(4).Value & "',dni='" & dgvVendedores.Rows(i).Cells(5).Value & "',telCelular='" & _
                dgvVendedores.Rows(i).Cells(6).Value & "',telFijo='" & dgvVendedores.Rows(i).Cells(7).Value & "',cargo='" & _
                dgvVendedores.Rows(i).Cells(8).Value & "' where idPersonal=" & dgvVendedores.Rows(i).Cells(0).Value & ""

                ListSqlStrings.Add(SqlString)
            Next

            If transaccionProducto(ListSqlStrings) Then
                MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                flag = 0
                Me.Close()
            Else
                MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        'Try
        '    Dim oDataRow As DataRow
        '    ' obtener el objeto fila, de la tabla del dataset en el que estamos posicionados
        '    oDataRow = Me.oDataSet.Tables("Vendedores").Rows(Me.iPosicFilaActual)
        '    oDataRow.Delete() ' borrar la fila
        '    ' mediante el método GetChanges(), obtenemos una tabla con las filas borradas
        '    Dim oTablaBorrados As DataTable
        '    oTablaBorrados = Me.oDataSet.Tables("Vendedores").GetChanges(DataRowState.Deleted)
        '    ' actualizar en el almacén de datos las filas borradas
        '    Me.oDataAdapter.Update(oTablaBorrados)
        '    ' confirmar los cambios realizados
        '    Me.oDataSet.Tables("Clientes").AcceptChanges()
        '    ' reposicionar en la primera fila
        '    Me.btnPrimero.PerformClick()

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub dgvVendedores_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvVendedores.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvVendedores.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 5 Then
            letra = CShort(Validar_SoloNumeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvVendedores_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvVendedores.EditingControlShowing
        Dim columna As Integer = dgvVendedores.CurrentCell.ColumnIndex

        If columna = 5 Then
            DirectCast(e.Control, TextBox).MaxLength = 8
        End If
    End Sub
    Private Sub dgvVendedores_EditingControlShowing2(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvVendedores.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)

        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvVendedores.CurrentCell.ColumnIndex

        If columna = 1 Or columna = 2 Or columna = 3 Or columna = 4 Or columna = 8 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class