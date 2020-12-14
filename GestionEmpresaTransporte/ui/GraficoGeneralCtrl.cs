using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    public class GraficoGeneralCtrl
    {
        public GraficoGeneralCtrl(Empresa empresa, String tipo, int anho, String nif, String matricula, String comodidad)
        {
            View = new GraficoGeneralView();
            MiEmpresa = empresa;
            switch (tipo)
            {
                case "general":
                    generalNormal();
                    break;
                case "generalPorAnho":
                    esteAnho = anho;
                    generalPorAnho();
                    break;
                case "porCliente":
                    Nif = nif;
                    porCliente();
                    break;

                case "porClienteAnho":
                    Nif = nif;
                    esteAnho = anho;
                    porClienteAnho();
                    break;
                case "porCamion":
                    Matricula = matricula;
                    porCamion();
                    break;

                case "porCamionAnho":
                    esteAnho = anho;
                    Matricula = matricula;
                    porCamionAnho();
                    break;

                case "porComodidad":
                    Comodidad = comodidad;
                    porComodidad();
                    break;

                default:
                    break;

            }




        }

        void generalPorAnho()
        {
            int anho = esteAnho;

            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Anhos";

            var meses = new List<int>();

            int[] values = new int[12];

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                if (anho == Int16.Parse(transporte.FechaContratacion.ToString("yyyy")))
                {
                    meses.Add(Int16.Parse(transporte.FechaContratacion.ToString("MM")));
                }
            }
            meses.Sort();

            foreach (int mes in meses)
            {
                values[mes - 1]++;
            }

            View.Chart.Values = values;
            View.Chart.Draw();
        }

        void generalNormal()
        {
            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Anhos";

            var anhos = new List<int>();
            var aux = new List<int>();

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                anhos.Add(Int16.Parse(transporte.FechaContratacion.ToString("yyyy")));
            }
            //ORDENAMOS AÑOS DE MENOR A MAYOR
            anhos.Sort();
            //ELIMINO DUPLICADOS PARA SABER CUÁNTOS AÑOS HAY
            aux = anhos.Distinct().ToList();


            int[] values = new int[aux.Count];

            foreach (int anho in anhos)
            {
                for (int i = 0; i < aux.Count; i++)
                {
                    if (anho == aux[i])
                    {
                        values[i]++;
                    }
                }
            }
            View.Chart.Values = values;
            View.Chart.Draw();
        }

        void porCliente()
        {
            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Anhos";

            var anhos = new List<int>();
            var aux = new List<int>();

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                if (Nif == transporte.Cliente.Nif.ToString())
                {
                    anhos.Add(Int16.Parse(transporte.FechaContratacion.ToString("yyyy")));
                }
            }
            //Ordenamos amhos de menor a mayor

            anhos.Sort();
            //ELIMINO DUPLICADOS PARA SABER CUÁNTOS AÑOS HAY exactamente
            aux = anhos.Distinct().ToList();

            int[] values = new int[aux.Count];

            foreach (int anho in anhos)
            {
                for (int i = 0; i < aux.Count; i++)
                {
                    if (anho == aux[i])
                    {
                        values[i]++;
                    }
                }
            }

            this.View.Chart.Values = values;
            this.View.Chart.Draw();
        }

        void porClienteAnho()
        {
            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Meses";

            var meses = new List<int>();
            int[] values = new int[12];

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                if (Nif == transporte.Cliente.Nif.ToString())
                {
                    meses.Add(Int16.Parse(transporte.FechaContratacion.ToString("MM")));
                }
            }
            meses.Sort();

            foreach (int mes in meses)
            {
                values[mes - 1]++;
            }

            View.Chart.Values = values;
            View.Chart.Draw();

        }

        void porCamion()
        {
            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Anhos";

            var anhos = new List<int>();
            var aux = new List<int>();

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                if (Matricula == transporte.Camion.Matricula.ToString())
                {
                    anhos.Add(Int16.Parse(transporte.FechaContratacion.ToString("yyyy")));
                }
            }
            //Ordenamos amhos de menor a mayor

            anhos.Sort();
            //ELIMINO DUPLICADOS PARA SABER CUÁNTOS AÑOS HAY exactamente
            aux = anhos.Distinct().ToList();

            int[] values = new int[aux.Count];

            foreach (int anho in anhos)
            {
                for (int i = 0; i < aux.Count; i++)
                {
                    if (anho == aux[i])
                    {
                        values[i]++;
                    }
                }
            }

            View.Chart.Values = values;
            View.Chart.Draw();
        }

        void porCamionAnho()
        {
            View.Chart.LegendY = "Cantidad De Transportes (En unidades)";
            View.Chart.LegendX = "Meses";

            var meses = new List<int>();
            int[] values = new int[12];

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
            {
                if (Matricula == transporte.Camion.Matricula.ToString())
                {
                    meses.Add(Int16.Parse(transporte.FechaContratacion.ToString("MM")));
                }
            }
            meses.Sort();

            foreach (int mes in meses)
            {
                values[mes - 1]++;
            }

            View.Chart.Values = values;
            View.Chart.Draw();

        }

        void porComodidad()
        {
            View.Chart.LegendY = "Cumple (1) CON LA COMODIDAD, No Cumple (0) Con la comodidad";
            View.Chart.LegendX = "CAMIONES";

            int x = 0;
            Boolean hayPorLoMenosUno = false;
            int[] values = new int[MiEmpresa.ColeccionVehiculos.Count];

            foreach (var camion in MiEmpresa.ColeccionVehiculos)
            {
                if (camion.Comodidades.Contains(Comodidad))
                {
                    values[x++] = 1;
                    hayPorLoMenosUno = true;
                }
                else
                {
                    values[x++] = 0;
                }
            }



            this.View.Chart.Values = values;
            this.View.Chart.Draw();


        }

        public GraficoGeneralView View
        {
            get;
        }
        public int esteAnho { get; set; }

        public string Nif { get; set; }

        public string Matricula { get; set; }

        public string Comodidad { get; set; }

        public GraficoPanelView ViewGrafico
        {
            get;
        }
        public Empresa MiEmpresa { get; }
    }
}
