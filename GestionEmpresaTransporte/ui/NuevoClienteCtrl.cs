using System;
using GestionEmpresaTransporte.Core;
using GestionEmpresaTransporte.Utils;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class NuevoClienteCtrl
    {
        private readonly EventHandler handler_añadir;

        public NuevoClienteCtrl()
        {
            View = new NuevoClienteView();

        }

        /// <summary>
        /// Crea un formulario distinto dependiendo del tipo de opción
        /// </summary>
        /// <param name="unCliente">el <see cref="cliente"/> para mostrar/modificar</param>
        /// <param name="opcion">0:añadir 1:modificar 2:consulta</param>
        public NuevoClienteCtrl(Cliente unCliente) : this()
        {
            if (unCliente != null)
            {
                View.EdNif.Text = unCliente.Nif;
                View.EdNombre.Text = unCliente.Nombre;
                View.EdTelefono.Text = unCliente.Telefono;
                View.EdCorreo.Text = unCliente.Email;
                View.EdDireccion.Text = unCliente.Dirección;
                ElCliente = unCliente;
            }
            View.ModoConsulta();
            
                   
               
            
        }

        public Cliente ElCliente { get; set; }
        public NuevoClienteView View { get; }

        /*/// <summary>
        ///     Recoge los datos de los textbox, valida el email y actuliza el objeto
        ///     pasado por referencia con los datos nuevos (NO se puede modificar el NIF)
        /// </summary>
        private void ModificarCliente()
        {
            var nombre = View.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.EdTelefono.Text);
            var correo = View.EdCorreo.Text;
            var direccion = View.EdDireccion.Text;

            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    View.DialogResult = WForms.DialogResult.None;
                }

            //Si las validaciones son correctas se actulizan los datos del cliente
            if (View.DialogResult == WForms.DialogResult.OK)
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
            var nif = View.EdNif.Text.ToUpper();
            var nombre = View.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.EdTelefono.Text);
            var correo = View.EdCorreo.Text;
            var direccion = View.EdDireccion.Text;

            //El NIF es obligatorio, si no se ponde no se puede dar de alta
            if (!utilidades.valida_NIFCIFNIE(nif))
            {
                WForms.MessageBox.Show("El nif " + nif + " no es correcto");
                View.DialogResult = WForms.DialogResult.None;
            }

            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    View.DialogResult = WForms.DialogResult.None;
                }

            //Si  fue bien se crea un nuevo cliente
            if (View.DialogResult == WForms.DialogResult.OK)
                ElCliente = new Cliente(nif, nombre, telefono, correo, direccion);
        }*/
    }
}