Imports System.IO
Imports Microsoft
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class frmconsultaLetrasGen
    Private texto As StreamReader
    Private oDataSet As DataSet
    Dim ctaFilas As Integer
    Dim NroPaginasImpresas As Integer = 0
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        If ctaFilas > 0 Then
            MsgBox("Ya existe información procesada, presione botón 'Imprimir'.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            Dim Max As Long = 1000
            Dim z As Long

            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 1
            ProgressBar1.Maximum = Max
            ProgressBar1.Value = 1
            ProgressBar1.Step = 1

            oDataSet = New DataSet()
            Connection.Open()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes", Connection)
            daCliente.Fill(oDataSet, "cliente")
            Connection.Close()
            ctaFilas = Me.oDataSet.Tables(0).Rows.Count

            For z = 1 To Max
                ProgressBar1.PerformStep()
                'Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        If ctaFilas <= 0 Then
            MsgBox("No hay información procesada para imprimir.", MsgBoxStyle.Information)
            Exit Sub
        End If

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        'ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        Dim arrayMoneda() As String = {"S/.", "$", "€"}
        Dim totalSoles, totalDolares, totalEuros As Decimal

        Dim en, t As Keys
        Dim enter, tab As Char
        Dim swEscritor As StreamWriter

        en = Keys.Enter
        t = Keys.Tab
        enter = Convert.ToChar(en)
        tab = Convert.ToChar(t)

        Try
            If My.Computer.FileSystem.DirectoryExists("C:\Cuotas") Then My.Computer.FileSystem.DeleteDirectory("C:\Cuotas", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory("C:\Cuotas")
            swEscritor = New StreamWriter("C:\Cuotas\cuotas.txt", True)
            swEscritor.WriteLine("                              C o m e r c i a l  O r i e n t e   H n o s.  S A C")
            swEscritor.WriteLine("                            R e p o r t e  d e  C u e n t a s  p o r  C o b r a r")
            swEscritor.WriteLine(" ")
            swEscritor.WriteLine("                                                                                    fecha reporte: " & DateTime.Today)
            swEscritor.WriteLine("Cuotas vencidas y x vencer")
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------")
            swEscritor.WriteLine("Núm.Letra      Corr. Mon. Importe  MN    Importe  ME Amortizacion  Saldo     Fecha Emis.    Fecha Venc. St.  ")
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------")

            ProgressBar1.Maximum = Me.oDataSet.Tables(0).Rows.Count() - 1
            For i As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count - 1
                Connection.Open()
                Dim daLetrasClientes As New SqlDataAdapter("select * from letrasClientes where idCliente=" & Me.oDataSet.Tables(0).Rows(i).Item(0) & " and ((numRecibo=' ' and status=' ') or  (numRecibo<>' ' and status='A'))", Connection)
                daLetrasClientes.Fill(oDataSet, "letrasClientes")
                Connection.Close()

                If Me.oDataSet.Tables(1).Rows.Count > 0 Then
                    swEscritor.WriteLine(Trim(Me.oDataSet.Tables(0).Rows(i).Item(1)))

                    For x As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
                        Dim amortizacion As Decimal
                        Dim saldo As Decimal

                        amortizacion = buscarAmortizaciones(Me.oDataSet.Tables(1).Rows(x).Item(0), Me.oDataSet.Tables(1).Rows(x).Item(3))
                        swEscritor.Write(Me.oDataSet.Tables(1).Rows(x).Item(0).ToString().PadRight(16))
                        swEscritor.Write(Me.oDataSet.Tables(1).Rows(x).Item(3).ToString().PadLeft(4))
                        swEscritor.Write(arrayMoneda(Me.oDataSet.Tables(1).Rows(x).Item(10) - 1).ToString().PadLeft(5))
                        swEscritor.Write(Me.oDataSet.Tables(1).Rows(x).Item(4).ToString().PadLeft(12))
                        swEscritor.Write(Me.oDataSet.Tables(1).Rows(x).Item(5).ToString().PadLeft(12))
                        swEscritor.Write(amortizacion.ToString().PadLeft(12))
                        If Me.oDataSet.Tables(1).Rows(x).Item(10) > 1 Then
                            saldo = Me.oDataSet.Tables(1).Rows(x).Item(5) - amortizacion
                            If Me.oDataSet.Tables(1).Rows(x).Item(10) = 2 Then
                                totalDolares += Me.oDataSet.Tables(1).Rows(x).Item(5)
                            Else
                                totalEuros += Me.oDataSet.Tables(1).Rows(x).Item(5)
                            End If
                        Else
                            saldo = Me.oDataSet.Tables(1).Rows(x).Item(4) - amortizacion
                            totalSoles += Me.oDataSet.Tables(1).Rows(x).Item(4)
                        End If
                        swEscritor.Write(saldo.ToString().PadLeft(12))
                        swEscritor.Write(VisualBasic.Left(Me.oDataSet.Tables(1).Rows(x).Item(6).ToString, 10).PadLeft(15))
                        swEscritor.Write(VisualBasic.Left(Me.oDataSet.Tables(1).Rows(x).Item(7).ToString, 10).PadLeft(15))
                        swEscritor.WriteLine(Me.oDataSet.Tables(1).Rows(x).Item(12).ToString.PadLeft(3))
                    Next x
                    swEscritor.WriteLine(" ")
                    Me.oDataSet.Tables(1).Clear()
                End If
                ProgressBar1.PerformStep()
                'Me.lblAvance.Text = Format((i + 1) / (Me.oDataSet.Tables(0).Rows.Count() - 1), "0%")
                Application.DoEvents()
            Next i

            swEscritor.WriteLine("--------------------------------------------------------------------------------------------------------------")
            swEscritor.WriteLine("Total Soles  : " & Format(totalSoles, "###,##0.00").ToString.PadLeft(12))
            swEscritor.WriteLine("Total Dolares: " & Format(totalDolares, "###,##0.00").ToString.PadLeft(12))
            swEscritor.WriteLine("Total Euros  : " & Format(totalEuros, "###,##0.00").ToString.PadLeft(12))
            swEscritor.Close()

            If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                configurarImpresion()
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()
            End If

            PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog = DialogResult.OK Then
                configurarImpresion()
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        texto = New StreamReader("C:\Cuotas\cuotas.txt", System.Text.ASCIIEncoding.Default)
        NroPaginasImpresas = 0
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim lineaTexto As String = Nothing
            Dim posicion As Integer = 50
            Dim NroLineasImpresas As Integer = 0

            ' Establecemos la fuente para impresión
            Dim Fuente As New Font("Courier New", 10)

            ' Calculamos el numero de líneas por pagina
            'Dim NroLineasPagina As Integer = e.PageBounds.Height / Fuente.GetHeight(e.Graphics)
            Dim NroLineasPagina As Integer = e.PageBounds.Height / Fuente.GetHeight
            NroLineasPagina = 62

            ' Comenzamos a contar las paginas impresas
            NroPaginasImpresas += 1

            While NroLineasImpresas < NroLineasPagina
                lineaTexto = texto.ReadLine()
                If Not lineaTexto Is Nothing Then
                    e.Graphics.DrawString(lineaTexto, Fuente, Brushes.Black, 10, posicion)
                    ' Aumentamos la posición a nivel de fila
                    posicion += 15
                    ' Contamos una nueva línea impresa
                    NroLineasImpresas += 1
                Else
                    Exit While
                End If
            End While

            e.Graphics.DrawString("N° Pag. " & NroPaginasImpresas, Fuente, Brushes.Black, 450, 1030)
            If Not lineaTexto Is Nothing Then
                e.HasMorePages = True
                If NroPaginasImpresas > 1 Then
                    e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 5)
                    e.Graphics.DrawString("Núm.Letra      Corr. Mon. Importe  MN    Importe  ME Amortizacion  Saldo     Fecha Emis.    Fecha Venc. St.  ", Fuente, Brushes.Black, 10, 20)
                    e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 35)
                End If
            Else
                e.HasMorePages = False
                texto.Close()
            End If
            If e.HasMorePages <> False Then
                e.Graphics.DrawString("--------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 980)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "MyPaper"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 992
        Dim Alto As Short = 1094
        Dim left As Short = 78
        Dim top As Short = 78
        Dim bottom As Short = 78
        Dim right As Short = 78

        TamañoPersonal = New Printing.PaperSize(nombrePapel, Ancho, Alto)
        margenes = New Printing.Margins(left, right, top, bottom)

        ' Asignamos la impresora seleccionada
        'prdDocumento.PrinterSettings = ImpresoraActual
        ' Asignamos el tamaño personalizado de papel
        Me.PrintDocument1.DefaultPageSettings.PaperSize = TamañoPersonal
        ' Asignamos los márgenes al documento
        Me.PrintDocument1.DefaultPageSettings.Margins = margenes
    End Sub
        Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            My.Computer.FileSystem.DeleteDirectory("C:\Cuotas", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            'MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK)
        Finally
            Me.Close()
        End Try
    End Sub
End Class