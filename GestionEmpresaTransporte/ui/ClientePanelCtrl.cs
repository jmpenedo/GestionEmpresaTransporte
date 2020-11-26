using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClientePanelCtrl
    {
        private readonly BindingList<Cliente> _bindingList;
    public NuevoClienteCtrl pnlClienteCtrl; 

        /// <summary>
        ///     Constructor pensado para usar en el módulo
        ///     de gestión de clientes de manera independiente
        /// </summary>
        /// <param name="gClientes"></param>
        public ClientePanelCtrl(GestorDeClientes gClientes)
        {
            View = new ClientePanelView();
            GestorClientes = gClientes;
            //Enlazamos el datagrid con la lista de clientes
            _bindingList = new BindingList<Cliente>(GestorClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarEdDireccion();
       }

        public ClientePanelView View { get; }
        public GestorDeClientes GestorClientes { get; set; }

    /// <summary>
        ///     Actuliza la caja de texto de dirección con la información
        ///     del cliente seleccionado
        /// </summary>
        private void ActualizarEdDireccion()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
                View.EdDireccion.Text = row.Cells[4].Value.ToString();
        }
       
  
      
        
        
    }
}