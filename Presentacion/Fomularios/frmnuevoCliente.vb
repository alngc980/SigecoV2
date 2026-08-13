Imports System.data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Public Class frmNuevoCliente
    Private Const FactilizaToken As String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMTAifQ.A0KWiSYCdoEnmfaBEE3vi9K-Df7ng0VG7BDO9ZT3xVw"
    Private Const FactilizaBaseUrl As String = "https://api.factiliza.com/v1"
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
    Private Sub btnConsultarDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultarDocumento.Click
        ConsultarDocumentoFactiliza()
    End Sub

    Private Sub txtDocumento_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRUC.KeyDown, txtDNI.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ConsultarDocumentoFactiliza()
        End If
    End Sub

    Private Sub ConsultarDocumentoFactiliza()
        Dim documento As String
        Dim tipoDocumento As String

        If SoloNumeros(Me.txtRUC.Text).Length > 0 Then
            documento = SoloNumeros(Me.txtRUC.Text)
            tipoDocumento = "ruc"
        Else
            documento = SoloNumeros(Me.txtDNI.Text)
            tipoDocumento = "dni"
        End If

        If tipoDocumento = "ruc" And documento.Length <> 11 Then
            MsgBox("Ingrese un RUC válido de 11 dígitos.", MsgBoxStyle.Exclamation)
            Me.txtRUC.Focus()
            Exit Sub
        End If

        If tipoDocumento = "dni" And documento.Length <> 8 Then
            MsgBox("Ingrese un DNI válido de 8 dígitos.", MsgBoxStyle.Exclamation)
            Me.txtDNI.Focus()
            Exit Sub
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnConsultarDocumento.Enabled = False

            Dim respuesta As String = ConsultarApiFactiliza(tipoDocumento, documento)
            If TieneErrorApi(respuesta) Then
                Dim mensajeApi As String = ObtenerValorJson(respuesta, "message", "mensaje", "error")
                If mensajeApi = "" Then mensajeApi = "No se encontró información para el documento ingresado."
                MsgBox(mensajeApi, MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            If tipoDocumento = "ruc" Then
                AplicarDatosRuc(documento, respuesta)
            Else
                AplicarDatosDni(documento, respuesta)
            End If
        Catch ex As WebException
            MsgBox("No se pudo consultar Factiliza. Verifique la conexión a internet o el token de la API." & vbCrLf & ObtenerDetalleWebException(ex), MsgBoxStyle.Critical)
        Catch ex As Exception
            MsgBox("No se pudo consultar Factiliza." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        Finally
            Me.btnConsultarDocumento.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ConsultarApiFactiliza(ByVal tipoDocumento As String, ByVal documento As String) As String
        Try
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        Catch
        End Try

        Dim url As String = FactilizaBaseUrl & "/" & tipoDocumento & "/info/" & documento
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "GET"
        request.Accept = "application/json"
        request.Timeout = 30000
        request.Headers.Add("Authorization", "Bearer " & FactilizaToken)

        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using stream As Stream = response.GetResponseStream()
                Using reader As New StreamReader(stream, Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Using
    End Function

    Private Sub AplicarDatosRuc(ByVal ruc As String, ByVal respuesta As String)
        Dim razonSocial As String = ObtenerValorJson(respuesta, "nombre_o_razon_social", "razon_social", "razonSocial", "nombre", "nombre_completo")
        Dim direccion As String = ObtenerValorJson(respuesta, "direccion", "direccion_completa", "domicilio_fiscal")

        Me.txtRUC.Text = ruc
        If razonSocial <> "" Then Me.txtNombres.Text = razonSocial.ToUpper()
        If direccion <> "" Then Me.txtDireccion.Text = direccion.ToUpper()

        If razonSocial = "" And direccion = "" Then
            MsgBox("La consulta fue realizada, pero no se encontraron datos para completar.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub AplicarDatosDni(ByVal dni As String, ByVal respuesta As String)
        Dim nombreCompleto As String = ObtenerValorJson(respuesta, "nombre_completo", "nombreCompleto", "nombre")

        If nombreCompleto = "" Then
            Dim nombres As String = ObtenerValorJson(respuesta, "nombres")
            Dim apellidoPaterno As String = ObtenerValorJson(respuesta, "apellido_paterno", "apellidoPaterno")
            Dim apellidoMaterno As String = ObtenerValorJson(respuesta, "apellido_materno", "apellidoMaterno")
            nombreCompleto = Trim(apellidoPaterno & " " & apellidoMaterno & " " & nombres)
        End If

        Me.txtDNI.Text = dni
        If nombreCompleto <> "" Then
            Me.txtNombres.Text = nombreCompleto.ToUpper()
        Else
            MsgBox("La consulta fue realizada, pero no se encontraron nombres para completar.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function TieneErrorApi(ByVal respuesta As String) As Boolean
        Dim success As String = ObtenerValorJson(respuesta, "success")
        Dim status As String = ObtenerValorJson(respuesta, "status")

        If success.ToLower() = "false" Then Return True
        If status <> "" AndAlso status <> "200" AndAlso status.ToLower() <> "true" AndAlso status.ToLower() <> "success" Then Return True
        Return False
    End Function

    Private Function ObtenerValorJson(ByVal json As String, ByVal ParamArray claves() As String) As String
        If json Is Nothing Then Return ""

        For Each clave As String In claves
            Dim patron As String = """" & Regex.Escape(clave) & """\s*:\s*(?:""(?<valor>(?:\\""|[^""])*)""|(?<valor>[^,}\]]+))"
            Dim match As Match = Regex.Match(json, patron, RegexOptions.IgnoreCase)
            If match.Success Then
                Dim valor As String = match.Groups("valor").Value.Trim()
                If valor.ToLower() <> "null" Then Return LimpiarValorJson(valor)
            End If
        Next

        Return ""
    End Function

    Private Function LimpiarValorJson(ByVal valor As String) As String
        valor = valor.Replace("\/", "/")
        valor = Regex.Unescape(valor)
        Return valor.Trim()
    End Function

    Private Function SoloNumeros(ByVal texto As String) As String
        If texto Is Nothing Then Return ""
        Return Regex.Replace(texto, "[^0-9]", "")
    End Function

    Private Function ObtenerDetalleWebException(ByVal ex As WebException) As String
        If ex.Response Is Nothing Then Return ex.Message

        Try
            Using stream As Stream = ex.Response.GetResponseStream()
                Using reader As New StreamReader(stream, Encoding.UTF8)
                    Dim detalle As String = reader.ReadToEnd()
                    Dim mensaje As String = ObtenerValorJson(detalle, "message", "mensaje", "error")
                    If mensaje <> "" Then Return mensaje
                    Return detalle
                End Using
            End Using
        Catch
            Return ex.Message
        End Try
    End Function
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