Imports Microsoft
Imports System.IO
Public Class frmVaciarInterfazSunat
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Step = 1

        Dim i, ii, numeroArchivosCAB, numeroArchivosNOT As Int16
        Dim archivosNoEnviadosCAB As Boolean = False
        Dim archivosNoEnviadosNOT As Boolean = False
        Dim arrayArchivosNoEnviados(100) As String

        Dim sqlString As String
        Dim listaSqlString As New ArrayList

        'If AccesoLogica.devuelveNameComputer.ToString.ToUpper <> My.Computer.Name.ToString.ToUpper Then
        '    MsgBox("Lo siento, procedimiento de vaciado de Interfaz Sunat, debe ser realizado en el equipo SERVIDOR  !  !  !", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        Dim pathData, pathEnvio, pathFirma, pathOridat, pathParse, pathRepo, pathRpta, pathTemp As String
        If generaDocumentoTicket = True Then
            pathData = "\data\" : pathEnvio = "\envio\" : pathFirma = "\firma\" : pathOridat = "\oridat\"
            pathParse = "\parse\" : pathRepo = "\repo\" : pathRpta = "\rpta\" : pathTemp = "\temp\"
        Else
            pathData = "\SFS_v1.4_A4\sunat_archivos\sfs\data\" : pathEnvio = "\SFS_v1.4_A4\sunat_archivos\sfs\envio\"
            pathFirma = "\SFS_v1.4_A4\sunat_archivos\sfs\firma\" : pathOridat = "\SFS_v1.4_A4\sunat_archivos\sfs\oridat\"
            pathParse = "\SFS_v1.4_A4\sunat_archivos\sfs\parse\" : pathRepo = "\SFS_v1.4_A4\sunat_archivos\sfs\repo\"
            pathRpta = "\SFS_v1.4_A4\sunat_archivos\sfs\rpta\" : pathTemp = "\SFS_v1.4_A4\sunat_archivos\sfs\temp\"
        End If

        Try
            numeroArchivosCAB = My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathData, FileIO.SearchOption.SearchAllSubDirectories, "*.CAB").Count
            Dim arrayArchivosCAB(numeroArchivosCAB) As String

            numeroArchivosNOT = My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathData, FileIO.SearchOption.SearchAllSubDirectories, "*.NOT").Count
            Dim arrayArchivosNOT(numeroArchivosNOT) As String

            If numeroArchivosCAB <= 0 And numeroArchivosNOT <= 0 Then
                MsgBox("Po favor, no existen documentos en el facturador SUNAT  !  !  !", MsgBoxStyle.Information)
                Exit Sub
            End If

            For Each Archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathData, FileIO.SearchOption.SearchAllSubDirectories, "*.CAB")
                arrayArchivosCAB(i) = Archivo
                i += 1
            Next

            i = 0
            For Each Archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathData, FileIO.SearchOption.SearchAllSubDirectories, "*.NOT")
                arrayArchivosNOT(i) = Archivo
                i += 1
            Next

            ProgressBar1.Value = 1
            ProgressBar1.Maximum = numeroArchivosCAB
            For x As Integer = 0 To numeroArchivosCAB - 1
                Dim nameFile As String

                nameFile = VisualBasic.Right(arrayArchivosCAB(x), Len(arrayArchivosCAB(x)) - ("\\" & devuelveNameComputer_sfs & pathData).ToString.Length)
                nameFile = VisualBasic.Left(nameFile, Len(nameFile) - 4)

                If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathRpta & "R" & nameFile & ".ZIP") = False Then
                    archivosNoEnviadosCAB = True
                    arrayArchivosNoEnviados(ii) = nameFile
                    ii += 1
                End If
                sqlString = "update vtaCabecera set status='@' where tipDocumento='" & nameFile.Substring(15, 1) & "V' and numDocumento=" & VisualBasic.Right(nameFile, Len(nameFile) - 20) & ""
                listaSqlString.Add(sqlString)

                ProgressBar1.PerformStep()
                Application.DoEvents()
            Next

            For x As Integer = 0 To numeroArchivosNOT - 1
                Dim nameFile As String

                nameFile = VisualBasic.Right(arrayArchivosNOT(x), Len(arrayArchivosNOT(x)) - ("\\" & devuelveNameComputer_sfs & pathData).ToString.Length)
                nameFile = VisualBasic.Left(nameFile, Len(nameFile) - 4)

                If My.Computer.FileSystem.FileExists("\\" & devuelveNameComputer_sfs & pathRpta & "R" & nameFile & ".ZIP") = False Then
                    archivosNoEnviadosNOT = True
                    arrayArchivosNoEnviados(ii) = nameFile
                    ii += 1
                End If

                If nameFile.Substring(13, 1) = "7" Then
                    sqlString = "update notaCreditoCa set status='@' where numDocumento=" & VisualBasic.Right(nameFile, Len(nameFile) - 20) & ""
                    listaSqlString.Add(sqlString)
                Else
                    sqlString = "update notaDebitoCa set status='@' where numDocumento=" & VisualBasic.Right(nameFile, Len(nameFile) - 20) & ""
                    listaSqlString.Add(sqlString)
                End If
            Next

            If archivosNoEnviadosCAB = True Or archivosNoEnviadosNOT = True Then
                MsgBox("Lo sentimos, existe(n) documento(s) no enviados en el facturador SUNAT, envíelos para continuar el procedimiento   !  !  !", MsgBoxStyle.Critical)
                For iii As Int16 = 0 To arrayArchivosNoEnviados.Length - 1
                    If arrayArchivosNoEnviados(iii) <> "" Then
                        MsgBox(arrayArchivosNoEnviados(iii))
                    End If
                Next
                Exit Sub
            End If

            If MsgBox("Está seguro de vaciar los documentos del facturador SUNAT?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If My.Computer.FileSystem.DirectoryExists("D:\Reportes_SUNAT") = False Then My.Computer.FileSystem.CreateDirectory("D:\Reportes_SUNAT")

                If generaDocumentoTicket = True Then
                    My.Computer.FileSystem.CopyDirectory("D:\SFS_v1.4\sunat_archivos\sfs\REPO", "D:\Reportes_SUNAT\Reportes " & Today.Day & "-" & Today.Month & "-" & Today.Year, True)
                Else
                    My.Computer.FileSystem.CopyDirectory("D:\SFS_v1.4_A4\sunat_archivos\sfs\REPO", "D:\Reportes_SUNAT\Reportes " & Today.Day & "-" & Today.Month & "-" & Today.Year, True)
                End If

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathData, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathEnvio, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathFirma, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathOridat, FileIO.SearchOption.SearchAllSubDirectories, "*.XML")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathParse, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathRepo, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathRpta, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                For Each archivo As String In My.Computer.FileSystem.GetFiles("\\" & devuelveNameComputer_sfs & pathTemp, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                    File.Delete(archivo)
                Next

                If transaccionProducto(listaSqlString) Then
                    'MsgBox("procedimiento ejecutado correctamente  !  !  !", MsgBoxStyle.Information)
                Else
                    MsgBox("Error, procedimiento no se realizó correctamente  !  !  !", MsgBoxStyle.Critical)
                End If
                btnSalir_Click(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class