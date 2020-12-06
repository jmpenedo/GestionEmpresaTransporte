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

        public ClienteListarPanelCtrl(GestorDeClientes gClientes)
        {
            GestorClientes = gClientes;
            _bindingList = new BindingList<Cliente>(GestorClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            //Creamos el control del Panel inferior con los datos del cliente
            clienteVerPanelCtrl = new ClienteVerPanelCtrl(_bindingList);
            View = new ClienteListarPanelView(clienteVerPanelCtrl.View);
            //Enlazamos el datagrid con la lista de clientes
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelCliente();
            clienteVerPanelCtrl._padre = View.grdLista;
        }

        public ClienteListarPanelView View { get; }
        public ClienteVerPanelCtrl clienteVerPanelCtrl { get; }
        public GestorDeClientes GestorClientes { get; set; }

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