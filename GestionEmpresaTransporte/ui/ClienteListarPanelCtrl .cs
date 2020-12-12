using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteListarPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar
        }

        private readonly BindingList<Cliente> _bindingList;
        private Cliente miCliente;

        public ClienteListarPanelCtrl(Empresa unaEmpresa)
        {
            MiEmpresa = unaEmpresa;
            _bindingList = new BindingList<Cliente>(unaEmpresa.ColeccionClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            View = new ClienteListarPanelView();
            //Enlazamos el datagrid con la lista de clientes
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelCliente();
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.pnlCliente.BtInsertar.Click += (sender, e) => InsertarCliente();
            View.pnlCliente.BtAceptar.Click += (sender, e) => Aceptar();
            View.pnlCliente.BtCancelar.Click += (sender, e) => Cancelar();
            View.pnlCliente.BtBorrar.Click += (sender, e) => BorrarCliente();
            View.pnlCliente.BtModificar.Click += (sender, e) => ModoModificar();
            View.pnlCliente.BtVolver.Click += (sender, e) => Volver();
            View.pnlCliente.BtSeleccionar.Click += (sender, e) => Seleccionar();

            EstadoPnlCliente = Estados.Consultar; //Al crearse siempre está en modo consulta
        }

        public Estados EstadoPnlCliente { get; set; }
        public ClienteListarPanelView View { get; }
        private Empresa MiEmpresa { get; }

        public Cliente ElCliente
        {
            get => miCliente;
            set
            {
                miCliente = value;
                ActualizaTextCliente();
            }
        }

        /// <summary>
        ///     Muestra la información  del cliente seleccionado
        ///     en el panel inferior
        /// </summary>
        public void ActualizarPanelCliente()
        {
            if (View.grdLista.Rows.Count > 0 && MiEmpresa.ColeccionClientes.Count > 0)
                //Hay clientes seleccionados
                foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
                {
                    var nif = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                    ElCliente = _bindingList.FirstOrDefault(item => item.Nif == nif);
                }
            else //NO hay cliente seleccionado
                ElCliente = null;

            ActualizaTextCliente();
            View.AjustarColGrid();
        }

        private void ActualizaTextCliente()
        {
            if (ElCliente != null)
            {
                View.pnlCliente.EdNif.Text = ElCliente.Nif;
                View.pnlCliente.EdNombre.Text = ElCliente.Nombre;
                View.pnlCliente.EdCorreo.Text = ElCliente.Email;
                View.pnlCliente.EdDireccion.Text = ElCliente.Dirección;
                View.pnlCliente.EdTelefono.Text = ElCliente.Telefono;
            }
            else
            {
                View.pnlCliente.EdNif.Clear();
                View.pnlCliente.EdNombre.Clear();
                View.pnlCliente.EdCorreo.Clear();
                View.pnlCliente.EdDireccion.Clear();
                View.pnlCliente.EdTelefono.Clear();
            }
        }

        private void Aceptar()
        {
            switch (EstadoPnlCliente)
            {
                case Estados.Insertar:
                    AñadirCliente();
                    return;
                case Estados.Borrar:
                    BorrarCliente();
                    return;
                case Estados.Modificar:
                    ModificarCliente();
                    return;
                case Estados.Consultar:
                    //ConsultarCliente();
                    return;
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.pnlCliente.ModoConsulta();
            ActualizaTextCliente();
        }

        private void ModoModificar()
        {
            EstadoPnlCliente = Estados.Modificar;
            View.pnlCliente.ModoModificar();
        }

        private void BorrarCliente()
        {
            if (ElCliente != null)
            {
                if (!MiEmpresa.ColeccionTransportes.ExisteCliente(ElCliente))
                {
                    var nif = ElCliente.Nif;
                    /*MessageBOX de confirmacion*/
                    var message = string.Format("¿Estás seguro de borrar el cliente con identificador: {0}?", nif);
                    var caption = "Borrar cliente";
                    var buttons = WForms.MessageBoxButtons.YesNo;
                    WForms.DialogResult result;
                    // Displays the MessageBox.
                    result = WForms.MessageBox.Show(message, caption, buttons);
                    if (result == WForms.DialogResult.Yes)
                    {
                        _bindingList.Remove(ElCliente);
                        ActualizarPanelCliente();
                    }
                }
                else
                {
                    WForms.MessageBox.Show("El cliente tiene transportes asignados, no se puede borrar ");
                }
            }

            View.Actualizar();
            View.pnlCliente.ModoConsulta();
            ActualizarPanelCliente();
        }

        private void InsertarCliente()
        {
            View.pnlCliente.ModoInsercion();
            EstadoPnlCliente = Estados.Insertar;
        }

        /// <summary>
        ///     Recoge los datos de los textbox, valida el email y actuliza el objeto
        ///     pasado por referencia con los datos nuevos (NO se puede modificar el NIF)
        /// </summary>
        private void ModificarCliente()
        {
            var nombre = View.pnlCliente.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.pnlCliente.EdTelefono.Text);
            var correo = View.pnlCliente.EdCorreo.Text;
            var direccion = View.pnlCliente.EdDireccion.Text;
            var valido = true;
            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    valido = false;
                }

            //Si las validaciones son correctas se actulizan los datos del cliente
            if (valido)
            {
                ElCliente.Nombre = nombre;
                ElCliente.Telefono = telefono;
                ElCliente.Email = correo;
                ElCliente.Dirección = direccion;
            }

            View.Actualizar(); //ActualizarPadre();
            View.pnlCliente.ModoConsulta();
        }

        /// <summary>
        ///     Para ejecutar al terminar de rellenar  el formulario y
        ///     pulsar aceptar. Crea el nuevo cliente
        /// </summary>
        private void AñadirCliente()
        {
            //Recogemos los datos del formulario
            var valido = true;
            var nif = View.pnlCliente.EdNif.Text.ToUpper();
            var nombre = View.pnlCliente.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.pnlCliente.EdTelefono.Text);
            var correo = View.pnlCliente.EdCorreo.Text;
            var direccion = View.pnlCliente.EdDireccion.Text;

            //El NIF es obligatorio, si no se ponde no se puede dar de alta
            if (!utilidades.valida_NIFCIFNIE(nif))
            {
                WForms.MessageBox.Show("El nif " + nif + " no es correcto");
                valido = false;
            }

            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    valido = false;
                }

            //Si  fue bien se crea un nuevo cliente
            if (valido)
            {
                var nuevoCliente = new Cliente(nif, nombre, telefono, correo, direccion);
                _bindingList.Add(nuevoCliente);
                SeleccionarCliente(nuevoCliente);
                View.pnlCliente.ModoConsulta();
            }
        }

        private void Volver()
        {
            ElCliente = null;
            View.Visible = false;
            View.pnlCliente.ModoConsulta();
            View.pnlCliente.ModoSeleccion(false);
        }

        private void Seleccionar()
        {
            //Antes de volver revisar que hay un cliente seleccionado
            if (ElCliente == null)
            {
                ActualizarPanelCliente(); //Devolvemos el cliente seleccionado en el grid
                if (ElCliente == null)
                    WForms.MessageBox.Show("NO se ha seleccionado ningún cliente"); //No hay clientes en la BD
            }

            View.Visible = false;
            View.pnlCliente.ModoConsulta();
            View.pnlCliente.ModoSeleccion(false);
        }


        public void SeleccionarCliente(Cliente cliente)
        {
            var pos = MiEmpresa.ColeccionClientes
                .PosCliente(cliente); //CtrlpnlCliente.empresa.ColeccionClientes.PosCliente(cliente);

            if (View.grdLista.Columns.Count > 0) View.grdLista.Rows[pos].Selected = true;
        }
    }
}