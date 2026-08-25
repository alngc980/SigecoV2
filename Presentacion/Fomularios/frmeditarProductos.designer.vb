<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmeditarProductos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeditarProductos))
        Me.dgvProductos = New System.Windows.Forms.DataGridView()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnGenerarExcel = New System.Windows.Forms.Button()
        Me.txtTexto = New System.Windows.Forms.TextBox()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.btnGenerarPDF = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtProducto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToResizeColumns = False
        Me.dgvProductos.AllowUserToResizeRows = False
        Me.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductos.Location = New System.Drawing.Point(3, 44)
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.RowHeadersVisible = False
        Me.dgvProductos.Size = New System.Drawing.Size(1087, 360)
        Me.dgvProductos.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnGenerarExcel)
        Me.GroupBox1.Controls.Add(Me.txtTexto)
        Me.GroupBox1.Controls.Add(Me.btnEliminar)
        Me.GroupBox1.Controls.Add(Me.btnGrabar)
        Me.GroupBox1.Controls.Add(Me.btnGenerarPDF)
        Me.GroupBox1.Controls.Add(Me.btnSalir)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 421)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1088, 65)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'btnGenerarExcel
        '
        Me.btnGenerarExcel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGenerarExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerarExcel.Image = Global.Presentacion.My.Resources.Resources.excel
        Me.btnGenerarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenerarExcel.Location = New System.Drawing.Point(483, 10)
        Me.btnGenerarExcel.Name = "btnGenerarExcel"
        Me.btnGenerarExcel.Size = New System.Drawing.Size(100, 50)
        Me.btnGenerarExcel.TabIndex = 57
        Me.btnGenerarExcel.Text = "Generar Excel"
        Me.btnGenerarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarExcel.UseVisualStyleBackColor = True
        '
        'txtTexto
        '
        Me.txtTexto.Location = New System.Drawing.Point(1000, 50)
        Me.txtTexto.Multiline = True
        Me.txtTexto.Name = "txtTexto"
        Me.txtTexto.Size = New System.Drawing.Size(1, 1)
        Me.txtTexto.TabIndex = 56
        '
        'btnEliminar
        '
        Me.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Image = Global.Presentacion.My.Resources.Resources.DeleteHS
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEliminar.Location = New System.Drawing.Point(639, 10)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(100, 50)
        Me.btnEliminar.TabIndex = 7
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnGrabar
        '
        Me.btnGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrabar.Image = Global.Presentacion.My.Resources.Resources.document_save
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGrabar.Location = New System.Drawing.Point(172, 10)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(100, 50)
        Me.btnGrabar.TabIndex = 55
        Me.btnGrabar.Text = "Grabar"
        Me.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'btnGenerarPDF
        '
        Me.btnGenerarPDF.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGenerarPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerarPDF.Image = Global.Presentacion.My.Resources.Resources.iconoPDF
        Me.btnGenerarPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenerarPDF.Location = New System.Drawing.Point(328, 10)
        Me.btnGenerarPDF.Name = "btnGenerarPDF"
        Me.btnGenerarPDF.Size = New System.Drawing.Size(100, 50)
        Me.btnGenerarPDF.TabIndex = 6
        Me.btnGenerarPDF.Text = "Generar PDF"
        Me.btnGenerarPDF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarPDF.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = Global.Presentacion.My.Resources.Resources.salir
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(791, 10)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 50)
        Me.btnSalir.TabIndex = 8
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.Location = New System.Drawing.Point(5, 407)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(0, 16)
        Me.lblMensaje.TabIndex = 22
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtProducto)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(2, -4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1088, 45)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'txtProducto
        '
        Me.txtProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducto.Location = New System.Drawing.Point(172, 15)
        Me.txtProducto.MaxLength = 40
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(910, 20)
        Me.txtProducto.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Descripción producto:"
        '
        'frmeditarProductos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(1090, 487)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.dgvProductos)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmeditarProductos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo Editar Datos Productos"
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnGenerarPDF As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTexto As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerarExcel As System.Windows.Forms.Button
End Class
