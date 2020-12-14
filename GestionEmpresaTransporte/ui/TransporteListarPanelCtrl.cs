using System;
using System.ComponentModel;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;

    public class TransporteListarPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar,
            Volver
        }

        private BindingList<Transporte> _bindingList;
        private Transporte miTransporte;

        public TransporteListarPanelCtrl(Empresa empresa)
        {
            MiEmpresa = empresa;
            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            View = new TransporteListarPanelView();
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            //Asignamos Handlers
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.grdLista.CellDoubleClick += (sender, args) => CeldaSeleccionada();
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelTransporte();
            View.pnlTransporte.BtSelecCliente.Click += (sender, e) => SeleccionarCliente();
            View.pnlTransporte.BtSelecVehiculo.Click += (sender, e) => SeleccionarVehiculo();
            View.pnlTransporte.BtPendiente.Click += (sender, e) => ListarPendiente();
            View.pnlTransporte.BtVolver.Click += (sender, e) => Volver();
            View.pnlTransporte.BtFiltroFecha.Click += (sender, e) => ListarFecha();
            View.pnlTransporte.BtFiltroYear.Click += (sender, e) => ListarYear();
            View.pnlTransporte.BtInsertar.Click += (sender, e) => InsertarTransporte();
            View.pnlTransporte.BtAceptar.Click += (sender, e) => Aceptar();
            View.pnlTransporte.BtCancelar.Click += (sender, e) => Cancelar();
            View.pnlTransporte.BtBorrar.Click += (sender, e) => BorrarTransporte();
            View.pnlTransporte.BtModificar.Click += (sender, e) => ModoModificar();
            View.pnlTransporte.ModoConsulta();
            EstadoPnlTransporte = Estados.Consultar;
        }

        public TransporteListarPanelCtrl(Empresa empresa, MainWindowCtrl controlPrincipal) : this(empresa)
        {
            MainWindowControl = controlPrincipal;
        }

        public Estados EstadoPnlTransporte { get; set; }
        public TransporteListarPanelView View { get; }

        public Empresa MiEmpresa { get; set; }
        public MainWindowCtrl MainWindowControl { get; set; }

        public Transporte ElTransporte
        {
            get => miTransporte;
            set
            {
                miTransporte = value;
                ActualizaTextTransporte();
            }
        }

        private void ActualizaTextTransporte()
        {
            if (ElTransporte != null)
            {
                View.pnlTransporte.EdCliente.Text = ElTransporte.Cliente.Nif;
                View.pnlTransporte.EdFlota.Text = ElTransporte.Camion.Matricula;
                View.pnlTransporte.EdFechaContratacion.Value = ElTransporte.FechaContratacion;
                View.pnlTransporte.EdKmsRecorridos.Text = Convert.ToString(ElTransporte.KmRecorridos);
                View.pnlTransporte.EdFechaSalida.Value = ElTransporte.FechaSalida;
                View.pnlTransporte.EdFechaEntrega.Value = ElTransporte.FechaEntrega;
                View.pnlTransporte.EdImporteDia.Text = Convert.ToString(ElTransporte.ImportePorDia);
                View.pnlTransporte.EdIVA.Text = Convert.ToString(ElTransporte.IVA);
                View.pnlTransporte.EdPrecioLitro.Text = Convert.ToString(ElTransporte.PrecioLitro);
                View.pnlTransporte.EdGas.Text = Convert.ToString(ElTransporte.GasConsumido);
                View.pnlTransporte.EdFactura.Text = ElTransporte.ToString();
            }
            else
            {
                View.pnlTransporte.EdCliente.Text = "";
                View.pnlTransporte.EdFlota.Text = "";
                View.pnlTransporte.EdFechaContratacion.Text = "";
                View.pnlTransporte.EdKmsRecorridos.Text = "";
                View.pnlTransporte.EdFechaSalida.Text = "";
                View.pnlTransporte.EdFechaEntrega.Text = "";
                View.pnlTransporte.EdImporteDia.Text = "";
                View.pnlTransporte.EdIVA.Text = "";
                View.pnlTransporte.EdPrecioLitro.Text = "";
                View.pnlTransporte.EdGas.Text = "";
                View.pnlTransporte.EdFactura.Text = "";
            }
        }

        private void Aceptar()
        {
            switch (EstadoPnlTransporte)
            {
                case Estados.Insertar:
                    AñadirTransporte();
                    return;
                case Estados.Borrar:
                    BorrarTransporte();
                    return;
                case Estados.Modificar:
                    ModificarTransporte();
                    return;
                case Estados.Consultar:
                    //ConsultarTransporte();
                    return;
                case Estados.Volver:
                    return;
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.pnlTransporte.ModoConsulta();
            ActualizaTextTransporte();
        }

        private void ModoModificar()
        {
            EstadoPnlTransporte = Estados.Modificar;
            View.pnlTransporte.ModoModificar();
        }


        private void BorrarTransporte()
        {
            if (ElTransporte != null)
            {
                if (MiEmpresa.ColeccionTransportes.ExisteTransporte(ElTransporte.IdTransporte))
                {
                    /*MessageBOX de confirmacion*/
                    var message = string.Format("¿Estás seguro de borrar el transporte con identificador: {0}?", ElTransporte.IdTransporte);
                    var caption = "Borrar Transporte";
                    var buttons = WForms.MessageBoxButtons.YesNo;
                    WForms.DialogResult result;
                    // Displays the MessageBox.
                    result = WForms.MessageBox.Show(message, caption, buttons);
                    if (result == WForms.DialogResult.Yes) _bindingList.Remove(ElTransporte);

                    if (_bindingList.Count == 0)
                    {
                        ElTransporte = null;
                    }
                    
                    View.Actualizar();
                    ActualizaTextTransporte();
                    View.pnlTransporte.ModoConsulta();

                }
            }
        }

        private void InsertarTransporte()
        {
            View.pnlTransporte.ModoInsercion();
            View.pnlTransporte.EdFactura.Text = "";
            EstadoPnlTransporte = Estados.Insertar;
        }


        private void ModificarTransporte()
        {
            //Si las validaciones son correctas se actulizan los datos del cliente
            if (ComprobarDatos())
            {
                var kmRecorridos = Convert.ToInt32(View.pnlTransporte.EdKmsRecorridos.Value);
                var fechaSalida = View.pnlTransporte.EdFechaSalida.Value;
                var fechaEntrega = View.pnlTransporte.EdFechaEntrega.Value;
                var importeDia = Convert.ToDouble(View.pnlTransporte.EdImporteDia.Value);
                var iva = Convert.ToDouble(View.pnlTransporte.EdIVA.Value);
                var precioLitro = Convert.ToDouble(View.pnlTransporte.EdPrecioLitro.Value);
                var gas = Convert.ToDouble(View.pnlTransporte.EdGas.Value);

                var vehiculo = this.MiEmpresa.ColeccionVehiculos.RecuperarVehiculo(View.pnlTransporte.EdFlota.Text);
                var precioTotal = CalcularPrecioTotal(vehiculo, kmRecorridos, fechaSalida, fechaEntrega, importeDia, iva, precioLitro, gas);

                ElTransporte.KmRecorridos = Convert.ToInt32(View.pnlTransporte.EdKmsRecorridos.Value);
                ElTransporte.FechaSalida = View.pnlTransporte.EdFechaSalida.Value;
                ElTransporte.FechaEntrega = View.pnlTransporte.EdFechaEntrega.Value;
                ElTransporte.ImportePorDia = Convert.ToDouble(View.pnlTransporte.EdImporteDia.Value);
                ElTransporte.IVA = Convert.ToDouble(View.pnlTransporte.EdIVA.Value);
                ElTransporte.PrecioLitro = Convert.ToDouble(View.pnlTransporte.EdPrecioLitro.Value);
                ElTransporte.GasConsumido = Convert.ToDouble(View.pnlTransporte.EdGas.Value);
                ElTransporte.PrecioTotal = precioTotal;
                View.Actualizar();
                ActualizaTextTransporte();
                View.pnlTransporte.ModoConsulta();
                
            }
        }

        /// <summary>
        ///     Para ejecutar al terminar de rellenar  el formulario y
        ///     pulsar aceptar. Crea el nuevo cliente
        /// </summary>
        private void AñadirTransporte()
        {
            if (ComprobarDatos())
            {
                var matricula = View.pnlTransporte.EdFlota.Text;
                var fecha = View.pnlTransporte.EdFechaContratacion.Value.ToString("yyyyMMdd");
                if (MiEmpresa.ColeccionTransportes.ExisteTransporte(matricula + fecha))
                {
                    WForms.MessageBox.Show("El transporte que desea agragar ya existe. El id de los transporte (matrícula + fecha de contratación) debe ser único");
                }
                else
                {
                    var nifCliente = View.pnlTransporte.EdCliente.Text;
                    var fechaContratacion = View.pnlTransporte.EdFechaContratacion.Value;
                    var kmRecorridos = Convert.ToInt32(View.pnlTransporte.EdKmsRecorridos.Value);
                    var fechaSalida = View.pnlTransporte.EdFechaSalida.Value;
                    var fechaEntrega = View.pnlTransporte.EdFechaEntrega.Value;
                    var importeDia = Convert.ToDouble(View.pnlTransporte.EdImporteDia.Value);
                    var iva = Convert.ToDouble(View.pnlTransporte.EdIVA.Value);
                    var precioLitro = Convert.ToDouble(View.pnlTransporte.EdPrecioLitro.Value);
                    var gas = Convert.ToDouble(View.pnlTransporte.EdGas.Value);

                    var vehiculo = this.MiEmpresa.ColeccionVehiculos.RecuperarVehiculo(matricula);
                    var cliente = this.MiEmpresa.ColeccionClientes.getClientebyNif(nifCliente);
                    var precioTotal = CalcularPrecioTotal(vehiculo, kmRecorridos, fechaSalida, fechaEntrega, importeDia, iva, precioLitro, gas);

                    var nuevoTransporte = new Transporte(vehiculo, cliente, fechaContratacion, kmRecorridos,
                                                         fechaSalida, fechaEntrega, importeDia, iva, precioLitro,
                                                         gas, precioTotal);
                    _bindingList.Add(nuevoTransporte);
                    View.Actualizar();
                    ActualizaTextTransporte();
                    View.pnlTransporte.ModoConsulta();

                }

            }
        }

        private Boolean ComprobarDatos()
        {
            var valido = true;
            var nifCliente = View.pnlTransporte.EdCliente.Text;
            var matricula = View.pnlTransporte.EdFlota.Text;
            var fechaContratacion = View.pnlTransporte.EdFechaContratacion.Value;
            var kmRecorridos = Convert.ToInt32(View.pnlTransporte.EdKmsRecorridos.Value);
            var fechaSalida = View.pnlTransporte.EdFechaSalida.Value;
            var fechaEntrega = View.pnlTransporte.EdFechaEntrega.Value;
            var importeDia = Convert.ToDouble(View.pnlTransporte.EdImporteDia.Value);
            var iva = Convert.ToDouble(View.pnlTransporte.EdIVA.Value);
            var precioLitro = Convert.ToDouble(View.pnlTransporte.EdPrecioLitro.Value);
            var gas = Convert.ToDouble(View.pnlTransporte.EdGas.Value);

            if (string.IsNullOrWhiteSpace(nifCliente))
            {
                WForms.MessageBox.Show("Debe seleccionar un cliente");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(matricula))
            {
                WForms.MessageBox.Show("Debe seleccionar una matrícula");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(fechaContratacion.ToString("yyyyMMdd")) || !utilidades.IsValidFechaContratacion(fechaContratacion, fechaSalida, fechaEntrega))
            {
                WForms.MessageBox.Show("La fecha de contratación debe ser igual o anterior a las fechas de entrega y salida");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(kmRecorridos)) || kmRecorridos <= 0.0)
            {
                WForms.MessageBox.Show("Los km Recorridos deben ser mayor que 0");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(fechaSalida.ToString("yyyyMMdd")) || !utilidades.IsValidFechaSalida(fechaContratacion, fechaSalida))
            {
                WForms.MessageBox.Show("La fecha de salida debe ser posterior a la de contratacion");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(fechaEntrega.ToString("yyyyMMdd")) || !utilidades.IsValidFechaEntrega(fechaSalida, fechaEntrega))
            {
                WForms.MessageBox.Show("La fecha de entrega debe ser posterior a la de salida");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(importeDia)) || importeDia <= 0.0)
            {
                WForms.MessageBox.Show("El Importe por Dia debe ser mayor que 0");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(iva)) || iva <= 0.0 || iva >= 1.0)
            {
                WForms.MessageBox.Show("El IVA aplicado debe ser mayor que 0 y menor que 1");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(precioLitro)) || precioLitro <= 0.0)
            {
                WForms.MessageBox.Show("El Precio del Litro debe ser mayor que 0");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(gas)) || gas <= 0.0)
            {
                WForms.MessageBox.Show("El Gas Consumido debe ser mayor que 0");
                valido = false;
            }

            return valido;
        }

        private double CalcularPrecioTotal(Vehiculo camion, int kmRecorridos, DateTime fechaSalida, DateTime fechaEntrega, double importeDia, double iva, double precioLitro, double gas)
        {


            double numd = (fechaEntrega - fechaSalida).TotalDays;
            var suplencia = CalcularSuplencia(numd);
            double ppkm = 3 * Convert.ToDouble(camion.Consumo) * precioLitro;
            var precioParcial = (numd * importeDia * suplencia) + (Convert.ToDouble(kmRecorridos) * ppkm) + gas;

            return Math.Round(precioParcial + precioParcial * iva, 2);
        }

        private int CalcularSuplencia(double numd)
        {
            var toret = 1;

            if (numd > 1.0)
            {
                toret = 2;
            }

            return toret;
        }

        public DateTime ObtenerFecha()
        {
            var fechaSeleccionada = View.pnlTransporte.EdFechaFiltro.Value;

            if (string.IsNullOrWhiteSpace(fechaSeleccionada.ToString("yyyyMMdd")))
            {
                WForms.MessageBox.Show("La fecha para filtrar es inválida, se muestran resultados para hoy");
                fechaSeleccionada = DateTime.Now;
            }

            return fechaSeleccionada;
        }


        public int ObtenerYear()
        {
            var yearSeleccionado = Convert.ToInt32(View.pnlTransporte.EdYearFiltro.Value);

            if (string.IsNullOrWhiteSpace(Convert.ToString(yearSeleccionado))
                || yearSeleccionado < 2000
                || yearSeleccionado > 2030
                )
            {
                WForms.MessageBox.Show("El año para filtrar debe estar en el 2000 y el 2030");
                yearSeleccionado = 2020;
            }


            return yearSeleccionado;
        }

        public void ActualizarPanelTransporte()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                ElTransporte = TransporteSeleccionado;

                foreach (WForms.DataGridViewCell cell in row.Cells)
                    if (cell.ColumnIndex == 3)
                        cell.ToolTipText = "Doble click para más información del cliente";
                    else if (cell.ColumnIndex == 2) cell.ToolTipText = "Doble click para más información del vehículo";
            }

            View.AjustarColGrid();
        }

        private void CeldaSeleccionada()
        {
            var columna = Math.Max(0, View.grdLista.CurrentCell.ColumnIndex);
            if (columna == 3)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                ElTransporte = TransporteSeleccionado;
                var Cliente = TransporteSeleccionado.Cliente;
                MainWindowControl.VerCliente(Cliente);
            }
            else if (columna == 2)
            {
                var idTransporte = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var TransporteSeleccionado = _bindingList.FirstOrDefault(item => item.IdTransporte == idTransporte);
                ElTransporte = TransporteSeleccionado;
                var Vehiculo = TransporteSeleccionado.Camion;
                MainWindowControl.VerVehiculo(Vehiculo);
            }
        }

        private void SeleccionarCliente()
        {
            var instanciaClientes = MainWindowControl.getInstanceCliente();

            instanciaClientes.View.pnlCliente.BtSeleccionar.Click +=
                (sender, e) => CambiarCliente(instanciaClientes);
            instanciaClientes.View.pnlCliente.BtVolver.Click +=
                (sender, e) => CambiarCliente(instanciaClientes);
            instanciaClientes.View.grdLista.CellDoubleClick +=
                (sender, e) => CambiarCliente(instanciaClientes);
            instanciaClientes.View.pnlCliente.ModoSeleccion(true);
            MainWindowControl.GestionClientes();
        }

        private void CambiarCliente(ClienteListarPanelCtrl instanciaClientes)
        {
            if (instanciaClientes.ElCliente != null) //Caso seleccionar
                View.pnlTransporte.EdCliente.Text = instanciaClientes.ElCliente.Nif;
            else //caso volver
                View.pnlTransporte.EdCliente.Text = "";
        }

        private void SeleccionarVehiculo()
        {
            var instanciaVehiculos = MainWindowControl.getInstanceVehiculo();
            instanciaVehiculos.seleccion = true;
            instanciaVehiculos.View.pnlVehiculo.BtSeleccionar.Click +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.pnlVehiculo.BtVolver.Click +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.grdLista.CellDoubleClick +=
                (sender, e) => CambiarVehiculo(instanciaVehiculos);
            instanciaVehiculos.View.pnlVehiculo.ModoSeleccion(true);
            MainWindowControl.GestionVehiculos();
        }

        private void CambiarVehiculo(VehiculoListarPanelCtrl instanciaVehiculos)
        {
            if (instanciaVehiculos.ElVehiculo != null) //Caso seleccionar
                View.pnlTransporte.EdFlota.Text = instanciaVehiculos.ElVehiculo.Matricula;
            else //caso volver
                View.pnlTransporte.EdFlota.Text = "";
            instanciaVehiculos.seleccion = false;
        }

        private void ListarPendiente()
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.Pendiente().ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }

        private void ListarYear()
        {
            var filtro = ObtenerYear();
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCamion(filtro).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }

        private void ListarFecha()
        {
            var filtro = ObtenerFecha();
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasDia(filtro).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }

        private void Volver()
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ColeccionTransportes.ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoConsulta();
        }

        public void ListarReservasCliente(string nif)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCliente(nif).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }

        public void ListarReservasCliente(string nif, int year)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCliente(nif, year).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }
        
        public void ListarReservasCamion(string matricula)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCamion(matricula).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }

        public void ListarReservasCamion(string matricula, int year)
        {
            _bindingList = new BindingList<Transporte>(MiEmpresa.ReservasCamion(matricula, year).ListaTransportes);
            var sourceTransportes = new WForms.BindingSource(_bindingList, null);
            //Enlazamos el datagrid con la lista de transportes
            View.grdLista.DataSource = sourceTransportes;
            ElTransporte = null;
            View.Actualizar();
            ActualizarPanelTransporte();
            View.pnlTransporte.ModoVolver();
        }
    }
}