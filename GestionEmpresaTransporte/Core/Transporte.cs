﻿using System;
using System.Globalization;

namespace GestionEmpresaTransporte.Core
{
    public class Transporte
    {
        public Transporte(Vehiculo camion, Cliente cliente, DateTime fechaContratacion, int kmRecorridos,
            DateTime fechaSalida, DateTime fechaEntrega, double importeDia, double iva, double precioLitro, double gas,
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

        public string IdTransporte => Camion.Matricula + FechaContratacion.ToString("yyyyMMdd");

        public string TipoTransporte => Camion.Tipo;

        public Vehiculo Camion { get; }

        public Cliente Cliente { get; }

        public DateTime FechaContratacion { get; }

        public int KmRecorridos { get; set; }

        public DateTime FechaSalida { get; set; }

        public DateTime FechaEntrega { get; set; }

        public double ImportePorDia { get; set; }

        public double IVA { get; set; }

        public double PrecioLitro { get; set; }

        public double GasConsumido { get; set; }

        public double PrecioTotal { get; set; }

        private string NumDias => (FechaEntrega - FechaSalida).TotalDays.ToString();


        public override string ToString()
        {
            var toret = "";
            toret += string.Format(
                "Cliente: {0}{7}Precio por día: {1} euros, Número de días: {2}, Precio por Km: {3} euros, Número de Km: {4}, IVA aplicado: {5}, PRECIO TOTAL: {6} euros",
                Cliente.ImprimirDatos(), ImportePorDia, NumDias, 3 * Camion.Consumo * PrecioLitro, KmRecorridos, IVA, PrecioTotal, Environment.NewLine);
            return toret;
        }
    }
}