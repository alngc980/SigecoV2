Imports System.Data.SqlClient
Imports System.Data.Odbc
Public Class frmExcelToSqlServer
    Private oDataSet As DataSet
    Dim oConexion As New SqlConnection()
    Dim oConexionOdbc As New OdbcConnection()
    Private Sub btnVisualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
        Dim SqlString As String = ""
        Dim ListSqlStrings As New ArrayList

        oConexionOdbc.ConnectionString = "Dsn=Excel Files;dbq=D:\EXCEL_COMERORI\precios.xlsx;defaultdir=D:\EXCEL_COMERORI;driverid=1046;fil=excel 12.0;maxbuffersize=2048;pagetimeout=5"
        oConexion.ConnectionString = "Data Source=Servidor;Database=COMERORI;Persist Security Info=True;User ID=sa;password=123"

        Try
            Dim oDataSet As New DataSet()
            oConexionOdbc.Open()
            Dim daProductos As OdbcDataAdapter = New OdbcDataAdapter("select * from [precios$]", oConexionOdbc)
            daProductos.Fill(oDataSet, "Hoja")
            oConexionOdbc.Close()

            Me.dgvProductos.DataSource = oDataSet
            Me.dgvProductos.DataMember = "Hoja"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            oConexionOdbc.Close()
            oConexion.Close()
        End Try
    End Sub
    Private Sub btnInsertar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertar.Click
        Dim sqlString As String = ""
        Dim listaSqlStrings As New ArrayList
        Dim daProductos As SqlDataAdapter
        Dim oDataTable As DataTable

        daProductos = New SqlDataAdapter("select * from productos", Connection)
        odatatable = New DataTable
        daProductos.Fill(oDataTable)

        If oDataTable.Rows.Count > 9 Then
            MsgBox("Procedimiento incorrecto, productos ya fueron importados de hoja Excel  !  !  !", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try
            For i As Integer = 10 To 123
                If dgvProductos.Rows(i).Cells(0).Value.ToString <> "" Then
                    sqlString = "insert into productos (idGrupo,desProducto,presentacion,marca,modelo,numSerie,preContado,preCredito,preTarjeta," & _
                    "preTarjetaOferta,preTarjetaRemate,preOferta,preRemate,stoInicial,afeIGV,status) VALUES (1,'" & Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(1).Value, 250) & "','UND','" & _
                    Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(2).Value, 15) & "','" & Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(3).Value, 20) & "','S'," & _
                    dgvProductos.Rows(i).Cells(4).Value & "," & dgvProductos.Rows(i).Cells(5).Value & "," & dgvProductos.Rows(i).Cells(6).Value & "," & dgvProductos.Rows(i).Cells(7).Value & "," & _
                    dgvProductos.Rows(i).Cells(8).Value & "," & dgvProductos.Rows(i).Cells(9).Value & "," & dgvProductos.Rows(i).Cells(10).Value & ",0,'N','')"

                    listaSqlStrings.Add(sqlString)
                End If
            Next

            If ejecutarTransaccion(listaSqlStrings) Then
                MsgBox("Lista productos insertados correctamente en BBDD  !  !  !", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("Procedimiento inserción productos en BBDD ha fallado  !  !  !", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnActualizar_Click(sender As System.Object, e As System.EventArgs) Handles btnActualizar.Click
        Dim sqlString As String = ""
        Dim listaSqlStrings As New ArrayList
        Dim daProductos As SqlDataAdapter
        Dim oDataTable As DataTable

        daProductos = New SqlDataAdapter("select * from productos", Connection)
        oDataTable = New DataTable
        daProductos.Fill(oDataTable)

        Try
            For i As Integer = 0 To oDataTable.Rows.Count - 6
                sqlString = "update productos set desProducto='" & Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(2).Value, 250) & _
                                                              "',marca='" & Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(4).Value, 15) & _
                                                              "',modelo='" & Microsoft.VisualBasic.Left(dgvProductos.Rows(i).Cells(5).Value, 20) & _
                                                              "',preContado=" & dgvProductos.Rows(i).Cells(7).Value & _
                                                              ",preCredito=" & dgvProductos.Rows(i).Cells(8).Value & _
                                                              ",preTarjeta=" & dgvProductos.Rows(i).Cells(9).Value & _
                                                              ",preTarjetaOferta=" & dgvProductos.Rows(i).Cells(10).Value & _
                                                              ",preTarjetaRemate=" & dgvProductos.Rows(i).Cells(11).Value & _
                                                              ",preOferta=" & dgvProductos.Rows(i).Cells(12).Value & _
                                                              ",preRemate=" & dgvProductos.Rows(i).Cells(13).Value & " where idProducto='" & dgvProductos.Rows(i).Cells(0).Value & "'"
                listaSqlStrings.Add(sqlString)
            Next

            If ejecutarTransaccion(listaSqlStrings) Then
                MsgBox("Lista productos actualizados correctamente en BBDD  !  !  !", MsgBoxStyle.Information)
                Me.Close()
            Else
                MsgBox("Procedimiento actualización productos en BBDD ha fallado  !  !  !", MsgBoxStyle.Critical)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub frmExcelToSqlServer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class