Imports System.Data.SqlClient
Imports System.IO

Public Class frmconfiguracionFacturador
    Private Sub frmconfiguracionFacturador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Try
            CargarConfiguracionFacturador()
            Me.txtRutaBase.Text = rutaBaseFacturador
            Me.txtUrlServicio.Text = urlServicioFacturador
            Me.txtRutaBase.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExaminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExaminar.Click
        Using dialogo As New FolderBrowserDialog()
            dialogo.Description = "Seleccione o cree la carpeta donde se generaran los archivos del facturador."
            dialogo.ShowNewFolderButton = True
            If Directory.Exists(Me.txtRutaBase.Text.Trim()) Then dialogo.SelectedPath = Me.txtRutaBase.Text.Trim()
            If dialogo.ShowDialog() = Windows.Forms.DialogResult.OK Then Me.txtRutaBase.Text = dialogo.SelectedPath
        End Using
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim ruta As String = Me.txtRutaBase.Text.Trim().TrimEnd("\"c)
        Dim url As String = Me.txtUrlServicio.Text.Trim()

        If ruta = "" Then
            MessageBox.Show("Ingrese la ruta base del facturador.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtRutaBase.Focus()
            Exit Sub
        End If
        If Not Uri.IsWellFormedUriString(url, UriKind.Absolute) Then
            MessageBox.Show("Ingrese una URL valida para el servicio SFS.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.txtUrlServicio.Focus()
            Exit Sub
        End If
        Try
            AsegurarCarpetasFacturador(ruta)

            Const sql As String = "IF EXISTS (SELECT 1 FROM configuracionFacturador WHERE idConfiguracion = 1) " & _
                "UPDATE configuracionFacturador SET rutaBase=@ruta, urlServicio=@url, fechaModificacion=GETDATE() WHERE idConfiguracion=1 " & _
                "ELSE INSERT INTO configuracionFacturador(idConfiguracion,rutaBase,urlServicio) VALUES(1,@ruta,@url)"
            Using cn As New SqlConnection(CadenaConexion)
                cn.Open()
                Using cmd As New SqlCommand(sql, cn)
                    cmd.Parameters.Add("@ruta", SqlDbType.VarChar, 500).Value = ruta
                    cmd.Parameters.Add("@url", SqlDbType.VarChar, 250).Value = url
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            CargarConfiguracionFacturador()
            MessageBox.Show("Configuracion guardada correctamente.", "Facturador", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class
