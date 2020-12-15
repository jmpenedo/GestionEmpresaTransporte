using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using GestionEmpresaTransporte.Core;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteListarPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar
        }

        private readonly BindingList<Cliente> _bindingList;
        private Cliente miCliente;
        private int tipoGrafico;


        public ClienteListarPanelCtrl(Empresa unaEmpresa)
        {
            MiEmpresa = unaEmpresa;
            _bindingList = new BindingList<Cliente>(unaEmpresa.ColeccionClientes.Clientes);
            var sourceClientes = new WForms.BindingSource(_bindingList, null);
            View = new ClienteListarPanelView();
            //Enlazamos el datagrid con la lista de clientes
            View.grdLista.DataSource = sourceClientes;
            //Asignamos Handlers
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelCliente();
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();
            View.pnlCliente.BtInsertar.Click += (sender, e) => ModoInsertarCliente();
            View.pnlCliente.BtAceptar.Click += (sender, e) => Aceptar();
            View.pnlCliente.BtCancelar.Click += (sender, e) => Cancelar();
            View.pnlCliente.BtBorrar.Click += (sender, e) => BorrarCliente();
            View.pnlCliente.BtModificar.Click += (sender, e) => ModoModificar();
            View.pnlCliente.BtVolver.Click += (sender, e) => Volver();
            View.pnlCliente.BtSeleccionar.Click += (sender, e) => Seleccionar();
            View.pnlCliente.BtReservasCliente.Click += (sender, e) => Reservas();
            View.pnlCliente.BtReservasClienteYear.Click += (sender, e) => ReservasYear();
            View.pnlCliente.BtTipoGrafico.Click += (sender, e) => CambiarTipoGrafico();
            View.grdLista.CellDoubleClick += (sender, args) => Seleccionar(); //FIX 20201214830Unificada seleccion
            EstadoPnlCliente = Estados.Consultar; //Al crearse siempre está en modo consulta
            tipoGrafico = 0;
        }


        public ClienteListarPanelCtrl(Empresa empresa, MainWindowCtrl controlPrincipal) : this(empresa)
        {
            MainWindowControl = controlPrincipal;
        }

        public Estados EstadoPnlCliente { get; set; }
        public ClienteListarPanelView View { get; }
        private Empresa MiEmpresa { get; }

        public MainWindowCtrl MainWindowControl { get; set; }

        public Cliente ElCliente
        {
            get => miCliente;
            set
            {
                miCliente = value;
                ActualizaTextCliente();
            }
        }

        private void CambiarTipoGrafico()
        {
            if (tipoGrafico == 0)
                tipoGrafico = 1;
            else
                tipoGrafico = 0;
            MostrarGrafico();
        }

        /// <summary>
        ///     Muestra la información  del cliente seleccionado
        ///     en el panel inferior
        /// </summary>
        public void ActualizarPanelCliente()
        {
            if (View.grdLista.Rows.Count > 0 && MiEmpresa.ColeccionClientes.Count > 0)
            {
                //Hay clientes seleccionados
                if (View.grdLista.SelectedRows.Count > 0)
                {
                    var nif = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                    ElCliente = _bindingList.FirstOrDefault(item => item.Nif == nif);
                    MostrarGrafico();
                }
            }
            else
            {
                ElCliente = null;
            } //NO hay cliente seleccionado

            ActualizaTextCliente();
            View.AjustarColGrid();
        }

        private void ActualizaTextCliente()
        {
            if (ElCliente != null)
            {
                View.pnlCliente.EdNif.Text = ElCliente.Nif;
                View.pnlCliente.EdNombre.Text = ElCliente.Nombre;
                View.pnlCliente.EdCorreo.Text = ElCliente.Email;
                View.pnlCliente.EdDireccion.Text = ElCliente.Dirección;
                View.pnlCliente.EdTelefono.Text = ElCliente.Telefono;
            }
            else
            {
                View.pnlCliente.EdNif.Clear();
                View.pnlCliente.EdNombre.Clear();
                View.pnlCliente.EdCorreo.Clear();
                View.pnlCliente.EdDireccion.Clear();
                View.pnlCliente.EdTelefono.Clear();
            }
        }

        /// <summary>
        ///     Accion a realizar cuando se pulsa ACEPTAR
        /// </summary>
        private void Aceptar()
        {
            switch (EstadoPnlCliente)
            {
                case Estados.Insertar:
                    AñadirCliente();
                    return;
                case Estados.Borrar:
                    BorrarCliente();
                    return;
                case Estados.Modificar:
                    ModificarCliente();
                    return;
                default:
                    return;
            }
        }

        private void Cancelar()
        {
            View.pnlCliente.ModoInicial();
            ActualizaTextCliente();
        }


        /// <summary>
        ///     Borra  ElCliente actual
        /// </summary>
        private void BorrarCliente()
        {
            if (ElCliente != null)
            {
                if (!MiEmpresa.ColeccionTransportes.ExisteCliente(ElCliente))
                {
                    var nif = ElCliente.Nif;
                    /*MessageBOX de confirmacion*/
                    var message = string.Format("¿Estás seguro de borrar el cliente con identificador: {0}?", nif);
                    var caption = "Borrar cliente";
                    var buttons = WForms.MessageBoxButtons.YesNo;
                    WForms.DialogResult result;
                    // Displays the MessageBox.
                    result = WForms.MessageBox.Show(message, caption, buttons);
                    if (result == WForms.DialogResult.Yes)
                        _bindingList.Remove(ElCliente);
                    //ActualizarPanelCliente();
                }
                else
                {
                    WForms.MessageBox.Show("El cliente tiene transportes asignados, no se puede borrar ");
                }
            }

            View.Actualizar();
            //View.pnlCliente.ModoConsulta();
            //ActualizarPanelCliente();
        }

        private void ModoInsertarCliente()
        {
            EstadoPnlCliente = Estados.Insertar;
            View.pnlCliente.ModoInsercion();
        }

        /// <summary>
        ///     Iniciamos el proceso de modificacion del ElCliente actual
        /// </summary>
        private void ModoModificar()
        {
            if (ElCliente != null)
            {
                EstadoPnlCliente = Estados.Modificar;
                View.pnlCliente.ModoModificar();
            }
        }

        /// <summary>
        ///     Recoge los datos de los textbox, valida el email y actualiza ElCliente
        ///     con los datos nuevos (NO se puede modificar el NIF)
        /// </summary>
        private void ModificarCliente()
        {
            var nombre = View.pnlCliente.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.pnlCliente.EdTelefono.Text);
            var correo = View.pnlCliente.EdCorreo.Text;
            var direccion = View.pnlCliente.EdDireccion.Text;
            var valido = true;
            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    valido = false;
                }

            //Si las validaciones son correctas se actulizan los datos del cliente
            if (valido)
            {
                if (ElCliente != null)
                {
                    ElCliente.Nombre = nombre;
                    ElCliente.Telefono = telefono;
                    ElCliente.Email = correo;
                    ElCliente.Dirección = direccion;
                    View.Actualizar();
                    View.pnlCliente.ModoInicial();
                }
                else
                {
                    Trace.WriteLine("Error intentando modificar cliente NULL");
                }
            }
        }

        /// <summary>
        ///     Para ejecutar al terminar de rellenar  el formulario y
        ///     pulsar aceptar. Crea el nuevo cliente
        /// </summary>
        private void AñadirCliente()
        {
            //Recogemos los datos del formulario
            var valido = true;
            var nif = View.pnlCliente.EdNif.Text.ToUpper();
            var nombre = View.pnlCliente.EdNombre.Text;
            var telefono = utilidades.stringToTelString(View.pnlCliente.EdTelefono.Text);
            var correo = View.pnlCliente.EdCorreo.Text;
            var direccion = View.pnlCliente.EdDireccion.Text;

            //El NIF es obligatorio, si no se ponde no se puede dar de alta
            if (!utilidades.valida_NIFCIFNIE(nif))
            {
                WForms.MessageBox.Show("El nif " + nif + " no es correcto");
                valido = false;
            }

            if (MiEmpresa.ColeccionClientes.getClientebyNif(nif) != null)
            {
                WForms.MessageBox.Show("Ya existe un cliente con el NIF: " + nif);
                valido = false;
            }

            //El correo, NO es obligatorio pero si se pone tiene que tener un formato correcto
            if (correo.Length > 0)
                if (!utilidades.IsValidEmail(correo))
                {
                    WForms.MessageBox.Show("El email " + correo + " no es correcto");
                    valido = false;
                }


            //Si  fue bien se crea un nuevo cliente
            if (valido)
            {
                var nuevoCliente = new Cliente(nif, nombre, telefono, correo, direccion);
                _bindingList.Add(nuevoCliente);
                SeleccionarCliente(nuevoCliente);
                View.pnlCliente.ModoInicial();
            }
        }

        /// <summary>
        ///     Accion que se ejecuta al pulsar el boton Volver
        /// </summary>
        private void Volver()
        {
            ElCliente = null;
            View.SendToBack(); //Vuelve al anterior panel llamado
            View.pnlCliente.ModoInicial();
            View.pnlCliente.ModoSeleccion(false);
        }

        /// <summary>
        ///     Accion que se ejecuta al pulsar el boton Seleccionar
        /// </summary>
        private void Seleccionar()
        {
            if (View.pnlCliente.BtSeleccionar.Visible)
            {
                //Antes de volver revisar que hay un cliente seleccionado
                if (ElCliente == null)
                {
                    ActualizarPanelCliente(); //Devolvemos el cliente seleccionado en el grid
                    if (ElCliente == null)
                        WForms.MessageBox.Show("NO se ha seleccionado ningún cliente"); //No hay clientes en la BD
                }

                View.SendToBack();
                View.pnlCliente.ModoInicial();
                View.pnlCliente.ModoSeleccion(false);
            }
        }

        /// <summary>
        ///     Selecciona en el gdrLista de clientes un cliente pasado como parámetro
        /// </summary>
        /// <param name="cliente"></param>
        public void SeleccionarCliente(Cliente cliente)
        {
            View.pnlCliente.BtVolver.Visible = true;
            var pos = MiEmpresa.ColeccionClientes
                .PosCliente(cliente);

            if (View.grdLista.Columns.Count > 0) View.grdLista.Rows[pos].Selected = true;
        }

        public void Reservas()

        {
            if (MainWindowControl != null) //FIX 20201214730 pasar el controlador del main
            {
                var nif = View.pnlCliente.EdNif.Text.ToUpper();
                MainWindowControl.getInstanceTransporte().ListarReservasCliente(nif);
                MainWindowControl.GestionTransportes();
            }
            else
            {
                Trace.WriteLine("MainWindowControl es null al intentar ver Reservas");
            }
        }

        public void ReservasYear()
        {
            if (MainWindowControl != null) //FIX 20201214730 pasar el controlador del main
            {
                var nif = View.pnlCliente.EdNif.Text.ToUpper();
                var year = Convert.ToInt32(View.pnlCliente.EdYearFiltro.Value);
                MainWindowControl.getInstanceTransporte().ListarReservasCliente(nif, year);
                MainWindowControl.GestionTransportes();
            }
            else
            {
                Trace.WriteLine("MainWindowControl es null al intentar ver ReservasYear");
            }
        }

        private void porCliente(string nif)
        {
            var anhos = new List<int>();
            var aux = new List<int>();

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
                if (nif.ToUpper() == transporte.Cliente.Nif)
                    anhos.Add(short.Parse(transporte.FechaContratacion.ToString("yyyy")));
            //Ordenamos amhos de menor a mayor

            anhos.Sort();
            //ELIMINO DUPLICADOS PARA SABER CUÁNTOS AÑOS HAY exactamente
            aux = anhos.Distinct().ToList();

            var values = new int[aux.Count];

            foreach (var anho in anhos)
                for (var i = 0; i < aux.Count; i++)
                    if (anho == aux[i])
                        values[i]++;

            if (values.Length > 0)
            {
                View.pnlCliente.pnlChart.Controls.Clear();
                View.pnlCliente.Chart = new Chart(500,
                    500)
                {
                    Dock = WForms.DockStyle.Fill
                };
                View.pnlCliente.pnlChart.Controls.Add(View.pnlCliente.Chart);
                View.pnlCliente.Chart.LegendY = "Cant. transpt.";
                View.pnlCliente.Chart.LegendX = "Años";
                View.pnlCliente.Chart.Visible = true;
                View.pnlCliente.Chart.Values = values;
                View.pnlCliente.Chart.Draw();
            }
            else
            {
                View.pnlCliente.Chart.Visible = false;
            }
        }

        private void MostrarGrafico()
        {
            var nif = View.pnlCliente.EdNif.Text;
            switch (tipoGrafico)
            {
                case 0:
                    porCliente(nif);
                    break;
                case 1:
                    porClienteAnho(nif);
                    break;
                default:
                    porCliente(nif);
                    break;
            }
        }

        private void porClienteAnho(string nif)
        {
            var meses = new List<int>();
            var values = new int[12];

            foreach (var transporte in MiEmpresa.ColeccionTransportes)
                if (nif == transporte.Cliente.Nif)
                    meses.Add(short.Parse(transporte.FechaContratacion.ToString("MM")));
            meses.Sort();

            foreach (var mes in meses) values[mes - 1]++;

            if (values.Max() > 0) //revisar...
            {
                View.pnlCliente.pnlChart.Controls.Clear();
                View.pnlCliente.Chart = new Chart(500,
                    500)
                {
                    Dock = WForms.DockStyle.Fill
                };
                View.pnlCliente.pnlChart.Controls.Add(View.pnlCliente.Chart);
                View.pnlCliente.Chart.LegendY = "Cant. transpt.";
                View.pnlCliente.Chart.LegendX = "Meses";
                View.pnlCliente.Chart.Visible = true;
                View.pnlCliente.Chart.Values = values;
                View.pnlCliente.Chart.Draw();
            }
            else
            {
                View.pnlCliente.Chart.Visible = false;
            }
        }
    }
}