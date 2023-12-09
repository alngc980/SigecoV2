Imports System.Data.SqlClient
Module funcionesCO
    Dim cnd As New conexion
    Public ConnectionCO As New SqlConnection(cnd.ObtenerCadena)
    'Public ConnectionCO As New SqlConnection("Data Source=SERVER;Initial Catalog=COMERORI;User ID=sa;Password=123456")
    Public Function devuelveUltimoNumeroCO(ByVal SQL As String) As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varUltimoNumero As Integer
        objcommand = New SqlCommand(SQL, ConnectionCO)

        Try
            AbrirConexionCO()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                varUltimoNumero = oDataReader("numero")
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            CerrarConexionCO()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varUltimoNumero
    End Function
    Public Function devuelveCodigoCO(ByVal SQL As String) As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim vCodigo As Integer
        objcommand = New SqlCommand(SQL, ConnectionCO)

        Try
            ConnectionCO.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                vCodigo = oDataReader.Item(0)
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            ConnectionCO.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return vCodigo
    End Function
    Public Function verificaDniCO(ByVal SQL As String) As Boolean
        Dim band As Boolean = False
        Dim rows As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        objcommand = New SqlCommand(SQL, ConnectionCO)

        Try
            ConnectionCO.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                rows += 1
            End While
            If rows > 0 Then band = True

        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            ConnectionCO.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return band
    End Function
    Public Function grabarSqlStringCO(ByVal cadenaSqlString As String) As Boolean
        Dim band As Boolean = False
        If AbrirConexionCO() Then
            Dim command As SqlCommand = ConnectionCO.CreateCommand()

            Try
                command.CommandText = cadenaSqlString.ToString()
                command.ExecuteNonQuery()
                band = True
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                CerrarConexionCO()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionLetrasCO(ByVal ListaSentencias As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexionCO() Then

            Dim command As SqlCommand = ConnectionCO.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""

            Dim strSentencia As Object

            transaction = ConnectionCO.BeginTransaction()
            command.Connection = ConnectionCO
            command.Transaction = transaction

            Try
                For Each strSentencia In ListaSentencias
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next
                transaction.Commit()
                band = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    MsgBox(ex2.Message)
                End Try
            Finally
                CerrarConexionCO()
            End Try
        End If
        Return band
    End Function
    Public Function AbrirConexionCO() As Boolean
        Dim band As Boolean = False
        Try
            ConnectionCO.Open()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function
    Public Function CerrarConexionCO() As Boolean
        Dim band As Boolean = False
        Try
            ConnectionCO.Close()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function
End Module
