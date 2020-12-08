using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GestionEmpresaTransporte.Core
{
    public abstract class Vehiculo
    {
        public string Matricula { get; }

        public string Modelo { get;

        public string Marca { get;  }

        public double Consumo { get; set;}

        public DateTime FechaFabricacion { get; }

        public DateTime FechaAdquisicion { get; set; }

        public string Tipo { get; protected set; }

        public float Peso { get; protected set; }

        public List<string> Comodidades;

        /// <summary>
        ///     Crea un objeto de tipo vehiculo <see cref="Vehiculo" />
        /// </summary>
        /// <param name="matricula">El identificador del vehiculo, formato europero 0000XXX</param>
        /// <param name="fechaFabricacion">Fecha en la cual el vehiculo ha salido de la fábrica</param>
        /// <param name="fechaAdquisicion">Fecha en la cual el vehiculo ha sido adquirido por la empresa</param>
        /// <param name="consumo">Consumo de combustible en L/100Km del vehiculo</param>
        /// <param name="marca">Fabricante del vehiculo</param>
        /// <param name="modelo">Modelo del vehiculo</param>
        /// <param name="comodidades">Comodidades de las que dispone el vehiculo</param>
        protected Vehiculo(string matricula, DateTime fechaFabricacion, DateTime fechaAdquisicion, double consumo, 
            string marca, string modelo, List<String> comodidades)
        {
            if (utilidades.ValidarMatricula(matricula.ToUpper()))
            {
                this.Matricula = matricula.ToUpper();
            }
            else
            {
                throw new InvalidDataException("Formato de matrícula incorrecto");
            }

            if (Consumo > 0)
            {
                this.Consumo = consumo;  
            }
            else
            {
                throw new InvalidDataException("El consumo debe se mayor que 0");
            }

            if (DateTime.Compare(fechaFabricacion, fechaAdquisicion) <= 0)
            {
                this.FechaAdquisicion = fechaAdquisicion;
                this.FechaFabricacion = fechaFabricacion;
            }
            else
            {
                throw new InvalidDataException("La fecha de fabricacion no puede ser anterior a la de adquisicion");
            }
            
            this.Modelo = modelo;
            this.Marca = marca;
            this.Comodidades = comodidades;
            
            
        }

        public String ComodidadesToString()
        {
            String toret = "Comodidades: "; 
            foreach (var comodidad in this.Comodidades)
            {
                toret += comodidad + " ";
            }

            return toret;

        }


        public override string ToString()
        {
            return this.Matricula;
        }

        public string ImprimirDatos()
        {
            return
                $":\n\tMatricula: {this.Matricula}\t\tpeso máximo: {this.Peso}t" +
                $"\n\tFecha de fabricacion: {this.FechaFabricacion}\nFecha de adquisicion: {this.FechaAdquisicion}";
        }
        
        

        public abstract String GetTipo();


    }



}