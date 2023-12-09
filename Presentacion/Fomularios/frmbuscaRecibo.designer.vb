<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbuscaRecibo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvClientes = New System.Windows.Forms.DataGridView()
        Me.numRecibo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.concepto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.importeME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fecEmision = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.moneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idCliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.txtBuscaCliente = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.AdelantosVentaContado = New System.Windows.Forms.CheckBox()
        Me.OtrosPagos = New System.Windows.Forms.CheckBox()
        Me.VentaRemate = New System.Windows.Forms.CheckBox()
        Me.VentaOferta = New System.Windows.Forms.CheckBox()
        Me.VentaTarjetaRemate = New System.Windows.Forms.CheckBox()
        Me.VentaTarjeta = New System.Windows.Forms.CheckBox()
        Me.VentaTarjetaOferta = New System.Windows.Forms.CheckBox()
        Me.AnticipoCuota = New System.Windows.Forms.CheckBox()
        Me.CuotaInicial = New System.Windows.Forms.CheckBox()
        Me.VentaContado = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        CType(Me.dgvClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvClientes
        '
        Me.dgvClientes.AllowUserToAddRows = False
        Me.dgvClientes.AllowUserToDeleteRows = False
        Me.dgvClientes.AllowUserToResizeColumns = False
        Me.dgvClientes.AllowUserToResizeRows = False
        Me.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClientes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.numRecibo, Me.concepto, Me.importe, Me.importeME, Me.fecEmision, Me.moneda, Me.idCliente, Me.nombre, Me.status})
        Me.dgvClientes.Location = New System.Drawing.Point(2, 37)
        Me.dgvClientes.Name = "dgvClientes"
        Me.dgvClientes.RowHeadersVisible = False
        Me.dgvClientes.Size = New System.Drawing.Size(790, 210)
        Me.dgvClientes.TabIndex = 2
        '
        'numRecibo
        '
        Me.numRecibo.HeaderText = "NumRec"
        Me.numRecibo.Name = "numRecibo"
        Me.numRecibo.ReadOnly = True
        Me.numRecibo.Width = 70
        '
        'concepto
        '
        Me.concepto.HeaderText = "Concepto"
        Me.concepto.Name = "concepto"
        Me.concepto.ReadOnly = True
        Me.concepto.Width = 120
        '
        'importe
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.importe.DefaultCellStyle = DataGridViewCellStyle1
        Me.importe.HeaderText = "Importe MN"
        Me.importe.Name = "importe"
        Me.importe.ReadOnly = True
        Me.importe.Width = 87
        '
        'importeME
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.importeME.DefaultCellStyle = DataGridViewCellStyle2
        Me.importeME.HeaderText = "Importe ME"
        Me.importeME.Name = "importeME"
        Me.importeME.ReadOnly = True
        Me.importeME.Width = 87
        '
        'fecEmision
        '
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.fecEmision.DefaultCellStyle = DataGridViewCellStyle3
        Me.fecEmision.FillWeight = 80.0!
        Me.fecEmision.HeaderText = "Fec Emisión"
        Me.fecEmision.Name = "fecEmision"
        Me.fecEmision.ReadOnly = True
        Me.fecEmision.Width = 90
        '
        'moneda
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.moneda.DefaultCellStyle = DataGridViewCellStyle4
        Me.moneda.HeaderText = "Moneda"
        Me.moneda.Name = "moneda"
        Me.moneda.Width = 50
        '
        'idCliente
        '
        Me.idCliente.FillWeight = 80.0!
        Me.idCliente.HeaderText = "Código"
        Me.idCliente.Name = "idCliente"
        Me.idCliente.ReadOnly = True
        Me.idCliente.Visible = False
        Me.idCliente.Width = 70
        '
        'nombre
        '
        Me.nombre.HeaderText = "Nombre Cliente"
        Me.nombre.Name = "nombre"
        Me.nombre.ReadOnly = True
        Me.nombre.Width = 200
        '
        'status
        '
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.Width = 43
        '
        'txtBuscaCliente
        '
        Me.txtBuscaCliente.BackColor = System.Drawing.Color.White
        Me.txtBuscaCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscaCliente.Location = New System.Drawing.Point(193, 7)
        Me.txtBuscaCliente.Name = "txtBuscaCliente"
        Me.txtBuscaCliente.Size = New System.Drawing.Size(570, 20)
        Me.txtBuscaCliente.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(181, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Apellidos y Nombres Cliente:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.AdelantosVentaContado)
        Me.GroupBox1.Controls.Add(Me.OtrosPagos)
        Me.GroupBox1.Controls.Add(Me.VentaRemate)
        Me.GroupBox1.Controls.Add(Me.VentaOferta)
        Me.GroupBox1.Controls.Add(Me.VentaTarjetaRemate)
        Me.GroupBox1.Controls.Add(Me.VentaTarjeta)
        Me.GroupBox1.Controls.Add(Me.VentaTarjetaOferta)
        Me.GroupBox1.Controls.Add(Me.AnticipoCuota)
        Me.GroupBox1.Controls.Add(Me.CuotaInicial)
        Me.GroupBox1.Controls.Add(Me.VentaContado)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 261)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(790, 53)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'AdelantosVentaContado
        '
        Me.AdelantosVentaContado.AutoSize = True
        Me.AdelantosVentaContado.Enabled = False
        Me.AdelantosVentaContado.Location = New System.Drawing.Point(602, 30)
        Me.AdelantosVentaContado.Name = "AdelantosVentaContado"
        Me.AdelantosVentaContado.Size = New System.Drawing.Size(184, 17)
        Me.AdelantosVentaContado.TabIndex = 14
        Me.AdelantosVentaContado.Text = "Recibos Adelanto Venta Contado"
        Me.AdelantosVentaContado.UseVisualStyleBackColor = True
        '
        'OtrosPagos
        '
        Me.OtrosPagos.AutoSize = True
        Me.OtrosPagos.Location = New System.Drawing.Point(462, 31)
        Me.OtrosPagos.Name = "OtrosPagos"
        Me.OtrosPagos.Size = New System.Drawing.Size(126, 17)
        Me.OtrosPagos.TabIndex = 13
        Me.OtrosPagos.Text = "Recibos Otros Pagos"
        Me.OtrosPagos.UseVisualStyleBackColor = True
        '
        'VentaRemate
        '
        Me.VentaRemate.AutoSize = True
        Me.VentaRemate.Location = New System.Drawing.Point(316, 31)
        Me.VentaRemate.Name = "VentaRemate"
        Me.VentaRemate.Size = New System.Drawing.Size(136, 17)
        Me.VentaRemate.TabIndex = 11
        Me.VentaRemate.Text = "Recibos Venta Remate"
        Me.VentaRemate.UseVisualStyleBackColor = True
        '
        'VentaOferta
        '
        Me.VentaOferta.AutoSize = True
        Me.VentaOferta.Location = New System.Drawing.Point(182, 31)
        Me.VentaOferta.Name = "VentaOferta"
        Me.VentaOferta.Size = New System.Drawing.Size(128, 17)
        Me.VentaOferta.TabIndex = 10
        Me.VentaOferta.Text = "Recibos Venta Oferta"
        Me.VentaOferta.UseVisualStyleBackColor = True
        '
        'VentaTarjetaRemate
        '
        Me.VentaTarjetaRemate.AutoSize = True
        Me.VentaTarjetaRemate.Location = New System.Drawing.Point(6, 31)
        Me.VentaTarjetaRemate.Name = "VentaTarjetaRemate"
        Me.VentaTarjetaRemate.Size = New System.Drawing.Size(172, 17)
        Me.VentaTarjetaRemate.TabIndex = 9
        Me.VentaTarjetaRemate.Text = "Recibos Venta Tarjeta Remate"
        Me.VentaTarjetaRemate.UseVisualStyleBackColor = True
        '
        'VentaTarjeta
        '
        Me.VentaTarjeta.AutoSize = True
        Me.VentaTarjeta.Location = New System.Drawing.Point(462, 11)
        Me.VentaTarjeta.Name = "VentaTarjeta"
        Me.VentaTarjeta.Size = New System.Drawing.Size(132, 17)
        Me.VentaTarjeta.TabIndex = 8
        Me.VentaTarjeta.Text = "Recibos Venta Tarjeta"
        Me.VentaTarjeta.UseVisualStyleBackColor = True
        '
        'VentaTarjetaOferta
        '
        Me.VentaTarjetaOferta.AutoSize = True
        Me.VentaTarjetaOferta.Location = New System.Drawing.Point(602, 11)
        Me.VentaTarjetaOferta.Name = "VentaTarjetaOferta"
        Me.VentaTarjetaOferta.Size = New System.Drawing.Size(164, 17)
        Me.VentaTarjetaOferta.TabIndex = 7
        Me.VentaTarjetaOferta.Text = "Recibos Venta Tarjeta Oferta"
        Me.VentaTarjetaOferta.UseVisualStyleBackColor = True
        '
        'AnticipoCuota
        '
        Me.AnticipoCuota.AutoSize = True
        Me.AnticipoCuota.Location = New System.Drawing.Point(316, 11)
        Me.AnticipoCuota.Name = "AnticipoCuota"
        Me.AnticipoCuota.Size = New System.Drawing.Size(137, 17)
        Me.AnticipoCuota.TabIndex = 6
        Me.AnticipoCuota.Text = "Recibos Anticipo Cuota"
        Me.AnticipoCuota.UseVisualStyleBackColor = True
        '
        'CuotaInicial
        '
        Me.CuotaInicial.AutoSize = True
        Me.CuotaInicial.Location = New System.Drawing.Point(182, 11)
        Me.CuotaInicial.Name = "CuotaInicial"
        Me.CuotaInicial.Size = New System.Drawing.Size(126, 17)
        Me.CuotaInicial.TabIndex = 5
        Me.CuotaInicial.Text = "Recibos Cuota Inicial"
        Me.CuotaInicial.UseVisualStyleBackColor = True
        '
        'VentaContado
        '
        Me.VentaContado.AutoSize = True
        Me.VentaContado.Location = New System.Drawing.Point(6, 11)
        Me.VentaContado.Name = "VentaContado"
        Me.VentaContado.Size = New System.Drawing.Size(139, 17)
        Me.VentaContado.TabIndex = 4
        Me.VentaContado.Text = "Recibos Venta Contado"
        Me.VentaContado.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCancelar)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 320)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(790, 50)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Image = My.Resources.DeleteHS
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.Location = New System.Drawing.Point(464, 13)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(90, 30)
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = My.Resources.TaskHS
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptar.Location = New System.Drawing.Point(196, 13)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(90, 30)
        Me.btnAceptar.TabIndex = 9
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.Location = New System.Drawing.Point(3, 250)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(0, 13)
        Me.lblMensaje.TabIndex = 23
        '
        'frmbuscaRecibo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(794, 371)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvClientes)
        Me.Controls.Add(Me.txtBuscaCliente)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmbuscaRecibo"
        Me.Text = "Módulo Búsqueda Recibos"
        CType(Me.dgvClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvClientes As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscaCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents AnticipoCuota As System.Windows.Forms.CheckBox
    Friend WithEvents CuotaInicial As System.Windows.Forms.CheckBox
    Friend WithEvents VentaContado As System.Windows.Forms.CheckBox
    Friend WithEvents VentaTarjetaOferta As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents VentaTarjeta As System.Windows.Forms.CheckBox
    Friend WithEvents OtrosPagos As System.Windows.Forms.CheckBox
    Friend WithEvents VentaRemate As System.Windows.Forms.CheckBox
    Friend WithEvents VentaOferta As System.Windows.Forms.CheckBox
    Friend WithEvents VentaTarjetaRemate As System.Windows.Forms.CheckBox
    Friend WithEvents numRecibo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents concepto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents importeME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fecEmision As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idCliente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AdelantosVentaContado As System.Windows.Forms.CheckBox
End Class
