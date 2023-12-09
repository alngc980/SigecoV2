Imports System.Data.SqlClient
Public Class frmtasasCredito
    Private oDataSet As DataSet
    Private Sub frmtasasCredito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            oDataSet = New DataSet()
            Connection.Open()
            Dim daCTaCte As SqlDataAdapter = New SqlDataAdapter("SELECT  * from tasasCreditos", Connection)
            daCTaCte.Fill(oDataSet, "ctaCorriente")
            Connection.Close()

            Me.dgvTasasCredito.DataSource = oDataSet
            Me.dgvTasasCredito.DataMember = "ctaCorriente"
            With Me.dgvTasasCredito
                .Columns(0).ReadOnly = True
                .Columns(1).ReadOnly = False
                .Columns(2).ReadOnly = False
                .Columns(3).ReadOnly = False
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

            If MsgBox("Está seguro de modificar tipo cambio?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim oFrmAcceso As New frmaccesoAdministrador()
                oFrmAcceso.ShowDialog()
                If flag <> 1 Then
                    Exit Sub
                End If

                For i As Integer = 0 To dgvTasasCredito.Rows.Count - 1
                    SqlString = "UPDATE tasasCreditos Set tasInt_1='" & dgvTasasCredito.Rows(i).Cells(1).Value & "',tasInt_2 ='" & _
                    dgvTasasCredito.Rows(i).Cells(2).Value & "',tasInt_3='" & dgvTasasCredito.Rows(i).Cells(3).Value & _
                    "', tasInt_4='" & dgvTasasCredito.Rows(i).Cells(4).Value & "'where numMes= '" & dgvTasasCredito.Rows(i).Cells(0).Value & "'"
                    ListSqlStrings.Add(SqlString)
                Next
                If transaccionProducto(ListSqlStrings) Then
                    MsgBox("Información modificada correctamente.", MsgBoxStyle.Information)
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
    Private Sub dgvTasasCredito_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvTasasCredito.EditingControlShowing
        Dim validar As TextBox = CType(e.Control, TextBox)
        AddHandler validar.KeyPress, AddressOf validar_Keypress
    End Sub
    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim columna As Integer = dgvTasasCredito.CurrentCell.ColumnIndex
        If columna = 1 Or columna = 2 Or columna = 3 Or columna = 4 Then
            'Dim caracter As Char = e.KeyChar
            'If Not Char.IsDigit(caracter) And (caracter = ChrW(Keys.Back)) = False Then
            '    e.KeyChar = Chr(0)
            'End If

            Dim letra As Short = CShort(Asc(e.KeyChar))

            letra = CShort(Validar_Numeros(letra))
            If letra = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class