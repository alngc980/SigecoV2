<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmreciboPago
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmreciboPago))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNumRecibo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblRUC = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTelefono = New System.Windows.Forms.Label()
        Me.lblDireccion = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnBuscaLetra = New System.Windows.Forms.Button()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnProducto = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDiasVencidos = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpFechaVcmto = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCodigoCliente = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDNI = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.btnNuevoCliente = New System.Windows.Forms.Button()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtNombres = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCodigoVendedor = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.lblMoneda4 = New System.Windows.Forms.Label()
        Me.txtInteresPagoCuota = New System.Windows.Forms.TextBox()
        Me.lblMoneda3 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtDiferencia = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblMoneda2 = New System.Windows.Forms.Label()
        Me.txtPMultiple = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblMoneda1 = New System.Windows.Forms.Label()
        Me.lblMoneda = New System.Windows.Forms.Label()
        Me.txtOtrosPagos = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTotalPagar = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInteres = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCorrelativo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMontoMN = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbxTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cbxTipoDocumento = New System.Windows.Forms.ComboBox()
        Me.lblMontoME = New System.Windows.Forms.Label()
        Me.txtMontoME = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbxConcepto = New System.Windows.Forms.ComboBox()
        Me.cbxTipoPago = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNumLetra = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.lblMsj = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSerie)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtNumRecibo)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblRUC)
        Me.GroupBox2.Location = New System.Drawing.Point(418, -4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(405, 94)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        '
        'txtSerie
        '
        Me.txtSerie.BackColor = System.Drawing.SystemColors.Window
        Me.txtSerie.Location = New System.Drawing.Point(109, 64)
        Me.txtSerie.MaxLength = 2
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.ReadOnly = True
        Me.txtSerie.Size = New System.Drawing.Size(82, 20)
        Me.txtSerie.TabIndex = 7
        Me.txtSerie.Text = "B001"
        Me.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(94, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(211, 25)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "RECIBO ENTRADA"
        '
        'txtNumRecibo
        '
        Me.txtNumRecibo.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumRecibo.Location = New System.Drawing.Point(312, 64)
        Me.txtNumRecibo.Name = "txtNumRecibo"
        Me.txtNumRecibo.ReadOnly = True
        Me.txtNumRecibo.Size = New System.Drawing.Size(90, 20)
        Me.txtNumRecibo.TabIndex = 5
        Me.txtNumRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(192, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Número:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Serie:"
        '
        'lblRUC
        '
        Me.lblRUC.AutoSize = True
        Me.lblRUC.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRUC.Location = New System.Drawing.Point(89, 10)
        Me.lblRUC.Name = "lblRUC"
        Me.lblRUC.Size = New System.Drawing.Size(0, 25)
        Me.lblRUC.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTelefono)
        Me.GroupBox1.Controls.Add(Me.lblDireccion)
        Me.GroupBox1.Controls.Add(Me.lblNombre)
        Me.GroupBox1.Location = New System.Drawing.Point(1, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(415, 94)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        '
        'lblTelefono
        '
        Me.lblTelefono.AutoSize = True
        Me.lblTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelefono.Location = New System.Drawing.Point(99, 57)
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
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnBuscaLetra)
        Me.GroupBox5.Controls.Add(Me.btnLimpiar)
        Me.GroupBox5.Controls.Add(Me.btnSalir)
        Me.GroupBox5.Controls.Add(Me.btnAnular)
        Me.GroupBox5.Controls.Add(Me.btnGrabar)
        Me.GroupBox5.Controls.Add(Me.btnImprimir)
        Me.GroupBox5.Controls.Add(Me.btnProducto)
        Me.GroupBox5.Location = New System.Drawing.Point(1, 334)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(822, 63)
        Me.GroupBox5.TabIndex = 37
        Me.GroupBox5.TabStop = False
        '
        'btnBuscaLetra
        '
        Me.btnBuscaLetra.Location = New System.Drawing.Point(699, 23)
        Me.btnBuscaLetra.Name = "btnBuscaLetra"
        Me.btnBuscaLetra.Size = New System.Drawing.Size(1, 1)
        Me.btnBuscaLetra.TabIndex = 7
        Me.btnBuscaLetra.UseVisualStyleBackColor = True
        '
        'btnLimpiar
        '
        Me.btnLimpiar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = My.Resources.DeleteHS
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLimpiar.Location = New System.Drawing.Point(539, 16)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(100, 37)
        Me.btnLimpiar.TabIndex = 6
        Me.btnLimpiar.Text = "F12 Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = My.Resources.FillRightHS
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(657, 16)
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
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Image = My.Resources.CutHS1
        Me.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnular.Location = New System.Drawing.Point(303, 16)
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
        Me.btnGrabar.Image = My.Resources.FormRunHS
        Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGrabar.Location = New System.Drawing.Point(185, 16)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(100, 37)
        Me.btnGrabar.TabIndex = 2
        Me.btnGrabar.Text = "F4 Procesar"
        Me.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Image = My.Resources.PrintHS
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImprimir.Location = New System.Drawing.Point(421, 16)
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
        Me.btnProducto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnProducto.Image = My.Resources.ZoomHS
        Me.btnProducto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProducto.Location = New System.Drawing.Point(68, 16)
        Me.btnProducto.Name = "btnProducto"
        Me.btnProducto.Size = New System.Drawing.Size(100, 37)
        Me.btnProducto.TabIndex = 0
        Me.btnProducto.Text = "F2 Buscar"
        Me.btnProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProducto.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDiasVencidos)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.dtpFechaVcmto)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtCodigoCliente)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.txtDNI)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox3.Controls.Add(Me.btnNuevoCliente)
        Me.GroupBox3.Controls.Add(Me.txtDireccion)
        Me.GroupBox3.Controls.Add(Me.txtNombres)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txtCodigoVendedor)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.dtpFecha)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(1, 89)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(415, 230)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Datos Cliente"
        '
        'txtDiasVencidos
        '
        Me.txtDiasVencidos.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiasVencidos.Enabled = False
        Me.txtDiasVencidos.Location = New System.Drawing.Point(84, 165)
        Me.txtDiasVencidos.MaxLength = 12
        Me.txtDiasVencidos.Name = "txtDiasVencidos"
        Me.txtDiasVencidos.Size = New System.Drawing.Size(80, 20)
        Me.txtDiasVencidos.TabIndex = 68
        Me.txtDiasVencidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 16)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "D.Vencidos:"
        '
        'dtpFechaVcmto
        '
        Me.dtpFechaVcmto.Enabled = False
        Me.dtpFechaVcmto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaVcmto.Location = New System.Drawing.Point(84, 139)
        Me.dtpFechaVcmto.Name = "dtpFechaVcmto"
        Me.dtpFechaVcmto.Size = New System.Drawing.Size(80, 20)
        Me.dtpFechaVcmto.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 16)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Fec. Vcto:"
        '
        'txtCodigoCliente
        '
        Me.txtCodigoCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoCliente.Location = New System.Drawing.Point(84, 11)
        Me.txtCodigoCliente.Name = "txtCodigoCliente"
        Me.txtCodigoCliente.ReadOnly = True
        Me.txtCodigoCliente.Size = New System.Drawing.Size(80, 20)
        Me.txtCodigoCliente.TabIndex = 35
        Me.txtCodigoCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(5, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(55, 16)
        Me.Label19.TabIndex = 34
        Me.Label19.Text = "Código:"
        '
        'txtDNI
        '
        Me.txtDNI.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNI.Location = New System.Drawing.Point(84, 86)
        Me.txtDNI.MaxLength = 11
        Me.txtDNI.Name = "txtDNI"
        Me.txtDNI.Size = New System.Drawing.Size(80, 20)
        Me.txtDNI.TabIndex = 33
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(4, 89)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(67, 16)
        Me.Label23.TabIndex = 32
        Me.Label23.Text = "DNI/RUC:"
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(167, 9)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(64, 23)
        Me.btnBuscarCliente.TabIndex = 9
        Me.btnBuscarCliente.Text = "Buscar"
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'btnNuevoCliente
        '
        Me.btnNuevoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoCliente.Location = New System.Drawing.Point(237, 9)
        Me.btnNuevoCliente.Name = "btnNuevoCliente"
        Me.btnNuevoCliente.Size = New System.Drawing.Size(64, 23)
        Me.btnNuevoCliente.TabIndex = 8
        Me.btnNuevoCliente.Text = "Nuevo"
        Me.btnNuevoCliente.UseVisualStyleBackColor = True
        '
        'txtDireccion
        '
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Location = New System.Drawing.Point(84, 60)
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.ReadOnly = True
        Me.txtDireccion.Size = New System.Drawing.Size(326, 20)
        Me.txtDireccion.TabIndex = 5
        '
        'txtNombres
        '
        Me.txtNombres.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombres.Location = New System.Drawing.Point(84, 36)
        Me.txtNombres.Name = "txtNombres"
        Me.txtNombres.ReadOnly = True
        Me.txtNombres.Size = New System.Drawing.Size(326, 20)
        Me.txtNombres.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 64)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 16)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Dirección:"
        '
        'txtCodigoVendedor
        '
        Me.txtCodigoVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoVendedor.Location = New System.Drawing.Point(84, 191)
        Me.txtCodigoVendedor.MaxLength = 8
        Me.txtCodigoVendedor.Name = "txtCodigoVendedor"
        Me.txtCodigoVendedor.ReadOnly = True
        Me.txtCodigoVendedor.Size = New System.Drawing.Size(80, 20)
        Me.txtCodigoVendedor.TabIndex = 57
        Me.txtCodigoVendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 39)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 16)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Nombres:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(84, 112)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(80, 20)
        Me.dtpFecha.TabIndex = 31
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 115)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 16)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Fec. Recibo:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(4, 194)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(71, 16)
        Me.Label21.TabIndex = 39
        Me.Label21.Text = "Vendedor:"
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Location = New System.Drawing.Point(109, 165)
        Me.txtTipoCambio.MaxLength = 8
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.Size = New System.Drawing.Size(82, 20)
        Me.txtTipoCambio.TabIndex = 47
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 168)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 16)
        Me.Label25.TabIndex = 46
        Me.Label25.Text = "Tipo Cambio:"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblMoneda4)
        Me.GroupBox7.Controls.Add(Me.txtInteresPagoCuota)
        Me.GroupBox7.Controls.Add(Me.lblMoneda3)
        Me.GroupBox7.Controls.Add(Me.Label24)
        Me.GroupBox7.Controls.Add(Me.txtDiferencia)
        Me.GroupBox7.Controls.Add(Me.Label26)
        Me.GroupBox7.Controls.Add(Me.lblMoneda2)
        Me.GroupBox7.Controls.Add(Me.txtPMultiple)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.lblMoneda1)
        Me.GroupBox7.Controls.Add(Me.lblMoneda)
        Me.GroupBox7.Controls.Add(Me.txtOtrosPagos)
        Me.GroupBox7.Controls.Add(Me.Label10)
        Me.GroupBox7.Controls.Add(Me.txtTotalPagar)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.txtInteres)
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Controls.Add(Me.txtCorrelativo)
        Me.GroupBox7.Controls.Add(Me.Label1)
        Me.GroupBox7.Controls.Add(Me.txtTipoCambio)
        Me.GroupBox7.Controls.Add(Me.lblMontoMN)
        Me.GroupBox7.Controls.Add(Me.Label25)
        Me.GroupBox7.Controls.Add(Me.Label18)
        Me.GroupBox7.Controls.Add(Me.cbxTipoMoneda)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Controls.Add(Me.cbxTipoDocumento)
        Me.GroupBox7.Controls.Add(Me.lblMontoME)
        Me.GroupBox7.Controls.Add(Me.txtMontoME)
        Me.GroupBox7.Controls.Add(Me.Label16)
        Me.GroupBox7.Controls.Add(Me.txtMonto)
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.Label8)
        Me.GroupBox7.Controls.Add(Me.cbxConcepto)
        Me.GroupBox7.Controls.Add(Me.cbxTipoPago)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.txtNumLetra)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(418, 89)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(405, 230)
        Me.GroupBox7.TabIndex = 39
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Datos Recibo"
        '
        'lblMoneda4
        '
        Me.lblMoneda4.AutoSize = True
        Me.lblMoneda4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda4.Location = New System.Drawing.Point(252, 63)
        Me.lblMoneda4.Name = "lblMoneda4"
        Me.lblMoneda4.Size = New System.Drawing.Size(0, 16)
        Me.lblMoneda4.TabIndex = 80
        '
        'txtInteresPagoCuota
        '
        Me.txtInteresPagoCuota.BackColor = System.Drawing.SystemColors.Window
        Me.txtInteresPagoCuota.Enabled = False
        Me.txtInteresPagoCuota.Location = New System.Drawing.Point(312, 60)
        Me.txtInteresPagoCuota.MaxLength = 12
        Me.txtInteresPagoCuota.Name = "txtInteresPagoCuota"
        Me.txtInteresPagoCuota.Size = New System.Drawing.Size(90, 20)
        Me.txtInteresPagoCuota.TabIndex = 79
        Me.txtInteresPagoCuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMoneda3
        '
        Me.lblMoneda3.AutoSize = True
        Me.lblMoneda3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda3.Location = New System.Drawing.Point(255, 167)
        Me.lblMoneda3.Name = "lblMoneda3"
        Me.lblMoneda3.Size = New System.Drawing.Size(0, 16)
        Me.lblMoneda3.TabIndex = 77
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(192, 63)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(64, 16)
        Me.Label24.TabIndex = 78
        Me.Label24.Text = "Int.P/Tarj."
        '
        'txtDiferencia
        '
        Me.txtDiferencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiferencia.Enabled = False
        Me.txtDiferencia.Location = New System.Drawing.Point(312, 165)
        Me.txtDiferencia.MaxLength = 12
        Me.txtDiferencia.Name = "txtDiferencia"
        Me.txtDiferencia.Size = New System.Drawing.Size(90, 20)
        Me.txtDiferencia.TabIndex = 76
        Me.txtDiferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(192, 167)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(69, 16)
        Me.Label26.TabIndex = 75
        Me.Label26.Text = "Diferencia"
        '
        'lblMoneda2
        '
        Me.lblMoneda2.AutoSize = True
        Me.lblMoneda2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda2.Location = New System.Drawing.Point(252, 141)
        Me.lblMoneda2.Name = "lblMoneda2"
        Me.lblMoneda2.Size = New System.Drawing.Size(0, 16)
        Me.lblMoneda2.TabIndex = 74
        '
        'txtPMultiple
        '
        Me.txtPMultiple.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMultiple.Enabled = False
        Me.txtPMultiple.Location = New System.Drawing.Point(312, 138)
        Me.txtPMultiple.MaxLength = 12
        Me.txtPMultiple.Name = "txtPMultiple"
        Me.txtPMultiple.Size = New System.Drawing.Size(90, 20)
        Me.txtPMultiple.TabIndex = 73
        Me.txtPMultiple.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(192, 141)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 16)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "P.Multiple"
        '
        'lblMoneda1
        '
        Me.lblMoneda1.AutoSize = True
        Me.lblMoneda1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda1.Location = New System.Drawing.Point(245, 115)
        Me.lblMoneda1.Name = "lblMoneda1"
        Me.lblMoneda1.Size = New System.Drawing.Size(0, 16)
        Me.lblMoneda1.TabIndex = 71
        '
        'lblMoneda
        '
        Me.lblMoneda.AutoSize = True
        Me.lblMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda.Location = New System.Drawing.Point(235, 90)
        Me.lblMoneda.Name = "lblMoneda"
        Me.lblMoneda.Size = New System.Drawing.Size(0, 16)
        Me.lblMoneda.TabIndex = 70
        '
        'txtOtrosPagos
        '
        Me.txtOtrosPagos.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtrosPagos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtrosPagos.Enabled = False
        Me.txtOtrosPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtrosPagos.Location = New System.Drawing.Point(71, 191)
        Me.txtOtrosPagos.MaxLength = 100
        Me.txtOtrosPagos.Name = "txtOtrosPagos"
        Me.txtOtrosPagos.Size = New System.Drawing.Size(331, 20)
        Me.txtOtrosPagos.TabIndex = 69
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 194)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 16)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = "C. Otros:"
        '
        'txtTotalPagar
        '
        Me.txtTotalPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPagar.Enabled = False
        Me.txtTotalPagar.Location = New System.Drawing.Point(312, 112)
        Me.txtTotalPagar.MaxLength = 12
        Me.txtTotalPagar.Name = "txtTotalPagar"
        Me.txtTotalPagar.Size = New System.Drawing.Size(90, 20)
        Me.txtTotalPagar.TabIndex = 66
        Me.txtTotalPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(192, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 16)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "T.Pagar"
        '
        'txtInteres
        '
        Me.txtInteres.BackColor = System.Drawing.SystemColors.Window
        Me.txtInteres.Enabled = False
        Me.txtInteres.Location = New System.Drawing.Point(312, 86)
        Me.txtInteres.MaxLength = 12
        Me.txtInteres.Name = "txtInteres"
        Me.txtInteres.Size = New System.Drawing.Size(90, 20)
        Me.txtInteres.TabIndex = 64
        Me.txtInteres.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(192, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Interés"
        '
        'txtCorrelativo
        '
        Me.txtCorrelativo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorrelativo.Enabled = False
        Me.txtCorrelativo.Location = New System.Drawing.Point(109, 35)
        Me.txtCorrelativo.MaxLength = 12
        Me.txtCorrelativo.Name = "txtCorrelativo"
        Me.txtCorrelativo.ReadOnly = True
        Me.txtCorrelativo.Size = New System.Drawing.Size(83, 20)
        Me.txtCorrelativo.TabIndex = 62
        Me.txtCorrelativo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 16)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "N° Cuota:"
        '
        'lblMontoMN
        '
        Me.lblMontoMN.AutoSize = True
        Me.lblMontoMN.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoMN.Location = New System.Drawing.Point(231, 14)
        Me.lblMontoMN.Name = "lblMontoMN"
        Me.lblMontoMN.Size = New System.Drawing.Size(0, 16)
        Me.lblMontoMN.TabIndex = 60
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1, 89)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(95, 16)
        Me.Label18.TabIndex = 59
        Me.Label18.Text = "T. Documento:"
        '
        'cbxTipoMoneda
        '
        Me.cbxTipoMoneda.Enabled = False
        Me.cbxTipoMoneda.FormattingEnabled = True
        Me.cbxTipoMoneda.Items.AddRange(New Object() {"Soles", "Dolares"})
        Me.cbxTipoMoneda.Location = New System.Drawing.Point(110, 60)
        Me.cbxTipoMoneda.Name = "cbxTipoMoneda"
        Me.cbxTipoMoneda.Size = New System.Drawing.Size(82, 21)
        Me.cbxTipoMoneda.TabIndex = 58
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1, 63)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(61, 16)
        Me.Label17.TabIndex = 56
        Me.Label17.Text = "Moneda:"
        '
        'cbxTipoDocumento
        '
        Me.cbxTipoDocumento.FormattingEnabled = True
        Me.cbxTipoDocumento.Items.AddRange(New Object() {"BV", "FV"})
        Me.cbxTipoDocumento.Location = New System.Drawing.Point(109, 86)
        Me.cbxTipoDocumento.Name = "cbxTipoDocumento"
        Me.cbxTipoDocumento.Size = New System.Drawing.Size(82, 21)
        Me.cbxTipoDocumento.TabIndex = 55
        '
        'lblMontoME
        '
        Me.lblMontoME.AutoSize = True
        Me.lblMontoME.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoME.Location = New System.Drawing.Point(232, 38)
        Me.lblMontoME.Name = "lblMontoME"
        Me.lblMontoME.Size = New System.Drawing.Size(0, 16)
        Me.lblMontoME.TabIndex = 54
        '
        'txtMontoME
        '
        Me.txtMontoME.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoME.Enabled = False
        Me.txtMontoME.Location = New System.Drawing.Point(312, 35)
        Me.txtMontoME.MaxLength = 12
        Me.txtMontoME.Name = "txtMontoME"
        Me.txtMontoME.Size = New System.Drawing.Size(90, 20)
        Me.txtMontoME.TabIndex = 53
        Me.txtMontoME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(192, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 16)
        Me.Label16.TabIndex = 52
        Me.Label16.Text = "Monto"
        '
        'txtMonto
        '
        Me.txtMonto.Enabled = False
        Me.txtMonto.Location = New System.Drawing.Point(312, 11)
        Me.txtMonto.MaxLength = 12
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(90, 20)
        Me.txtMonto.TabIndex = 51
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(192, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(45, 16)
        Me.Label15.TabIndex = 50
        Me.Label15.Text = "Monto"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 16)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Concepto:"
        '
        'cbxConcepto
        '
        Me.cbxConcepto.FormattingEnabled = True
        Me.cbxConcepto.Items.AddRange(New Object() {"Venta Contado", "Amortización Letra", "Cancelación Letra", "Cuota Inicial", "Anticipo Cuota Inicial", "Venta Tarjeta", "Venta Tarjeta Oferta", "Venta Tarjeta Remate", "Venta Oferta", "Venta Remate", "Otros Pagos", "Cobro Interés", "Cargo Operación"})
        Me.cbxConcepto.Location = New System.Drawing.Point(72, 138)
        Me.cbxConcepto.Name = "cbxConcepto"
        Me.cbxConcepto.Size = New System.Drawing.Size(119, 21)
        Me.cbxConcepto.TabIndex = 48
        '
        'cbxTipoPago
        '
        Me.cbxTipoPago.FormattingEnabled = True
        Me.cbxTipoPago.Items.AddRange(New Object() {"Cheque MN", "Cheque ME", "Efectivo MN", "Efectivo ME", "Pago Tarjeta", "Transf/Abono Cta"})
        Me.cbxTipoPago.Location = New System.Drawing.Point(71, 112)
        Me.cbxTipoPago.Name = "cbxTipoPago"
        Me.cbxTipoPago.Size = New System.Drawing.Size(120, 21)
        Me.cbxTipoPago.TabIndex = 36
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1, 115)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(75, 16)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Tipo Pago:"
        '
        'txtNumLetra
        '
        Me.txtNumLetra.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumLetra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNumLetra.Location = New System.Drawing.Point(72, 11)
        Me.txtNumLetra.MaxLength = 8
        Me.txtNumLetra.Name = "txtNumLetra"
        Me.txtNumLetra.ReadOnly = True
        Me.txtNumLetra.Size = New System.Drawing.Size(120, 20)
        Me.txtNumLetra.TabIndex = 43
        Me.txtNumLetra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(1, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(71, 16)
        Me.Label22.TabIndex = 42
        Me.Label22.Text = "N° Cta Cte:"
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
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'lblMsj
        '
        Me.lblMsj.AutoSize = True
        Me.lblMsj.Location = New System.Drawing.Point(2, 323)
        Me.lblMsj.Name = "lblMsj"
        Me.lblMsj.Size = New System.Drawing.Size(0, 13)
        Me.lblMsj.TabIndex = 58
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'frmreciboPago
        '
        Me.AcceptButton = Me.btnBuscaLetra
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(824, 401)
        Me.Controls.Add(Me.lblMsj)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmreciboPago"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Movimientos Caja: Recibo entrada"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNumRecibo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblRUC As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTelefono As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents btnGrabar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnProducto As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDNI As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents btnNuevoCliente As System.Windows.Forms.Button
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents txtNombres As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtNumLetra As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoPago As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbxConcepto As System.Windows.Forms.ComboBox
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMontoME As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents txtCodigoVendedor As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbxTipoMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents txtCodigoCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnBuscaLetra As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents lblMontoMN As System.Windows.Forms.Label
    Friend WithEvents txtCorrelativo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMontoME As System.Windows.Forms.Label
    Friend WithEvents dtpFechaVcmto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInteres As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPagar As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDiasVencidos As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtOtrosPagos As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda1 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda As System.Windows.Forms.Label
    Friend WithEvents txtPMultiple As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda2 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda3 As System.Windows.Forms.Label
    Friend WithEvents txtDiferencia As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblMsj As System.Windows.Forms.Label
    Friend WithEvents txtInteresPagoCuota As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda4 As System.Windows.Forms.Label
    Public WithEvents Timer1 As System.Windows.Forms.Timer
End Class
