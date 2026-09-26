Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class conexion
    Public Shared BaseDatosSeleccionada As String = "SIGECO"
    Public Shared RazonSocialSeleccionada As String = "Comercial Oriente Hnos. SAC"
    Public Shared RucSeleccionado As String = "20103855391"

    Public ServerName As String = "SERVER"  'SERVIDOR
    Public UserID As String = "sa"
    Public Password As String = "123456"
    Public IntSec As Boolean = False

    Public ReadOnly Property DataBaseName() As String
        Get
            Return BaseDatosSeleccionada
        End Get
    End Property

    Public Function ObtenerCadena() As String
        Return ObtenerCadena(BaseDatosSeleccionada)
    End Function

    Public Function ObtenerCadena(ByVal baseDatos As String) As String
        Dim builder As New SqlConnectionStringBuilder()
        builder.DataSource = ServerName
        builder.InitialCatalog = baseDatos
        builder.UserID = UserID
        builder.Password = Password
        builder.PersistSecurityInfo = True
        Return builder.ConnectionString
    End Function

    Public Function conectar() As SqlConnection
        Return New SqlConnection(ObtenerCadena())
    End Function
End Class
