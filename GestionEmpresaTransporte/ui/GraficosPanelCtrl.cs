using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;
    public class GraficosPanelCtrl

    {
        public GraficosPanelCtrl(Empresa unaEmpresa)
        {
            MiEmpresa = unaEmpresa;
            View = new GraficoPanelView();

            //Asignamos Handlers
            View.BtGraficoGeneral.Click += (sender, e) => GraficoGeneral();
            View.BtGraficoGeneralPorAnho.Click += (sender, e) => GraficoGeneralPorAnho();
            View.BtGraficoPorCliente.Click += (sender, e) => GraficoPorCliente();
            View.BtGraficoPorClientePorAnho.Click += (sender, e) => GraficoPorClientePorAnho();
            View.BtGraficoPorCamion.Click += (sender, e) => GraficoPorCamion();
            View.BtGraficoPorCamionPorAnho.Click += (sender, e) => GraficoPorCamionPorAnho();
            View.BtGraficoComodidadPorCamion.Click += (sender, e) => GraficoComidadesPorCamion();
            llenarComboBoxes();
        }
        private Empresa MiEmpresa { get; }
        public GraficoPanelView View { get; }

        void GraficoGeneral()
        {
            new GraficoGeneralCtrl(MiEmpresa, "general", 0000, "ninguno", "ninguno", "ninguno").View.Show();


        }
        void GraficoGeneralPorAnho()
        {
            new GraficoGeneralCtrl(MiEmpresa, "generalPorAnho", Int16.Parse(View.CbGraficoGPorAnho.Text), "ninguno", "ninguno", "ninguno").View.Show();

        }
        void GraficoPorCliente()
        {
            new GraficoGeneralCtrl(MiEmpresa, "porCliente", 0000, View.CbGraficoPorCliente.Text, "ninguno", "ninguno").View.Show();
        }
        void GraficoPorClientePorAnho()
        {
            new GraficoGeneralCtrl(MiEmpresa, "porClienteAnho", Int16.Parse(View.CbGraficoPorClienteAnho.Text), View.CbGraficoPorCliente2.Text, "ninguno", "ninguno").View.Show();
        }
        void GraficoPorCamion()
        {
            new GraficoGeneralCtrl(MiEmpresa, "porCamion", 0000, "ninguno", View.CbGraficoPorCamion.Text, "ninguno").View.Show();
        }

        void GraficoPorCamionPorAnho()
        {
            new GraficoGeneralCtrl(MiEmpresa, "porCamionAnho", Int16.Parse(View.CbGraficoPorCamionAnho.Text), "ninguno", View.CbGraficoPorCamion2.Text, "ninguno").View.Show();
        }

        void GraficoComidadesPorCamion()
        {
            Boolean hayPorLoMenosUnCamionConComodidad = false;
            foreach (var camion in MiEmpresa.ColeccionVehiculos)
            {
                if (camion.Comodidades.Contains(View.CbGraficoPorComodidad.Text))
                {
                    hayPorLoMenosUnCamionConComodidad = true;
                }
            }
            if (hayPorLoMenosUnCamionConComodidad)
            {
                new GraficoGeneralCtrl(MiEmpresa, "porComodidad", 0000, "ninguno", "ninguno", View.CbGraficoPorComodidad.Text).View.Show();

            }
            else
            {
                View.mostrarMensaje();
            }
        }

        void llenarComboBoxes()
        {
            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                View.CbGraficoGPorAnho.Items.Add(transporte.FechaContratacion.ToString("yyyy"));

                View.CbGraficoPorCliente.Items.Add(transporte.Cliente.Nif.ToString());

                View.CbGraficoPorClienteAnho.Items.Add(transporte.FechaContratacion.ToString("yyyy"));
                View.CbGraficoPorCliente2.Items.Add(transporte.Cliente.Nif.ToString());

                View.CbGraficoPorCamion.Items.Add(transporte.Camion.Matricula.ToString());

                View.CbGraficoPorCamion2.Items.Add(transporte.Camion.Matricula.ToString());
                View.CbGraficoPorCamionAnho.Items.Add(transporte.FechaContratacion.ToString("yyyy"));



            }
            View.CbGraficoPorComodidad.Items.Add("WIFI");
            View.CbGraficoPorComodidad.Items.Add("AC");
            View.CbGraficoPorComodidad.Items.Add("TV");
            View.CbGraficoPorComodidad.Items.Add("BLUETOOTH");
            View.CbGraficoPorComodidad.Items.Add("NEVERA");

            View.CbGraficoGPorAnho.SelectedIndex = 0;
            View.CbGraficoPorCliente.SelectedIndex = 0;
            View.CbGraficoPorClienteAnho.SelectedIndex = 0;
            View.CbGraficoPorCliente2.SelectedIndex = 0;

            View.CbGraficoPorCamion.SelectedIndex = 0;
            View.CbGraficoPorCamion2.SelectedIndex = 0;
            View.CbGraficoPorCamionAnho.SelectedIndex = 0;
            View.CbGraficoPorComodidad.SelectedIndex = 0;

        }



    }
}
