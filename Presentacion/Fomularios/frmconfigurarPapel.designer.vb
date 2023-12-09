<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmconfigurarPapel
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
        Me.prdImpresoras = New System.Windows.Forms.PrintDialog
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.lblImpresoraActual = New System.Windows.Forms.Label
        Me.btnSeleccionarImpresora = New System.Windows.Forms.Button
        Me.prdDocumento = New System.Drawing.Printing.PrintDocument
        Me.SuspendLayout()
        '
        'prdImpresoras
        '
        Me.prdImpresoras.UseEXDialog = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(179, 222)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(79, 23)
        Me.btnCerrar.TabIndex = 19
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'lblImpresoraActual
        '
        Me.lblImpresoraActual.AutoSize = True
        Me.lblImpresoraActual.Location = New System.Drawing.Point(12, 77)
        Me.lblImpresoraActual.Name = "lblImpresoraActual"
        Me.lblImpresoraActual.Size = New System.Drawing.Size(53, 13)
        Me.lblImpresoraActual.TabIndex = 18
        Me.lblImpresoraActual.Text = "(Ninguna)"
        Me.lblImpresoraActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSeleccionarImpresora
        '
        Me.btnSeleccionarImpresora.Location = New System.Drawing.Point(12, 24)
        Me.btnSeleccionarImpresora.Name = "btnSeleccionarImpresora"
        Me.btnSeleccionarImpresora.Size = New System.Drawing.Size(177, 23)
        Me.btnSeleccionarImpresora.TabIndex = 12
        Me.btnSeleccionarImpresora.Text = "Seleccionar Impresora"
        Me.btnSeleccionarImpresora.UseVisualStyleBackColor = True
        '
        'frmconfigurarPapel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 258)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.lblImpresoraActual)
        Me.Controls.Add(Me.btnSeleccionarImpresora)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmconfigurarPapel"
        Me.Text = "Módulo Definición Papel e Impresora"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents prdImpresoras As System.Windows.Forms.PrintDialog
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents lblImpresoraActual As System.Windows.Forms.Label
    Friend WithEvents btnSeleccionarImpresora As System.Windows.Forms.Button
    Friend WithEvents prdDocumento As System.Drawing.Printing.PrintDocument
End Class
