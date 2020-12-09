using System;
using System.Collections.Generic;

namespace GestionEmpresaTransporte.Core.Vehiculos
{
    
    public class Camion : Vehiculo
    {
        
        private const string T = "Camion";

        private const float Pesomax = 25f;

        public Camion(string m, DateTime f, DateTime a, double c, string mar, string mod, List<String> como) :base(m,f,a,c,mar,mod,como)
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
            return base.ToString();
        }
    }
}