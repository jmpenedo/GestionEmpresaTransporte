using System;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class MainWindowCtrl
    {
        private static ClienteListarPanelCtrl CtrlpnlCliente;
        private static TransporteListarPanelCtrl CtrlpnlTransporte;
        private static VehiculoListarPanelCtrl CtrlpnlVehiculo;
        private static GraficosPanelCtrl CtrlGraficos;

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
            View.opGestionGraficos.Click += (sender, e) => GestionGraficos();
            View.opGuardar.Click += (sender, e) => Guardar();

            GestionTransportes();
        }

        public Empresa empresa { get; set; }
        public GestorDeClientes GestorClientes { get; set; }
        public MainWindowView View { get; }

        public ClienteListarPanelCtrl getInstanceCliente()
        {
            if (CtrlpnlCliente == null)
                CtrlpnlCliente = new ClienteListarPanelCtrl(empresa, this); //Creamos el controlador de clientes

            CtrlpnlCliente.View.Visible = true;

            return CtrlpnlCliente;
        }

        public VehiculoListarPanelCtrl getInstanceVehiculo()
        {
            if (CtrlpnlVehiculo == null)
                CtrlpnlVehiculo = new VehiculoListarPanelCtrl(empresa, this); //Creamos el controlador de vehiculo

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
            var pnlListarCliente = getInstanceCliente().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlListarCliente); //lo asignamos al formulario principal
            pnlListarCliente.pnlCliente.ModoInicial();
            pnlListarCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
            getInstanceCliente().ActualizarPanelCliente(); //Correcion de error tras pulsar volver
        }

        public void GestionVehiculos()
        {
            var pnlVehiculo = getInstanceVehiculo().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlVehiculo); //lo asignamos al formulario principal
            pnlVehiculo.Visible = true;
            pnlVehiculo.BringToFront(); //la traemos al frente (el de transportes queda detrás)
        }

        public void GestionTransportes()
        {
            var pnlTransporte = getInstanceTransporte().View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlTransporte); //lo asignamos al formulario principal
            pnlTransporte.BringToFront();
        }
        public void GestionGraficos()
        {
           
            CtrlGraficos = new GraficosPanelCtrl(empresa);
            var pnlGrafico = CtrlGraficos.View;
            View.Controls.Add(pnlGrafico);
            pnlGrafico.BringToFront();
        }

        public void VerCliente(Cliente cliente)
        {
            CtrlpnlCliente =
                new ClienteListarPanelCtrl(empresa, this); //FIX 20201214730 pasar el controlador del main
            var pnlCliente = CtrlpnlCliente.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlCliente); //lo asignamos al formulario principal
            pnlCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
            CtrlpnlCliente.SeleccionarCliente(cliente);
        }

        public void VerVehiculo(Vehiculo vehiculo)
        {
            CtrlpnlVehiculo = new VehiculoListarPanelCtrl(empresa); //Creamos el controlador
            var pnlVehiculo = CtrlpnlVehiculo.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlVehiculo); //lo asignamos al formulario principal
            pnlVehiculo.BringToFront(); //la traemos al frente (el de transportes queda detrás)
            CtrlpnlVehiculo.SeleccionarVehiculo(vehiculo);
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