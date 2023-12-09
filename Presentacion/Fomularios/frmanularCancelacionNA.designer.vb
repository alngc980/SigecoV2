<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmanularCancelacionNA
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmanularCancelacionNA))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.dtpFechaVcto = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCodigoCliente = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDNI = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.btnBuscarCuotas = New System.Windows.Forms.Button
        Me.btnNuevoCliente = New System.Windows.Forms.Button
        Me.txtDireccion = New System.Windows.Forms.TextBox
        Me.txtNombres = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSerieDocumento = New System.Windows.Forms.TextBox
        Me.txtNumDocumento = New System.Windows.Forms.TextBox
        Me.dgvLetras = New System.Windows.Forms.DataGridView
        Me.letra = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.numero = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.moneda = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.montoMN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.montoME = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.adelantos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.saldos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.fecEmision = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.fecVence = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.fecPago = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tipMoneda = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblMensaje = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnLimpiar = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnProcesar = New System.Windows.Forms.Button
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvLetras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dtpFechaVcto)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtCodigoCliente)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtDNI)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.btnBuscarCuotas)
        Me.GroupBox3.Controls.Add(Me.btnNuevoCliente)
        Me.GroupBox3.Controls.Add(Me.txtDireccion)
        Me.GroupBox3.Controls.Add(Me.txtNombres)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.dtpFecha)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(411, 148)
        Me.GroupBox3.TabIndex = 53
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Datos Cliente"
        '
        'dtpFechaVcto
        '
        Me.dtpFechaVcto.Enabled = False
        Me.dtpFechaVcto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaVcto.Location = New System.Drawing.Point(280, 117)
        Me.dtpFechaVcto.Name = "dtpFechaVcto"
        Me.dtpFechaVcto.Size = New System.Drawing.Size(111, 20)
        Me.dtpFechaVcto.TabIndex = 22
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(201, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Fecha vcmto.:"
        '
        'txtCodigoCliente
        '
        Me.txtCodigoCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoCliente.Location = New System.Drawing.Point(80, 13)
        Me.txtCodigoCliente.MaxLength = 8
        Me.txtCodigoCliente.Name = "txtCodigoCliente"
        Me.txtCodigoCliente.ReadOnly = True
        Me.txtCodigoCliente.Size = New System.Drawing.Size(74, 20)
        Me.txtCodigoCliente.TabIndex = 20
        Me.txtCodigoCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Código:"
        '
        'txtDNI
        '
        Me.txtDNI.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNI.Location = New System.Drawing.Point(80, 91)
        Me.txtDNI.MaxLength = 11
        Me.txtDNI.Name = "txtDNI"
        Me.txtDNI.Size = New System.Drawing.Size(111, 20)
        Me.txtDNI.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(2, 94)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 13)
        Me.Label23.TabIndex = 18
        Me.Label23.Text = "DNI/RUC:"
        '
        'btnBuscarCuotas
        '
        Me.btnBuscarCuotas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCuotas.Location = New System.Drawing.Point(158, 11)
        Me.btnBuscarCuotas.Name = "btnBuscarCuotas"
        Me.btnBuscarCuotas.Size = New System.Drawing.Size(55, 23)
        Me.btnBuscarCuotas.TabIndex = 1
        Me.btnBuscarCuotas.Text = "Buscar"
        Me.btnBuscarCuotas.UseVisualStyleBackColor = True
        '
        'btnNuevoCliente
        '
        Me.btnNuevoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoCliente.Location = New System.Drawing.Point(216, 11)
        Me.btnNuevoCliente.Name = "btnNuevoCliente"
        Me.btnNuevoCliente.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoCliente.TabIndex = 2
        Me.btnNuevoCliente.Text = "Nuevo"
        Me.btnNuevoCliente.UseVisualStyleBackColor = True
        '
        'txtDireccion
        '
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Location = New System.Drawing.Point(80, 65)
        Me.txtDireccion.MaxLength = 50
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.ReadOnly = True
        Me.txtDireccion.Size = New System.Drawing.Size(311, 20)
        Me.txtDireccion.TabIndex = 4
        '
        'txtNombres
        '
        Me.txtNombres.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombres.Location = New System.Drawing.Point(80, 39)
        Me.txtNombres.MaxLength = 50
        Me.txtNombres.Name = "txtNombres"
        Me.txtNombres.ReadOnly = True
        Me.txtNombres.Size = New System.Drawing.Size(311, 20)
        Me.txtNombres.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(2, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Dirección:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Nombre:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Enabled = False
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(80, 117)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(111, 20)
        Me.dtpFecha.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(1, 124)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = "Fecha emisión:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(214, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Número:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSerieDocumento)
        Me.GroupBox1.Controls.Add(Me.txtNumDocumento)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(420, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 148)
        Me.GroupBox1.TabIndex = 57
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos Nota Abono(NA)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(97, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Serie:"
        '
        'txtSerieDocumento
        '
        Me.txtSerieDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerieDocumento.Location = New System.Drawing.Point(146, 11)
        Me.txtSerieDocumento.MaxLength = 2
        Me.txtSerieDocumento.Name = "txtSerieDocumento"
        Me.txtSerieDocumento.ReadOnly = True
        Me.txtSerieDocumento.Size = New System.Drawing.Size(56, 20)
        Me.txtSerieDocumento.TabIndex = 25
        Me.txtSerieDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNumDocumento
        '
        Me.txtNumDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocumento.Location = New System.Drawing.Point(267, 12)
        Me.txtNumDocumento.MaxLength = 12
        Me.txtNumDocumento.Name = "txtNumDocumento"
        Me.txtNumDocumento.Size = New System.Drawing.Size(65, 20)
        Me.txtNumDocumento.TabIndex = 24
        Me.txtNumDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvLetras
        '
        Me.dgvLetras.AllowUserToAddRows = False
        Me.dgvLetras.AllowUserToDeleteRows = False
        Me.dgvLetras.AllowUserToResizeColumns = False
        Me.dgvLetras.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgvLetras.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLetras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLetras.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.letra, Me.numero, Me.moneda, Me.montoMN, Me.montoME, Me.adelantos, Me.saldos, Me.fecEmision, Me.fecVence, Me.fecPago, Me.tipMoneda})
        Me.dgvLetras.Location = New System.Drawing.Point(3, 157)
        Me.dgvLetras.MultiSelect = False
        Me.dgvLetras.Name = "dgvLetras"
        Me.dgvLetras.RowHeadersVisible = False
        Me.dgvLetras.Size = New System.Drawing.Size(756, 192)
        Me.dgvLetras.TabIndex = 55
        '
        'letra
        '
        Me.letra.HeaderText = "Letra"
        Me.letra.Name = "letra"
        Me.letra.ReadOnly = True
        Me.letra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.letra.Width = 90
        '
        'numero
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.numero.DefaultCellStyle = DataGridViewCellStyle2
        Me.numero.HeaderText = "N°"
        Me.numero.Name = "numero"
        Me.numero.ReadOnly = True
        Me.numero.Width = 35
        '
        'moneda
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.moneda.DefaultCellStyle = DataGridViewCellStyle3
        Me.moneda.HeaderText = "moneda"
        Me.moneda.Name = "moneda"
        Me.moneda.ReadOnly = True
        Me.moneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.moneda.Width = 50
        '
        'montoMN
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.montoMN.DefaultCellStyle = DataGridViewCellStyle4
        Me.montoMN.HeaderText = "MontoMN"
        Me.montoMN.Name = "montoMN"
        Me.montoMN.ReadOnly = True
        Me.montoMN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.montoMN.Width = 70
        '
        'montoME
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.montoME.DefaultCellStyle = DataGridViewCellStyle5
        Me.montoME.HeaderText = "MontoME"
        Me.montoME.Name = "montoME"
        Me.montoME.ReadOnly = True
        Me.montoME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.montoME.Width = 70
        '
        'adelantos
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.adelantos.DefaultCellStyle = DataGridViewCellStyle6
        Me.adelantos.HeaderText = "Adelantos"
        Me.adelantos.Name = "adelantos"
        Me.adelantos.ReadOnly = True
        Me.adelantos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.adelantos.Width = 70
        '
        'saldos
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.saldos.DefaultCellStyle = DataGridViewCellStyle7
        Me.saldos.HeaderText = "Saldos"
        Me.saldos.Name = "saldos"
        Me.saldos.ReadOnly = True
        Me.saldos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.saldos.Width = 70
        '
        'fecEmision
        '
        DataGridViewCellStyle8.Format = "d"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.fecEmision.DefaultCellStyle = DataGridViewCellStyle8
        Me.fecEmision.HeaderText = "F. Emis."
        Me.fecEmision.Name = "fecEmision"
        Me.fecEmision.ReadOnly = True
        Me.fecEmision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fecEmision.Width = 70
        '
        'fecVence
        '
        DataGridViewCellStyle9.Format = "d"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.fecVence.DefaultCellStyle = DataGridViewCellStyle9
        Me.fecVence.HeaderText = "F. Venc."
        Me.fecVence.Name = "fecVence"
        Me.fecVence.ReadOnly = True
        Me.fecVence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fecVence.Width = 70
        '
        'fecPago
        '
        DataGridViewCellStyle10.Format = "d"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.fecPago.DefaultCellStyle = DataGridViewCellStyle10
        Me.fecPago.HeaderText = "F. Pago"
        Me.fecPago.Name = "fecPago"
        Me.fecPago.ReadOnly = True
        Me.fecPago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fecPago.Width = 70
        '
        'tipMoneda
        '
        Me.tipMoneda.HeaderText = "Moneda"
        Me.tipMoneda.Name = "tipMoneda"
        Me.tipMoneda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.tipMoneda.Visible = False
        Me.tipMoneda.Width = 40
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.Location = New System.Drawing.Point(0, 354)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(0, 13)
        Me.lblMensaje.TabIndex = 56
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnLimpiar)
        Me.GroupBox5.Controls.Add(Me.btnSalir)
        Me.GroupBox5.Controls.Add(Me.btnProcesar)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 367)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(756, 52)
        Me.GroupBox5.TabIndex = 54
        Me.GroupBox5.TabStop = False
        '
        'btnLimpiar
        '
        Me.btnLimpiar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = CType(resources.GetObject("btnLimpiar.Image"), System.Drawing.Image)
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiar.Location = New System.Drawing.Point(310, 14)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(100, 30)
        Me.btnLimpiar.TabIndex = 6
        Me.btnLimpiar.Text = "F12 Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(504, 14)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 30)
        Me.btnSalir.TabIndex = 5
        Me.btnSalir.Text = "[Esc] Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnProcesar
        '
        Me.btnProcesar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcesar.Image = CType(resources.GetObject("btnProcesar.Image"), System.Drawing.Image)
        Me.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProcesar.Location = New System.Drawing.Point(132, 14)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.Size = New System.Drawing.Size(100, 30)
        Me.btnProcesar.TabIndex = 2
        Me.btnProcesar.Text = "F4 Procesar"
        Me.btnProcesar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProcesar.UseVisualStyleBackColor = True
        '
        'frmanularCancelacionNA
        '
        Me.AcceptButton = Me.btnBuscarCuotas
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(760, 420)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvLetras)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.GroupBox5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmanularCancelacionNA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo Auxiliar Anulación Cuotas Cancelas x Nota Abono"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvLetras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFechaVcto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDNI As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCuotas As System.Windows.Forms.Button
    Friend WithEvents btnNuevoCliente As System.Windows.Forms.Button
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents txtNombres As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSerieDocumento As System.Windows.Forms.TextBox
    Friend WithEvents txtNumDocumento As System.Windows.Forms.TextBox
    Friend WithEvents dgvLetras As System.Windows.Forms.DataGridView
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents letra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numero As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents montoMN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents montoME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents adelantos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents saldos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fecEmision As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fecVence As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fecPago As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tipMoneda As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
