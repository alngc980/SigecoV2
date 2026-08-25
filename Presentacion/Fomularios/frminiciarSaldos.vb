Imports System.Data.SqlClient
Public Class frminiciarSaldos
    Private oDataSet As DataSet
    Private txtIdGrupo As Integer
    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        arrayDatos(0) = ""
        iniciarSaldos = True
        frmbuscaProducto.ShowDialog()
        iniciarSaldos = False

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
        Dim sqlString As String = ""
        Dim listaSqlString As New ArrayList
        Dim ofrmnumerosSerie As New frmingresarSeries()
        Dim fechaCierre As String

        fechaCierre = devuelveFecha("select * from cierreDiario")

        If txtCodigoProducto.Text = "" Then
            MsgBox("Por favor, tiene que indicar el producto a iniciar  ! ! !", MsgBoxStyle.Information)
            Exit Sub
        End If

        'If CInt(Trim(txtStockInicial.Text)) <= 0 Then
        '    MsgBox("Por favor, no puede iniciar producto en cero  ! ! !", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        Try
            sqlString = "UPDATE productos set stoInicial=1 where idProducto= " & CInt(txtCodigoProducto.Text) & ""
            listaSqlString.Add(sqlString)

            sqlString = "INSERT INTO saldosAlmacenes (idProducto,stock,fechaSaldo) VALUES (" & Me.txtCodigoProducto.Text & " ,'" & _
                        Me.txtStockInicial.Text + "' ,'" & CDate(fechaCierre) & "' )"
            listaSqlString.Add(sqlString)

            'If MsgBox("Crear números de serie de este producto?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '    flagString = "IS"
            '    codigoProducto = Me.txtCodigoProducto.Text
            '    codigoGrupo = txtIdGrupo
            '    canNumSeries = Me.txtStockInicial.Text
            '    ofrmnumerosSerie.ShowDialog()
            '    flagString = ""
            '    codigoProducto = 0
            '    codigoGrupo = 0
            '    canNumSeries = 0
            'End If

            'If flag = 1 Then
            If transaccionProducto(listaSqlString) Then
                MsgBox("Saldo producto iniciado correctamente  !  !  !", MsgBoxStyle.Information)
                actualizaNumItem()
                Me.limpiar()
            Else
                MsgBox("Error, saldo producto no fue iniciado  !  !  !", MsgBoxStyle.Critical)
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtStockInicial_Leave(sender As Object, e As System.EventArgs) Handles txtStockInicial.Leave
        If String.IsNullOrEmpty(CStr(txtStockInicial.Text)) Then
            txtStockInicial.Text = 0
        End If
    End Sub
    Private Sub txtStockInicial_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStockInicial.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtStockMinimo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStockMinimo.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))
        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub limpiar()
        Me.txtCodigoProducto.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtMarca.Text = ""
        Me.txtModelo.Text = ""
        Me.txtStockInicial.Text = "0"
        'Me.txtStockMinimo.Text = ""
        Me.btnProducto.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class