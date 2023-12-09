Imports System.Data.SqlClient
Public Class frmbuscaRecibo
    Private oDataSet As DataSet
    Dim contador As Byte = 0
    Dim tipoMoneda As Integer
    Dim arrayConceptos() As String = {"V.Contado", "A.Letra", "C.Letra", "C.Inicial", "A.Cuota Inicial", "V.Tarjeta", "V.Tarjeta Oferta", "V.Tarjeta Remate", "V.Oferta", "V.Remate", "O.Pagos"}
    Dim arrayMonedas() As String = {"S/.", "$", "€"}
    Private Sub frmbuscaRecibo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        numModulo = 0
        contador = 0

        Me.dgvClientes.ReadOnly = False
        Me.dgvClientes.Rows.Clear()

        Me.VentaContado.Checked = False
        Me.CuotaInicial.Checked = False
        Me.AnticipoCuota.Checked = False
        Me.VentaTarjeta.Checked = False
        Me.VentaTarjetaOferta.Checked = False
        Me.VentaTarjetaRemate.Checked = False
        Me.VentaOferta.Checked = False
        Me.VentaRemate.Checked = False
        Me.OtrosPagos.Checked = False

        totalRecibosMN = 0
        totalRecibosME = 0
    End Sub
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaCliente.KeyUp
        numModulo = 0
        contador = 0

        Me.dgvClientes.ReadOnly = False
        Me.dgvClientes.Rows.Clear()

        Me.VentaContado.Checked = False
        Me.CuotaInicial.Checked = False
        Me.AnticipoCuota.Checked = False
        Me.VentaTarjeta.Checked = False
        Me.VentaTarjetaOferta.Checked = False
        Me.VentaTarjetaRemate.Checked = False
        Me.VentaOferta.Checked = False
        Me.VentaRemate.Checked = False
        Me.OtrosPagos.Checked = False

        Try
            oDataSet = New DataSet()
            Dim CDENA As String
            CDENA = "SELECT  idRecibo,concepto,impDocumento,impDocumentoME,fecEmision,idMoneda,c.idCliente,status , CL.nombres " &
                    "From recibosClientes c " &
                    "inner join clientes cl on cl.idCliente = c.idCliente " &
                    "Where (concepto ='0'or concepto='3'or concepto='4'or concepto='5' or concepto='6'or concepto='7' or concepto='8'or concepto='9' or concepto='10') and " &
                    "numDocGenACI ='' " &
                    "And cl.nombres Like '" & "%" & Me.txtBuscaCliente.Text & "%" & "'" &
                    "And status<>'X' "
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter(CDENA, Connection)
            daCliente.Fill(oDataSet, "cliente")

            'CDENA = "SELECT  idRecibo,concepto,impDocumento,impDocumentoME,fecEmision,idMoneda,idCliente,status FROM recibosClientes" &
            '                                                    " where (concepto='0'or concepto='3'or concepto='4'or concepto='5' or concepto='6'or concepto='7'" &
            '                                                    " or concepto='8'or concepto='9' or concepto='10') and numDocGenACI='' and idCliente=" & CInt(oDataSet.Tables(0).Rows(0).Item(0)) & " and status<>'X'"
            'Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter(CDENA, Connection)
            'daCTaCte.Fill(oDataSet, "ctaCorriente")

            'Dim colNombre As DataColumn = New DataColumn()
            'colNombre.AllowDBNull = True
            'colNombre.Caption = "Nombre Cliente"
            'colNombre.ColumnName = "nombreCliente"
            'Me.oDataSet.Tables(0).Columns.Add(colNombre)

            'Dim oDataRow As DataRow
            'For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
            '    For x As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
            '        If Me.oDataSet.Tables(0).Rows.Item(x).Item(0) = Me.oDataSet.Tables(0).Rows.Item(i).Item(6) Then
            '            oDataRow = Me.oDataSet.Tables(0).Rows(i)
            '            oDataRow(8) = Me.oDataSet.Tables(0).Rows.Item(x).Item(1)
            '        End If
            '    Next x
            'Next i

            For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                Me.dgvClientes.Rows.Add()
                Me.dgvClientes.Rows(i).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                Me.dgvClientes.Rows(i).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                Me.dgvClientes.Rows(i).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                Me.dgvClientes.Rows(i).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                Me.dgvClientes.Rows(i).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                Me.dgvClientes.Rows(i).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                Me.dgvClientes.Rows(i).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                Me.dgvClientes.Rows(i).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                Me.dgvClientes.Rows(i).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
            Next i
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub VentaContado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentaContado.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 1
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "0" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub CuotaInicial_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CuotaInicial.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 2
                Me.VentaContado.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "3" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AnticipoCuota_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnticipoCuota.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 3
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "4" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VentaTarjeta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VentaTarjeta.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 4
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "5" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VentaTarjetaOferta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentaTarjetaOferta.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 5
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "6" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VentaTarjetaRemate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentaTarjetaRemate.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 6
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "7" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VentaOferta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentaOferta.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 7
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaRemate.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "8" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub VentaRemate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VentaRemate.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 8
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.OtrosPagos.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "9" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub OtrosPagos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtrosPagos.CheckedChanged
        Try
            If sender.Checked = True And Me.dgvClientes.Rows.Count > 0 Then
                numModulo = 9
                Me.VentaContado.Checked = False
                Me.CuotaInicial.Checked = False
                Me.AnticipoCuota.Checked = False
                Me.VentaTarjeta.Checked = False
                Me.VentaTarjetaOferta.Checked = False
                Me.VentaTarjetaRemate.Checked = False
                Me.VentaOferta.Checked = False
                Me.VentaRemate.Checked = False

                Dim row As Byte = 0
                Me.dgvClientes.Rows.Clear()

                For i As Integer = 0 To oDataSet.Tables(0).Rows.Count() - 1
                    If Me.oDataSet.Tables(0).Rows(i).Item(1).ToString.Trim = "10" Then
                        Me.dgvClientes.Rows.Add()
                        Me.dgvClientes.Rows(row).Cells(0).Value = oDataSet.Tables(0).Rows(i).Item(0).ToString
                        Me.dgvClientes.Rows(row).Cells(1).Value = arrayConceptos(oDataSet.Tables(0).Rows(i).Item(1))
                        Me.dgvClientes.Rows(row).Cells(2).Value = oDataSet.Tables(0).Rows(i).Item(2).ToString
                        Me.dgvClientes.Rows(row).Cells(3).Value = oDataSet.Tables(0).Rows(i).Item(3).ToString
                        Me.dgvClientes.Rows(row).Cells(4).Value = oDataSet.Tables(0).Rows(i).Item(4).ToString
                        'Me.dgvClientes.Rows(row).Cells(5).Value = arrayMonedas(oDataSet.Tables(0).Rows(i).Item(5) - 1)
                        Me.dgvClientes.Rows(row).Cells(5).Value = oDataSet.Tables(0).Rows(i).Item(5).ToString
                        Me.dgvClientes.Rows(row).Cells(6).Value = oDataSet.Tables(0).Rows(i).Item(6).ToString
                        Me.dgvClientes.Rows(row).Cells(7).Value = oDataSet.Tables(0).Rows(i).Item(8).ToString
                        row += 1
                    End If
                Next i
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If numModulo = 0 Then
            Exit Sub
        End If

        Try
            If numModulo = 1 Then 'Venta Contado
                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                        Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                        Exit For
                    End If
                Next i

                Dim x As Byte
                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                        If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                            matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                            If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                            Else
                                totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                            End If
                            x += 1
                        Else
                            Dim y As Integer
                            MsgBox("No se puede elegir recibos 'Venta Contado' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                            For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                matrizRecibos(y, 0) = ""
                                matrizRecibos(y, 1) = ""
                            Next y
                            totalRecibosME = 0
                            totalRecibosMN = 0
                            Exit Sub
                        End If
                    End If
                Next i
            Else
                If numModulo = 2 Then 'Cuota Inicial
                    For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                        If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                            Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                            Exit For
                        End If
                    Next i

                    Dim x As Byte
                    For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                        If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                            If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                    totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                    matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                Else
                                    totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                    matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                End If
                                x += 1
                            Else
                                Dim y As Integer
                                MsgBox("No se permite elegir recibos 'Cuota Inicial' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                    matrizRecibos(y, 0) = ""
                                    matrizRecibos(y, 1) = ""
                                Next y
                                totalRecibosME = 0
                                totalRecibosMN = 0
                                Exit Sub
                            End If
                        End If
                    Next i
                Else
                    If numModulo = 3 Then 'Anticipos Couta Inicial
                        Dim x As Byte
                        For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                            If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                Exit For
                            End If
                        Next i

                        For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                            If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                    matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                    If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                        totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                        matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                    Else
                                        totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                        matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                    End If
                                    x += 1
                                Else
                                    Dim y As Integer
                                    MsgBox("No se permite elegir recibos 'Anticipo Cuota Inicial' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                    For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                        matrizRecibos(y, 0) = ""
                                        matrizRecibos(y, 1) = ""
                                    Next y
                                    totalRecibosME = 0
                                    totalRecibosMN = 0
                                    Exit Sub
                                End If
                            End If
                        Next i
                    Else
                        If numModulo = 4 Then 'Venta Tarjeta
                            Dim x As Byte
                            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                    Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                    Exit For
                                End If
                            Next i

                            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                    If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                        matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                        If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                            totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                            matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                        Else
                                            totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                            matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                        End If
                                        x += 1
                                    Else
                                        Dim y As Integer
                                        MsgBox("No se permite elegir recibos 'Venta Tarjeta' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                        For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                            matrizRecibos(y, 0) = ""
                                            matrizRecibos(y, 1) = ""
                                        Next y
                                        totalRecibosME = 0
                                        totalRecibosMN = 0
                                        Exit Sub
                                    End If
                                End If
                            Next i
                        Else
                            If numModulo = 5 Then 'Venta Tarjeta Oferta
                                Dim x As Byte
                                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                        Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                        Exit For
                                    End If
                                Next i

                                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                        If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                            matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                            If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                                totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                            Else
                                                totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                            End If
                                            x += 1
                                        Else
                                            Dim y As Integer
                                            MsgBox("No se permite elegir recibos 'Venta Tarjeta Oferta' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                            For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                                matrizRecibos(y, 0) = ""
                                                matrizRecibos(y, 1) = ""
                                            Next y
                                            totalRecibosME = 0
                                            totalRecibosMN = 0
                                            Exit Sub
                                        End If
                                    End If
                                Next i
                            Else
                                If numModulo = 6 Then 'Venta Tarjeta Remate
                                    Dim x As Byte
                                    For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                        If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                            Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                            Exit For
                                        End If
                                    Next i

                                    For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                        If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                            If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                                matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                                If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                                    totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                                    matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                                Else
                                                    totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                                    matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                                End If
                                                x += 1
                                            Else
                                                Dim y As Integer
                                                MsgBox("No se permite elegir recibos 'Venta Tarjeta Remate' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                                For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                                    matrizRecibos(y, 0) = ""
                                                    matrizRecibos(y, 1) = ""
                                                Next y
                                                totalRecibosME = 0
                                                totalRecibosMN = 0
                                                Exit Sub
                                            End If
                                        End If
                                    Next i
                                Else
                                    If numModulo = 7 Then 'Venta Oferta
                                        Dim x As Byte
                                        For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                            If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                                Exit For
                                            End If
                                        Next i

                                        For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                            If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                                    matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                                    If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                                        totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                                        matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                                    Else
                                                        totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                                        matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                                    End If
                                                    x += 1
                                                Else
                                                    Dim y As Integer
                                                    MsgBox("No se permite elegir recibos 'Venta Oferta' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                                    For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                                        matrizRecibos(y, 0) = ""
                                                        matrizRecibos(y, 1) = ""
                                                    Next y
                                                    totalRecibosME = 0
                                                    totalRecibosMN = 0
                                                    Exit Sub
                                                End If
                                            End If
                                        Next i
                                    Else
                                        If numModulo = 8 Then 'Venta Remate
                                            Dim x As Byte
                                            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                                If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                    Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                                    Exit For
                                                End If
                                            Next i

                                            For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                                If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                    If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                                        matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                                        If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                                            totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                                            matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                                        Else
                                                            totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                                            matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                                        End If
                                                        x += 1
                                                    Else
                                                        Dim y As Integer
                                                        MsgBox("No se permite elegir recibos 'Venta Remate' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                                        For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                                            matrizRecibos(y, 0) = ""
                                                            matrizRecibos(y, 1) = ""
                                                        Next y
                                                        totalRecibosME = 0
                                                        totalRecibosMN = 0
                                                        Exit Sub
                                                    End If
                                                End If
                                            Next i
                                        Else
                                            If numModulo = 9 Then 'Otros Pagos
                                                Dim x As Byte
                                                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                        Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value
                                                        Exit For
                                                    End If
                                                Next i

                                                For i As Integer = 0 To Me.dgvClientes.RowCount - 1
                                                    If Me.dgvClientes.Rows(i).Cells(8).Value = True Then
                                                        If Me.tipoMoneda = Me.dgvClientes.Rows(i).Cells(5).Value Then
                                                            matrizRecibos(x, 0) = Me.dgvClientes.Rows(i).Cells(0).Value
                                                            If Me.dgvClientes.Rows(i).Cells(5).Value >= 2 Then
                                                                totalRecibosME += Me.dgvClientes.Rows(i).Cells(3).Value
                                                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(3).Value
                                                            Else
                                                                totalRecibosMN += Me.dgvClientes.Rows(i).Cells(2).Value
                                                                matrizRecibos(x, 1) = Me.dgvClientes.Rows(i).Cells(2).Value
                                                            End If
                                                            x += 1
                                                        Else
                                                            Dim y As Integer
                                                            MsgBox("No se permite elegir recibos 'Otros Pagos' en diferentes monedas  !  !  !", MsgBoxStyle.Critical)
                                                            For y = matrizRecibos.GetLowerBound(0) To matrizRecibos.GetUpperBound(0)
                                                                matrizRecibos(y, 0) = ""
                                                                matrizRecibos(y, 1) = ""
                                                            Next y
                                                            totalRecibosME = 0
                                                            totalRecibosMN = 0
                                                            Exit Sub
                                                        End If
                                                    End If
                                                Next i
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.txtBuscaCliente.Text = ""
            Me.dgvClientes.Rows.Clear()
            Me.oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
    Private Sub dgvClientes_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClientes.CellValueChanged
        Try
            '---------- 10-08-15
            If (dgvClientes.Columns(e.ColumnIndex).Name = "status") And (numModulo <> 1 And numModulo <> 3) Then
                contador += 1
                If contador > 1 Then
                    Me.dgvClientes.ReadOnly = True
                    MsgBox("Concepto no permite elección multiple  !  !  !", MsgBoxStyle.Critical)
                    contador = 0
                    numModulo = 0
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvClientes_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvClientes.MouseEnter
        Me.lblMensaje.Text = "Escriba iniciales de apellido cliente. Haz check sobre uno de los grupos, el sistema" & _
                             " mostrará recibos que corresponda, luego sobre uno o varios de los recibos."
    End Sub
    Private Sub dgvClientes_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvClientes.MouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        numModulo = 0
        Me.Close()
    End Sub
End Class