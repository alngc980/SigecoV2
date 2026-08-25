Imports System.Data.SqlClient
Public Class frmeditarSeries
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Private Sub frmeditarSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
        Me.KeyPreview = True

        'dgvSeries.ContextMenuStrip = null;
    End Sub
    Private Sub txtCodigo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.DoubleClick
        BuscarProducto()
    End Sub

    Private Sub BuscarProducto()
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
        If Trim(Me.txtCodigo.Text) = "" Then Exit Sub
        If Not IsNumeric(Me.txtCodigo.Text) Then Exit Sub

        CargarSeriesProducto()
    End Sub

    Private Sub CargarSeriesProducto()
        oDataSet = New DataSet()

        Try
            Dim daNumerosSerie As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM numerosSerie WHERE idProducto=" & CInt(txtCodigo.Text) & " ORDER BY numItem", Connection)
            Connection.Open()
            daNumerosSerie.Fill(oDataSet, "numerosSerie")

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("No existen datos de este producto.", MsgBoxStyle.Information)
                Me.dgvSeries.DataSource = Nothing
                Me.txtCodigo.Text = ""
                Me.txtProducto.Text = ""
                Me.txtCodigo.Focus()
                Exit Sub
            End If

            Me.dgvSeries.DataSource = oDataSet
            Me.dgvSeries.DataMember = "numerosSerie"
            ConfigurarGrillaSeries()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Connection.State <> ConnectionState.Closed Then Connection.Close()
        End Try
    End Sub

    Private Sub ConfigurarGrillaSeries()
        With Me.dgvSeries
            .Columns(0).ReadOnly = True
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).ReadOnly = True
            .Columns(5).ReadOnly = True
            If .Columns.Count > 8 Then .Columns(8).ReadOnly = True
        End With
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        BuscarProducto()
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            If dgvSeries.Rows.Count <= 0 Then
                MsgBox("No hay información procesada para grabar.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.dgvSeries.IsCurrentCellInEditMode Then Me.dgvSeries.EndEdit()

            Dim mensajeValidacion As String = ""
            If Not ValidarDatosSeries(mensajeValidacion) Then
                MsgBox(mensajeValidacion, MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Está seguro de modificar datos de los productos?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                For i As Integer = 0 To dgvSeries.Rows.Count - 1
                    Dim idProducto As String = ValorCelda(i, 0)
                    Dim numItem As String = ValorCelda(i, 8)

                    SqlString = "UPDATE numerosSerie Set numSerie='" & SqlTexto(ValorCelda(i, 1)) & "',numMotor='" & _
                    SqlTexto(ValorCelda(i, 2)) & "',numChasis='" & SqlTexto(ValorCelda(i, 3)) & "',color='" & _
                    SqlTexto(ValorCelda(i, 6)) & "',anoFab='" & SqlTexto(ValorCelda(i, 7)) & "'" & _
                    " where idProducto=" & CInt(idProducto) & " and numItem=" & CInt(numItem)

                    ListSqlStrings.Add(SqlString)
                Next

                If transaccionProducto(ListSqlStrings) Then
                    MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    CargarSeriesProducto()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function ValidarDatosSeries(ByRef mensaje As String) As Boolean
        For fila As Integer = 0 To Me.dgvSeries.Rows.Count - 1
            If ValorCelda(fila, 0) = "" Or ValorCelda(fila, 8) = "" Then
                mensaje = "Hay registros sin idProducto o numItem. Vuelva a cargar el producto antes de grabar."
                Return False
            End If

            If ValorCelda(fila, 1).IndexOf("'") >= 0 Or ValorCelda(fila, 2).IndexOf("'") >= 0 Or ValorCelda(fila, 3).IndexOf("'") >= 0 Then
                mensaje = "Los campos Serie, Motor y Chásis no pueden contener comillas simples."
                Me.dgvSeries.CurrentCell = Me.dgvSeries.Rows(fila).Cells(1)
                Return False
            End If

            If Not ValidarDuplicadoCampo(fila, 1, "serie", mensaje) Then Return False
            If Not ValidarDuplicadoCampo(fila, 2, "motor", mensaje) Then Return False
            If Not ValidarDuplicadoCampo(fila, 3, "chásis", mensaje) Then Return False
        Next fila

        Return True
    End Function

    Private Function ValidarDuplicadoCampo(ByVal filaActual As Integer, ByVal columna As Integer, ByVal nombreCampo As String, ByRef mensaje As String) As Boolean
        Dim valor As String = ValorCelda(filaActual, columna)
        If valor = "" Then Return True

        For fila As Integer = 0 To Me.dgvSeries.Rows.Count - 1
            If fila <> filaActual Then
                If columna = 1 Then
                    If ValorCelda(fila, 1) = valor Then
                        mensaje = "Ya existe la " & nombreCampo & " '" & valor & "' en la pantalla para este producto."
                        Me.dgvSeries.CurrentCell = Me.dgvSeries.Rows(filaActual).Cells(columna)
                        Return False
                    End If
                Else
                    If ValorCelda(fila, 2) = valor Or ValorCelda(fila, 3) = valor Then
                        mensaje = "Ya existe el número de " & nombreCampo & " '" & valor & "' en la pantalla para este producto."
                        Me.dgvSeries.CurrentCell = Me.dgvSeries.Rows(filaActual).Cells(columna)
                        Return False
                    End If
                End If
            End If
        Next fila

        If ExisteDuplicadoEnBD(filaActual, columna, valor) Then
            mensaje = "Ya existe la " & nombreCampo & " '" & valor & "' registrada en BD para este mismo producto."
            Me.dgvSeries.CurrentCell = Me.dgvSeries.Rows(filaActual).Cells(columna)
            Return False
        End If

        Return True
    End Function

    Private Function ExisteDuplicadoEnBD(ByVal filaActual As Integer, ByVal columna As Integer, ByVal valor As String) As Boolean
        Dim idProducto As Integer = CInt(ValorCelda(filaActual, 0))
        Dim numItem As Integer = CInt(ValorCelda(filaActual, 8))
        Dim dato As String = SqlTexto(valor)
        Dim sqlString As String

        If columna = 1 Then
            sqlString = "SELECT * FROM numerosSerie WHERE idProducto=" & idProducto & " AND numItem<>" & numItem & " AND LTRIM(RTRIM(ISNULL(numSerie,'')))='" & dato & "'"
        Else
            sqlString = "SELECT * FROM numerosSerie WHERE idProducto=" & idProducto & " AND numItem<>" & numItem & " AND (LTRIM(RTRIM(ISNULL(numMotor,'')))='" & dato & "' OR LTRIM(RTRIM(ISNULL(numChasis,'')))='" & dato & "')"
        End If

        Return verificarDocumento(sqlString) > 0
    End Function

    Private Function ValorCelda(ByVal fila As Integer, ByVal columna As Integer) As String
        Dim valor As Object = Me.dgvSeries.Rows(fila).Cells(columna).Value
        If valor Is Nothing OrElse valor Is DBNull.Value Then Return ""
        Return valor.ToString().Trim().ToUpper()
    End Function

    Private Function SqlTexto(ByVal valor As String) As String
        If valor Is Nothing Then Return ""
        Return valor.Trim().Replace("'", "''")
    End Function
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

    Private Sub dgvSeries_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvSeries.KeyDown
        Clipboard.Clear() ' Limpia el portapapeles
        If e.Control AndAlso e.KeyCode = Keys.C Then
            Dim oFrmAcceso As New frmaccesoAdministrador()
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                e.SuppressKeyPress = True ' Bloquea la acción de copiar
                Exit Sub
            End If
            e.SuppressKeyPress = False ' Bloquea la acción de copiar
            'MessageBox.Show("Copiar datos está deshabilitado.")
        End If
    End Sub
End Class