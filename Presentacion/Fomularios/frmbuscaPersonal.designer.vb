<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaPersonal
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvPersonal = New System.Windows.Forms.DataGridView()
        Me.txtBuscaTrabajador = New System.Windows.Forms.TextBox()
        CType(Me.dgvPersonal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 16)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Nombre Trabajador:"
        '
        'dgvPersonal
        '
        Me.dgvPersonal.AllowUserToAddRows = False
        Me.dgvPersonal.AllowUserToDeleteRows = False
        Me.dgvPersonal.AllowUserToResizeColumns = False
        Me.dgvPersonal.AllowUserToResizeRows = False
        Me.dgvPersonal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvPersonal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPersonal.Location = New System.Drawing.Point(1, 28)
        Me.dgvPersonal.Name = "dgvPersonal"
        Me.dgvPersonal.ReadOnly = True
        Me.dgvPersonal.RowHeadersVisible = False
        Me.dgvPersonal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvPersonal.Size = New System.Drawing.Size(790, 339)
        Me.dgvPersonal.TabIndex = 11
        '
        'txtBuscaTrabajador
        '
        Me.txtBuscaTrabajador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscaTrabajador.Location = New System.Drawing.Point(140, 3)
        Me.txtBuscaTrabajador.Name = "txtBuscaTrabajador"
        Me.txtBuscaTrabajador.Size = New System.Drawing.Size(651, 20)
        Me.txtBuscaTrabajador.TabIndex = 10
        '
        'frmbuscaPersonal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 367)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvPersonal)
        Me.Controls.Add(Me.txtBuscaTrabajador)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmbuscaPersonal"
        Me.Text = "Módulo Búsqueda Personal Empresa"
        CType(Me.dgvPersonal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvPersonal As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscaTrabajador As System.Windows.Forms.TextBox
End Class
