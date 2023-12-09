Imports Microsoft
Imports System.Data.SqlClient
Public Class frmMovimientosAlmRangos
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim ctaFilas As Integer
    Private Sub frmMovimientosAlmRangos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        Me.te.Multiline = True
        Me.te.Visible = False
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim Max As Long = 1000
        Dim z As Long

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1
        oDataSet = New DataSet()

        Try
            Dim daProveedor As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM proveedores", Connection)
            daProveedor.Fill(oDataSet, "proveedores")

            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes", Connection)
            daCliente.Fill(oDataSet, "clientes")

            Dim daAlmCabecera As New SqlDataAdapter("SELECT * FROM almCabecera where (fecOrigen>='" & CDate(dtpFechaInicio.Text) & "' and fecOrigen<='" & CDate(dtpFechaLimite.Text) & "') and status<>'A'", Connection)
            daAlmCabecera.Fill(oDataSet, "almCabecera")

            If Me.oDataSet.Tables(2).Rows.Count <= 0 Then
                MsgBox("No hay información para procesar de está fecha.", MsgBoxStyle.Information)
                Exit Sub
            End If
            ctaFilas = 1

            For i As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                Dim daAlmDetalle As New SqlDataAdapter("SELECT * FROM almDetalle where tipDocumento='" & Me.oDataSet.Tables(2).Rows(i).Item(1) & "' and  numDocumento=" & Me.oDataSet.Tables(2).Rows(i).Item(2) & "", Connection)
                daAlmDetalle.Fill(oDataSet, "almDetalle")
            Next

            Dim daProductos As New SqlDataAdapter("SELECT * FROM productos", Connection)
            daProductos.Fill(oDataSet, "productos")

            Dim daNumerosSerie As SqlDataAdapter
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                If Me.oDataSet.Tables(3).Rows.Item(i).Item(1).ToString() = "SA" Then
                    daNumerosSerie = New SqlDataAdapter("SELECT * from numerosSerie where numDoc='" & Me.oDataSet.Tables(3).Rows.Item(i).Item(2).ToString() & "'", Connection)
                Else
                    daNumerosSerie = New SqlDataAdapter("SELECT * from numerosSerie where numDoc1='" & Me.oDataSet.Tables(3).Rows.Item(i).Item(2).ToString() & "'", Connection)
                End If
                daNumerosSerie.Fill(oDataSet, "numerosSerie")
            Next

            Dim colNombreProducto As DataColumn = New DataColumn()
            colNombreProducto.Caption = "Descripción Producto"
            colNombreProducto.ColumnName = "descripcionProducto"
            Me.oDataSet.Tables(3).Columns.Add(colNombreProducto)

            Dim colMarcaProducto As DataColumn = New DataColumn()
            colMarcaProducto.Caption = "Marca"
            colMarcaProducto.ColumnName = "marca"
            Me.oDataSet.Tables(3).Columns.Add(colMarcaProducto)

            Dim colModeloProducto As DataColumn = New DataColumn()
            colModeloProducto.Caption = "Modelo"
            colModeloProducto.ColumnName = "modelo"
            Me.oDataSet.Tables(3).Columns.Add(colModeloProducto)

            Dim colNumSerie As DataColumn = New DataColumn()
            colNumSerie.Caption = "Número Serie"
            colNumSerie.ColumnName = "numeroSerie"
            Me.oDataSet.Tables(3).Columns.Add(colNumSerie)

            Dim colNumMotor As DataColumn = New DataColumn()
            colNumMotor.Caption = "Número Motor"
            colNumMotor.ColumnName = "numeroMotor"
            Me.oDataSet.Tables(3).Columns.Add(colNumMotor)

            Dim colNumChasis As DataColumn = New DataColumn()
            colNumChasis.Caption = "Número Chásis"
            colNumChasis.ColumnName = "numeroChasis"
            Me.oDataSet.Tables(3).Columns.Add(colNumChasis)

            Dim colCodigoCliente As DataColumn = New DataColumn()
            colCodigoCliente.Caption = "Código Cliente"
            colCodigoCliente.ColumnName = "codigoCliente"
            Me.oDataSet.Tables(3).Columns.Add(colCodigoCliente)

            Dim colNombre As DataColumn = New DataColumn()
            colNombre.AllowDBNull = True
            colNombre.Caption = "Nombre Cliente"
            colNombre.ColumnName = "nombreCliente"
            Me.oDataSet.Tables(3).Columns.Add(colNombre)

            Dim colProveedor As DataColumn = New DataColumn()
            colProveedor.AllowDBNull = True
            colProveedor.Caption = "Nombre Proveedor"
            colProveedor.ColumnName = "nombreProveedor"
            Me.oDataSet.Tables(3).Columns.Add(colProveedor)

            Dim colFecha As DataColumn = New DataColumn()
            colFecha.AllowDBNull = True
            colFecha.Caption = "Fecha"
            colFecha.ColumnName = "fecha"
            Me.oDataSet.Tables(3).Columns.Add(colFecha)

            Dim oDataRow As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(4).Rows.Count() - 1
                    oDataRow = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(4).Rows.Item(x).Item(0) = Me.oDataSet.Tables(3).Rows.Item(i).Item(3) Then
                        oDataRow(6) = Me.oDataSet.Tables(4).Rows(x).Item(2)
                        oDataRow(7) = Me.oDataSet.Tables(4).Rows(x).Item(4)
                        oDataRow(8) = Me.oDataSet.Tables(4).Rows(x).Item(5)
                        Exit For
                    End If
                Next x
            Next i

            Dim oDataRow1 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                'For x As Integer = 0 To oDataSet.Tables(5).Rows.Count() - 1
                oDataRow1 = Me.oDataSet.Tables(3).Rows(i)
                'If Me.oDataSet.Tables(5).Rows.Item(x).Item(0) = Me.oDataSet.Tables(3).Rows.Item(i).Item(3) Then
                oDataRow1(9) = Me.oDataSet.Tables(3).Rows(i).Item(1)
                oDataRow1(10) = Me.oDataSet.Tables(3).Rows(i).Item(2)
                oDataRow1(11) = Me.oDataSet.Tables(3).Rows(i).Item(3)
                'Exit For
                'End If
                'Next x
            Next i

            Dim oDataRow2 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    oDataRow2 = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(2).Rows(x).Item(1) = Me.oDataSet.Tables(3).Rows(i).Item(1) And _
                    Me.oDataSet.Tables(2).Rows(x).Item(2) = Me.oDataSet.Tables(3).Rows(i).Item(2) Then
                        If Me.oDataSet.Tables(3).Rows(i).Item(1) = "SA" Then
                            oDataRow2(12) = Me.oDataSet.Tables(2).Rows(x).Item(9)
                        Else
                            oDataRow2(12) = Me.oDataSet.Tables(2).Rows(x).Item(3)
                        End If
                        Exit For
                    End If
                Next x
            Next i

            Dim oDataRow3 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                    oDataRow3 = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(3).Rows(i).Item(1) = "SA" Then
                        If Me.oDataSet.Tables(1).Rows(x).Item(0) = Me.oDataSet.Tables(3).Rows(i).Item(12) Then
                            oDataRow3(13) = Me.oDataSet.Tables(1).Rows(x).Item(1)
                            Exit For
                        End If
                    End If
                Next x
            Next i

            Dim oDataRow4 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    oDataRow4 = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(3).Rows(i).Item(1) = "EN" Then
                        If Me.oDataSet.Tables(0).Rows(x).Item(0) = Me.oDataSet.Tables(3).Rows(i).Item(12) Then
                            oDataRow4(14) = Me.oDataSet.Tables(0).Rows(x).Item(1)
                            Exit For
                        End If
                    End If
                Next x
            Next i

            Dim oDataRow5 As DataRow
            For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                For x As Integer = 0 To oDataSet.Tables(2).Rows.Count() - 1
                    oDataRow5 = Me.oDataSet.Tables(3).Rows(i)
                    If Me.oDataSet.Tables(2).Rows(x).Item(1) = Me.oDataSet.Tables(3).Rows(i).Item(1) And _
                        Me.oDataSet.Tables(2).Rows(x).Item(2) = Me.oDataSet.Tables(3).Rows(i).Item(2) Then
                        oDataRow5(15) = Me.oDataSet.Tables(2).Rows(x).Item(4)
                        Exit For
                    End If
                Next x
            Next i

            For z = 1 To Max
                ProgressBar1.PerformStep()
                Me.lblAvance.Text = Format(z / Max, "0%")
                Application.DoEvents()
            Next z

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            If ctaFilas > 0 Then
                Dim en, t As Keys
                Dim enter, tab As Char
                en = Keys.Enter
                t = Keys.Tab
                enter = Convert.ToChar(en)
                tab = Convert.ToChar(t)
                te.Text = enter & _
                txtNombreEmpresa & enter & enter & _
                txtRUCEmpresa & "                                                                                                                           Fecha :" & DateTime.Today & enter & enter
                te.Text = te.Text & "                                        MOVIMIENTO DIARIO ALMACEN del " & Me.dtpFechaInicio.Text & " al " & Me.dtpFechaLimite.Text & enter
                te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------------------------------------------------" & enter
                te.Text = te.Text & "Item Descripción           Marca            Modelo         N° Serie             N° Motor            Cliente/Proveedor         Fecha      N°Docum.  Cantidad " & enter
                te.Text = te.Text & "                                                                                                                                                   Ent.|Sal." & enter
                te.Text = te.Text & "------------------------------------------------------------------------------------------------------------------------------------------------------------" & enter

                For i As Integer = 0 To oDataSet.Tables(3).Rows.Count() - 1
                    Dim spacio As String = ""
                    Dim nombres As String

                    If Me.oDataSet.Tables(3).Rows(i).Item(1) = "SA" Then
                        spacio = "     "
                        nombres = Me.oDataSet.Tables(3).Rows(i).Item(13)
                    Else
                        nombres = Me.oDataSet.Tables(3).Rows(i).Item(14)
                    End If

                    te.Text = te.Text & i + 1 & Space(5 - Len(Str(i + 1))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(6), 20) & Space(20 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(6), 20))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(7), 15) & Space(15 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(7), 15))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(8), 15) & Space(15 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(8), 15))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(9), 20) & Space(20 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(9), 20))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(10), 20) & Space(20 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(10), 20))) & " "
                    te.Text = te.Text & VisualBasic.Left(nombres, 20) & Space(25 - Len(VisualBasic.Left(nombres, 20))) & " "
                    te.Text = te.Text & VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(15), 10) & Space(10 - Len(VisualBasic.Left(Me.oDataSet.Tables(3).Rows(i).Item(15), 10))) & " "
                    te.Text = te.Text & Me.oDataSet.Tables(3).Rows(i).Item(2) & Space(10 - Len(Str(Me.oDataSet.Tables(3).Rows(i).Item(2)))) & "  "
                    te.Text = te.Text & spacio & " " & Me.oDataSet.Tables(3).Rows(i).Item(4) & enter
                Next i
                te.Text = te.Text & "-----------------------------------------------------------------------------------------------------------------------------------------------------------" & enter

                If MsgBox("Desea hacer una vista previa del documento?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    configurarImpresion()
                    PrintPreviewDialog1.Document = PrintDocument1
                    PrintDocument1.DefaultPageSettings.Landscape = True
                    PrintPreviewDialog1.ShowDialog()
                End If

                configurarImpresion()
                PrintDialog1.Document = PrintDocument1
                If PrintDialog1.ShowDialog = DialogResult.OK Then
                    PrintDocument1.DefaultPageSettings.Landscape = True
                    PrintDocument1.Print()
                End If
            Else
                MsgBox("No se puede imprimir si no hay información previamente procesada.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VistaPrevia(ByVal TipoFuente As String, ByVal TamañoFuente As Byte, ByVal TextoImpresion As String, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim Fuente As New Font(TipoFuente, TamañoFuente)
        Dim AreaImpresion_Alto, AreaImpresion_Ancho, MargenIzquierdo, MargenSuperior As Integer

        With Me.PrintDocument1.DefaultPageSettings
            'Area Neta de Impresion (se descuenta los margenes)
            AreaImpresion_Alto = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            AreaImpresion_Ancho = .PaperSize.Width - .Margins.Left - .Margins.Right

            MargenIzquierdo = .Margins.Left
            MargenSuperior = .Margins.Top

            'Verificar si se ha elegido el modo horizontal
            If .Landscape Then
                Dim NroTemp As Integer
                NroTemp = AreaImpresion_Alto
                AreaImpresion_Alto = AreaImpresion_Ancho
                AreaImpresion_Ancho = NroTemp
            End If
            Dim Formato As New StringFormat(StringFormatFlags.LineLimit)
            Dim Rectangulo As New RectangleF(MargenIzquierdo, MargenSuperior, _
            AreaImpresion_Ancho, AreaImpresion_Alto)
            Dim NroLineasImpresion As Integer = CInt(AreaImpresion_Alto / Fuente.Height)
            Dim NroLineasRelleno, NroLetrasLinea As Integer
            Static CaracterActual As Integer
            e.Graphics.MeasureString(Mid(te.Text, +1), Fuente, _
            New SizeF(AreaImpresion_Ancho, AreaImpresion_Alto), Formato, NroLetrasLinea, _
            NroLineasRelleno)
            e.Graphics.DrawString(Mid(TextoImpresion, CaracterActual + 1), Fuente, _
            Brushes.Black, Rectangulo, Formato)
            CaracterActual += NroLetrasLinea
            If CaracterActual < TextoImpresion.Length Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
                CaracterActual = 0
            End If
        End With
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        VistaPrevia("Courier New", 8, te.Text, e)
    End Sub
    Private Sub configurarImpresion()
        Dim nombrePapel As String = "Almacen"
        Dim TamañoPersonal As Printing.PaperSize
        Dim margenes As New Printing.Margins

        Dim Ancho As Short = 890
        Dim Alto As Short = 1090

        Dim left As Short = 10
        Dim top As Short = 5
        Dim bottom As Short = 5
        Dim right As Short = 5

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
        Me.Close()
    End Sub
End Class