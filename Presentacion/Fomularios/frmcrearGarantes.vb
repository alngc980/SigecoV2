Imports System.data.SqlClient
Public Class frmcrearGarantes
    Private oDataSet As DataSet
    Dim SqlString1 As String = "SELECT *FROM garantes"
    Private Sub frmcrearGarantes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoGarante.Text = devuelveCodigo(SqlString1) + 1
        If flag <> 0 Then Me.txtCodigoGarante.Text = codigoCliente
        Me.txtZona.Text = 3
    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        arrayDatos(0) = ""
        frmbuscaGarante.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtNombres.Text = arrayDatos(1)
            Me.txtDireccion.Text = arrayDatos(2)
            Me.txtDNI.Text = arrayDatos(3)
            Me.txtCelular.Text = arrayDatos(4)
            Me.txtFijo.Text = arrayDatos(5)
            Me.txtDirTrabajo.Text = arrayDatos(6)
            Me.txtZona.Text = arrayDatos(7)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub txtZona_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZona.DoubleClick
        arrayDatos(0) = ""
        frmbuscaZonas.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtZona.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub txtCodigoGarante_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigoGarante.DoubleClick
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigoGarante.Text = arrayDatos(0)
            Me.txtNombres.Focus()
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String
            If (Me.txtNombres.Text <> "" And Me.txtDNI.Text <> "") Then

                'If Me.buscarDocumento(Trim(Me.txtDNI.Text)) >= 1 Then
                '    MsgBox("Ya existe un cliente registrado con este documento: " & Trim(Me.txtDNI.Text), MsgBoxStyle.Information)
                '    Exit Sub
                'End If

                SqlString = "INSERT INTO garantes (idCliente,nombreGarante,direccion,dni,telCelular,telFijo,dirTrabajo,nomPareja,dirPareja," & _
                "dniPareja,celPareja,dirTraPareja,zona,fecAlta) VALUES (" & Me.txtCodigoGarante.Text & ",'" & Me.txtNombres.Text & "','" & _
                Me.txtDireccion.Text & "','" & Me.txtDNI.Text & "','" & Me.txtCelular.Text & "','" & Me.txtFijo.Text & "','" & _
                Me.txtDirTrabajo.Text & "','" & Me.txtNomPareja.Text & "','" & Me.txtDirPareja.Text & "','" & Me.txtDNIPareja.Text & "','" & _
                Me.txtCelPareja.Text & "','" & Me.txtDirTraPareja.Text & "', " & CInt(Me.txtZona.Text) & ",'" & Me.dtpFecha.Text & "' )"

                If grabarSqlString(SqlString) Then
                    If flag <> 1 Then MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                    Me.Close()
                Else
                    MsgBox("La Información no se guardó.", MsgBoxStyle.Exclamation)
                    Me.Close()
                End If
            Else
                MsgBox("Faltan Datos del Garante.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Private Function buscarDocumento(ByVal Documento As String) As Byte
    '    Dim daClientes As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM garantes where dni Like '" & Documento & "'", Connection)
    '    oDataSet = New DataSet()

    '    Try
    '        Connection.Open()
    '        daClientes.Fill(oDataSet, "garantes")
    '        Connection.Close()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return Me.oDataSet.Tables(0).Rows.Count()
    'End Function
    Private Sub GroupBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseEnter
        Me.lblMensaje.Text = "Haz doble click en el campo código para buscar el cliente al cual va a garantizar esta persona."
    End Sub
    Private Sub GroupBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox1.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class