Imports System.Data.SqlClient
Public Class frmcostosFijos
    Private oDataSet As DataSet
    Dim item As Byte
    Private Sub frmTipoCambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daCostos As SqlDataAdapter = New SqlDataAdapter("SELECT  * from costosFijos", Connection)
            daCostos.Fill(oDataSet, "costosFijos")

            Dim daFactor As SqlDataAdapter = New SqlDataAdapter("SELECT  * from factorInteres", Connection)
            daFactor.Fill(oDataSet, "factor")
            Connection.Close()

            Me.dgvTipoCambio.DataSource = oDataSet
            Me.dgvTipoCambio.DataMember = "costosFijos"
            With Me.dgvTipoCambio
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).ReadOnly = True
                .Columns(0).Width = 50
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            Me.dgvFactor.DataSource = oDataSet
            Me.dgvFactor.DataMember = "factor"
            With Me.dgvFactor
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).ReadOnly = True
                .Columns(0).Width = 50
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(1).ReadOnly = True
                .Columns(1).DisplayIndex = 4
                .Columns(2).DisplayIndex = 1
                .Columns(3).DisplayIndex = 2
                .Columns(4).DisplayIndex = 3
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Connection.Close()
        End Try
    End Sub
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim sqlString As String = ""
            Dim listaSqlStrings As New ArrayList

            If MsgBox("Está seguro de modificar costos fijos?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                For i As Integer = 0 To dgvTipoCambio.Rows.Count - 1
                    sqlString = "UPDATE costosFijos Set numLimite=" & CInt(dgvTipoCambio.Rows(i).Cells(1).Value) & ",numLimite_1 =" & _
                    CInt(dgvTipoCambio.Rows(i).Cells(2).Value) & ",monFijo=" & CDec(dgvTipoCambio.Rows(i).Cells(3).Value) & _
                    " where idCosto=" & CInt(dgvTipoCambio.Rows(i).Cells(0).Value) & ""
                    listaSqlStrings.Add(sqlString)
                Next

                sqlString = "UPDATE factorInteres Set factor=" & CDec(dgvFactor.Rows(0).Cells(1).Value) & ",ic=" & _
                CDec(dgvFactor.Rows(0).Cells(2).Value) & ",im=" & CDec(dgvFactor.Rows(0).Cells(3).Value) & ",cc=" & _
                CDec(dgvFactor.Rows(0).Cells(4).Value) & ""
                listaSqlStrings.Add(sqlString)

                If transaccionProducto(listaSqlStrings) Then
                    MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
                    flag = 0
                    Me.Close()
                Else
                    MsgBox("La Información no se procesó correctamente.", MsgBoxStyle.Critical)
                    Me.Close()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub dgvTipoCambio_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvTipoCambio.EditingControlShowing, dgvFactor.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvTipoCambio.CurrentCell.ColumnIndex
        Dim col As Integer = dgvFactor.CurrentCell.ColumnIndex

        If columna = 1 Or columna = 2 Or columna = 3 Or col = 2 Or col = 3 Or col = 4 Then
            Dim letra As Short = CShort(Asc(e.KeyChar))

            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvFactor_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvFactor.CellMouseClick
        Me.dgvFactor.Rows(0).Cells(1).Value = Format((Me.dgvFactor.Rows(0).Cells(2).Value + Me.dgvFactor.Rows(0).Cells(3).Value + Me.dgvFactor.Rows(0).Cells(4).Value) / 30 / 100, "0.000000")
    End Sub
    Private Sub dgvFactor_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFactor.CellMouseEnter
        Me.lblMensaje.Text = "Después de modificar valor(es), haz click en cualquier celda para obtener nuevo factor."
    End Sub
    Private Sub dgvFactor_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFactor.CellMouseLeave
        Me.lblMensaje.Text = ""
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class