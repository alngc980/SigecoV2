Imports System.Data.SqlClient

Public Class frmseleccionarEmpresa
    Private Class EmpresaItem
        Public Nombre As String
        Public BaseDatos As String
        Public Ruc As String

        Public Sub New(ByVal nombreEmpresa As String, ByVal nombreBaseDatos As String, ByVal numeroRuc As String)
            Nombre = nombreEmpresa
            BaseDatos = nombreBaseDatos
            Ruc = numeroRuc
        End Sub

        Public Overrides Function ToString() As String
            Return Nombre
        End Function
    End Class

    Private Sub frmseleccionarEmpresa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cbxEmpresa.Items.Clear()
        Me.cbxEmpresa.Items.Add(New EmpresaItem("Comercial Oriente Hnos. SAC", "SIGECO", "20103855391"))
        Me.cbxEmpresa.Items.Add(New EmpresaItem("Créditos Oriente Hermanos S.A.", "CreditosOriente", "20329450661"))
        Me.cbxEmpresa.SelectedIndex = 0
    End Sub

    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        If Me.cbxEmpresa.SelectedItem Is Nothing Then
            MessageBox.Show("Seleccione una razon social.", "Empresa", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim empresa As EmpresaItem = DirectCast(Me.cbxEmpresa.SelectedItem, EmpresaItem)
        Try
            Dim datosConexion As New conexion()
            Using cn As New SqlConnection(datosConexion.ObtenerCadena(empresa.BaseDatos))
                cn.Open()
            End Using

            conexion.BaseDatosSeleccionada = empresa.BaseDatos
            conexion.RazonSocialSeleccionada = empresa.Nombre
            conexion.RucSeleccionado = empresa.Ruc
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar a la base de datos " & empresa.BaseDatos & "." & vbCrLf & ex.Message, "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
