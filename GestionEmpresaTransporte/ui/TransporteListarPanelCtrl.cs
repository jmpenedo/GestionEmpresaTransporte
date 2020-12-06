namespace GestionEmpresaTransporte.ui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using GestionEmpresaTransporte.Core;
    using WForms = System.Windows.Forms;

    using GestionEmpresaTransporte.Core.Vehiculos;

    class TransporteListarPanelCtrl
    {
        private readonly BindingList<Transporte> _bindingList;

        public TransporteListarPanelCtrl(Empresa empresa)
        {
            MiEmpresa = empresa;

            Cliente cliente1 = new Cliente("44660662K", "David", "650597495", "Encarnacion", "david@hotmail.com");
            Cliente cliente2 = new Cliente("44660663E", "Monica", "650597495", "Encarnacion", "monica@hotmail.com");

            Vehiculo vehiculo1 = new Camion("9999aaa", DateTime.Now, DateTime.Today, 0.1, "aaaa", "aaaa", new List<string> { "aire" } );
            Vehiculo vehiculo2 = new Camion("8888bbb", DateTime.Now, DateTime.Today, 0.1, "bbbb", "bbbb", new List<string> { "aire" });


            MiEmpresa.ColeccionClientes.Add(cliente1);
            MiEmpresa.ColeccionClientes.Add(cliente2);
            MiEmpresa.ColeccionVehiculos.Add(vehiculo1);
            MiEmpresa.ColeccionVehiculos.Add(vehiculo2);

            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            TransporteVerPanelCtrl = new TransporteVerPanelCtrl(_bindingList, MiEmpresa);
            View = new TransporteListarPanelView(TransporteVerPanelCtrl.View);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;

            //Asignamos Handlers
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.grdLista.CellDoubleClick += (sender, args) => this.CeldaSeleccionada();
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelTransporte();
            
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
            int columna = System.Math.Max(0, View.grdLista.CurrentCell.ColumnIndex);
            if (columna == 3)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                TransporteVerPanelCtrl.ElTransporte = TransporteSeleccionado;
                var Cliente = TransporteSeleccionado.Cliente;
                this.MainWindowControl.VerCliente(Cliente);
            }
            else if (columna == 2)
            {
                //TODO 
            }

            return;
        }
    }
}
