﻿using System.ComponentModel;
using GestionEmpresaTransporte.Core;

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
        public ClienteListarPanelView _padre;
        private Cliente miCliente;

        /// <summary>
        /// </summary>
        /// <param name="gestorDeClientes"></param>
        public ClienteVerPanelCtrl(Empresa laEmpresa)
        {
            GestorClientes = laEmpresa.ColeccionClientes;
            cTransportes = laEmpresa.ColeccionTransportes;
            //_bindingList = new BindingList<Cliente>(GestorClientes.Clientes);
            View = new ClienteVerPanelView();
            View.BtInsertar.Click += (sender, e) => InsertarCliente();
            View.BtAceptar.Click += (sender, e) => Aceptar();
            View.BtCancelar.Click += (sender, e) => Cancelar();
            View.BtBorrar.Click += (sender, e) => BorrarCliente();
            View.BtModificar.Click += (sender, e) => ModoModificar();
            View.BtVolver.Click += (sender, e) => Volver();
            View.BtSeleccionar.Click += (sender, e) => Seleccionar();
            EstadoPnlCliente = Estados.Consultar;
            View.ModoConsulta();
        }

        /*
        public ClienteVerPanelCtrl(BindingList<Cliente> unaBindingList) : this()
        {
            _bindingList = unaBindingList;
        }
*/
        public ClienteVerPanelCtrl(Empresa laEmpresa, Cliente unCliente) : this(laEmpresa)
        {
            ElCliente = unCliente;
        }

        public ClienteVerPanelCtrl(Empresa laEmpresa, BindingList<Cliente> bndListClientes) : this(laEmpresa)
        {
            _bindingList = bndListClientes;
        }

        public GestorDeClientes GestorClientes { get; set; }
        public ColeccionTransportes cTransportes { get; set; }

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

        private void Seleccionar()
        {
            _padre.Visible = false;
        }

        private void Volver()
        {
            ElCliente = null;
            _padre.Visible = false;
        }

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
                    //ConsultarCliente();
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

        private void ModoModificar()
        {
            EstadoPnlCliente = Estados.Modificar;
            View.ModoModificar();
        }

        private void BorrarCliente()
        {
            if (ElCliente != null)
            {
                if (!cTransportes.ExisteCliente(ElCliente))
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
                }
                else
                {
                    WForms.MessageBox.Show("El cliente tiene transportes asignados, no se puede borrar ");
                }

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

            ActualizarPadre();
            View.ModoConsulta();
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
            if (_padre != null) _padre.Actualizar();
        }
    }
}