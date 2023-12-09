Imports Capa_Datos
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing
Imports ZXing
Imports System.IO

Public Class ReportesBD
    Private MyPath As String
    Public informe As New ReportDocument
    Public miConexionInfo As ConnectionInfo

    Dim conex As New conexion
    Public Sub MostrarReporte(NombreReporte As String,
                              nVar1 As String, vVar1 As String,
                              nVar2 As String, vVar2 As String,
                              nVar3 As String, vVar3 As String,
                              nVar4 As String, vVar4 As String,
                              nVar5 As String, vVar5 As String, Optional ByVal ImageQr As Byte() = Nothing)
        Try
            Dim NombreReport As String = NombreReporte

            miConexionInfo = New ConnectionInfo()
            miConexionInfo.DatabaseName = conex.DataBaseName    ' DataBaseName
            miConexionInfo.UserID = conex.UserID                ' UserID
            miConexionInfo.Password = conex.Password            ' Password
            miConexionInfo.ServerName = conex.ServerName        ' ServerName
            miConexionInfo.IntegratedSecurity = conex.IntSec    ' IntegratedSecurity

            If informe.IsLoaded Then
                informe.Close()
                informe.Dispose()
            End If

            informe = New ReportDocument()
            Dim reportPath As String = Application.StartupPath & "\rpt\" & NombreReporte
            informe.Load(reportPath)

            If nVar1 <> "" And nVar1 <> "" Then
                informe.SetParameterValue(nVar1, vVar1)
            End If

            If nVar2 <> "" And nVar2 <> "" Then
                informe.SetParameterValue(nVar2, vVar2)
            End If

            If nVar3 <> "" And nVar3 <> "" Then
                informe.SetParameterValue(nVar3, vVar3)
            End If

            If nVar4 <> "" And nVar4 <> "" Then
                informe.SetParameterValue(nVar4, vVar4)
            End If

            If nVar5 <> "" And nVar5 <> "" Then
                informe.SetParameterValue(nVar5, vVar5)
            End If

            'Dim ds As New dsRptVenta
            'ds.DataTable1.Rows.Add(ImageQr)

            'informe.SetDataSource(ds.Tables("DataTable1"))

            Loguearse(informe)

        Catch ex As Exception
            Cursor.Current = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Try
            Exit Sub
        End Try
    End Sub

    Public Sub Loguearse(ByVal myReportDocument As ReportDocument)
        Dim myTables As Tables = myReportDocument.Database.Tables
        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
            Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
            myTableLogonInfo.ConnectionInfo = miConexionInfo
            myTable.ApplyLogOnInfo(myTableLogonInfo)
        Next
    End Sub

    'Private Function ImageToByteArray(ByVal image As Image) As Byte()
    '    Dim ms As New System.IO.MemoryStream()
    '    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg) ' Puedes cambiar el formato de imagen aquí si es necesario
    '    Return ms.ToArray()
    'End Function

    'Public Sub GenerarQR(ruc As String, TipoDoc As String, txtserie As String, txtcorrelativo As String, txttotal As String, Fecha As Date, txtdniruc As String, PictureBox As PictureBox)
    '    Dim GENERADOR As BarcodeWriter = New BarcodeWriter 'INICIALIZA EL GENERADOR       
    '    GENERADOR.Format = BarcodeFormat.QR_CODE

    '    Dim textQR As String = ""
    '    Dim fecha2 = Fecha.Year.ToString & "-" & Fecha.Month.ToString & "-" & Fecha.Day.ToString

    '    Select Case TipoDoc
    '        Case "1"
    '            textQR = ruc & "|" & TipoDoc & "|" & txtserie & "|" & txtcorrelativo & "|0|" & txttotal & "|" & fecha2 & "|6|" & txtdniruc & "|"
    '    End Select

    '    'GENERA UN BITMAP Y LO PRESENTA EN EL PICTUREBOX
    '    Dim IMAGEN As Bitmap = New Bitmap(GENERADOR.Write(textQR), 1000, 1000)
    '    PictureBox.Image = IMAGEN
    'End Sub

End Class
