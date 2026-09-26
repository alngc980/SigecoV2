Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Module configuracionReporteComprobante
    Public Function CrearReporteComprobante() As rptComprobante
        Dim reporte As New rptComprobante()
        ConfigurarIdentidadComprobante(reporte)
        Return reporte
    End Function

    Private Sub ConfigurarIdentidadComprobante(ByVal reporte As ReportDocument)
        Dim encabezado As Section = reporte.ReportDefinition.Sections(0)

        For Each objeto As ReportObject In encabezado.ReportObjects
            If TypeOf objeto Is TextObject Then
                ConfigurarTextoEncabezado(DirectCast(objeto, TextObject))
            End If
        Next

        ConfigurarLogo(reporte)
    End Sub

    Private Sub ConfigurarTextoEncabezado(ByVal texto As TextObject)
        Dim valorActual As String = texto.Text.Trim()
        Dim valorMayuscula As String = valorActual.ToUpperInvariant()

        If valorMayuscula.Contains("COMERCIAL ORIENTE") Then
            texto.Text = txtNombreEmpresa
        ElseIf valorMayuscula.StartsWith("RUC") OrElse valorMayuscula.StartsWith("R.U.C") Then
            texto.Text = txtRUCEmpresa
        ElseIf valorMayuscula.StartsWith("CEL") OrElse valorMayuscula.StartsWith("TEL") Then
            texto.Text = txtTelefonoEmpresa
        ElseIf valorMayuscula.Contains("LORETO") OrElse valorMayuscula.Contains("MAYNAS") Then
            texto.Text = txtDireccionEmpresa
        End If
    End Sub

    Private Sub ConfigurarLogo(ByVal reporte As ReportDocument)
        Dim rutaLogo As String = Path.Combine(Application.StartupPath, ruc_archivoPlano & ".jpg")

        If Not File.Exists(rutaLogo) Then
            Exit Sub
        End If

        Dim propiedadCliente As Reflection.PropertyInfo = reporte.GetType().GetProperty("ReportClientDocument")
        Dim documentoCliente As Object = propiedadCliente.GetValue(reporte, Nothing)
        Dim controladorDefinicion As Object = documentoCliente.ReportDefController
        Dim definicion As Object = controladorDefinicion.ReportDefinition

        For Each seccion As Object In definicion.ReportHeaderArea.Sections
            For Each objeto As Object In seccion.ReportObjects
                If objeto.Kind.ToString().ToUpperInvariant().Contains("PICTURE") Then
                    Dim logo As Object = objeto.Clone()
                    logo.GraphicLocation = rutaLogo
                    controladorDefinicion.ReportObjectController.Modify(objeto, logo)
                    Exit Sub
                End If
            Next
        Next
    End Sub
End Module
