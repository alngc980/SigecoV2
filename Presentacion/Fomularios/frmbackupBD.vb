Imports System.Text
Imports System.Data.SqlClient
Public Class frmbackupBD
    Private Sub frmbackupBD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cbxServidor.SelectedIndex = 0
        Me.cbxBaseDatos.SelectedIndex = 0
    End Sub
    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        If crear_backup() = True Then
            'copiaBBDD()
            MessageBox.Show("Backup de la data creado satisfactoriamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Error, no se pudo crear backup de la data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function crear_backup() As Boolean
        Try
            'If My.Computer.FileSystem.FileExists("\\SERVIDOR\BackupData\SIGECO.bak") = True Then
            '    My.Computer.FileSystem.DeleteFile("\\SERVIDOR\BackupData\SIGECO.bak")
            'End If

            'If My.Computer.FileSystem.FileExists("\\TESORERIA\BackupData\SIGECO.bak") = True Then
            '    My.Computer.FileSystem.DeleteFile("\\TESORERIA\BackupData\SIGECO.bak")
            'End If

            'If My.Computer.FileSystem.FileExists("\\DIRECTOR\BackupData\SIGECO.bak") = True Then
            '    My.Computer.FileSystem.DeleteFile("\\DIRECTOR\BackupData\SIGECO.bak")
            'End If

            Dim sCmd As New StringBuilder
            Connection.Open()
            sCmd.Append("BACKUP DATABASE [SIGECO] TO  DISK = N'D:\BackupData\SIGECO.bak'")
            'sCmd.Append("BACKUP DATABASE [SIGECO] TO  DISK = N'\\SERVIDOR\BackupData\SIGECO.bak'")
            sCmd.Append("WITH NOFORMAT, NOINIT, ")
            sCmd.Append("NAME = N'SIGECO', SKIP, NOREWIND, NOUNLOAD,  STATS = 10")

            Dim cmd As New SqlCommand(sCmd.ToString, Connection)

            cmd.ExecuteNonQuery()
            crear_backup = True
        Catch ex As Exception
            crear_backup = False
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Function
    Private Sub copiaBBDD()
        Try
            If My.Computer.FileSystem.FileExists("\\SERVIDOR\BackupData\SIGECO.bak") = True Then
                My.Computer.FileSystem.CopyFile("\\SERVIDOR\BackupData\SIGECO.bak", "\\TESORERIA\BackupData\SIGECO.bak")
                My.Computer.FileSystem.CopyFile("\\SERVIDOR\BackupData\SIGECO.bak", "\\DIRECTOR\BackupData\SIGECO.bak")
            Else
                MsgBox("Error, no se hizo backup de la DATA en algún terminal   !  !  !", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class