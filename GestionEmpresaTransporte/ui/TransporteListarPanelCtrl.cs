using System;
using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class TransporteListarPanelCtrl
    {
        private BindingList<Transporte> _bindingList;

        public TransporteListarPanelCtrl(Empresa empresa)
        {
            MiEmpresa = empresa;
            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            TransporteVerPanelCtrl = new TransporteVerPanelCtrl(_bindingList, MiEmpresa);
            View = new TransporteListarPanelView(TransporteVerPanelCtrl.View);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();

            //Asignamos Handlers
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.grdLista.CellDoubleClick += (sender, args) => CeldaSeleccionada();
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.BtSelecCliente.Click += (sender, e) => SeleccionarCliente();
            TransporteVerPanelCtrl.View.BtSelecVehiculo.Click += (sender, e) => SeleccionarVehiculo();
            TransporteVerPanelCtrl.View.BtPendiente.Click += (sender, e) => ListarPendiente();
            TransporteVerPanelCtrl.View.BtVolver.Click += (sender, e) => Volver();
            TransporteVerPanelCtrl.View.BtFiltroFecha.Click += (sender, e) => ListarFecha();
            TransporteVerPanelCtrl.View.BtFiltroYear.Click += (sender, e) => ListarYear();
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
        public void ActualizarPanelTransporte()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                TransporteVerPanelCtrl.ElTransporte = TransporteSeleccionado;

                foreach (WForms.DataGridViewCell cell in row.Cells)
                    if (cell.ColumnIndex == 3)
                        cell.ToolTipText = "Doble click para más información del cliente";
                    else if (cell.ColumnIndex == 2) cell.ToolTipText = "Doble click para más información del vehículo";
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
            instanciaClientes.View.grdLista.CellDoubleClick +=
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

        private void SeleccionarVehiculo()
        {
            var instanciaVehiculos = MainWindowControl.getInstanceVehiculo();
            instanciaVehiculos.seleccion = true;
            instanciaVehiculos.View.pnlVehiculo.BtSeleccionar.Click +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.pnlVehiculo.BtVolver.Click +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.grdLista.CellDoubleClick +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.pnlVehiculo.ModoSeleccion(true);
            MainWindowControl.GestionVehiculos();
        }

        private void CambiarVehiculo(VehiculoListarPanelCtrl instanciaVehiculos)
        {
            if (instanciaVehiculos.ElVehiculo != null) //Caso seleccionar
                TransporteVerPanelCtrl.View.EdFlota.Text = instanciaVehiculos.ElVehiculo.Matricula;
            else //caso volver
                TransporteVerPanelCtrl.View.EdFlota.Text = "";
            instanciaVehiculos.seleccion = false;
        }

        private void ListarPendiente()
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.Pendiente().ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoVolver();
        }

        private void ListarYear()
        {
            var filtro = TransporteVerPanelCtrl.ObtenerYear();
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCamion(filtro).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoVolver();
        }

        private void ListarFecha()
        {
            var filtro = TransporteVerPanelCtrl.ObtenerFecha();
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasDia(filtro).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoVolver();
        }

        private void Volver()
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoConsulta();
        }

        public void ListarReservasCliente(string nif)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCliente(nif).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoVolver();
        }

        public void ListarReservasCliente(string nif, int year)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCliente(nif, year).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ActualizarPanelTransporte();
            TransporteVerPanelCtrl.View.ModoVolver();
        }
    }
}