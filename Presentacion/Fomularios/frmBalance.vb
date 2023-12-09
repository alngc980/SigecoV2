Imports System.IO
Imports Microsoft
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class frmBalance
    Private texto As StreamReader
    Private oDataSet As DataSet
    Dim NroPaginasImpresas As Integer = 0
    Private existe As Boolean
    Private Sub frmBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Try
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 1
            'ProgressBar1.Maximum = Max
            ProgressBar1.Value = 1
            ProgressBar1.Step = 1

            oDataSet = New DataSet()
            Connection.Open()
            Dim daResumen As SqlDataAdapter = New SqlDataAdapter("SELECT * from resumenAjuste where fecha='" & Me.dtpFechaBalance.Text & "' order by idProducto ", Connection)
            daResumen.Fill(oDataSet, "resumen")

            Dim daProducto As SqlDataAdapter = New SqlDataAdapter("SELECT  * from Productos where stoInicial>=0", Connection)
            daProducto.Fill(oDataSet, "productos")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count <= 0 Then
                MsgBox("No existe información de 'ajuste de stock' de esta fecha.", MsgBoxStyle.Information)
                Exit Sub
            End If

            existe = True
            Dim colDescripcion As DataColumn = New DataColumn()
            colDescripcion.AllowDBNull = True
            colDescripcion.Caption = "Descripcion Producto"
            colDescripcion.ColumnName = "descripcionProducto"
            Me.oDataSet.Tables(0).Columns.Add(colDescripcion)

            Dim colMarca As DataColumn = New DataColumn()
            colMarca.AllowDBNull = True
            colMarca.Caption = "Marca Producto"
            colMarca.ColumnName = "marca"
            Me.oDataSet.Tables(0).Columns.Add(colMarca)

            Dim colModelo As DataColumn = New DataColumn()
            colModelo.AllowDBNull = True
            colModelo.Caption = "Modelo Producto"
            colModelo.ColumnName = "Modelo"
            Me.oDataSet.Tables(0).Columns.Add(colModelo)

            Dim oDataRow As DataRow
            For i As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count() - 1
                For x As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count() - 1
                    If Me.oDataSet.Tables(1).Rows(x).Item(0) = Me.oDataSet.Tables(0).Rows(i).Item(0) Then
                        oDataRow = Me.oDataSet.Tables(0).Rows(i)
                        oDataRow(6) = Me.oDataSet.Tables(1).Rows(x).Item(2)
                        oDataRow(7) = Me.oDataSet.Tables(1).Rows(x).Item(4)
                        oDataRow(8) = Me.oDataSet.Tables(1).Rows(x).Item(5)
                    End If
                Next x
            Next i

            ProgressBar1.Maximum = Me.oDataSet.Tables(1).Rows.Count - 1
            For x As Integer = 0 To ProgressBar1.Maximum
                ProgressBar1.PerformStep()
                Application.DoEvents()
            Next x

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If existe = False Then
            MsgBox("No existe información procesada para imprimir.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim arrayMoneda() As String = {"S/.", "$", "€"}
        Dim en, t As Keys
        Dim enter, tab As Char
        Dim swEscritor As StreamWriter

        en = Keys.Enter
        t = Keys.Tab
        enter = Convert.ToChar(en)
        tab = Convert.ToChar(t)

        Try
            If My.Computer.FileSystem.DirectoryExists("C:\Balance") Then My.Computer.FileSystem.DeleteDirectory("C:\Balance", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory("C:\Balance")
            swEscritor = New StreamWriter("C:\Balance\balance.txt", True)
            swEscritor.WriteLine("                              C o m e r c i a l  O r i e n t e   H n o s.  S A C")
            swEscritor.WriteLine("                                B a l a n c e  d e  I n v e n t a r i o s")
            swEscritor.WriteLine(" ")
            swEscritor.WriteLine("Reporte Balance al " & Me.dtpFechaBalance.Text & "                                       fecha reporte: " & DateTime.Today)
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------")
            swEscritor.WriteLine("                                                         |           Stock  de  Mercaderia           |")
            swEscritor.WriteLine("                                                                            | Diferencia   |")
            swEscritor.WriteLine("|Cod|Descripcion         | Marca        | Modelo         | Sistema | Fisico | Mas  | Menos |  Total  |")
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------")

            ProgressBar1.Value = 1
            ProgressBar1.Maximum = Me.oDataSet.Tables(0).Rows.Count - 1
            For x As Integer = 0 To Me.oDataSet.Tables(0).Rows.Count - 1
                swEscritor.Write(Me.oDataSet.Tables(0).Rows(x).Item(0).ToString().PadLeft(4) & " ")
                swEscritor.Write(VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(6).ToString(), 20).PadRight(21))
                swEscritor.Write(VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(7).ToString(), 15).PadRight(16))
                swEscritor.Write(VisualBasic.Left(Me.oDataSet.Tables(0).Rows(x).Item(8).ToString(), 15).PadRight(16))
                swEscritor.Write(Me.oDataSet.Tables(0).Rows(x).Item(1).ToString().PadLeft(8))
                swEscritor.Write(Me.oDataSet.Tables(0).Rows(x).Item(2).ToString().PadLeft(8))
                swEscritor.Write(Me.oDataSet.Tables(0).Rows(x).Item(3).ToString().PadLeft(7))
                swEscritor.Write(Me.oDataSet.Tables(0).Rows(x).Item(4).ToString().PadLeft(7))
                swEscritor.WriteLine(Me.oDataSet.Tables(0).Rows(x).Item(2).ToString().PadLeft(9))

                ProgressBar1.PerformStep()
                Application.DoEvents()
            Next x
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
        texto = New StreamReader("C:\Balance\balance.txt", System.Text.ASCIIEncoding.Default)
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
                    e.Graphics.DrawString("|Núm|Cód|Descripción       | Marca   | Modelo  |Saldo| Fec Saldo |", Fuente, Brushes.Black, 10, 20)
                    e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 35)
                End If
            Else
                e.HasMorePages = False
                If NroPaginasImpresas > 1 Then
                    e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 5)
                    e.Graphics.DrawString("|Núm|Cód|Descripción       | Marca   | Modelo  |Saldo| Fec Saldo |", Fuente, Brushes.Black, 10, 20)
                    e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------------", Fuente, Brushes.Black, 10, 35)
                End If
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
    'Private Sub cbxAnno_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxAnno.SelectedIndexChanged
    '    Try
    '        My.Computer.FileSystem.DeleteDirectory("C:\Cuotas", FileIO.DeleteDirectoryOption.DeleteAllContents)
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK)
    '    Finally
    '        ctaFilas = 0
    '    End Try
    'End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            My.Computer.FileSystem.DeleteDirectory("C:\Balance", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            'MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK)
        Finally
            Me.Close()
        End Try
    End Sub
End Class