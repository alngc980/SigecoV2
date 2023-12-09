Imports System.Data.SqlClient
Public Class frmcancelarCuotas
    Private oDataSet As DataSet
    Private arrayLetras(100, 6) As String
    Private x, y, ctaFilas As Integer
    Dim sumaAmortizacionesMN, sumaAmortizacionesME As Decimal
    Private Sub frmcancelarCuotas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(3)
            arrayDatos(0) = "" : arrayDatos(1) = ""
            arrayDatos(2) = "" : arrayDatos(3) = ""
            ctaFilas = 0
            buscarCuotas()
        End If
    End Sub
    Private Sub buscarCuotas()
        Try
            sumaAmortizacionesMN = 0
            sumaAmortizacionesME = 0

            Me.dgvLetras.Rows.Clear()
            oDataSet = New DataSet()

            If flag <> 1 Then
                Connection.Open()
                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where idCliente Like '" & Me.txtCodigoCliente.Text & "' ", Connection)
                daCliente.Fill(oDataSet, "clientes")

                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,fecPago,idMoneda,idCliente " & _
                "FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")

                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes", Connection)
                daRecibos.Fill(oDataSet, "recibos")
                Connection.Close()
            Else
                Connection.Open()
                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where idCliente Like '" & Me.txtCodigoCliente.Text & "' ", Connection)
                daCliente.Fill(oDataSet, "clientes")

                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,fecPago,idMoneda,idCliente " & _
                "FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and numLetra Like '" & numeroLetra & "' and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")

                Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes", Connection)
                daRecibos.Fill(oDataSet, "recibos")
                Connection.Close()
            End If

            If Me.oDataSet.Tables(1).Rows.Count <= 0 Then
                MsgBox("Cliente no tiene cuotas pendientes.", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(1).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(1).Rows(i)
                If Me.oDataSet.Tables(1).Rows(i).Item(7) = 1 Then
                    oDataRowMoneda(9) = "S/."
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(7) = 2 Then
                        oDataRowMoneda(9) = "$"
                    Else
                        oDataRowMoneda(9) = "€"
                    End If
                End If
            Next i

            Dim colAmortizacion As DataColumn = New DataColumn()
            colAmortizacion.AllowDBNull = True
            colAmortizacion.Caption = "Amortizaciones"
            colAmortizacion.ColumnName = "amortizaciones"
            colAmortizacion.DefaultValue = 0
            Me.oDataSet.Tables(1).Columns.Add(colAmortizacion)

            Dim colSaldo As DataColumn = New DataColumn()
            colSaldo.AllowDBNull = True
            colSaldo.Caption = "Saldos"
            colSaldo.ColumnName = "saldos"
            colSaldo.DefaultValue = 0
            Me.oDataSet.Tables(1).Columns.Add(colSaldo)

            Dim oDataRowAmortiza As DataRow
            Dim oDataRowSaldos As DataRow

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                oDataRowAmortiza = Me.oDataSet.Tables(1).Rows(i)
                oDataRowSaldos = Me.oDataSet.Tables(1).Rows(i)
                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    If Me.oDataSet.Tables(1).Rows(i).Item(0) = Me.oDataSet.Tables(2).Rows(x).Item(2) And _
                       Me.oDataSet.Tables(1).Rows(i).Item(1) = Me.oDataSet.Tables(2).Rows(x).Item(5) Then

                        If Me.oDataSet.Tables(2).Rows(x).Item(14) > 1 Then
                            sumaAmortizacionesME += Me.oDataSet.Tables(2).Rows(x).Item(4)
                            oDataRowAmortiza(10) = sumaAmortizacionesME
                        Else
                            sumaAmortizacionesMN += Me.oDataSet.Tables(2).Rows(x).Item(3)
                            oDataRowAmortiza(10) = sumaAmortizacionesMN
                        End If
                    End If
                    If Me.oDataSet.Tables(1).Rows(i).Item(7) > 1 Then
                        oDataRowSaldos(11) = Me.oDataSet.Tables(1).Rows(i).Item(3) - sumaAmortizacionesME
                    Else
                        oDataRowSaldos(11) = Me.oDataSet.Tables(1).Rows(i).Item(2) - sumaAmortizacionesMN
                    End If
                Next x
                sumaAmortizacionesME = 0
                sumaAmortizacionesMN = 0
            Next i

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                Me.dgvLetras.Rows.Add()
                Me.dgvLetras.Rows(i).Cells(0).Value = Me.oDataSet.Tables(1).Rows(i).Item(0)
                Me.dgvLetras.Rows(i).Cells(1).Value = Me.oDataSet.Tables(1).Rows(i).Item(1)
                Me.dgvLetras.Rows(i).Cells(2).Value = Me.oDataSet.Tables(1).Rows(i).Item(9)
                Me.dgvLetras.Rows(i).Cells(3).Value = Me.oDataSet.Tables(1).Rows(i).Item(2)
                Me.dgvLetras.Rows(i).Cells(4).Value = Me.oDataSet.Tables(1).Rows(i).Item(3)
                Me.dgvLetras.Rows(i).Cells(5).Value = Me.oDataSet.Tables(1).Rows(i).Item(10)
                Me.dgvLetras.Rows(i).Cells(6).Value = Me.oDataSet.Tables(1).Rows(i).Item(11)
                Me.dgvLetras.Rows(i).Cells(7).Value = Me.oDataSet.Tables(1).Rows(i).Item(4)
                Me.dgvLetras.Rows(i).Cells(8).Value = Me.oDataSet.Tables(1).Rows(i).Item(5)
                Me.dgvLetras.Rows(i).Cells(9).Value = Me.oDataSet.Tables(1).Rows(i).Item(6)
                Me.dgvLetras.Rows(i).Cells(10).Value = ""
                Me.dgvLetras.Rows(i).Cells(11).Value = Me.oDataSet.Tables(1).Rows(i).Item(7)
            Next
            Me.oDataSet.Tables.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        x = 0
        Try
            If Me.txtCodigoCliente.Text = "" Then
                MsgBox("Use el botón 'Buscar' para elegir el cliente y cancelar sus cuotas.", MsgBoxStyle.Information)
                Exit Sub
            End If

            For i As Integer = 0 To Me.dgvLetras.Rows.Count - 1
                If Trim(Me.dgvLetras.Rows(i).Cells(10).Value) <> "" Then ctaFilas += 1
            Next

            If ctaFilas <= 0 Then
                MsgBox("Tiene que indicar fecha de pago y N° recibo para continuar.", MsgBoxStyle.Information)
                Exit Sub
            End If

            oDataSet = New DataSet()
            Connection.Open()
            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                If Me.dgvLetras.Rows(i).Cells(10).Value <> "" Then
                    Dim daBuscaLetra As SqlDataAdapter = New SqlDataAdapter("SELECT *from vtaCabecera where numLetra='" & Me.dgvLetras.Rows(i).Cells(0).Value.ToString & "' ", Connection)
                    daBuscaLetra.Fill(oDataSet, "vtaCabecera")
                End If
            Next
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count > 0 Then
                MsgBox("No se puede cancelar las cuotas elegidas porque derivan de un procedimiento regular.", MsgBoxStyle.Critical)
                Me.btnLimpiar_Click(sender, e)
                Exit Sub
            End If

            Dim SqlString As String = ""
            Dim SqlString1 As String = ""
            Dim ListSqlStrings As New ArrayList

            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                If Trim(Me.dgvLetras.Rows(i).Cells(10).Value) <> "" Then
                    arrayLetras(x, 0) = Me.dgvLetras.Rows(i).Cells(0).Value
                    arrayLetras(x, 1) = Me.dgvLetras.Rows(i).Cells(1).Value
                    arrayLetras(x, 2) = Me.dgvLetras.Rows(i).Cells(9).Value
                    arrayLetras(x, 3) = Me.dgvLetras.Rows(i).Cells(10).Value
                    If Me.dgvLetras.Rows(i).Cells(11).Value > 1 Then
                        arrayLetras(x, 4) = 0
                        arrayLetras(x, 5) = Me.dgvLetras.Rows(i).Cells(6).Value
                    Else
                        arrayLetras(x, 4) = Me.dgvLetras.Rows(i).Cells(6).Value
                        arrayLetras(x, 5) = 0
                    End If
                    arrayLetras(x, 6) = Me.dgvLetras.Rows(i).Cells(11).Value
                    x += 1
                End If
            Next

            For x = LBound(arrayLetras) To UBound(arrayLetras)
                If arrayLetras(x, 0) <> "" Then
                    If CDate(arrayLetras(x, 2)) = CDate("01-01-1900") Then
                        MsgBox("No ha ingresado fecha de cancelación de una o algunas letras.", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If
            Next

            For x = LBound(arrayLetras) To UBound(arrayLetras)
                If arrayLetras(x, 0) <> "" Then
                    SqlString = "UPDATE letrasClientes Set fecPago='" & arrayLetras(x, 2) & "', numRecibo='" & Trim(arrayLetras(x, 3)) & _
                                               "', status='C' where numLetra='" & Me.arrayLetras(x, 0) & "' and numCorrelativo=" & CInt(Me.arrayLetras(x, 1)) & " and idCliente= " & CInt(Me.txtCodigoCliente.Text) & ""
                    ListSqlStrings.Add(SqlString)
                End If
            Next

            'For x = LBound(arrayLetras) To UBound(arrayLetras)
            '    If arrayLetras(x, 0) <> "" Then
            '        SqlString1 = "INSERT INTO recibosClientes (idRecibo,concepto,numLetra,impDocumento,impDocumentoME,numCorrelativo,numDocGenCI," & _
            '                     "numDocGenACI,idCliente,idVendedor,fecEmision,fecVencimiento,fecPago,descuento,idMoneda,tipCambio,status) VALUES (" & _
            '                      CInt(arrayLetras(x, 3)) & ",2,'" & Me.arrayLetras(x, 0) & "'," & CDec(Me.arrayLetras(x, 4)) & "," & _
            '                      CDec(Me.arrayLetras(x, 5)) & "," & CInt(Me.arrayLetras(x, 1)) & ",' ',' '," & Me.txtCodigoCliente.Text & _
            '                      ",1,'" & arrayLetras(x, 2) & "',' ',' ',0," & CInt(Me.arrayLetras(x, 6)) & ",0,'C')"
            '        ListSqlStrings.Add(SqlString1)
            '    End If
            'Next

            If MsgBox("Está seguro de cancelar las cuotas elegidas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                '------agregado 05-09-13
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If
                flag = 0
                '-----------

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Proceso cancelación ejecutado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se cancelaron las cuotas.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        If Me.txtCodigoCliente.Text = "" Then
            MsgBox("No hay nada que limpiar.", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtDNI.Text = ""
        Me.x = 0
        Me.ctaFilas = 0

        For y = LBound(arrayLetras) To UBound(arrayLetras)
            arrayLetras(y, 0) = "" : arrayLetras(y, 1) = "" : arrayLetras(y, 2) = ""
            arrayLetras(y, 3) = "" : arrayLetras(y, 4) = "" : arrayLetras(y, 5) = ""
        Next

        Me.oDataSet.Tables.Clear()
        Me.dgvLetras.Rows.Clear()
        Me.btnBuscarCliente.Focus()
    End Sub
    Private Sub dgvLetras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellClick
        Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex

        If columna = 0 Then
            flag = 1
            numeroLetra = Me.dgvLetras.Rows(Me.dgvLetras.CurrentCell.RowIndex).Cells(0).Value
            buscarCuotas()
            numeroLetra = ""
            flag = 0
        End If
    End Sub
    'Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvLetras.EditingControlShowing
    '    Dim validar As TextBox = CType(e.Control, TextBox)
    '    AddHandler validar.KeyPress, AddressOf validar_Keypress
    'End Sub
    'Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex
    '    Dim letra As Short = CShort(Asc(e.KeyChar))

    '    If columna = 10 Then
    '        letra = CShort(Validar_Letras_NC(letra))
    '        If letra = 0 Then
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub
    Private Sub dgvProductos_EditingControlShowing1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvLetras.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvLetras.CurrentCell.ColumnIndex
        If columna = 10 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvLetras_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvLetras.MouseDoubleClick
        Dim ofrmbuscaAdelantos As New frmbuscaAdelantos()
        Try
            If Me.dgvLetras.Rows(dgvLetras.CurrentCell.RowIndex).Cells(5).Value > 0 Then
                arrayDatos(0) = Me.dgvLetras.Rows(dgvLetras.CurrentCell.RowIndex).Cells(0).Value
                arrayDatos(1) = Me.dgvLetras.Rows(dgvLetras.CurrentCell.RowIndex).Cells(1).Value
                frmbuscaAdelantos.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub GroupBox3_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox3.MouseEnter
        Me.lblMensaje.Text = "Utilice botón 'Buscar' para escoger tus clientes y sus cuotas."
    End Sub
    Private Sub GroupBox3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox3.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvLetras_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseEnter
        Me.lblMensaje.Text = "Modifique fecha pago e ingrese N° de recibo si desea cancelar la cuota o cuotas, luego haz click en el botón 'Procesar'."
    End Sub
    Private Sub dgvLetras_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class