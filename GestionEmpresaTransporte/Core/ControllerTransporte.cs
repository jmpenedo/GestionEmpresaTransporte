﻿namespace GestionEmpresaTransporte.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Collections.ObjectModel;

    class ControllerTransporte : IControllerTransporte
    {
        
        public ControllerTransporte(Empresa empresa, ControllerEmpresa controladorEmpresa, IVistaTransportes vistaTransportes)
        {
            Vista = vistaTransportes;
            Empresa = empresa;
            ControladorEmpresa = controladorEmpresa;
            Vista.referenciaController(this);
            Application.Run((Form)Vista);
        }

        private IVistaTransportes Vista { get; }

        private ControllerEmpresa ControladorEmpresa { get; }

        
        private Empresa Empresa { get; }


        public void AñadirTransporte(Dictionary<string, object> datos)
        {
            if (this.Empresa.ColeccionTransportes.ExisteTransporte(datos["MATRICULA"].ToString() + datos["FECHACONTRATACION"].ToString()))
            {
                this.EditarTransporte(datos);
            }
            else
            {
                var flota = this.Empresa.ColeccionVehiculos.RecuperarVehiculo(datos["MATRICULA"].ToString());
                var cliente = this.Empresa.ColeccionClientes.getClientebyNif(datos["CLIENTE"].ToString());

                this.Empresa.ColeccionTransportes.Add(new Transporte(flota, cliente, datos["FECHACONTRATACION"].ToString(),
                    Convert.ToInt32(datos["KMRECORRIDOS"]), datos["FECHASALIDA"].ToString(), datos["FECHAENTREGA"].ToString(),
                    Convert.ToDouble(datos["IMPORTEDIA"]), Convert.ToDouble(datos["IVA"]), Convert.ToDouble(datos["PRECIOLITRO"]),
                    Convert.ToDouble(datos["GAS"]), Convert.ToDouble(datos["PRECIOTOTAL"])));

                Console.WriteLine("Transporte añadido correctamente");
            }
        }



        public void EliminarTransporte(String idTransporte)
        {

            var transporte = this.Empresa.ColeccionTransportes.RecuperarTransporte(idTransporte);
            this.Empresa.ColeccionTransportes.Remove(transporte);

        }

        public void EditarTransporte(Dictionary<string, object> datos)
        {
            var transporte = this.Empresa.ColeccionTransportes.RecuperarTransporte(datos["MATRICULA"].ToString() + datos["FECHACONTRATACION"].ToString());
            transporte.KmRecorridos = Convert.ToInt32(datos["KMRECORRIDOS"]);
            transporte.FechaSalida = datos["FECHASALIDA"].ToString();
            transporte.FechaEntrega = datos["FECHAENTREGA"].ToString();
            transporte.ImportePorDia = Convert.ToDouble(datos["IMPORTEDIA"]);
            transporte.IVA = Convert.ToDouble(datos["IVA"]);
            transporte.PrecioLitro = Convert.ToDouble(datos["PRECIOLITRO"]);
            transporte.GasConsumido = Convert.ToDouble(datos["GAS"]);
            transporte.PrecioTotal = Convert.ToDouble(datos["PRECIOTOTAL"]);
        }

        public ReadOnlyCollection<Transporte> ListaTransportes()
        {
            return this.Empresa.ColeccionTransportes.AsReadOnly();
        }

        public List<string> ObtenerClientes()
        {
            var toret = new List<string>();
            foreach (Cliente Cliente in this.Empresa.ColeccionClientes.Clientes)
            {
                toret.Add(Cliente.Nif);
            }
            return toret;
        }

        public List<string> ObtenerFlota()
        {
            var toret = new List<string>();
            foreach (Vehiculo Vehiculo in this.Empresa.ColeccionVehiculos.Flota)
            {
                toret.Add(Vehiculo.Matricula);
            }
            return toret;
        }

        public Dictionary<string, object> RecuperarTransporte(string idTransporte)
        {
            var toret = new Dictionary<string, object>();
            Transporte transporte = this.Empresa.ColeccionTransportes.RecuperarTransporte(idTransporte);

            toret.Add("CLIENTE", transporte.Cliente.Nif);
            toret.Add("MATRICULA", transporte.Camion.Matricula);
            toret.Add("FECHACONTRATACION", transporte.FechaContratacion);
            toret.Add("KMRECORRIDOS", transporte.KmRecorridos);
            toret.Add("FECHASALIDA", transporte.FechaSalida);
            toret.Add("FECHAENTREGA", transporte.FechaEntrega);
            toret.Add("IMPORTEDIA", transporte.ImportePorDia);
            toret.Add("IVA", transporte.IVA);
            toret.Add("PRECIOLITRO", transporte.PrecioLitro);
            toret.Add("GAS", transporte.GasConsumido);
            toret.Add("PRECIOTOTAL", transporte.PrecioTotal);

            return toret;
        }

        public Dictionary<string, object> RecuperarCliente(string nif)
        {
            var toret = new Dictionary<string, object>();
            Cliente cliente = this.Empresa.ColeccionClientes.getClientebyNif(nif);

            toret.Add("NIF", cliente.Nif);

            return toret;
        }

        public Dictionary<string, object> RecuperarCamion(string matricula)
        {
            var toret = new Dictionary<string, object>();
            Vehiculo camion = this.Empresa.ColeccionVehiculos.RecuperarVehiculo(matricula);

            toret.Add("MATRICULA", camion.Matricula);
            toret.Add("TIPO", camion.Tipo);
            toret.Add("CONSUMO", camion.Consumo);

            return toret;
        }


    }
}
