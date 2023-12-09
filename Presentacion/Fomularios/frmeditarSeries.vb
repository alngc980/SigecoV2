Imports System.Data.SqlClient
Public Class frmeditarSeries
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Private Sub frmeditarSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True
    End Sub
    Private Sub txtCodigo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.DoubleClick
        Try
            arrayDatos(0) = ""
            frmbuscaProducto.ShowDialog()
            If arrayDatos(0) <> "" Then
                Me.txtCodigo.Text = arrayDatos(0)
                Me.txtProducto.Text = arrayDatos(2) & ", " & arrayDatos(3) & ", " & arrayDatos(4)
                arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = "" : arrayDatos(3) = ""
                arrayDatos(4) = "" : arrayDatos(5) = "" : arrayDatos(6) = "" : arrayDatos(7) = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        oDataSet = New DataSet()

        Try
            Connection.Open()
            Dim daNumerosSerie As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM  numerosSerie where idProducto='" & CInt(txtCodigo.Text) & "'", Connection)
            daNumerosSerie.Fill(oDataSet, "numerosSerie")
            Connection.Close()

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existen datos de este producto.", MsgBoxStyle.Information)
                Me.txtCodigo.Text = ""
                Me.txtCodigo.Focus()
                Connection.Close()
                Exit Sub
            End If

            Me.dgvSeries.DataSource = oDataSet
            Me.dgvSeries.DataMember = "numerosSerie"
            With Me.dgvSeries
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).ReadOnly = True
                .Columns(5).ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Este procedimiento sólo tiene la finalidad de adjudicarle un valor incremental unitario, empezando del uno, 
        'a la tabla "numerosSerie", campo "numItem". Esto, con la finalidad de hacer único los registros y poder hacer
        'modificaciones en los campos. Por lo tanto, sólo debe usarse una vez.

        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim grupo As Integer

            oDataSet = New DataSet()
            Connection.Open()
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM  productos where idProducto>=6", Connection)
            daProductos.Fill(oDataSet, "productos")

            For i As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count - 1
                grupo = Me.oDataSet.Tables(0).Rows(i).Item(1)
                Dim daNumerosSerie As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM  numerosSerie where idProducto='" & CInt(Me.oDataSet.Tables(0).Rows(i).Item(0)) & "'", Connection)
                daNumerosSerie.Fill(oDataSet, "numerosSerie")
                Connection.Close()

                For x As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
                    If grupo <> 6 Then
                        SqlString = "UPDATE numerosSerie Set numItem='" & CInt(x + 1) & "'  where idProducto='" & _
                        CInt(Me.oDataSet.Tables(0).Rows(i).Item(0)) & "' and numSerie='" & Me.oDataSet.Tables(1).Rows(x).Item(1) & "'"
                    Else
                        SqlString = "UPDATE numerosSerie Set numItem='" & CInt(x + 1) & "'  where idProducto='" & _
                        CInt(Me.oDataSet.Tables(0).Rows(i).Item(0)) & "' and numMotor='" & Me.oDataSet.Tables(1).Rows(x).Item(2) & "'"
                    End If
                    ListSqlStrings.Add(SqlString)
                Next
                If transaccionProducto(ListSqlStrings) Then
                    'MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    ListSqlStrings.Clear()
                    Me.oDataSet.Tables(1).Clear()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                    Me.Close()
                End If
            Next
            Me.Close()
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

            If dgvSeries.Rows.Count > 0 Then
                If MsgBox("Está seguro de modificar datos de los productos?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    For i As Integer = 0 To dgvSeries.Rows.Count - 1
                        SqlString = "UPDATE numerosSerie Set numSerie='" & Me.dgvSeries.Rows(i).Cells(1).Value & "',numMotor='" & _
                        dgvSeries.Rows(i).Cells(2).Value & "',numChasis='" & Me.dgvSeries.Rows(i).Cells(3).Value & "',color='" & _
                        Me.dgvSeries.Rows(i).Cells(6).Value & "',anoFab='" & Me.dgvSeries.Rows(i).Cells(7).Value & "'" & _
                        " where idProducto='" & Me.dgvSeries.Rows(i).Cells(0).Value & "' and numItem='" & Me.dgvSeries.Rows(i).Cells(8).Value & "'"

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
            Else
                MsgBox("No hay información procesada para grabar.", MsgBoxStyle.Critical)
                'Me.txtBuscaCliente.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvSeries_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvSeries.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvSeries.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 4 Or columna = 7 Then
            letra = CShort(Validar_SoloNumeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvSeries_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvSeries.EditingControlShowing
        Dim columna As Integer = dgvSeries.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 3 Then
            DirectCast(e.Control, TextBox).MaxLength = 25
        Else
            If columna = 4 Or columna = 6 Then
                DirectCast(e.Control, TextBox).MaxLength = 10
            Else
                If columna = 7 Then
                    DirectCast(e.Control, TextBox).MaxLength = 4
                End If
            End If
        End If
    End Sub
    Private Sub dgvSeries_EditingControlShowing2(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvSeries.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvSeries.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 3 Or columna = 6 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub GroupBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseEnter
        Me.lblMsj.Text = "Haz doble clic en la cajita código para buscar el producto a editar."
    End Sub
    Private Sub GroupBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseLeave
        Me.lblMsj.Text = ""
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMsj.Text = "Haz doble clic en la cajita código para buscar el producto a editar."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMsj.Text = ""
    End Sub
    Private Sub dgvSeries_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeries.CellMouseEnter
        Me.lblMsj.Text = "Dependiendo del producto tendrá que ingresar series o N° de motor, etc.."
    End Sub
    Private Sub dgvSeries_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeries.CellMouseLeave
        Me.lblMsj.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class