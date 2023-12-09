Imports System.Data.SqlClient
Public Class frmbuscaLetra
    Private oDataSet As DataSet
    Private Sub frmbuscaLetra_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.txtCliente.Clear()
        Me.dgvClientes.DataMember = ""
        Me.txtCliente.Focus()
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.txtCliente.Clear()
        oDataSet = New DataSet()

        Try
            If flag <> 1 Then
                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where dni Like '" & Me.txtcodCliente.Text & "' ", Connection)
                daCliente.Fill(oDataSet, "cliente")

                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento," & _
                "fecPago,idCliente FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")
            Else
                Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where idCliente Like '" & Me.txtcodCliente.Text & "' ", Connection)
                daCliente.Fill(oDataSet, "cliente")

                Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento," & _
                "fecPago,idCliente FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and numLetra Like '" & numeroLetra & "' ", Connection)
                daCTaCte.Fill(oDataSet, "ctaCorriente")
            End If

            Dim colNombre As DataColumn = New DataColumn()
            colNombre.AllowDBNull = True
            colNombre.Caption = "Nombre Cliente"
            colNombre.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(1).Columns.Add(colNombre)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows.Item(x).Item(0) = Me.oDataSet.Tables(1).Rows.Item(i).Item(7) Then
                        oDataRow = Me.oDataSet.Tables(1).Rows(i)
                        oDataRow(8) = Me.oDataSet.Tables(0).Rows.Item(x).Item(1)
                    End If
                Next x
            Next i

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "ctaCorriente"
            With Me.dgvClientes
                .Columns(0).DisplayIndex = 1
                .Columns(1).DisplayIndex = 2
                .Columns(2).DisplayIndex = 3
                .Columns(3).DisplayIndex = 4
                .Columns(4).DisplayIndex = 5
                .Columns(5).DisplayIndex = 6
                .Columns(6).DisplayIndex = 7
                .Columns(7).DisplayIndex = 8
                .Columns(8).DisplayIndex = 0
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub txtCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyUp
        Me.txtcodCliente.Text = ""
        oDataSet = New DataSet()

        Try
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where nombres Like '" & "%" & Me.txtCliente.Text & "%" & "' ", Connection)
            daCliente.Fill(oDataSet, "cliente")

            If oDataSet.Tables(0).Rows.Count <= 0 Then
                Exit Sub
            End If

            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT numLetra,numCorrelativo,impLetra,impLetraME,fecEmision,fecVencimiento," & _
            "fecPago,idCliente FROM letrasClientes where ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A')) and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            Dim colNombre As DataColumn = New DataColumn()
            colNombre.AllowDBNull = True
            colNombre.Caption = "Nombre Cliente"
            colNombre.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(1).Columns.Add(colNombre)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows.Item(x).Item(0) = Me.oDataSet.Tables(1).Rows.Item(i).Item(7) Then
                        oDataRow = Me.oDataSet.Tables(1).Rows(i)
                        oDataRow(8) = Me.oDataSet.Tables(0).Rows.Item(x).Item(1)
                    End If
                Next x
            Next i

            Me.dgvClientes.DataSource = oDataSet
            Me.dgvClientes.DataMember = "ctaCorriente"
            With Me.dgvClientes
                .Columns(0).DisplayIndex = 1
                .Columns(1).DisplayIndex = 2
                .Columns(2).DisplayIndex = 3
                .Columns(3).DisplayIndex = 4
                .Columns(4).DisplayIndex = 5
                .Columns(5).DisplayIndex = 6
                .Columns(6).DisplayIndex = 7
                .Columns(7).DisplayIndex = 8
                .Columns(8).DisplayIndex = 0
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            Me.oDataSet.Tables.Clear()
        End Try
    End Sub
    Private Sub procesaLetra()
        oDataSet = New DataSet()
        Try
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM letrasClientes where numLetra Like '" & Trim(arrayDatos(0)) & "' order by numCorrelativo", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            If arrayDatos(1) = 1 Then
                Exit Sub
            Else
                If CDate(Me.oDataSet.Tables(0).Rows(arrayDatos(1) - 2).Item(8)) = CDate("01-01-1900") Then
                    MsgBox("No se puede cancelar o amortizar esta letra, hay una previa pendiente.", MsgBoxStyle.Information)
                    arrayDatos(0) = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvClientes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellDoubleClick
        Try
            With Me.dgvClientes
                arrayDatos(0) = .Item(0, e.RowIndex).Value
                arrayDatos(1) = .Item(1, e.RowIndex).Value
                arrayDatos(2) = .Item(2, e.RowIndex).Value
                arrayDatos(3) = .Item(3, e.RowIndex).Value
                oDataSet.Tables.Clear()
            End With
            procesaLetra()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvClientes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellClick
        Dim columna As Integer = dgvClientes.CurrentCell.ColumnIndex

        If columna = 0 Then
            flag = 1
            Me.txtcodCliente.Text = Me.dgvClientes.Rows(Me.dgvClientes.CurrentCell.RowIndex).Cells(7).Value
            numeroLetra = Me.dgvClientes.Rows(Me.dgvClientes.CurrentCell.RowIndex).Cells(0).Value
            Me.btnBuscar_Click(sender, e)
            numeroLetra = ""
            flag = 0
        End If
    End Sub
    Private Sub txtcodCliente_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcodCliente.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Ingrese DNI o RUC,  o inicial del apellido del cliente para hacer la búsqueda."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvLetras_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellMouseEnter
        Me.lblMensaje.Text = "Haz click en el número de cta. cte. para individualizarlo y ordenarlo por el correlativo, si así lo desea."
    End Sub
    Private Sub dgvLetras_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Close()
    End Sub
End Class