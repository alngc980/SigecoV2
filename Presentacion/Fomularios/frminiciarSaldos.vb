Imports System.Data.SqlClient
Public Class frminiciarSaldos
    Private oDataSet As DataSet
    Private txtIdGrupo As Integer
    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        arrayDatos(0) = ""
        frmbuscaProducto.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoProducto.Text = arrayDatos(0)
            Me.txtIdGrupo = arrayDatos(1)
            Me.txtDescripcion.Text = arrayDatos(2)
            Me.txtMarca.Text = arrayDatos(3)
            Me.txtModelo.Text = arrayDatos(4)
            Me.txtStockInicial.Focus()

            arrayDatos(0) = "" : arrayDatos(1) = "" : arrayDatos(2) = ""
            arrayDatos(3) = "" : arrayDatos(4) = ""
        End If
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String
        Dim ofrmnumerosSerie As New frmnumerosSerie()
        Dim fechaCierre As String

        fechaCierre = devuelveFecha("SELECT * FROM cierreDiario")

        Try
            If (Me.txtCodigoProducto.Text <> "" And Me.txtDescripcion.Text <> "" And Me.txtStockInicial.Text <> "") Then

                If Me.buscarCodigo(Trim(Me.txtCodigoProducto.Text)) >= 1 Then
                    MsgBox("Producto ya fue inicializado.", MsgBoxStyle.Exclamation)
                    Me.limpiar()
                    Exit Sub
                End If

                SqlString = "INSERT INTO saldosAlmacenes (idProducto,stock,fechaSaldo) VALUES (" & Me.txtCodigoProducto.Text & " ,'" & _
                            Me.txtStockInicial.Text + "' ,'" & CDate(fechaCierre) & "' )"

                If MsgBox("Crear números de serie de este producto?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    flagString = "IS"
                    codigoProducto = Me.txtCodigoProducto.Text
                    codigoGrupo = txtIdGrupo
                    canNumSeries = Me.txtStockInicial.Text
                    ofrmnumerosSerie.ShowDialog()
                    flagString = ""
                    codigoProducto = 0
                    codigoGrupo = 0
                    canNumSeries = 0
                End If

                If flag = 1 Then
                    If grabarSqlString(SqlString) Then
                        MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                        actualizaNumItem()
                        Me.limpiar()
                    Else
                        MsgBox("La Información no se guardó.", MsgBoxStyle.Information)
                    End If
                End If

            Else
                MsgBox("Faltan Datos del Producto.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtStockInicial_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStockInicial.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtStockMinimo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStockMinimo.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_Numeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Function buscarCodigo(ByVal codigo As String) As Byte
        Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM saldosAlmacenes where idProducto Like '" & codigo & "'", Connection)
        oDataSet = New DataSet()
        Try
            Connection.Open()
            daProductos.Fill(oDataSet, "productos")
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return Me.oDataSet.Tables(0).Rows.Count()
    End Function
    Private Sub limpiar()
        Me.txtCodigoProducto.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtMarca.Text = ""
        Me.txtModelo.Text = ""
        Me.txtStockInicial.Text = ""
        'Me.txtStockMinimo.Text = ""
        Me.btnProducto.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class