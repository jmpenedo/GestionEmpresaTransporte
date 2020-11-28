using System;
using System.Collections.Generic;

namespace GestionEmpresaTransporte.Core.Vehiculos
{
    public class Furgoneta : Vehiculo
    {
        private const string T = "Furgoneta";

        private const float Pesomax = 1.5f;

        public Furgoneta(string m, DateTime f, DateTime a, double c, string mar, string mod,  List<String> como) :base(m,f,a,c,mar,mod,como)
        {
            this.Tipo = T;
            this.Peso = Pesomax;
        }
        public override String GetTipo()
        {
            return Tipo;
        }
        
        public override string ToString()
        {
            return Tipo + base.ToString();
        }
    }
}