Public Class frmReporte
    Public par1 As String
    Public par2 As String
    Public par3 As String
    Public par4 As String
    Public par5 As String

    Public var1 As String
    Public var2 As String
    Public var3 As String
    Public var4 As String
    Public var5 As String
    Public cNombreReport As String

    Public ImageQr As Byte() = Nothing

    Private Sub frmReporte_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim Report As New ReportesBD()
        ''Report.MostrarReporte("rpt_comprobante.rpt", "@var", var1, "", "", "", "", "", "", "", "")
        'Report.MostrarReporte(cNombreReport + ".rpt", par1, var1, par2, var2, par3, var3, par4, var4, par5, var5, ImageQr)
        'Me.CrystalReportViewer1.Refresh()
        'Me.CrystalReportViewer1.ReportSource = Report.informe

    End Sub

    Public Sub CargarReporte(
                            ByVal cNombreReport As String,
                            ByVal par1 As String, ByVal var1 As String,
                            ByVal par2 As String, ByVal var2 As String,
                            ByVal par3 As String, ByVal var3 As String,
                            ByVal par4 As String, ByVal var4 As String,
                            ByVal par5 As String, ByVal var5 As String,
                            Optional ByVal ImageQr As Byte() = Nothing
                           )

        Dim Report As New ReportesBD()
        'Report.MostrarReporte("rpt_comprobante.rpt", "@var", var1, "", "", "", "", "", "", "", "")
        Report.MostrarReporte(cNombreReport + ".rpt", par1, var1, par2, var2, par3, var3, par4, var4, par5, var5, ImageQr)
        Me.CrystalReportViewer1.Refresh()
        Me.CrystalReportViewer1.ReportSource = Report.informe

        Me.ShowDialog()
    End Sub
End Class