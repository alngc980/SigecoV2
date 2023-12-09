<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaGrupo
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
        Me.dgvGrupoProducto = New System.Windows.Forms.DataGridView()
        CType(Me.dgvGrupoProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvGrupoProducto
        '
        Me.dgvGrupoProducto.AllowUserToAddRows = False
        Me.dgvGrupoProducto.AllowUserToDeleteRows = False
        Me.dgvGrupoProducto.AllowUserToResizeColumns = False
        Me.dgvGrupoProducto.AllowUserToResizeRows = False
        Me.dgvGrupoProducto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvGrupoProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGrupoProducto.Location = New System.Drawing.Point(3, 1)
        Me.dgvGrupoProducto.Name = "dgvGrupoProducto"
        Me.dgvGrupoProducto.ReadOnly = True
        Me.dgvGrupoProducto.RowHeadersVisible = False
        Me.dgvGrupoProducto.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvGrupoProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGrupoProducto.Size = New System.Drawing.Size(390, 269)
        Me.dgvGrupoProducto.TabIndex = 5
        '
        'frmbuscaGrupo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 271)
        Me.Controls.Add(Me.dgvGrupoProducto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmbuscaGrupo"
        Me.Text = "Módulo Búsqueda Grupos"
        CType(Me.dgvGrupoProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvGrupoProducto As System.Windows.Forms.DataGridView
End Class
