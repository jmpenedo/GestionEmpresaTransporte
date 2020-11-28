﻿using System;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class MainWindowCtrl
    {
        private WForms.Panel pnlPrincipal;

        public MainWindowCtrl()
        {
            View = new MainWindowView();
            //Creacion de todos los contenedores y recuperacion desde ficheros//
            GestorClientes = new GestorDeClientes();
            //GestorVehiculos = new GestorDeVehiculos();
            //GestorTransportes = new GestorDeTransportes();

            Cargar();

            //Asignación de Handlers
            View.Closed += (sender, e) => Salir();
            View.opSalir.Click += (sender, e) => Salir();
            View.opGestionClientes.Click += (sender, e) => GestionClientes();
            View.opGestionVehiculos.Click += (sender, e) => GestionVehiculos();
            View.opGuardar.Click += (sender, e) => Guardar();
            View.opCargar.Click += (sender, e) => Cargar();
        }

        public GestorDeClientes GestorClientes { get; set; }
        public MainWindowView View { get; }

        private void GestionClientes()
        {
            View.Controls.Remove(pnlPrincipal); //1) Siempre quitamos el principal (si es nulo no da fallo)
            var ctrlPnlSample = new ClienteListarPanelCtrl(GestorClientes); //Creamos el controlador
            pnlPrincipal = ctrlPnlSample.View; //Recuperamos el panel del controlador
            View.Controls.Add(pnlPrincipal); //lo asignamos al formulario principal
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

        /// <summary>
        ///     Permite cargar los clientes
        /// </summary>
        private void Cargar()
        {
            try
            {
                GestorClientes.CargarXML("clientes.xml");
                Mensaje("Cargados...");
            }
            catch (Exception e)
            {
                WForms.MessageBox.Show("Se ha producido un error al cargar: " + e.Message);
            }
        }

        /// <summary>
        ///     Guarda los clientes en fichero
        /// </summary>
        private void Guardar()
        {
            try
            {
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