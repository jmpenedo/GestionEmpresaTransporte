﻿using System;
using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    internal class TransporteListarPanelCtrl
    {
        private readonly BindingList<Transporte> _bindingList;

        public TransporteListarPanelCtrl(Empresa empresa)
        {
            MiEmpresa = empresa;
            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            TransporteVerPanelCtrl = new TransporteVerPanelCtrl(_bindingList, MiEmpresa);
            View = new TransporteListarPanelView(TransporteVerPanelCtrl.View);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;

            //Asignamos Handlers
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.grdLista.CellDoubleClick += (sender, args) => CeldaSeleccionada();
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.BtSelecCliente.Click += (sender, e) => SeleccionarCliente();

            TransporteVerPanelCtrl._padre = View.grdLista;
        }

        public TransporteListarPanelCtrl(Empresa empresa, MainWindowCtrl controlPrincipal) : this(empresa)
        {
            MainWindowControl = controlPrincipal;
        }

        public TransporteListarPanelView View { get; }
        public TransporteVerPanelCtrl TransporteVerPanelCtrl { get; }
        public Empresa MiEmpresa { get; set; }
        public MainWindowCtrl MainWindowControl { get; set; }

        /// <summary>
        ///     Muestra la información  del cliente seleccionado
        ///     en el panel inferior
        /// </summary>
        private void ActualizarPanelTransporte()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                TransporteVerPanelCtrl.ElTransporte = TransporteSeleccionado;
            }

            View.AjustarColGrid();
        }

        private void CeldaSeleccionada()
        {
            var columna = Math.Max(0, View.grdLista.CurrentCell.ColumnIndex);
            if (columna == 3)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                TransporteVerPanelCtrl.ElTransporte = TransporteSeleccionado;
                var Cliente = TransporteSeleccionado.Cliente;
                MainWindowControl.VerCliente(Cliente);
            }
            else if (columna == 2)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                TransporteVerPanelCtrl.ElTransporte = TransporteSeleccionado;
                var Vehiculo = TransporteSeleccionado.Camion;
                MainWindowControl.VerVehiculo(Vehiculo);
            }
        }

        private void SeleccionarCliente()
        {
            var instanciaClientes = MainWindowControl.getInstanceCliente();

            instanciaClientes.View.pnlCliente.BtSeleccionar.Click +=
                (sender, e) => CambiarCliente(instanciaClientes);
            instanciaClientes.View.pnlCliente.BtVolver.Click +=
                (sender, e) => CambiarCliente(instanciaClientes);
            instanciaClientes.View.pnlCliente.ModoSeleccion(true);
            MainWindowControl.GestionClientes();
        }

        private void CambiarCliente(ClienteListarPanelCtrl instanciaClientes)
        {
            if (instanciaClientes.ElCliente != null) //Caso seleccionar
                TransporteVerPanelCtrl.View.EdCliente.Text = instanciaClientes.ElCliente.Nif;
            else //caso volver
                TransporteVerPanelCtrl.View.EdCliente.Text = "";
        }
    }
}