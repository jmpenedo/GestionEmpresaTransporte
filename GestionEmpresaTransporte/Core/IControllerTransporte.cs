namespace GestionEmpresaTransporte.Core
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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
