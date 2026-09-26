<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmconfiguracionFacturador
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grbConfiguracion = New System.Windows.Forms.GroupBox()
        Me.lblAyuda = New System.Windows.Forms.Label()
        Me.btnExaminar = New System.Windows.Forms.Button()
        Me.txtUrlServicio = New System.Windows.Forms.TextBox()
        Me.txtRutaBase = New System.Windows.Forms.TextBox()
        Me.lblUrl = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.grbAcciones = New System.Windows.Forms.GroupBox()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.grbConfiguracion.SuspendLayout()
        Me.grbAcciones.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbConfiguracion
        '
        Me.grbConfiguracion.Controls.Add(Me.lblAyuda)
        Me.grbConfiguracion.Controls.Add(Me.btnExaminar)
        Me.grbConfiguracion.Controls.Add(Me.txtUrlServicio)
        Me.grbConfiguracion.Controls.Add(Me.txtRutaBase)
        Me.grbConfiguracion.Controls.Add(Me.lblUrl)
        Me.grbConfiguracion.Controls.Add(Me.lblRuta)
        Me.grbConfiguracion.Location = New System.Drawing.Point(12, 12)
        Me.grbConfiguracion.Name = "grbConfiguracion"
        Me.grbConfiguracion.Size = New System.Drawing.Size(620, 175)
        Me.grbConfiguracion.TabIndex = 0
        Me.grbConfiguracion.TabStop = False
        Me.grbConfiguracion.Text = "Datos del Facturador SUNAT"
        '
        'lblAyuda
        '
        Me.lblAyuda.AutoSize = True
        Me.lblAyuda.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAyuda.Location = New System.Drawing.Point(126, 55)
        Me.lblAyuda.Name = "lblAyuda"
        Me.lblAyuda.Size = New System.Drawing.Size(347, 13)
        Me.lblAyuda.TabIndex = 7
        Me.lblAyuda.Text = "La ruta base debe contener las carpetas data, repo, rpta, firma y envio."
        '
        'btnExaminar
        '
        Me.btnExaminar.Location = New System.Drawing.Point(527, 26)
        Me.btnExaminar.Name = "btnExaminar"
        Me.btnExaminar.Size = New System.Drawing.Size(75, 23)
        Me.btnExaminar.TabIndex = 1
        Me.btnExaminar.Text = "Examinar"
        Me.btnExaminar.UseVisualStyleBackColor = True
        '
        'txtUrlServicio
        '
        Me.txtUrlServicio.Location = New System.Drawing.Point(129, 83)
        Me.txtUrlServicio.MaxLength = 250
        Me.txtUrlServicio.Name = "txtUrlServicio"
        Me.txtUrlServicio.Size = New System.Drawing.Size(473, 20)
        Me.txtUrlServicio.TabIndex = 2
        '
        'txtRutaBase
        '
        Me.txtRutaBase.Location = New System.Drawing.Point(129, 28)
        Me.txtRutaBase.MaxLength = 500
        Me.txtRutaBase.Name = "txtRutaBase"
        Me.txtRutaBase.Size = New System.Drawing.Size(392, 20)
        Me.txtRutaBase.TabIndex = 0
        '
        'lblUrl
        '
        Me.lblUrl.AutoSize = True
        Me.lblUrl.Location = New System.Drawing.Point(24, 86)
        Me.lblUrl.Name = "lblUrl"
        Me.lblUrl.Size = New System.Drawing.Size(72, 13)
        Me.lblUrl.TabIndex = 1
        Me.lblUrl.Text = "URL servicio:"
        '
        'lblRuta
        '
        Me.lblRuta.AutoSize = True
        Me.lblRuta.Location = New System.Drawing.Point(24, 31)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(56, 13)
        Me.lblRuta.TabIndex = 0
        Me.lblRuta.Text = "Ruta base:"
        '
        'grbAcciones
        '
        Me.grbAcciones.Controls.Add(Me.btnSalir)
        Me.grbAcciones.Controls.Add(Me.btnGrabar)
        Me.grbAcciones.Location = New System.Drawing.Point(12, 193)
        Me.grbAcciones.Name = "grbAcciones"
        Me.grbAcciones.Size = New System.Drawing.Size(620, 68)
        Me.grbAcciones.TabIndex = 1
        Me.grbAcciones.TabStop = False
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(344, 22)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(105, 30)
        Me.btnSalir.TabIndex = 1
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnGrabar
        '
        Me.btnGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnGrabar.Location = New System.Drawing.Point(170, 22)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(105, 30)
        Me.btnGrabar.TabIndex = 0
        Me.btnGrabar.Text = "Grabar"
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'frmconfiguracionFacturador
        '
        Me.AcceptButton = Me.btnGrabar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(644, 273)
        Me.ControlBox = False
        Me.Controls.Add(Me.grbAcciones)
        Me.Controls.Add(Me.grbConfiguracion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmconfiguracionFacturador"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuracion del Facturador SUNAT"
        Me.grbConfiguracion.ResumeLayout(False)
        Me.grbConfiguracion.PerformLayout()
        Me.grbAcciones.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents grbConfiguracion As System.Windows.Forms.GroupBox
    Friend WithEvents lblAyuda As System.Windows.Forms.Label
    Friend WithEvents btnExaminar As System.Windows.Forms.Button
    Friend WithEvents txtUrlServicio As System.Windows.Forms.TextBox
    Friend WithEvents txtRutaBase As System.Windows.Forms.TextBox
    Friend WithEvents lblUrl As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents grbAcciones As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
End Class
