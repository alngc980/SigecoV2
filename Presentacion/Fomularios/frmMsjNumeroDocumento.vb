Public Class frmMsjNumeroDocumento
    Private Sub frmMsjNumeroDocumento_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
        Label3.Text = "Proceso de venta ha generado archivo plano de la  " & tipoDocumento & " n°  " & numeDocumento & "  y debe ser enviado individualmente  !  !  !"
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        btnAceptar_Click(sender, e)
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub
End Class