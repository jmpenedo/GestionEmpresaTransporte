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
    public class GraficoPanelView : WForms.Panel
    {
        public GraficoGeneralView pnlGrafico;
        public GraficoPanelView()
        {
            Build();
        }


        private void Build()
        {
            var pnlGrafico = new WForms.TableLayoutPanel();

            pnlGrafico.SuspendLayout();
            pnlGrafico.Dock = WForms.DockStyle.Fill;

            this.BtGraficoGeneral = new WForms.Button
            {
                Text = "Grafico General",
                Dock = WForms.DockStyle.Top
            };

            this.BtGraficoGeneralPorAnho = new WForms.Button
            {
                Text = "Grafico General Por Anho Con el Anho que escojas abajo",
                Dock = WForms.DockStyle.Top
            };

            this.CbGraficoGPorAnho = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };



            this.BtGraficoPorCliente = new WForms.Button
            {
                Text = "Gráfico Por Cliente",
                Dock = WForms.DockStyle.Top
            };

            this.CbGraficoPorCliente = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };

            this.BtGraficoPorClientePorAnho = new WForms.Button
            {
                Text = "Gráfico Por Cliente Por Anho escogidos en las dos siguientes comboBox",
                Dock = WForms.DockStyle.Top
            };

            this.CbGraficoPorCliente2 = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };

            this.CbGraficoPorClienteAnho = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };

            this.BtGraficoPorCamion = new WForms.Button
            {
                Text = "Gráfico Por Camión Escogido Debajo",
                Dock = WForms.DockStyle.Top
            };
            this.CbGraficoPorCamion = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };

            this.BtGraficoPorCamionPorAnho = new WForms.Button
            {
                Text = "Gráfico Por Camión y  Por Anho Escogidos en los dos siguientes comboboxes",
                Dock = WForms.DockStyle.Top
            };

            this.CbGraficoPorCamion2 = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };

            this.CbGraficoPorCamionAnho = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };


            this.BtGraficoComodidadPorCamion = new WForms.Button
            {
                Text = "Gráfico Comodidad Por Camión",
                Dock = WForms.DockStyle.Top
            };
            this.CbGraficoPorComodidad = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Fill,
                DropDownStyle = WForms.ComboBoxStyle.DropDownList
            };



            pnlGrafico.Controls.Add(BtGraficoGeneral);

            pnlGrafico.Controls.Add(BtGraficoGeneralPorAnho);
            pnlGrafico.Controls.Add(CbGraficoGPorAnho);

            pnlGrafico.Controls.Add(BtGraficoPorCliente);
            pnlGrafico.Controls.Add(CbGraficoPorCliente);

            pnlGrafico.Controls.Add(BtGraficoPorClientePorAnho);
            pnlGrafico.Controls.Add(CbGraficoPorCliente2);
            pnlGrafico.Controls.Add(CbGraficoPorClienteAnho);



            pnlGrafico.Controls.Add(BtGraficoPorCamion);
            pnlGrafico.Controls.Add(CbGraficoPorCamion);


            pnlGrafico.Controls.Add(BtGraficoPorCamionPorAnho);
            pnlGrafico.Controls.Add(CbGraficoPorCamion2);
            pnlGrafico.Controls.Add(CbGraficoPorCamionAnho);


            pnlGrafico.Controls.Add(BtGraficoComodidadPorCamion);
            pnlGrafico.Controls.Add(CbGraficoPorComodidad);




            pnlGrafico.ResumeLayout(false);
            Controls.Add(pnlGrafico);
            MinimumSize = new Draw.Size(780, 600);
            MaximumSize = MinimumSize;

        }
        public WForms.Button BtGraficoGeneral { get; private set; }

        public WForms.Button BtGraficoGeneralPorAnho
        {
            get; set;
        }

        //public WForms.MessageBox mensaje
        //{
        //    get; set;
        //}
        public void mostrarMensaje()
        {
            WForms.MessageBox.Show("No Hay Camiones Con Esa Comodidad", "Error Camiones Sin Comodidad", WForms.MessageBoxButtons.OK, WForms.MessageBoxIcon.Error);
        }

        public WForms.Button BtGraficoPorCliente
        {
            get; set;
        }
        public WForms.Button BtGraficoPorClientePorAnho
        {
            get; set;
        }

        public WForms.Button BtGraficoPorCamion
        {
            get; set;
        }

        public WForms.Button BtGraficoPorCamionPorAnho
        {
            get; set;
        }

        public WForms.Button BtGraficoComodidadPorCamion
        {
            get; private set;
        }



        public WForms.ComboBox CbGraficoGPorAnho
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorCliente
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorClienteAnho
        {
            get; set;
        }
        public WForms.ComboBox CbGraficoPorCliente2
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorCamion
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorCamionAnho
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorCamion2
        {
            get; set;
        }

        public WForms.ComboBox CbGraficoPorComodidad
        {
            get; set;
        }




        //private new WForms.Control BuildGraficoGeneralPorAnho()
        //{
        //    pnlGrafico = new GraficoGeneralPorAnho
        //}

        //private new WForms.C
    }
}

