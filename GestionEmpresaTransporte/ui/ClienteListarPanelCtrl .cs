using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteListarPanelCtrl
    {
        private readonly BindingList<Cliente> _bindingList;

        public ClienteListarPanelCtrl(Empresa unaEmpresa)
        {
            MiEmpresa = unaEmpresa;
            GestorClientes = unaEmpresa.ColeccionClientes;
            _bindingList = new BindingList<Cliente>(GestorClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            //Creamos el control del Panel inferior con los datos del cliente
            clienteVerPanelCtrl = new ClienteVerPanelCtrl(MiEmpresa, _bindingList);
            View = new ClienteListarPanelView(clienteVerPanelCtrl.View);
            //Enlazamos el datagrid con la lista de clientes
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelCliente();
            clienteVerPanelCtrl._padre = View.grdLista;
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
        }
/*
        public ClienteListarPanelCtrl(Empresa unaEmpresa) : this(unaEmpresa.ColeccionClientes)
        {
            MiEmpresa = unaEmpresa;
        }*/


        public ClienteListarPanelView View { get; }
        public ClienteVerPanelCtrl clienteVerPanelCtrl { get; }
        public GestorDeClientes GestorClientes { get; set; }
        private Empresa MiEmpresa { get; }

        /// <summary>
        ///     Muestra la información  del cliente seleccionado
        ///     en el panel inferior
        /// </summary>
        public void ActualizarPanelCliente()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
            {
                var nif = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var ClienteSeleccionado = _bindingList.FirstOrDefault(item => item.Nif == nif);
                clienteVerPanelCtrl.ElCliente = ClienteSeleccionado;
            }

            View.AjustarColGrid();
        }
    }
}