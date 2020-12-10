using System;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    internal class MainWindowCtrl
    {
        private ClienteListarPanelCtrl CtrlpnlCliente;
        private WForms.Panel pnlPrincipal;
        private WForms.Panel pnlTransporte;

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
        }

        public Empresa empresa { get; set; }
        public GestorDeClientes GestorClientes { get; set; }
        public MainWindowView View { get; }

        private void GestionClientes()
        {
            CtrlpnlCliente = new ClienteListarPanelCtrl(empresa); //Creamos el controlador
            var pnlCliente = CtrlpnlCliente.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlCliente); //lo asignamos al formulario principal
            pnlCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
        }

        /// <summary>
        ///     REpetimos lo anterior para otra parte...
        /// </summary>
        private void GestionVehiculos()
        {
            View.Controls.Remove(pnlPrincipal); //1) Siempre quitamos el principal (si es nulo no da fallo)
            var ctrlPnlSample = new SamplePanelCtrl(); //Creamos el controlador
            pnlPrincipal = ctrlPnlSample.View; //Recuperamos el panle del controlador
            View.Controls.Add(pnlPrincipal); //lo asignamos al formulario principal
        }

        private void GestionTransportes()
        {
            View.Controls.Remove(pnlTransporte); //1) Siempre quitamos el principal (si es nulo no da fallo)
            var CtlTransporte = new TransporteListarPanelCtrl(empresa, this); //Creamos el controlador
            pnlTransporte = CtlTransporte.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlTransporte); //lo asignamos al formulario principal
            pnlTransporte.BringToFront();
        }

        public void VerCliente(Cliente cliente)
        {
            CtrlpnlCliente = new ClienteListarPanelCtrl(empresa); //Creamos el controlador
            var pnlCliente = CtrlpnlCliente.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlCliente); //lo asignamos al formulario principal
            pnlCliente.BringToFront(); //la traemos al frente (el de transportes queda detrás)
            var pos = CtrlpnlCliente.GestorClientes.PosCliente(cliente);
            CtrlpnlCliente.View.grdLista.Rows[pos].Selected = true;
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