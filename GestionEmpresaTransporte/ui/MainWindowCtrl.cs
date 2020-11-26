using System;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class MainWindowCtrl
    {
        private const string FClientes = "clientes.xml"; //pasar a config.ini
        public ClientePanelCtrl ctrlPnlCliente;
        public MainWindowCtrl()
        {
            View = new MainWindowView();
            //Creacion de todos los contenedores y recuperacion desde ficheros//
            GestorClientes = new GestorDeClientes(FClientes);
            //GestorVehiculos = new GestorDeVehiculos(FVehiculos);
            //GestorTransportes = new GestorDeTransportes(FTransportes);

            //Asignación de Handlers
            View.Closed += (sender, e) => Salir();
            View.opSalir.Click += (sender, e) => Salir();
            View.opGestionClientes.Click += (sender, e) => GestionClientes();
            View.opPruebaClientes.Click += (sender, e) => PruebaClientes();
            View.opGuardar.Click += (sender, e) => GuardarClientes(FClientes);
            View.opCargar.Click += (sender, e) => CargarClientes(FClientes);

        }

        public GestorDeClientes GestorClientes { get; set; }
        public MainWindowView View { get; }

        private void GestionClientes()
        {
            ctrlPnlCliente = new ClientePanelCtrl(GestorClientes);
            this.View.Controls.Add(ctrlPnlCliente.View);
            /*if (ctrlClientes.View.ShowDialog() == WForms.DialogResult.OK)
            {
                //Acciones para despues de gestionar clientes Guardar los clientes?
            }*/
        }

        private void PruebaClientes()
        {
            ctrlPnlCliente.View.Visible = false;
        }

        /// <summary>
        ///     Permite cargar los clientes desde el nombre de fichero
        ///     pasado como parametro
        /// </summary>
        /// <param name="fClientes">
        ///     <see cref="string" />
        /// </param>
        private void CargarClientes(string fClientes)
        {
            try
            {
                GestorClientes = new GestorDeClientes(fClientes);
                Mensaje(string.Format("Cargados desde {0} un total de {1} clientes", FClientes, GestorClientes.Count));
            }
            catch (Exception e)
            {
                WForms.MessageBox.Show("Se ha producido un error al cargar: " + e.Message);
                GestorClientes = new GestorDeClientes(); //creo un gestor de clientes vacio
                Mensaje("Creado un gestor de clientes vacío");
            }
        }

        /// <summary>
        ///     Guarda los clientes en fichero
        /// </summary>
        /// <param name="fichero">
        ///     <see cref="string" />
        /// </param>
        private void GuardarClientes(string fichero)
        {
            try
            {
                GestorClientes.GuardarXML(fichero);
                Mensaje(string.Format("Guardado en el {0} un total de {1} clientes", FClientes, GestorClientes.Count));
            }
            catch (Exception e)
            {
                WForms.MessageBox.Show("Se ha producido un error al guardar: " + e.Message);
            }
        }

        private void Salir()
        {
            View.Dispose();
        }

        /// <summary>
        ///     Escribe un mesaje en el Status Bar
        /// </summary>
        /// <param name="msg"></param>
        private void Mensaje(string msg)
        {
            View.sbStatus.Text = msg;
        }
    }
}