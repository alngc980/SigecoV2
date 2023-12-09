Imports System.Data.SqlClient
Public Class frmnumerosDocumentos
    Private oDataSet As DataSet
    Private Sub frmnumerosDocumentos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDataSet = New DataSet()

        Try
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("select * from ultimosNumeros", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")

            Dim tipoDocumento As DataColumn = New DataColumn()
            tipoDocumento.AllowDBNull = True
            tipoDocumento.Caption = "Nombre Documento"
            tipoDocumento.ColumnName = "nombreDocumento"
            Me.oDataSet.Tables(0).Columns.Add(tipoDocumento)

            Dim oDataRowNombre As DataRow
            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                oDataRowNombre = Me.oDataSet.Tables(0).Rows(i)
                If Me.oDataSet.Tables(0).Rows(i).Item(1) = "BV" Then
                    oDataRowNombre(3) = "BOLETA ELECTRÓNICA"
                Else
                    If Me.oDataSet.Tables(0).Rows(i).Item(1) = "FV" Then
                        oDataRowNombre(3) = "FACTURA ELECTRONICA"
                    Else
                        If Me.oDataSet.Tables(0).Rows(i).Item(1) = "NC" Then
                            oDataRowNombre(3) = "NOTA CREDITO"
                        Else
                            If Me.oDataSet.Tables(0).Rows(i).Item(1) = "ND" Then
                                oDataRowNombre(3) = "NOTA DEBITO"
                            Else
                                If Me.oDataSet.Tables(0).Rows(i).Item(1) = "NA" Then
                                    oDataRowNombre(3) = "NOTA ABONO"
                                Else
                                    If Me.oDataSet.Tables(0).Rows(i).Item(1) = "RC" Then
                                        oDataRowNombre(3) = "RECIBO CAJA"
                                    Else
                                        If Me.oDataSet.Tables(0).Rows(i).Item(1) = "RP" Then
                                            oDataRowNombre(3) = "RECIBO PRESTAMO"
                                        Else
                                            If Me.oDataSet.Tables(0).Rows(i).Item(0) = "PD" Then
                                                oDataRowNombre(3) = "PARTE DIARIO"
                                            Else
                                                oDataRowNombre(3) = "GUIA REMISION"
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next i

            Me.dgvTipoCambio.DataSource = oDataSet
            Me.dgvTipoCambio.DataMember = "ctaCorriente"
            With Me.dgvTipoCambio
                .Columns(0).DisplayIndex = 1
                .Columns(0).ReadOnly = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DisplayIndex = 2
                .Columns(1).ReadOnly = True
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DisplayIndex = 3
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).DisplayIndex = 0
                .Columns(3).ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim SqlString As String = ""
            Dim ListSqlStrings As New ArrayList
            Dim oFrmAcceso As New frmaccesoAdministrador()

            oFrmAcceso.ShowDialog()
            If flag <> 1 Then
                Exit Sub
            End If

            For i As Integer = 0 To dgvTipoCambio.Rows.Count - 1
                SqlString = "UPDATE ultimosNumeros Set numero='" & dgvTipoCambio.Rows(i).Cells(2).Value & "' where tipDocumento= '" & dgvTipoCambio.Rows(i).Cells(0).Value & "' and tipMovimiento= '" & dgvTipoCambio.Rows(i).Cells(1).Value & "'"
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
        End Try
    End Sub
    Private Sub dgvTipoCambio_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvTipoCambio.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvTipoCambio.CurrentCell.ColumnIndex
        If columna = 2 Then
            Dim caracter As Char = e.KeyChar
            If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class