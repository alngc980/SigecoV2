Imports System.Drawing
Imports System.Drawing.Printing
Public Class frmconfigurarPapel
    Private Sub btnSeleccionarImpresora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionarImpresora.Click
        Try
            prdImpresoras.PrinterSettings = ImpresoraActual
            If prdImpresoras.ShowDialog = DialogResult.OK Then
                ImpresoraActual = prdImpresoras.PrinterSettings
                lblImpresoraActual.Text = ImpresoraActual.PrinterName
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class