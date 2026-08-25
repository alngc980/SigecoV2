Imports Microsoft
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class frmeditarProductos
    Private oDataSet As DataSet
    Dim te As New RichTextBox
    Dim c As Cursor = Me.Cursor

    Public nComisionVisa As Decimal
    Private Sub frmeditarProductos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Controls.Add(te)
        te.Multiline = True
        te.Visible = False
        oDataSet = New DataSet()

        Try
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("select * from productos where stoInicial=0 or stoInicial=1", Connection)
            daProductos.Fill(oDataSet, "productos")

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "productos"
            With Me.dgvProductos
                .Columns(0).ReadOnly = True
                .Columns(0).Width = 60
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Width = 40
                .Columns(1).ReadOnly = True
                .Columns(2).Width = 250
                .Columns(3).ReadOnly = True
                .Columns(3).Width = 40
                .Columns(6).ReadOnly = True
                .Columns(6).Width = 40
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "#####0.00"
                .Columns(7).Width = 70
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Format = "#####0.00"
                .Columns(8).Width = 70
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "#####0.00"
                .Columns(9).ReadOnly = True
                .Columns(9).Width = 70
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Format = "#####0.00"
                .Columns(10).ReadOnly = True
                .Columns(10).Width = 85
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Format = "#####0.00"
                .Columns(11).ReadOnly = True
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "#####0.00"
                .Columns(12).Width = 70
                .Columns(12).DisplayIndex = 8
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Format = "#####0.00"
                .Columns(13).Width = 70
                .Columns(13).DisplayIndex = 9
                .Columns(14).ReadOnly = True
                .Columns(15).ReadOnly = True
                .Columns(16).ReadOnly = True
            End With


            Dim datos As DataTable = RetornaDataTable("select * from FactorComisionVisa")
            If datos.Rows.Count > 0 Then
                nComisionVisa = datos.Rows(0)(0).ToString()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub txtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daProductos As SqlDataAdapter = New SqlDataAdapter("select * from Productos where (stoInicial=0 or stoInicial=1) and desProducto Like '" & "%" & Me.txtProducto.Text & "%" & "'", Connection)
            daProductos.Fill(oDataSet, "productos")
            Connection.Close()

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "productos"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList
        Dim oFrmAcceso As New frmaccesoAdministrador()

        Try
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            For i As Integer = 0 To dgvProductos.Rows.Count - 1
                SqlString = "update productos set idGrupo='" & dgvProductos.Rows(i).Cells(1).Value & "',desProducto ='" & _
                dgvProductos.Rows(i).Cells(2).Value & "', marca ='" & dgvProductos.Rows(i).Cells(4).Value & "',modelo ='" & _
                dgvProductos.Rows(i).Cells(5).Value & "', preContado =" & dgvProductos.Rows(i).Cells(7).Value & ",preCredito =" & _
                dgvProductos.Rows(i).Cells(8).Value & ",preTarjeta =" & dgvProductos.Rows(i).Cells(9).Value & ",preTarjetaOferta =" & _
                dgvProductos.Rows(i).Cells(10).Value & ",preTarjetaRemate =" & dgvProductos.Rows(i).Cells(11).Value & ",preOferta =" & _
                dgvProductos.Rows(i).Cells(12).Value & ",preRemate =" & dgvProductos.Rows(i).Cells(13).Value & ",afeIGV ='" & _
                dgvProductos.Rows(i).Cells(15).Value & "' where idProducto= " & dgvProductos.Rows(i).Cells(0).Value & ""
                ListSqlStrings.Add(SqlString)
            Next
            If transaccionProducto(ListSqlStrings) Then
                MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                flag = 0
                Me.Close()
            Else
                MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGenerarPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarPDF.Click
        Dim swEscritor As StreamWriter

        If Me.dgvProductos.RowCount <= 0 Then
            MsgBox("No existe información para generar  impresión  !  !  !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor

        Try
            If My.Computer.FileSystem.DirectoryExists("D:\Reportes_Sistema") = False Then
                My.Computer.FileSystem.CreateDirectory("D:\Reportes_Sistema")
            Else
                If My.Computer.FileSystem.FileExists("D:\Reportes_Sistema\reporteProductos.txt") = True Then
                    My.Computer.FileSystem.DeleteFile("D:\Reportes_Sistema\reporteProductos.txt")
                End If
            End If

            swEscritor = New StreamWriter("D:\Reportes_Sistema\reporteProductos.txt", True)
            swEscritor.WriteLine("                                             Comercial  Oriente  Hnos. SAC")
            swEscritor.WriteLine("                                                  Productos en Almacén")
            swEscritor.WriteLine("Fecha: " & DateTime.Today)
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------------------------")
            swEscritor.WriteLine("|Cód|Descripción Producto   |   Marca  |  Modelo      |P.Contado|P.Crédito|P.Tarjet|P.Tarj.Ofert|P.Tarj.Remat|P.Oferta|P.Remat|")
            swEscritor.WriteLine("-------------------------------------------------------------------------------------------------------------------------------")
            For i As Integer = 0 To Me.dgvProductos.RowCount - 1
                swEscritor.Write(Me.dgvProductos.Rows(i).Cells(0).Value.ToString.PadLeft(5) & " ")
                swEscritor.Write(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(2).Value.ToString, 25).PadRight(26))
                swEscritor.Write(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(4).Value.ToString, 10).PadRight(11))
                swEscritor.Write(VisualBasic.Left(Me.dgvProductos.Rows(i).Cells(5).Value.ToString, 12).PadRight(13))

                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(7).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(8).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(9).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(10).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(11).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.Write(Format(Me.dgvProductos.Rows(i).Cells(12).Value, "#####0.00").ToString.PadLeft(10))
                swEscritor.WriteLine(Format(Me.dgvProductos.Rows(i).Cells(13).Value, "#####0.00").ToString.PadLeft(10))

            Next i
            swEscritor.Close()

            'Creamos el objeto documento PDF
            Dim documentoPDF As New Document

            'Comprobamos si existe archivo texto original
            If System.IO.File.Exists("D:\Reportes_Sistema\reporteProductos.txt") = False Then
                MsgBox("No existe archivo para generar documento PDF  !  !  !", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim SPath As String = "D:\Reportes_Sistema\reporteProductos.txt"
            Dim sContent As String = vbNullString

            With My.Computer.FileSystem
                If .FileExists(SPath) Then
                    sContent = .ReadAllText(SPath)
                    Me.txtTexto.Text = sContent
                Else
                    MsgBox("Ruta Documento Inválida   ! ! !", MsgBoxStyle.Critical, "error")
                End If
            End With

            PdfWriter.GetInstance(documentoPDF, New FileStream("D:\Reportes_Sistema\reporteProductos.pdf", FileMode.Create))
            'Formateando documento
            documentoPDF.SetPageSize(PageSize.LETTER)
            documentoPDF.SetMargins(0.0F, 0.0F, 28.34F, 28.34F)
            'Abrimos el archivo para generar el contenido
            documentoPDF.Open()

            'Escribimos el texto en el objeto documento PDF
            documentoPDF.Add(New Paragraph(Me.txtTexto.Text, FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL)))

            'Añadimos los metadatos para el fichero PDF
            documentoPDF.AddAuthor("Carlos Tang")
            documentoPDF.AddCreator("Carlos Tang")
            documentoPDF.AddKeywords("CT")
            documentoPDF.AddTitle("Reporte Productos")
            documentoPDF.AddCreationDate()

            'Cerramos el objeto documento, guardamos y creamos el PDF
            documentoPDF.Close()

            'Comprobamos si se ha creado el fichero PDF
            If System.IO.File.Exists("D:\Reportes_Sistema\reporteProductos.pdf") Then
                If MsgBox("Documento PDF con Productos en Almacén, Generado Correctamente. " + "¿Desea Abrir Documento PDF Generado?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    System.Diagnostics.Process.Start("D:\Reportes_Sistema\reporteProductos.pdf")
                End If
            Else
                MsgBox("El Documento PDF no se ha generado, " + "compruebe que tiene permisos en la carpeta de destino.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox("Se ha producido un error al intentar convertir el texto a PDF: " + vbCrLf + vbCrLf + ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        Finally
            Me.Cursor = c
        End Try
    End Sub
    Private Sub btnGenerarExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerarExcel.Click
        Dim m_Excel As New Excel.Application
        m_Excel.Cursor = Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Excel.Worksheet = objLibroExcel.Worksheets(1)

        Try
            Me.Cursor = Cursors.WaitCursor
            With objHojaExcel
                .Visible = Excel.XlSheetVisibility.xlSheetVisible
                .Activate()
                'Encabezado  
                '.Range("A1:L1").Merge()
                '.Range("A1:L1").Value = txtNombreEmpresa
                '.Range("A1:L1").Font.Bold = True
                '.Range("A1:L1").Font.Size = 14
                ''Copete  
                '.Range("A2:L2").Merge()
                '.Range("A2:L2").Value = "Lista Precios Productos"
                '.Range("A2:L2").Font.Bold = True
                '.Range("A2:L2").Font.Size = 12

                Const primeraLetra As Char = "A"
                Const primerNumero As Short = 1
                Dim Letra As Char, UltimaLetra As Char
                Dim Numero As Integer, UltimoNumero As Integer
                Dim cod_letra As Byte = Asc(primeraLetra) - 1
                Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
                Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator

                'Establecer formatos de las columnas de la hija de cálculo  
                Dim strColumna As String = ""
                Dim LetraIzq As String = ""
                Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
                Letra = primeraLetra
                Numero = primerNumero
                Dim objCelda As Excel.Range
                For Each c As DataGridViewColumn In dgvProductos.Columns
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra + Numero.ToString
                        objCelda = .Range(strColumna, Type.Missing)
                        objCelda.Value = c.HeaderText
                        objCelda.EntireColumn.Font.Size = 12
                        'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format  
                        If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                            objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                        End If
                    End If
                Next

                Dim objRangoEncab As Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
                objRangoEncab.BorderAround(1, Excel.XlBorderWeight.xlThin)
                UltimaLetra = Letra
                Dim UltimaLetraIzq As String = LetraIzq

                'CARGA DE DATOS  
                Dim i As Integer = Numero + 1
                For Each reg As DataGridViewRow In dgvProductos.Rows
                    LetraIzq = ""
                    cod_LetraIzq = Asc(primeraLetra) - 1
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra) - 1
                    For Each c As DataGridViewColumn In dgvProductos.Columns
                        If c.Visible Then
                            If Letra = "Z" Then
                                Letra = primeraLetra
                                cod_letra = Asc(primeraLetra)
                                cod_LetraIzq += 1
                                LetraIzq = Chr(cod_LetraIzq)
                            Else
                                cod_letra += 1
                                Letra = Chr(cod_letra)
                            End If
                            strColumna = LetraIzq + Letra
                            ' acá debería realizarse la carga  
                            .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                            '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))  
                            '.Range(strColumna + i, strColumna + i).In()  
                        End If
                    Next
                    Dim objRangoReg As Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                    objRangoReg.Rows.BorderAround()
                    objRangoReg.Select()
                    i += 1
                Next
                UltimoNumero = i

                'Dibujar las líneas de las columnas  
                LetraIzq = ""
                cod_LetraIzq = Asc("A")
                cod_letra = Asc(primeraLetra)
                Letra = primeraLetra
                For Each c As DataGridViewColumn In dgvProductos.Columns
                    If c.Visible Then
                        objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                        objCelda.BorderAround()
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            LetraIzq = Chr(cod_LetraIzq)
                            cod_LetraIzq += 1
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                    End If
                Next

                'Dibujar el border exterior grueso  
                Dim objRango As Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
                objRango.Select()
                objRango.Columns.AutoFit()
                objRango.Columns.BorderAround(1, Excel.XlBorderWeight.xlThin)
            End With

            If My.Computer.FileSystem.DirectoryExists("D:\EXCEL_COMERORI") = False Then
                My.Computer.FileSystem.CreateDirectory("D:\EXCEL_COMERORI")
            Else
                If My.Computer.FileSystem.FileExists("D:\EXCEL_COMERORI\precios.xlsx") = True Then
                    My.Computer.FileSystem.DeleteFile("D:\EXCEL_COMERORI\precios.xlsx")
                End If
            End If

            m_Excel.Cursor = Excel.XlMousePointer.xlDefault
            objHojaExcel.Name = "precios"
            objHojaExcel.SaveAs("D:\EXCEL_COMERORI\precios.xlsx")
            m_Excel.Quit()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            ' Restauramos el cursor
            Me.Cursor = c
        End Try
    End Sub
    Private Sub dgvProductos_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellEndEdit
        Dim celda = dgvProductos(e.ColumnIndex, e.RowIndex)

        If (e.ColumnIndex = 7 Or e.ColumnIndex = 12 Or e.ColumnIndex = 13) And IsDBNull(celda.value) Then
            celda.Value = 0
        End If

        If IsNumeric(celda.value) Then
            If e.ColumnIndex = 7 Then
                Me.dgvProductos.Rows(e.RowIndex).Cells(9).Value() = Me.dgvProductos.Rows(e.RowIndex).Cells(7).Value() / nComisionVisa
            Else
                If e.ColumnIndex = 12 Then
                    Me.dgvProductos.Rows(e.RowIndex).Cells(10).Value() = Me.dgvProductos.Rows(e.RowIndex).Cells(12).Value() / nComisionVisa
                Else
                    If e.ColumnIndex = 13 Then
                        Me.dgvProductos.Rows(e.RowIndex).Cells(11).Value() = Me.dgvProductos.Rows(e.RowIndex).Cells(13).Value() / nComisionVisa
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dgvProductos_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        Dim letra As Short = CShort(Asc(e.KeyChar))

        If columna = 7 Or columna = 8 Or columna = 9 Or columna = 10 Or columna = 11 Or columna = 12 Or columna = 13 Then
            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvProductos1_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim convierteMayuscula As TextBox = CType(e.Control, TextBox)
        AddHandler convierteMayuscula.KeyPress, AddressOf convierteMayuscula_Keypress
    End Sub
    Private Sub convierteMayuscula_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 2 Or columna = 4 Or columna = 5 Or columna = 15 Then
            Dim caracter As Char = e.KeyChar
            e.KeyChar = Char.ToUpper(caracter)
        End If
    End Sub
    Private Sub dgvProductos_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvProductos.EditingControlShowing
        Dim columna As Integer = dgvProductos.CurrentCell.ColumnIndex
        If columna = 15 Then
            DirectCast(e.Control, TextBox).MaxLength = 1
        End If
    End Sub
    Private Sub dgvProductos_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvProductos.MouseDoubleClick
        Dim ofrmmostrarSeries As New frmconsultarSeries()
        codigoProducto = Me.dgvProductos.Rows(dgvProductos.CurrentCell.RowIndex).Cells(0).Value
        ofrmmostrarSeries.ShowDialog()
    End Sub
    Private Sub txtProducto_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.MouseEnter
        Me.lblMensaje.Text = "Si desea escriba la inicial del producto para hacer una búsqueda incremental."
    End Sub
    Private Sub txtProducto_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub GroupBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseEnter
        Me.lblMensaje.Text = "Si desea escriba la inicial del producto para hacer una búsqueda incremental."
    End Sub
    Private Sub GroupBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBox2.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub dgvProductos_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseEnter
        Me.lblMensaje.Text = "Haz doble click en cualquier registro para visualizar series o números de motor correspondiente."
    End Sub
    Private Sub dgvProductos_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub dgvProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvProductos.KeyDown
        Clipboard.Clear() ' Limpia el portapapeles
        If e.Control AndAlso e.KeyCode = Keys.C Then
            Dim oFrmAcceso As New frmaccesoAdministrador()
            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                e.SuppressKeyPress = True
                Exit Sub
            End If
            e.SuppressKeyPress = False
        End If
    End Sub
End Class