
Imports Microsoft
Imports System.Data.SqlClient
Imports Libreria
Imports System.IO

Public Class frmFactorComisionVisa

    Private Sub frmFactorComisionVisa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim datos As DataTable = RetornaDataTable("select * from FactorComisionVisa")
        If datos.Rows.Count > 0 Then
            txtFactor.Text = datos.Rows(0)(0).ToString()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sqlString As String
        Dim listaSqlStringsVenta As New ArrayList
        sqlString = "update FactorComisionVisa set nFactor = " + txtFactor.Text

        listaSqlStringsVenta.Add(sqlString)

        transaccionLetras(listaSqlStringsVenta)
    End Sub
End Class