<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaVendedor
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
        Me.dgvVendedores = New System.Windows.Forms.DataGridView()
        Me.txtBuscaVendedor = New System.Windows.Forms.TextBox()
        CType(Me.dgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Nombre Vendedor:"
        '
        'dgvVendedores
        '
        Me.dgvVendedores.AllowUserToAddRows = False
        Me.dgvVendedores.AllowUserToDeleteRows = False
        Me.dgvVendedores.AllowUserToResizeColumns = False
        Me.dgvVendedores.AllowUserToResizeRows = False
        Me.dgvVendedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVendedores.Location = New System.Drawing.Point(1, 29)
        Me.dgvVendedores.Name = "dgvVendedores"
        Me.dgvVendedores.ReadOnly = True
        Me.dgvVendedores.RowHeadersVisible = False
        Me.dgvVendedores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvVendedores.Size = New System.Drawing.Size(790, 338)
        Me.dgvVendedores.TabIndex = 8
        '
        'txtBuscaVendedor
        '
        Me.txtBuscaVendedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscaVendedor.Location = New System.Drawing.Point(132, 4)
        Me.txtBuscaVendedor.Name = "txtBuscaVendedor"
        Me.txtBuscaVendedor.Size = New System.Drawing.Size(659, 20)
        Me.txtBuscaVendedor.TabIndex = 7
        '
        'frmbuscaVendedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 367)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvVendedores)
        Me.Controls.Add(Me.txtBuscaVendedor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmbuscaVendedor"
        Me.Text = "Módulo Búsqueda Vendedor"
        CType(Me.dgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvVendedores As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscaVendedor As System.Windows.Forms.TextBox
End Class
