Imports System.Data.SqlClient
Module funciones
    Public txtNombreEmpresa As String = "Comercial Oriente Hnos. SAC"
    Public txtDireccionEmpresa As String = "Próspero N° 663 - Iquitos"
    Public txtTelefonoEmpresa As String = "Teléfono: 065-241470"
    Public txtRUCEmpresa As String = "RUC: 20103855391"
    Public ruc_archivoPlano As String = "20103855391"
    Public codigoProducto As Integer
    Public codigoCliente As Integer
    Public codigoGrupo As Integer
    Public canNumSeries As Integer
    Public nSeriesBorrar As Integer
    Public matrizSeries(250, 5) As String

    Public numModulo As Byte
    Public totalRecibosMN As Decimal
    Public totalRecibosME As Decimal

    'Public arrayRecibos(10) As String
    Public matrizRecibos(10, 1) As String

    Public arrayDatos(15) As String
    Public arraySeries(5) As String
    Public flagString As String
    Public flag As Integer = 0
    Public y, z As Integer
    Public tipDocumento As String
    Public tipMovimiento As String
    Public numDocumento As Integer
    Public numeroLetra As String
    Public marcaProducto As String
    Public fecDocumento As Date
    'variables de geración de documento plano 26-01-23
    Public generaDocumentoPLano As Boolean = True
    Public generaDocumentoTicket As Boolean = True
    Public tipoDocumento As String '05-02-22
    Public numeDocumento As Integer '05-02-22
    Public nomArchivo As String
    Public devuelveNameComputer As String = "Servidor"
    Public devuelveNameComputer_sfs As String = My.Computer.Name.ToString.ToUpper
    Public iniciarSaldos As Boolean = False
    '----
    Public ImpresoraActual As New Printing.PrinterSettings
    Dim cnd As New conexion
    Public CadenaConexion As String = cnd.ObtenerCadena()

    Public Connection As New SqlConnection(CadenaConexion)
    'Public Connection As New SqlConnection("Data Source=SERVER;Initial Catalog=SIGECO;User ID=sa;Password=123456")
    Public Function sumaColumnas(ByVal Data As DataGridView, ByVal col As Byte) As Double
        Dim suma As Double
        For x As Integer = 0 To Data.Rows.Count - 1
            suma = suma + Val(Data.Rows(x).Cells(col).Value)
        Next
        Return suma
    End Function
    Public Function buscarCodigo(ByVal codigo As String) As Byte
        'Módulo viene de iniciar saldo el 02-02-23
        Dim odataset As DataSet

        Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM saldosAlmacenes where idProducto Like '" & codigo & "'", Connection)
        odataset = New DataSet()
        Try
            Connection.Open()
            daProductos.Fill(odataset, "productos")
            Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return odataset.Tables(0).Rows.Count()
    End Function
    Public Function buscarAmortizaciones(ByVal numLetra As String, ByVal numCorrelativo As Integer) As Decimal
        'Esta función trabaja con algunos reportes de ctas. ctes. (letras)
        Try
            Dim tabla As New DataTable
            Dim totalAmortizacion As Decimal
            Dim daLetras As SqlDataAdapter = New SqlDataAdapter("SELECT  *from recibosClientes where numLetra='" & numLetra & "'" & _
                                                                " and numCorrelativo=" & numCorrelativo & " and status='A'", Connection)
            Connection.Open()
            daLetras.Fill(tabla)
            Connection.Close()

            If tabla.Rows.Count() > 0 Then
                For i As Integer = 0 To tabla.Rows.Count() - 1
                    If tabla.Rows(i).Item(14) > 1 Then
                        totalAmortizacion = totalAmortizacion + tabla.Rows(i).Item(4)
                    Else
                        totalAmortizacion = totalAmortizacion + tabla.Rows(i).Item(3)
                    End If
                Next
            End If
            Return totalAmortizacion
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function EliminarSaltosLinea(ByVal texto As String, caracterReemplazar As String) As String
        EliminarSaltosLinea = Replace(Replace(texto, Chr(10), caracterReemplazar), Chr(13), caracterReemplazar)
    End Function
    Public Function Validar_Numeros(ByVal Numero As Short) As Short
        If InStr("1234567890.-", Chr(Numero)) = 0 Then 'Validar caracteres numericos
            Validar_Numeros = 0
        Else
            Validar_Numeros = Numero
        End If
        If Numero = 8 Then Validar_Numeros = Numero 'Validar Tecla Backspace
    End Function
    Public Function Validar_SoloNumeros(ByVal Numero As Short) As Short
        If InStr("1234567890", Chr(Numero)) = 0 Then 'Validar caracteres numericos
            Validar_SoloNumeros = 0
        Else
            Validar_SoloNumeros = Numero
        End If
        If Numero = 8 Then Validar_SoloNumeros = Numero 'Validar Tecla Backspace
    End Function
    Public Function Validar_Letras(ByVal Letra As Short) As Short
        If InStr("aAbBcCdDeEfFgGhHiIjJkKlLmMnNñÑoOpPqQrRsStTuUvVwWxXyYzZáéíóúÁÉÍÓÚ", Chr(Letra)) = 0 Then
            Validar_Letras = 0
        Else
            Validar_Letras = Letra
        End If
        If Letra = 8 Then Validar_Letras = Letra 'Validar Tecla Backspace
        If Letra = 32 Then Validar_Letras = Letra 'Validar Tecla Space
    End Function
    Public Function Validar_Letras_NC(ByVal Letra As Short) As Short
        If InStr("NCA1234567890", Chr(Letra)) = 0 Then
            Validar_Letras_NC = 0
        Else
            Validar_Letras_NC = Letra
        End If
        If Letra = 8 Then Validar_Letras_NC = Letra 'Validar Tecla Backspace
    End Function
    Public Function devuelveCodigo(ByVal SQL As String) As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim vCodigo As Integer
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                vCodigo = oDataReader.Item(0)
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return vCodigo
    End Function
    Public Function devuelveGrupo(ByVal SQL As String) As String
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim vGrupo As String = ""
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                vGrupo = oDataReader.Item(1)
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return vGrupo
    End Function
    Public Function devuelveFecha(ByVal SQL As String) As Date
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim vFecha As Date
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                vFecha = oDataReader.Item(0)
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return vFecha
    End Function
    Public Function devuelveStock(ByVal SQL As String) As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varStock As Integer
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                varStock = oDataReader("stock")
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varStock
    End Function
    Public Function devuelveTipoCambio(ByVal SQL As String, ByVal tipoMoneda As String) As Decimal
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varTipoCambio As Decimal
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                If tipoMoneda <> "Soles" Then
                    varTipoCambio = oDataReader("tipCamVenta")
                Else
                    varTipoCambio = oDataReader("tipCamCompra")
                End If
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varTipoCambio
    End Function
    Public Function devuelveTipoCambio1(ByVal SQL As String, ByVal tipoMoneda As String) As Decimal
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varTipoCambio As Decimal
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                If tipoMoneda <> "Soles" Then
                    varTipoCambio = oDataReader("tipCamCompra")
                Else
                    varTipoCambio = oDataReader("tipCamVenta")
                End If
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varTipoCambio
    End Function
    Public Function devuelveTasaVendedor(ByVal SQL As String) As DataTable
        Dim comando As SqlCommand
        Dim _tabla As New DataTable()
        comando = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            Dim _adaptador As New SqlDataAdapter
            _adaptador.SelectCommand = comando
            _adaptador.Fill(_tabla)
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
        End Try
        Return _tabla
    End Function
    Public Function devuelveTasa(ByVal SQL As String, ByVal percentPrecio As Decimal) As Decimal
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varInteres As Decimal
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                If percentPrecio >= 0.31 Then
                    varInteres = oDataReader("tasInt_1")
                Else
                    If percentPrecio >= 0.2 Then
                        varInteres = oDataReader("tasInt_2")
                    Else
                        If percentPrecio >= 0.1 Then
                            varInteres = oDataReader("tasInt_3")
                        Else
                            varInteres = oDataReader("tasInt_4")
                        End If
                    End If
                End If
            End While

        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varInteres
    End Function
    Public Function devuelveUltimoNumero(ByVal SQL As String) As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim varUltimoNumero As Integer
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                varUltimoNumero = oDataReader("numero")
            End While
        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return varUltimoNumero
    End Function
    Public Function verificarDocumento(ByVal sqlString As String) As Integer
        Dim objComando As SqlCommand
        Dim objTabla As New DataTable()
        Dim objAdaptador As New SqlDataAdapter
        Dim rows As Integer

        Try
            objComando = New SqlCommand(sqlString, Connection)
            Connection.Open()
            objAdaptador.SelectCommand = objComando
            objAdaptador.Fill(objTabla)
            rows = objTabla.Rows.Count
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
            objComando = Nothing
            objTabla = Nothing
        End Try
        Return rows
    End Function
    Public Function verificaNumeroDocumento(ByVal SQL As String) As Boolean
        Dim band As Boolean = False
        Dim rows As Integer
        Dim objcommand As SqlCommand
        Dim oDataReader As SqlDataReader
        objcommand = New SqlCommand(SQL, Connection)

        Try
            Connection.Open()
            oDataReader = objcommand.ExecuteReader()
            While oDataReader.Read()
                rows += 1
            End While
            If rows > 0 Then band = True

        Catch xErr As Exception
            MsgBox(xErr.Message)
        Finally
            Connection.Close()
            objcommand = Nothing
            oDataReader = Nothing
        End Try
        Return band
    End Function
    Public Function grabarSqlString(ByVal cadenaSqlString As String) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()

            Try
                command.CommandText = cadenaSqlString.ToString()
                command.ExecuteNonQuery()
                band = True
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function ejecutarTransaccion(ByVal ListaSentencias As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            'Dim command As SqlCommand = oConexion.CreateCommand()
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim strSentencia As Object

            Dim sentencia As String = ""
            'transaction = oConexion.BeginTransaction
            'command.Connection = oConexion
            transaction = Connection.BeginTransaction()
            command.Transaction = transaction
            command.Connection = Connection
            Try
                For Each strSentencia In ListaSentencias
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                transaction.Commit()
                band = True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    MessageBox.Show(ex2.Message)
                End Try
            Finally
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function grabacionFacturacion(ByVal SqlString As String, ByVal SqlString1 As String, ByVal SqlString2 As String, _
                                         ByVal SqlString3 As String, ByVal ListaSentencias As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""

            Dim strSentencia As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
            command.Transaction = transaction
            Try
                command.CommandText = SqlString.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString1.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString2.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString3.ToString()
                command.ExecuteNonQuery()

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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function grabacionNotaCredito(ByVal SqlString As String, ByVal SqlString1 As String, ByVal SqlString2 As String, ByVal SqlString3 As String, ByVal SqlString4 As String, ByVal SqlString5 As String, ByVal SqlString6 As String, ByVal SqlString7 As String, _
                                        ByVal ListaSentencias As ArrayList, ByVal ListaSentencias1 As ArrayList, ByVal ListaSentencias2 As ArrayList, ByVal ListaSentencias3 As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""

            Dim strSentencia As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
            command.Transaction = transaction
            Try
                command.CommandText = SqlString.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString1.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString2.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString3.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString4.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString5.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString6.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString7.ToString()
                command.ExecuteNonQuery()

                For Each strSentencia In ListaSentencias
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias1
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias2
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias3
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionAnulacionGuias(ByVal ListaSentencias As ArrayList, ByVal ListaSentencias1 As ArrayList, ByVal ListaSentencias2 As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""

            Dim strSentencia As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
            command.Transaction = transaction
            Try
                For Each strSentencia In ListaSentencias
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias1
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias2
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionAlmacen(ByVal SqlString As String, ByVal SqlString1 As String, ByVal ListaSentencias1 As ArrayList, ByVal ListaSentencias2 As ArrayList, ByVal ListaSentencias3 As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""
            Dim strSentencia1 As Object
            Dim strSentencia2 As Object
            Dim strSentencia3 As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
            command.Transaction = transaction
            Try
                command.CommandText = SqlString.ToString()
                command.ExecuteNonQuery()

                command.CommandText = SqlString1.ToString()
                command.ExecuteNonQuery()

                For Each strSentencia1 In ListaSentencias1
                    sentencia = strSentencia1.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia2 In ListaSentencias2
                    sentencia = strSentencia2.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia3 In ListaSentencias3
                    sentencia = strSentencia3.ToString()
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionLetras(ByVal ListaSentencias As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""

            Dim strSentencia As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionProducto(ByVal ListaSentencias As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim sentencia As String = ""
            Dim strSentencia As Object

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function transaccionAjuste(ByVal cadenaSqlString As String, ByVal ListaSentencias As ArrayList, ByVal ListaSentencias1 As ArrayList, ByVal ListaSentencias2 As ArrayList) As Boolean
        Dim band As Boolean = False
        If AbrirConexion() Then
            Dim command As SqlCommand = Connection.CreateCommand()
            Dim transaction As SqlTransaction
            Dim strSentencia As Object
            Dim sentencia As String = ""

            transaction = Connection.BeginTransaction()
            command.Connection = Connection
            command.Transaction = transaction
            Try

                command.CommandText = cadenaSqlString.ToString()
                command.ExecuteNonQuery()

                For Each strSentencia In ListaSentencias
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias1
                    sentencia = strSentencia.ToString()
                    command.CommandText = sentencia.ToString()
                    command.ExecuteNonQuery()
                Next

                For Each strSentencia In ListaSentencias2
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
                CerrarConexion()
            End Try
        End If
        Return band
    End Function
    Public Function AbrirConexion() As Boolean
        Dim band As Boolean = False
        Try
            Connection.Open()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function
    Public Function CerrarConexion() As Boolean
        Dim band As Boolean = False
        Try
            Connection.Close()
            band = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return band
    End Function
    Public Function numeroLetras(ByVal value As Double) As String
        Select Case value
            Case 0 : numeroLetras = "CERO"
            Case 1 : numeroLetras = "UNO"
            Case 2 : numeroLetras = "DOS"
            Case 3 : numeroLetras = "TRES"
            Case 4 : numeroLetras = "CUATRO"
            Case 5 : numeroLetras = "CINCO"
            Case 6 : numeroLetras = "SEIS"
            Case 7 : numeroLetras = "SIETE"
            Case 8 : numeroLetras = "OCHO"
            Case 9 : numeroLetras = "NUEVE"
            Case 10 : numeroLetras = "DIEZ"
            Case 11 : numeroLetras = "ONCE"
            Case 12 : numeroLetras = "DOCE"
            Case 13 : numeroLetras = "TRECE"
            Case 14 : numeroLetras = "CATORCE"
            Case 15 : numeroLetras = "QUINCE"
            Case Is < 20 : numeroLetras = "DIECI" & numeroLetras(value - 10)
            Case 20 : numeroLetras = "VEINTE"
            Case Is < 30 : numeroLetras = "VEINTI" & numeroLetras(value - 20)
            Case 30 : numeroLetras = "TREINTA"
            Case Is < 40 : numeroLetras = "TREINTI" & numeroLetras(value - 30)
            Case 40 : numeroLetras = "CUARENTA"
            Case Is < 50 : numeroLetras = "CUARENTI" & numeroLetras(value - 40)
            Case 50 : numeroLetras = "CINCUENTA"
            Case Is < 60 : numeroLetras = "CINCUENTI" & numeroLetras(value - 50)
            Case 60 : numeroLetras = "SESENTA"
            Case Is < 70 : numeroLetras = "SESENTI" & numeroLetras(value - 60)
            Case 70 : numeroLetras = "SETENTA"
            Case Is < 80 : numeroLetras = "SETENTI" & numeroLetras(value - 70)
            Case 80 : numeroLetras = "OCHENTA"
            Case Is < 90 : numeroLetras = "OCHENTI" & numeroLetras(value - 80)
            Case 90 : numeroLetras = "NOVENTA"
            Case Is < 100 : numeroLetras = "NOVENTI" & numeroLetras(value - 90)
            Case 100 : numeroLetras = "CIEN"
            Case Is < 200 : numeroLetras = "CIENTO " & numeroLetras(value - 100)
            Case 200, 300, 400, 600, 800 : numeroLetras = numeroLetras(Int(value \ 100)) & "CIENTOS"
            Case 500 : numeroLetras = "QUINIENTOS"
            Case 700 : numeroLetras = "SETECIENTOS"
            Case 900 : numeroLetras = "NOVECIENTOS"
            Case Is < 1000 : numeroLetras = numeroLetras(Int(value \ 100) * 100) & " " & numeroLetras(value Mod 100)
            Case 1000 : numeroLetras = "MIL"
            Case Is < 2000 : numeroLetras = "MIL " & numeroLetras(value Mod 1000)
            Case Is < 1000000 : numeroLetras = numeroLetras(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then numeroLetras = numeroLetras & " " & numeroLetras(value Mod 1000)
            Case 1000000 : numeroLetras = "UN MILLON"
            Case Is < 2000000 : numeroLetras = "UN MILLON " & numeroLetras(value Mod 1000000)
            Case Is < 1000000000000.0# : numeroLetras = numeroLetras(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then numeroLetras = numeroLetras & " " & numeroLetras(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : numeroLetras = "UN BILLON"
            Case Is < 2000000000000.0# : numeroLetras = "UN BILLON " & numeroLetras(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : numeroLetras = numeroLetras(Int(value / 1000000000000.0#)) & " BILLONES "
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then
                    numeroLetras = numeroLetras & " " & numeroLetras(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
                End If
        End Select
    End Function
    Public Function obtieneDecimales(ByVal value As Decimal) As String
        Return Microsoft.VisualBasic.Right(CStr(value), 2)
    End Function
    Public Sub actualizaNumItem()
        Dim oDataSet As DataSet
        Dim sqlString As String = ""
        Dim listSqlStrings As New ArrayList
        Dim grupo As Integer

        Try
            oDataSet = New DataSet()
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM  productos where idProducto>=6", Connection)
            daProductos.Fill(oDataSet, "productos")

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count - 1
                grupo = oDataSet.Tables(0).Rows(i).Item(1)
                Dim daNumerosSerie As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM  numerosSerie where idProducto='" & CInt(oDataSet.Tables(0).Rows(i).Item(0)) & "'", Connection)
                daNumerosSerie.Fill(oDataSet, "numerosSerie")

                For x As Integer = 0 To oDataSet.Tables(1).Rows.Count - 1
                    If grupo <> 6 Then
                        sqlString = "UPDATE numerosSerie Set numItem='" & CInt(x + 1) & "'  where idProducto='" & _
                        CInt(oDataSet.Tables(0).Rows(i).Item(0)) & "' and numSerie='" & oDataSet.Tables(1).Rows(x).Item(1) & "'"
                    Else
                        sqlString = "UPDATE numerosSerie Set numItem='" & CInt(x + 1) & "'  where idProducto='" & _
                        CInt(oDataSet.Tables(0).Rows(i).Item(0)) & "' and numMotor='" & oDataSet.Tables(1).Rows(x).Item(2) & "'"
                    End If
                    listSqlStrings.Add(sqlString)
                Next

                If transaccionProducto(listSqlStrings) Then
                    'MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    listSqlStrings.Clear()
                    oDataSet.Tables(1).Clear()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''---------------------- 05-08-15
    'Private Function buscarOtrosPagos(ByVal concepto As Byte, ByVal codigo As Integer) As Byte
    '    Dim tabla As New DataTable
    '    Dim rows As Byte
    '    Dim daRecibos As SqlDataAdapter = New SqlDataAdapter("SELECT *from recibosClientes where concepto=" & concepto & " and numDocGEnCI='' and numDocGenACI='' and idCliente=" & codigo & " and status<>'X'", Connection)

    '    Try
    '        Connection.Open()
    '        daRecibos.Fill(tabla)
    '        rows = tabla.Rows.Count
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        Connection.Close()
    '        tabla = Nothing
    '    End Try
    '    Return rows
    'End Function

    Public Function RetornaDataTable(Query As String) As DataTable
        Dim dt As New DataTable
        Try
            If AbrirConexion() Then
                Dim comand As SqlCommand
                Dim sda As New SqlDataAdapter
                comand = New SqlCommand(Query, Connection)
                sda = New SqlDataAdapter(comand)
                sda.Fill(dt)
            End If
        Catch ex As Exception

        Finally
            CerrarConexion()
        End Try

        Return dt
    End Function

    Public Sub AbrirAppQr()
        ' Ruta de la aplicación de consola a ejecutar
        Dim rutaAplicacion As String = Application.StartupPath + "\ConsoleApplication1.exe"

        ' Crear un proceso y configurarlo para la aplicación de consola
        Dim proceso As New Process()
        Dim infoProceso As New ProcessStartInfo(rutaAplicacion)

        ' Opcionalmente, puedes configurar más propiedades del proceso si es necesario
        ' Por ejemplo, redireccionar la entrada/salida estándar, establecer argumentos, etc.
        infoProceso.UseShellExecute = False
        infoProceso.RedirectStandardInput = True
        infoProceso.RedirectStandardOutput = True
        infoProceso.CreateNoWindow = True

        proceso.StartInfo = infoProceso

        ' Iniciar el proceso
        proceso.Start()

        ' Esperar a que el proceso termine
        proceso.WaitForExit()
    End Sub

End Module
