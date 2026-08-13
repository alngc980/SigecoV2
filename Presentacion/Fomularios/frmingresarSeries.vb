Public Class frmingresarSeries
    Dim cMatrizSeries(,) As String = New String(,) {}
    Dim items, vBorrar As Integer
    Dim x, i, ctaRegistros As Integer
    Dim iDatos(,) As String = New String(,) {}
    Dim vEntrada As String
    Dim columna As Integer
    Dim fila As Integer
    Dim flagInterno As Integer

    Dim nCantRegistrados As Integer

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
        If e.RowIndex < 0 Then Exit Sub
        If Not EsColumnaDocumentoSerie(e.ColumnIndex) Then Exit Sub

        Dim mensaje As String = ""
        Dim valor As String = NormalizarDato(e.FormattedValue)

        If Not ValidarValorSerie(e.RowIndex, e.ColumnIndex, valor, mensaje) Then
            MsgBox(mensaje, MsgBoxStyle.Critical)
            e.Cancel = True
        End If
    End Sub

    Private Function ValidarSeriesIngresadas() As Boolean
        If Me.dgvSeries.IsCurrentCellInEditMode Then Me.dgvSeries.EndEdit()

        For fila As Integer = 0 To Me.dgvSeries.Rows.Count - 1
            For columna As Integer = 1 To 3
                If EsColumnaDocumentoSerie(columna) Then
                    Dim mensaje As String = ""
                    Dim valor As String = ValorCelda(fila, columna)
                    If Not ValidarValorSerie(fila, columna, valor, mensaje) Then
                        Me.dgvSeries.CurrentCell = Me.dgvSeries.Rows(fila).Cells(columna)
                        MsgBox(mensaje, MsgBoxStyle.Critical)
                        Return False
                    End If
                End If
            Next columna
        Next fila

        Return True
    End Function

    Private Function ValidarValorSerie(ByVal filaActual As Integer, ByVal columnaActual As Integer, ByVal valor As String, ByRef mensaje As String) As Boolean
        If valor = "" Then Return True

        If valor.IndexOf("'") >= 0 Then
            mensaje = "El dato ingresado no puede contener comillas simples."
            Return False
        End If

        If ExisteDuplicadoEnPantalla(filaActual, columnaActual, valor) Then
            mensaje = "Ya existe el dato '" & valor & "' ingresado en pantalla."
            Return False
        End If

        If ExisteDuplicadoEnMatriz(valor) Then
            mensaje = "Ya existe el dato '" & valor & "' ingresado en otro producto de este documento."
            Return False
        End If

        If ExisteDuplicadoEnBD(valor) Then
            mensaje = "Ya existe el dato '" & valor & "' registrado en la base de datos."
            Return False
        End If

        Return True
    End Function

    Private Function EsColumnaDocumentoSerie(ByVal columna As Integer) As Boolean
        If codigoGrupo <> 6 Then
            Return columna = 1
        End If

        Return columna = 2 Or columna = 3
    End Function

    Private Function ExisteDuplicadoEnPantalla(ByVal filaActual As Integer, ByVal columnaActual As Integer, ByVal valor As String) As Boolean
        For fila As Integer = 0 To Me.dgvSeries.Rows.Count - 1
            If codigoGrupo <> 6 Then
                If fila <> filaActual AndAlso ValorCelda(fila, 1) = valor Then Return True
            Else
                For columna As Integer = 2 To 3
                    If Not (fila = filaActual And columna = columnaActual) Then
                        If ValorCelda(fila, columna) = valor Then Return True
                    End If
                Next columna
            End If
        Next fila

        Return False
    End Function

    Private Function ExisteDuplicadoEnMatriz(ByVal valor As String) As Boolean
        Try
            If flagString <> "GR" Then Return False
            If y <= 0 Then Return False

            For fila As Integer = 0 To y - 1
                If Val(matrizSeries(fila, 0)) = codigoProducto Then Continue For

                If codigoGrupo <> 6 Then
                    If NormalizarDato(matrizSeries(fila, 1)) = valor Then Return True
                Else
                    If NormalizarDato(matrizSeries(fila, 2)) = valor Or NormalizarDato(matrizSeries(fila, 3)) = valor Then Return True
                End If
            Next fila
        Catch
            Return False
        End Try

        Return False
    End Function

    Private Function ExisteDuplicadoEnBD(ByVal valor As String) As Boolean
        Dim dato As String = valor.Replace("'", "''")
        Dim sqlstring As String

        If codigoGrupo <> 6 Then
            sqlstring = "SELECT * FROM numerosSerie WHERE idProducto=" & codigoProducto & " AND LTRIM(RTRIM(ISNULL(numSerie,'')))='" & dato & "'"
        Else
            sqlstring = "SELECT * FROM numerosSerie WHERE idProducto=" & codigoProducto & " AND (LTRIM(RTRIM(ISNULL(numMotor,'')))='" & dato & "' OR LTRIM(RTRIM(ISNULL(numChasis,'')))='" & dato & "')"
        End If

        Return verificarDocumento(sqlstring) > 0
    End Function

    Private Function ValorCelda(ByVal fila As Integer, ByVal columna As Integer) As String
        If Me.dgvSeries.Rows(fila).Cells(columna).Value Is Nothing Then Return ""
        Return NormalizarDato(Me.dgvSeries.Rows(fila).Cells(columna).Value)
    End Function

    Private Function NormalizarDato(ByVal valor As Object) As String
        If valor Is Nothing Then Return ""
        Return valor.ToString().Trim().ToUpper()
    End Function
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim sqlString As String = ""
            Dim listaSqlStrings As New ArrayList

            If flagString = "IS" Then 'Asignar stock inicial
                verificaIngresoRegistros()
                If ctaRegistros > 0 Then
                    MsgBox("Tiene que rellenar todos los campos del registro.", MsgBoxStyle.Critical)
                    ctaRegistros = 0
                    Exit Sub
                End If

                If Not ValidarSeriesIngresadas() Then Exit Sub

                For i As Integer = 0 To dgvSeries.Rows.Count - 1
                    If codigoGrupo <> 6 Then
                        dgvSeries.Rows(i).Cells(2).Value = "" : dgvSeries.Rows(i).Cells(3).Value = ""
                        dgvSeries.Rows(i).Cells(5).Value = ""
                    Else
                        dgvSeries.Rows(i).Cells(1).Value = ""
                    End If
                    sqlString = "insert into numerosSerie (idProducto,numSerie,numMotor,numChasis,numDoc,numDoc1,color,anoFab) VALUES ( " & _
                    codigoProducto & ",'" & dgvSeries.Rows(i).Cells(1).Value.ToString() & "','" & dgvSeries.Rows(i).Cells(2).Value.ToString() & "','" & _
                    dgvSeries.Rows(i).Cells(3).Value.ToString() & "',' ',' ','" & dgvSeries.Rows(i).Cells(4).Value.ToString() & "','')"

                    listaSqlStrings.Add(sqlString)
                Next

                If transaccionLetras(listaSqlStrings) Then
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

                If Not ValidarSeriesIngresadas() Then Exit Sub

                For i As Integer = 0 To dgvSeries.Rows.Count - 1
                    matrizSeries(i + y, 0) = codigoProducto
                    matrizSeries(i + y, 1) = Me.dgvSeries.Rows(i).Cells(1).Value
                    matrizSeries(i + y, 2) = Me.dgvSeries.Rows(i).Cells(2).Value
                    matrizSeries(i + y, 3) = Me.dgvSeries.Rows(i).Cells(3).Value
                    matrizSeries(i + y, 4) = Me.dgvSeries.Rows(i).Cells(4).Value
                    matrizSeries(i + y, 5) = Me.dgvSeries.Rows(i).Cells(5).Value

                    matrizSeries(i + y, 6) = txtCostoUnitario.Text
                    matrizSeries(i + y, 7) = txtPrecioContado.Text
                    matrizSeries(i + y, 8) = txtPrecioCredito.Text
                    matrizSeries(i + y, 9) = txtPrecioOferta.Text
                    matrizSeries(i + y, 10) = txtPrecioRemate.Text
                    matrizSeries(i + y, 11) = txtCosProm.Text
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

                If Not ValidarSeriesIngresadas() Then Exit Sub

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


    '''************************************************************************************************************************************************************************************

    Public Sub ObtenerCostoPromedio(ByVal nIdProducto As Integer)
        Dim data As DataTable = RetornaDataTable("select isnull(CONVERT(decimal(18,2), AVG(isnull(nCostUnitario,0))),0)Prom, COUNT(1)Cant, isnull(sum(isnull(nCostUnitario,0)),0) nTotal from numerosSerie where idProducto = " & nIdProducto & " and numDoc = ''")
        If data.Rows.Count > 0 Then
            txtCostoDisponibles.Text = data.Rows(0)(2).ToString()
            nCantRegistrados = CInt(data.Rows(0)(1).ToString())
        End If
    End Sub

    Public Function CalcularPrecio(ByVal nCostUnitProm As Decimal, ByVal nValor As Decimal, ByVal nValorMax As Decimal, ByVal nValorMin As Decimal) As Decimal
        Dim nPrecio As Decimal

        nValorMax = nValorMax / 100
        nValorMin = nValorMin / 100

        If nCostUnitProm <= nValor Then
            nPrecio = nCostUnitProm / (1 - nValorMax) '+ nCostUnitProm
        Else
            nPrecio = nCostUnitProm / (1 - nValorMin) '+ nCostUnitProm
        End If

        Return nPrecio
    End Function

    Public Function CalcularPrecioDescuento(ByVal nPrecioCont As Decimal, ByVal nValor As Decimal, ByVal nValorMax As Decimal, ByVal nValorMin As Decimal) As Decimal
        nValorMax = nValorMax / 100
        nValorMin = nValorMin / 100

        Dim nMontoDescontar As Decimal
        If nPrecioCont <= nValor Then
            nMontoDescontar = nPrecioCont * nValorMax
        Else
            nMontoDescontar = nPrecioCont * nValorMin
        End If

        Return nPrecioCont - nMontoDescontar
    End Function


    Public Sub ProcesarPrecios()
        Dim nCostPromFinal As Decimal
        nCostPromFinal = (CDec(txtCostoDisponibles.Text) + (CDec(txtPrecioCF.Text))) / (nCantRegistrados + txtCant.Text)

        txtCosProm.Text = nCostPromFinal

        txtPrecioContado.Text = Math.Round(CalcularPrecio(nCostPromFinal, txtValor.Text, txtMaximo1.Text, txtMinimo1.Text), 2)
        txtPrecioCredito.Text = Math.Round(CalcularPrecio(nCostPromFinal, txtValor.Text, txtMaximo2.Text, txtMinimo2.Text), 2)

        txtPrecioOferta.Text = Math.Round(CalcularPrecioDescuento(txtPrecioContado.Text, txtValor.Text, txtMaximo3.Text, txtMinimo3.Text), 2)
        txtPrecioRemate.Text = Math.Round(CalcularPrecioDescuento(txtPrecioContado.Text, txtValor.Text, txtMaximo4.Text, txtMinimo4.Text), 2)

    End Sub


    Private Sub btnProcesarPrecios_Click(sender As Object, e As EventArgs) Handles btnProcesarPrecios.Click
        ProcesarPrecios()
    End Sub

    Private Sub txtCostoDisponibles_Leave(sender As Object, e As EventArgs) Handles txtCostoDisponibles.Leave
        Validar_EsDecimal(txtCostoDisponibles)
    End Sub

    Private Sub txtPrecioCF_Leave(sender As Object, e As EventArgs) Handles txtPrecioCF.Leave
        Validar_EsDecimal(txtPrecioCF)
    End Sub

    Private Sub txtCant_Leave(sender As Object, e As EventArgs) Handles txtCant.Leave
        Validar_EsDecimal(txtCant)
    End Sub

    Private Sub txtCostoUnitario_Leave(sender As Object, e As EventArgs) Handles txtCostoUnitario.Leave, txtCosProm.Leave
        Validar_EsDecimal(txtCostoUnitario)
    End Sub

    Private Sub txtValor_Leave(sender As Object, e As EventArgs) Handles txtValor.Leave
        Validar_EsDecimal(txtValor)
    End Sub

    Private Sub txtMaximo1_Leave(sender As Object, e As EventArgs) Handles txtMaximo1.Leave
        Validar_EsDecimal(txtMaximo1)
    End Sub

    Private Sub txtMaximo2_Leave(sender As Object, e As EventArgs) Handles txtMaximo2.Leave
        Validar_EsDecimal(txtMaximo2)
    End Sub

    Private Sub txtMaximo3_Leave(sender As Object, e As EventArgs) Handles txtMaximo3.Leave
        Validar_EsDecimal(txtMaximo3)
    End Sub

    Private Sub txtMaximo4_Leave(sender As Object, e As EventArgs) Handles txtMaximo4.Leave
        Validar_EsDecimal(txtMaximo4)
    End Sub

    Private Sub txtMinimo1_Leave(sender As Object, e As EventArgs) Handles txtMinimo1.Leave
        Validar_EsDecimal(txtMinimo1)
    End Sub

    Private Sub txtMinimo2_Leave(sender As Object, e As EventArgs) Handles txtMinimo2.Leave
        Validar_EsDecimal(txtMinimo2)
    End Sub

    Private Sub txtMinimo3_Leave(sender As Object, e As EventArgs) Handles txtMinimo3.Leave
        Validar_EsDecimal(txtMinimo3)
    End Sub

    Private Sub txtMinimo4_Leave(sender As Object, e As EventArgs) Handles txtMinimo4.Leave
        Validar_EsDecimal(txtMinimo4)
    End Sub

    Private Sub txtPrecioContado_Leave(sender As Object, e As EventArgs) Handles txtPrecioContado.Leave
        Validar_EsDecimal(txtPrecioContado)
    End Sub

    Private Sub txtPrecioCredito_Leave(sender As Object, e As EventArgs) Handles txtPrecioCredito.Leave
        Validar_EsDecimal(txtPrecioCredito)
    End Sub

    Private Sub txtPrecioOferta_Leave(sender As Object, e As EventArgs) Handles txtPrecioOferta.Leave
        Validar_EsDecimal(txtPrecioOferta)
    End Sub

    Private Sub txtPrecioRemate_Leave(sender As Object, e As EventArgs) Handles txtPrecioRemate.Leave
        Validar_EsDecimal(txtPrecioRemate)
    End Sub
End Class
