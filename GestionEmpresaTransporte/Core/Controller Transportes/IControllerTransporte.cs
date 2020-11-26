using Practica2_Ejercicio1.Clientes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practica2_Ejercicio1.Transporte.UI
{
    using Practica2_Ejercicio1.Flota;
    using Practica2_Ejercicio1.Clientes;

    interface IControllerTransporte
    {
        void AñadirTransporte(Dictionary<string, object> datos);

        void EditarTransporte(Dictionary<string, object> datos);

        void EliminarTransporte(string idTransporte);

        ReadOnlyCollection<Transporte> ListaTransportes();

        List<string> ObtenerClientes();

        List<string> ObtenerFlota();

        Dictionary<string, object> RecuperarTransporte(string idTransporte);

        Dictionary<string, object> RecuperarCliente(string nif);

        Dictionary<string, object> RecuperarCamion(string matricula);


    }
}
