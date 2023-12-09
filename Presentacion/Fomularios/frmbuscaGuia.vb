Imports System.Data.SqlClient
Public Class frmbuscaGuia
    Private oDataSet As DataSet
    Private Sub txtBuscaCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaCliente.KeyUp
        Me.dgvGuias.Rows.Clear()
        Try
            oDataSet = New DataSet()
            Dim daCliente As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM clientes where zona <> -1 and nombres Like '" & Me.txtBuscaCliente.Text & "%" & "' ", Connection)
            Connection.Open()
            daCliente.Fill(oDataSet, "cliente")

            Dim daGuiaCabecera As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM almCabecera where status<>'A' and tipDocumento<>'EN' and idCliente Like '" & oDataSet.Tables(0).Rows(0).Item(0) & "' ", Connection)
            daGuiaCabecera.Fill(oDataSet, "guiaCabecera")

            'For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
            '    Dim daGuiaDetalle As SqlDataAdapter = New SqlDataAdapter("SELECT *FROM almDetalle where status='0' and tipDocumento='" & oDataSet.Tables(1).Rows(i).Item(1) & "' and numDocumento='" & oDataSet.Tables(1).Rows(i).Item(2) & "' ", Connection)
            '    daGuiaDetalle.Fill(oDataSet, "guiaDetalle")
            'Next

            Connection.Close()

            Me.lblNombre.Text = oDataSet.Tables(0).Rows(0).Item(1)
            For i As Integer = 0 To oDataSet.Tables(1).Rows.Count() - 1
                Me.dgvGuias.Rows.Add()
                Me.dgvGuias.Rows(i).Cells(0).Value = oDataSet.Tables(1).Rows(i).Item(0).ToString
                Me.dgvGuias.Rows(i).Cells(1).Value = oDataSet.Tables(1).Rows(i).Item(1).ToString
                Me.dgvGuias.Rows(i).Cells(2).Value = oDataSet.Tables(1).Rows(i).Item(2).ToString
                Me.dgvGuias.Rows(i).Cells(3).Value = oDataSet.Tables(1).Rows(i).Item(3).ToString
                'Me.dgvGuias.Rows(i).Cells(4).Value = oDataSet.Tables(2).Rows(i).Item(4).ToString
                'Me.dgvGuias.Rows(i).Cells(5).Value = oDataSet.Tables(2).Rows(i).Item(3).ToString
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub dgvGuias_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGuias.CellDoubleClick
        Try
            With Me.dgvGuias
                arrayDatos(0) = .Item(2, e.RowIndex).Value
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.txtBuscaCliente.Clear()
            Me.txtBuscaCliente.Focus()
            oDataSet.Tables.Clear()
            Me.Close()
        End Try
    End Sub
End Class