namespace GestionEmpresaTransporte.Core
{
    class Empresa
    {
        public Empresa()
        {
            ColeccionClientes = new GestorDeClientes();
            ColeccionVehiculos = new ColeccionVehiculos();
            ColeccionTransportes = new ColeccionTransportes();
        }

        public GestorDeClientes ColeccionClientes { get; }
        public ColeccionVehiculos ColeccionVehiculos { get; }
        public ColeccionTransportes ColeccionTransportes { get; }

    }
}
