Imports System.Data.SqlClient
Public Class frmanularCancelacionNA
    Private oDataSet As DataSet
    Private Sub frmanularCancelacionNA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtSerieDocumento.Text = "01"
    End Sub
    Private Sub btnBuscarCuotas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCuotas.Click
        Me.dgvLetras.Rows.Clear()
        oDataSet = New DataSet()

        If Me.txtNumDocumento.Text = "" Then
            MsgBox("Ingrese número de documento para continuar.", MsgBoxStyle.Information)
            Me.txtNumDocumento.Focus()
            Exit Sub
        End If

        Try
            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("SELECT * from letrasClientes where numRecibo='NA'+'" & Trim(Me.txtNumDocumento.Text) & "'", Connection)
            daLetras.Fill(oDataSet, "Letras")

            If oDataSet.Tables(0).Rows.Count() <= 0 Then
                MsgBox("Número de nota abono inexistente.", MsgBoxStyle.Information)
                Me.txtNumDocumento.Text = ""
                Me.btnBuscarCuotas.Focus()
                Exit Sub
            End If

            Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * from clientes where idCliente='" & Me.oDataSet.Tables(0).Rows(0).Item(1) & "'", Connection)
            daClientes.Fill(oDataSet, "Clientes")

            Me.txtCodigoCliente.Text = Me.oDataSet.Tables(1).Rows(0).Item(0)
            Me.txtNombres.Text = Me.oDataSet.Tables(1).Rows(0).Item(1)
            Me.txtDireccion.Text = Me.oDataSet.Tables(1).Rows(0).Item(2)
            Me.txtDNI.Text = Me.oDataSet.Tables(1).Rows(0).Item(3)

            Dim colMoneda As DataColumn = New DataColumn()
            colMoneda.AllowDBNull = True
            colMoneda.Caption = "Moneda"
            colMoneda.ColumnName = "moneda"
            Me.oDataSet.Tables(0).Columns.Add(colMoneda)

            Dim oDataRowMoneda As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowMoneda = Me.oDataSet.Tables(0).Rows(i)
                If Me.oDataSet.Tables(0).Rows(i).Item(10) = 1 Then
                    oDataRowMoneda(15) = "S/."
                Else
                    If Me.oDataSet.Tables(0).Rows(i).Item(10) = 2 Then
                        oDataRowMoneda(15) = "$"
                    Else
                        oDataRowMoneda(15) = "€"
                    End If
                End If
            Next i

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                Me.dgvLetras.Rows.Add()
                Me.dgvLetras.Rows(i).Cells(0).Value = Me.oDataSet.Tables(0).Rows(i).Item(0)
                Me.dgvLetras.Rows(i).Cells(1).Value = Me.oDataSet.Tables(0).Rows(i).Item(3)
                Me.dgvLetras.Rows(i).Cells(2).Value = Me.oDataSet.Tables(0).Rows(i).Item(15)
                If Me.oDataSet.Tables(0).Rows(i).Item(10) <= 1 Then
                    Me.dgvLetras.Rows(i).Cells(3).Value = Me.oDataSet.Tables(0).Rows(i).Item(4)
                Else
                    Me.dgvLetras.Rows(i).Cells(4).Value = Me.oDataSet.Tables(0).Rows(i).Item(5)
                End If
                Me.dgvLetras.Rows(i).Cells(5).Value = ""
                Me.dgvLetras.Rows(i).Cells(6).Value = ""
                Me.dgvLetras.Rows(i).Cells(7).Value = Me.oDataSet.Tables(0).Rows(i).Item(6)
                Me.dgvLetras.Rows(i).Cells(8).Value = Me.oDataSet.Tables(0).Rows(i).Item(7)
                Me.dgvLetras.Rows(i).Cells(9).Value = Me.oDataSet.Tables(0).Rows(i).Item(8)
                Me.dgvLetras.Rows(i).Cells(10).Value = Me.oDataSet.Tables(0).Rows(i).Item(11)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim sqlString As String = ""
        Dim listaSqlStrings As New ArrayList

        Try
            If Me.dgvLetras.Rows.Count <= 0 Then
                MsgBox("No hay información para procesar.", MsgBoxStyle.Critical)
                Me.btnBuscarCuotas.Focus()
                Exit Sub
            End If


            sqlString = "UPDATE letrasClientes set fecPago='01-01-1900',numRecibo='',status='' where numRecibo='NA'+'" & Trim(Me.txtNumDocumento.Text) & "'"
            listaSqlStrings.Add(sqlString)

            If MsgBox("Está seguro de anular proceso de cancelación de las cuotas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If
                flag = 0

                If transaccionLetras(listaSqlStrings) Then
                    MsgBox("Proceso anulación ejecutado correctamente.", MsgBoxStyle.Information)
                    btnLimpiar_Click(sender, e)
                Else
                    MsgBox("Error en el proceso, no se anularon los documentos.", MsgBoxStyle.Critical)
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
        Me.txtNumDocumento.Text = ""
        Me.oDataSet.Tables.Clear()
        Me.dgvLetras.Rows.Clear()
        Me.btnBuscarCuotas.Focus()
    End Sub
    Private Sub txtNumDocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumDocumento.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub GroupBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseEnter
        Me.lblMensaje.Text = "Ingrese el número de Nota Abono y luego 'Enter' o Click en el botón 'Buscar' para hacer la búsqueda del documento."
    End Sub
    Private Sub GroupBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class