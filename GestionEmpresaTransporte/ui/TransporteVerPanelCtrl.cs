namespace GestionEmpresaTransporte.ui
{
    using WForms = System.Windows.Forms;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using GestionEmpresaTransporte.Core;
    class TransporteVerPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar
        }

        private readonly BindingList<Transporte> _bindingList;
        public WForms.Control _padre;
        private Transporte miTransporte;

  
        private TransporteVerPanelCtrl(Empresa miEmpresa)
        {
            MiEmpresa = miEmpresa;
            View = new TransporteVerPanelView(MiEmpresa.ColeccionClientes.ListaNifs(), MiEmpresa.ColeccionVehiculos.ListaMatriculas());
            View.BtInsertar.Click += (sender, e) => InsertarTransporte();
            View.BtAceptar.Click += (sender, e) => Aceptar();
            View.BtCancelar.Click += (sender, e) => Cancelar();
            View.BtBorrar.Click += (sender, e) => BorrarTransporte();
            View.BtModificar.Click += (sender, e) => View.ModoModificar();
            EstadoPnlTransporte = Estados.Consultar;
            View.ModoConsulta();
        }

        public TransporteVerPanelCtrl(BindingList<Transporte> unaBindingList, Empresa miEmpresa) : this(miEmpresa)
        {
            _bindingList = unaBindingList;
        }

        public TransporteVerPanelCtrl(Empresa miEmpresa, Transporte unTransporte) : this(miEmpresa)
        {
            ElTransporte = unTransporte;
        }

        public ColeccionTransportes ColeccionTransportes { get; set; }

        public Empresa MiEmpresa { get; }

        public TransporteVerPanelView View { get; }


        public Transporte ElTransporte
        {
            get => miTransporte;
            set
            {
                miTransporte = value;
                ActualizaTextTransporte();
            }
        }

        public Estados EstadoPnlTransporte { get; set; }


        private void ActualizaTextTransporte()
        {
            if (ElTransporte != null)
            {
                View.EdCliente.Text = ElTransporte.Cliente.Nif;
                View.EdFlota.Text = ElTransporte.Camion.Matricula;
                View.EdFechaContratacion.Text = DateTime.ParseExact(ElTransporte.FechaContratacion, "yyyyMMdd", CultureInfo.InvariantCulture).ToString();
                View.EdKmsRecorridos.Text = Convert.ToString(ElTransporte.KmRecorridos);
                View.EdFechaSalida.Text = DateTime.ParseExact(ElTransporte.FechaSalida, "yyyyMMdd", CultureInfo.InvariantCulture).ToString();
                View.EdFechaEntrega.Text = DateTime.ParseExact(ElTransporte.FechaEntrega, "yyyyMMdd", CultureInfo.InvariantCulture).ToString();
                View.EdImporteDia.Text = Convert.ToString(ElTransporte.ImportePorDia);
                View.EdIVA.Text = Convert.ToString(ElTransporte.IVA);
                View.EdPrecioLitro.Text = Convert.ToString(ElTransporte.PrecioLitro);
                View.EdGas.Text = Convert.ToString(ElTransporte.GasConsumido);
                View.EdFactura.Text = ElTransporte.ToString();
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
                    ConsultarTransporte();
                    return;
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.ModoConsulta();
            ActualizaTextTransporte();
        }

        private void ConsultarTransporte()
        {
            throw new NotImplementedException();
        }

        private void BorrarTransporte()
        {
            if (ElTransporte != null)
            {
                /*MessageBOX de confirmacion*/
                var message = string.Format("¿Estás seguro de borrar el transporte con identificador: {0}?", ElTransporte.IdTransporte);
                var caption = "Borrar Transporte";
                var buttons = WForms.MessageBoxButtons.YesNo;
                WForms.DialogResult result;
                // Displays the MessageBox.
                result = WForms.MessageBox.Show(message, caption, buttons);
                if (result == WForms.DialogResult.Yes) _bindingList.Remove(ElTransporte);

                ActualizarPadre();
                View.ModoConsulta();
            }
        }

        private void InsertarTransporte()
        {
            View.ModoInsercion();
            EstadoPnlTransporte = Estados.Insertar;
        }

        
        private void ModificarTransporte()
        {
            //Si las validaciones son correctas se actulizan los datos del cliente
            if (ComprobarDatos())
            {
                ElTransporte.KmRecorridos = Convert.ToInt32(View.EdKmsRecorridos.Value);
                ElTransporte.FechaSalida = View.EdFechaSalida.Value.ToString("yyyyMMdd");
                ElTransporte.FechaEntrega = View.EdFechaEntrega.Value.ToString("yyyyMMdd");
                ElTransporte.ImportePorDia = Convert.ToDouble(View.EdImporteDia.Value);
                ElTransporte.IVA = Convert.ToDouble(View.EdIVA.Value);
                ElTransporte.PrecioLitro = Convert.ToDouble(View.EdPrecioLitro.Value);
                ElTransporte.GasConsumido = Convert.ToDouble(View.EdGas.Value);
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
                var nifCliente = View.EdCliente.Text;
                var matricula = View.EdFlota.Text;
                var fechaContratacion = View.EdFechaContratacion.Value.ToString("yyyyMMdd");
                var kmRecorridos = Convert.ToInt32(View.EdKmsRecorridos.Value);
                var fechaSalida = View.EdFechaSalida.Value.ToString("yyyyMMdd");
                var fechaEntrega = View.EdFechaEntrega.Value.ToString("yyyyMMdd");
                var importeDia = Convert.ToDouble(View.EdImporteDia.Value);
                var iva = Convert.ToDouble(View.EdIVA.Value);
                var precioLitro = Convert.ToDouble(View.EdPrecioLitro.Value);
                var gas = Convert.ToDouble(View.EdGas.Value);

                var vehiculo = this.MiEmpresa.ColeccionVehiculos.RecuperarVehiculo(matricula);
                var cliente = this.MiEmpresa.ColeccionClientes.getClientebyNif(nifCliente);
                var precioTotal = CalcularPrecioTotal(vehiculo, kmRecorridos, fechaSalida, fechaEntrega, importeDia, precioLitro, gas);
                
                var nuevoTransporte = new Transporte(vehiculo, cliente, fechaContratacion, kmRecorridos,
                                                     fechaSalida, fechaEntrega, importeDia, iva, precioLitro,
                                                     gas, precioTotal);
                _bindingList.Add(nuevoTransporte);
                //ActualizarPadre();
                View.ModoConsulta();
            }
        }

        private void ActualizarPadre()
        {
            if (_padre != null)
            {
                _padre.Update();
                _padre.Refresh();
            }
        }

        private Boolean ComprobarDatos()
        {
            var valido = true;
            var nifCliente = View.EdCliente.Text;
            var matricula = View.EdFlota.Text;
            var fechaContratacion = View.EdFechaContratacion.Value.ToString("yyyyMMdd");
            var kmRecorridos = Convert.ToInt32(View.EdKmsRecorridos.Value);
            var fechaSalida = View.EdFechaSalida.Value.ToString("yyyyMMdd");
            var fechaEntrega = View.EdFechaEntrega.Value.ToString("yyyyMMdd");
            var importeDia = Convert.ToDouble(View.EdImporteDia.Value);
            var iva = Convert.ToDouble(View.EdIVA.Value);
            var precioLitro = Convert.ToDouble(View.EdPrecioLitro.Value);
            var gas = Convert.ToDouble(View.EdGas.Value);

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
            else if (string.IsNullOrWhiteSpace(fechaContratacion) || !utilidades.IsValidFechaContratacion(fechaContratacion, fechaSalida, fechaEntrega))
            {
                WForms.MessageBox.Show("La fecha de contratación debe ser igual o anterior a las fechas de entrega y salida");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(Convert.ToString(kmRecorridos)) || kmRecorridos <= 0.0)
            {
                WForms.MessageBox.Show("Los km Recorridos deben ser mayor que 0");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(fechaSalida) || !utilidades.IsValidFechaSalida(fechaContratacion, fechaSalida))
            {
                WForms.MessageBox.Show("La fecha de salida debe ser posterior a la de contratacion");
                valido = false;
            }
            else if (string.IsNullOrWhiteSpace(fechaEntrega) || !utilidades.IsValidFechaEntrega(fechaSalida, fechaEntrega))
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

        private double CalcularPrecioTotal(Vehiculo camion, int kmRecorridos, string fechaSalida, string fechaEntrega, double importeDia, double precioLitro, double gas)
        {
        

            double numd = (DateTime.ParseExact(fechaEntrega, "yyyyMMdd", CultureInfo.InvariantCulture) - DateTime.ParseExact(fechaSalida, "yyyyMMdd", CultureInfo.InvariantCulture)).TotalDays;
            var suplencia = CalcularSuplencia(numd);
            double ppkm = 3 * Convert.ToDouble(camion.Consumo) * precioLitro;
            var precioTotal = (numd * importeDia * suplencia) + (Convert.ToDouble(kmRecorridos) * ppkm) + gas;

            return precioTotal;
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

    }
}
