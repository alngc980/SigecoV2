<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmconfiguracionEmpresa
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grbEmpresa = New System.Windows.Forms.GroupBox()
        Me.txtRuc = New System.Windows.Forms.TextBox()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtRazonSocial = New System.Windows.Forms.TextBox()
        Me.lblRuc = New System.Windows.Forms.Label()
        Me.lblTelefono = New System.Windows.Forms.Label()
        Me.lblDireccion = New System.Windows.Forms.Label()
        Me.lblRazonSocial = New System.Windows.Forms.Label()
        Me.grbAcciones = New System.Windows.Forms.GroupBox()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.grbEmpresa.SuspendLayout()
        Me.grbAcciones.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbEmpresa
        '
        Me.grbEmpresa.Controls.Add(Me.txtRuc)
        Me.grbEmpresa.Controls.Add(Me.txtTelefono)
        Me.grbEmpresa.Controls.Add(Me.txtDireccion)
        Me.grbEmpresa.Controls.Add(Me.txtRazonSocial)
        Me.grbEmpresa.Controls.Add(Me.lblRuc)
        Me.grbEmpresa.Controls.Add(Me.lblTelefono)
        Me.grbEmpresa.Controls.Add(Me.lblDireccion)
        Me.grbEmpresa.Controls.Add(Me.lblRazonSocial)
        Me.grbEmpresa.Location = New System.Drawing.Point(12, 12)
        Me.grbEmpresa.Name = "grbEmpresa"
        Me.grbEmpresa.Size = New System.Drawing.Size(620, 183)
        Me.grbEmpresa.TabIndex = 0
        Me.grbEmpresa.TabStop = False
        Me.grbEmpresa.Text = "Datos de la Empresa"
        '
        'txtRuc
        '
        Me.txtRuc.Location = New System.Drawing.Point(126, 139)
        Me.txtRuc.MaxLength = 11
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.Size = New System.Drawing.Size(160, 20)
        Me.txtRuc.TabIndex = 3
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(126, 104)
        Me.txtTelefono.MaxLength = 50
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(220, 20)
        Me.txtTelefono.TabIndex = 2
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(126, 69)
        Me.txtDireccion.MaxLength = 250
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(470, 20)
        Me.txtDireccion.TabIndex = 1
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.Location = New System.Drawing.Point(126, 34)
        Me.txtRazonSocial.MaxLength = 200
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.Size = New System.Drawing.Size(470, 20)
        Me.txtRazonSocial.TabIndex = 0
        '
        'lblRuc
        '
        Me.lblRuc.AutoSize = True
        Me.lblRuc.Location = New System.Drawing.Point(25, 142)
        Me.lblRuc.Name = "lblRuc"
        Me.lblRuc.Size = New System.Drawing.Size(33, 13)
        Me.lblRuc.TabIndex = 3
        Me.lblRuc.Text = "RUC:"
        '
        'lblTelefono
        '
        Me.lblTelefono.AutoSize = True
        Me.lblTelefono.Location = New System.Drawing.Point(25, 107)
        Me.lblTelefono.Name = "lblTelefono"
        Me.lblTelefono.Size = New System.Drawing.Size(52, 13)
        Me.lblTelefono.TabIndex = 2
        Me.lblTelefono.Text = "Telefono:"
        '
        'lblDireccion
        '
        Me.lblDireccion.AutoSize = True
        Me.lblDireccion.Location = New System.Drawing.Point(25, 72)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(55, 13)
        Me.lblDireccion.TabIndex = 1
        Me.lblDireccion.Text = "Direccion:"
        '
        'lblRazonSocial
        '
        Me.lblRazonSocial.AutoSize = True
        Me.lblRazonSocial.Location = New System.Drawing.Point(25, 37)
        Me.lblRazonSocial.Name = "lblRazonSocial"
        Me.lblRazonSocial.Size = New System.Drawing.Size(73, 13)
        Me.lblRazonSocial.TabIndex = 0
        Me.lblRazonSocial.Text = "Razon social:"
        '
        'grbAcciones
        '
        Me.grbAcciones.Controls.Add(Me.btnSalir)
        Me.grbAcciones.Controls.Add(Me.btnGrabar)
        Me.grbAcciones.Location = New System.Drawing.Point(12, 201)
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
        'frmconfiguracionEmpresa
        '
        Me.AcceptButton = Me.btnGrabar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(644, 281)
        Me.ControlBox = False
        Me.Controls.Add(Me.grbAcciones)
        Me.Controls.Add(Me.grbEmpresa)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmconfiguracionEmpresa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuracion de Empresa"
        Me.grbEmpresa.ResumeLayout(False)
        Me.grbEmpresa.PerformLayout()
        Me.grbAcciones.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents grbEmpresa As System.Windows.Forms.GroupBox
    Friend WithEvents txtRuc As System.Windows.Forms.TextBox
    Friend WithEvents txtTelefono As System.Windows.Forms.TextBox
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents txtRazonSocial As System.Windows.Forms.TextBox
    Friend WithEvents lblRuc As System.Windows.Forms.Label
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblRazonSocial As System.Windows.Forms.Label
    Friend WithEvents grbAcciones As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
End Class
