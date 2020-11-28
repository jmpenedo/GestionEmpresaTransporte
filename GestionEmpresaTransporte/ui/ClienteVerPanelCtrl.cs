using System;
using System.ComponentModel;
using GestionEmpresaTransporte.Core;
using GestionEmpresaTransporte.Utils;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteVerPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar
        }

        private readonly BindingList<Cliente> _bindingList;
        public WForms.Control _padre;
        private Cliente miCliente;

        /// <summary>
        /// </summary>
        /// <param name="gestorDeClientes"></param>
        private ClienteVerPanelCtrl()
        {
            View = new ClienteVerPanelView();
            View.BtInsertar.Click += (sender, e) => InsertarCliente();
            View.BtAceptar.Click += (sender, e) => Aceptar();
            View.BtCancelar.Click += (sender, e) => Cancelar();
            View.BtBorrar.Click += (sender, e) => BorrarCliente();
            View.BtModificar.Click += (sender, e) => View.ModoModificar();
            EstadoPnlCliente = Estados.Consultar;
            View.ModoConsulta();
        }

        public ClienteVerPanelCtrl(GestorDeClientes gestorDeClientes) : this()
        {
            GestorClientes = gestorDeClientes;
        }

        public ClienteVerPanelCtrl(BindingList<Cliente> unaBindingList) : this()
        {
            _bindingList = unaBindingList;
        }

        public ClienteVerPanelCtrl(GestorDeClientes gestorDeClientes, Cliente unCliente) : this(gestorDeClientes)
        {
            ElCliente = unCliente;
        }

        public GestorDeClientes GestorClientes { get; set; }

        public ClienteVerPanelView View { get; }


        public Cliente ElCliente
        {
            get => miCliente;
            set
            {
                miCliente = value;
                ActualizaTextCliente();
            }
        }

        public Estados EstadoPnlCliente { get; set; }


        private void ActualizaTextCliente()
        {
            if (ElCliente != null)
            {
                View.EdNif.Text = ElCliente.Nif;
                View.EdNombre.Text = ElCliente.Nombre;
                View.EdCorreo.Text = ElCliente.Email;
                View.EdDireccion.Text = ElCliente.Dirección;
                View.EdTelefono.Text = ElCliente.Telefono;
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
                    ConsultarCliente();
                    return;
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.ModoConsulta();
            ActualizaTextCliente();
        }

        private void ConsultarCliente()
        {
            throw new NotImplementedException();
        }

        private void BorrarCliente()
        {
            if (ElCliente != null)
            {
                var nif = ElCliente.Nif;
                /*MessageBOX de confirmacion*/
                var message = string.Format("¿Estás seguro de borrar el cliente con identificador: {0}?", nif);
                var caption = "Borrar cliente";
                var buttons = WForms.MessageBoxButtons.YesNo;
                WForms.DialogResult result;
                // Displays the MessageBox.
                result = WForms.MessageBox.Show(message, caption, buttons);
                if (result == WForms.DialogResult.Yes) _bindingList.Remove(ElCliente);

                ActualizarPadre();
                View.ModoConsulta();
            }
        }

        private void InsertarCliente()
        {
            View.ModoInsercion();
            EstadoPnlCliente = Estados.Insertar;
        }

        /// <summary>
        ///     Recoge los datos de los textbox, valida el email y actuliza el objeto
        ///     pasado por referencia con los datos nuevos (NO se puede modificar el NIF)
        /// </summary>
        private void ModificarCliente()
        {
            var nombre = View.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.EdTelefono.Text);
            var correo = View.EdCorreo.Text;
            var direccion = View.EdDireccion.Text;
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
        }

        /// <summary>
        ///     Para ejecutar al terminar de rellenar  el formulario y
        ///     pulsar aceptar. Crea el nuevo cliente
        /// </summary>
        private void AñadirCliente()
        {
            //Recogemos los datos del formulario
            var valido = true;
            var nif = View.EdNif.Text.ToUpper();
            var nombre = View.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.EdTelefono.Text);
            var correo = View.EdCorreo.Text;
            var direccion = View.EdDireccion.Text;

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
                //ActualizarPadre();
                View.ModoConsulta();
            }
        }

        private void ActualizarPadre()
        {
            if (_padre != null)
            {
                _padre.Update();
                _padre.Refresh();
            }
        }
    }
}