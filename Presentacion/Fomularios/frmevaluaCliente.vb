Imports System.Data.SqlClient
Public Class frmevaluaCliente
    Private oDataSet As DataSet
    Private arrayLetras(5, 3) As String
    Private Sub txtCodigo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.DoubleClick
        arrayDatos(0) = ""
        frmbuscaCliente.ShowDialog()
        If arrayDatos(0) <> "" Then
            Me.txtCodigo.Text = arrayDatos(0)
            arrayDatos(0) = ""
        End If
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim x As Integer
        Dim daLetras As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM letrasClientes where idCliente=" & Me.txtCodigo.Text & "", Connection)
        oDataSet = New DataSet()

        Try
            Connection.Open()
            daLetras.Fill(oDataSet, "letras")
            Connection.Close()

            If Me.oDataSet.Tables(0).Rows.Count <= 0 Then
                MsgBox("No tiene registrado información.", MsgBoxStyle.Information)
                Exit Sub
            End If

            'Me.dgvLetras.DataSource = oDataSet
            'Me.dgvLetras.DataMember = "letras"

            If (Me.oDataSet.Tables(0).Rows.Count > 6) Then
                For i As Integer = Me.oDataSet.Tables(0).Rows.Count - 1 To 0 Step -1
                    arrayLetras(x, 0) = Me.oDataSet.Tables(0).Rows(i).Item(0)
                    arrayLetras(x, 1) = Me.oDataSet.Tables(0).Rows(i).Item(3)
                    arrayLetras(x, 2) = Me.oDataSet.Tables(0).Rows(i).Item(7)
                    arrayLetras(x, 3) = Me.oDataSet.Tables(0).Rows(i).Item(8)
                    x += 1
                    If x = 6 Then Exit For
                Next
            Else
                For i As Integer = Me.oDataSet.Tables(0).Rows.Count - 1 To 0 Step -1
                    arrayLetras(x, 0) = Me.oDataSet.Tables(0).Rows(i).Item(0)
                    arrayLetras(x, 1) = Me.oDataSet.Tables(0).Rows(i).Item(3)
                    arrayLetras(x, 2) = Me.oDataSet.Tables(0).Rows(i).Item(7)
                    arrayLetras(x, 3) = Me.oDataSet.Tables(0).Rows(i).Item(8)
                    x += 1
                Next
            End If

            Dim letrasPagadasAntesIgualActualVcto As Integer
            Dim letrasPagadasAntesSgteVcto As Integer
            Dim letrasNoPagadas As Integer
            Dim percentLetrasNoPagadas As Decimal
            Dim registrosAProcesar As Integer
            Dim rangoFechas As Integer

            If (Me.oDataSet.Tables(0).Rows.Count > 1) Then rangoFechas = DateDiff(DateInterval.Day, CDate(arrayLetras(1, 2)), CDate(arrayLetras(0, 2)))

            If (Me.oDataSet.Tables(0).Rows.Count > 6) Then
                registrosAProcesar = 6
            Else
                registrosAProcesar = Me.oDataSet.Tables(0).Rows.Count
            End If

            For i As Integer = registrosAProcesar - 1 To 0 Step -1
                Dim fechaVcto, fechaPago As Date
                fechaVcto = CDate(arrayLetras(i, 2))
                fechaPago = CDate(arrayLetras(i, 3))

                If fechaPago <> CDate("01-01-1900") Then
                    If fechaPago <= fechaVcto Then
                        letrasPagadasAntesIgualActualVcto += 1
                    Else
                        If fechaPago > fechaVcto Or fechaPago <= fechaVcto.AddDays(rangoFechas) Then
                            letrasPagadasAntesSgteVcto += 1
                        End If
                    End If
                Else
                    letrasNoPagadas += 1
                End If
            Next

            percentLetrasNoPagadas = Math.Round(letrasNoPagadas * 100 / registrosAProcesar, 2)

            If letrasPagadasAntesIgualActualVcto = registrosAProcesar Then
                MsgBox("Cliente Excelente.", MsgBoxStyle.Information)
            Else
                If (letrasPagadasAntesIgualActualVcto + letrasPagadasAntesSgteVcto + letrasNoPagadas) = registrosAProcesar And percentLetrasNoPagadas <= 33.33 Then
                    MsgBox("Cliente Bueno.", MsgBoxStyle.Information)
                Else
                    If percentLetrasNoPagadas > 33.33 And letrasNoPagadas < registrosAProcesar Then
                        MsgBox("Cliente Regular.", MsgBoxStyle.Information)
                    Else
                        MsgBox("Cliente Malo.", MsgBoxStyle.Information)
                    End If
                End If
            End If

            MsgBox("La cantidad de letras pagadas antes o hasta la fecha de vcto.: " & letrasPagadasAntesIgualActualVcto)
            MsgBox("La cantidad de letras pagadas hasta antes del vcto. de la sgte. letra: " & letrasPagadasAntesSgteVcto)
            MsgBox("La cantidad de letras impagas por el cliente: " & letrasNoPagadas)
            'MsgBox("La direfencia de letras es: " & rangoFechas)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class