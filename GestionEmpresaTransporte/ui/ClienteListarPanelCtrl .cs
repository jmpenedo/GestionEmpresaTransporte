using System.ComponentModel;
using System.Diagnostics;
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

        public bool seleccion;
        
        private readonly BindingList<Cliente> _bindingList;
        private Cliente miCliente;

        public ClienteListarPanelCtrl(Empresa unaEmpresa)
        {
            MiEmpresa = unaEmpresa;
            seleccion = false;
            _bindingList = new BindingList<Cliente>(unaEmpresa.ColeccionClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            View = new ClienteListarPanelView();
            //Enlazamos el datagrid con la lista de clientes
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelCliente();
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.pnlCliente.BtInsertar.Click += (sender, e) => ModoInsertarCliente();
            View.pnlCliente.BtAceptar.Click += (sender, e) => Aceptar();
            View.pnlCliente.BtCancelar.Click += (sender, e) => Cancelar();
            View.pnlCliente.BtBorrar.Click += (sender, e) => BorrarCliente();
            View.pnlCliente.BtModificar.Click += (sender, e) => ModoModificar();
            View.pnlCliente.BtVolver.Click += (sender, e) => Volver();
            View.pnlCliente.BtSeleccionar.Click += (sender, e) => Seleccionar();
            
            View.grdLista.CellDoubleClick += (sender, args) => CeldaSeleccionada();

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

        /// <summary>
        ///     Accion a realizar cuando se pulsa ACEPTAR
        /// </summary>
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
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.pnlCliente.ModoInicial();
            ActualizaTextCliente();
        }


        /// <summary>
        ///     Borra  ElCliente actual
        /// </summary>
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
                        _bindingList.Remove(ElCliente);
                    //ActualizarPanelCliente();
                }
                else
                {
                    WForms.MessageBox.Show("El cliente tiene transportes asignados, no se puede borrar ");
                }
            }

            View.Actualizar();
            //View.pnlCliente.ModoConsulta();
            //ActualizarPanelCliente();
        }

        private void ModoInsertarCliente()
        {
            EstadoPnlCliente = Estados.Insertar;
            View.pnlCliente.ModoInsercion();
        }

        /// <summary>
        ///     Iniciamos el proceso de modificacion del ElCliente actual
        /// </summary>
        private void ModoModificar()
        {
            if (ElCliente != null)
            {
                EstadoPnlCliente = Estados.Modificar;
                View.pnlCliente.ModoModificar();
            }
        }

        /// <summary>
        ///     Recoge los datos de los textbox, valida el email y actualiza ElCliente
        ///     con los datos nuevos (NO se puede modificar el NIF)
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
                if (ElCliente != null)
                {
                    ElCliente.Nombre = nombre;
                    ElCliente.Telefono = telefono;
                    ElCliente.Email = correo;
                    ElCliente.Dirección = direccion;
                    View.Actualizar();
                    View.pnlCliente.ModoInicial();
                }
                else
                {
                    Trace.WriteLine("Error intentando modificar cliente NULL");
                }
            }
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

            if (MiEmpresa.ColeccionClientes.getClientebyNif(nif) != null)
            {
                WForms.MessageBox.Show("Ya existe un cliente con el NIF: " + nif);
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
                View.pnlCliente.ModoInicial();
            }
        }

        /// <summary>
        ///     Accion que se ejecuta al pulsar el boton Volver
        /// </summary>
        private void Volver()
        {
            ElCliente = null;
            View.SendToBack(); //Vuelve al anterior panel llamado
            View.pnlCliente.ModoInicial();
            View.pnlCliente.ModoSeleccion(false);
        }

        /// <summary>
        ///     Accion que se ejecuta al pulsar el boton Seleccionar
        /// </summary>
        private void Seleccionar()
        {
            //Antes de volver revisar que hay un cliente seleccionado
            if (ElCliente == null)
            {
                ActualizarPanelCliente(); //Devolvemos el cliente seleccionado en el grid
                if (ElCliente == null)
                    WForms.MessageBox.Show("NO se ha seleccionado ningún cliente"); //No hay clientes en la BD
            }

            View.SendToBack();
            View.pnlCliente.ModoInicial();
            View.pnlCliente.ModoSeleccion(false);
        }

        /// <summary>
        ///     Selecciona en el gdrLista de clientes un cliente pasado como parámetro
        /// </summary>
        /// <param name="cliente"></param>
        public void SeleccionarCliente(Cliente cliente)
        {
            View.pnlCliente.BtVolver.Visible = true;
            var pos = MiEmpresa.ColeccionClientes
                .PosCliente(cliente); //CtrlpnlCliente.empresa.ColeccionClientes.PosCliente(cliente);

            if (View.grdLista.Columns.Count > 0) View.grdLista.Rows[pos].Selected = true;
        }
        private void CeldaSeleccionada()
        {
            if (seleccion)
            {
                ActualizarPanelCliente(); //Devolvemos el vehiculo seleccionado en el grid
                if (ElCliente == null)
                    WForms.MessageBox.Show("No se ha seleccionado ningún cleinte"); //No hay cloentes en la BD

                seleccion = false;
                View.SendToBack();
                View.pnlCliente.ModoInicial();
                View.pnlCliente.ModoSeleccion(false);
            }

        }
        
    }
}