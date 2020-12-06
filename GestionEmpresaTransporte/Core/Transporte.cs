namespace GestionEmpresaTransporte.Core
{
    using System;
    using System.Globalization;

    class Transporte
    {
        public Transporte(Vehiculo camion, Cliente cliente, string fechaContratacion, int kmRecorridos, string fechaSalida, string fechaEntrega, double importeDia, double iva, double precioLitro, double gas, double precioTotal)
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

        public string IdTransporte { get { return this.Camion.Matricula + this.FechaContratacion; } }

        public string TipoTransporte { get { return this.Camion.Tipo; } }

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

        private string NumDias
        {
            get
            {
                return (DateTime.ParseExact(this.FechaEntrega, "yyyyMMdd", CultureInfo.InvariantCulture) -
                                      DateTime.ParseExact(this.FechaSalida, "yyyyMMdd", CultureInfo.InvariantCulture)).TotalDays.ToString();
            }
        }

        
        public override String ToString()
        {
            var toret = "";
            toret += String.Format("Cliente: {0}, Precio por día: {1} euros, Número de días: {2}, Precio por Km: {3} euros, Número de Km: {4}, IVA aplicado: {5}, Precio Total: {6} euros", this.Cliente.ToString(), this.ImportePorDia, this.NumDias, 3*this.Camion.Consumo*this.PrecioLitro , this.KmRecorridos, this.IVA, this.PrecioTotal);
            return toret;
        }


    }
}
