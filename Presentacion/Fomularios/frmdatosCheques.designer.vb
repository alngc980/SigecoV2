<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdatosCheques
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTipoMoneda1 = New System.Windows.Forms.Label()
        Me.lblTipoMoneda = New System.Windows.Forms.Label()
        Me.cbxTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMontoCambioCheque = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMontoCheque = New System.Windows.Forms.TextBox()
        Me.txtNumCheque = New System.Windows.Forms.TextBox()
        Me.cbxNombreBanco = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCancelar)
        Me.GroupBox1.Controls.Add(Me.btnAceptar)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 158)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(392, 60)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Image = My.Resources.DeleteHS
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.Location = New System.Drawing.Point(222, 13)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(80, 40)
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = My.Resources.CheckSpellingHS
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptar.Location = New System.Drawing.Point(77, 13)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(80, 40)
        Me.btnAceptar.TabIndex = 7
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbxNombreBanco)
        Me.GroupBox2.Controls.Add(Me.lblTipoMoneda1)
        Me.GroupBox2.Controls.Add(Me.lblTipoMoneda)
        Me.GroupBox2.Controls.Add(Me.cbxTipoMoneda)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtMontoCambioCheque)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtMontoCheque)
        Me.GroupBox2.Controls.Add(Me.txtNumCheque)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(1, -2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(392, 154)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos Movimiento"
        '
        'lblTipoMoneda1
        '
        Me.lblTipoMoneda1.AutoSize = True
        Me.lblTipoMoneda1.Location = New System.Drawing.Point(104, 79)
        Me.lblTipoMoneda1.Name = "lblTipoMoneda1"
        Me.lblTipoMoneda1.Size = New System.Drawing.Size(0, 16)
        Me.lblTipoMoneda1.TabIndex = 62
        '
        'lblTipoMoneda
        '
        Me.lblTipoMoneda.AutoSize = True
        Me.lblTipoMoneda.Location = New System.Drawing.Point(93, 109)
        Me.lblTipoMoneda.Name = "lblTipoMoneda"
        Me.lblTipoMoneda.Size = New System.Drawing.Size(0, 16)
        Me.lblTipoMoneda.TabIndex = 61
        '
        'cbxTipoMoneda
        '
        Me.cbxTipoMoneda.FormattingEnabled = True
        Me.cbxTipoMoneda.Items.AddRange(New Object() {"Soles", "Dolares"})
        Me.cbxTipoMoneda.Location = New System.Drawing.Point(275, 75)
        Me.cbxTipoMoneda.Name = "cbxTipoMoneda"
        Me.cbxTipoMoneda.Size = New System.Drawing.Size(112, 24)
        Me.cbxTipoMoneda.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 16)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Nombre Banco:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Monto Cambio"
        '
        'txtMontoCambioCheque
        '
        Me.txtMontoCambioCheque.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoCambioCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoCambioCheque.Location = New System.Drawing.Point(165, 105)
        Me.txtMontoCambioCheque.MaxLength = 10
        Me.txtMontoCambioCheque.Multiline = True
        Me.txtMontoCambioCheque.Name = "txtMontoCambioCheque"
        Me.txtMontoCambioCheque.ReadOnly = True
        Me.txtMontoCambioCheque.Size = New System.Drawing.Size(104, 24)
        Me.txtMontoCambioCheque.TabIndex = 5
        Me.txtMontoCambioCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 16)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Monto Procesar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 16)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Número Cheque/Tarjeta:"
        '
        'txtMontoCheque
        '
        Me.txtMontoCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoCheque.Location = New System.Drawing.Point(165, 75)
        Me.txtMontoCheque.MaxLength = 10
        Me.txtMontoCheque.Multiline = True
        Me.txtMontoCheque.Name = "txtMontoCheque"
        Me.txtMontoCheque.Size = New System.Drawing.Size(104, 24)
        Me.txtMontoCheque.TabIndex = 3
        Me.txtMontoCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNumCheque
        '
        Me.txtNumCheque.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNumCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumCheque.Location = New System.Drawing.Point(165, 45)
        Me.txtNumCheque.MaxLength = 25
        Me.txtNumCheque.Multiline = True
        Me.txtNumCheque.Name = "txtNumCheque"
        Me.txtNumCheque.Size = New System.Drawing.Size(222, 24)
        Me.txtNumCheque.TabIndex = 2
        '
        'cbxNombreBanco
        '
        Me.cbxNombreBanco.FormattingEnabled = True
        Me.cbxNombreBanco.Items.AddRange(New Object() {"BANCO AZTECA", "BANBIF", "BANCO CONTINENTAL", "BANCO DE COMERCIO", "BANCO DE CREDITO", "BANCO DE LA NACION", "BANCO SANTANDER", "CAJA MAYNAS", "INTERBANK", "SCOTIANBANK"})
        Me.cbxNombreBanco.Location = New System.Drawing.Point(165, 11)
        Me.cbxNombreBanco.Name = "cbxNombreBanco"
        Me.cbxNombreBanco.Size = New System.Drawing.Size(222, 24)
        Me.cbxNombreBanco.TabIndex = 63
        '
        'frmdatosCheques
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(394, 221)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmdatosCheques"
        Me.Text = "Módulo Datos de Cheques y Montos "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbxTipoMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMontoCambioCheque As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMontoCheque As System.Windows.Forms.TextBox
    Friend WithEvents txtNumCheque As System.Windows.Forms.TextBox
    Friend WithEvents lblTipoMoneda1 As System.Windows.Forms.Label
    Friend WithEvents lblTipoMoneda As System.Windows.Forms.Label
    Friend WithEvents cbxNombreBanco As System.Windows.Forms.ComboBox
End Class
