Imports System.data.SqlClient
Public Class frmNuevoCliente
    Private oDataSet As DataSet
    Dim SqlString1 As String = "SELECT *FROM clientes"
    Private Sub frmNuevoCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCodigoCliente.Text = devuelveCodigo(SqlString1) + 1
        Me.txtZona.Text = 3
    End Sub
    Private Sub txtZona_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZona.DoubleClick
        arrayDatos(0) = ""
        frmbuscaZonas.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtZona.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String

        Try
            If (Me.txtNombres.Text = "" Or Me.txtDireccion.Text = "" Or (Me.txtRUC.Text = "" And Me.txtDNI.Text = "")) Then
                MsgBox("Faltan datos del cliente  !  !  !", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Me.txtRUC.Text <> "" And Len(Trim(Me.txtRUC.Text)) = 11 Then
                If Me.buscarDocumento(Trim(Me.txtRUC.Text), 1) >= 1 Then
                    MsgBox("Ya existe un cliente registrado con N° RUC: " & Trim(Me.txtRUC.Text), MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If Me.txtDNI.Text <> "" And Len(Trim(Me.txtDNI.Text)) = 8 Then
                If Me.buscarDocumento(Trim(Me.txtDNI.Text), 2) >= 1 Then
                    MsgBox("Ya existe un cliente registrado con N° DNI: " & Trim(Me.txtDNI.Text), MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            Me.txtNombres.Text = EliminarSaltosLinea(Me.txtNombres.Text, " ")
            Me.txtDireccion.Text = EliminarSaltosLinea(Me.txtDireccion.Text, " ")

            SqlString = "insert into clientes (nombres,direccion,ruc,dni,telCelular,telFijo,dirTrabajo,nomPareja,dirPareja,dniPareja," & _
            "celPareja,dirTraPareja,zona,fecAlta) VALUES ('" & Me.txtNombres.Text & "','" & Me.txtDireccion.Text & "','" & Me.txtRUC.Text & "','" & _
            Me.txtDNI.Text & "' ,'" & Me.txtCelular.Text & "' ,'" & Me.txtFijo.Text & "','" & Me.txtDirTrabajo.Text & "','" & Me.txtNomPareja.Text & "','" & _
            Me.txtDirPareja.Text & "','" & Me.txtDNIPareja.Text & "','" & Me.txtCelPareja.Text & "','" & Me.txtDirTraPareja.Text & "'," & _
            CInt(Me.txtZona.Text) & ",'" & Me.dtpFecha.Text & "' )"

            If grabarSqlString(SqlString) Then
                MsgBox("Información guardada correctamente.", MsgBoxStyle.Information)
                Me.txtCodigoCliente.Text = devuelveCodigo(SqlString1)
                arrayDatos(0) = Me.txtCodigoCliente.Text
                arrayDatos(1) = Me.txtNombres.Text
                arrayDatos(2) = Me.txtDireccion.Text
                arrayDatos(3) = Me.txtRUC.Text
                arrayDatos(4) = Me.txtDNI.Text
                If flagString = "1" Then
                    If MsgBox("Crear garantes para esta operación?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        flag = 1
                        codigoCliente = Me.txtCodigoCliente.Text
                        frmcrearGarantes.ShowDialog()
                        flag = 0
                    End If
                End If
                flagString = ""
                Me.limpiar()
            Else
                MsgBox("La Información no se guardó.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function buscarDocumento(ByVal Documento As String, ByRef tipo As Byte) As Byte
        Dim daClientes As SqlDataAdapter

        oDataSet = New DataSet()
        If tipo = 1 Then
            daClientes = New SqlDataAdapter("SELECT * FROM clientes where ruc Like '" & Documento & "'", Connection)
        Else
            daClientes = New SqlDataAdapter("SELECT * FROM clientes where dni Like '" & Documento & "'", Connection)
        End If

        Try
            daClientes.Fill(oDataSet, "clientes")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try

        Return Me.oDataSet.Tables(0).Rows.Count()
    End Function
    Private Sub txtRUC_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRUC.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtDNI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDNI.KeyPress
        Dim letra As Short = CShort(Asc(e.KeyChar))

        letra = CShort(Validar_SoloNumeros(letra))
        If letra = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub limpiar()
        Me.txtCodigoCliente.Text = ""
        Me.txtNombres.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtRUC.Text = ""
        Me.txtDNI.Text = ""
        Me.txtCelular.Text = ""
        Me.txtFijo.Text = ""
        Me.txtDirTrabajo.Text = ""
        Me.txtNomPareja.Text = ""
        Me.txtDirPareja.Text = ""
        Me.txtDNIPareja.Text = ""
        Me.txtCelPareja.Text = ""
        Me.txtDirTraPareja.Text = ""

        Me.txtCodigoCliente.Text = devuelveCodigo(SqlString1) + 1
        Me.txtNombres.Focus()
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class