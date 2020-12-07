using System;
using System.Globalization;

namespace GestionEmpresaTransporte.Core
{
    internal class Transporte
    {
        public Transporte(Vehiculo camion, Cliente cliente, string fechaContratacion, int kmRecorridos,
            string fechaSalida, string fechaEntrega, double importeDia, double iva, double precioLitro, double gas,
            double precioTotal)
        {
            Camion = camion;
            Cliente = cliente;
            FechaContratacion = fechaContratacion;
            KmRecorridos = kmRecorridos;
            FechaSalida = fechaSalida;
            FechaEntrega = fechaEntrega;
            ImportePorDia = importeDia;
            IVA = iva;
            PrecioLitro = precioLitro;
            GasConsumido = gas;
            PrecioTotal = precioTotal;
        }

        public string IdTransporte => Camion.Matricula + FechaContratacion;

        public string TipoTransporte => Camion.Tipo;

        public Vehiculo Camion { get; }

        public Cliente Cliente { get; }

        public string FechaContratacion { get; }

        public int KmRecorridos { get; set; }

        public string FechaSalida { get; set; }

        public string FechaEntrega { get; set; }

        public double ImportePorDia { get; set; }

        public double IVA { get; set; }

        public double PrecioLitro { get; set; }

        public double GasConsumido { get; set; }

        public double PrecioTotal { get; set; }

        private string NumDias =>
            (DateTime.ParseExact(FechaEntrega, "yyyyMMdd", CultureInfo.InvariantCulture) -
             DateTime.ParseExact(FechaSalida, "yyyyMMdd", CultureInfo.InvariantCulture)).TotalDays.ToString();


        public override string ToString()
        {
            var toret = "";
            toret += string.Format(
                "Cliente: {0}, Precio por día: {1} euros, Número de días: {2}, Precio por Km: {3} euros, Número de Km: {4}, IVA aplicado: {5}, Precio Total: {6} euros",
                Cliente, ImportePorDia, NumDias, 3 * Camion.Consumo * PrecioLitro, KmRecorridos, IVA, PrecioTotal);
            return toret;
        }
    }
}