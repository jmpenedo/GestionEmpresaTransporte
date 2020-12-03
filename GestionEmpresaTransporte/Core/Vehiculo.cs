using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GestionEmpresaTransporte.Core
{
    public abstract class Vehiculo
    {
        public string Matricula { get; private set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }

        public double Consumo { get; set; }

        public DateTime FechaFabricacion { get; set; }

        public DateTime FechaAdquisicion { get; set; }

        public string Tipo { get; set; }

        public float Peso { get; set; }

        public List<string> Comodidades;

        protected Vehiculo(string m, DateTime f, DateTime a, double c, string mar, string mod, List<String> comodidades)
        {
            if (ValidarMatricula(m.ToUpper()))
            {
                this.Matricula = m.ToUpper();
                this.Consumo = c;
                this.FechaAdquisicion = a;
                this.FechaFabricacion = f;
                this.Modelo = mod;
                this.Marca = mar;
                this.Comodidades = comodidades;
            }
            else
            {
                throw new InvalidDataException("Formato de matrícula incorrecto");
            }
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
            return 
                $":\n\tMatricula: {this.Matricula}\t\tpeso máximo: {this.Peso}t" +
                $"\n\tFecha de fabricacion: {this.FechaFabricacion}\nFecha de adquisicion: {this.FechaAdquisicion}";
        }
        public static bool ValidarMatricula(string m)
        {
           
            Regex rx = new Regex(@"[0-9]{4}[A-Z]{3}");
            return rx.IsMatch(m.ToUpper()) & m.Length == 7;
        }

        public void AñadirComodidades(List<string> c)
        {
            Comodidades = c;
        }

        public abstract String GetTipo();


    }



}