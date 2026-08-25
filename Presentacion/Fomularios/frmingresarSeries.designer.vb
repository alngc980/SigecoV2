<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmingresarSeries
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.dgvSeries = New System.Windows.Forms.DataGridView()
        Me.item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.numSerie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.numMotor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.numChasis = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.color = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.anoFab = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPrecioCF = New System.Windows.Forms.TextBox()
        Me.txtCostoUnitario = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCant = New System.Windows.Forms.TextBox()
        Me.txtCostoDisponibles = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPrecioContado = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPrecioCredito = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPrecioOferta = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPrecioRemate = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtMinimo4 = New System.Windows.Forms.TextBox()
        Me.txtMinimo3 = New System.Windows.Forms.TextBox()
        Me.txtMinimo2 = New System.Windows.Forms.TextBox()
        Me.txtMinimo1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtMaximo1 = New System.Windows.Forms.TextBox()
        Me.txtMaximo2 = New System.Windows.Forms.TextBox()
        Me.txtMaximo3 = New System.Windows.Forms.TextBox()
        Me.txtMaximo4 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtValor = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnProcesarPrecios = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtCosProm = New System.Windows.Forms.TextBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSalir)
        Me.GroupBox2.Controls.Add(Me.btnAceptar)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 387)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(745, 50)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = Global.Presentacion.My.Resources.Resources.FillRightHS
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalir.Location = New System.Drawing.Point(472, 13)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 30)
        Me.btnSalir.TabIndex = 3
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = Global.Presentacion.My.Resources.Resources.CheckSpellingHS
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptar.Location = New System.Drawing.Point(163, 13)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(100, 30)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'dgvSeries
        '
        Me.dgvSeries.AllowUserToAddRows = False
        Me.dgvSeries.AllowUserToResizeColumns = False
        Me.dgvSeries.AllowUserToResizeRows = False
        Me.dgvSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSeries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.item, Me.numSerie, Me.numMotor, Me.numChasis, Me.color, Me.anoFab, Me.eliminar})
        Me.dgvSeries.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvSeries.Location = New System.Drawing.Point(10, 211)
        Me.dgvSeries.Name = "dgvSeries"
        Me.dgvSeries.RowHeadersVisible = False
        Me.dgvSeries.Size = New System.Drawing.Size(745, 170)
        Me.dgvSeries.TabIndex = 1
        '
        'item
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.item.DefaultCellStyle = DataGridViewCellStyle1
        Me.item.HeaderText = "N°"
        Me.item.MaxInputLength = 3
        Me.item.Name = "item"
        Me.item.ReadOnly = True
        Me.item.Width = 50
        '
        'numSerie
        '
        Me.numSerie.HeaderText = "Número Serie"
        Me.numSerie.MaxInputLength = 25
        Me.numSerie.Name = "numSerie"
        Me.numSerie.Width = 150
        '
        'numMotor
        '
        Me.numMotor.HeaderText = "Número Motor"
        Me.numMotor.MaxInputLength = 25
        Me.numMotor.Name = "numMotor"
        Me.numMotor.Width = 150
        '
        'numChasis
        '
        Me.numChasis.HeaderText = "Número Chásis"
        Me.numChasis.MaxInputLength = 25
        Me.numChasis.Name = "numChasis"
        Me.numChasis.Width = 150
        '
        'color
        '
        Me.color.HeaderText = "Color"
        Me.color.MaxInputLength = 10
        Me.color.Name = "color"
        '
        'anoFab
        '
        Me.anoFab.HeaderText = "Año Fab."
        Me.anoFab.MaxInputLength = 4
        Me.anoFab.Name = "anoFab"
        Me.anoFab.Width = 80
        '
        'eliminar
        '
        Me.eliminar.HeaderText = "Borrar"
        Me.eliminar.Name = "eliminar"
        Me.eliminar.Width = 50
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.Location = New System.Drawing.Point(7, 188)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(0, 13)
        Me.lblMensaje.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(228, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Precio CF:"
        '
        'txtPrecioCF
        '
        Me.txtPrecioCF.Location = New System.Drawing.Point(290, 6)
        Me.txtPrecioCF.Name = "txtPrecioCF"
        Me.txtPrecioCF.Size = New System.Drawing.Size(76, 20)
        Me.txtPrecioCF.TabIndex = 26
        Me.txtPrecioCF.Text = "0"
        Me.txtPrecioCF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCostoUnitario
        '
        Me.txtCostoUnitario.Location = New System.Drawing.Point(570, 6)
        Me.txtCostoUnitario.Name = "txtCostoUnitario"
        Me.txtCostoUnitario.ReadOnly = True
        Me.txtCostoUnitario.Size = New System.Drawing.Size(76, 20)
        Me.txtCostoUnitario.TabIndex = 28
        Me.txtCostoUnitario.Text = "0"
        Me.txtCostoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(488, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Costo Unitario:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(382, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Cantidad:"
        '
        'txtCant
        '
        Me.txtCant.Location = New System.Drawing.Point(440, 6)
        Me.txtCant.Name = "txtCant"
        Me.txtCant.ReadOnly = True
        Me.txtCant.Size = New System.Drawing.Size(42, 20)
        Me.txtCant.TabIndex = 28
        Me.txtCant.Text = "0"
        Me.txtCant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCostoDisponibles
        '
        Me.txtCostoDisponibles.Location = New System.Drawing.Point(140, 6)
        Me.txtCostoDisponibles.Name = "txtCostoDisponibles"
        Me.txtCostoDisponibles.Size = New System.Drawing.Size(76, 20)
        Me.txtCostoDisponibles.TabIndex = 30
        Me.txtCostoDisponibles.Text = "0"
        Me.txtCostoDisponibles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Monto Total Disponibles:"
        '
        'txtPrecioContado
        '
        Me.txtPrecioContado.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrecioContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioContado.Location = New System.Drawing.Point(80, 107)
        Me.txtPrecioContado.Name = "txtPrecioContado"
        Me.txtPrecioContado.Size = New System.Drawing.Size(85, 21)
        Me.txtPrecioContado.TabIndex = 32
        Me.txtPrecioContado.Text = "0"
        Me.txtPrecioContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Precios"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(199, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Credito"
        '
        'txtPrecioCredito
        '
        Me.txtPrecioCredito.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrecioCredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioCredito.Location = New System.Drawing.Point(179, 107)
        Me.txtPrecioCredito.Name = "txtPrecioCredito"
        Me.txtPrecioCredito.Size = New System.Drawing.Size(85, 21)
        Me.txtPrecioCredito.TabIndex = 32
        Me.txtPrecioCredito.Text = "0"
        Me.txtPrecioCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(302, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Oferta"
        '
        'txtPrecioOferta
        '
        Me.txtPrecioOferta.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrecioOferta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioOferta.Location = New System.Drawing.Point(278, 107)
        Me.txtPrecioOferta.Name = "txtPrecioOferta"
        Me.txtPrecioOferta.Size = New System.Drawing.Size(85, 21)
        Me.txtPrecioOferta.TabIndex = 32
        Me.txtPrecioOferta.Text = "0"
        Me.txtPrecioOferta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(394, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Remate"
        '
        'txtPrecioRemate
        '
        Me.txtPrecioRemate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrecioRemate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioRemate.Location = New System.Drawing.Point(377, 107)
        Me.txtPrecioRemate.Name = "txtPrecioRemate"
        Me.txtPrecioRemate.Size = New System.Drawing.Size(85, 21)
        Me.txtPrecioRemate.TabIndex = 32
        Me.txtPrecioRemate.Text = "0"
        Me.txtPrecioRemate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(91, 129)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Contado"
        '
        'txtMinimo4
        '
        Me.txtMinimo4.Location = New System.Drawing.Point(377, 81)
        Me.txtMinimo4.Name = "txtMinimo4"
        Me.txtMinimo4.Size = New System.Drawing.Size(85, 20)
        Me.txtMinimo4.TabIndex = 35
        Me.txtMinimo4.Text = "14.5"
        Me.txtMinimo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMinimo3
        '
        Me.txtMinimo3.Location = New System.Drawing.Point(278, 81)
        Me.txtMinimo3.Name = "txtMinimo3"
        Me.txtMinimo3.Size = New System.Drawing.Size(85, 20)
        Me.txtMinimo3.TabIndex = 36
        Me.txtMinimo3.Text = "8"
        Me.txtMinimo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMinimo2
        '
        Me.txtMinimo2.Location = New System.Drawing.Point(179, 81)
        Me.txtMinimo2.Name = "txtMinimo2"
        Me.txtMinimo2.Size = New System.Drawing.Size(85, 20)
        Me.txtMinimo2.TabIndex = 37
        Me.txtMinimo2.Text = "32"
        Me.txtMinimo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMinimo1
        '
        Me.txtMinimo1.Location = New System.Drawing.Point(80, 81)
        Me.txtMinimo1.Name = "txtMinimo1"
        Me.txtMinimo1.Size = New System.Drawing.Size(85, 20)
        Me.txtMinimo1.TabIndex = 38
        Me.txtMinimo1.Text = "19"
        Me.txtMinimo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 84)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 13)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "%Minimo"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "% Maximo"
        '
        'txtMaximo1
        '
        Me.txtMaximo1.Location = New System.Drawing.Point(80, 55)
        Me.txtMaximo1.Name = "txtMaximo1"
        Me.txtMaximo1.Size = New System.Drawing.Size(85, 20)
        Me.txtMaximo1.TabIndex = 38
        Me.txtMaximo1.Text = "29"
        Me.txtMaximo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMaximo2
        '
        Me.txtMaximo2.Location = New System.Drawing.Point(179, 55)
        Me.txtMaximo2.Name = "txtMaximo2"
        Me.txtMaximo2.Size = New System.Drawing.Size(85, 20)
        Me.txtMaximo2.TabIndex = 37
        Me.txtMaximo2.Text = "37"
        Me.txtMaximo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMaximo3
        '
        Me.txtMaximo3.Location = New System.Drawing.Point(278, 55)
        Me.txtMaximo3.Name = "txtMaximo3"
        Me.txtMaximo3.Size = New System.Drawing.Size(85, 20)
        Me.txtMaximo3.TabIndex = 36
        Me.txtMaximo3.Text = "11"
        Me.txtMaximo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMaximo4
        '
        Me.txtMaximo4.Location = New System.Drawing.Point(377, 55)
        Me.txtMaximo4.Name = "txtMaximo4"
        Me.txtMaximo4.Size = New System.Drawing.Size(85, 20)
        Me.txtMaximo4.TabIndex = 35
        Me.txtMaximo4.Text = "17.5"
        Me.txtMaximo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(40, 32)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 13)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Valor"
        '
        'txtValor
        '
        Me.txtValor.Location = New System.Drawing.Point(80, 29)
        Me.txtValor.Name = "txtValor"
        Me.txtValor.Size = New System.Drawing.Size(85, 20)
        Me.txtValor.TabIndex = 38
        Me.txtValor.Text = "250"
        Me.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(291, 32)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "Descuento"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtMaximo4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtMinimo4)
        Me.GroupBox1.Controls.Add(Me.txtPrecioContado)
        Me.GroupBox1.Controls.Add(Me.txtMaximo3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtMinimo3)
        Me.GroupBox1.Controls.Add(Me.txtPrecioCredito)
        Me.GroupBox1.Controls.Add(Me.txtMaximo2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtValor)
        Me.GroupBox1.Controls.Add(Me.txtPrecioOferta)
        Me.GroupBox1.Controls.Add(Me.txtMaximo1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPrecioRemate)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtMinimo2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtMinimo1)
        Me.GroupBox1.Location = New System.Drawing.Point(173, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(481, 159)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Calculo de precios"
        '
        'btnProcesarPrecios
        '
        Me.btnProcesarPrecios.Location = New System.Drawing.Point(660, 68)
        Me.btnProcesarPrecios.Name = "btnProcesarPrecios"
        Me.btnProcesarPrecios.Size = New System.Drawing.Size(75, 23)
        Me.btnProcesarPrecios.TabIndex = 40
        Me.btnProcesarPrecios.Text = "Procesar"
        Me.btnProcesarPrecios.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(652, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Prom:"
        '
        'txtCosProm
        '
        Me.txtCosProm.Location = New System.Drawing.Point(685, 6)
        Me.txtCosProm.Name = "txtCosProm"
        Me.txtCosProm.ReadOnly = True
        Me.txtCosProm.Size = New System.Drawing.Size(70, 20)
        Me.txtCosProm.TabIndex = 28
        Me.txtCosProm.Text = "0"
        Me.txtCosProm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmingresarSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 452)
        Me.Controls.Add(Me.btnProcesarPrecios)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtCostoDisponibles)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCant)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCosProm)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtCostoUnitario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPrecioCF)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.dgvSeries)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmingresarSeries"
        Me.Text = "Módulo Ingreso Números de Serie y Motor"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvSeries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents dgvSeries As System.Windows.Forms.DataGridView
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numSerie As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numMotor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents numChasis As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents anoFab As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioCF As System.Windows.Forms.TextBox
    Friend WithEvents txtCostoUnitario As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCant As System.Windows.Forms.TextBox
    Friend WithEvents txtCostoDisponibles As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioContado As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioCredito As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioOferta As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioRemate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMinimo4 As System.Windows.Forms.TextBox
    Friend WithEvents txtMinimo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMinimo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMinimo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtMaximo1 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximo3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMaximo4 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtValor As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnProcesarPrecios As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtCosProm As System.Windows.Forms.TextBox
End Class
