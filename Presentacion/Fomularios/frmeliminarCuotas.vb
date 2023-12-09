Imports System.Data.SqlClient
Public Class frmeliminarCuotas
    Private oDataSet As DataSet
    Private arrayLetras(100, 1) As String
    Private x, y, ctaFilas As Integer
    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoCliente.Text = arrayDatos(0)
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(3)
            ctaFilas = 0
            buscarCuotas()
        End If
    End Sub
    Private Sub buscarCuotas()

        Try
            Me.dgvLetras.Rows.Clear()
            oDataSet = New DataSet()

            Connection.Open()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where idCliente Like '" & Me.txtCodigoCliente.Text & "' ", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento,idMoneda,idCliente " & _
            "FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")
            Connection.Close()

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
                If Me.oDataSet.Tables(1).Rows(i).Item(6) = 1 Then
                    oDataRowMoneda(8) = "S/."
                Else
                    If Me.oDataSet.Tables(1).Rows(i).Item(6) = 2 Then
                        oDataRowMoneda(8) = "$"
                    Else
                        oDataRowMoneda(8) = "€"
                    End If
                End If
            Next i

            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                Me.dgvLetras.Rows.Add()
                Me.dgvLetras.Rows(i).Cells(0).Value = Me.oDataSet.Tables(1).Rows(i).Item(0)
                Me.dgvLetras.Rows(i).Cells(1).Value = Me.oDataSet.Tables(1).Rows(i).Item(1)
                Me.dgvLetras.Rows(i).Cells(2).Value = Me.oDataSet.Tables(1).Rows(i).Item(8)
                Me.dgvLetras.Rows(i).Cells(3).Value = Me.oDataSet.Tables(1).Rows(i).Item(2)
                Me.dgvLetras.Rows(i).Cells(4).Value = Me.oDataSet.Tables(1).Rows(i).Item(3)
                Me.dgvLetras.Rows(i).Cells(5).Value = Me.oDataSet.Tables(1).Rows(i).Item(4)
            Next
            Me.oDataSet.Tables.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Try
            If Me.txtCodigoCliente.Text = "" Then
                MsgBox("Use el botón 'Buscar' para elegir el cliente y eliminar sus cuotas.", MsgBoxStyle.Information)
                Exit Sub
            End If

            For i As Integer = 0 To Me.dgvLetras.Rows.Count - 1
                If Me.dgvLetras.Rows(i).Cells(6).Value = True Then ctaFilas += 1
            Next

            If ctaFilas <= 0 Then
                MsgBox("Tiene que elegir las cuotas que desea eliminar.", MsgBoxStyle.Information)
                Exit Sub
            End If

            Connection.Open()
            Dim daBuscaLetra As SqlDataAdapter = New SqlDataAdapter("SELECT *from vtaCabecera where numLetra='" & Me.dgvLetras.Rows(0).Cells(0).Value.ToString & "' ", Connection)
            daBuscaLetra.Fill(oDataSet, "vtaCabecera")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count > 0 Then
                MsgBox("No se puede eliminar las cuotas elegidas porque derivan de un procedimiento regular.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList

            For i As Integer = 0 To Me.dgvLetras.RowCount - 1
                If Me.dgvLetras.Rows(i).Cells(6).Value = True Then
                    '----revisar
                    arrayLetras(x, 0) = Me.dgvLetras.Rows(i).Cells(0).Value.ToString
                    arrayLetras(x, 1) = Me.dgvLetras.Rows(i).Cells(1).Value
                    x += 1
                    '-----
                End If
            Next

            For x = LBound(arrayLetras) To UBound(arrayLetras)
                If arrayLetras(x, 0) <> "" Then
                    SqlString = "DELETE FROM letrasClientes where numLetra='" & Me.arrayLetras(x, 0) & "' and numCorrelativo=" & CInt(Me.arrayLetras(x, 1)) & " and idCliente= " & CInt(Me.txtCodigoCliente.Text) & ""
                    ListSqlStrings.Add(SqlString)
                End If
            Next

            If MsgBox("Está seguro de eliminar las cuotas elegidas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                '------agregado 05-09-13
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If
                flag = 0
                '-----------

                If transaccionLetras(ListSqlStrings) Then
                    MsgBox("Proceso eliminación ejecutado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se eliminaron las cuotas.", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
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
        Me.ctafilas = 0

        For y = LBound(arrayLetras) To UBound(arrayLetras)
            arrayLetras(y, 0) = "" : arrayLetras(y, 1) = ""
        Next

        Me.dgvLetras.Rows.Clear()
        Me.btnBuscarCliente.Focus()
    End Sub
    Private Sub GroupBox3_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox3.MouseEnter
        Me.lblMensaje.Text = "Utilice botón 'Buscar' para escoger tus clientes y sus cuotas."
    End Sub
    Private Sub GroupBox3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox3.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvLetras_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseEnter
        Me.lblMensaje.Text = "Haz un 'Click' en la casilla verificación de columna 'Borrar', de uno o más cuotas, luego elimine."
    End Sub
    Private Sub dgvLetras_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLetras.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class