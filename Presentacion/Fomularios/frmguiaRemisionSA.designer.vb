<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmguiaRemisionSA
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmguiaRemisionSA))
        Me.dgvProductos = New System.Windows.Forms.DataGridView
        Me.numItem = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.codProducto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.desProducto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.marca = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.modelo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.numSerie = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.numMotor = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.numChasis = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.eliminar = New System.Windows.Forms.DataGridViewButtonColumn
        Me.color = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grupo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnLimpiar = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSerie = New System.Windows.Forms.TextBox
        Me.txtNumDocumento = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblRUC = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnBuscaRecibo = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnAnular = New System.Windows.Forms.Button
        Me.btnGrabar = New System.Windows.Forms.Button
        Me.btnImprimir = New System.Windows.Forms.Button
        Me.btnProducto = New System.Windows.Forms.Button
        Me.txtDNIRUC = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtDireccion = New System.Windows.Forms.TextBox
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtpFecOrigen = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtTransportista = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.btnBuscarCliente = New System.Windows.Forms.Button
        Me.btnNuevoCliente = New System.Windows.Forms.Button
        Me.cbxTipoDocumento = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbxTipoMovimiento = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTransLlegada = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtDocCliente = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtDirCliente = New System.Windows.Forms.TextBox
        Me.txtNomCliente = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.dtpFecLlegada = New System.Windows.Forms.DateTimePicker
        Me.Label18 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblTelefono = New System.Windows.Forms.Label
        Me.lblDireccion = New System.Windows.Forms.Label
        Me.lblNombre = New System.Windows.Forms.Label
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToDeleteRows = False
        Me.dgvProductos.AllowUserToResizeColumns = False
        Me.dgvProductos.AllowUserToResizeRows = False
        Me.dgvProductos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvProductos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.numItem, Me.codProducto, Me.desProducto, Me.marca, Me.modelo, Me.numSerie, Me.numMotor, Me.numChasis, Me.cantidad, Me.eliminar, Me.color, Me.grupo})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvProductos.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvProductos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvProductos.Location = New System.Drawing.Point(0, 247)
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvProductos.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvProductos.Size = New System.Drawing.Size(921, 210)
        Me.dgvProductos.TabIndex = 40
        '
        'numItem
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = Nothing
        Me.numItem.DefaultCellStyle = DataGridViewCellStyle2
        Me.numItem.Frozen = True
        Me.numItem.HeaderText = "N° Item"
        Me.numItem.Name = "numItem"
        Me.numItem.ReadOnly = True
        Me.numItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.numItem.Width = 50
        '
        'codProducto
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.codProducto.DefaultCellStyle = DataGridViewCellStyle3
        Me.codProducto.Frozen = True
        Me.codProducto.HeaderText = "Cod. Prod."
        Me.codProducto.Name = "codProducto"
        Me.codProducto.ReadOnly = True
        Me.codProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.codProducto.Width = 70
        '
        'desProducto
        '
        Me.desProducto.Frozen = True
        Me.desProducto.HeaderText = "Descripción Producto"
        Me.desProducto.Name = "desProducto"
        Me.desProducto.ReadOnly = True
        Me.desProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.desProducto.Width = 160
        '
        'marca
        '
        Me.marca.Frozen = True
        Me.marca.HeaderText = "Marca"
        Me.marca.Name = "marca"
        Me.marca.ReadOnly = True
        Me.marca.Width = 80
        '
        'modelo
        '
        Me.modelo.Frozen = True
        Me.modelo.HeaderText = "Modelo"
        Me.modelo.Name = "modelo"
        Me.modelo.ReadOnly = True
        Me.modelo.Width = 80
        '
        'numSerie
        '
        Me.numSerie.Frozen = True
        Me.numSerie.HeaderText = "N° Serie"
        Me.numSerie.Name = "numSerie"
        Me.numSerie.ReadOnly = True
        '
        'numMotor
        '
        Me.numMotor.Frozen = True
        Me.numMotor.HeaderText = "N° Motor"
        Me.numMotor.Name = "numMotor"
        Me.numMotor.ReadOnly = True
        '
        'numChasis
        '
        Me.numChasis.Frozen = True
        Me.numChasis.HeaderText = "N° Chásis"
        Me.numChasis.Name = "numChasis"
        Me.numChasis.ReadOnly = True
        '
        'cantidad
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = Nothing
        Me.cantidad.DefaultCellStyle = DataGridViewCellStyle4
        Me.cantidad.Frozen = True
        Me.cantidad.HeaderText = "Cantidad"
        Me.cantidad.Name = "cantidad"
        Me.cantidad.ReadOnly = True
        Me.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cantidad.Width = 80
        '
        'eliminar
        '
        Me.eliminar.Frozen = True
        Me.eliminar.HeaderText = "Eliminar"
        Me.eliminar.Name = "eliminar"
        Me.eliminar.ReadOnly = True
        Me.eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.eliminar.ToolTipText = "Elimina item"
        Me.eliminar.Width = 50
        '
        'color
        '
        Me.color.HeaderText = "Color"
        Me.color.Name = "color"
        Me.color.ReadOnly = True
        Me.color.Visible = False
        '
        'grupo
        '
        Me.grupo.HeaderText = "Grupo"
        Me.grupo.Name = "grupo"
        Me.grupo.ReadOnly = True
        Me.grupo.Visible = False
        '
        'btnLimpiar
        '
        Me.btnLimpiar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = CType(resources.GetObject("btnLimpiar.Image"), System.Drawing.Image)
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiar.Location = New System.Drawing.Point(429, 28)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(100, 37)
        Me.btnLimpiar.TabIndex = 6
        Me.btnLimpiar.Text = "F12 Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(47, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(377, 25)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Guía Remisión/Parte Diario  Salida"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSerie)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtNumDocumento)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblRUC)
        Me.GroupBox2.Location = New System.Drawing.Point(405, -4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(516, 94)
        Me.GroupBox2.TabIndex = 38
        Me.GroupBox2.TabStop = False
        '
        'txtSerie
        '
        Me.txtSerie.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerie.Location = New System.Drawing.Point(353, 69)
        Me.txtSerie.MaxLength = 2
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(37, 20)
        Me.txtSerie.TabIndex = 14
        Me.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNumDocumento
        '
        Me.txtNumDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocumento.Location = New System.Drawing.Point(455, 68)
        Me.txtNumDocumento.MaxLength = 10
        Me.txtNumDocumento.Name = "txtNumDocumento"
        Me.txtNumDocumento.ReadOnly = True
        Me.txtNumDocumento.Size = New System.Drawing.Size(56, 20)
        Me.txtNumDocumento.TabIndex = 13
        Me.txtNumDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(396, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Número:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(304, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Serie:"
        '
        'lblRUC
        '
        Me.lblRUC.AutoSize = True
        Me.lblRUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRUC.Location = New System.Drawing.Point(138, 10)
        Me.lblRUC.Name = "lblRUC"
        Me.lblRUC.Size = New System.Drawing.Size(0, 25)
        Me.lblRUC.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnBuscaRecibo)
        Me.GroupBox5.Controls.Add(Me.btnLimpiar)
        Me.GroupBox5.Controls.Add(Me.btnSalir)
        Me.GroupBox5.Controls.Add(Me.btnAnular)
        Me.GroupBox5.Controls.Add(Me.btnGrabar)
        Me.GroupBox5.Controls.Add(Me.btnImprimir)
        Me.GroupBox5.Controls.Add(Me.btnProducto)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 463)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(647, 87)
        Me.GroupBox5.TabIndex = 43
        Me.GroupBox5.TabStop = False
        '
        'btnBuscaRecibo
        '
        Me.btnBuscaRecibo.BackColor = System.Drawing.Color.Transparent
        Me.btnBuscaRecibo.Location = New System.Drawing.Point(609, 89)
        Me.btnBuscaRecibo.Name = "btnBuscaRecibo"
        Me.btnBuscaRecibo.Size = New System.Drawing.Size(1, 1)
        Me.btnBuscaRecibo.TabIndex = 7
        Me.btnBuscaRecibo.UseVisualStyleBackColor = False
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(533, 28)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 37)
        Me.btnSalir.TabIndex = 5
        Me.btnSalir.Text = "[Esc] Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnAnular
        '
        Me.btnAnular.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnular.Enabled = False
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Image = CType(resources.GetObject("btnAnular.Image"), System.Drawing.Image)
        Me.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnular.Location = New System.Drawing.Point(221, 28)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(100, 37)
        Me.btnAnular.TabIndex = 4
        Me.btnAnular.Text = "F8 Anular"
        Me.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnular.UseVisualStyleBackColor = True
        '
        'btnGrabar
        '
        Me.btnGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGrabar.Location = New System.Drawing.Point(117, 28)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(100, 37)
        Me.btnGrabar.TabIndex = 2
        Me.btnGrabar.Text = "F4 Grabar"
        Me.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.Location = New System.Drawing.Point(325, 28)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(100, 37)
        Me.btnImprimir.TabIndex = 1
        Me.btnImprimir.Text = "F10 Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnProducto
        '
        Me.btnProducto.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProducto.Image = CType(resources.GetObject("btnProducto.Image"), System.Drawing.Image)
        Me.btnProducto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProducto.Location = New System.Drawing.Point(13, 28)
        Me.btnProducto.Name = "btnProducto"
        Me.btnProducto.Size = New System.Drawing.Size(100, 37)
        Me.btnProducto.TabIndex = 0
        Me.btnProducto.Text = "F2 Producto"
        Me.btnProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProducto.UseVisualStyleBackColor = True
        '
        'txtDNIRUC
        '
        Me.txtDNIRUC.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNIRUC.Location = New System.Drawing.Point(83, 69)
        Me.txtDNIRUC.MaxLength = 11
        Me.txtDNIRUC.Name = "txtDNIRUC"
        Me.txtDNIRUC.ReadOnly = True
        Me.txtDNIRUC.Size = New System.Drawing.Size(111, 20)
        Me.txtDNIRUC.TabIndex = 14
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 72)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 13)
        Me.Label23.TabIndex = 18
        Me.Label23.Text = "DNI/RUC:"
        '
        'txtDireccion
        '
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Location = New System.Drawing.Point(83, 43)
        Me.txtDireccion.MaxLength = 80
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.ReadOnly = True
        Me.txtDireccion.Size = New System.Drawing.Size(310, 20)
        Me.txtDireccion.TabIndex = 13
        '
        'txtNombre
        '
        Me.txtNombre.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombre.Location = New System.Drawing.Point(83, 18)
        Me.txtNombre.MaxLength = 80
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.ReadOnly = True
        Me.txtNombre.Size = New System.Drawing.Size(310, 20)
        Me.txtNombre.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Dirección:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Nombre:"
        '
        'dtpFecOrigen
        '
        Me.dtpFecOrigen.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecOrigen.Location = New System.Drawing.Point(83, 120)
        Me.dtpFecOrigen.Name = "dtpFecOrigen"
        Me.dtpFecOrigen.Size = New System.Drawing.Size(111, 20)
        Me.dtpFecOrigen.TabIndex = 15
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 126)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 13)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = "Fecha:"
        '
        'PrintDocument1
        '
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtTransportista)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtDNIRUC)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.txtDireccion)
        Me.GroupBox3.Controls.Add(Me.txtNombre)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.dtpFecOrigen)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 93)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(399, 148)
        Me.GroupBox3.TabIndex = 39
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Punto Partida: Datos Empresa"
        '
        'txtTransportista
        '
        Me.txtTransportista.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportista.Location = New System.Drawing.Point(83, 94)
        Me.txtTransportista.MaxLength = 80
        Me.txtTransportista.Name = "txtTransportista"
        Me.txtTransportista.Size = New System.Drawing.Size(310, 20)
        Me.txtTransportista.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 98)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Transportista:"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox7.Controls.Add(Me.btnNuevoCliente)
        Me.GroupBox7.Controls.Add(Me.cbxTipoDocumento)
        Me.GroupBox7.Controls.Add(Me.Label2)
        Me.GroupBox7.Controls.Add(Me.cbxTipoMovimiento)
        Me.GroupBox7.Controls.Add(Me.Label1)
        Me.GroupBox7.Controls.Add(Me.txtTransLlegada)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.txtDocCliente)
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.txtDirCliente)
        Me.GroupBox7.Controls.Add(Me.txtNomCliente)
        Me.GroupBox7.Controls.Add(Me.Label16)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Controls.Add(Me.dtpFecLlegada)
        Me.GroupBox7.Controls.Add(Me.Label18)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(405, 93)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(516, 148)
        Me.GroupBox7.TabIndex = 41
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Punto Llegada: Datos Cliente"
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(341, 19)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(55, 23)
        Me.btnBuscarCliente.TabIndex = 67
        Me.btnBuscarCliente.Text = "Buscar"
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'btnNuevoCliente
        '
        Me.btnNuevoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoCliente.Location = New System.Drawing.Point(341, 45)
        Me.btnNuevoCliente.Name = "btnNuevoCliente"
        Me.btnNuevoCliente.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoCliente.TabIndex = 66
        Me.btnNuevoCliente.Text = "Nuevo"
        Me.btnNuevoCliente.UseVisualStyleBackColor = True
        '
        'cbxTipoDocumento
        '
        Me.cbxTipoDocumento.FormattingEnabled = True
        Me.cbxTipoDocumento.Items.AddRange(New Object() {"GR", "PD"})
        Me.cbxTipoDocumento.Location = New System.Drawing.Point(455, 18)
        Me.cbxTipoDocumento.Name = "cbxTipoDocumento"
        Me.cbxTipoDocumento.Size = New System.Drawing.Size(56, 21)
        Me.cbxTipoDocumento.TabIndex = 65
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(396, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Tipo Doc.:"
        '
        'cbxTipoMovimiento
        '
        Me.cbxTipoMovimiento.FormattingEnabled = True
        Me.cbxTipoMovimiento.Items.AddRange(New Object() {"SA"})
        Me.cbxTipoMovimiento.Location = New System.Drawing.Point(455, 46)
        Me.cbxTipoMovimiento.Name = "cbxTipoMovimiento"
        Me.cbxTipoMovimiento.Size = New System.Drawing.Size(56, 21)
        Me.cbxTipoMovimiento.TabIndex = 63
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(396, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "Tipo Mov.:"
        '
        'txtTransLlegada
        '
        Me.txtTransLlegada.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransLlegada.Location = New System.Drawing.Point(72, 97)
        Me.txtTransLlegada.MaxLength = 80
        Me.txtTransLlegada.Name = "txtTransLlegada"
        Me.txtTransLlegada.Size = New System.Drawing.Size(263, 20)
        Me.txtTransLlegada.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(2, 101)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 13)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Transportista:"
        '
        'txtDocCliente
        '
        Me.txtDocCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocCliente.Location = New System.Drawing.Point(72, 72)
        Me.txtDocCliente.MaxLength = 11
        Me.txtDocCliente.Name = "txtDocCliente"
        Me.txtDocCliente.ReadOnly = True
        Me.txtDocCliente.Size = New System.Drawing.Size(111, 20)
        Me.txtDocCliente.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 13)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "DNI/RUC:"
        '
        'txtDirCliente
        '
        Me.txtDirCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtDirCliente.Location = New System.Drawing.Point(72, 46)
        Me.txtDirCliente.MaxLength = 80
        Me.txtDirCliente.Name = "txtDirCliente"
        Me.txtDirCliente.ReadOnly = True
        Me.txtDirCliente.Size = New System.Drawing.Size(263, 20)
        Me.txtDirCliente.TabIndex = 23
        '
        'txtNomCliente
        '
        Me.txtNomCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNomCliente.Location = New System.Drawing.Point(72, 21)
        Me.txtNomCliente.MaxLength = 80
        Me.txtNomCliente.Name = "txtNomCliente"
        Me.txtNomCliente.ReadOnly = True
        Me.txtNomCliente.Size = New System.Drawing.Size(263, 20)
        Me.txtNomCliente.TabIndex = 22
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 49)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 13)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "Dirección:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(47, 13)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "Nombre:"
        '
        'dtpFecLlegada
        '
        Me.dtpFecLlegada.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecLlegada.Location = New System.Drawing.Point(72, 123)
        Me.dtpFecLlegada.Name = "dtpFecLlegada"
        Me.dtpFecLlegada.Size = New System.Drawing.Size(111, 20)
        Me.dtpFecLlegada.TabIndex = 25
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(7, 129)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(40, 13)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "Fecha:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Location = New System.Drawing.Point(653, 463)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(268, 87)
        Me.GroupBox4.TabIndex = 42
        Me.GroupBox4.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTelefono)
        Me.GroupBox1.Controls.Add(Me.lblDireccion)
        Me.GroupBox1.Controls.Add(Me.lblNombre)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(399, 94)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        '
        'lblTelefono
        '
        Me.lblTelefono.AutoSize = True
        Me.lblTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelefono.Location = New System.Drawing.Point(106, 61)
        Me.lblTelefono.Name = "lblTelefono"
        Me.lblTelefono.Size = New System.Drawing.Size(0, 16)
        Me.lblTelefono.TabIndex = 3
        '
        'lblDireccion
        '
        Me.lblDireccion.AutoSize = True
        Me.lblDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDireccion.Location = New System.Drawing.Point(80, 35)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(0, 24)
        Me.lblDireccion.TabIndex = 1
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(30, 11)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(0, 25)
        Me.lblNombre.TabIndex = 0
        '
        'frmguiaRemisionSA
        '
        Me.AcceptButton = Me.btnBuscarCliente
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(920, 552)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvProductos)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmguiaRemisionSA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo Almacenes: Guía Remisión"
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNumDocumento As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblRUC As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBuscaRecibo As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnProducto As System.Windows.Forms.Button
    Friend WithEvents txtDNIRUC As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFecOrigen As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTransportista As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTransLlegada As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDocCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtDirCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtNomCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpFecLlegada As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents numItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents codProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents desProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents marca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents modelo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numSerie As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numMotor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numChasis As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grupo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cbxTipoDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoMovimiento As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents btnNuevoCliente As System.Windows.Forms.Button
End Class
