Public Class frmbarraMenu
    Private Sub frmMenuPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim oFrmAccesoAdministrador As New frmAccesoAdministrador()
        'If oFrmAcceso.ShowDialog = DialogResult.OK Then
        'End If
    End Sub
    Private Sub mnuArchivoConfigurar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArchivoConfigurar.Click
        Dim ofrmconfigurarPapel As New frmconfigurarPapel()
        ofrmconfigurarPapel.MdiParent = Me
        ofrmconfigurarPapel.Show()
    End Sub
    Private Sub mnuFacturacionBoleta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionBoleta.Click, tsbboletaVenta.Click
        Dim ofrmboletaVenta As New frmboletaVenta()
        ofrmboletaVenta.MdiParent = Me
        ofrmboletaVenta.Show()
    End Sub
    Private Sub mnuFacturacionFactura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionFactura.Click, tsbfacturaVenta.Click
        Dim ofrmfacturaVenta As New frmfacturaVenta()
        ofrmfacturaVenta.MdiParent = Me
        ofrmfacturaVenta.Show()
    End Sub
    Private Sub mnuFacturacionAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionAnular.Click
        Dim ofrmanularDocumentoVta As New frmanularDocumentoVta()
        ofrmanularDocumentoVta.MdiParent = Me
        ofrmanularDocumentoVta.Show()
    End Sub
    Private Sub mnuFacturacionNotaCreditoExtorno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaCreditoExtorno.Click
        Dim ofrmnotaCreditoNoPago As New frmnotaCreditoNoPago()
        ofrmnotaCreditoNoPago.MdiParent = Me
        ofrmnotaCreditoNoPago.Show()
    End Sub
    Private Sub mnuFacturacionNotaCreditoFallas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaCreditoFallas.Click
        Dim ofrmnotaCreditoFallas As New frmnotaCreditoFallas()
        ofrmnotaCreditoFallas.MdiParent = Me
        ofrmnotaCreditoFallas.Show()
    End Sub
    Private Sub mnuFacturacionNotaCreditoCambio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaCreditoCambio.Click
        Dim ofrmnotaCreditoCambio As New frmnotaCreditoCambio()
        ofrmnotaCreditoCambio.MdiParent = Me
        ofrmnotaCreditoCambio.Show()
    End Sub
    Private Sub mnuFacturacionNotaCreditoInicial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaCreditoInicial.Click
        Dim ofrmnotaCreditoInicial As New frmnotaCreditoInicial()
        ofrmnotaCreditoInicial.MdiParent = Me
        ofrmnotaCreditoInicial.Show()
    End Sub
    Private Sub mnuFacturacionNotaCreditoAnulacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaCreditoAnulacion.Click
        Dim ofrmanularNotaCredito As New frmanularNotaCredito()
        ofrmanularNotaCredito.MdiParent = Me
        ofrmanularNotaCredito.Show()
    End Sub
    Private Sub mnuFacturacionNotaDebitoGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaDebitoGenerar.Click
        Dim ofrmnotaDebito As New frmnotaDebito()
        ofrmnotaDebito.MdiParent = Me
        ofrmnotaDebito.Show()
    End Sub
    Private Sub mnuFacturacionNotaDebitoAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFacturacionNotaDebitoAnular.Click
        Dim ofrmanularNotaDebito As New frmanularNotaDebito()
        ofrmanularNotaDebito.MdiParent = Me
        ofrmanularNotaDebito.Show()
    End Sub
    Private Sub mnuCajaRecibos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajaRecibos.Click, tsbReciboEntrada.Click
        Dim ofrmreciboPago As New frmreciboPago()
        ofrmreciboPago.MdiParent = Me
        ofrmreciboPago.Show()
    End Sub
    Private Sub mnuCajaRecibosSalida_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajaRecibosSalida.Click, tsbReciboSalida.Click
        Dim ofrmreciboSalida As New frmreciboSalida()
        ofrmreciboSalida.MdiParent = Me
        ofrmreciboSalida.Show()
    End Sub
    Private Sub mnuCajaCierreGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajaCierreGeneral.Click, tsbCierreCaja.Click
        Dim ofrmcierreCaja As New frmcierreCaja()
        ofrmcierreCaja.MdiParent = Me
        ofrmcierreCaja.Show()
    End Sub
    Private Sub mnuCajaCierreRangos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajaCierreRangos.Click, tsbCierreCajaRango.Click
        Dim ofrmcierreCajaRangos As New frmcierreCajaRangos()
        ofrmcierreCajaRangos.MdiParent = Me
        ofrmcierreCajaRangos.Show()
    End Sub
    Private Sub mnuCajaAnularRecibo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajaAnularRecibo.Click
        Dim ofrmanularRecibo As New frmanularRecibo()
        ofrmanularRecibo.MdiParent = Me
        ofrmanularRecibo.Show()
    End Sub
    Private Sub mnuCajaAnularReciboSalida_Click(sender As System.Object, e As System.EventArgs) Handles mnuCajaAnularReciboSalida.Click
        Dim ofrmanularReciboSalida As New frmanularReciboSalida()
        ofrmanularReciboSalida.MdiParent = Me
        ofrmanularReciboSalida.Show()
    End Sub
    Private Sub mnuCtaCtesLetrasCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCtesLetrasCodigo.Click, tsbLetrasCodigo.Click
        Dim ofrmconsultaLetrasCod As New frmconsultaLetrasCod()
        ofrmconsultaLetrasCod.MdiParent = Me
        ofrmconsultaLetrasCod.Show()
    End Sub
    Private Sub mnuCtaCteLetrasNombre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCteLetrasNombre.Click, tsbLetrasNombre.Click
        Dim ofrmconsultaLetrasNom As New frmconsultaLetrasNom()
        ofrmconsultaLetrasNom.MdiParent = Me
        ofrmconsultaLetrasNom.Show()
    End Sub
    Private Sub mnuCtaCteCobranzasCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCteCobranzasCodigo.Click, tsbRecibosCodigo.Click
        Dim ofrmconsultaRecibosCod As New frmconsultaRecibosCod()
        ofrmconsultaRecibosCod.MdiParent = Me
        ofrmconsultaRecibosCod.Show()
    End Sub
    Private Sub mnuCtaCteCobranzasNombre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCteCobranzasNombre.Click, tsbRecibosNombre.Click
        Dim ofrmconsultaRecibosNom As New frmconsultaRecibosNom()
        ofrmconsultaRecibosNom.MdiParent = Me
        ofrmconsultaRecibosNom.Show()
    End Sub
    Private Sub mnuCtaCtesPagoMultiple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCtesPagoMultiple.Click, tsbLetrasVarias.Click
        Dim ofrmconsultaLetrasMul As New frmconsultaLetrasMul()
        ofrmconsultaLetrasMul.MdiParent = Me
        ofrmconsultaLetrasMul.Show()
    End Sub
    Private Sub mnuCtaCtesEvaluaCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCtesEvaluaCliente.Click
        Dim ofrmevaluaCliente As New frmevaluaCliente()
        ofrmevaluaCliente.MdiParent = Me
        ofrmevaluaCliente.Show()
    End Sub
    Private Sub mnuCtaCtesAntecedenteCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCtaCtesAntecedenteCliente.Click
        Dim ofrmantecedenteCliente As New frmantecedenteCliente()
        ofrmantecedenteCliente.MdiParent = Me
        ofrmantecedenteCliente.Show()
    End Sub
    Private Sub mnuInventarioEntrada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioEntrada.Click, tsbGuiaEntrada.Click
        Dim ofrmguiaRemisionEN As New frmguiaRemisionEN()
        ofrmguiaRemisionEN.MdiParent = Me
        ofrmguiaRemisionEN.Show()
    End Sub
    Private Sub mnuInventariosSalida_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosSalida.Click, tsbGuiaSalida.Click
        Dim ofrmguiaRemisionSa As New frmguiaRemisionSA()
        ofrmguiaRemisionSa.MdiParent = Me
        ofrmguiaRemisionSa.Show()
    End Sub
    Private Sub mnuInventariosAjustarPrecios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosAjustarPrecios.Click
        Dim ofrmmodificaPrecios As New frmmodificaPrecios()
        ofrmmodificaPrecios.MdiParent = Me
        ofrmmodificaPrecios.Show()
    End Sub
    Private Sub mnuInventariosSaldos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosSaldos.Click, tsbSaldos.Click
        Dim ofrmsaldosAlmacen As New frmsaldoAlmacen()
        ofrmsaldosAlmacen.MdiParent = Me
        ofrmsaldosAlmacen.Show()
    End Sub
    Private Sub mnuInventariosIniciarSaldos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosIniciarSaldos.Click
        Dim ofrmIniciarSaldos As New frminiciarSaldos()
        ofrmIniciarSaldos.MdiParent = Me
        ofrmIniciarSaldos.Show()
    End Sub
    Private Sub mnuInventariosAjustarStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosAjustarStock.Click
        Dim ofrmajustarStock As New frmajustarStock
        ofrmajustarStock.MdiParent = Me
        ofrmajustarStock.Show()
    End Sub
    Private Sub mnuInventariosAnularDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosAnularDocumento.Click
        Dim ofrmAnularGuia As New frmanularGuia
        ofrmAnularGuia.MdiParent = Me
        ofrmAnularGuia.Show()
    End Sub
    Private Sub mnuInventariosKardexProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosKardexProducto.Click
        Dim ofrmkardexProducto As New frmkardexProducto
        ofrmkardexProducto.MdiParent = Me
        ofrmkardexProducto.Show()
    End Sub
    Private Sub mnuInventariosKardexRango_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosKardexRango.Click
        Dim ofrmkardexProductoRango As New frmkardexProductoRango
        ofrmkardexProductoRango.MdiParent = Me
        ofrmkardexProductoRango.Show()
    End Sub
    Private Sub mnuInventariosKardexSimple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosKardexSimple.Click
        Dim ofrmkardexProductoSimple As New frmkardexProductoSimple
        ofrmkardexProductoSimple.MdiParent = Me
        ofrmkardexProductoSimple.Show()
    End Sub
    Private Sub mnuInventariosBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosBalance.Click
        Dim ofrmBalance As New frmBalance()
        ofrmBalance.MdiParent = Me
        ofrmBalance.Show()
    End Sub
    Private Sub mnuInventariosCierreDiario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventariosCierreDiario.Click
        Dim ofrmcierreDiario As New frmcierreDiario
        ofrmcierreDiario.MdiParent = Me
        ofrmcierreDiario.Show()
    End Sub
    Private Sub mnuUtilitarioClientesNuevos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioClientesNuevos.Click
        Dim ofrmnuevoCliente As New frmnuevoCliente()
        ofrmnuevoCliente.MdiParent = Me
        ofrmnuevoCliente.Show()
    End Sub
    Private Sub mnuUtilitarioClientesEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioClientesEditar.Click
        Dim ofrmeditarClientes As New frmEditarClientes()
        ofrmeditarClientes.MdiParent = Me
        ofrmeditarClientes.Show()
    End Sub
    Private Sub mnuUtilitarioGarantesCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioGarantesCrear.Click
        Dim ofrmcrearGarantes As New frmcrearGarantes()
        ofrmcrearGarantes.MdiParent = Me
        ofrmcrearGarantes.Show()
    End Sub
    Private Sub mnuUtilitarioGarantesEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioGarantesEditar.Click
        Dim ofrmeditarGarantes As New frmeditarGarantes()
        ofrmeditarGarantes.MdiParent = Me
        ofrmeditarGarantes.Show()
    End Sub
    Private Sub mnuUtilitarioProductosNuevos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioProductosNuevos.Click
        Dim ofrmnuevoProducto As New frmnuevoProducto()
        ofrmnuevoProducto.MdiParent = Me
        ofrmnuevoProducto.Show()
    End Sub
    Private Sub mnuUtilitarioProductosEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioProductosEditar.Click
        Dim ofrmeditarProductos As New frmeditarProductos()
        ofrmeditarProductos.MdiParent = Me
        ofrmeditarProductos.Show()
    End Sub
    Private Sub mnuUtilitarioSeriesEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioSeriesEditar.Click
        Dim ofrmeditarSeries As New frmeditarSeries()
        ofrmeditarSeries.MdiParent = Me
        ofrmeditarSeries.Show()
    End Sub
    Private Sub mnuUtilitarioProveedoresNuevos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioProveedoresNuevos.Click
        Dim ofrmnuevoProveedor As New frmnuevoProveedor()
        ofrmnuevoProveedor.MdiParent = Me
        ofrmnuevoProveedor.Show()
    End Sub
    Private Sub mnuUtilitarioVendedoresNuevos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioVendedoresNuevos.Click
        Dim ofrmnuevoVendedor As New frmnuevoVendedor()
        ofrmnuevoVendedor.MdiParent = Me
        ofrmnuevoVendedor.Show()
    End Sub
    Private Sub mnuUtilitarioVendedoresEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioVendedoresEditar.Click
        Dim ofrmeditarVendedores As New frmeditarVendedores()
        ofrmeditarVendedores.MdiParent = Me
        ofrmeditarVendedores.Show()
    End Sub
    Private Sub mnuUtilitarioCobradoresNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCobradoresNuevo.Click
        Dim ofrmnuevoCobrador As New frmnuevoCobrador()
        ofrmnuevoCobrador.MdiParent = Me
        ofrmnuevoCobrador.Show()
    End Sub
    Private Sub mnuUtilitarioCobradoresEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCobradoresEditar.Click
        Dim ofrmeditarCobradores As New frmeditarCobradores()
        ofrmeditarCobradores.MdiParent = Me
        ofrmeditarCobradores.Show()
    End Sub
    Private Sub mnuUtilitarioUsuariosNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioUsuariosNuevo.Click
        Dim ofrmnuevoUsuario As New frmnuevoUsuario()
        ofrmnuevoUsuario.MdiParent = Me
        ofrmnuevoUsuario.Show()
    End Sub
    Private Sub mnuUtilitarioUsuariosEditar_Click(sender As System.Object, e As System.EventArgs) Handles mnuUtilitarioUsuariosEditar.Click
        Dim ofrmeditarUsuarios As New frmeditarUsuarios()
        ofrmeditarUsuarios.MdiParent = Me
        ofrmeditarUsuarios.Show()
    End Sub
    Private Sub mnuUtilitarioPersonalNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioPersonalNuevo.Click
        Dim ofrmnuevoPersonal As New frmnuevoPersonal()
        ofrmnuevoPersonal.MdiParent = Me
        ofrmnuevoPersonal.Show()
    End Sub
    Private Sub mnuUtilitarioPersonalEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioPersonalEditar.Click
        Dim ofrmeditarPersonal As New frmeditarPersonal()
        ofrmeditarPersonal.MdiParent = Me
        ofrmeditarPersonal.Show()
    End Sub
    Private Sub mnuUtilitarioCuotasCrear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCuotasCrear.Click
        Dim ofrmcreaLetras As New frmcreaLetras()
        ofrmcreaLetras.MdiParent = Me
        ofrmcreaLetras.Show()
    End Sub
    Private Sub mnuUtilitarioCuotasCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCuotasCancelar.Click
        Dim ofrmcancelarCuotas As New frmcancelarCuotas()
        ofrmcancelarCuotas.MdiParent = Me
        ofrmcancelarCuotas.Show()
    End Sub
    Private Sub mnuUtilitarioCuotasEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCuotasEliminar.Click
        Dim ofrmeliminarCuotas As New frmeliminarCuotas()
        ofrmeliminarCuotas.MdiParent = Me
        ofrmeliminarCuotas.Show()
    End Sub
    Private Sub mnuUtilitarioCuotasNAAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCuotasNAAnular.Click
        Dim ofrmanularCancelacionNA As New frmanularCancelacionNA()
        ofrmanularCancelacionNA.MdiParent = Me
        ofrmanularCancelacionNA.Show()
    End Sub
    Private Sub mnuUtilitarioCuotasNACancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUtilitarioCuotasNACancelar.Click
        Dim ofrmcancelarCuotasNA As New frmcancelarCuotasNA()
        ofrmcancelarCuotasNA.MdiParent = Me
        ofrmcancelarCuotasNA.Show()
    End Sub
    Private Sub mnuHistoricoLetrasCod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHistoricoLetrasCod.Click
        Dim ofrmhistoricoLetrasCod As New frmhistoricoLetrasCod()
        ofrmhistoricoLetrasCod.MdiParent = Me
        ofrmhistoricoLetrasCod.Show()
    End Sub
    Private Sub mnuHistoricoLetrasNom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHistoricoLetrasNom.Click
        Dim ofrmhistoricoLetrasNom As New frmhistoricoLetrasNom()
        ofrmhistoricoLetrasNom.MdiParent = Me
        ofrmhistoricoLetrasNom.Show()
    End Sub
    Private Sub mnuHistoricoRecibosCod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHistoricoRecibosCod.Click
        Dim ofrmhistoricoRecibosCod As New frmhistoricoRecibosCod()
        ofrmhistoricoRecibosCod.MdiParent = Me
        ofrmhistoricoRecibosCod.Show()
    End Sub
    Private Sub mnuHistoricoRecibosNom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHistoricoRecibosNom.Click
        Dim ofrmhistoricoRecibosNom As New frmhistoricoRecibosNom()
        ofrmhistoricoRecibosNom.MdiParent = Me
        ofrmhistoricoRecibosNom.Show()
    End Sub
    Private Sub mnuDocumentosEmitidos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDocumentosEmitidos.Click, tsbDocsEmitidos.Click
        Dim ofrmdocumentosEmitidos As New frmdocumentosEmitidos()
        ofrmdocumentosEmitidos.MdiParent = Me
        ofrmdocumentosEmitidos.Show()
    End Sub
    Private Sub mnuMovimientosAlmacenDiarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMovimientosAlmacenDiarios.Click
        Dim ofrmmovimientosAlm As New frmmovimientosAlm()
        ofrmmovimientosAlm.MdiParent = Me
        ofrmmovimientosAlm.Show()
    End Sub
    Private Sub mnuMovimientosAlmacenRangos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMovimientosAlmacenRangos.Click
        Dim ofrmMovimientosAlmRangos As New frmMovimientosAlmRangos()
        ofrmMovimientosAlmRangos.MdiParent = Me
        ofrmMovimientosAlmRangos.Show()
    End Sub
    Private Sub mnuReporteCuotasGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteCuotasGeneral.Click
        Dim ofrmconsultaLetrasGen As New frmconsultaLetrasGen()
        ofrmconsultaLetrasGen.MdiParent = Me
        ofrmconsultaLetrasGen.Show()
    End Sub
    Private Sub mnuReporteCuotasVencidas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteCuotasVencidas.Click
        Dim ofrmconsultaLetrasVencidas As New frmconsultaLetrasVencidas()
        ofrmconsultaLetrasVencidas.MdiParent = Me
        ofrmconsultaLetrasVencidas.Show()
    End Sub
    Private Sub mnuReporteCuotasRangos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteCuotasRangos.Click
        Dim ofrmconsultaLetrasRangos As New frmconsultaLetrasRangos()
        ofrmconsultaLetrasRangos.MdiParent = Me
        ofrmconsultaLetrasRangos.Show()
    End Sub
    Private Sub mnuReporteCuotasFechas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteCuotasFechas.Click
        Dim ofrmconsultaLetrasFecha As New frmconsultaLetrasFecha()
        ofrmconsultaLetrasFecha.MdiParent = Me
        ofrmconsultaLetrasFecha.Show()
    End Sub
    Private Sub mnuReporteCuotasAnno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteCuotasAnno.Click
        Dim ofrmconsultaLetrasAnno As New frmconsultaLetrasAnno()
        ofrmconsultaLetrasAnno.MdiParent = Me
        ofrmconsultaLetrasAnno.Show()
    End Sub
    Private Sub mnuReporteVentasMes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteVentasMes.Click
        Dim ofrmreporteVentasMes As New frmreporteVentasMes()
        ofrmreporteVentasMes.MdiParent = Me
        ofrmreporteVentasMes.Show()
    End Sub
    Private Sub mnuReporteVentasAncho_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteVentasAncho.Click
        Dim ofrmreporteVentasTodoMes As New frmreporteVentasTodoMes()
        ofrmreporteVentasTodoMes.MdiParent = Me
        ofrmreporteVentasTodoMes.Show()
    End Sub
    Private Sub mnuReporteVentasRango_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteVentasRango.Click
        Dim ofrmreporteVentasRango As New frmreporteVentasRango()
        ofrmreporteVentasRango.MdiParent = Me
        ofrmreporteVentasRango.Show()
    End Sub
    Private Sub mnuReporteComisionVendedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteComisionVendedor.Click
        Dim ofrmcomisionVendedor As New frmcomisionVendedor()
        ofrmcomisionVendedor.MdiParent = Me
        ofrmcomisionVendedor.Show()
    End Sub
    Private Sub mnuConfiguracionTasaCredito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracionTasaCredito.Click
        Dim ofrmtasasCredito As New frmtasasCredito()
        ofrmtasasCredito.MdiParent = Me
        ofrmtasasCredito.Show()
    End Sub
    Private Sub mnuConfiguracionTipoCambio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracionTipoCambio.Click
        Dim ofrmtipoCambio As New frmtipoCambio()
        ofrmtipoCambio.MdiParent = Me
        ofrmtipoCambio.Show()
    End Sub
    Private Sub mnuComisionPagoAtrasado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComisionPagoAtrasado.Click
        Dim ofrmcostosFijos As New frmcostosFijos()
        ofrmcostosFijos.MdiParent = Me
        ofrmcostosFijos.Show()
    End Sub
    Private Sub mnuConfiguracionTasasVendedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracionTasasVendedor.Click
        Dim ofrmtasasVendedores As New frmtasasVendedores()
        ofrmtasasVendedores.MdiParent = Me
        ofrmtasasVendedores.Show()
    End Sub
    Private Sub mnuConfiguracionNumerosDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracionNumerosDocumento.Click
        Dim ofrmnumerosDocumentos As New frmnumerosDocumentos()
        ofrmnumerosDocumentos.MdiParent = Me
        ofrmnumerosDocumentos.Show()
    End Sub
    Private Sub mnuConfiguracionBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracionBackup.Click
        Dim ofrmfrmbackupBD As New frmbackupBD()
        ofrmfrmbackupBD.MdiParent = Me
        ofrmfrmbackupBD.Show()
    End Sub
    Private Sub mnuConfiguracionImportarDatosExcel_Click(sender As System.Object, e As System.EventArgs) Handles mnuConfiguracionImportarDatosExcel.Click
        Dim ofrmExcelToSqlServer As New frmExcelToSqlServer()
        ofrmExcelToSqlServer.MdiParent = Me
        ofrmExcelToSqlServer.Show()
    End Sub
    Private Sub mnuConfiguracionLimpiarInterfazSunat_Click(sender As System.Object, e As System.EventArgs) Handles mnuConfiguracionLimpiarInterfazSunat.Click
        Dim ofrmVaciarInterfazSunat As New frmVaciarInterfazSunat()
        ofrmVaciarInterfazSunat.MdiParent = Me
        ofrmVaciarInterfazSunat.Show()
    End Sub
    Private Sub tsbCalculator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCalculator.Click
        Try
            Dim Proceso As New Process()
            Proceso.StartInfo.FileName = "calc.exe"
            Proceso.StartInfo.Arguments = ""
            Proceso.Start()
        Catch xErr As Exception
            MsgBox(xErr.Message)
        End Try
    End Sub
    Private Sub mnuArchivoSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArchivoSalir.Click
        Me.Close()
    End Sub
End Class
