using System;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    internal class MainWindowCtrl
    {
        private static ClienteListarPanelCtrl CtrlpnlCliente;
        private static TransporteListarPanelCtrl CtrlpnlTransporte;
        private static VehiculoListarPanelCtrl CtrlpnlVehiculo;

        public MainWindowCtrl()
        {
            View = new MainWindowView();
            empresa = new Empresa();
            empresa.CargarXML();
            //Asignación de Handlers
            View.Closed += (sender, e) => Salir();
            View.opSalir.Click += (sender, e) => Salir();
            View.opGestionClientes.Click += (sender, e) => GestionClientes();
            View.opGestionVehiculos.Click += (sender, e) => GestionVehiculos();
            View.opGestionTransportes.Click += (sender, e) => GestionTransportes();
            View.opGuardar.Click += (sender, e) => Guardar();

            GestionTransportes();
        }

        public Empresa empresa { get; set; }
        public GestorDeClientes GestorClientes { get; set; }
        public MainWindowView View { get; }

        public ClienteListarPanelCtrl getInstanceCliente()
        {
            if (CtrlpnlCliente == null)
                CtrlpnlCliente = new ClienteListarPanelCtrl(empresa); //Creamos el controlador de clientes

            CtrlpnlCliente.clienteVerPanelCtrl.View.Visible = true;
            //CtrlpnlCliente.clienteVerPanelCtrl._padre.Visible = true;

            return CtrlpnlCliente;
        }
        
        public VehiculoListarPanelCtrl getInstanceVehiculo()
        {
            if (CtrlpnlVehiculo == null)
                CtrlpnlVehiculo = new VehiculoListarPanelCtrl(empresa); //Creamos el controlador de vehiculo

            CtrlpnlVehiculo.vehiculoVerPanelCtrl.View.Visible = true;
            

            return CtrlpnlVehiculo;
        }

        public TransporteListarPanelCtrl getInstanceTransporte()
        {
            if (CtrlpnlTransporte == null)
                CtrlpnlTransporte =
                    new TransporteListarPanelCtrl(empresa, this); //Creamos el controlador de transportes
            return CtrlpnlTransporte;
        }

        public void GestionClientes()
        {
            var pnlCliente = getInstanceCliente().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlCliente); //lo asignamos al formulario principal
            pnlCliente.Visible = true;
            pnlCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
        }

        private void GestionVehiculos()
        {
            var pnlVehiculo = getInstanceVehiculo().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlVehiculo); //lo asignamos al formulario principal
            pnlVehiculo.Visible = true;
            pnlVehiculo.BringToFront(); //la traemos al frente (el de transportes queda detrás)
        }

        private void GestionTransportes()
        {
            var pnlTransporte = getInstanceTransporte().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlTransporte); //lo asignamos al formulario principal
            pnlTransporte.BringToFront();
        }

        public void VerCliente(Cliente cliente)
        {
            CtrlpnlCliente = new ClienteListarPanelCtrl(empresa); //Creamos el controlador
            var pnlCliente = CtrlpnlCliente.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlCliente); //lo asignamos al formulario principal
            pnlCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
            CtrlpnlCliente.SeleccionarCliente(cliente);
        }


        /// <summary>
        ///     Guarda los clientes en fichero
        /// </summary>
        private void Guardar()
        {
            try
            {
                empresa.GuardaXML();
                Mensaje("Guardados... ");
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