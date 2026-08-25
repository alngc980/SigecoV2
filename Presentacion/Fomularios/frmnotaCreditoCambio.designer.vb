<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmnotaCreditoCambio
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmnotaCreditoCambio))
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtmFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtNombres = New System.Windows.Forms.TextBox()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.btnNuevoCliente = New System.Windows.Forms.Button()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtDNI = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFechaVcmto = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dgvDetalles = New System.Windows.Forms.DataGridView()
        Me.numItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.codProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.desProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.precioUnitario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.subTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnBuscaDocumento = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.cbxTipoVenta = New System.Windows.Forms.ComboBox()
        Me.txtNumGuia = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cbxTipoCredito = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbxTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cbxCanCuotas = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtNumRecibo = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtGlosa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtCodigoVendedor = New System.Windows.Forms.TextBox()
        Me.cbxTipoDocumento = New System.Windows.Forms.ComboBox()
        Me.txtNumDocumentoVenta = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtMotivoNotaCredito = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbxTipoNotaCredito = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalDocumentoVenta = New System.Windows.Forms.TextBox()
        Me.txtInteres = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtIGV = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtTotalNotaCredito = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.lblRuc = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNumNotaCredito = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSerieDocumento = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblDireccion = New System.Windows.Forms.Label()
        Me.lblTelefono = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(2, 97)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = "F. Emis:"
        '
        'dtmFecha
        '
        Me.dtmFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmFecha.Location = New System.Drawing.Point(56, 93)
        Me.dtmFecha.Name = "dtmFecha"
        Me.dtmFecha.Size = New System.Drawing.Size(111, 20)
        Me.dtmFecha.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Nombre:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Dirección:"
        '
        'txtNombres
        '
        Me.txtNombres.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombres.Location = New System.Drawing.Point(56, 15)
        Me.txtNombres.MaxLength = 80
        Me.txtNombres.Name = "txtNombres"
        Me.txtNombres.ReadOnly = True
        Me.txtNombres.Size = New System.Drawing.Size(300, 20)
        Me.txtNombres.TabIndex = 12
        '
        'txtDireccion
        '
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Location = New System.Drawing.Point(56, 41)
        Me.txtDireccion.MaxLength = 80
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.ReadOnly = True
        Me.txtDireccion.Size = New System.Drawing.Size(300, 20)
        Me.txtDireccion.TabIndex = 13
        '
        'btnNuevoCliente
        '
        Me.btnNuevoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoCliente.Location = New System.Drawing.Point(359, 39)
        Me.btnNuevoCliente.Name = "btnNuevoCliente"
        Me.btnNuevoCliente.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoCliente.TabIndex = 8
        Me.btnNuevoCliente.Text = "Nuevo"
        Me.btnNuevoCliente.UseVisualStyleBackColor = True
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(359, 13)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(55, 23)
        Me.btnBuscarCliente.TabIndex = 9
        Me.btnBuscarCliente.Text = "Buscar"
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 70)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(29, 13)
        Me.Label23.TabIndex = 18
        Me.Label23.Text = "DNI:"
        '
        'txtDNI
        '
        Me.txtDNI.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNI.Location = New System.Drawing.Point(56, 67)
        Me.txtDNI.MaxLength = 8
        Me.txtDNI.Name = "txtDNI"
        Me.txtDNI.ReadOnly = True
        Me.txtDNI.Size = New System.Drawing.Size(111, 20)
        Me.txtDNI.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(174, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Fec. Docum:"
        '
        'dtpFechaVcmto
        '
        Me.dtpFechaVcmto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaVcmto.Location = New System.Drawing.Point(245, 93)
        Me.dtpFechaVcmto.Name = "dtpFechaVcmto"
        Me.dtpFechaVcmto.Size = New System.Drawing.Size(111, 20)
        Me.dtpFechaVcmto.TabIndex = 20
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dtpFechaVcmto)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtDNI)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox3.Controls.Add(Me.btnNuevoCliente)
        Me.GroupBox3.Controls.Add(Me.txtDireccion)
        Me.GroupBox3.Controls.Add(Me.txtNombres)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.dtmFecha)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 96)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(417, 120)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Datos Cliente"
        '
        'dgvDetalles
        '
        Me.dgvDetalles.AllowUserToAddRows = False
        Me.dgvDetalles.AllowUserToDeleteRows = False
        Me.dgvDetalles.AllowUserToResizeColumns = False
        Me.dgvDetalles.AllowUserToResizeRows = False
        Me.dgvDetalles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetalles.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.numItem, Me.codProducto, Me.desProducto, Me.precioUnitario, Me.cantidad, Me.subTotal})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetalles.DefaultCellStyle = DataGridViewCellStyle15
        Me.dgvDetalles.Location = New System.Drawing.Point(0, 219)
        Me.dgvDetalles.Name = "dgvDetalles"
        Me.dgvDetalles.ReadOnly = True
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetalles.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvDetalles.RowHeadersVisible = False
        Me.dgvDetalles.Size = New System.Drawing.Size(921, 211)
        Me.dgvDetalles.TabIndex = 47
        '
        'numItem
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.NullValue = Nothing
        Me.numItem.DefaultCellStyle = DataGridViewCellStyle10
        Me.numItem.Frozen = True
        Me.numItem.HeaderText = "N° Item"
        Me.numItem.Name = "numItem"
        Me.numItem.ReadOnly = True
        Me.numItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.numItem.Width = 50
        '
        'codProducto
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.codProducto.DefaultCellStyle = DataGridViewCellStyle11
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
        Me.desProducto.Width = 400
        '
        'precioUnitario
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.precioUnitario.DefaultCellStyle = DataGridViewCellStyle12
        Me.precioUnitario.Frozen = True
        Me.precioUnitario.HeaderText = "Precio Unitario"
        Me.precioUnitario.Name = "precioUnitario"
        Me.precioUnitario.ReadOnly = True
        Me.precioUnitario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'cantidad
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.NullValue = Nothing
        Me.cantidad.DefaultCellStyle = DataGridViewCellStyle13
        Me.cantidad.Frozen = True
        Me.cantidad.HeaderText = "Cantidad"
        Me.cantidad.Name = "cantidad"
        Me.cantidad.ReadOnly = True
        Me.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'subTotal
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N2"
        DataGridViewCellStyle14.NullValue = Nothing
        Me.subTotal.DefaultCellStyle = DataGridViewCellStyle14
        Me.subTotal.Frozen = True
        Me.subTotal.HeaderText = "Total"
        Me.subTotal.Name = "subTotal"
        Me.subTotal.ReadOnly = True
        Me.subTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.subTotal.Width = 120
        '
        'btnBuscar
        '
        Me.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Image = CType(resources.GetObject("btnBuscar.Image"), System.Drawing.Image)
        Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBuscar.Location = New System.Drawing.Point(12, 44)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(100, 37)
        Me.btnBuscar.TabIndex = 0
        Me.btnBuscar.Text = "F2 Buscar"
        Me.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.Location = New System.Drawing.Point(324, 44)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(100, 37)
        Me.btnImprimir.TabIndex = 1
        Me.btnImprimir.Text = "F10 Imprimir"
        Me.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnProcesar
        '
        Me.btnProcesar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcesar.Image = CType(resources.GetObject("btnProcesar.Image"), System.Drawing.Image)
        Me.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProcesar.Location = New System.Drawing.Point(116, 44)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(100, 37)
        Me.btnProcesar.TabIndex = 2
        Me.btnProcesar.Text = "F4 Procesar"
        Me.btnProcesar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'btnAnular
        '
        Me.btnAnular.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Image = CType(resources.GetObject("btnAnular.Image"), System.Drawing.Image)
        Me.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnular.Location = New System.Drawing.Point(220, 44)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(100, 37)
        Me.btnAnular.TabIndex = 4
        Me.btnAnular.Text = "F8 Anular"
        Me.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnular.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(532, 44)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 37)
        Me.btnSalir.TabIndex = 5
        Me.btnSalir.Text = "[Esc] Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnLimpiar
        '
        Me.btnLimpiar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = CType(resources.GetObject("btnLimpiar.Image"), System.Drawing.Image)
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiar.Location = New System.Drawing.Point(428, 44)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(100, 37)
        Me.btnLimpiar.TabIndex = 6
        Me.btnLimpiar.Text = "F12 Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnBuscaDocumento
        '
        Me.btnBuscaDocumento.BackColor = System.Drawing.Color.Transparent
        Me.btnBuscaDocumento.Location = New System.Drawing.Point(606, 87)
        Me.btnBuscaDocumento.Name = "btnBuscaDocumento"
        Me.btnBuscaDocumento.Size = New System.Drawing.Size(1, 1)
        Me.btnBuscaDocumento.TabIndex = 7
        Me.btnBuscaDocumento.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnBuscaDocumento)
        Me.GroupBox5.Controls.Add(Me.btnLimpiar)
        Me.GroupBox5.Controls.Add(Me.btnSalir)
        Me.GroupBox5.Controls.Add(Me.btnAnular)
        Me.GroupBox5.Controls.Add(Me.btnProcesar)
        Me.GroupBox5.Controls.Add(Me.btnImprimir)
        Me.GroupBox5.Controls.Add(Me.btnBuscar)
        Me.GroupBox5.Location = New System.Drawing.Point(0, 430)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(647, 123)
        Me.GroupBox5.TabIndex = 50
        Me.GroupBox5.TabStop = False
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
        'cbxTipoVenta
        '
        Me.cbxTipoVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cbxTipoVenta.FormattingEnabled = True
        Me.cbxTipoVenta.Items.AddRange(New Object() {"Venta Contado", "Venta Credito", "Venta Credito", "Cuota Inicial", "Anticipo Cuota Inicial", "Venta Tarjeta", "Venta Tarjeta Oferta", "Venta Tarjeta Remate", "Venta Oferta", "Venta Remate"})
        Me.cbxTipoVenta.Location = New System.Drawing.Point(66, 15)
        Me.cbxTipoVenta.Name = "cbxTipoVenta"
        Me.cbxTipoVenta.Size = New System.Drawing.Size(64, 21)
        Me.cbxTipoVenta.TabIndex = 7
        '
        'txtNumGuia
        '
        Me.txtNumGuia.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumGuia.Location = New System.Drawing.Point(434, 92)
        Me.txtNumGuia.MaxLength = 10
        Me.txtNumGuia.Name = "txtNumGuia"
        Me.txtNumGuia.ReadOnly = True
        Me.txtNumGuia.Size = New System.Drawing.Size(65, 20)
        Me.txtNumGuia.TabIndex = 15
        Me.txtNumGuia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(2, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(62, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Tipo Venta:"
        '
        'cbxTipoCredito
        '
        Me.cbxTipoCredito.FormattingEnabled = True
        Me.cbxTipoCredito.Items.AddRange(New Object() {"Mensual", "Semanal"})
        Me.cbxTipoCredito.Location = New System.Drawing.Point(66, 41)
        Me.cbxTipoCredito.Name = "cbxTipoCredito"
        Me.cbxTipoCredito.Size = New System.Drawing.Size(64, 21)
        Me.cbxTipoCredito.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(2, 46)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 13)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "TipoCrédito:"
        '
        'cbxTipoMoneda
        '
        Me.cbxTipoMoneda.FormattingEnabled = True
        Me.cbxTipoMoneda.Items.AddRange(New Object() {"Soles", "Dolares", "Euros"})
        Me.cbxTipoMoneda.Location = New System.Drawing.Point(185, 15)
        Me.cbxTipoMoneda.Name = "cbxTipoMoneda"
        Me.cbxTipoMoneda.Size = New System.Drawing.Size(64, 21)
        Me.cbxTipoMoneda.TabIndex = 5
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(132, 19)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 13)
        Me.Label21.TabIndex = 39
        Me.Label21.Text = "Moneda:"
        '
        'cbxCanCuotas
        '
        Me.cbxCanCuotas.FormattingEnabled = True
        Me.cbxCanCuotas.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"})
        Me.cbxCanCuotas.Location = New System.Drawing.Point(185, 41)
        Me.cbxCanCuotas.Name = "cbxCanCuotas"
        Me.cbxCanCuotas.Size = New System.Drawing.Size(64, 21)
        Me.cbxCanCuotas.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(130, 45)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(55, 13)
        Me.Label20.TabIndex = 41
        Me.Label20.Text = "N°Cuotas:"
        '
        'txtNumRecibo
        '
        Me.txtNumRecibo.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumRecibo.Location = New System.Drawing.Point(434, 41)
        Me.txtNumRecibo.MaxLength = 10
        Me.txtNumRecibo.Name = "txtNumRecibo"
        Me.txtNumRecibo.ReadOnly = True
        Me.txtNumRecibo.Size = New System.Drawing.Size(65, 20)
        Me.txtNumRecibo.TabIndex = 1
        Me.txtNumRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(5, 96)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(37, 13)
        Me.Label24.TabIndex = 44
        Me.Label24.Text = "Glosa:"
        '
        'txtGlosa
        '
        Me.txtGlosa.BackColor = System.Drawing.SystemColors.Window
        Me.txtGlosa.Location = New System.Drawing.Point(66, 93)
        Me.txtGlosa.MaxLength = 50
        Me.txtGlosa.Name = "txtGlosa"
        Me.txtGlosa.Size = New System.Drawing.Size(300, 20)
        Me.txtGlosa.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(253, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Tipo Doc.:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(255, 45)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(56, 13)
        Me.Label34.TabIndex = 50
        Me.Label34.Text = "Vendedor:"
        '
        'txtCodigoVendedor
        '
        Me.txtCodigoVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoVendedor.Location = New System.Drawing.Point(310, 41)
        Me.txtCodigoVendedor.MaxLength = 10
        Me.txtCodigoVendedor.Name = "txtCodigoVendedor"
        Me.txtCodigoVendedor.ReadOnly = True
        Me.txtCodigoVendedor.Size = New System.Drawing.Size(56, 20)
        Me.txtCodigoVendedor.TabIndex = 3
        Me.txtCodigoVendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbxTipoDocumento
        '
        Me.cbxTipoDocumento.FormattingEnabled = True
        Me.cbxTipoDocumento.Items.AddRange(New Object() {"BV", "FV"})
        Me.cbxTipoDocumento.Location = New System.Drawing.Point(310, 15)
        Me.cbxTipoDocumento.Name = "cbxTipoDocumento"
        Me.cbxTipoDocumento.Size = New System.Drawing.Size(56, 21)
        Me.cbxTipoDocumento.TabIndex = 55
        '
        'txtNumDocumentoVenta
        '
        Me.txtNumDocumentoVenta.Location = New System.Drawing.Point(434, 15)
        Me.txtNumDocumentoVenta.MaxLength = 10
        Me.txtNumDocumentoVenta.Name = "txtNumDocumentoVenta"
        Me.txtNumDocumentoVenta.Size = New System.Drawing.Size(65, 20)
        Me.txtNumDocumentoVenta.TabIndex = 56
        Me.txtNumDocumentoVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(367, 46)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 13)
        Me.Label22.TabIndex = 59
        Me.Label22.Text = "Recibo N°:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Doc.Vta.N°:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(367, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Guía N°:"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtMotivoNotaCredito)
        Me.GroupBox7.Controls.Add(Me.Label10)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.cbxTipoNotaCredito)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Controls.Add(Me.txtNumDocumentoVenta)
        Me.GroupBox7.Controls.Add(Me.cbxTipoDocumento)
        Me.GroupBox7.Controls.Add(Me.txtCodigoVendedor)
        Me.GroupBox7.Controls.Add(Me.Label34)
        Me.GroupBox7.Controls.Add(Me.Label2)
        Me.GroupBox7.Controls.Add(Me.txtGlosa)
        Me.GroupBox7.Controls.Add(Me.Label24)
        Me.GroupBox7.Controls.Add(Me.txtNumRecibo)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.cbxCanCuotas)
        Me.GroupBox7.Controls.Add(Me.Label21)
        Me.GroupBox7.Controls.Add(Me.cbxTipoMoneda)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.cbxTipoCredito)
        Me.GroupBox7.Controls.Add(Me.Label19)
        Me.GroupBox7.Controls.Add(Me.txtNumGuia)
        Me.GroupBox7.Controls.Add(Me.cbxTipoVenta)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(419, 96)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(501, 120)
        Me.GroupBox7.TabIndex = 48
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Datos Venta"
        '
        'txtMotivoNotaCredito
        '
        Me.txtMotivoNotaCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtMotivoNotaCredito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMotivoNotaCredito.Location = New System.Drawing.Point(185, 67)
        Me.txtMotivoNotaCredito.MaxLength = 120
        Me.txtMotivoNotaCredito.Multiline = True
        Me.txtMotivoNotaCredito.Name = "txtMotivoNotaCredito"
        Me.txtMotivoNotaCredito.ReadOnly = True
        Me.txtMotivoNotaCredito.Size = New System.Drawing.Size(314, 21)
        Me.txtMotivoNotaCredito.TabIndex = 65
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(130, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "Motivo NC:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "Tipo NC:"
        '
        'cbxTipoNotaCredito
        '
        Me.cbxTipoNotaCredito.FormattingEnabled = True
        Me.cbxTipoNotaCredito.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.cbxTipoNotaCredito.Location = New System.Drawing.Point(66, 67)
        Me.cbxTipoNotaCredito.Name = "cbxTipoNotaCredito"
        Me.cbxTipoNotaCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbxTipoNotaCredito.Size = New System.Drawing.Size(64, 21)
        Me.cbxTipoNotaCredito.TabIndex = 62
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(123, 13)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Total Documento Venta:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(4, 58)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(28, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "IGV:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(4, 82)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Descuento:"
        '
        'txtTotalDocumentoVenta
        '
        Me.txtTotalDocumentoVenta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalDocumentoVenta.Location = New System.Drawing.Point(160, 8)
        Me.txtTotalDocumentoVenta.Name = "txtTotalDocumentoVenta"
        Me.txtTotalDocumentoVenta.ReadOnly = True
        Me.txtTotalDocumentoVenta.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalDocumentoVenta.TabIndex = 16
        Me.txtTotalDocumentoVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInteres
        '
        Me.txtInteres.BackColor = System.Drawing.SystemColors.Window
        Me.txtInteres.Location = New System.Drawing.Point(160, 31)
        Me.txtInteres.Name = "txtInteres"
        Me.txtInteres.ReadOnly = True
        Me.txtInteres.Size = New System.Drawing.Size(105, 20)
        Me.txtInteres.TabIndex = 17
        Me.txtInteres.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Location = New System.Drawing.Point(160, 77)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.ReadOnly = True
        Me.txtDescuento.Size = New System.Drawing.Size(105, 20)
        Me.txtDescuento.TabIndex = 19
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Interes:"
        '
        'txtIGV
        '
        Me.txtIGV.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGV.Location = New System.Drawing.Point(160, 54)
        Me.txtIGV.Name = "txtIGV"
        Me.txtIGV.ReadOnly = True
        Me.txtIGV.Size = New System.Drawing.Size(105, 20)
        Me.txtIGV.TabIndex = 18
        Me.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(4, 104)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(115, 13)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "Total Nota Crédito:"
        '
        'txtTotalNotaCredito
        '
        Me.txtTotalNotaCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalNotaCredito.Location = New System.Drawing.Point(160, 100)
        Me.txtTotalNotaCredito.Name = "txtTotalNotaCredito"
        Me.txtTotalNotaCredito.ReadOnly = True
        Me.txtTotalNotaCredito.Size = New System.Drawing.Size(105, 20)
        Me.txtTotalNotaCredito.TabIndex = 20
        Me.txtTotalNotaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtTotalNotaCredito)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.txtIGV)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtDescuento)
        Me.GroupBox4.Controls.Add(Me.txtInteres)
        Me.GroupBox4.Controls.Add(Me.txtTotalDocumentoVenta)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Location = New System.Drawing.Point(653, 430)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(268, 123)
        Me.GroupBox4.TabIndex = 49
        Me.GroupBox4.TabStop = False
        '
        'PrintDocument1
        '
        '
        'lblRuc
        '
        Me.lblRuc.AutoSize = True
        Me.lblRuc.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuc.Location = New System.Drawing.Point(135, 8)
        Me.lblRuc.Name = "lblRuc"
        Me.lblRuc.Size = New System.Drawing.Size(0, 25)
        Me.lblRuc.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(255, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Serie:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(367, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Número:"
        '
        'txtNumNotaCredito
        '
        Me.txtNumNotaCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumNotaCredito.Location = New System.Drawing.Point(434, 65)
        Me.txtNumNotaCredito.MaxLength = 10
        Me.txtNumNotaCredito.Name = "txtNumNotaCredito"
        Me.txtNumNotaCredito.ReadOnly = True
        Me.txtNumNotaCredito.Size = New System.Drawing.Size(65, 20)
        Me.txtNumNotaCredito.TabIndex = 13
        Me.txtNumNotaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(38, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(430, 25)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "NOTA CREDITO - PARA MOVIMIENTOS"
        '
        'txtSerieDocumento
        '
        Me.txtSerieDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerieDocumento.Location = New System.Drawing.Point(310, 65)
        Me.txtSerieDocumento.MaxLength = 2
        Me.txtSerieDocumento.Name = "txtSerieDocumento"
        Me.txtSerieDocumento.Size = New System.Drawing.Size(56, 20)
        Me.txtSerieDocumento.TabIndex = 14
        Me.txtSerieDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSerieDocumento)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtNumNotaCredito)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblRuc)
        Me.GroupBox2.Location = New System.Drawing.Point(419, -1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(501, 94)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(30, 9)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(0, 25)
        Me.lblNombre.TabIndex = 0
        '
        'lblDireccion
        '
        Me.lblDireccion.AutoSize = True
        Me.lblDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDireccion.Location = New System.Drawing.Point(80, 33)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(0, 24)
        Me.lblDireccion.TabIndex = 1
        '
        'lblTelefono
        '
        Me.lblTelefono.AutoSize = True
        Me.lblTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelefono.Location = New System.Drawing.Point(106, 59)
        Me.lblTelefono.Name = "lblTelefono"
        Me.lblTelefono.Size = New System.Drawing.Size(0, 16)
        Me.lblTelefono.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTelefono)
        Me.GroupBox1.Controls.Add(Me.lblDireccion)
        Me.GroupBox1.Controls.Add(Me.lblNombre)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(417, 94)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'frmnotaCreditoCambio
        '
        Me.AcceptButton = Me.btnBuscar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(920, 552)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.dgvDetalles)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmnotaCreditoCambio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo Nota Crédito Movimientos"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgvDetalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtmFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtNombres As System.Windows.Forms.TextBox
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents btnNuevoCliente As System.Windows.Forms.Button
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtDNI As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaVcmto As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvDetalles As System.Windows.Forms.DataGridView
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents btnBuscaDocumento As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents cbxTipoVenta As System.Windows.Forms.ComboBox
    Friend WithEvents txtNumGuia As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoCredito As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cbxCanCuotas As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtNumRecibo As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtGlosa As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoVendedor As System.Windows.Forms.TextBox
    Friend WithEvents cbxTipoDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents txtNumDocumentoVenta As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalDocumentoVenta As System.Windows.Forms.TextBox
    Friend WithEvents txtInteres As System.Windows.Forms.TextBox
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtIGV As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtTotalNotaCredito As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents lblRuc As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNumNotaCredito As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSerieDocumento As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents txtMotivoNotaCredito As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoNotaCredito As System.Windows.Forms.ComboBox
    Friend WithEvents numItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents codProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents desProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents precioUnitario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents subTotal As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
