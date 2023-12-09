<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaGarante
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
        Me.dgvGarantes = New System.Windows.Forms.DataGridView()
        Me.txtBuscaGarante = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvGarantes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvGarantes
        '
        Me.dgvGarantes.AllowUserToAddRows = False
        Me.dgvGarantes.AllowUserToDeleteRows = False
        Me.dgvGarantes.AllowUserToResizeRows = False
        Me.dgvGarantes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvGarantes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvGarantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGarantes.Location = New System.Drawing.Point(1, 37)
        Me.dgvGarantes.Name = "dgvGarantes"
        Me.dgvGarantes.ReadOnly = True
        Me.dgvGarantes.RowHeadersVisible = False
        Me.dgvGarantes.Size = New System.Drawing.Size(790, 331)
        Me.dgvGarantes.TabIndex = 6
        '
        'txtBuscaGarante
        '
        Me.txtBuscaGarante.BackColor = System.Drawing.Color.White
        Me.txtBuscaGarante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscaGarante.Location = New System.Drawing.Point(120, 9)
        Me.txtBuscaGarante.Name = "txtBuscaGarante"
        Me.txtBuscaGarante.Size = New System.Drawing.Size(671, 20)
        Me.txtBuscaGarante.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Nombre Garante:"
        '
        'frmbuscaGarante
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 367)
        Me.Controls.Add(Me.dgvGarantes)
        Me.Controls.Add(Me.txtBuscaGarante)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmbuscaGarante"
        Me.Text = "Módulo Búsqueda Garantes"
        CType(Me.dgvGarantes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvGarantes As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscaGarante As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
