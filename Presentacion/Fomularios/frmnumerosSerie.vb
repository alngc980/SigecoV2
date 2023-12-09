Public Class frmnumerosSerie
    Dim cMatrizSeries(,) As String = New String(,) {}
    Dim items, vBorrar As Integer
    Dim x, i, ctaRegistros As Integer
    Dim iDatos(,) As String = New String(,) {}
    Dim vEntrada As String
    Dim columna As Integer
    Dim fila As Integer
    Dim flagInterno As Integer
    Private Sub frmnumerosSerie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If flagString = "AS" Then 'Ajuste de stock cuando stockSistema>stockFisico
                Me.dgvSeries.ReadOnly = True
                For x As Integer = matrizSeries.GetLowerBound(0) To matrizSeries.GetUpperBound(0)
                    If codigoProducto = matrizSeries(x, 0) And Trim(matrizSeries(x, 4)) = "" Then
                        Me.dgvSeries.Rows.Add()
                        Me.dgvSeries.Rows(i).Cells(0).Value = matrizSeries(x, 0)
                        Me.dgvSeries.Rows(i).Cells(1).Value = matrizSeries(x, 1)
                        Me.dgvSeries.Rows(i).Cells(2).Value = matrizSeries(x, 2)
                        Me.dgvSeries.Rows(i).Cells(3).Value = matrizSeries(x, 3)
                        Me.dgvSeries.Rows(i).Cells(4).Value = matrizSeries(x, 6)
                        Me.dgvSeries.Rows(i).Cells(5).Value = matrizSeries(x, 7)
                        i += 1
                    End If
                Next x
            Else
                flag = 0
                If flagString = "ASI" Then 'Ajuste de stock cuando stockSistema<stockFisico
                    For x As Integer = matrizSeries.GetLowerBound(0) To matrizSeries.GetUpperBound(0)
                        If codigoProducto = matrizSeries(x, 0) And Trim(matrizSeries(x, 4)) = "" Then
                            Me.dgvSeries.Rows.Add()
                            Me.dgvSeries.Rows(i).Cells(0).Value = matrizSeries(x, 0)
                            Me.dgvSeries.Rows(i).Cells(1).Value = matrizSeries(x, 1)
                            Me.dgvSeries.Rows(i).Cells(2).Value = matrizSeries(x, 2)
                            Me.dgvSeries.Rows(i).Cells(3).Value = matrizSeries(x, 3)
                            Me.dgvSeries.Rows(i).Cells(4).Value = matrizSeries(x, 6)
                            Me.dgvSeries.Rows(i).Cells(5).Value = matrizSeries(x, 7)
                            Me.dgvSeries.Rows(i).ReadOnly = True
                            i += 1
                        End If
                    Next x

                    Dim vNumFilas = Me.dgvSeries.RowCount
                    For i As Integer = 0 To canNumSeries - 1
                        Me.dgvSeries.Rows.Add()
                        Me.dgvSeries.Rows(vNumFilas + i).Cells(0).Value = Me.dgvSeries.Rows(i).Cells(0).Value
                    Next i
                    If codigoGrupo = 6 Then
                        Me.dgvSeries.CurrentCell = dgvSeries.Rows(Me.dgvSeries.RowCount - canNumSeries).Cells(2)
                    Else
                        Me.dgvSeries.CurrentCell = dgvSeries.Rows(Me.dgvSeries.RowCount - canNumSeries).Cells(1)
                    End If
                Else
                    For i As Integer = 0 To canNumSeries - 1
                        Me.dgvSeries.Rows.Add()
                        Me.dgvSeries.Rows(i).Cells(0).Value = i + 1
                    Next i

                    If codigoGrupo = 6 Then
                        Me.dgvSeries.CurrentCell = dgvSeries.Rows(0).Cells(2)
                    Else
                        Me.dgvSeries.CurrentCell = dgvSeries.Rows(0).Cells(1)
                    End If
                End If

                'A partir de aqui se genera la interfaz para ingresar los datos de inicio de saldo de un producto y entrada con GR y PD
                If codigoGrupo <> 6 Then
                    Me.dgvSeries.Columns(2).ReadOnly = True
                    Me.dgvSeries.Columns(3).ReadOnly = True
                    Me.dgvSeries.Columns(5).ReadOnly = True
                Else
                    Me.dgvSeries.Columns(1).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvSeries_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvSeries.CellValidating
        ReDim iDatos(Me.dgvSeries.RowCount - 1, 3)
        Dim sqlstring As String

        If codigoGrupo <> 6 Then
            sqlstring = "SELECT * FROM numerosSerie where idProducto=" & codigoProducto & " and numSerie='" & e.FormattedValue.ToString() & "'"
        Else
            sqlstring = "SELECT * FROM numerosSerie where idProducto=" & codigoProducto & " and (numMotor='" & e.FormattedValue.ToString() & "' or numChasis='" & e.FormattedValue.ToString() & "')"
        End If

        For x As Integer = iDatos.GetLowerBound(0) To iDatos.GetUpperBound(0)
            For y As Integer = iDatos.GetLowerBound(1) To iDatos.GetUpperBound(1)
                iDatos(x, y) = Me.dgvSeries.Rows(x).Cells(y).Value
            Next y
        Next x

        For x As Integer = iDatos.GetLowerBound(0) To iDatos.GetUpperBound(0)
            For y As Integer = iDatos.GetLowerBound(1) To iDatos.GetUpperBound(1)
                If iDatos(x, y) = e.FormattedValue.ToString() And e.FormattedValue.ToString() <> "" And e.ColumnIndex >= 1 And e.ColumnIndex <= 3 Then
                    MsgBox("Ya existe entrada en la tabla.", MsgBoxStyle.Critical)
                    e.Cancel = True
                End If
            Next y
        Next x

        If verificarDocumento(sqlstring) > 0 And e.FormattedValue.ToString() <> "" And e.ColumnIndex >= 1 And e.ColumnIndex <= 3 Then
            MsgBox("Ya existe entrada en la BD.", MsgBoxStyle.Critical)
            e.Cancel = True
        End If
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim SqlString As String = ""
            Dim ListaSqlStrings As New ArrayList

            If flagString = "IS" Then 'Asignar stock inicial
                verificaIngresoRegistros()
                If ctaRegistros > 0 Then
                    MsgBox("Tiene que rellenar todos los campos del registro.", MsgBoxStyle.Critical)
                    ctaRegistros = 0
                    Exit Sub
                End If

                For i As Integer = 0 To dgvSeries.Rows.Count - 1
                    If codigoGrupo <> 6 Then
                        dgvSeries.Rows(i).Cells(2).Value = "" : dgvSeries.Rows(i).Cells(3).Value = ""
                        dgvSeries.Rows(i).Cells(5).Value = ""
                    Else
                        dgvSeries.Rows(i).Cells(1).Value = ""
                    End If
                    SqlString = "INSERT INTO numerosSerie (idProducto,numSerie,numMotor,numChasis,numDoc,numDoc1,color,anoFab) VALUES ( " & _
                    codigoProducto & ",'" & dgvSeries.Rows(i).Cells(1).Value.ToString() & "','" & dgvSeries.Rows(i).Cells(2).Value.ToString() & "','" & _
                    dgvSeries.Rows(i).Cells(3).Value.ToString() & "',' ',' ','" & dgvSeries.Rows(i).Cells(4).Value.ToString() & "','" & _
                    dgvSeries.Rows(i).Cells(5).Value.ToString() & "')"

                    ListaSqlStrings.Add(SqlString)
                Next

                If transaccionLetras(ListaSqlStrings) Then
                    'MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    flag = 1
                    Me.Close()
                Else
                    MsgBox("La información no se guardó.", MsgBoxStyle.Exclamation)
                End If
            End If

            If flagString = "GR" Then 'ingreso con GR y PD
                verificaIngresoRegistros()
                If ctaRegistros > 0 Then
                    MsgBox("Tiene que rellenar todos los campos del registro.", MsgBoxStyle.Critical)
                    ctaRegistros = 0
                    Exit Sub
                End If

                For i As Integer = 0 To dgvSeries.Rows.Count - 1
                    matrizSeries(i + y, 0) = codigoProducto
                    matrizSeries(i + y, 1) = Me.dgvSeries.Rows(i).Cells(1).Value
                    matrizSeries(i + y, 2) = Me.dgvSeries.Rows(i).Cells(2).Value
                    matrizSeries(i + y, 3) = Me.dgvSeries.Rows(i).Cells(3).Value
                    matrizSeries(i + y, 4) = Me.dgvSeries.Rows(i).Cells(4).Value
                    matrizSeries(i + y, 5) = Me.dgvSeries.Rows(i).Cells(5).Value
                    x = i
                Next
                y = y + x + 1
                flag = 1
                Me.Close()
            End If

            If flagString = "AS" Then 'Ajustar stock
                If vBorrar < nSeriesBorrar Then
                    MsgBox("Tiene que eliminar el total registros sobrantes.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                i = 0
                ReDim cMatrizSeries(matrizSeries.GetUpperBound(0) - canNumSeries, 7)
                For x As Integer = matrizSeries.GetLowerBound(0) To matrizSeries.GetUpperBound(0) - 1
                    If (codigoProducto <> matrizSeries(x, 0)) Or (codigoProducto = matrizSeries(x, 0) And Trim(matrizSeries(x, 4)) <> "") Then
                        cMatrizSeries(i, 0) = matrizSeries(x, 0)
                        cMatrizSeries(i, 1) = matrizSeries(x, 1)
                        cMatrizSeries(i, 2) = matrizSeries(x, 2)
                        cMatrizSeries(i, 3) = matrizSeries(x, 3)
                        cMatrizSeries(i, 4) = matrizSeries(x, 4)
                        cMatrizSeries(i, 5) = matrizSeries(x, 5)
                        cMatrizSeries(i, 6) = matrizSeries(x, 6)
                        cMatrizSeries(i, 7) = matrizSeries(x, 7)
                        i += 1
                    End If
                Next x

                ReDim matrizSeries(cMatrizSeries.GetUpperBound(0), 7)
                For x As Integer = cMatrizSeries.GetLowerBound(0) To cMatrizSeries.GetUpperBound(0) - 1
                    matrizSeries(x, 0) = cMatrizSeries(x, 0)
                    matrizSeries(x, 1) = cMatrizSeries(x, 1)
                    matrizSeries(x, 2) = cMatrizSeries(x, 2)
                    matrizSeries(x, 3) = cMatrizSeries(x, 3)
                    matrizSeries(x, 4) = cMatrizSeries(x, 4)
                    matrizSeries(x, 5) = cMatrizSeries(x, 5)
                    matrizSeries(x, 6) = cMatrizSeries(x, 6)
                    matrizSeries(x, 7) = cMatrizSeries(x, 7)
                Next x

                ReDim cMatrizSeries(matrizSeries.GetUpperBound(0) + Me.dgvSeries.Rows.Count(), 7)
                For x As Integer = cMatrizSeries.GetLowerBound(0) To cMatrizSeries.GetUpperBound(0) - Me.dgvSeries.Rows.Count()
                    cMatrizSeries(x, 0) = matrizSeries(x, 0)
                    cMatrizSeries(x, 1) = matrizSeries(x, 1)
                    cMatrizSeries(x, 2) = matrizSeries(x, 2)
                    cMatrizSeries(x, 3) = matrizSeries(x, 3)
                    cMatrizSeries(x, 4) = matrizSeries(x, 4)
                    cMatrizSeries(x, 5) = matrizSeries(x, 5)
                    cMatrizSeries(x, 6) = matrizSeries(x, 6)
                    cMatrizSeries(x, 7) = matrizSeries(x, 7)
                    i = x
                Next x

                For x As Integer = 0 To Me.dgvSeries.Rows.Count() - 1
                    cMatrizSeries(x + i, 0) = codigoProducto
                    cMatrizSeries(x + i, 1) = Me.dgvSeries.Rows(x).Cells(1).Value
                    cMatrizSeries(x + i, 2) = Me.dgvSeries.Rows(x).Cells(2).Value
                    cMatrizSeries(x + i, 3) = Me.dgvSeries.Rows(x).Cells(3).Value
                    cMatrizSeries(x + i, 4) = " "
                    cMatrizSeries(x + i, 5) = " "
                    cMatrizSeries(x + i, 6) = Me.dgvSeries.Rows(x).Cells(4).Value
                    cMatrizSeries(x + i, 7) = Me.dgvSeries.Rows(x).Cells(5).Value
                Next x

                ReDim matrizSeries(cMatrizSeries.GetUpperBound(0), 7)
                For x As Integer = cMatrizSeries.GetLowerBound(0) To cMatrizSeries.GetUpperBound(0) - 1
                    matrizSeries(x, 0) = cMatrizSeries(x, 0)
                    matrizSeries(x, 1) = cMatrizSeries(x, 1)
                    matrizSeries(x, 2) = cMatrizSeries(x, 2)
                    matrizSeries(x, 3) = cMatrizSeries(x, 3)
                    matrizSeries(x, 4) = cMatrizSeries(x, 4)
                    matrizSeries(x, 5) = cMatrizSeries(x, 5)
                    matrizSeries(x, 6) = cMatrizSeries(x, 6)
                    matrizSeries(x, 7) = cMatrizSeries(x, 7)
                Next x
                flag = 1
                Me.Close()
            End If

            If flagString = "ASI" Then 'Ajustar stock inicial
                verificaIngresoRegistros()
                If ctaRegistros > 0 Then
                    MsgBox("Tiene que rellenar todos los campos del registro.", MsgBoxStyle.Critical)
                    ctaRegistros = 0
                    Exit Sub
                End If

                ReDim cMatrizSeries(matrizSeries.GetUpperBound(0) + Me.dgvSeries.Rows.Count(), 7)
                For x As Integer = cMatrizSeries.GetLowerBound(0) To cMatrizSeries.GetUpperBound(0) - Me.dgvSeries.Rows.Count()
                    cMatrizSeries(x, 0) = matrizSeries(x, 0)
                    cMatrizSeries(x, 1) = matrizSeries(x, 1)
                    cMatrizSeries(x, 2) = matrizSeries(x, 2)
                    cMatrizSeries(x, 3) = matrizSeries(x, 3)
                    cMatrizSeries(x, 4) = matrizSeries(x, 4)
                    cMatrizSeries(x, 5) = matrizSeries(x, 5)
                    cMatrizSeries(x, 6) = matrizSeries(x, 6)
                    cMatrizSeries(x, 7) = matrizSeries(x, 7)
                    i = x
                Next x

                For x As Integer = Me.dgvSeries.RowCount - canNumSeries To Me.dgvSeries.Rows.Count() - 1
                    cMatrizSeries(x + i, 0) = codigoProducto
                    cMatrizSeries(x + i, 1) = Me.dgvSeries.Rows(x).Cells(1).Value
                    cMatrizSeries(x + i, 2) = Me.dgvSeries.Rows(x).Cells(2).Value
                    cMatrizSeries(x + i, 3) = Me.dgvSeries.Rows(x).Cells(3).Value
                    cMatrizSeries(x + i, 4) = " "
                    cMatrizSeries(x + i, 5) = " "
                    cMatrizSeries(x + i, 6) = Me.dgvSeries.Rows(x).Cells(4).Value
                    cMatrizSeries(x + i, 7) = Me.dgvSeries.Rows(x).Cells(5).Value
                Next x

                ReDim matrizSeries(cMatrizSeries.GetUpperBound(0), 7)
                For x As Integer = cMatrizSeries.GetLowerBound(0) To cMatrizSeries.GetUpperBound(0)
                    matrizSeries(x, 0) = cMatrizSeries(x, 0)
                    matrizSeries(x, 1) = cMatrizSeries(x, 1)
                    matrizSeries(x, 2) = cMatrizSeries(x, 2)
                    matrizSeries(x, 3) = cMatrizSeries(x, 3)
                    matrizSeries(x, 4) = cMatrizSeries(x, 4)
                    matrizSeries(x, 5) = cMatrizSeries(x, 5)
                    matrizSeries(x, 6) = cMatrizSeries(x, 6)
                    matrizSeries(x, 7) = cMatrizSeries(x, 7)
                Next x
                flag = 1
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvSeries_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeries.CellContentClick
        Me.items = 0
        If vBorrar < nSeriesBorrar Then
            If Me.dgvSeries.Columns(e.ColumnIndex).Name = "eliminar" Then
                Try
                    Me.dgvSeries.Rows.RemoveAt(e.RowIndex)
                    For i As Integer = 0 To Me.dgvSeries.Rows.Count - 1
                        items += 1
                        Me.dgvSeries.Rows(i).Cells(0).Value = items
                    Next
                    vBorrar += 1
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub
    Private Sub verificaIngresoRegistros()
        For i As Integer = 0 To Me.dgvSeries.Rows.Count - 1
            If codigoGrupo <> 6 Then
                If Trim(Me.dgvSeries.Rows(i).Cells(1).Value) = "" Or Trim(Me.dgvSeries.Rows(i).Cells(4).Value) = "" Then
                    ctaRegistros += 1
                End If
            Else
                If Trim(Me.dgvSeries.Rows(i).Cells(2).Value) = "" Or Trim(Me.dgvSeries.Rows(i).Cells(3).Value) = "" Or _
                   Trim(Me.dgvSeries.Rows(i).Cells(4).Value) = "" Or Trim(Me.dgvSeries.Rows(i).Cells(5).Value) = "" Then
                    ctaRegistros += 1
                End If
            End If
        Next i
    End Sub
    Private Sub dgvSeries_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvSeries.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvSeries.CurrentCell.ColumnIndex
        If columna = 5 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub dgvSeries1_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvSeries.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvSeries.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 3 Or columna = 4 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvSeries_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeries.CellMouseEnter
        lblMensaje.Text = "Dependiendo, tendrá que ingresar o eliminar datos producto, núm motor y chásis, color y año fab. para los motorizados; y num serie y color para los demás."
    End Sub
    Private Sub dgvSeries_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeries.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        flag = 0
        Me.Close()
    End Sub
End Class