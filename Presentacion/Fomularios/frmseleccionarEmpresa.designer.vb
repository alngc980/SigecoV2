<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmseleccionarEmpresa
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
        Me.cbxEmpresa = New System.Windows.Forms.ComboBox()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.grbAcciones = New System.Windows.Forms.GroupBox()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnIngresar = New System.Windows.Forms.Button()
        Me.grbEmpresa.SuspendLayout()
        Me.grbAcciones.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbEmpresa
        '
        Me.grbEmpresa.Controls.Add(Me.cbxEmpresa)
        Me.grbEmpresa.Controls.Add(Me.lblEmpresa)
        Me.grbEmpresa.Location = New System.Drawing.Point(12, 12)
        Me.grbEmpresa.Name = "grbEmpresa"
        Me.grbEmpresa.Size = New System.Drawing.Size(465, 79)
        Me.grbEmpresa.TabIndex = 0
        Me.grbEmpresa.TabStop = False
        Me.grbEmpresa.Text = "Empresa"
        '
        'cbxEmpresa
        '
        Me.cbxEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxEmpresa.FormattingEnabled = True
        Me.cbxEmpresa.Location = New System.Drawing.Point(116, 31)
        Me.cbxEmpresa.Name = "cbxEmpresa"
        Me.cbxEmpresa.Size = New System.Drawing.Size(328, 21)
        Me.cbxEmpresa.TabIndex = 0
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoSize = True
        Me.lblEmpresa.Location = New System.Drawing.Point(20, 34)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(70, 13)
        Me.lblEmpresa.TabIndex = 0
        Me.lblEmpresa.Text = "Razon social:"
        '
        'grbAcciones
        '
        Me.grbAcciones.Controls.Add(Me.btnSalir)
        Me.grbAcciones.Controls.Add(Me.btnIngresar)
        Me.grbAcciones.Location = New System.Drawing.Point(12, 97)
        Me.grbAcciones.Name = "grbAcciones"
        Me.grbAcciones.Size = New System.Drawing.Size(465, 67)
        Me.grbAcciones.TabIndex = 1
        Me.grbAcciones.TabStop = False
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(273, 22)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 30)
        Me.btnSalir.TabIndex = 1
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnIngresar
        '
        Me.btnIngresar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnIngresar.Location = New System.Drawing.Point(92, 22)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(100, 30)
        Me.btnIngresar.TabIndex = 0
        Me.btnIngresar.Text = "Ingresar"
        Me.btnIngresar.UseVisualStyleBackColor = True
        '
        'frmseleccionarEmpresa
        '
        Me.AcceptButton = Me.btnIngresar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(489, 176)
        Me.ControlBox = False
        Me.Controls.Add(Me.grbAcciones)
        Me.Controls.Add(Me.grbEmpresa)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmseleccionarEmpresa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccionar Empresa"
        Me.grbEmpresa.ResumeLayout(False)
        Me.grbEmpresa.PerformLayout()
        Me.grbAcciones.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents grbEmpresa As System.Windows.Forms.GroupBox
    Friend WithEvents cbxEmpresa As System.Windows.Forms.ComboBox
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents grbAcciones As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
End Class
