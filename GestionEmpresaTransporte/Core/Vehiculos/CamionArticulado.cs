using System;
using System.Collections.Generic;

namespace GestionEmpresaTransporte.Core.Vehiculos
{
    public class CamionArticulado : Vehiculo
    {

        private const string T = "Camion Articulado";

            private const float Pesomax = 45f;
            
            public CamionArticulado(string m, DateTime f, DateTime a, double c, string mar, string mod, List<String> como) :base(m,f,a,c,mar,mod,como)
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