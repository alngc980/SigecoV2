Imports System.Data.SqlClient
Public Class frmeditarUsuarios
    Private oDataSet As DataSet
    Private Sub frmeditarUsuarios_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            oDataSet = New DataSet()
            Dim daVendedores As SqlDataAdapter = New SqlDataAdapter("SELECT  * from usuariosSistema", Connection)
            daVendedores.Fill(oDataSet, "usuarios")

            Me.dgvUsuarios.DataSource = oDataSet
            Me.dgvUsuarios.DataMember = "usuarios"
            With Me.dgvUsuarios
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).ReadOnly = True
                .Columns(4).ReadOnly = True
                .Columns(5).ReadOnly = True
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(sender As System.Object, e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String
        Dim ListSqlStrings As New ArrayList
        Dim miClave As String

        miClave = InputBox("Ingrese clave de acceso de creación de usuarios:", , , 700, 300)
        If miClave <> "13579acegi" Then
            MsgBox("I'm sorry, your password is incorrect.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            For i As Integer = 0 To dgvUsuarios.Rows.Count - 1
                SqlString = "update usuariosSistema set nombreUsuario='" & dgvUsuarios.Rows(i).Cells(1).Value & "',clave='" & dgvUsuarios.Rows(i).Cells(3).Value & "' where idUsuario=" & dgvUsuarios.Rows(i).Cells(0).Value & ""

                ListSqlStrings.Add(SqlString)
            Next

            If transaccionProducto(ListSqlStrings) Then
                MsgBox("Información usuarios modificada correctamente !  !  !", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("Las modificaciones no fueron procesadas !  !  !", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvUsuarios_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvUsuarios.EditingControlShowing
        Dim columna As Integer = dgvUsuarios.CurrentCell.ColumnIndex

        If columna = 1 Then
            DirectCast(e.Control, TextBox).MaxLength = 25
        Else
            DirectCast(e.Control, TextBox).MaxLength = 12
        End If
    End Sub
    Private Sub dgvVendedores_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvUsuarios.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)

        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvUsuarios.CurrentCell.ColumnIndex
        Dim caracter As Char = e.KeyChar

        If columna = 1 Then
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvUsuarios_MouseEnter(sender As Object, e As System.EventArgs) Handles dgvUsuarios.MouseEnter
        lblMensaje.Text = "Sólo puede modificar los campos 'nombreUsuario' y 'clave'."
    End Sub
    Private Sub dgvUsuarios_MouseLeave(sender As Object, e As System.EventArgs) Handles dgvUsuarios.MouseLeave
        lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class