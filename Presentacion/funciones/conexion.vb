Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class conexion
    Dim conexion As SqlConnection

    'CADENAS DE CONEXION DE LA APLICACION
    Public ServerName As String = "SERVIDOR" '"SERVER" SERVIDOR  '192.168.8.100, 1433
    Public DataBaseName As String = IIf(bitProduccion, "SIGECO", "SIGECO")
    Public UserID As String = "sa"
    Public Password As String = "123456"
    Public IntSec As Boolean = False

    Public Function ObtenerCadena() As String
        Dim Cadena As String = ""
        Cadena = "SERVER=" + ServerName + "; "
        Cadena = Cadena + "Initial Catalog=" + DataBaseName + "; "
        If UserID <> "" Then
            Cadena = Cadena + "User ID=" + UserID + "; "
        End If
        If Password <> "" Then
            Cadena = Cadena + "Password=" + Password + "; "
        End If
        'If IntSec Then
        '    Cadena = Cadena + "Integrated Security=true"
        'Else
        '    Cadena = Cadena + "Integrated Security=false"
        'End If
        If bitProduccion Then
            Cadena = "Data Source=Servidor;Initial Catalog=SIGECO;User ID=sa;Password=123"
        Else
            Cadena = "Data Source=Server;Initial Catalog=SIGECO;User ID=sa;Password=123456"
        End If

        Return Cadena
    End Function

    Public Function conectar() As SqlConnection
        conexion = New SqlConnection(ObtenerCadena())
        Return conexion
    End Function

End Class
