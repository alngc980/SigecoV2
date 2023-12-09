<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaAdelantos
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
        Me.dgvRecibos = New System.Windows.Forms.DataGridView()
        CType(Me.dgvRecibos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvRecibos
        '
        Me.dgvRecibos.AllowUserToAddRows = False
        Me.dgvRecibos.AllowUserToDeleteRows = False
        Me.dgvRecibos.AllowUserToResizeColumns = False
        Me.dgvRecibos.AllowUserToResizeRows = False
        Me.dgvRecibos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvRecibos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRecibos.Location = New System.Drawing.Point(1, 1)
        Me.dgvRecibos.Name = "dgvRecibos"
        Me.dgvRecibos.ReadOnly = True
        Me.dgvRecibos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvRecibos.Size = New System.Drawing.Size(590, 167)
        Me.dgvRecibos.TabIndex = 9
        '
        'frmbuscaAdelantos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 167)
        Me.Controls.Add(Me.dgvRecibos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmbuscaAdelantos"
        Me.Text = "Módulo Búsqueda Amortizaciones"
        CType(Me.dgvRecibos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvRecibos As System.Windows.Forms.DataGridView
End Class
