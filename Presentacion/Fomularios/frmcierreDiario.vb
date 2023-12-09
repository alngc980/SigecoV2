Imports System.Data.SqlClient
Public Class frmcierreDiario
    Private oDataSet As DataSet
    Private Sub frmcierreDiario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dtpFechaCierre.Text = devuelveFecha("SELECT * FROM cierreDiario")
    End Sub
    Private Sub btnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim Max As Long = 1000
        Dim z As Long

        If DateDiff(DateInterval.Day, CDate(dtpFechaCierre.Text), Now) <= 0 Then
            MsgBox("Por favor, cierre del día en curso ya se procesó.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim SqlString As String = ""
        Dim SqlString1 As String = ""
        Dim SqlString2 As String = ""
        Dim ListSqlStrings As New ArrayList

        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = Max
        ProgressBar1.Value = 1
        ProgressBar1.Step = 1

        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daCierre As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM cierreDiario", Connection)
            daCierre.Fill(oDataSet, "cierreDiario")

            Dim daSaldos As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM saldosAlmacenes where fechaSaldo='" & oDataSet.Tables(0).Rows(0).Item(0) & "'", Connection)
            daSaldos.Fill(oDataSet, "saldosAlmacenes")
            Connection.Close()

            For i As Integer = 0 To Me.oDataSet.Tables(1).Rows.Count - 1
                SqlString = "INSERT INTO saldosHistoricos (idProducto,stock,fechaSaldo) values ('" & oDataSet.Tables(1).Rows(i).Item(0) & "','" & _
                oDataSet.Tables(1).Rows(i).Item(1) & "','" & CDate(oDataSet.Tables(1).Rows(i).Item(2)).AddDays(1) & "')"
                ListSqlStrings.Add(SqlString)
            Next

            SqlString1 = "UPDATE saldosAlmacenes set fechaSaldo='" & CDate(oDataSet.Tables(0).Rows(0).Item(0)).AddDays(1) & "'"

            ListSqlStrings.Add(SqlString1)

            SqlString2 = "UPDATE cierreDiario set fechaCierre='" & CDate(oDataSet.Tables(0).Rows(0).Item(0)).AddDays(1) & "'"

            ListSqlStrings.Add(SqlString2)

            If transaccionLetras(ListSqlStrings) Then

                For z = 1 To Max
                    ProgressBar1.PerformStep()
                    Me.lblAvance.Text = Format(z / Max, "0%")
                    Application.DoEvents()
                Next z

                MsgBox("Proceso de cierre del día generada correctamente.", MsgBoxStyle.Information)
                Me.dtpFechaCierre.Text = devuelveFecha("SELECT * FROM cierreDiario")
            Else
                MsgBox("Error en proceso de cierre del día.", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class