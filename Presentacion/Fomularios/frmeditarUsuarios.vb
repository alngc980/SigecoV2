Imports System.Data.SqlClient
Public Class frmeditarUsuarios
    Private oDataSet As DataSet

    Private Sub frmeditarUsuarios_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If Not UsuarioActualEsAdministrador() Then
                flag = 0
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Me.Close()
                    Exit Sub
                End If
            End If

            CargarUsuarios()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CargarUsuarios()
        oDataSet = New DataSet()
        Using cn As New SqlConnection(CadenaConexion)
            Using daUsuarios As New SqlDataAdapter("SELECT idUsuario, nombreUsuario, usuario, clave, status, fecha FROM usuariosSistema", cn)
                daUsuarios.Fill(oDataSet, "usuarios")
            End Using
        End Using

        Me.dgvUsuarios.DataSource = oDataSet
        Me.dgvUsuarios.DataMember = "usuarios"

        With Me.dgvUsuarios
            .Columns("idUsuario").ReadOnly = True
            .Columns("idUsuario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("fecha").ReadOnly = True
            .Columns("nombreUsuario").HeaderText = "Nombre"
            .Columns("usuario").HeaderText = "Tipo"
            .Columns("clave").HeaderText = "Clave"
            .Columns("status").HeaderText = "Estado"
            .Columns("fecha").HeaderText = "Fecha"
        End With
    End Sub

    Private Sub btnGrabar_Click(sender As System.Object, e As System.EventArgs) Handles btnGrabar.Click
        Try
            If Not UsuarioActualEsAdministrador() Then
                flag = 0
                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then Exit Sub
            End If

            Using cn As New SqlConnection(CadenaConexion)
                cn.Open()
                Using tx As SqlTransaction = cn.BeginTransaction()
                    Try
                        For i As Integer = 0 To dgvUsuarios.Rows.Count - 1
                            If dgvUsuarios.Rows(i).IsNewRow Then Continue For

                            Dim idUsuario As Integer = CInt(dgvUsuarios.Rows(i).Cells("idUsuario").Value)
                            Dim nombreUsuario As String = dgvUsuarios.Rows(i).Cells("nombreUsuario").Value.ToString().Trim()
                            Dim tipoUsuario As String = NormalizarTipoUsuario(dgvUsuarios.Rows(i).Cells("usuario").Value.ToString())
                            Dim clave As String = dgvUsuarios.Rows(i).Cells("clave").Value.ToString()
                            Dim status As Integer = CInt(dgvUsuarios.Rows(i).Cells("status").Value)

                            If nombreUsuario = "" OrElse clave = "" Then
                                Throw New ApplicationException("Nombre y clave son obligatorios.")
                            End If

                            If tipoUsuario <> "administrador" AndAlso tipoUsuario <> "vendedor" Then
                                Throw New ApplicationException("Solo se permiten usuarios de tipo administrador o vendedor.")
                            End If

                            Using cmd As New SqlCommand("UPDATE usuariosSistema SET nombreUsuario=@nombreUsuario, usuario=@usuario, clave=@clave, status=@status WHERE idUsuario=@idUsuario", cn, tx)
                                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario)
                                cmd.Parameters.AddWithValue("@usuario", tipoUsuario)
                                cmd.Parameters.AddWithValue("@clave", clave)
                                cmd.Parameters.AddWithValue("@status", status)
                                cmd.Parameters.AddWithValue("@idUsuario", idUsuario)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        tx.Commit()
                    Catch
                        tx.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            MsgBox("Información usuarios modificada correctamente !  !  !", MsgBoxStyle.Information)
            CargarUsuarios()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As System.Object, e As System.EventArgs) Handles btnEliminar.Click
        Try
            If dgvUsuarios.CurrentRow Is Nothing Then Exit Sub

            Dim idUsuario As Integer = CInt(dgvUsuarios.CurrentRow.Cells("idUsuario").Value)
            If idUsuario = UsuarioActualId Then
                MsgBox("No puede eliminar el usuario con la sesión activa.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If MsgBox("¿Está seguro de eliminar este usuario?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

            Using cn As New SqlConnection(CadenaConexion)
                cn.Open()
                Using cmd As New SqlCommand("DELETE FROM usuariosSistema WHERE idUsuario=@idUsuario", cn)
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Usuario eliminado correctamente.", MsgBoxStyle.Information)
            CargarUsuarios()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvUsuarios_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvUsuarios.EditingControlShowing
        If TypeOf e.Control Is TextBox Then
            Dim columna As String = dgvUsuarios.Columns(dgvUsuarios.CurrentCell.ColumnIndex).Name
            If columna = "nombreUsuario" Then
                DirectCast(e.Control, TextBox).MaxLength = 25
            ElseIf columna = "usuario" OrElse columna = "clave" Then
                DirectCast(e.Control, TextBox).MaxLength = 12
            End If
        End If
    End Sub

    Private Sub dgvVendedores_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvUsuarios.EditingControlShowing
        If TypeOf e.Control Is TextBox Then
            Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
            RemoveHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
            AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
        End If
    End Sub

    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As String = dgvUsuarios.Columns(dgvUsuarios.CurrentCell.ColumnIndex).Name
        Dim caracter As Char = e.KeyChar

        If columna = "nombreUsuario" Then
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub

    Private Sub dgvUsuarios_MouseEnter(sender As Object, e As System.EventArgs) Handles dgvUsuarios.MouseEnter
        lblMensaje.Text = "Solo existen dos tipos: administrador y vendedor."
    End Sub

    Private Sub dgvUsuarios_MouseLeave(sender As Object, e As System.EventArgs) Handles dgvUsuarios.MouseLeave
        lblMensaje.Text = ""
    End Sub

    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class